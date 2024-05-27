#include "Hemming.h"
#include <ctime>
#ifndef byte
typedef unsigned char byte;
#endif // !byte

BinaryMatrix BinaryMatrix::Transpose() {
	BinaryMatrix M = BinaryMatrix(_m, _n);
	for (int i = 0; i < _m; i++) {
		for (int j = 0; j < _n; j++) {
			M[i][j] = arr[j][i];
		}
	}
	return M;
}
BinaryMatrix BinaryMatrix::SubMatrix(int n, int m) {
	if (n < 1 || m < 1)throw "SubMatrix exception: не корректные входные параметры";
	if (n > _n || m > _m)throw "SubMatrix exception: ѕодматрица выходит за пределы матрицы, уменьшите значени€";
	BinaryMatrix sub = BinaryMatrix(n, m);
	for (int i = 0; i < n; i++) {
		for (int j = 0; j < m; j++) {
			sub[i][j] = arr[i][j];
		}
	}
	return sub;
}
BinaryMatrix BinaryMatrix::SubMatrix(int startN, int startM, int n, int m) {
	if (n < 1 || m < 1)throw "SubMatrix exception: не корректные входные параметры";
	if (n > _n || m > _m)throw "SubMatrix exception: ѕодматрица выходит за пределы матрицы, уменьшите значени€";
	if (startN + n > _n || startM + m > _m)throw "SubMatrix exception: ѕодматрица выходит за пределы матрицы, уменьшите значени€";
	if (startN < 0 || startM < 0)throw "SubMatrix exception: начальный индекс не может быть меньше нул€";
	BinaryMatrix sub = BinaryMatrix(n, m);
	for (int i = 0; i < n; i++) {
		for (int j = 0; j < m; j++) {
			sub[i][j] = arr[i + startN][j + startM];
		}
	}
	return sub;
}
BinaryMatrix ConcatMatrix(BinaryMatrix m1, BinaryMatrix m2) {
	if (m1.getN() != m2.getN())throw "ћатрицы должны иметь одинаковое количество строк чтобы их соеденить";
	int n = m1.getN();
	int m = m1.getM();
	BinaryMatrix M = BinaryMatrix(n, m + m2.getM());
	for (int i = 0; i < n; i++) {
		for (int j = 0; j < m; j++) {
			M[i][j] = m1[i][j];
		}
	}
	for (int i = 0; i < n; i++) {
		for (int j = 0; j < m2.getM(); j++) {
			M[i][j + m] = m2[i][j];
		}
	}
	return M;
}
BinaryMatrix CreateGeneratingHemmingMatrix(unsigned int k/*длинна сообщени€*/) {
	int r = getRmin(k);
	BinaryMatrix R = BinaryMatrix(r, k);
	int l = 3;
	for (int j = 0; j < k; j++) {
		if (log2(l) == (int)log2(l))l++;
		for (int i = 0; i < r; i++) {
			R[i][j] = (l & (1 << i)) / (1 << i);
		}
		l++;
	}
	return ConcatMatrix(CreateSingleMatrix(k), R.Transpose());
}
BinaryMatrix CreateHemmingMatrix(unsigned int k/*длинна сообщени€*/) {
	int r = getRmin(k);
	BinaryMatrix R = BinaryMatrix(r, k);
	int l = 3;
	for (int j = 0; j < k; j++) {
		if (log2(l) == (int)log2(l))l++;
		for (int i = 0; i < r; i++) {
			R[i][j] = (l & (1 << i)) / (1 << i);
		}
		l++;
	}
	return ConcatMatrix(R, CreateSingleMatrix(r));
}
BinaryMatrix CreateSingleMatrix(int n) {
	BinaryMatrix M = BinaryMatrix(n, n);
	for (int i = 0; i < n; i++) {
		M[i][i] = 1;
	}
	return M;
}
int getRmin(int k) {
	if (k == 1)return 2;
	int r = ceil(log2(k));
	while ((1 << r) < r + k + 1)r += 1;
	return r;
}
int getRminFromN(int n) {
	return ceil(log2((double)n + 1));
}
BinaryMatrix StringToBinaryMatrix(std::string str) {
	int len = str.length();
	BinaryMatrix result = BinaryMatrix(len * 8);
	for (int i = 0; i < len; i++) {
		byte c = (byte)str[i];
		for (int j = 0; j < 8; j++) {
			if (c % 2 == 0) {
				result[0][i * 8 + 7 - j] = 0;
				c /= 2;
			}
			else {
				result[0][i * 8 + 7 - j] = 1;
				c -= 1;
				c /= 2;
			}
		}
	}
	return result;
}
BinaryMatrix BinaryStringToBinaryMatrix(std::string str) {
	int len = str.length();
	BinaryMatrix result = BinaryMatrix(len);
	for (int i = 0; i < len; i++) {
		result[0][i] = str[i] == '1' ? 1 : 0;
	}
	return result;
}

