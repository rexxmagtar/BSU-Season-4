#include <stdlib.h>
#include <stdio.h>
#include <string.h>
#include <Windows.h>

#pragma warning(disable:4996)

enum threadStatus
{
	active,
	inactive
};

int* array;
int arraySize;
enum threadStatus* threadStatuses;


	
void WINAPI marker(LPVOID params)
{
	int threadIndex = ((int*)params)[0];
	printf("Thread with index %d started\n", threadIndex);
	while (threadStatuses[threadIndex]!=active)
	{
		//wating
	}
	//logic starts here
	srand(threadIndex);
	int markedElementsCount = 0;
	while (threadStatuses[threadIndex]==active)
	{
		int digit = rand();
		digit %= arraySize;
		if(array[digit]==0)
		{
			markedElementsCount++;
			Sleep(5);
			array[digit] = threadIndex;
			Sleep(5);
		}
		else
		{
			printf(" index: %d\n marked elements count: %d\n could not mark element at index: %d\n",
				threadIndex,markedElementsCount,digit);
			threadStatuses[threadIndex] = inactive;
		}
	}
	
}

int main()
{
	int threadCount;
	HANDLE* threads;
	
	
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
		int a=i;
		threadStatuses[i] = inactive;
		threads[i]= CreateThread(
			NULL,                   // default security attributes
			0,                      // use default stack size  
			marker,       // thread function name
			&a,          // argument to thread function 
			0,                      // use default creation flags 
			NULL);   // returns the thread identifier 
	}

	for (int i = 0; i < threadCount; ++i)
	{
		threadStatuses[i] = active;
	}
	WaitForMultipleObjects(threadCount, threads, 1, INFINITE);
}