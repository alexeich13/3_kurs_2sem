// dllmain.cpp : Определяет точку входа для приложения DLL.
#include "pch.h"
#include "ClassFactory.h"
#include "Registry.h"

HMODULE g_hModule;                                          // Описатель модуля
const WCHAR* FNAME = L"OS13_HTCOM.dll";
const WCHAR* VerInd = L"Smw.CA";
const WCHAR* ProgId = L"Smw.CA.1";

long g_cComponents = 0;		                                // Количество активных компонентов
long g_cServerLocks = 0;                                    // Счетчик блокировок

// {76592BF6-D264-474F-89A0-7DF45250F093}
static const GUID CLSID_ComponentHT =
{ 0x76592bf6, 0xd264, 0x474f, { 0x89, 0xa0, 0x7d, 0xf4, 0x52, 0x50, 0xf0, 0x93 } };


BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
        g_hModule = hModule;
        break;
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

STDAPI DllCanUnloadNow()
{
	if ((g_cComponents == 0) && (g_cServerLocks == 0))
	{
		return S_OK;
	}
	else
	{
		return S_FALSE;
	}
}

STDAPI DllGetClassObject(const CLSID& clsid,
	const IID& iid,
	void** ppv)
{
	std::cout << "DllGetClassObject:\tСоздать фабрику класса" << std::endl;
	if (clsid != CLSID_ComponentHT)
	{
		return CLASS_E_CLASSNOTAVAILABLE;
	}
	ClassFactory* pFactory = new ClassFactory;
	if (pFactory == NULL)
	{
		return E_OUTOFMEMORY;
	}
	HRESULT hr = pFactory->QueryInterface(iid, ppv);
	pFactory->Release();

	return hr;
}

HRESULT __declspec(dllexport) DllRegisterServer() {
	return RegisterServer(g_hModule, CLSID_ComponentHT, FNAME, VerInd, ProgId);
}

HRESULT __declspec(dllexport) DllUnregisterServer() {
	return UnregisterServer(CLSID_ComponentHT, VerInd, ProgId);
}

STDAPI DllInstall(bool b, PCWSTR s)
{
	return S_OK;
}