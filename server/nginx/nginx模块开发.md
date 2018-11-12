# 一、概述
## 1.特点
    1).更快
        充分利用异步，使得cpu利用率达到最高，对于高并发(数以万计的连接)也能更快的响应。
    2).高扩展性
        模块之间的依赖性极低，并便于设计第三方模块。
    3).高可靠性
        worker进程相对对立，并且会进行自动拉起。
    4).低内存消耗
        nginx的数据结构设计充分考虑的对内存的利用。
    5).承载高并发连接
        利用epoll戒指，可以建立单机上十万的tcp连接。(当然qps和请求业务相关，但是至少可以建立稳定的上十万的连接)
    6).热部署
        master和worker的工作进程分离涉及，使得nginx可以进行在线升级。
## 2.准备工作
    1).Linux
        nginx虽然也可以在windows，mac上使用，但是nginx通常是为了高性能服务器使用的，高性能服务器通常搭建与linux上。
        linux内核版本需要在2.6以上，因为只有这个以上的版本才能支持epoll。
    2).必备依赖
        yum -y install  gcc                     # 用来编译nginx代码
                        gcc-c++                 # 用来编译用C++写的Nginx第三方模块
                        pcre pcre-devel         # nginx采用的正则表达式工具
                        zlib zlib-devel         # 用于对http的body进行gzip压缩, 安装后才能在nginx.conf配置gzip on
                        openssl openssl-devel   # 添加对https的支持
    3).磁盘目录
        * Nginx源代码存放目录
        * Nginx编译阶段所产生的中间文件存放目录
        * 部署目录
        * 日志文件存放目录
    4).内核参数
## 3.编译安装Nginx
## 4.命令行控制
## 5.Nginx进程间关系
    Nginx通过master-workers的进程模型进行
    1).Master
        master负责管理，诸如启动服务，停止服务，热更新等。也对worker进行监控，当worker在dump时会进行自动拉起。
    2).Worker
        一个Nginx可以配置多个端口，所有的Worker都会在这些端口上进行监听，客户端请求到达端口后，Workers中有会有一个拿到连接，并负责对该请求进行处理和响应。
        当然多个Worker监听同一个端口，存在竞争问题，因此需要加锁。(在新版本中，通过reuseport配置，可以避免加锁，提升性能)
    Nginx进程模型的优势:
        * 减少进程间的切换。Worker进程个数通常为CPU个数，并且不变，每一个Worker都可以承载多个连接。
        * 避免进程和线程过多，导致内存消耗严重。
        * 避免进程间的同步，主要是指引入reuseport。
# 二、配置
# 三、HTTP模块
## 1.HTTP模块调用流程
## 2.基本数据结构
    1).整型
        typedef intptr_t ngx_int_t;
        typedef uintptr_t ngx_uint_t;
    2).字符串
        ngx_str_t
    3).链表
        ngx_list_t
    4).kv对
        ngx_table_elt_t
    5).缓冲区
        ngx_buf_t
    6).缓冲区链
        ngx_chain_t
## 3.第三方模块置入Nginx
## 4.HTTP模块数据结构
### 1).*第三方模块数据结构*
第三方模块数据结构的含义:
```cpp
typedef struct ngx_module_s ngx_module_t;
struct ngx_module_s {
# define NGX_MODULE_V1  0, 0, 0, 0, 0, 0, 1

/********************************************************************************
 ctx_index表示当前模块在【同类模块】中的序号。同类模块指的是type字段相同的模块。
 index表示当前模块在所有模块(ngx_modules数组)中的序号。
********************************************************************************/
ngx_uint_t  ctx_index;
ngx_uint_t  index;

// 保留变量，未使用
ngx_uint_t  spare0;
ngx_uint_t  spare1;
ngx_uint_t  spare2;
ngx_uint_t  spare3;

// 模块的版本，便于将来扩展
ngx_uint_t  version;

/********************************************************************************
ctx用于指定一类模块的上下文结构，这个非常重要，上下文结构中描述了。
不同的模块类型，ctx只想的上下文类型并不一样，如http模块，ctx指向ngx_http_module_t结构体
********************************************************************************/
void*   ctx;

/********************************************************************************
一个模块是允许有多个配置项的，并且每个配置项都有设置conf数据结构的方法。
commands指明了该模块所包含的配置项。
********************************************************************************/
ngx_command_t*  commands;

// 描述ctx所对应的类型:NGX_HTTP_MODULE, NGX_CORE_MODULE, NGX_CONF_MODULE, NGX_EVENT_MODULE, NGX_MAIL_MODULES
ngx_uint_t  type;

ngx_int_t   (*init_master)(ngx_conf_t *log);
ngx_int_t   (*init_module)(ngx_cycle_t *cycle);
ngx_int_t   (*init_process)ngx_cycle_t *cycle);
ngx_int_t   (*init_thread)(ngx_cycle_t *cycle);
ngx_int_t   (*exit_thread)(ngx_cycle_t *cycle);
ngx_int_t   (*exit_process)(ngx_cycle_t *cycle);
ngx_int_t   (*exit_master)(ngx_cycle_t *cycle);
};
```
在了解第三方模块的各个字段后，就可以定义第三方模块了: 
```cpp
ngx_module_t <module-name>;
```
# 四、模块配置、日志和请求上下文
# 五、访问第三方服务
# 六、HTTP过滤模块
# 七、Nginx高级数据结构
# 八、Nginx基础架构
# 九、事件模块
# 十、HTTP框架初始化
# 十一、HTTP框架执行流程
# 十二、upstream机制的涉及与实现
# 十三、邮件代理模块
# 十四、进程间通信机制
# 十五、变量
# 十六、SLAB共享内存