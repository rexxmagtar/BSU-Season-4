#include <stdlib.h>
#include <stdio.h>
#include "threadStatus.h"
#include <Windows.h>

#pragma warning(disable:4996)


int* array;
int arraySize;
enum threadStatus* threadStatuses;
int threadCount;
CRITICAL_SECTION section;

void WINAPI marker(LPVOID params);

//Checks if all thread's aren't active anymore
int CheckAllThreads()
{
	for (int i = 0; i < threadCount; ++i)
	{
	    if(threadStatuses[i]==active)
	    {
			return 0;
	    }
	}

	return 1;
}

int main()
{
	HANDLE* threads;
	InitializeCriticalSection(&section);
	
	printf("Write array size\n");
	scanf("%d", &arraySize);

	array = (int*)calloc(arraySize, sizeof(int));

	for (int i = 0; i < arraySize; ++i)
	{
		array[i] = 0;
	}
	
	printf("Write threads count\n");
	scanf("%d", &threadCount);

	threads = (HANDLE*)calloc(threadCount, sizeof(HANDLE));
	threadStatuses = (enum threadStatus*)calloc(threadCount, sizeof(threadStatuses));

	for (int i = 0; i < threadCount; ++i)//creating threads
	{
		int a=i+1;
		threadStatuses[i] = paused;
		threads[i]= CreateThread(
			NULL,                   // default security attributes
			0,                      // use default stack size  
			marker,       // thread function name
			(void*)a,          // argument to thread function 
			0,                      // use default creation flags 
			NULL);   // returns the thread identifier 
	}

	//Sending signals to threads to start
	for (int i = 0; i < threadCount; ++i)
	{
		threadStatuses[i] = active;
	}

	while(1)
	{
		//Wating for all threads to pause
		while (CheckAllThreads()!=1)
		{
		//wating	
		}

		printf("Array: ");
		for (int i = 0; i < arraySize; ++i)
		{
			printf("%d ", array[i]);
		}
		printf("\n Enter markers number to finish its work\n");
		int index = 0;
		scanf("%d", &index);

		threadStatuses[index-1] = finishing;
		//Wating for selected thread to finish
		while (threadStatuses[index-1]!=finished)
		{
			//wating
		}
		printf("Array changed: ");
		for (int i = 0; i < arraySize; ++i)
		{
			printf("%d ", array[i]);
		}

		//Sending to remaining threads signal to start
		for (int i = 0; i < threadCount; ++i)
		{
			if (threadStatuses[i] == paused) {
				threadStatuses[i] = active;
			}
		}
		
		int allFinished = 1;
		//Checking if all thread finished to work
		for (int i = 0; i < threadCount; ++i)
		{
			if(threadStatuses[i]!=finished)
			{
				allFinished = 0;
				break;
			}
		}

		//If all threads stopped - stop the main thread
		if(allFinished)
		{
			break;
		}
		
	}
	DeleteCriticalSection(&section);
	
}