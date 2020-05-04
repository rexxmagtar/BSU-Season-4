#include <math.h>
#include <stdlib.h>
#include <stdio.h>
#include "windows.h"

#pragma warning(disable:4996)
int main(int argc, char** argv)
{
	HANDLE mutex = CreateMutexA(NULL, FALSE, "m1");
	FILE* binFile = NULL;
	binFile = fopen(argv[1], "a");
	int messageCount = atoi(argv[2]);

	HANDLE semaphoreSender = CreateSemaphoreA(NULL, messageCount, messageCount, "semSender");
	HANDLE semaphoreReceiver = CreateSemaphoreA(NULL, 0, 1, "semReceiver");
	HANDLE waitAllSemaphore = CreateSemaphoreA(NULL, 0, 2, "waitAllSemaphore");

	ReleaseSemaphore(waitAllSemaphore, 1, NULL);
	while (1)
	{
		printf("choose option\n");
		printf("1. Write file\n");
		printf("2. Finish work\n");

		int option = 0;

		if(!scanf("%d", &option))
		{
			printf("Scanf error\n");
		}

		switch (option)
		{
		case 1:
		{
			char msg[20];
			printf("Insert message\n");

			if(!scanf("%s", &msg))
			{
				printf("Scanf error\n");
			}


			WaitForSingleObject(semaphoreSender, INFINITE);

			WaitForSingleObject(mutex, INFINITE);

			fprintf(binFile, "%s\n", msg);
			fflush(binFile);

			ReleaseMutex(mutex);
			ReleaseSemaphore(semaphoreReceiver, 1, NULL);
			break;

		}
		case 2:
		{
			return 0;

		}
		default:
			{
				break;
			}
		}

	}
}