const http = require("http");
const url = require("url");
const fs = require("fs");

const app = http.createServer((req, res) => {
    const Url = url.parse(req.url, true);
    const path = Url.pathname;
    if (path.startsWith("/fact")) {
        const K = Url.query.k;
        res.writeHead(200, { "Content-Type": "application/json; charset=utf-8" });
        if (!K) {
            res.end(JSON.stringify({ error: "Parameter 'k' is missing" }));
        } else {
            const factResult = factorial(parseInt(K, 10));
            res.end(JSON.stringify({ k: K, fact: factResult }));
        }
    }
    else if (path === "/") {
        fs.readFile("text.html", "utf8", function (err, data) {
            if (err) {
                res.writeHead(500, { "Content-Type": "text/plain" });
                res.end("Internal Server Error");
            } else {
                res.writeHead(200, { "Content-Type": "text/html; charset=utf-8" });
                res.end(data);
            }
        });
    } else {
        res.writeHead(404, { "Content-Type": "text/html" });
        res.end(`<h1>Not Found</h1><p>Unknown URL: ${path}</p>`);
    }
});

const factorial = (k) => {
    if (k === 0) {
        return 1;
    } else {
        return k * factorial(k - 1);
    }
};

const main = async () => {
    try {
        app.listen(5000);
        console.log('Server start listening');
    } catch (err) {
        console.error(err);
    }
};

main();
