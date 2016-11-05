#include <stdio.h>
#include "mm.h"

int main(void)
{
    int *a=NULL, *b=NULL, *c=NULL, *d=NULL;

    printf("init : %d\n", mm_init());

    mm_display();
    a = (int *)mm_malloc(sizeof(int)*1);
    b = (int *)mm_malloc(sizeof(int)*1);
    c = (int *)mm_malloc(sizeof(int)*1);
    mm_display();
    d = (int *)mm_malloc(sizeof(int)*4096);
    mm_display();
    mm_free(a);
    mm_free(c);
    mm_display();
    mm_free(b);
    mm_display();
    mm_free(d);
    mm_display();
    mm_free(d);
    return 0;
}
