#pragma warning(disable : 4996)

#include <windows.h>
#include <string>

#include "sddl.h"

using namespace std;


const wchar_t* getWCC(char* c, string mode = "a"s) {
	wchar_t wc[100];
	string s = string(c) + "_stop_"s + mode;
	mbstowcs(wc, s.c_str(), s.size() + 1);
	return wc;
}


int main(int argc, char* argv[])
{

	string mode = "a"s;
	// a, i, r, g
	if (argc >= 3) {
		char symb = argv[2][0];
		switch (symb)
		{
		case 'i': case 'r': case 'g':
			mode = symb;
			break;
		default:
			mode = 'a';
		}
	}

	HANDLE hStopEvent = CreateEvent(NULL,
		TRUE,
		FALSE,
		getWCC(argv[1], mode));

	SetEvent(hStopEvent);
}
	