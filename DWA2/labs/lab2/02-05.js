const http = require("http");
const fs = require("fs");

const app = http.createServer((req, res) => {
    if (req.url === "/fetch") {
        fs.readFile("xmlhttprequest.html", (err, data) => {
            if (err) {
                res.writeHead(500, { "Content-Type": "text/plain" });
                res.end("Internal Server Error");
            } else {
                res.writeHead(200, {"Content-Type": "text/html; charset=utf-8" });
                res.end(data);
            }
        });
    }  else if (req.url === "/api/name") {
        res.writeHead(200, {'Content-Type': 'text/plain; charset=utf-8'});
        res.end('Дрозд Алексей Игоревич');

    }
    else {
        res.writeHead(404, { "Content-Type": "text/html" });
        res.end(`
            <h1>Not Found</h1>
        `);
    }
});

const main = async () => {
    try {
        await app.listen(5000);
        console.log(`Server is running`);
    } catch (e) {
        console.error(e);
    }
};

main();