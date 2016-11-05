#ifndef __MM_H
#define __MM_H

int mm_init(void);
int mm_free(void *bp);
void *mm_malloc(size_t size);
void mm_display(void);
#endif
