#include <sys/socket.h>
#include <sys/types.h>
#include <netdb.h>
#include <arpa/inet.h>
#include <netinet/in.h>
#include <sys/ioctl.h>
#include <stdio.h>
#include <stdlib.h>

void echo(int connfd);

int main(int argc, char *argv[])
{
    int listenfd=0, connfd=0, port=0, clientlen=0;
    struct sockaddr_in client_addr, server_addr;
    struct hostent *client_hp;

    if(argc!=2)	exit(1);

    port = atoi(argv[1]);
    

    listenfd = socket(AF_INET, SOCK_STREAM, 0);
    server_addr.sin_family = AF_INET;
    server_addr.sin_addr.s_addr = htonl(INADDR_ANY);
    server_addr.sin_port = htons((unsigned short)port);
    bind(listenfd, (struct sockaddr *)&server_addr, sizeof(server_addr));
    
    while(1);
    listen(listenfd, 1024);

//    while(1);		//不执行accept
    sleep(10);		//推迟执行accept
    printf("server : start\n");
    while(1)
    {
	int client_len = sizeof(client_addr);
	connfd = accept( listenfd, (struct sockaddr *)&client_addr, &client_len );

	client_hp = gethostbyaddr((const char *)&(client_addr.sin_addr.s_addr), sizeof(client_addr.sin_addr.s_addr), AF_INET);    //获得客户端的dns条目
	
	printf("server : connected to %s (%s)\n", client_hp->h_name, inet_ntoa(client_addr.sin_addr));
	
	echo(connfd);

	printf("server : break with %s (%s)\n", client_hp->h_name, inet_ntoa(client_addr.sin_addr));
	close(connfd);
    }
}

void echo(int connfd)
{
    size_t n;
    char buf[256];

    ssize_t read(int fd, void *buf, size_t count);
    while( (n = read(connfd, buf, 256)) != 0 )
    {
	printf("server : received %d bytes\n", n);
    }

}

