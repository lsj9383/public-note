#include <csapp.h>

/*
int main(int argc, char *argv[])
{
    int clientfd=0, server_port=0;
    char *host, buf[MAXLINE];

    if(argc!=3)	exit(1);

    host = argv[1];
    server_port = atoi(argv[2]);

    clientfd = open_clientfd(host, server_port);

    printf("client fd:%d\n", clientfd);

    while( Fgets(buf, MAXLINE, stdin)!=NULL )	    //Fgets中是包含回车的
    {
	Rio_writen(clientfd, buf, strlen(buf));
	if(buf[0]=='e' && buf[1]=='x' && buf[2]=='i' && buf[3]=='t')
	{    
	    break;
	}
    }

    Close(clientfd);
    printf("client exit...\n");
    return 0;
}
*/

#include <sys/ioctl.h>
int main(int argc, char *argv[])
{
    int clientfd=0, server_port=0;
    char *host, buf[MAXLINE];
    int ul=0, opts=0;
    struct sockaddr_in server_addr;

    if(argc!=3)	exit(1);

    host = argv[1];
    server_port = atoi(argv[2]);

//    clientfd = open_clientfd(host, server_port);

    clientfd = socket( AF_INET, SOCK_STREAM, 0 );
    ioctl(clientfd, FIONBIO, &ul); //设置为阻塞模式
    opts=fcntl(clientfd, F_GETFL);

    if(opts&O_NONBLOCK)
	printf("non block\n");
    else
	printf("block\n");

    server_addr.sin_family = AF_INET;
    inet_aton("127.0.0.1", &server_addr.sin_addr);
    server_addr.sin_port = htons(server_port);
    printf("%d\n", connect( clientfd, (SA *)&server_addr, sizeof(server_addr)));

    printf("client fd:%d\n", clientfd);

    return 0;


    while( Fgets(buf, MAXLINE, stdin)!=NULL )	    //Fgets中是包含回车的
    {
	Rio_writen(clientfd, buf, strlen(buf));
	if(buf[0]=='e' && buf[1]=='x' && buf[2]=='i' && buf[3]=='t')
	{    
	    break;
	}
    }

    Close(clientfd);
    printf("client exit...\n");
    return 0;
}
