#pragma once
#include <objbase.h>

class MyFactory : public IClassFactory {
protected:
    ULONG m_lRef;

public:
    MyFactory();
    ~MyFactory();

    STDMETHOD(QueryInterface)(REFIID riid, LPVOID* ppv);
    STDMETHOD_(ULONG, AddRef)();
    STDMETHOD_(ULONG, Release)();
    STDMETHOD(CreateInstance)(IUnknown* pUnkOuter, REFIID riid, void** ppv);
    STDMETHOD(LockServer)(BOOL fLock);
};
