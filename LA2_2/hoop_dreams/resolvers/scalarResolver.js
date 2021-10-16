const { GraphQLScalarType, Kind } = require('graphql');
const moment = require('moment');

const momentScalar = new GraphQLScalarType({
  name: 'Date',
  description: 'Date custom scalar type',
  serialize(value) {
    return value.format('llll'); // Convert outgoing moment to integer for JSON
  },
  parseValue(value) {
    return new moment(value); // Convert incoming integer to moment
  },
  parseLiteral(ast) {
    if (ast.kind === Kind.INT) {
      return new moment(parseInt(ast.value, 10)); // Convert hard-coded AST string to integer and then to Date
    }
    return null; // Invalid hard-coded value (not an integer)
  },
});

module.exports.momentScalar=momentScalar