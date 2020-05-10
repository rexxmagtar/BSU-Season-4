#include <stdio.h>
#include "Employee.h"
#include  "Windows.h"

#pragma warning(disable:4996)

#define BUFSIZE 20

extern CRITICAL_SECTION* recordWriteSections;
extern CRITICAL_SECTION* recordReadSections;

extern CRITICAL_SECTION* readersCountsSections;

extern HANDLE* allClientsFinishedSemaphore;

extern struct employee* employeeData;

extern int employeesCount;
extern int countActiveClients;
extern int* currentReaderCounts;


DWORD WINAPI handleClient(LPVOID params)
{
	DWORD cbWritten;
	TCHAR chBuf[BUFSIZE];
	DWORD cbRead;
	HANDLE hPipe = (HANDLE)params;

	while (1)
	{
		int recordNumber = 0;
		ReadFile(
			hPipe, // pipe handle 
			chBuf, // buffer to receive reply 
			sizeof(chBuf), // size of buffer 
			&cbRead, // number of bytes read 
			NULL); // not overlapped

		sscanf(chBuf, "%*c %d", &recordNumber);


		switch (chBuf[0])
		{
		case 'r':
		{
			EnterCriticalSection(&recordReadSections[recordNumber - 1]);
			LeaveCriticalSection(&recordReadSections[recordNumber - 1]);

			EnterCriticalSection(&readersCountsSections[recordNumber - 1]);

			currentReaderCounts[recordNumber - 1]++;

			if (currentReaderCounts[recordNumber - 1] == 1)
			{
				EnterCriticalSection(&recordWriteSections[recordNumber - 1]);
			}

			LeaveCriticalSection(&readersCountsSections[recordNumber - 1]);

			WriteFile(hPipe, employeeData + recordNumber - 1, sizeof(struct employee), &cbWritten, NULL);

			ReadFile(
				hPipe, // pipe handle 
				chBuf, // buffer to receive reply 
				sizeof(chBuf), // size of buffer 
				&cbRead, // number of bytes read 
				NULL); // not overlapped

			EnterCriticalSection(&readersCountsSections[recordNumber - 1]);

			currentReaderCounts[recordNumber - 1]--;

			if (currentReaderCounts[recordNumber - 1] == 0)
			{
				LeaveCriticalSection(&recordWriteSections[recordNumber - 1]);
			}

			LeaveCriticalSection(&readersCountsSections[recordNumber - 1]);


			break;
		}
		case 'w':
		{
			EnterCriticalSection(&recordWriteSections[recordNumber - 1]);

			EnterCriticalSection(&recordReadSections[recordNumber - 1]);

			WriteFile(hPipe, employeeData + recordNumber - 1, sizeof(struct employee), &cbWritten, NULL);


			ReadFile(hPipe, employeeData + recordNumber - 1, sizeof(struct employee), &cbWritten, NULL);

			ReadFile(
				hPipe, // pipe handle 
				chBuf, // buffer to receive reply 
				sizeof(chBuf), // size of buffer 
				&cbRead, // number of bytes read 
				NULL); // not overlapped


			LeaveCriticalSection(&recordReadSections[recordNumber - 1]);

			LeaveCriticalSection(&recordWriteSections[recordNumber - 1]);

			break;
		}
		case 'q':
		{
			ReleaseSemaphore(allClientsFinishedSemaphore, 1, NULL);
			return 0;
		}
		default:
		{
			printf("Unknown request %s \n", chBuf);
		}
		}
	}
}
