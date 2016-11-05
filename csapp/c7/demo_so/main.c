#include<stdio.h>

int main()
{
	int x[2] = {1, 2};
	int y[2] = {3, 4};
	int z1[2];
	int z2[2];

	addvec(x, y, z1, 2);
	mulvec(x, y, z2, 2);
	printf("z = [%d %d]\n", z1[0], z1[1]);
	printf("z = [%d %d]\n", z2[0], z2[1]);
}
