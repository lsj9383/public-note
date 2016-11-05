#ifndef __MEM_LIB_H
#define __MEM_LIB_H

void  mem_init(void);
void *mem_sbrk(int incr);
int   mem_isInHeap(void *bp);
#endif
