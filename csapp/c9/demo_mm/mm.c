#include <csapp.h>
#include "memlib.h"

#define WSIZE 4			/*   字的字节数 */
#define DSIZE 8			/* 双字的字节数 */
#define CHUNKSIZE   (1<<12)	/* 初始空闲块的大小 以及 扩展堆时的默认大小 */

#define MAX(x, y)	    ( (x)>(y)?(x):(y) ) 
#define PACK(size, alloc)   ( (size) | (alloc) )    //将大小和分配情况打包为1个4字节的uint对象，
						    //这里块大小为16的倍数，所以size肯定低3bit都是0，所以可以直接或
//向指定的地址读或写一个unsigned int(4Byte)数据
#define GET(p)		(*(unsigned int *)(p))
#define PUT(p, val)	(*(unsigned int *)(p)=(val))

//读取块大小以及块的分配情况
#define GET_SIZE(p)	(GET(p)&~(0x7))	    /* 去掉uint数据的后3位,块的大小情况 */
#define GET_ALLOC(p)	(GET(p)&(0x1))	    /* uint的最后1bit数,块的分配情况 */

//根据块地址得到他的header地址以及footer地址
#define HDRP(bp)	((char *)bp - WSIZE)
#define FTRP(bp)	((char *)bp + GET_SIZE(HDRP(bp)) - DSIZE)

//根据给出的块地址，计算下一个或者上个块的地址
#define NEXT_BLKP(bp)	( (char *)(bp) + GET_SIZE( ((char *)(bp)-WSIZE) ) )
#define PREV_BLKP(bp)	( (char *)(bp) - GET_SIZE( ((char *)(bp)-DSIZE) ) )

void *heap_listp=NULL;
	
static void *coalesce(void *bp);
static void *extend_heap(size_t words);
static void place(void *bp, size_t size);
static void *find_fit(size_t size);

int mm_init(void)
{
    mem_init();
    if( (heap_listp = mem_sbrk(4*WSIZE)) == (void *)-1 )	//在堆空间中申请16个字节的块作为堆的初始化
    {
	return -1;
    }

    PUT(heap_listp, 0);				//双字边界对齐的 不使用的 填充块
    PUT(heap_listp+(1*WSIZE), PACK(DSIZE, 1));	//序言块header
    PUT(heap_listp+(2*WSIZE), PACK(DSIZE, 1));	//序言块footer
    PUT(heap_listp+(3*WSIZE), PACK(0, 1));	//特殊结束块

    heap_listp+=(2*WSIZE);
    //扩展堆
    if( extend_heap(CHUNKSIZE/WSIZE)==NULL )
    {
	return -1;
    }

    return 0;
}

void mm_display(void)
{
    char *ptr = (char *)heap_listp;
    printf("===================block size and alloc===================\n");
    for(ptr+=GET_SIZE(ptr); GET_SIZE(HDRP(ptr))!=0 ;ptr=NEXT_BLKP(ptr))
    {
	printf("size:%d, alloc:%d\n", GET_SIZE(HDRP(ptr)), GET_ALLOC(HDRP(ptr)));
    }
    printf("\n");
}

void *mm_malloc(size_t size)
{
    size_t asize=0;	    //实际要分配的块大小
    size_t extendsize=0;    //由于已有空间中无可分配的块，因此要得到额外的空间
    char *bp=NULL;

    if(size==0)	return NULL;
    if(size <= DSIZE)	asize = 2*DSIZE;
    else		asize = DSIZE * (( size + DSIZE + DSIZE-1 )/DSIZE);

    //根据asize寻找合适的块
    if( (bp=find_fit(asize))!=NULL )
    {	//找到合适的块,进行块的放置
	place(bp, asize);
	return bp;
    }
    
    //设置堆扩展的大小
    extendsize = MAX(asize, CHUNKSIZE);
    if( (bp=extend_heap(extendsize/WSIZE))==NULL )
    {	//扩展失败
	return NULL;
    }
    else
    {	//扩展成功
	place(bp, asize);
	return bp;
    }
}

