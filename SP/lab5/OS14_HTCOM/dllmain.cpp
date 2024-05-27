
#include "pch.h"
#include "ClassFactory.h"
#include "Registry.h"

HMODULE g_hModule;                                         

// {76840F7A-5EF5-49F9-8AFB-1545837501F7}
static const GUID CLSID_ComponentHT =
{ 0x76840f7a, 0x5ef5, 0x49f9, { 0x8a, 0xfb, 0x15, 0x45, 0x83, 0x75, 0x1, 0xf7 } };

const WCHAR* FNAME = L"OS14_HTCOM.dll";
const WCHAR* VerInd = L"Smw.CA";
const WCHAR* ProgId = L"Smw.CA.1";

long g_cComponents = 0;		                                
long g_cServerLocks = 0;                                    

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