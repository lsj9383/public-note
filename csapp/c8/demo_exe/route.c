#include <stdio.h>
#include <unistd.h>

int main(void)
{
	printf("%s\n", getcwd(NULL, 0));
	return 0;
}
