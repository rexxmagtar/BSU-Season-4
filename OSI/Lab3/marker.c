#include <stdlib.h>
#include <stdio.h>
#include <Windows.h>
#include "threadStatus.h"

extern int* array;
extern int arraySize;
extern enum threadStatus* threadStatuses;
extern int threadCount;
extern  LPCRITICAL_SECTION section;

void WINAPI marker(LPVOID params)
{
	int threadIndex = (int)params - 1;
	srand(threadIndex + 1);
	printf("Thread with number %d started\n", threadIndex + 1);
	while (1)
	{


		while (threadStatuses[threadIndex] != active)
		{
			//wating
		}
		//logic starts here
		int markedElementsCount = 0;
		while (1)
		{
			int digit = rand();
			digit %= arraySize;
			EnterCriticalSection(&section);
			if (array[digit] == 0)
			{
				markedElementsCount++;
				Sleep(5);
				array[digit] = threadIndex + 1;
				Sleep(5);
			}
			else
			{
				printf(" number: %d\n marked elements count: %d\n could not mark element at index: %d\n",
					threadIndex + 1, markedElementsCount, digit);
				threadStatuses[threadIndex] = paused;
				LeaveCriticalSection(&section);
				break;
			}
			LeaveCriticalSection(&section);
		}
		while (threadStatuses[threadIndex] == paused)
		{
			//waiting
		}

		if (threadStatuses[threadIndex] == finishing)
		{
			for (int i = 0; i < arraySize; ++i)
			{
				if (array[i] == threadIndex + 1)
				{
					array[i] = 0;
				}
			}
			threadStatuses[threadIndex] = finished;
			break;
		}
	}

}