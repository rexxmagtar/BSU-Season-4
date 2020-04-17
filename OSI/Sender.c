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

	while (1)
	{
		printf("choose option\n");
		printf("1. Write file\n");
		printf("2. Finish work\n");

		int option = 0;

		scanf("%d", &option);

		switch (option)
		{
		case 1:
		{
			char msg[20];
			printf("Insert message\n");

			scanf("%s", &msg);


			WaitForSingleObject(semaphoreSender, INFINITY);

			WaitForSingleObject(mutex, INFINITY);

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