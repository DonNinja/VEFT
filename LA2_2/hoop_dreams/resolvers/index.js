const momentScalar = require('./scalarResolver');
const basketballFieldResolver = require('./basketballFieldResolver');
const playerResolver = require('./playerResolver');
const pickupGameResolver = require('./pickupGameResolver');

const resolvers = { 
    Query:{
        ...basketballFieldResolver.queries,
        ...playerResolver.queries,
        ...pickupGameResolver.queries,
    },
    Mutation:{
        ...pickupGameResolver.mutations,
        ...playerResolver.mutations
    },
    Moment: momentScalar
};
module.exports = resolvers;