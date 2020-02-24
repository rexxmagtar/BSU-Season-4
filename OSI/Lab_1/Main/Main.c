#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "windows.h"
#include "Header.h"

#pragma warning(disable:4996);



void PrintAllFile(FILE* file) {
	/* Get the number of bytes */
	fseek(file, 0, SEEK_END);
	long numbytes = ftell(file);

	/* reset the file position indicator to
	the beginning of the file */
	fseek(file, 0L, SEEK_SET);

	char* info = (char*)calloc(numbytes, sizeof(char));

	fread(info, 1, numbytes, file);
	printf("FileInfo: \n %s\n", info);
}



int main() {

	FILE* binFile = NULL;
	FILE* answerFile = NULL;
	char* binFileName = (char*)calloc(200, sizeof(char));
	char* answerFileName = (char*)calloc(200, sizeof(char));
	int countEmployees = 0;
	double grade = 0;

	printf("write binary file name\n");
	scanf("%s", binFileName);
	printf("write employees count\n");
	scanf("%d", &countEmployees);

	char creatorFullName[500];

	sprintf(creatorFullName, "Creator.exe %s %d", binFileName, countEmployees);

	STARTUPINFO cif;
	ZeroMemory(&cif, sizeof(STARTUPINFO));
	cif.cb = (sizeof(STARTUPINFO));
	PROCESS_INFORMATION pi;
	

	CreateProcess(NULL, creatorFullName, NULL, NULL, FALSE, CREATE_NEW_CONSOLE, NULL, NULL, &cif, &pi);
	WaitForSingleObject(pi.hProcess, INFINITE);

	CloseHandle(pi.hProcess);
	CloseHandle(pi.hThread);

	binFile = fopen(binFileName, "rb");
	int c=0;
	fscanf(binFile, "%d", &c);

	struct employee* employees = (struct student*)calloc(countEmployees, sizeof(struct employee));

	fread(employees, sizeof(struct employee), countEmployees, binFile);
	
	for (size_t i = 0; i < countEmployees; i++)
	{
		printf("employee %d info: \n name: %s ,  number: %d , hours : %lf\n", i, employees[i].name, employees[i].num, employees[i].hours);

	}

	printf("write answer file name\n");
	scanf("%s", answerFileName);
	printf("write salary\n");
	scanf("%lf", &grade);

	char reporterFullName[500];

	sprintf(reporterFullName, "Reporter.exe %s %s %lf", binFileName,answerFileName, grade);

	STARTUPINFO cifR;
	ZeroMemory(&cifR, sizeof(STARTUPINFO));
	cifR.cb = (sizeof(STARTUPINFO));
	PROCESS_INFORMATION piR;

	CreateProcess(NULL, reporterFullName, NULL, NULL, FALSE, CREATE_NEW_CONSOLE, NULL, NULL, &cifR, &piR);
	WaitForSingleObject(piR.hProcess, INFINITE);
	
	CloseHandle(piR.hProcess);
	CloseHandle(piR.hThread);
	
	answerFile = fopen(answerFileName, "r");


	PrintAllFile(answerFile);
	printf("\n End");
	free(binFileName);
	free(answerFileName);
	free(employees);
	system("Pause");

	

}

