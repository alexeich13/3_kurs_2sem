#include "pch.h"
#include "OS14_HTCOM_LIB.h"

//{76840F7A-5EF5-49F9-8AFB-1545837501F7}
static const GUID CLSID_ComponentHT =
{ 0x76840f7a, 0x5ef5, 0x49f9, { 0x8a, 0xfb, 0x15, 0x45, 0x83, 0x75, 0x1, 0xf7 } };

IHT* pIHT = nullptr;
IElement* pIElement = nullptr;



OS14_HTCOM_HANDEL OS14_HTCOM::Init() {
	IUnknown* pIUnknown = NULL;
	CoInitialize(NULL);                       
	HRESULT hr0 = CoCreateInstance(CLSID_ComponentHT, NULL, CLSCTX_INPROC_SERVER, IID_IUnknown, (void**)&pIUnknown);
	if (SUCCEEDED(hr0))
	{
		return pIUnknown;
	}
	else {
		throw (int)hr0;
		return NULL;
	}
}

void OS14_HTCOM::Dispose(OS14_HTCOM_HANDEL h) {
	((IUnknown*)h)->Release();
	CoFreeUnusedLibraries();
	CoUninitialize();
}



ht::HtHandle* OS14_HTCOM::HT::create(OS14_HTCOM_HANDEL h, int capacity, int secSnapshotInterval, int maxKeyLength, int maxPayloadLength, const wchar_t* htUsersGroup, const wchar_t* fileName)
{
	ht::HtHandle** ht = new ht::HtHandle*;

	HRESULT hr0 = ((IUnknown*)h)->QueryInterface(IID_IHT, (void**)&pIHT);
	if (SUCCEEDED(hr0))
	{
		HRESULT hr1 = pIHT->create(ht, capacity, secSnapshotInterval, maxKeyLength, maxPayloadLength, htUsersGroup, fileName);
		if (FAILED(hr1)) {
			pIHT->Release();
			throw (int)hr1;
			return nullptr;
		}
		else {
			pIHT->Release();
			return *ht;
		}
	}
	else {

		throw (int)hr0;
		return nullptr;
	}
}

ht::HtHandle* OS14_HTCOM::HT::open(OS14_HTCOM_HANDEL h, const wchar_t* fileName, bool isMapFile)
{
	ht::HtHandle** ht = new ht::HtHandle*;

	HRESULT hr0 = ((IUnknown*)h)->QueryInterface(IID_IHT, (void**)&pIHT);
	if (SUCCEEDED(hr0))
	{
		HRESULT hr1 = pIHT->open(ht, fileName, isMapFile);
		if (!SUCCEEDED(hr1)) {
			pIHT->Release();
			throw (int)hr1;
			return nullptr;
		}
		else {
			pIHT->Release();
			return *ht;
		}
	}
	else {

		throw (int)hr0;
		return nullptr;
	}
}

ht::HtHandle* OS14_HTCOM::HT::open(OS14_HTCOM_HANDEL h, const wchar_t* fileName, const wchar_t* htUser, const wchar_t* htPassword, bool isMapFile)
{
	ht::HtHandle** ht = new ht::HtHandle*;

	HRESULT hr0 = ((IUnknown*)h)->QueryInterface(IID_IHT, (void**)&pIHT);
	if (SUCCEEDED(hr0))
	{
		HRESULT hr1 = pIHT->openAuth(ht, fileName, htUser, htPassword, isMapFile);
		if (FAILED(hr1)) {
			pIHT->Release();
			throw (int)hr1;
			return nullptr;
		}
		else {
			pIHT->Release();
			return *ht;
		}
	}
	else {

		throw (int)hr0;
		return nullptr;
	}
}

BOOL OS14_HTCOM::HT::snap(OS14_HTCOM_HANDEL h, ht::HtHandle* htHandle)
{
	BOOL rc = false;

	HRESULT hr0 = ((IUnknown*)h)->QueryInterface(IID_IHT, (void**)&pIHT);
	if (SUCCEEDED(hr0))
	{
		HRESULT hr1 = pIHT->snap(rc, htHandle);
		if (!SUCCEEDED(hr1)) {
			pIHT->Release();
			throw (int)hr1;
			return rc;
		}
		else {
			pIHT->Release();
			return rc;
		}
	}
	else {

		throw (int)hr0;
		return rc;
	}
}

BOOL OS14_HTCOM::HT::close(OS14_HTCOM_HANDEL h, ht::HtHandle* htHandle)
{
	BOOL rc = false;

	HRESULT hr0 = ((IUnknown*)h)->QueryInterface(IID_IHT, (void**)&pIHT);
	if (SUCCEEDED(hr0))
	{
		HRESULT hr1 = pIHT->close(rc, htHandle);
		if (!SUCCEEDED(hr1)) {
			pIHT->Release();
			throw (int)hr1;
			return rc;
		}
		else {
			pIHT->Release();
			return rc;
		}
	}
	else {

		throw (int)hr0;
		return rc;
	}
}

BOOL OS14_HTCOM::HT::insert(OS14_HTCOM_HANDEL h, ht::HtHandle* htHandle, const ht::Element* element)
{
	BOOL rc = false;

	HRESULT hr0 = ((IUnknown*)h)->QueryInterface(IID_IHT, (void**)&pIHT);
	if (SUCCEEDED(hr0))
	{
		HRESULT hr1 = pIHT->insert(rc, htHandle, element);
		if (!SUCCEEDED(hr1)) {
			pIHT->Release();
			throw (int)hr1;
			return rc;
		}
		else {
			pIHT->Release();
			return rc;
		}
	}
	else {

		throw (int)hr0;
		return rc;
	}
}

