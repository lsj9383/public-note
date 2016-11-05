#include "csapp.h"

void handler(int sig)
{
	printf("BEEP\n");
}

int main()
{
//	Signal(SIGALRM, handler);
	while(1);
	return 0;
}
