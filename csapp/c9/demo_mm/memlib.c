
#include <csapp.h>

static char *mem_heap	    = NULL; /* 堆空间起始地址 */
static char *mem_brk	    = NULL; /* 从堆中申请的可用空间的最后位置 */
static char *mem_max_addr   = NULL; /* 堆的位置必须小于mem_max_addr */

#define MAX_HEAP (1<<30)

void mem_init(void)
{
    mem_heap	= (char *)malloc(MAX_HEAP);
    mem_brk	= (char *)mem_heap;		    //刚刚初始的空间，还没有进行任何的使用申请
    mem_max_addr= (char *)(mem_heap + MAX_HEAP);
}


/*************************************************
 *
 *function  : mem_sbrk
 *input	    : incr, brk增长的数量
 *output    : 原brk的位置
 *descri    : 从堆中申请incr字节大小的使用空间地址
 *
 *************************************************/
void *mem_sbrk(int incr)
{
    char *old_brk = mem_brk;

    if( (incr<0) || ((mem_brk+incr) > mem_max_addr) )
    {
	errno = ENOMEM;
	fprintf( stderr, "ERROR: mem_Sbrk failed. Ran out of memory...\n" );
	return (void *)-1;
    }

    mem_brk += incr;
    
    return (void *)old_brk;
}

int mem_isInHeap(void *p)
{
    if(( p>(void *)mem_brk ) || (p<(void *)mem_heap))
	return 0;			    //地址不在可用的堆空间里
    else
	return 1;
}
