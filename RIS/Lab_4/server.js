const express = require('express');
const bodyParser = require('body-parser');
const sqlite3 = require('sqlite3').verbose();
const moment = require('moment');
const fs = require('fs');
const morgan = require('morgan');
const axios = require('axios');

const app = express();
const PORT = 3000;

const db = new sqlite3.Database('./server/db.sqlite');

const axiosInstance = axios.create({
    validateStatus: function (status) {
      return status >= 200 && status < 300 || status === 304;
    }
  });

db.serialize(() => {
    db.run(`CREATE TABLE IF NOT EXISTS data (
        id INTEGER PRIMARY KEY AUTOINCREMENT,
        value TEXT,
        timestamp TEXT
    )`);
});

app.use(bodyParser.json());

app.use(morgan('combined', { 
    stream: fs.createWriteStream('./server/server.log', { flags: 'a' }) 
}));

app.post('/data', async (req, res) => {
    const { value } = req.body;
    const timestamp = moment().format('YYYY-MM-DD HH:mm:ss');

    try {
        await insertData(value, timestamp);
        console.log('Data inserted successfully');
        res.status(200).json({ message: 'Data inserted successfully' });
    } catch (error) {
        console.error('Error inserting data:', error);
        res.status(500).json({ error: 'Error inserting data' });
    }
});

app.use((err, req, res, next) => {
    if (err.code === 'ECONNREFUSED' || err.code === 'ENOTFOUND') {
        console.error('Error connecting to remote service:', err);
        res.status(500).json({ error: 'Error connecting to remote service' });
    } else {
        next(err);
    }
});

function insertData(value, timestamp) {
    return new Promise((resolve, reject) => {
        db.run('INSERT INTO data (value, timestamp) VALUES (?, ?)', [value, timestamp], (err) => {
            if (err) {
                reject(err);
            } else {
                resolve();
            }
        });
    });
}

async function synchronizeTime() {
    try {
        const lastTimestamp = await getLastTimestamp();
        const currentTime = moment().format('YYYY-MM-DD HH:mm:ss');
        console.log(`Synchronized time with last timestamp from database: ${lastTimestamp}`);
        console.log(`Current server time: ${currentTime}`);
    } catch (error) {
        console.error('Error synchronizing time:', error);
    }
}

function getLastTimestamp() {
    return new Promise((resolve, reject) => {
        db.get('SELECT MAX(timestamp) AS lastTimestamp FROM data', (err, row) => {
            if (err) {
                reject(err);
            } else {
                const lastTimestamp = row ? row.lastTimestamp : null;
                resolve(lastTimestamp);
            }
        });
    });
}

setInterval(synchronizeTime, 5000);

const clientStates = { 
    3001: { state: 'IDLE', retries: 0 },
    3002: { state: 'IDLE', retries: 0 }
};

function updateClientState(clientId, state) {
    if (!clientStates.hasOwnProperty(clientId)) {
        clientStates[clientId] = { state: 'IDLE', retries: 0 };
    }

    if (clientStates[clientId].state === 'DEAD' && state === 'WAIT') {
        clientStates[clientId] = { state: 'DEAD', retries: 0 };
        return;
    }

    if (state === 'OK') {
        clientStates[clientId] = { state: 'OK', retries: 0 };
    } else if (state === 'WAIT') {
        clientStates[clientId] = { state: 'WAIT', retries: clientStates[clientId].retries + 1 };
    }

    if (clientStates[clientId].retries >= 3 && clientStates[clientId].state !== 'DEAD') {
        clientStates[clientId] = { state: 'DEAD', retries: 0 };
    }
}

const clientIds = Object.keys(clientStates);

async function pollClients() {
    for (const clientId of clientIds) {
        try {
            await axiosInstance.get(`http://localhost:${clientId}/status`);
            updateClientState(clientId, 'OK');
        } catch (error) {
            updateClientState(clientId, 'WAIT');
            console.log(clientStates);
        }
    }
}

setInterval(pollClients, 5000);

app.listen(PORT, () => {
    console.log(`Server is running on http://localhost:${PORT}`);
});
