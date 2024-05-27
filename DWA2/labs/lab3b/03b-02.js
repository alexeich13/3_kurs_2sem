const secondJob=()=> {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            reject(new Error('There is error'));
        }, 3000);
    });
}

secondJob().then((result) => {
    console.log('Result:', result);
})
.catch((error) => {
    console.error('Error', error.message);
});

const handleSecondJob= async()=> {
    try {
        const result = await secondJob();
        console.log('Result:', result);
    } catch (error) {
        console.error('Error:', error.message);
    }
}

handleSecondJob();
