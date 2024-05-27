#pragma once

#include "Element.h"

#define SECOND 10000000

namespace ht    
{
	

	extern OS11_HTAPI_API struct HtHandle    
	{
		OS11_HTAPI_API HtHandle();
		OS11_HTAPI_API HtHandle(int capacity, int secSnapshotInterval, int maxKeyLength, int maxPayloadLength, const wchar_t* fileName);
		int     capacity;                
		int     secSnapshotInterval;    
		int     maxKeyLength;           
		int     maxPayloadLength;      
		wchar_t fileName[512];         
		HANDLE  file;                   
		HANDLE  fileMapping;              
		LPVOID  addr;                   
		char    lastErrorMessage[512];  
		time_t  lastSnaptime;           

		int count;						
		HANDLE snapshotTimer;			
		HANDLE mutex;					
	};

	extern OS11_HTAPI_API HtHandle* create          
	(
		int	  capacity,					   
		int   secSnapshotInterval,		   
		int   maxKeyLength,                
		int   maxPayloadLength,            
		const wchar_t* fileName            
	); 	

	extern OS11_HTAPI_API HtHandle* open                
	(
		const wchar_t* fileName,         
		bool isMapFile = false			
	); 

	extern OS11_HTAPI_API BOOL snap         
	(
		HtHandle* htHandle           
	);

	extern OS11_HTAPI_API BOOL close        
	(
		const HtHandle* htHandle           
	);	  


 OS11_HTAPI_API BOOL insert      
	(
		HtHandle* htHandle,          
		const Element* element              
	);	


	extern OS11_HTAPI_API BOOL removeOne      
	(
		HtHandle* htHandle,           
		const Element* element              
	);	

	extern OS11_HTAPI_API Element* get     
	(
		HtHandle* htHandle,            
		const Element* element              
	); 	 


	extern OS11_HTAPI_API BOOL update     
	(
		HtHandle* htHandle,        
		const Element* oldElement,          
		const void* newPayload,          
		int             newPayloadLength     
	); 	

	extern OS11_HTAPI_API const char* getLastError  
	(
		const HtHandle* htHandle                         
	);

	extern OS11_HTAPI_API void print                               
	(
		const Element* element             
	);

	int hashFunction(const char* key, int capacity);
	int nextHash(int currentHash, const char* key, int capacity);

	int findFreeIndex(const HtHandle* htHandle, const Element* element);
	BOOL writeToMemory(const HtHandle* const htHandle, const Element* const element, int index);
	int incrementCount(HtHandle* const htHandle);

	int findIndex(const HtHandle* htHandle, const Element* element);
	Element* readFromMemory(const HtHandle* const htHandle, Element* const element, int index);
	BOOL clearMemoryByIndex(const HtHandle* const htHandle, int index);
	int decrementCount(HtHandle* const htHandle);

	void CALLBACK snapAsync(LPVOID prm, DWORD, DWORD);
	const char* writeLastError(HtHandle* const htHandle, const char* msg);

	HtHandle* createHt(
		int	  capacity,					// емкость хранилища
		int   secSnapshotInterval,		// переодичность сохранения в сек.
		int   maxKeyLength,             // максимальный размер ключа
		int   maxPayloadLength,			// максимальный размер данных
		const wchar_t* fileName);		// имя файла 
	HtHandle* openHtFromFile(const wchar_t* fileName);
	HtHandle* openHtFromMapName(const wchar_t* fileName);
	BOOL runSnapshotTimer(HtHandle* htHandle);
};
