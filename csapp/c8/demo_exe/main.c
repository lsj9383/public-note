#include <csapp.h>

#define MAXARGS 128

typedef enum RUN_TYPE{ bg, fg }run_type;

run_type parseline(char *buf, char *argv[]);
void eval(char *cmdline);

void debug( char *argv[] , run_type rt)
{
	for(; *argv != NULL; argv++)
	{
		puts(*argv);
	}
	switch(rt)
	{
		case bg:	puts("run in the background!");		break;
		case fg:	puts("run in the frontground!");	break;
		default:	puts("wrong...");			break;
	}
}

int main(void)
{
	char cmdline[MAXLINE];

	while(1)
	{
		printf("> ");
		Fgets( cmdline, MAXLINE, stdin );
		if(feof(stdin))
			exit(0);
		eval(cmdline);
	}
	return 0;
}

void eval(char *cmdline)
{
	char *argv[MAXARGS];
	char bufCmd[MAXLINE];
	pid_t pid=0;
	run_type rt;

	strcpy(bufCmd, cmdline);
	rt = parseline(bufCmd, argv);
	
	/* create sub child process */
	if((pid=Fork()) == 0)
	{
		if(execve(argv[0], argv, environ)<0)		/* child */
		{
			printf(" %s: command not found.\n ", argv[0]);
			return ;
		}
	}

	/* wait or no */
	if(rt==fg)
	{
		if(waitpid(pid, NULL, 0)<0)			/* wait.. */
		{
			unix_error("waitfg: waitpid error\n");
		}
	}
	else
	{
		printf("%d process run in the background\n", pid);
	}
}

run_type parseline(char *buf, char *argv[])
{
	char *firstSpace;
	int argc=0;

	buf[strlen(buf)-1] = ' ';			/* let '\n' to ' ' */
	for(; *buf==' '; buf++);			/* ignore space of the front */

	while( (firstSpace=strchr(buf, ' ')) )		/* have space */
	{
		argv[argc++] = buf;
		*firstSpace = '\0';
		buf = firstSpace+1;
		for(; *buf==' '; buf++);		/* ignore space of the front */
	}
	argv[argc]=NULL;

	if(argv[argc-1][0]=='&')
	{
		argv[argc-1]=NULL;
		argc--;
		return bg;
	}
	else
	{
		return fg;
	}
}
