#include <csapp.h>
#include <stdlib.h>
#include <string.h>

unsigned int get_int(char *str)
{
    unsigned int number=0, i=0, ret=0;
    char *first = &str[2], *nump=(char *)&number, *retp=(char *)&ret;
    if(str[0]!='0' || str[1]!='x')  return -1;
    if(strlen(str)!=10)		    return -1;

    for(i=0; i<8; i++)
    {
	char chr = first[i];
	int value=0;

	if(chr<='9' && chr>='0')
	    value = chr - '0';
	else if(chr<='F' && chr>='A')
	    value = chr - 'A' + 10;
	else if(chr<='f' && chr>='a')
	    value = chr - 'a' + 10;
	else
	    return -1;
	
	number <<= 4;
	number += value;
    }

    return number;
}

int main(int argc, char *argv[])
{
    if(argc!=2)	exit(1);
    struct in_addr ip;
    ip.s_addr = htonl(get_int(argv[1]));
    if(ip.s_addr == -1)	exit(1);

    printf("%s\n", inet_ntoa(ip));

    return 0;    
}
