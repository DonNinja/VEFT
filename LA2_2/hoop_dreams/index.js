const { ApolloServer } = require('apollo-server');
const moment = require('moment')
moment.locale('is')
const typeDefs = require('./schema');
const resolvers = require('./resolvers');

const server = new ApolloServer({
    typeDefs,
    resolvers
});

server.listen()
    .then(({ url }) => console.log(`GraphQL Service is running on ${ url }`));
