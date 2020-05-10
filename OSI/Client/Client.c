#include <stdio.h>

#include "Employee.h"
#include  "Windows.h"

#pragma warning(disable:4996)

int main()
{
	HANDLE* accessMutex = CreateMutexA(NULL, FALSE, "accessMutex");

	LPTSTR lpszPipename = TEXT("\\\\.\\pipe\\accessPipe");

	HANDLE* hPipe = INVALID_HANDLE_VALUE;

	while (hPipe == INVALID_HANDLE_VALUE)
	{
		hPipe = CreateFile(
			lpszPipename, // pipe name 
			GENERIC_READ | // read and write access 
			GENERIC_WRITE,
			0, // no sharing 
			NULL, // default security attributes
			OPEN_EXISTING, // opens existing pipe 
			0, // default attributes 
			NULL);
	}


	printf("Client started\n");

	while (1)
	{
		printf("Insert command.\n r - read \n w - write  \n q - quit \n");


		char* lpvMessage = (char*)calloc(30, sizeof(char));

		scanf("%s", lpvMessage);

		printf("Insert record number \n");

		DWORD cbWritten;

		int Success;
		int recordNumber = 0;

		if (lpvMessage[0] != 'q')
		{
			scanf("%d", &recordNumber);

			sprintf(lpvMessage, "%c %d", lpvMessage[0], recordNumber);

			Success = WriteFile(
				hPipe, // pipe handle 
				lpvMessage, // message 
				sizeof(lpvMessage), // message length 
				&cbWritten, // bytes written 
				NULL); // not overlapped 

			if (Success == FALSE)
			{
				printf("fail\n");
				system("pause");
				return -1;
			}
		}
		switch (lpvMessage[0])
		{
		case 'r':
			{
				//TODO: implement read
				struct employee employee;

				ReadFile(hPipe, &employee, sizeof(employee), &cbWritten, NULL);

				printf("Employee %d info:\n number = %d \n name = %s \n hours = %f \n", recordNumber, employee.num,
				       employee.name, employee.hours);

				break;
			}

		case 'w':
			{
				//TODO: implement write

				struct employee employee;

				ReadFile(hPipe, &employee, sizeof(employee), &cbWritten, NULL);

				printf("Employee %d info:\n number = %d \n name = %s \n hours = %f \n", recordNumber, employee.num,
				       employee.name, employee.hours);

				printf("Insert employee data\n");
				char name[10];
				int num = 0;
				double hours = 0;

				printf("num:\n");
				scanf("%d", &num);
				printf("name:\n");
				scanf("%s", name);
				printf("hours:\n");
				scanf("%lf", &hours);

				strcpy(employee.name, name);
				employee.num = num;
				employee.hours = hours;

				printf("Enter any key to send modified record\n");
				getchar();
				getchar();

				WriteFile(hPipe, &employee, sizeof(employee), &cbWritten, NULL);


				break;
			}

		case 'q':
			{
				WriteFile(hPipe, "quit", sizeof("quit"), &cbWritten, NULL);
				return 0;
			}

		default:
			{
				printf("Unknown command %c \n", lpvMessage[0]);
				break;
			}
		}

		//release record here...
		printf("Enter any key to free access to record\n");
		getchar();
		if (lpvMessage[0] == 'r')
		{
			getchar();
		}

		WriteFile(hPipe, "Free record", sizeof("Free record"), &cbWritten, NULL);
	}
}
