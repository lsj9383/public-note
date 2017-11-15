import gevent

hosts = ['www.baidu.com', 'www.qq.com', 'www.tencent.com']
jobs = [gevent.spawn(gevent.socket.gethostbyname, host) for host in hosts]
print(jobs);