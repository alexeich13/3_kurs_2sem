const http = require("http");
const url = require("url");

const app = http.createServer((req, res) => {
    if (req.url.startsWith("/fact")) {
        const Url = url.parse(req.url, true);
        const K = Url.query.k;
        res.writeHead(200, { "Content-Type": "application/json; charset=utf-8" });
        if (!K) {
            res.end(JSON.stringify({ error: "There is no 'k'" }));
        } else {
            const factResult = factorial(parseInt(K, 10));
            res.end(JSON.stringify({ k: K, fact: factResult }));
        }
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
        console.log("Server start listening");
    } catch (err) {
        console.error(err);
    }
};

main();
