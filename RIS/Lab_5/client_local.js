const axios = require('axios');
const sqlite3 = require('sqlite3').verbose();

const db = new sqlite3.Database('client_local_db.sqlite'); 

db.serialize(() => {
    db.run("CREATE TABLE IF NOT EXISTS local_data (id INTEGER PRIMARY KEY AUTOINCREMENT, timestamp TEXT, creatorNumber INTEGER, itNew INTEGER)");
});

async function getDataFromServer() {
    try {
        const response = await axios.get('http://172.20.10.10:3000/generate', {
            params: {
                clientNumber: '2' 
            }
        });
        const data = response.data;
        console.log('Received data from server:', data);

        await sendDataToServerAndLocalDB(data);
    } catch (error) {
    }
}

async function sendDataToServerAndLocalDB(data) {
    try {
        const response = await axios.post('http://172.20.10.10:3000/data', data);

        if (response.status === 200 && response.data.success) {
            db.run("INSERT INTO local_data (timestamp, creatorNumber, itNew) VALUES (?, ?, ?)",
                [data.timestamp, data.creatorNumber, data.itNew],
                function(err) {
                    if (err) {
                        console.error(err.message);
                    } else {
                        console.log(`Data added to local database with rowid ${this.lastID}`);
                    }
                });
        } else {
        }
    } catch (error) {
    }
}

setInterval(() => {
    getDataFromServer();
}, 5000);