#ifndef __OVER_VIEW_H__

//====================================================================
//============================ 操作右侧位 ============================
//====================================================================
#define RIGHT_BIT_SET_ZERO( x )		x&(x-1)		//最右侧位的1设置为0			如：01010010->01010000
#define RIGHT_BIT_ISOLATE( x )		x&(-x)		//保留最右侧位的1，其他设为0	如：11010000->00010000
#define RIGHT_BIT_ZERO_MASK( x )	(~x)&(x-1)	//构造识别0后缀的掩码			如：11010000->00001111
#define RIGHT_BIT_ONEZERO_MASK(x)	x^(x-1)		//构造识别最右侧的1和0后缀掩码	如：11010000->00011111
#define RIGHT_BIT_ONE_BROAD(x)		x|(x-1)		//将最0后缀全部转换为1			如：11010000->11011111

int DOWN_K2POWER(int x, int N);									//下舍入到2的N次幂的倍数
int UP_K2POWER(int x, int N);									//上舍入到2的N次幂的倍数
int flp2(int x);												//下舍入到2的幂
int flp2_cyc_easyOne(int x);									//下舍入到2的幂，1较少时突显又是
int flp2_cyc_easyNlz(int x);									//下舍入到2的幂，前导零少时突显又是
int clp2(int x);												//上舍入到2的幂

int pop(unsigned int x);										//1位计数
int pop_easy(unsigned int x);									//1位计数的优化
int nlz(int x);													//前导零计算
int nlz_2dic(unsigned x);										//基于二分法的前导零计算

unsigned int zbytel_nlz(unsigned int x);						//首个0字节的位置
unsigned int ffstr1_nlz(unsigned int x, unsigned int n);		//定长1位串位置

unsigned int BitRev_32BIT(unsigned int x);						//32位反转
unsigned int BitRev_Broad(unsigned int x, int k);				//广义反转
unsigned int PerfectShuffle_OUT(unsigned int x);				//外全混洗
unsigned int PerfectShuffle_IN(unsigned int x);					//内全混洗

#endif