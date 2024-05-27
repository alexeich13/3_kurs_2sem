#include <string>
#include <sstream>
#include <locale.h>
#include "../OS11_HTAPI/pch.h"
#include "../OS11_HTAPI/HT.h"

using namespace std;

string intToString(int number);

int main(int argc, char* argv[])
{
    setlocale(LC_ALL, "Russian");
    try
    {
        if (argc != 2) {
            throw "-- error | args | provide filename";
        }

        const size_t cSize = strlen(argv[1]) + 1;
        wchar_t* wc = new wchar_t[cSize];
        mbstowcs(wc, argv[1], cSize);

        HMODULE hmdll = LoadLibrary(L"OS11_HTAPI.dll");

        if (!hmdll)
            throw "-- LoadLibrary failed";
        cout << "-- LoadLibrary success" << endl;

        auto open = (ht::HtHandle * (*)(const wchar_t*, bool)) GetProcAddress(hmdll, "open");
        auto removeOne = (BOOL(*)(ht::HtHandle*, const ht::Element*)) GetProcAddress(hmdll, "removeOne");
        auto createInsertElement = (ht::Element * (*)(const void*, int, const void*, int)) GetProcAddress(hmdll, "createInsertElement");

        ht::HtHandle* ht = open(wc, true);  
        if (!ht)
            throw "-- open: error. File mapping.";

        cout << "-- open: success" << endl;

        while (true) {
            int numberKey = rand() % 50;
            string key = intToString(numberKey);
            cout << key << endl;

            ht::Element* element = createInsertElement(key.c_str(), key.length() + 1, "0", 2);
            if (!element)
                throw "-- createInsertElement failed";

            if (removeOne(ht, element)) {
                cout << "-- delete: success | Element: " << key << endl;

                cout << "  - Key: " << (char*)element->key << endl;
                cout << "  - Value: " << (char*)element->payload << endl;
            }
            else {
                cout << "-- delete: error" << endl;
            }

            delete element;

            Sleep(1000);
        }
        FreeLibrary(hmdll);
    }
    catch (const char* msg)
    {
        cout << msg << endl;
    }
}

string intToString(int number)
{
    stringstream convert;
    convert << number;

    return convert.str();
}