BinaryMatrix GenerateErrors(BinaryMatrix M, int countErrors) {
	if (countErrors < 0 || countErrors > M.getM())throw "Generate Errors exception: Ќекоректное кол-во ошибок";
	if (1 != M.getN())throw "Generate Errors exception: ћатрица должна €вл€тс€ вектором - иметь 1 строку";
	int* poss = new int[countErrors];
	srand(time(NULL));
	BinaryMatrix result = BinaryMatrix(M.getN(), M.getM());
	for (int i = 0; i < M.getM(); i++)result[0][i] = M[0][i];
	for (int i = 0; i < countErrors; i++) poss[i] = -1;
	for (int i = 0; i < countErrors; i++) {
		int pos = rand() % M.getM();
		bool itsOrigin = true;
		for (int i = 0; i < countErrors; i++)if (pos == poss[i])itsOrigin = false;
		if (itsOrigin) {
			poss[i] = pos;
			result[0][pos] = result[0][pos].Inverce();
		}
		else {
			i--;
		}
	}
	return result;
}
BinaryMatrix CreateErrorVector(BinaryMatrix H, BinaryMatrix Yr) {
	BinaryMatrix HT = H.Transpose();
	BinaryMatrix E = BinaryMatrix(1, HT.getN());
	for (int i = 0; i < HT.getN(); i++) {
		bool itsError = true;
		for (int j = 0; j < HT.getM(); j++) {
			if (HT[i][j] != Yr[0][j])itsError = false;
		}
		if (itsError) {
			E[0][i] = 1;
		}
	}
	return E;
}
BinaryMatrix CreateHemmigMatrixWithEvenDetection(unsigned int k/*длинна сообщени€*/) {
	int r = getRmin(k);
	BinaryMatrix R = BinaryMatrix(r + 1, k + r + 1);
	int l = 3;
	for (int j = 0; j < k; j++) {
		if (log2(l) == (int)log2(l))l++;
		for (int i = 0; i < r; i++) {
			R[i][j] = (l & (1 << i)) / (1 << i);
		}
		l++;
		R[r][j] = 1;
	}
	for (int i = 0; i < r + 1; i++) {
		for (int j = 0; j < r + 1; j++) {
			if (i == j)R[j][i + k] = 1;
		}
		R[r][i + k] = 1;
	}
	return R;
}

BinaryMatrix CreateGeneratingHemmigMatrixWithEvenDetection(unsigned int k/*длинна сообщени€*/) {
	int r = getRmin(k);
	BinaryMatrix R = BinaryMatrix(r + 1, k);
	int l = 3;
	for (int j = 0; j < k; j++) {
		if (log2(l) == (int)log2(l))l++;
		for (int i = 0; i < r; i++) {
			R[i][j] = (l & (1 << i)) / (1 << i);
		}
		l++;
		R[r][j] = 1;
	}
	return ConcatMatrix(CreateSingleMatrix(k), R.Transpose());
}