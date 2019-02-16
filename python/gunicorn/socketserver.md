## socketserver.py
构造一个TCP服务器类`class TCPServer(socket.socket)`
```py
import socket

class TCPServer(socket.socket):

def __init__(self, address, **opts):
    self.address = address
    self.backlog = opts.get('backlog', 1024)
    self.timeout = opts.get('timeout', 300)
    self.reuseaddr = opts.get('reuseaddr', True)
    self.nodelay = opts.get('nodelay', True)
    self.recbuf = opts.get('recbuf', 8192)

    # 初始化socket, socket.AF_INET指定使用IPv4协议, socket.SOCK_STREAM代表使用流的TCP.
    socket.socket.__init__(self, socket.AF_INET, socket.SOCK_STREAM)
    '''
        setsockopt(level, optname, value)
    '''
    # 当socket关闭后，本地端用于该socket的端口号立刻就可以被重用。通常来说，只有经过系统定义一段时间后，才能被重用.
    if self.reuseaddr:
        self.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)

    # 开启Nagle算法
    if self.nodelay:
        self.setsockopt(socket.IPPROTO_TCP, socket.TCP_NODELAY, 1)

    # 套接字接收缓冲区大小
    if self.recbuf:
        self.setsockopt(socket.SOL_SOCKET, socket.SO_RCVBUF, self.recbuf)

    self.settimeout(self.timeout)
    self.bind(address)
    self.listen()

    def listen(self):
        # 进行监听, 三次握手完成，但是没有被accept取走的连接将会被放入队列，队列长度为backlog，超过了backlog的连接将会被拒绝。
        super(TCPServer, self).listen(self.backlog)

    def accept(self):
        return super(TCPServer, self).accept()

    def accept_nonblock(self):
        # 获取非阻塞socket
        self.setblocking(0)
        return self.accept()
```