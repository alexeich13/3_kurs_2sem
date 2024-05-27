const http = require("http");

const app = http.createServer((req, res) => {
    if (req.url === "/api/name" && req.method === "GET") {
        res.writeHead(200,{"Content-type": "text/plain; charset=utf-8"});
        res.end("Дрозд Алексей Игоревич");
    }
    res.writeHead(404, { "Content-type": "text/html" });
    res.end(`
        <h1>Not Found</h1>
    `);
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