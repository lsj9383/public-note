#include <csapp.h>
#define N 2

int main(void)
{
	int status=0 , i=0;
	pid_t pid=0;
	
	for(i=0; i<N; i++)
	{
		if( (pid=Fork())==0 )	/* child */
		{
			exit( 100+i );
		}
	}

	/* parent wait N children */
	while( (pid=waitpid(-1, &status, 0))>0 )	/* if pid==-1, means no children */
	{
		if(WIFEXITED(status))
		{
			printf("child %d terminated normally with exit status = %d\n", pid, WEXITSTATUS(status));
		}
		else
		{
			printf("child %d terminated abnormally\n", pid);
		}
	}

	if(errno != ECHILD)
	{
		unix_error("waitpid eror\n");
	}
	return 0;
}
