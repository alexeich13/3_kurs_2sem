// Функция для генерации случайного бинарного вектора длиной n
const generateRandomBinaryVector = (n) => {
    const binaryVector = [];
    for (let i = 0; i < n; i++) {
      const randomBit = Math.round(Math.random());
      binaryVector.push(randomBit);
    }
    return binaryVector;
  };
  
  // Функция для вычисления остатка от деления на 2
  const mod2 = (x) => {
    return x % 2;
  };
  
  // Функция для проверки битов
  const checkBits = (matrix, groups, k1, k2) => {
    let xorResults = [];

    // Вычисление суммы элементов по модулю 2 для каждой строки
    for (let i = 0; i < k1; i++) {
        const rowSum = matrix[i].reduce((acc, val) => acc + val, 0) % 2;
        xorResults.push(rowSum);
    }

    // Вычисление суммы элементов по модулю 2 для каждого столбца
    for (let j = 0; j < k2; j++) {
        let columnSum = 0;
        for (let i = 0; i < k1; i++) {
            columnSum += matrix[i][j];
        }
        xorResults.push(columnSum % 2);
    }

    return xorResults;
};

  
  // Функция для генерации случайной ошибки в матрице
  const introduceRandomError = (matrix, k1, k2, amount) => {
    const receivedMatrix = matrix.map(row => [...row]);
    var errorPositions = [];
    for (let n = 0; n < amount; n++) {
      const i = Math.floor(Math.random() * k1);
      const j = Math.floor(Math.random() * k2);
      receivedMatrix[i][j] = 1 - receivedMatrix[i][j];
      errorPositions.push({row: i, column: j});
    }
    return { matrix: receivedMatrix, errorPositions: errorPositions };
  };
  
  // Функция для нахождения исправленной ошибки
  const findCorrectedError = (receivedVector, originalVector, k1, k2, receivedMatrix, errorPositions) => {
    const errorRows = errorPositions.map(pos => pos.row);
    const errorColumns = errorPositions.map(pos => pos.column);
    const correctedMatrix = receivedMatrix.map(row => [...row]);
    for (const rowIndex of errorRows) {
        for (const columnIndex of errorColumns) {
            correctedMatrix[rowIndex][columnIndex] = 1 - correctedMatrix[rowIndex][columnIndex];
        }
    }
    return correctedMatrix;
};

  // Основная функция
  const main = () => {
    const informationLength = 24;
    const informationVector = generateRandomBinaryVector(informationLength);
    console.log('Информационное слово:', informationVector.join(' '));
  
    const parityRows = [4, 3];
    const parityColumns = [6, 8];
    const parityGroups = [[2, 3], [2, 3]];
  
    for (let i = 0; i < parityRows.length; i++) {
      console.log(`\n---------------------- ${i + 1}-я матрица ----------------------\n`);
  
      const matrix = [];
      for (let j = 0; j < parityRows[i]; j++) {
        matrix.push(informationVector.slice(j * parityColumns[i], (j + 1) * parityColumns[i]));
      }
      console.log(`Матрица ${parityRows[i]}x${parityColumns[i]}:`);
      for (const row of matrix) {
        console.log(row.join(' '));
      }
  
      console.log('Количество групп паритетов =', parityGroups[i].join(', '));
  
      const checkBitsResult = checkBits(matrix, parityGroups[i], parityRows[i], parityColumns[i]);
      console.log('Проверочные биты:', checkBitsResult.join(' '));
  
      const codedVector = [...informationVector, ...checkBitsResult];
      console.log('Кодовое слово:', codedVector.join(' '));
  
      const singleMistakeAmount = 1;
      const errorInfo = introduceRandomError(matrix, parityRows[i], parityColumns[i], singleMistakeAmount);
      const receivedMatrix = errorInfo.matrix;
      const errorPositions = errorInfo.errorPositions;
      console.log('Матрица принятого сообщения:');
      for (const row of receivedMatrix) {
        console.log(row.join(' '));
      }
  
      const receivedCheckBits = checkBits(receivedMatrix, parityGroups[i], parityRows[i], parityColumns[i]);
      console.log('Проверочные биты принятого слова:', receivedCheckBits.join(' '));
  
      if (checkBitsResult.every((val, index) => val === receivedCheckBits[index])) {
        console.log('Xr и Yr одинаковые, ошибок не обнаружено');
      } else {
        console.log('Xr и Yr не одинаковые');
        const correctedMatrix = findCorrectedError(receivedCheckBits, checkBitsResult, parityRows[i], parityColumns[i], receivedMatrix, errorPositions);
        console.log('Исправленная матрица:');
        for (const row of correctedMatrix) {
          console.log(row.join(' '));
        }
      }
    }
  };
  
  main();
  