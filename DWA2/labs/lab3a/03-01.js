const http = require("http");
const readline = require("readline");
let state = "norm";

const app = http.createServer((req, res) => {
    if (state != null) {
        res.writeHead(200, { "Content-Type": "text/html; charset=utf-8" });
        res.end(`<h1>${state}</h1>`);
    } else {
        res.end("<h1>Error</h1>");
    }
});

app.listen(5000, () => {
    console.log("Server start listening");
});

const readlin = readline.createInterface({
    input: process.stdin,
    output: process.stdout
});

readlin.setPrompt('Enter new state (norm, stop, test, idle, exit): ');

readlin.on('line', (input) => {
    const trimmedInput = input.trim();
    const states = ['norm', 'stop', 'test', 'idle', 'exit'];

    if (states.includes(trimmedInput)) {
        console.log(`${state} - ${trimmedInput}`);

        if (trimmedInput === 'exit') {
            process.exit(0);
        } else {
            state = trimmedInput;
        }
    } else {
        console.log(`Unknown state: ${trimmedInput}\n${state} -> ${state}`);
    }
    readlin.prompt();
});

readlin.prompt();