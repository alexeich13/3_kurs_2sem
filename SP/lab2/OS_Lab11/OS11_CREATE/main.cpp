#define _CRT_SECURE_NO_WARNINGS

#include <iostream>
#include <locale.h>
#include <fstream>
#include "../OS11_HTAPI/pch.h"
#include "../OS11_HTAPI/HT.h"

using namespace std;

wchar_t* getWC(const char* c);

int main(int argc, char* argv[]) 
{
    setlocale(LC_ALL, "Russian");

    try
    {
        if (argc != 6)
        {
            throw "Неверное количество аргументов. Использование: <capacity> <secSnapshotInterval> <maxKeyLength> <maxPayloadLength> <filename>";
        }

        for (int i = 1; i <= 4; ++i)
        {
            int value = atoi(argv[i]);
            if (value <= 0)
            {
                throw "Некорректное значение параметра. Ожидались положительные числа.";
            }
        }

        ifstream file(argv[5]);
        if (file.good())
        {
            throw "Файл с таким именем уже существует.";
        }
     

        ht::HtHandle* ht = nullptr;
        ht = ht::create(atoi(argv[1]), atoi(argv[2]), atoi(argv[3]), atoi(argv[4]), getWC(argv[5]));
        if (ht)
        {
            cout << "HT-Storage Created" << endl;
            wcout << "filename: " << ht->fileName << endl;
            cout << "secSnapshotInterval: " << ht->secSnapshotInterval << endl;
            cout << "capacity: " << ht->capacity << endl;
            cout << "maxKeyLength: " << ht->maxKeyLength << endl;
            cout << "maxPayloadLength: " << ht->maxPayloadLength << endl;

            ht::close(ht);
        }
        else
        {
            cout << "-- create: error" << endl;
        }
    }
    catch (const char* errorMessage)
    {
        cerr << "Ошибка: " << errorMessage << endl;
    }
    catch (...)
    {
        cerr << "Произошла неизвестная ошибка." << endl;
    }

    return 0;
}

wchar_t* getWC(const char* c)
{
    wchar_t* wc = new wchar_t[strlen(c) + 1];
    mbstowcs(wc, c, strlen(c) + 1);

    return wc;
}
