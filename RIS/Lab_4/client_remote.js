const axios = require('axios');
const moment = require('moment');

// Функция для отправки данных на сервер
async function sendData(value) {
    try {
        const timestamp = moment().format('YYYY-MM-DD HH:mm:ss');
        const response = await axios.post('http://172.20.10.10:3000/data', { value, timestamp });
        console.log(response.data);
    } catch (error) {
        console.error('Error sending data:', error.message);
    }
}

// Отправка данных на сервер каждые 5 секунд
setInterval(() => {
    sendData('Some data from local machine');
}, 5000);
