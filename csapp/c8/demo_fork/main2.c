#include <csapp.h>

int main(void)
{
	int i=0;

	for(i=0; i<3; i++)
		Fork();

	printf("world\n");
	return 0;
}
