#include <csapp.h>

void doit(int fd);									    //读取客户端请求，分析请求，返回客户端数据
void clienterror(int fd, char *cause, char *errnum, char *shortmsg, char *longmsg);	    //打印并发送错误信息
void read_requesthdrs(rio_t *rp);							    //读出请求头
int parse_uri(char *uri, char *filename, char *cgiargs);

void get_filetype(char *filename, char *filetype);
void serve_dynamic(int fd, char *filename, char *cgiargs);
void serve_static(int fd, char *filename, int filesize);

int main(void)
{
    int listenfd=0, connfd=0, port=80;
    struct sockaddr_in client_addr;

    listenfd = Open_listenfd(port);
    while(1)
    {
	int clientlen = sizeof(client_addr);
	connfd = Accept(listenfd, (SA *)&client_addr, &clientlen);
	doit(connfd);	    //读取客户端请求，分析请求，返回客户端数据
	Close(connfd);
    }
}

void doit(int fd)
{
    int is_static;
    char buf[MAXLINE];
    char method[MAXLINE], uri[MAXLINE], version[MAXLINE];	//请求头
    char filename[MAXLINE], cgiargs[MAXLINE];			//从uri得到文件名和cgi参数
    struct stat sbuf;
    rio_t rio;
    
    Rio_readinitb(&rio, fd);
    Rio_readlineb(&rio, buf, MAXLINE);
    sscanf(buf, "%s %s %s", method, uri, version);		//得到请求头的各段数据
    puts(method);
    puts(uri);
    puts(version);
    if( strcasecmp(method, "GET") )	//非GET方法，返回错误
    {
	clienterror(fd, method, "404", "not implemented", "tiny does not implement this method");
	return ;
    }
    //读请求头
    read_requesthdrs(&rio);
    //从uri得到资源文件位置以及cgi参数
    is_static = parse_uri(uri, filename, cgiargs);
    if( stat(filename, &sbuf)<0 )
    {
	clienterror(fd, filename, "404", "Not found", "Tiny couldn't find this file");
	return ;
    }
    //权限检查
    if(!(S_ISREG(sbuf.st_mode) || !(S_IRUSR & sbuf.st_mode)))
    {
	clienterror(fd, filename, "403", "Forbidden", "Tiny couldn't read or run the file");
	return ;
    }
    //返回资源内容
    if(is_static)
    {
	serve_static(fd, filename, sbuf.st_size);	//静态文件的服务
    }
    else
    {
	serve_dynamic(fd, filename, cgiargs);
    }
}

void get_filetype(char *filename, char *filetype)
{
    if(strstr(filename, ".html"))
    {
	strcpy(filetype, "text/html");
    }
    else if(strstr(filename, ".gif"))
    {
	strcpy(filetype, "image/gif");
    }
    else if(strstr(filename, ".jpg"))
    {
	strcpy(filetype, "image/jpeg");
    }
    else
    {
	strcpy(filetype, "text/plain");
    }
}

void serve_static(int fd, char *filename, int filesize)
{
    int srcfd;
    char *srcp, filetype[MAXLINE], buf[MAXBUF];

    get_filetype(filename, filetype);
    sprintf(buf, "HTTP/1.0 200 OK \r\n");
    sprintf(buf, "%sServer: Tiny Web Server\r\n", buf);
    sprintf(buf, "%sContent-length: %d\r\n", buf, filesize);
    sprintf(buf, "%sContent-type: %s\r\n\r\n", buf, filetype);
    Rio_writen(fd, buf, strlen(buf));				    //写服务器回应头以及报头
    
    srcfd = Open(filename, O_RDONLY, 0);
    srcp = Mmap(0, filesize, PROT_READ, MAP_PRIVATE, srcfd, 0);	    //将文件映射到存储器，映射成功后，就可以关闭文件描述符了.
    Close(srcfd);
    Rio_writen(fd, srcp, filesize);
    Munmap(srcp, filesize);					    //取消映射
}

void serve_dynamic(int fd, char *filename, char *cgiargs)
{
    char buf[MAXLINE], *emptylist[] = {NULL};

    sprintf(buf, "HTTP/1.0 200 OK\r\n");
    Rio_writen(fd, buf, strlen(buf));
    sprintf(buf, "Server: Tiny web Server\r\n");
    Rio_writen(fd, buf, strlen(buf));

    if(Fork()==0)
    {
	setenv("QUERY_STRING", cgiargs, 1);
	Dup2(fd, STDOUT_FILENO);
	Execve(filename, emptylist, environ);
    }
    Wait(NULL);	    //父进程等待....
}

int parse_uri(char *uri, char *filename, char *cgiargs)
{
    char *ptr;

    if (!strstr(uri, "cgi-bin"))
    {	/* static content */
	strcpy(cgiargs, "");
	strcpy(filename, ".");
	strcat(filename, uri);
	if(uri[strlen(uri)-1] == '/')
	    strcat(filename, "index.html");
	return 1;
    }
    else
    {	/* Dynamic content */
	ptr = index(uri, '?');
	if(ptr)
	{
	    strcpy(cgiargs, ptr+1);
	    *ptr = '\0';
	}
	else
	{
	    strcpy(cgiargs, "");
	}
	strcpy(filename, ".");
	strcat(filename, uri);
	return 0;
    }
}

void clienterror(int fd, char *cause, char *errnum, char *shortmsg, char *longmsg)
{
    char buf[MAXLINE], body[MAXBUF];

    // 1.构建http响应主体
    sprintf(body, "<html><title>Tiny Error</title>");
    sprintf(body, "%s<body bgcolor=""ffffff"">\r\n", body);
    sprintf(body, "%s<p>%s: %s\r\n", body, longmsg, cause);
    sprintf(body, "%s<hr><em>The Tiny Web Server</em>\r\n", body);

    // 2.发送HTTP回应
    // 1).发送响应头
    sprintf(buf, "HTTP/1.0 %s %s\r\n", errnum, shortmsg);
    Rio_writen(fd, buf, strlen(buf));
    // 2).发送响应报文
    sprintf(buf, "Content-type: text/html\r\n");
    Rio_writen(fd, buf, strlen(buf));
    sprintf(buf, "Content-length: %d\r\n\r\n", (int)strlen(body));	//包含空行终止报头
    Rio_writen(fd, buf, strlen(buf));
    // 3).发送响应主体
    Rio_writen(fd, body,strlen(body));	//
}


void read_requesthdrs(rio_t *rp)
{
    char buf[MAXLINE];

    do
    {
	Rio_readlineb(rp, buf, MAXLINE);
	printf("%s", buf);
    }while(strcmp(buf, "\r\n"));		//读出空文本行为止
}
