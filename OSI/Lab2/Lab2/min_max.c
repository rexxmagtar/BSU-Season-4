#include <stdio.h>
#include<stdlib.h>
#include<string.h>
#include<float.h>
#include"windows.h"

extern int minIndex;
extern int maxIndex;
extern int arraySize;

void WINAPI min_max(LPVOID params) {

	float minValue = FLT_MAX;
	float maxValue = FLT_MIN;

	for (int i = 0; i < arraySize; i++) {
		if (minValue > ((float*)params)[i]) {
			minValue = ((float*)params)[i];
			minIndex = i;
		}
		Sleep(7);
		if (maxValue < ((float*)params)[i]) {
			maxValue = ((float*)params)[i];
			maxIndex = i;
		}
		Sleep(7);
	}
}