#include <iostream>
#include "Hemming.h"

std::string removeSpacesAndPipe(const std::string& str)
{
    std::string result;
    for (char ch : str) {
        if (ch != ' ' && ch != '|') {
            result += ch;
        }
    }
    return result;
}

std::string perform2Err(const std::string& str1, const std::string& str2) {
    std::string result;
    for (size_t i = 0; i < str1.length(); ++i) {
        result += (str1[i] == str2[i]) ? '0' : '1';
    }
    return result;
}

std::string addSpacesAndPipe(const std::string& str) {
    std::string result = "";
    for (size_t i = 0; i < str.length(); ++i) {
        result += str[i];
        if ((i + 1) % 1 == 0 && i != str.length() - 1) {
            result += ' ';
        }
    }
    result += "";
    return result;
}

int main()
{
    std::string msg;
    setlocale(LC_CTYPE, "Russian");
    std::cout << "Введите строку: ";
    std::cin >> msg;
    BinaryMatrix X = StringToBinaryMatrix(msg);
    std::cout << "Cообщение: " << X.toString() << "\n";
    BinaryMatrix GMatrix = CreateGeneratingHemmingMatrix(X.getM());
    int r = getRmin(X.getM());
    BinaryMatrix Xn = X * GMatrix;
    std::cout << "Сообщение c избыточными битами: " << Xn.toString() << "\n";
    std::cout << "\n";
    BinaryMatrix H = CreateHemmingMatrix(X.getM());
    for (int i = 1; i < 3; i++) {
        BinaryMatrix Y = GenerateErrors(Xn, i);
        std::string binaryStringCleaned = removeSpacesAndPipe(Y.toString());
        std::string binaryStringCleaned2 = removeSpacesAndPipe(Xn.toString());
        BinaryMatrix Yk = Y.SubMatrix(1, Y.getM() - r);
        std::cout << "Сообщение с " << i << " ошибками: " << Y.toString() << "\n";
        std::cout << "Проверочная матрица Хемминга:\n" << H.toString() << "\n";
        std::cout << "\n";
        BinaryMatrix Yr2 = Yk * GMatrix;
        Yr2 = Yr2.SubMatrix(0, Yk.getM(), 1, r);
        std::cout << "\t Избыточные биты сообщения: " << Yr2.toString() << "\n";
        BinaryMatrix Yr = Y * H.Transpose();
        int Sindrom = 0;
        for (int i = 0; i < Yr.getM(); i++)Sindrom = Sindrom + (int)Yr[0][i];
        std::cout << "\t Синдром: " << Yr.toString() << "\n";
        BinaryMatrix E = CreateErrorVector(H, Yr);
        if (i == 2)
        {
            std::string EVector = perform2Err(binaryStringCleaned, binaryStringCleaned2);
            char searchChar = '1';
            size_t pos = EVector.find(searchChar);
            while (pos != std::string::npos)
            {
                std::cout << "\t Ошибка: " << pos + 1 << " бит" << std::endl;
                pos = EVector.find(searchChar, pos + 1);
            }
        }
        else 
        {
            std::string EVector2 = removeSpacesAndPipe(E.toString());
            char searchChar = '1';
            size_t pos = EVector2.find(searchChar);
            while (pos != std::string::npos)
            {
                std::cout << "\t Ошибка: " << pos + 1 << " бит" << std::endl;
                pos = EVector2.find(searchChar, pos + 1);
            }
        }
            
        BinaryMatrix Yfixed = Y + E;
        if (i == 2)
        {
            std::cout << "\t Исправленное сообщение: -" << "\n";
        }
        else
            std::cout << "\t Исправленное сообщение: " << Yfixed.toString() << "\n";
        std::cout << "\n";
    }
}
