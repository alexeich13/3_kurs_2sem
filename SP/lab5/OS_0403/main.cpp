#pragma warning(disable : 4996)

#include <iostream>
#include <windows.h>
#include <string>
#include <sstream>

#include "../OS14_HTCOM_LIB/pch.h"
#include "../OS14_HTCOM_LIB/OS14_HTCOM_LIB.h"

#ifdef _WIN64
#pragma comment(lib, "../x64/Debug/OS14_HTCOM_LIB.lib")
#else
#pragma comment(lib, "../Debug/OS14_HTCOM_LIB.lib")
#endif

using namespace std;

string intToString(int number);
wchar_t* getWC(char* c)
{
	wchar_t* wc = new wchar_t[strlen(c) + 1];
	mbstowcs(wc, c, strlen(c) + 1);

	return wc;
}

const wchar_t* getWCC(char* c, string mode = "a"s) {
	wchar_t wc[100];
	string s = string(c) + "_stop_"s + mode;
	mbstowcs(wc, s.c_str(), s.size() + 1);
	return wc;
}

int main(int argc, char* argv[])
{
	try
	{
	setlocale(LC_ALL, "Russian");
	wchar_t* fileName = getWC(argv[1]);

	if (_waccess(fileName, 0) != 0)
	{
		throw "Файл не существует.";
	}

	HANDLE hStopEvent = CreateEvent(NULL,
		TRUE,
		FALSE,
		getWCC(argv[1]));
	HANDLE hStopRemEvent = CreateEvent(NULL,
		TRUE,
		FALSE,
		getWCC(argv[1], "r"s));


		cout << "Инициализация компонента:" << endl;
		OS14_HTCOM_HANDEL h = OS14_HTCOM::Init();

		ht::HtHandle* ht = nullptr;

		if (argc == 4)
		{
			wchar_t* username = getWC(argv[2]);
			wchar_t* password = getWC(argv[3]);

			ht = OS14_HTCOM::HT::open(h, fileName, username, password,true);
		}
		else
		{
			ht = OS14_HTCOM::HT::open(h, fileName, true);
		}
		if (ht)
			cout << "-- open: success" << endl;
		else
			throw "-- open: error";

		while (WaitForSingleObject(hStopEvent, 0) == WAIT_TIMEOUT && WaitForSingleObject(hStopRemEvent, 0) == WAIT_TIMEOUT) {
			int numberKey = rand() % 50;
			string key = intToString(numberKey);
			cout << key << endl;

			ht::Element* element = OS14_HTCOM::Element::createGetElement(h, key.c_str(), key.length() + 1);
			if (OS14_HTCOM::HT::removeOne(h, ht, element))
				cout << "-- remove: success" << endl;
			else
				cout << "-- remove: error" << endl;

			delete element;

			Sleep(1000);
		}

		OS14_HTCOM::HT::close(h, ht);

		cout << endl << "Удалить компонент и выгрузить dll, если можно:" << endl;
		OS14_HTCOM::Dispose(h);
	}
	catch (const char* e) { cout << e << endl; }
	catch (int e) { cout << "HRESULT: " << e << endl; }

}

string intToString(int number)
{
	stringstream convert;
	convert << number;

	return convert.str();
}