void mm_free(void *bp)
{
    if( GET_ALLOC(HDRP(bp))==0 || (!mem_isInHeap(bp)) )
    {	//未分配空间，没得释放一说
	printf("no free!\n");
    }
    else
    {
	PUT(HDRP(bp), PACK(GET_SIZE(HDRP(bp)), 0));
	PUT(FTRP(bp), PACK(GET_SIZE(HDRP(bp)), 0));
        coalesce(bp);
    }
}

static void *coalesce(void *bp)
{
    size_t prev_alloc = GET_ALLOC( HDRP(PREV_BLKP(bp)) );
    size_t next_alloc = GET_ALLOC( HDRP(NEXT_BLKP(bp)) );
    size_t size = GET_SIZE(HDRP(bp));			//当前块大小

    if(prev_alloc && next_alloc)
    {
	return NULL;
    }
    else if(prev_alloc && !next_alloc)
    {
	size += GET_SIZE( HDRP(NEXT_BLKP(bp)) );
	PUT(HDRP(bp), PACK(size, 0));
	PUT(FTRP(bp), PACK(size, 0));
    }
    else if(!prev_alloc && next_alloc)
    {
	size += GET_SIZE( HDRP(PREV_BLKP(bp) ));
	PUT(HDRP(PREV_BLKP(bp)), PACK(size, 0));
	PUT(FTRP(PREV_BLKP(bp)), PACK(size, 0));
	bp = PREV_BLKP(bp);
    }
    else
    {
	size += GET_SIZE( HDRP(PREV_BLKP(bp) )) +  GET_SIZE( HDRP(NEXT_BLKP(bp)) );
	PUT(HDRP(PREV_BLKP(bp)), PACK(size, 0));
	PUT(FTRP(NEXT_BLKP(bp)), PACK(size, 0));
	bp = PREV_BLKP(bp);
    }

    return bp;
}

static void *extend_heap(size_t words)
{
    char *bp=NULL;
    size_t size=0;

    size = (words%2) ? (words+1) * WSIZE : words * WSIZE;	//使申请的空间为双字对齐
    if( (long)(bp=mem_sbrk(size))==-1 )
    {	// 没有申请到
	return NULL;
    }

    //申请到了空间，将其初始化为新添加的块，并调整结束块
    PUT(HDRP(bp), PACK(size, 1));				//覆盖以前空间的结束块为新空闲快的header
    PUT(FTRP(bp), PACK(size, 1));
    PUT(NEXT_BLKP(bp), PACK(0, 1));				//新的结尾块，之所以这个才是结尾块是因为bp对应的header并不是在新生成的空间里，那是在以前老的空间里面的

    //合并块，主要是将当前块 和 既相邻又空闲的块进行合并
    return coalesce(bp);
}

/*
 *first-fit策略
 * */
static void *find_fit(size_t size)
{
    char *ptr = (char *)heap_listp;

    for(ptr+=GET_SIZE(ptr); GET_SIZE(HDRP(ptr))!=0 ;ptr=NEXT_BLKP(ptr))
    {
	if( GET_SIZE(HDRP(ptr))>=size && (!GET_ALLOC(HDRP(ptr))) )
	{
	    return ptr;
        }
    }
}

static void place(void *bp, size_t size)
{
    int old_size = GET_SIZE(HDRP(bp));

    PUT(HDRP(bp), PACK(size, 1));			    //设置当前块的header
    PUT(FTRP(bp), PACK(size, 1));			    //设置当前块的footer
    PUT(HDRP(NEXT_BLKP(bp)), PACK(old_size-size, 0));	    //设置下个块的header
    PUT(HDRP(NEXT_BLKP(bp)), PACK(old_size-size, 0));	    //设置下个块的footer
}
