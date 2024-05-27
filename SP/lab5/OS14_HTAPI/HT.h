#pragma once

#include "Element.h"
#include "Auth.h"

#define SECOND 10000000

namespace ht   
{


	struct HtHandle    
	{
		HtHandle();
		HtHandle(int capacity, int secSnapshotInterval, int maxKeyLength, int maxPayloadLength, const wchar_t* htUsersGroup, const wchar_t* fileName);
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

		wchar_t htUsersGroup[512];
	};

	HtHandle* create               
	(
		int	  capacity,						
		int   secSnapshotInterval,		
		int   maxKeyLength,					
		int   maxPayloadLength,			
		const wchar_t* htUsersGroup,	
		const wchar_t* fileName				
	); 
	HtHandle* open               
	(
		const wchar_t* fileName,       
		const wchar_t* htUser,			
		const wchar_t* htPassword,		
		bool isMapFile = false			
	); 	

	HtHandle* open                
	(
		const wchar_t* fileName,       
		bool isMapFile = false			
	); 	

	BOOL snap         
	(
		HtHandle* htHandle       
	);

	BOOL close        
	(
		const HtHandle* htHandle  
	);  


	BOOL insert   
	(
		HtHandle* htHandle,            
		const Element* element             
	);	


	BOOL removeOne      
	(
		HtHandle* htHandle,            
		const Element* element              
	);	 

	Element* get    
	(
		HtHandle* htHandle,        
		const Element* element              
	);  


	BOOL update   
	(
		HtHandle* htHandle,           
		const Element* oldElement,        
		const void* newPayload,         
		int             newPayloadLength     
	); 	

	const char* getLastError  
	(
		const HtHandle* htHandle                         
	);

	void print                               
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
		int	  capacity,				
		int   secSnapshotInterval,		
		int   maxKeyLength,             
		int   maxPayloadLength,			
		const wchar_t* htUsersGroup,	
		const wchar_t* fileName);	
	bool canCreateHtFor(const wchar_t* htUsersGroup);

	HtHandle* openWithoutAuth(const wchar_t* fileName, bool isMapFile);
	HtHandle* openHtFromFile(const wchar_t* fileName);
	HtHandle* openHtFromMapName(const wchar_t* fileName);
	bool canOpenHt(HtHandle* htHandle);
	bool canOpenHt(HtHandle* htHandle, const wchar_t* htUser, const wchar_t* htPassword);

	BOOL runSnapshotTimer(HtHandle* htHandle);
};
