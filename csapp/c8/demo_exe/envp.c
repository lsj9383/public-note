#include <csapp.h>

int main(void)
{
	
	while( *environ != NULL)
	{
		puts(*environ);
		environ++;
	}

	return 0;
}
