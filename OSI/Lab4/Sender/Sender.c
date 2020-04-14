#include <stdlib.h>
#include <stdio.h>

#pragma warning(disable:4996)
int main(int argc,char **argv)
{
	FILE* binFile = NULL;
	binFile = fopen(argv[1], "a");

	while (1)
	{
		printf("choose option\n");
		printf("1. Write file\n");
		printf("2. Finish work\n");

		int option = 0;

		scanf("%d", &option);

		switch (option)
		{
		case 1:
		{
			char msg[20];
			printf("Insert message\n");
			scanf("%s", &msg);
			fprintf(binFile, "%s", msg);
				break;
		}
		case 2:
		{
			return 0;
				break;
			
		}
		}

	}
}