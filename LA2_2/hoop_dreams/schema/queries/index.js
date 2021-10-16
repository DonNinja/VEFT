module.exports = `
    type Query {
        allBasketballFields: [BasketballField!]!
        allPickupGames: [PickupGame!]!
        allPlayers: [Player!]!
        basketballField(id: String!): BasketballField!
        player(id: String!): Player!
        pickupGame(id: String!): PickupGame!
    }
`;