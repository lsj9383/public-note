#include<stdio.h>
#include<stdlib.h>
#include<dlfcn.h>

int x[2] = {1, 2};
int y[2] = {3, 4};
int z1[2];
int z2[2];

int main(void)
{
	void *handle;
	void (*addvec)(int *, int *, int *, int);
	void (*mulvec)(int *, int *, int *, int);
	char *error;

	handle = dlopen("./libvector.so", RTLD_LAZY);
	if(!handle){
		fprintf(stderr, "%s\n", dlerror());
		exit(1);
	}

	addvec = dlsym(handle, "addvec");
	mulvec = dlsym(handle, "mulvec");
	if( (error=dlerror()) != NULL ){
		fprintf(stderr, "%s\n", error);
		exit(1);
	}

	addvec(x, y, z1, 2);
	mulvec(x, y, z2, 2);
	printf("z=[%d, %d]\n", z1[0], z1[1]);
	printf("z=[%d, %d]\n", z2[0], z2[1]);
	if(dlclose(handle) <0 ){
		fprintf(stderr, "%s\n", error);
		exit(1);
	}
	return 0;
}
