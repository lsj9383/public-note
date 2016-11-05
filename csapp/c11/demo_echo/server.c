#include <csapp.h>

void echo(int connfd);

int main(int argc, char *argv[])
{
    int listenfd=0, connfd=0, port=0, clientlen=0;
    struct sockaddr_in client_addr;
    struct hostent *client_hp;

    if(argc!=2)	exit(1);

    port = atoi(argv[1]);

    listenfd = Open_listenfd(port);
    sleep(10);
    printf("server : start\n");
    while(1)
    {
	int client_len = sizeof(client_addr);
	connfd = Accept( listenfd, (SA *)&client_addr, &client_len );

	client_hp = Gethostbyaddr((const char *)&(client_addr.sin_addr.s_addr), sizeof(client_addr.sin_addr.s_addr), AF_INET);    //获得客户端的dns条目
	
	printf("server : connected to %s (%s)\n", client_hp->h_name, inet_ntoa(client_addr.sin_addr));
	
	echo(connfd);

	printf("server : break with %s (%s)\n", client_hp->h_name, inet_ntoa(client_addr.sin_addr));
	Close(connfd);
    }
}

void echo(int connfd)
{
    size_t n;
    char buf[MAXLINE];
    rio_t rio;

    Rio_readinitb(&rio, connfd);
    while( (n=Rio_readlineb(&rio, buf, MAXLINE)) != 0 )
    {
	printf("server : received %d bytes\n", n);
    }

}

