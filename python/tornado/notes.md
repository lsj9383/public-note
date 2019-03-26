一、介绍
Tornado 大体上可以被分为4个主要的部分:
* web框架 (包括创建web应用的 RequestHandler 类，还有很多其他支持的类).
* HTTP的客户端和服务端实现 (HTTPServer and AsyncHTTPClient).
* 异步网络库 (IOLoop and IOStream), 为HTTP组件提供构建模块，也可以用来实现其他协议.
* 协程库 (tornado.gen) 允许异步代码写的更直接而不用链式回调的方式.