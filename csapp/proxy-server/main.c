#include <csapp.h>

void doit(int fd)
{
    char buf[MAXLINE];
    rio_t rio;

    Rio_readinitb(&rio, fd);
    while( Rio_readlineb(&rio, buf, MAXLINE) > 0)
    {
	puts(buf);
    }
}

int main(void)
{
    int listenfd=0, connfd=0, port=1080;

    listenfd = Open_listenfd(port);

    while(1)
    {
	struct sockaddr_in clientaddr;
	int clientlen = sizeof(clientaddr);

	connfd = Accept(listenfd, (SA *)&clientaddr, &clientlen);

	doit(connfd);

	Close(connfd);
    }
}
