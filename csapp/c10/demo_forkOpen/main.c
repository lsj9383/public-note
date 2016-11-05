#include <csapp.h>

int main(void)
{
    int fd;
    char c=0;
    int pid=0;

    fd = Open("foobar.txt", O_RDONLY, 0);

    if( (pid=Fork())==0 )
    {	/* child process */
	Read(fd, &c, 1);
	exit(0);
    }

    waitpid(pid, NULL, 0);

    Read(fd, &c, 1);

    printf("c = %c\n", c);
    return 0;
}
