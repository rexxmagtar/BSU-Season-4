
#include <stdlib.h>
#include <stdio.h>
#include "windows.h"

#pragma warning(disable:4996)

int main()
{
	FILE* binaryFile = NULL;

	char binFileName[30];
	int binFileMessagesCount = 0;
	int receiversCount = 0;
	
	printf("Insert binary file's name\n");
	scanf("%s", &binFileName);

	binaryFile = fopen(binFileName, "w+");

	printf("Insert binary file's messages count\n");
	scanf("%d", &binFileMessagesCount);

	printf("Insert receivers count\n");
	scanf("%d", &receiversCount);


	for (int i = 0; i < receiversCount; i++) {
		char senderFullName[500];

		sprintf(senderFullName, "Sender.exe %s", binFileName);

		STARTUPINFO cif;
		ZeroMemory(&cif, sizeof(STARTUPINFO));
		cif.cb = (sizeof(STARTUPINFO));
		PROCESS_INFORMATION pi;

		if(!CreateProcess(NULL,
			senderFullName,
			NULL, 
			NULL, 
			FALSE,
			CREATE_NEW_CONSOLE, 
			NULL,
			NULL,
			&cif, 
			&pi))
		{
			printf("Could not create process\n");
		}
	}

	while (1)
	{
		printf("choose option\n");
		printf("1. Read file\n");
		printf("2. Finish work\n");

		int option = 0;

		scanf("%d", &option);

		switch(option)
		{
		case 1:
			{
			char line[100];
			while (fgets(line, 100, binaryFile))
			{
				fprintf("%s\n", line);
			}
			}
		case 2:
			{
			return 0;
			}
		}

	}
	
}



//#include <windows.h>
//#include <stdio.h>
//#include <tchar.h>
//
//int main(int argc, char **argv)
//{
//    STARTUPINFO si;
//    PROCESS_INFORMATION pi;
//
//    ZeroMemory(&si, sizeof(si));
//    si.cb = sizeof(si);
//    ZeroMemory(&pi, sizeof(pi));
//    char path[40];
//    strcpy(path, "Sender.exe");
//    int a = strcmp(path, argv[1]);
// /*   if (argc != 2)
//    {
//        printf("Usage: %s [cmdline]\n", argv[0]);
//        return;
//    }*/
//
//    // Start the child process. 
//    if (!CreateProcess(NULL,   // No module name (use command line)
//       (LPWSTR)path,        // Command line
//        NULL,           // Process handle not inheritable
//        NULL,           // Thread handle not inheritable
//        FALSE,          // Set handle inheritance to FALSE
//        0,              // No creation flags
//        NULL,           // Use parent's environment block
//        NULL,           // Use parent's starting directory 
//        &si,            // Pointer to STARTUPINFO structure
//        &pi)           // Pointer to PROCESS_INFORMATION structure
//        )
//    {
//        printf("CreateProcess failed (%d).\n", GetLastError());
//        return;
//    }
//
//    // Wait until child process exits.
//    WaitForSingleObject(pi.hProcess, INFINITE);
//
//    // Close process and thread handles. 
//    CloseHandle(pi.hProcess);
//    CloseHandle(pi.hThread);
//}