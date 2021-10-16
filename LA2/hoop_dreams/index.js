const { ApolloServer } = require('apollo-server');

const typeDefs = require('./schema');
const resolvers = require('./resolvers');

// console.log(typeDefs)
// console.log(resolvers)
const server = new ApolloServer({
    typeDefs,
    resolvers
});

server.listen()
    .then(({ url }) => console.log(`GraphQL Service is running on ${ url }`));
