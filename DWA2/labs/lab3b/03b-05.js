const calculateSquare=(number)=> {
    return new Promise((resolve, reject) => {
        if (typeof number !== 'number') {
            reject(new Error('Invalid input, please provide a valid number'));
        } else {
            resolve(number * number);
        }
    });
}
const calculateCube=(number)=> {
    return new Promise((resolve, reject) => {
        if (typeof number !== 'number') {
            reject(new Error('Invalid input, please provide a valid number'));
        } else {
            resolve(number * number * number);
        }
    });
}
const calculateFourthPower=(number)=> {
    return new Promise((resolve, reject) => {
        if (typeof number !== 'number') {
            reject(new Error('Invalid input, please provide a valid number'));
        } else {
            resolve(number * number * number * number);
        }
    });
}
const inputNumber = 5;
Promise.all([
    calculateSquare(inputNumber),
    calculateCube(inputNumber),
    calculateFourthPower(inputNumber)
])
    .then((results) => {
        const [squareResult, cubeResult, fourthPowerResult] = results;
        console.log('Square:', squareResult);
        console.log('Cube:', cubeResult);
        console.log('Fourth Power:', fourthPowerResult);
    })
    .catch((error) => {
        console.error('Error:', error.message);
    });
