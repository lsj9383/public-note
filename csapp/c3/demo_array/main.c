#include <stdio.h>

#define uchar unsigned char

void fun(uchar (*c)[2])
{
	printf("c[1][0]=%d, sizeof(c)=%d\n", c[1][0], sizeof(c));
}

int main(void)
{
	uchar a[2][2]={{1, 2},{3, 4}};

	unsigned int b = *(a+1);
	unsigned int c = *(a[0]+2);
	unsigned int d = **(a+1);
	printf( "*(a+1)=\t%o\na+1=\t%o\na[0]+2=\t%o\n", *(a+1), a+1, a[0]+2 );
	printf( "*(a+1)=%d, *(a[0]+1)=%d, **(a+1)=%d\n", b, c, d);
	printf( "*(*a)=%d\n", **a );
	return 0;
}
