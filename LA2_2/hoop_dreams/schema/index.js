const types = require("./types");
const scalar = require("./scalar");
const queries = require("./queries");
const enums = require("./enums");
const mutations = require("./mutations");
const inputs = require("./input");

module.exports = `
    ${queries}
    ${types}
    ${scalar}
    ${enums}
    ${mutations}
    ${inputs}
`;