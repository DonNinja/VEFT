const mongoose = require('mongoose');
const sharkSchema = require('../schemas/sharkSchema');
const attackSchema = require('../schemas/attackSchema');
const areaSchema = require('../schemas/areaSchema');

const connection = mongoose.createConnection('mongodb+srv://yngvi:goodpassword@spy-on-shark.qeno6.mongodb.net/spy-on-shark?retryWrites=true&w=majority', {
    useNewUrlParser: true,
    useUnifiedTopology: true
});

module.exports = {
    Shark: connection.model('Shark', sharkSchema),
    Attack: connection.model('Attack', attackSchema),
    Area: connection.model('Area', areaSchema),
    connection
};