BOOL OS14_HTCOM::HT::removeOne(OS14_HTCOM_HANDEL h, ht::HtHandle* htHandle, const ht::Element* element)
{
	BOOL rc = false;

	HRESULT hr0 = ((IUnknown*)h)->QueryInterface(IID_IHT, (void**)&pIHT);
	if (SUCCEEDED(hr0))
	{
		HRESULT hr1 = pIHT->removeOne(rc, htHandle, element);
		if (!SUCCEEDED(hr1)) {
			pIHT->Release();
			throw (int)hr1;
			return rc;
		}
		else {
			pIHT->Release();
			return rc;
		}
	}
	else {

		throw (int)hr0;
		return rc;
	}
}

ht::Element* OS14_HTCOM::HT::get(OS14_HTCOM_HANDEL h, ht::HtHandle* htHandle, const ht::Element* element)
{
	ht::Element** rcElement = new ht::Element*;

	HRESULT hr0 = ((IUnknown*)h)->QueryInterface(IID_IHT, (void**)&pIHT);
	if (SUCCEEDED(hr0))
	{
		HRESULT hr1 = pIHT->get(rcElement, htHandle, element);
		if (!SUCCEEDED(hr1)) {
			pIHT->Release();
			throw (int)hr1;
			return nullptr;
		}
		else {
			pIHT->Release();
			return *rcElement;
		}
	}
	else {

		throw (int)hr0;
		return nullptr;
	}
}

BOOL OS14_HTCOM::HT::update(OS14_HTCOM_HANDEL h, ht::HtHandle* htHandle, const ht::Element* oldElement, const void* newPayload, int newPayloadLength)
{
	BOOL rc = false;

	HRESULT hr0 = ((IUnknown*)h)->QueryInterface(IID_IHT, (void**)&pIHT);
	if (SUCCEEDED(hr0))
	{
		HRESULT hr1 = pIHT->update(rc, htHandle, oldElement, newPayload, newPayloadLength);
		if (!SUCCEEDED(hr1)) {
			pIHT->Release();
			throw (int)hr1;
			return rc;
		}
		else {
			pIHT->Release();
			return rc;
		}
	}
	else {

		throw (int)hr0;
		return rc;
	}
}

const char* OS14_HTCOM::HT::getLastError(OS14_HTCOM_HANDEL h, ht::HtHandle* htHandle)
{
	const char** rcLastError = new const char*;

	HRESULT hr0 = ((IUnknown*)h)->QueryInterface(IID_IHT, (void**)&pIHT);
	if (SUCCEEDED(hr0))
	{
		HRESULT hr1 = pIHT->getLastError(rcLastError, htHandle);
		if (!SUCCEEDED(hr1)) {
			pIHT->Release();
			throw (int)hr1;
			return nullptr;
		}
		else {
			pIHT->Release();
			return *rcLastError;
		}
	}
	else {

		throw (int)hr0;
		return nullptr;
	}
}

void OS14_HTCOM::HT::print(OS14_HTCOM_HANDEL h, const ht::Element* element)
{
	BOOL rc = false;

	HRESULT hr0 = ((IUnknown*)h)->QueryInterface(IID_IHT, (void**)&pIHT);
	if (SUCCEEDED(hr0))
	{
		HRESULT hr1 = pIHT->print(element);
		if (!SUCCEEDED(hr1)) {
			pIHT->Release();
			throw (int)hr1;
		}
		else {
			pIHT->Release();
		}
	}
	else {

		throw (int)hr0;
	}
}



ht::Element* OS14_HTCOM::Element::createGetElement(OS14_HTCOM_HANDEL h, const void* key, int keyLength)
{
	ht::Element** rcElement = new ht::Element*;

	HRESULT hr0 = ((IUnknown*)h)->QueryInterface(IID_IElement, (void**)&pIElement);
	if (SUCCEEDED(hr0))
	{
		HRESULT hr1 = pIElement->createGetElement(rcElement, key, keyLength);
		if (!SUCCEEDED(hr1)) {
			pIElement->Release();
			throw (int)hr1;
			return nullptr;
		}
		else {
			pIElement->Release();
			return *rcElement;
		}
	}
	else {

		throw (int)hr0;
		return nullptr;
	}
}

ht::Element* OS14_HTCOM::Element::createInsertElement(OS14_HTCOM_HANDEL h, const void* key, int keyLength, const void* payload, int  payloadLength)
{
	ht::Element** rcElement = new ht::Element*;

	HRESULT hr0 = ((IUnknown*)h)->QueryInterface(IID_IElement, (void**)&pIElement);
	if (SUCCEEDED(hr0))
	{
		HRESULT hr1 = pIElement->createInsertElement(rcElement, key, keyLength, payload, payloadLength);
		if (!SUCCEEDED(hr1)) {
			pIElement->Release();
			throw (int)hr1;
			return nullptr;
		}
		else {
			pIElement->Release();
			return *rcElement;
		}
	}
	else {

		throw (int)hr0;
		return nullptr;
	}
}

ht::Element* OS14_HTCOM::Element::createUpdateElement(OS14_HTCOM_HANDEL h, const ht::Element* oldElement, const void* newPayload, int  newPayloadLength)
{
	ht::Element** rcElement = new ht::Element*;

	HRESULT hr0 = ((IUnknown*)h)->QueryInterface(IID_IElement, (void**)&pIElement);
	if (SUCCEEDED(hr0))
	{
		HRESULT hr1 = pIElement->createUpdateElement(rcElement, oldElement, newPayload, newPayloadLength);
		if (!SUCCEEDED(hr1)) {
			pIElement->Release();
			throw (int)hr1;
			return nullptr;
		}
		else {
			pIElement->Release();
			return *rcElement;
		}
	}
	else {

		throw (int)hr0;
		return nullptr;
	}
}

