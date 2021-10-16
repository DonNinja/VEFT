const { MongoClient } = require('mongodb');

const connectionString = 'mongodb+srv://mvy19:pass123@hoopdreams.avdi1.mongodb.net/HoopDreamsdb?retryWrites=true&w=majority';

module.exports.execDatabaseQuery = async fn => {
    const connection = new MongoClient(connectionString, {
        useUnifiedTopology: true,
        useNewUrlParser: true
    });
    try {
        await connection.connect();
        return await fn(connection.db('HoopDreamsdb'));
    } 
    finally {
        await connection.close();
    }
}
