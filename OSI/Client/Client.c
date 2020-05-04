
#include <stdio.h>
#include  "Windows.h"

#pragma warning(disable:4996)

int main()
{
	HANDLE* accessMutex = CreateMutexA(NULL, FALSE, "accessMutex");

	LPTSTR lpszPipename = TEXT("\\\\.\\pipe\\accessPipe");

	HANDLE* hPipe=INVALID_HANDLE_VALUE;

	while (hPipe==INVALID_HANDLE_VALUE)
	{
		hPipe = CreateFile(
			lpszPipename,   // pipe name 
			GENERIC_READ |  // read and write access 
			GENERIC_WRITE,
			0,              // no sharing 
			NULL,           // default security attributes
			OPEN_EXISTING,  // opens existing pipe 
			0,              // default attributes 
			NULL);
	}
	

	
	printf("Client started\n");
	
	while (1)
	{

		printf("Insert msg\n");
		
		LPCVOID lpvMessage= (char*)calloc(30, sizeof(char));

		scanf("%s", lpvMessage);

		printf("Sending %s \n", lpvMessage);
		
		DWORD cbWritten;
		
		int Success;
		
	Success =	WriteFile(
			hPipe,                  // pipe handle 
			lpvMessage,             // message 
			sizeof( lpvMessage),              // message length 
			&cbWritten,             // bytes written 
			NULL);                  // not overlapped 
	
		if (Success == FALSE) {
			printf("fail\n");
			system("pause");
			return -1;
		}
	}
	
}
