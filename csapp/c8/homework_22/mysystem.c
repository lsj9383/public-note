#include <csapp.h>

void GenCommand( char *command, char **argv )
{
	while( *argv != NULL )
	{
		char *args = *argv;
		while( *args!='\0' )
		{
			*(command++)=*(args++);
		}
		*(command++)=' ';
		argv++;
	}
	*(command-1)='\0';
}

int main(int argc, char *argv[])
{
	char *args[4];
	char command[256];
	int pid=0;

	GenCommand( command, argv+1 );	
	puts(command);

	args[0] = "sh";
	args[1] = "-c";
	args[2] = command;
	args[3] = NULL;
	

	if( (pid = Fork())==0 )
	{	/* child */
		execve( "/bin/sh", args, environ );
	}
	else
	{	/* parent */
		int status;

		waitpid( pid, &status, 0);
		printf(" done! \n");
	}

	return 0;
}

