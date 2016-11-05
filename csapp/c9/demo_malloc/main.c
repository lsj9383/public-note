
#include <stdio.h>
#include <stdlib.h>

#define sizeMalloc(p) (*(((unsigned int *)p)-1) & ~(0x01|0x02))


int main(void)
{
	int a=1;
	char *b = (char *)&a;
	
	char *this=NULL, *next=NULL;

	this = (char *)malloc(sizeof(char)*1);

	printf("%d\n", sizeMalloc(this));
	printf("%d\n", *((unsigned int *)this - 1));	
	printf("%d\n", (char)*(this-3));
	
	printf( "%d\n", *b );
	*(b+1) = 2;
	printf( "%d\n", a );
	free(this);this=NULL;



	return 0;

}
