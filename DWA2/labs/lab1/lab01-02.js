const http = require("http");

const app = http.createServer((request, response) => {
    if (request.method === 'GET') {
        response.end(gaveInfo(request))
    } else if (request.method === 'POST') {
        takeInfo(request, response)
    }
})

function gaveInfo(request) {
    return `<html><body><h1>Request Info:</h1>
          <p>Method: ${request.method}</p>
          <p>URI: ${request.url}</p>
          <p>Protocol Version: ${request.httpVersion}</p>
          <p>Headers: ${JSON.stringify(request.headers)}</p>
          </body></html>`
}

function takeInfo(request, response) {
    let data = ''
    request.on('data', info => {
        data += info
    });
    request.on('end', () => {
        response.end(`<html>
        <body><h1>data:</h1>
        <p>${data}</p>
        </body>
        </html>`)
    });
}

const main = async () => {
    try {
        app.listen(3000)
        console.log(`Server started`)
    } catch (err) {
        console.log(`Error: ${err.error}`)
    }
}

main()