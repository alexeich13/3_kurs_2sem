#pragma once
#include <string>
class bit {
private:
	unsigned int value;
public:
	bit() {
		value = 0;
	}
	bit(unsigned int i) {
		if (i != i % 2)throw "Bit exception: bit может принимать значения только 0 и 1";
		value = i;
	}
	bit& operator= (int i) {
		if (i != i % 2)throw "Bit exception: bit может принимать значения только 0 и 1";
		value = i;
		return *this;
	}
	bit Inverce() { return bit((1 + value) % 2); }
	operator int() {
		return value;
	}
	operator char() {
		return value == 1 ? '1' : '0';
	}
	operator bool() {
		return value == 1;
	}
	bit operator+ (bit b) {
		return bit((value + b.value) % 2);
	}
	bit operator* (bit b) {
		return bit(value * b.value);
	}
	bit operator== (bit b) {
		return value == b.value;
	}
	bit operator== (int b) {
		return value == b;
	}
	bit operator!= (bit b) {
		return value != b.value;
	}
	bit operator!= (int b) {
		return value != b;
	}
};
class BinaryMatrix {
private:
	unsigned int _n;
	unsigned int _m;
	bit** arr;
public:
	BinaryMatrix(int n,//n - кол-во строк
		int m) {//m - кол-во стобцов
		_n = n;
		_m = m;
		arr = new bit * [n];
		for (int i = 0; i < n; i++) {
			arr[i] = new bit[m];
			for (int j = 0; j < m; j++) {
				arr[i][j] = 0;
			}
		}
	}
	BinaryMatrix(int m) {//m - кол-во стобцов
		_n = 1;
		_m = m;
		arr = new bit * [1];
		arr[0] = new bit[m];
		for (int j = 0; j < m; j++) {
			arr[0][j] = 0;
		}
	}
	BinaryMatrix() {//m - кол-во стобцов
		_n = 1;
		_m = 1;
		arr = new bit * [1];
		arr[0] = new bit[1];
		arr[0][0] = 0;
	}
	int getN() { return _n; }
	int getM() { return _m; }
	BinaryMatrix Transpose();
	bit*& operator[] (int i) {
		return arr[i];
	}
	BinaryMatrix operator* (BinaryMatrix m2) {
		if (_m != m2._n)throw "BinaryMatrix exception: Левосторонняя матрица должна иметь тоже количество столбцов сколько у правосторонней матрицы строк";
		BinaryMatrix result = BinaryMatrix(_n, m2._m);
		for (int i = 0; i < _n; i++) {
			for (int j = 0; j < m2._m; j++) {
				bit c = 0;
				for (int k = 0; k < _m; k++) {
					c = c + (arr[i][k] * m2[k][j]);
				}
				result[i][j] = c;
			}
		}
		return result;
	}

	BinaryMatrix operator+ (BinaryMatrix m2) {
		if (_m != m2._m || _n != m2._n)throw "BinaryMatrix exception: Матрицы должны иметь одинаковые размерности";
		BinaryMatrix result = BinaryMatrix(_n, m2._m);
		for (int i = 0; i < _n; i++) {
			for (int j = 0; j < m2._m; j++) {
				result[i][j] = arr[i][j] + m2[i][j];
			}
		}
		return result;
	}
	BinaryMatrix Inverce() {
		BinaryMatrix M = BinaryMatrix(_n, _m);
		for (int i = 0; i < _n; i++) {
			for (int j = 0; j < _m; j++) {
				M[i][j] = arr[i][j].Inverce();
			}
		}
		return M;
	}
	std::string toString() {
		if (_n == 1) {
			std::string result = "";
			for (int j = 0; j < _m; j++) {
				result += (char)arr[0][j] + std::string(j != _m - 1 ? " " : "");
			}
			return result;
		}
		std::string result = "";
		for (int i = 0; i < _n; i++) {
			for (int j = 0; j < _m; j++) {
				result += (char)arr[i][j];
				if (i == 0 && j == _m - 1)result += "" ;
				else if (i == _n - 1 && j == _m - 1)result += "";
				else result += std::string(j != _m - 1 ? " " : "");
			}
			if (i == _n - 2)result += "\n";
			else if (i != _n - 1)result += "\n";
		}
		return result;
	}
	BinaryMatrix SubMatrix(int n, int m);
	BinaryMatrix SubMatrix(int startN, int startM, int n, int m);
};
BinaryMatrix ConcatMatrix(BinaryMatrix m1, BinaryMatrix m2);
BinaryMatrix CreateHemmingMatrix(unsigned int k/*длинна сообщения*/);
BinaryMatrix StringToBinaryMatrix(std::string str);
BinaryMatrix BinaryStringToBinaryMatrix(std::string str);
BinaryMatrix CreateSingleMatrix(int n);
BinaryMatrix CreateHemmigMatrixWithEvenDetection(unsigned int k/*длинна сообщения*/);
BinaryMatrix CreateGeneratingHemmigMatrixWithEvenDetection(unsigned int k/*длинна сообщения*/);
int getRmin(int k);
int getRminFromN(int n);
BinaryMatrix CreateGeneratingHemmingMatrix(unsigned int k/*длинна сообщения*/);
BinaryMatrix GenerateErrors(BinaryMatrix M, int coutErrors);
BinaryMatrix CreateErrorVector(BinaryMatrix H, BinaryMatrix Yr);

