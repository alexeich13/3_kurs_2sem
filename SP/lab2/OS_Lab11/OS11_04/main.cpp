#include <string>
#include <locale.h>
#include <sstream>
#include "../OS11_HTAPI/pch.h"
#include "../OS11_HTAPI/HT.h"

using namespace std;

string intToString(int number);
string incrementString(PCHAR number);

int main(int argc, char* argv[])
{

	setlocale(LC_ALL, "Russian");
	try
	{
		if (argc <= 1) {
			cout << "-- error | args | provide filename";
			return 0;
		}

		const size_t cSize = strlen(argv[1]) + 1;
		wchar_t* wc = new wchar_t[cSize];
		mbstowcs(wc, argv[1], cSize);

		HMODULE hmdll = LoadLibrary(L"OS11_HTAPI.dll");

		if (!hmdll)
			throw "-- LoadLibrary failed";
		cout << "-- LoadLibrary success" << endl;

		auto open = (ht::HtHandle * (*)(const wchar_t*, bool)) GetProcAddress(hmdll, "open");
		auto get = (ht::Element * (*)(ht::HtHandle*, const ht::Element*)) GetProcAddress(hmdll, "get");
		auto update = (BOOL(*)(ht::HtHandle*, const ht::Element*, const void*, int)) GetProcAddress(hmdll, "update");
		auto createInsertElement = (ht::Element * (*)(const void*, int, const void*, int)) GetProcAddress(hmdll, "createInsertElement");

		ht::HtHandle* ht = open(wc, true);
		if (!ht)
			throw "-- open: error. File mapping.";

		cout << "-- open: success" << endl;

		ht::Element* element;
		ht::Element* felement;
		string key, payload;
		int numberKey;

		while (true) {
			numberKey = rand() % 50;
			key = intToString(numberKey);
			cout << "-- Before update --" << endl;
			cout << "Key: " << key << endl;

			element = createInsertElement(key.c_str(), key.length() + 1, "0", 2);
			if ((felement = get(ht, element)) != NULL) {
				cout << "Payload: " << (PCHAR)felement->payload << endl;

				payload = incrementString((PCHAR)felement->payload).c_str();
				if (update(ht, felement, payload.c_str(), payload.length() + 1)) {
				
					cout << "-- After update --" << endl;
					
					ht::Element* updatedElement = get(ht, element);
					if (updatedElement != NULL) {
						cout << "Key: " << (char*)updatedElement->key << endl;
						cout << "Payload: " << (char*)updatedElement->payload << endl;
					}
					else {
						cout << "-- Unable to retrieve updated element --" << endl;
					}
					delete updatedElement;
				}
				else {
					cout << "-- update: error" << endl;
				}
			}
			else {
				cout << "-- get: error" << endl;
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

string incrementString(PCHAR number)
{
	int num = atoi(number);
	return intToString(num + 1);
}
