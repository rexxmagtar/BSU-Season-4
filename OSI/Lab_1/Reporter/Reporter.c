#include <stdio.h>
#include<stdlib.h>
#include<string.h>
#include "Header.h"

#pragma warning(disable:4996);


int main(int argc, char **argv) {

	FILE* binFile = NULL;
	FILE* answerFile = NULL;

	char* binFileName = (char*)calloc(200, sizeof(char));
	char* answerFileName = (char*)calloc(200, sizeof(char));
	double salaryPerHour = 0;
	int countEmployeers = 0;
	int countGreater = 0;

	strcpy(binFileName, argv[1]);
	strcpy(answerFileName, argv[2]);
	salaryPerHour = atof(argv[3]);

	binFile = fopen(binFileName, "rb");
	answerFile = fopen(answerFileName, "w");

	

	fscanf(binFile, "%d", &countEmployeers);

	struct employee* employees = (struct employee*)calloc(countEmployeers, sizeof(struct employee));

	fread(employees, sizeof(struct employee), countEmployeers, binFile);


	fprintf(answerFile, "report of file: %s\n", binFileName);
	fprintf(answerFile, " employee's number, employee's name, hours, salary\n");

	for (int i = 0; i < countEmployeers; i++) {
		
		

			fprintf(answerFile," %d,  %s,  %lf, %lf\n", 
				 employees[i].num, employees[i].name , employees[i].hours, employees[i].hours*salaryPerHour);
		
	}

	free(answerFileName);
	free(binFileName);
	free(employees);
}