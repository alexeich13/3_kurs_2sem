const axios = require('axios');
const sqlite3 = require('sqlite3').verbose();

const db = new sqlite3.Database('generator_db.sqlite'); 
const db_server = new sqlite3.Database('serverDB.sqlite'); 
const client_remote_db = new sqlite3.Database('client_local_db.sqlite'); 
const client_local_db = new sqlite3.Database('client_remote_db.sqlite'); 

db.serialize(() => {
    db.run("CREATE TABLE IF NOT EXISTS generator_data (id INTEGER PRIMARY KEY AUTOINCREMENT, timestamp TEXT, creatorNumber INTEGER, itNew INTEGER)");
});
db_server.serialize(() => {
    db.run("CREATE TABLE IF NOT EXISTS data (id INTEGER PRIMARY KEY AUTOINCREMENT, timestamp TEXT, creatorNumber INTEGER, itNew INTEGER)");
});
client_remote_db.serialize(() => {
    client_remote_db.run("CREATE TABLE IF NOT EXISTS local_data (id INTEGER PRIMARY KEY AUTOINCREMENT, timestamp TEXT, creatorNumber INTEGER, itNew INTEGER)");
});
client_local_db.serialize(() => {
    client_local_db.run("CREATE TABLE IF NOT EXISTS local_data (id INTEGER PRIMARY KEY AUTOINCREMENT, timestamp TEXT, creatorNumber INTEGER, itNew INTEGER)");
});

async function generateNewData() {
    const timestamp = new Date().toISOString();

    try {
        db.run("INSERT INTO generator_data (timestamp, creatorNumber, itNew) VALUES (?, ?, ?)",
                [timestamp, 0, 1],
                function(err) {
                    if (err) {
                        console.error(err.message);
                    } else {
                        console.log(`Data added to local database with rowid ${this.lastID}`);
                    }
                });
        db.run("INSERT INTO generator_data (timestamp, creatorNumber, itNew) VALUES (?, ?, ?)",
                [timestamp, 0, 1],
                function(err) {
                    if (err) {
                        console.error(err.message);
                    } else {
                        console.log(`Data added to local database with rowid ${this.lastID}`);
                    }
                });
    } catch (error) {
        console.error('Error insert data to server:', error);
    }
}

async function sendDataToLocalDB() {
    const timestamp = new Date().toISOString();

    try {
        db_server.all("SELECT * FROM data WHERE creatorNumber = ?", [1], (err, rows) => {
            if (err) {
                console.error(err.message);
            } else {
                rows.forEach(row => {
                    client_remote_db.run("INSERT INTO local_data (timestamp, creatorNumber, itNew) VALUES (?, ?, ?)",
                        [timestamp, row.creatorNumber, 1],
                        function(err) {
                            if (err) {
                                console.error(err.message);
                            } else {
                                console.log(`Data added to local database with rowid ${this.lastID}`);
                            }
                        });
                });
            }
        });
        db_server.all("SELECT * FROM data WHERE creatorNumber = ?", [2], (err, rows) => {
            if (err) {
                console.error(err.message);
            } else {
                rows.forEach(row => {
                    client_local_db.run("INSERT INTO local_data (timestamp, creatorNumber, itNew) VALUES (?, ?, ?)",
                        [timestamp, row.creatorNumber, 1],
                        function(err) {
                            if (err) {
                                console.error(err.message);
                            } else {
                                console.log(`Data added to local database with rowid ${this.lastID}`);
                            }
                        });
                });
            }
        });
    } catch (error) {
        console.error('Error sending data to server:', error);
    }
}

function callGenerateNewDataMultipleTimes() {
    let callCount = 0;
    const interval = setInterval(() => {
        generateNewData();
        callCount++;
        if (callCount === 5) {
            clearInterval(interval);
            setTimeout(sendDataToLocalDBMultipleTimes, 10000);
        }
    }, 5000);
}

async function sendDataToLocalDBMultipleTimes() {
    for (let i = 0; i < 5; i++) {
        await sendDataToLocalDB();
        await new Promise(resolve => setTimeout(resolve, 10000)); 
    }
}

callGenerateNewDataMultipleTimes();