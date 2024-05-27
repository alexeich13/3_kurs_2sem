const express = require('express');
const bodyParser = require('body-parser');
const sqlite3 = require('sqlite3').verbose();

const app = express();
const port = 3000;

const db = new sqlite3.Database('serverDB.sqlite'); 

db.serialize(() => {
    db.run("CREATE TABLE IF NOT EXISTS data (id INTEGER PRIMARY KEY AUTOINCREMENT, timestamp TEXT, creatorNumber INTEGER, itNew INTEGER)");
});

app.use(bodyParser.json());

app.get('/generate', (req, res) => {
    try {
        const clientNumber = req.query.clientNumber;
    const timestamp = new Date().toISOString();

    let responseData;

    if (clientNumber === '1') {
        responseData = { timestamp, creatorNumber: 1, itNew: 1 };
    } else if (clientNumber === '2') {
        responseData = { timestamp, creatorNumber: 2, itNew: 1 };
    } else {
        return res.status(400).json({ error: 'Invalid client number' });
    }

    res.json(responseData);
    } catch (error) {
    }
});

app.post('/data', (req, res) => {
    try {
        const data = req.body;

        db.run("INSERT INTO data (timestamp, creatorNumber, itNew) VALUES (?, ?, ?)",
            [data.timestamp, data.creatorNumber, data.itNew],
            function(err) {
                if (err) {
                    console.error(err.message);
                    return res.status(500).json({ error: 'Failed to add data to database' });
                }
                console.log(`A row has been inserted with rowid ${this.lastID}`);
                res.json({ success: true });
            });
    } catch (error) {
        
    }
});

app.listen(port, () => {
    console.log(`Server is listening at http://localhost:${port}`);
});