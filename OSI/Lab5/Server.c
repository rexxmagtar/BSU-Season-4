#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#include "Employee.h"
#include  "Windows.h"

#pragma warning(disable:4996)

#define BUFSIZE 3

int main()
{
	FILE* file = NULL;
	HANDLE* accessMutex = CreateMutexA(NULL, FALSE, "accessMutex");
	HANDLE* hPipe;
	DWORD cbRead;
	LPTSTR lpszPipename = TEXT("\\\\.\\pipe\\accessPipe");
	TCHAR chBuf[BUFSIZE];
	char* fileName = (char*)calloc(30, sizeof(char));
	int employeesCount;
	int clientCount = 0;


	printf("Insert file name\n");
	scanf("%s", fileName);

	printf("Insert employees count\n");
	scanf("%d", &employeesCount);

	struct employee* employees = (struct employee*)calloc(employeesCount, sizeof(struct employee));

	for (int i = 0; i < employeesCount; i++)
	{
		printf("Insert employee %d data\n", i + 1);
		char name[10];
		int num = 0;
		double hours = 0;

		printf("num:\n");
		scanf("%d", &num);
		printf("name:\n");
		scanf("%s", name);
		printf("hours:\n");
		scanf("%lf", &hours);

		strcpy(employees[i].name, name);
		employees[i].num = num;
		employees[i].hours = hours;
	}

	file = fopen(fileName, "wb");
	fwrite(employees, sizeof(struct employee), employeesCount, file);

	FILE* binFile = fopen(fileName, "rb");

	for (size_t i = 0; i < employeesCount; i++)
	{
		printf("employee %d info: \n name: %s ,  number: %d , hours : %lf\n", i, employees[i].name, employees[i].num,
		       employees[i].hours);
	}


	// Try to open a named pipe; wait for it, if necessary. 

	hPipe = CreateNamedPipe(
		lpszPipename,
		PIPE_ACCESS_DUPLEX,
		PIPE_TYPE_MESSAGE |
		PIPE_WAIT,
		PIPE_UNLIMITED_INSTANCES,
		sizeof(DWORD),
		sizeof(DWORD),
		100,
		NULL);

	if (hPipe == INVALID_HANDLE_VALUE)
	{
		printf("Failure!!!\n");
		system("pause");
		return -1;
	}

	printf("Success!!!\n");

	printf("Insert client count\n");

	if (!scanf("%d", &clientCount))
	{
		printf("Error scanf\n");
	}

	for (int i = 0; i < clientCount; i++)
	{
		char senderFullName[500];

		sprintf(senderFullName, "Client.exe");

		STARTUPINFO cif;
		ZeroMemory(&cif, sizeof(STARTUPINFO));
		cif.cb = (sizeof(STARTUPINFO));
		PROCESS_INFORMATION pi;

		if (!CreateProcess(NULL,
		                   senderFullName,
		                   NULL,
		                   NULL,
		                   TRUE,
		                   CREATE_NEW_CONSOLE,
		                   NULL,
		                   NULL,
		                   &cif,
		                   &pi))
		{
			printf("Could not create process\n");
		}
	}

	printf("Success!!!\nConnecting to pipe...");
	ConnectNamedPipe(hPipe, NULL);

	while (1)
	{
		DWORD cbWritten;

		LPCVOID lpvMessage = (char*)calloc(30, sizeof(char));

		ReadFile(
			hPipe, // pipe handle 
			chBuf, // buffer to receive reply 
			sizeof(chBuf), // size of buffer 
			&cbRead, // number of bytes read 
			NULL); // not overlapped

		switch (chBuf[0])
		{
		case 'r':
			{
				//read request code goes here...
				break;
			}
		case 'w':
		{
			//read request code goes here...
			break;
		}
		default:
			{
			printf("Unknown request %s \n", chBuf);
			}
		}

		printf("message: %s \n", chBuf);
	}
}
