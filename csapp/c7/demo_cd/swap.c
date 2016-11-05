
extern int buf[];

int *bufp0 = &buf[0];
int *bufp1;
int x=0;

void swap()
{
	int temp;
	swap();
	bufp1 = &buf[1];
	temp = *bufp0;
	*bufp0 = *bufp1;
	*bufp1 = temp;
}
