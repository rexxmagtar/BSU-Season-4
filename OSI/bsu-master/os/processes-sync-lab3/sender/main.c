#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <windows.h>

#define MESSAGE_LENGTH 20

const char* event_name_format = "ev-sender%d-ready";
const char* mutex_name = "mtx-binary-file";
const char* empty_semaphore_name = "sem-empty";
const char* full_semaphore_name = "sem-full";

const int number_of_messages = 3;

int main(int argc, char* argv[]) {
	FILE* binary_file = fopen(argv[1], "r+b");

	int process_number = atoi(argv[2]);
	
	char lpEventName[20];
	sprintf(lpEventName, event_name_format, process_number);

	HANDLE hEvent = OpenEvent(EVENT_MODIFY_STATE, FALSE, lpEventName);
	if (hEvent == NULL)
		return GetLastError();

	SetEvent(hEvent);
	CloseHandle(hEvent);

	HANDLE hMutex = OpenMutex(SYNCHRONIZE, FALSE, mutex_name); 

	if (hMutex == NULL)
		return GetLastError();

	HANDLE hEmptySemaphore = OpenSemaphore(SYNCHRONIZE, FALSE, empty_semaphore_name);
	if (hEmptySemaphore == NULL)
		return GetLastError();

	HANDLE hFullSemaphore = OpenSemaphore(SEMAPHORE_MODIFY_STATE, FALSE, full_semaphore_name);
	if (hFullSemaphore == NULL)
		return GetLastError();

	char operation;
	char message[MESSAGE_LENGTH];

	int rear;

	for (;;) {
		printf("Input 's' to send message or 'q' to quit: ");
		scanf("%c%*c", &operation);

		if (operation == 's') {
			// produceProduct(); ���������� ������� 
			printf("Input message (less than 20 symbols): ");
			fgets(message, MESSAGE_LENGTH, stdin);
			message[strlen(message) - 1] = 0;

			// empty.wait(); ��������� ���-�� ��������� ����� �� ������ 
			WaitForSingleObject(hEmptySemaphore, INFINITE);

			// mutex.acquire(); ���� � ����������� ������   
			WaitForSingleObject(hMutex, INFINITE);

			//putProduct(); ��������� ������� �� ����� 
			rewind(binary_file);
			fread(&rear, sizeof(int), 1, binary_file);
			
			fseek(binary_file, sizeof(int) + sizeof(char) * MESSAGE_LENGTH * rear, SEEK_SET);
			fwrite(message, sizeof(char), MESSAGE_LENGTH, binary_file);

			rear++;
			rear %= number_of_messages;

			rewind(binary_file);
			fwrite(&rear, sizeof(int), 1, binary_file);

			fflush(binary_file);

			// mutex.release(); ����� �� ����������� ������  
			ReleaseMutex(hMutex);
			
			// full.signal(); ��������� ���-�� ��������� �� ������ 
			ReleaseSemaphore(hFullSemaphore, 1, NULL);
		}
		else if (operation == 'q')
			break;
	}

	CloseHandle(hFullSemaphore);
	CloseHandle(hEmptySemaphore);
	CloseHandle(hMutex);

	fclose(binary_file);

	return 0;
}