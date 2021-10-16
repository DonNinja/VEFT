const { ObjectId } = require('mongodb')
const { execDatabaseQuery } = require('../data/db');
const { NotFoundError } = require('./errors');

const getPlayerById = async (id) =>
        await execDatabaseQuery(
            (async connection => {
                // TODO: implement not found error
                var player = await connection.collection("Players").findOne({'_id': ObjectId(id), "deleted": false});
                if (player == undefined)
                {
                    throw new NotFoundError();
                }
                player['id'] = player['_id']
                return player
            })
        )

module.exports = {
    
    getPlayerById,
    queries: {
        allPlayers: () => execDatabaseQuery(
            (async connection => {
                const cursor = connection.collection("Players").find({"deleted":false});
                var players = []
                while (await cursor.hasNext()) {
                    var player = await cursor.next()
                    player['id'] = player['_id']
                    players.push(player);
                }
                return players
            })
        ),
        player: async (a, b) => await getPlayerById(b.id)
    },
    mutations:
    {
        createPlayer: (a,b) => execDatabaseQuery(
            (async connection =>
            {
                const newPlayer = {
                    name: b.input.name,
                    playedGames: [],
                    deleted: false
                }
                var id = await connection.collection("Players").insertOne(newPlayer)
                return await getPlayerById(id.insertedId)
            })
        ),
        updatePlayer: (a,b) => execDatabaseQuery(
            (async connection =>{
                connection.collection("Players").updateOne({_id:ObjectId(b.id)}, {$set: {name:b.name}})
                return await getPlayerById(b.id)
            })
        ),
        removePlayer: (a,b) => execDatabaseQuery(
            (async connection => {
                var player = await getPlayerById(b.id)
                // var allGames = await connection.collection("Pickup Games").find({_id: {$in:player['playedGames'] }})

                const { removePlayerFromPickupGameTheFunction } = require('./pickupGameResolver');
                player['playedGames'].forEach(i => 
                    {
                    removePlayerFromPickupGameTheFunction({Playerid: b.id, Gameid:i.toString() })
                    })

                var resp = await connection.collection("Players").updateOne({_id:ObjectId(b.id)}, {$set: {deleted:true}})
                return true
            })
        ),

    },
}