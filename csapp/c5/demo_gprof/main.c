#include <stdio.h>

void swap1( int *a, int *b  )
{
	int tmp=0;

	tmp = *a;
	*a = *b;
	*b = tmp;
}

void swap2( int *a, int *b )
{
	*a = *a + *b;
	*b = *a - *b;
	*a = *a - *b;
}

void show( int a, int b)
{
	printf( "a=%d, b=%d\n", a, b );
}

int main(void)
{
	int a=3, b=4;
	int i=0;

	for(i=0; i<1e7; i++)
		swap1(&a, &b);
	for(i=0; i<1e8; i++)
		swap2(&a, &b);
	for(i=0; i<1e7; i++)
		swap1(&a, &b);
	show(a, b);
	return 0;
}
