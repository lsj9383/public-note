# Proxy Protocol

# 简述
这是一种用于tcp透传的协议，在这个里面有3个角色:client, sender, receiver。请求的发送路径有:
```sh
client --tcp--> sender --proxy protocol--> receiver
```
sender是proxy protocol的客户端，receiver是proxy protocol的服务器端，sender首先会将client信息打包为短小的header发送给receiver，然后再发送tcp请求。虽然client到sender是tcp连接，不过包括http在内的一切tcp连接均可采用。sender本职上就是将普通tcp请求转变为了proxy protocol这一变种tcp请求。nginx中receiver可以配置能够接收proxy protocol请求的http服务器。

# 背景
为了满足全球用户的需要，我们通常会进行分区域多接入点部署。为了接入ssl，需要在每个接入点都配置https证书，过多的节点配置一样功能的证书过于浪费。
因此可以将证书集中配置在中端服务器，直接面向用户的前端服务器就不进行https证书配置了。

## 配置举例
```sh
worker_processes  1;

events {
    worker_connections  1024;
}

http {
    include       mime.types;
    default_type  application/octet-stream;

    sendfile        on;

    keepalive_timeout  65;

    upstream test{
        server 127.0.0.1:5000;
    }

    server {
        listen 80 ;
        server_name server;
        location = /index {
            proxy_pass http://test;
            proxy_set_header Host      $host;
            proxy_set_header remote-addr $remote_addr;  # 发送请求到nginx的ip
            proxy_set_header proxy-protocol-addr $proxy_protocol_addr;  # nginx的ip
        }
    }
}
```