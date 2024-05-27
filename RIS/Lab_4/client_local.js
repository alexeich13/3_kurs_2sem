const axios = require('axios');
const moment = require('moment');
const express = require('express');
const app = express();
const PORT = 3002;

async function sendData(value) {
    try {
        const timestamp = moment().format('YYYY-MM-DD HH:mm:ss');
        const response = await axios.post('http://172.20.10.10:3000/data', { value, timestamp });
        console.log(response.data);
    } catch (error) {
        console.error('Error sending data:', error.message);
    }
}

setInterval(() => {
    sendData('Some data from local machine');
}, 5000);

app.get('/status', (req, res) => {
    res.sendStatus(200);
});

app.post('/status', (req, res) => {
    console.log('Received status update from server');
    res.sendStatus(200);
});

app.listen(PORT, () => {
    console.log(`Client is running on http://localhost:${PORT}`);
});
