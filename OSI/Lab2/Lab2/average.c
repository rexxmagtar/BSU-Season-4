#include <stdio.h>
#include<stdlib.h>
#include<string.h>
#include<float.h>
#include"windows.h"
#pragma warning(disable:4996)

extern float averageValue;
extern int arraySize;

void WINAPI average(LPVOID params) {
	averageValue = 0;

	for (int i = 0; i < arraySize; i++) {
		averageValue += ((float*)params)[i];
		Sleep(12);
	}
	averageValue /= arraySize;
}