#include <stdio.h>
#include<stdlib.h>
#include<string.h>
#include"windows.h"
#pragma warning(disable:4996)



int *vector;
int *resultVector;
int matrixSize = 0;


void WINAPI MulFunc(LPVOID params) {
	int sumResult=0;

	for (int i = 0; i < matrixSize; i++) {
		sumResult += ((int*)params)[i] * vector[i];
	}
	int index = ((int*)params)[matrixSize];
	resultVector[index] = sumResult;

	printf("multiplication raw %d on vector equal: %d\n", index, sumResult);
	Sleep(7);
}

int main() {


	

	printf("write matrix size\n");
	scanf("%d", &matrixSize);

	HANDLE  *threadArray = (HANDLE*)calloc(matrixSize, sizeof(HANDLE));
	int **matrix = (int**)calloc(matrixSize, sizeof(int*));
	for (int i = 0; i < matrixSize; i++) {
		matrix[i]= (int*)calloc(matrixSize+1, sizeof(int));
	}
	vector = (int*)calloc(matrixSize, sizeof(int));
	resultVector = (int*)calloc(matrixSize, sizeof(int));


	for (int i = 0; i < matrixSize; i++) {

		for (size_t j = 0; j < matrixSize; j++)
		{
			int value = 0;
			printf("write matrix [%d][%d] element value\n", 1 + i , 1 + j);
			scanf("%d", &value);
			matrix[i][j] = value;
		}
		matrix[i][matrixSize] = i;

	}

	printf("--------------------");

	for (int i = 0; i < matrixSize; i++) {

		int value = 0;
		printf("write vector [%d] element value\n", i + 1);
		scanf("%d", &value);
		vector[i] = value;
	}

	for (size_t i = 0; i < matrixSize; i++)
	{
		threadArray[i] = CreateThread(
			NULL,                   // default security attributes
			0,                      // use default stack size  
			MulFunc,       // thread function name
			matrix[i],          // argument to thread function 
			0,                      // use default creation flags 
			NULL);   // returns the thread identifier 

	}

	WaitForMultipleObjects(matrixSize, threadArray, TRUE, INFINITE);

	printf("Result vector:\n");
	for (size_t i = 0; i < matrixSize; i++)
	{
		printf("%d ", resultVector[i]);
	}

	system("pause");
	
}