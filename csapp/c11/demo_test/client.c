#include <errno.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <netdb.h>
#include <arpa/inet.h>
#include <netinet/in.h>
#include <sys/ioctl.h>
#include <stdio.h>
#include <stdlib.h>

int main(int argc, char *argv[])
{
    int clientfd=0, server_port=0;
    char *host;
    int fg=0;
    struct sockaddr_in server_addr;

    if(argc!=2)	exit(1);

    host = "localhost";
    server_port = atoi(argv[1]);


    clientfd = socket( AF_INET, SOCK_STREAM, 0 );

    server_addr.sin_family = AF_INET;
    inet_aton("127.0.0.1", &server_addr.sin_addr);
    server_addr.sin_port = htons(server_port);
    printf("client : connect - %d\n", (fg=connect( clientfd, (struct sockaddr *)&server_addr, sizeof(server_addr))) );
 
    if(fg==-1)
    {
	fprintf(stderr, "Connect Error:%s\n", strerror(errno));
    }
    
    printf("client : fd:%d\n", clientfd);

    printf("client : close fd\n");
    close(clientfd);
    return 0;
}
