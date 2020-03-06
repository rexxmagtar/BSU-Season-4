#include <stdio.h>
#include<stdlib.h>
#include<string.h>
#include<float.h>
#include"windows.h"
#pragma warning(disable:4996)

int minIndex = 0;
int maxIndex = 0;
float averageValue;
int arraySize = 0;

extern void WINAPI min_max(LPVOID params);
extern void WINAPI average(LPVOID params);

int main() {


	

	printf("write array size\n");
	scanf("%d", &arraySize);

	HANDLE  *threadArray = (HANDLE*)calloc(arraySize, sizeof(HANDLE));
	HANDLE MinMaxThread;
	HANDLE AverageThread;
	float *array = (float*)calloc(arraySize, sizeof(float));



	for (int i = 0; i < arraySize; i++) {


			float value = 0;
			printf("write array [%d] element value\n", i);
			scanf("%f", &value);
			array[i] = value;


	}

	printf("--------------------\n");




		MinMaxThread = CreateThread(
			NULL,                   // default security attributes
			0,                      // use default stack size  
			min_max,       // thread function name
			array,          // argument to thread function 
			0,                      // use default creation flags 
			NULL);   // returns the thread identifier 


		AverageThread = CreateThread(
			NULL,                   // default security attributes
			0,                      // use default stack size  
			average,       // thread function name
			array,          // argument to thread function 
			0,                      // use default creation flags 
			NULL);   // returns the thread identifier 
	

		WaitForSingleObject(MinMaxThread, INFINITE);
		WaitForSingleObject(AverageThread, INFINITE);

		array[minIndex] = averageValue;
		array[maxIndex] = averageValue;
	printf("Result array:\n");
	for (size_t i = 0; i < arraySize; i++)
	{
		printf("%f ", array[i]);
	}

	system("pause");
	
}