#include <csapp.h>

int main(void)
{
	char *argv[4];

	argv[0] = "sh";
	argv[1] = "-c";
	argv[2] = "./out sdf df sad df";
	argv[3] = NULL;
	execve("/bin/sh", argv, environ);

	return 0;
}
