#include <csapp.h>

int main(int argc, char *argv[])
{
//	execve("/bin/sh -c command ", argv, environ);
	int i=0;
	for(i=0; i<argc+1; i++)
	{
		if(argv[i]!=NULL)
			puts(argv[i]);
		else
			puts("NULL");
	}
	return 0;
}
