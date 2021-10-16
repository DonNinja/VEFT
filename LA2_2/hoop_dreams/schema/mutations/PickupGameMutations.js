module.exports = `
    createPickupGame(input: PickupGameInput!): PickupGame!
    removePickupGame(id: String!): Boolean!
    addPlayerToPickupGame(Playerid: String!, Gameid: String!): PickupGame!
    removePlayerFromPickupGame(Playerid: String!, Gameid: String!): Boolean!
`;