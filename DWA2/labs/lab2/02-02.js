const http = require("http");
const fs = require("fs");

const app = http.createServer((req, res) => {
    if (req.url === "/png" && req.method === "GET") {
        return readFileAndAnswer(res, "images.png", "image/png");
    }
    res.writeHead(404, { "Content-type": "text/html" });
    res.end(`
        <h1>Not Found</h1>
    `);
});

function readFileAndAnswer(res, fileName, contentType) {
    fs.readFile(fileName, (err, data) => {
        if (err) {
            res.writeHead(500, { "Content-type": "text/plain" });
            res.end("Error");
        } else {
            res.writeHead(200, { "Content-type": contentType });
            res.end(data);
        }
    });
}
const main = async () => {
    try {
        await app.listen(5000);
        console.log(`Server is running`);
    } catch (e) {
        console.error(e);
    }
};

main();