#include <csapp.h>

int main(int argc, char *argv[])
{
    struct in_addr ip;

    if(argc!=2)	exit(1);

    inet_aton( argv[1], &ip );
    
    printf("0x%x\n", ntohl(ip.s_addr));

    return 0;
}
