const { ObjectId } = require('mongodb')
const { execDatabaseQuery } = require('../data/db');
const { getBasketballFieldById } = require('./basketballFieldResolver')
const { getPlayerById } = require('./playerResolver')
const { NotFoundError, BasketballFieldClosedError, PickupGameOverlapError, EndBeforeStartError, DateBeforeCurrentDateError, InvalidTimeLengthError, PickupGameAlreadyPassedError, PickupGameExceedMaximumError, PlayerAlreadyPlayingError, PlayerNotPlayingError } = require('./errors')

const moment = require('moment')

const getPickupGameById = async (id) => await execDatabaseQuery(
            (async connection =>{
                var game = await connection.collection("Pickup Games").findOne({"_id": ObjectId(id), "deleted": false});
                if (game == undefined) {
                   throw new NotFoundError()
                }
                game['id'] = game['_id'] // Set 'id' to the same as '_id'
                return game
            })
        )


const removePlayerFromPickupGameTheFunction = async (b) =>
execDatabaseQuery(
          async connection =>{
              var deleted = false
              var field = await getPickupGameById(b.Gameid)
              if (field == undefined)
              {
                  throw new NotFoundError();
              }

              if (field['end'] <= moment().format())
              {
                throw new PickupGameAlreadyPassedError();
              }

              var player = field['registeredPlayers'].filter(item => item._id.equals(ObjectId(b.Playerid))) // gets player from basketball field
              if (player.length == 0) { // If player does not exist
                throw new PlayerNotPlayingError();
              }

              var out = field['registeredPlayers'].filter(item => !item._id.equals(ObjectId(b.Playerid))) // get's every player except the one to remove

              if (out.length == 0)
              {
                var changed = await connection.collection("Pickup Games").updateOne({_id:ObjectId(b.Gameid)}, {$set: {deleted:true}})
              }
              else
              {
                if (field['host']._id == b.Playerid)
                {
                    out.sort((a,b) => a.name > b.name)
                    host['host'] = out[0];
                }
                var changed = await connection.collection("Pickup Games").updateOne({_id:ObjectId(b.Gameid)}, {$set: {registeredPlayers: out}})
              }
              return changed['modifiedCount']; // Return if modifiedCount (0 if failed, 1 if true)
          }
      )


module.exports = {
    queries: {
        allPickupGames: () => execDatabaseQuery(
            (async connection => {
                const cursor = connection.collection("Pickup Games").find({"deleted": false});
                var games = []
                while (await cursor.hasNext()) {
                    var game = await cursor.next();
                    game['id'] = game['_id']
                    games.push(game)
                }
                return games
            })
        ),
        pickupGame: async (a, b) => getPickupGameById(b.id)
    },

    mutations:{
    createPickupGame: async (a,b) => await execDatabaseQuery(
            (async connection =>{
                var host = await getPlayerById(b.input.hostId)

                host['id'] = null
                var location = await getBasketballFieldById(b.input.basketballFieldId)

                if (location['status'] == 'CLOSED')
                {
                    throw new BasketballFieldClosedError();
                }

                if (moment(b.input.end) < moment(b.input.start)) {
                    throw new EndBeforeStartError();
                }

                if (moment(b.input.start) < moment()) {
                    throw new DateBeforeCurrentDateError();
                }
                
                var gamesOnCourt = await connection.collection("Pickup Games").find({
                    location,
                    start:{ $gte : b.input.start, $lte : b.input.end},
                    end: {$gte : b.input.start, $lte : b.input.end },
                    "deleted": false
                });
                
                if (await gamesOnCourt.hasNext()) {
                    throw new PickupGameOverlapError(); 
                }

                var leng = moment.duration(moment(b.input.end).diff(moment(b.input.start))) / 1000;

                if (leng < 5 * 60 || leng > 2 * 60 * 60) {
                    throw new InvalidTimeLengthError();
                }

                const newGame = {
                    start : b.input.start,
                    end : b.input.end,
                    location,
                    registeredPlayers : [host],
                    host,
                    deleted: false
                }
                var id = await connection.collection("Pickup Games").insertOne(newGame)
                var newGamePlus = await getPickupGameById(id.insertedId)
                host['playedGames'].push(newGamePlus['_id'])
                await connection.collection("Players").updateOne({_id:ObjectId(b.input.hostId)}, {$set: {playedGames:host['playedGames']}})
                return newGamePlus
            })
        ),
    removePickupGame: async (a,b) => await execDatabaseQuery(
        (async connection =>{
            var resp = await connection.collection("Pickup Games").updateOne({_id:ObjectId(b.id)}, {$set: {deleted:true}})
            if (resp == undefined)
            {
                throw new NotFoundError()
            }
            return resp.acknowledged
        })
    ),
     addPlayerToPickupGame: (a,b) => execDatabaseQuery(
      (async connection =>{

        var player = await getPlayerById(b.Playerid)
        var game = await connection.collection("Pickup Games").findOne({
            _id: ObjectId(b.Gameid),
            "deleted": false
        });

        if (game == undefined)
        {
            throw new NotFoundError();
        }

        if (game['end'] <= moment().format() )
        {
            throw new PickupGameAlreadyPassedError();
        }

        var pList = game['registeredPlayers'];

        var exists = pList.filter(item => !item._id.equals(ObjectId(b.Playerid)))

        if (exists.length == 0) 
        {
            throw new PlayerAlreadyPlayingError();
        }

        if (game['location']['capacity'] <= pList.length)
        {
            throw new PickupGameExceedMaximumError();
        }

        player['id'] = player['_id']

        pList.push(player)
        await connection.collection("Pickup Games").updateOne({_id:ObjectId(b.Gameid)}, {$set: {registeredPlayers: pList}})
        player['playedGames'].push(game['_id'])
        await connection.collection("Players").updateOne({_id:ObjectId(b.Playerid)}, {$set: {playedGames:player['playedGames']}})
        return await game
      })
      ),
      removePlayerFromPickupGame: (a,b) => removePlayerFromPickupGameTheFunction(b)
        },
    getPickupGameById,
    removePlayerFromPickupGameTheFunction,
}