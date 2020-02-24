#include <stdio.h>
#include<stdlib.h>
#include<string.h>
#include"Header.h"

#pragma warning(disable:4996);

int MaxNameSize = 200;

int main(int argc, char **argv) {

	FILE* file=NULL;
	char* fileName = (char*)calloc(MaxNameSize,sizeof(char));
	int count;

	strcpy(fileName, argv[1]);
	count = atoi(argv[2]);

	struct employee* employees = (struct employee*)calloc(count, sizeof(struct employee));

	for (int i = 0; i < count; i++) {
		printf("Insert employee %d data\n",i+1);
		char name[10];
		int num=0;
		double hours=0;

		
		printf("num:\n");
		scanf("%d", &num);
		printf("name:\n");
		scanf("%s", name);
		printf("hours:\n");
		scanf("%lf", &hours);

		strcpy(employees[i].name, name);
		employees[i].num = num;
		employees[i].hours = hours;


	}

	file = fopen(fileName, "wb");
	fprintf(file, "%d", count);
	fwrite(employees, sizeof(struct employee), count, file);

	
	
	free(fileName);
	free(employees);
}