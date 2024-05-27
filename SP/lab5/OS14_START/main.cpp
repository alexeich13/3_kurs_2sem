#pragma warning(disable : 4996)

#include <iostream>
#include <windows.h>
#include <conio.h>

#include "../OS14_HTCOM_LIB/pch.h"
#include "../OS14_HTCOM_LIB/OS14_HTCOM_LIB.h"

#ifdef _WIN64
#pragma comment(lib, "../x64/Debug/OS14_HTCOM_LIB.lib")
#else
#pragma comment(lib, "../Debug/OS14_HTCOM_LIB.lib")
#endif

using namespace std;

wchar_t* getWC(const char* c);


const wchar_t* getWCC(char* c, string mode = "a"s);


int main(int argc, char* argv[])
{
	auto fileName = getWC(argv[1]);

	HANDLE hStopEvent = CreateEvent(NULL,
		TRUE,
		FALSE,
		getWCC(argv[1]));

	setlocale(LC_ALL, "Russian");

	try
	{
	
		if (_waccess(fileName, 0) != 0)
		{
			throw "Файл не существует.";
		}

		cout << "Инициализация компонента:" << endl;
		OS14_HTCOM_HANDEL h = OS14_HTCOM::Init();

		ht::HtHandle* ht = nullptr;

		if (argc == 4)
		{
			wchar_t* username = getWC(argv[2]);
			wchar_t* password = getWC(argv[3]);
			
			ht = OS14_HTCOM::HT::open(h, fileName, username, password);
		}
		else
		{
			ht = OS14_HTCOM::HT::open(h, fileName);
		}

		if (ht)
		{
			cout << "HT-Storage Start" << endl;
			wcout << "filename: " << ht->fileName << endl;
			cout << "secSnapshotInterval: " << ht->secSnapshotInterval << endl;
			cout << "capacity: " << ht->capacity << endl;
			cout << "maxKeyLength: " << ht->maxKeyLength << endl;
			cout << "maxPayloadLength: " << ht->maxPayloadLength << endl;

			while (!kbhit() && WaitForSingleObject(hStopEvent, 0) == WAIT_TIMEOUT)
				SleepEx(0, TRUE);

			OS14_HTCOM::HT::snap(h, ht);
			OS14_HTCOM::HT::close(h, ht);
		}
		else
			cout << "-- open: error" << endl;

		cout << endl << "Удалить компонент и выгрузить dll, если можно:" << endl;
		OS14_HTCOM::Dispose(h);
	}
	catch (const char* e) { cout << e << endl; }
	catch (int e) { cout << "HRESULT: " << e << endl; }

	system("pause");
}

wchar_t* getWC(const char* c)
{
	wchar_t* wc = new wchar_t[strlen(c) + 1];
	mbstowcs(wc, c, strlen(c) + 1);
	return wc;
}

const wchar_t* getWCC(char* c, string mode) {
	wchar_t wc[100];
	string s = string(c) + "_stop_"s + mode;
	mbstowcs(wc, s.c_str(), s.size() + 1);
	return wc;
}