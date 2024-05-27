const { v4: uuidv4 } = require('uuid');
const validateCard=(cardNumber)=> {
    console.log('Card number:', cardNumber);
    return Math.random() < 0.5;
}

const createOrder=(cardNumber)=> {
    return new Promise((resolve, reject) => {
        if (!validateCard(cardNumber)) {
            reject(new Error('Card is not valid'));
        } else {
            const orderId = uuidv4();
            setTimeout(() => resolve(orderId), 5000);
        }
    });
}

const proceedToPayment=(orderId)=> {
    return new Promise((resolve, reject) => {
        console.log('Order ID:', orderId);
        const paymentSuccess = Math.random() < 0.5;
        if (paymentSuccess) {
            resolve('Payment successful');
        } else {
            reject(new Error('Payment failed'));
        }
    });
}

 const paymentProcess=async()=> {
    try {
        const orderId = await createOrder('3452-9240-3535-6666');
        const paymentResult = await proceedToPayment(orderId);
        console.log('Result:', paymentResult);
    } catch (error) {
        console.error('Error:', error.message);
    }
}

paymentProcess();
