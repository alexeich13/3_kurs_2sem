import { send } from "./m06_DAI/m06_DAI.js"

const from = 'gjoikhunki@gmail.com';
const to = 'hyoihjk8y0ikji@gmail.com\n';
const pass = '7uhkhuokjijkl';
let message = '06-03!';

async function main() {
    try {
        await send(from, to, pass, message);
        console.log('Функция send успешно выполнена');
    } catch (error) {
        console.error('Произошла ошибка при выполнении функции send:', error);
    }
}

main();
