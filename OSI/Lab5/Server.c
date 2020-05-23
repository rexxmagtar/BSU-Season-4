#include <math.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "Employee.h"
#include  "Windows.h"

#pragma warning(disable:4996)
#pragma warning(disable:6031)

#define BUFSIZE 20

CRITICAL_SECTION* recordWriteSections;
CRITICAL_SECTION* recordReadSections;

CRITICAL_SECTION* readersCountsSections;

HANDLE* allClientsFinishedSemaphore;

struct employee* employeeData;

int employeesCount;
int countActiveClients;
int* currentReaderCounts;

//Get pipe as an argument. Then sends/receive messages from client
extern DWORD WINAPI handleClient(LPVOID params);


int main()
{
	HANDLE file;

	LPTSTR lpszPipename = TEXT("\\\\.\\pipe\\accessPipe");
	char* fileName = (char*)calloc(30, sizeof(char));
	int clientCount = 0;


	printf("Insert file name\n");
	scanf("%s", fileName);

	file = CreateFile(fileName, GENERIC_READ | GENERIC_WRITE, 0, NULL, CREATE_ALWAYS,
	                  FILE_ATTRIBUTE_NORMAL, NULL);

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

		WriteFile(file, &employees[i], sizeof(struct employee), NULL, NULL);
	}

	DWORD dwFileSize = GetFileSize(file, NULL);

	HANDLE Mapping = CreateFileMapping(file, NULL, PAGE_READWRITE, 0, 0, NULL);

	if (Mapping == NULL)
	{
		printf("CreateFileMapping failed, %d\n", GetLastError());
		return -1;
	}

	employeeData = (struct employee*)MapViewOfFile(Mapping, FILE_MAP_READ | FILE_MAP_WRITE, 0, 0, dwFileSize);

	if (employeeData == NULL)
	{
		printf("MapViewOfFile failed, %d\n", GetLastError());
		return -1;
	}
	currentReaderCounts = (int*)calloc(employeesCount, sizeof(int));

	readersCountsSections = (CRITICAL_SECTION*)calloc(employeesCount, sizeof(CRITICAL_SECTION));
	recordWriteSections = (CRITICAL_SECTION*)calloc(employeesCount, sizeof(CRITICAL_SECTION));
	recordReadSections = (CRITICAL_SECTION*)calloc(employeesCount, sizeof(CRITICAL_SECTION));

	for (int i = 0; i < employeesCount; ++i)
	{
		CRITICAL_SECTION sectionW;
		InitializeCriticalSection(&sectionW);
		recordWriteSections[i] = sectionW;

		CRITICAL_SECTION sectionR;
		InitializeCriticalSection(&sectionR);
		recordReadSections[i] = sectionR;

		CRITICAL_SECTION sectionReaders;
		InitializeCriticalSection(&sectionReaders);

		readersCountsSections[i] = sectionReaders;

		currentReaderCounts[i] = 0;
	}

	for (size_t i = 0; i < employeesCount; i++)
	{
		printf("employee %d info: \n  number: %d , name: %s , hours : %lf\n", i+1, employees[i].num, employees[i].name,
		       employees[i].hours);
	}


	printf("Insert client count\n");

	if (!scanf("%d", &clientCount))
	{
		printf("Error scanf\n");
	}

	allClientsFinishedSemaphore = CreateSemaphoreA(NULL, 0, clientCount - 1, "FinishSemaphore");

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

		HANDLE hPipe = CreateNamedPipe(
			lpszPipename,
			PIPE_ACCESS_DUPLEX,
			PIPE_TYPE_MESSAGE |
			PIPE_WAIT,
			3,
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
		
		ConnectNamedPipe(hPipe, NULL);

		CreateThread(NULL, 0, handleClient, (LPVOID)hPipe, 0, NULL);
	}


	//Wating for all clients to finish
	for (int i = 0; i < clientCount; ++i)
	{
		WaitForSingleObject(allClientsFinishedSemaphore, INFINITE);
	}

	SetFilePointer(file, 0, NULL, FILE_BEGIN);

	int i = 1;
	struct employee employee;
	DWORD numOfBytesRead;

	printf("\nFile:\n");

	while (ReadFile(file, &employee, sizeof(struct employee), &numOfBytesRead, NULL)) {
		if (numOfBytesRead == 0)
			break;

		printf("employee %d info: \n   number: %d , name: %s , hours : %lf\n", i,  employee.num, employee.name,
			employee.hours);

		i++;
	}
	
	return 0;
}
