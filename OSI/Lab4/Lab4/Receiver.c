
#include <math.h>
#include <stdlib.h>
#include <stdio.h>
#include "windows.h"

#pragma warning(disable:4996)

int main()
{
	FILE* binaryFile = NULL;

	HANDLE fileAccessMutex = CreateMutexA(NULL, FALSE, "m1");
	HANDLE waitAllSemaphore;
	
	HANDLE semaphoreSender;
	HANDLE semaphoreReceiver;


	char binFileName[30];
	int binFileMessagesCount = 0;
	int sendersCount = 0;

	printf("Insert binary file's name\n");
	if(!scanf("%s", &binFileName))
	{
		printf("Error scanf\n");
	}

	binaryFile = fopen(binFileName, "w+");

	printf("Insert binary file's messages count\n");
	if(!scanf("%d", &binFileMessagesCount))
	{
		printf("Error scanf\n");
	}

	printf("Insert senders count\n");
	if(!scanf("%d", &sendersCount))
	{
		printf("Error scanf\n");
	}

	semaphoreSender = CreateSemaphoreA(NULL, binFileMessagesCount, binFileMessagesCount, "semSender");
    semaphoreReceiver = CreateSemaphoreA(NULL, 0, 1, "semReceiver");

	//waits for all processes to start.
	waitAllSemaphore = CreateSemaphoreA(NULL, 0,sendersCount,"waitAllSemaphore");

	


	for (int i = 0; i < sendersCount; i++) {
		char senderFullName[500];

		sprintf(senderFullName, "Sender.exe %s %d", binFileName, binFileMessagesCount);

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

	//Wating for all processes to start
	for (int i=0;i<sendersCount;i++)
	{
		WaitForSingleObject(waitAllSemaphore, INFINITE);
	}

	while (1)
	{
		printf("choose option\n");
		printf("1. Read file\n");
		printf("2. Finish work\n");

		int option = 0;

		if(!scanf("%d", &option))
		{
			printf("Scanf error");
		}

		switch (option)
		{
		case 1:
		{
			WaitForSingleObject(semaphoreReceiver, INFINITE);

			WaitForSingleObject(fileAccessMutex, INFINITE);

			binaryFile = fopen(binFileName, "rb");
			char line[100];
			int msgCount = 0;
			while (fgets(line, 100, binaryFile))
			{
				msgCount++;
				printf("%s", line);
			}
				
			if(!fopen(binFileName, "wb"))
			{
				printf("Fopen error\n");
			}

			ReleaseMutex(fileAccessMutex);

			ReleaseSemaphore(semaphoreSender, msgCount, NULL);

			break;


		}
		case 2:
		{
				
			return 0;
				break;
		}
		}

	}

}

