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
Worker进程通过epoll，反复检查网络事件，当检测到了来自client的tcp请求时，会建立tcp连接。在成功建立连接后，根据nginx.conf文件中的配置，会将连接交由http框架处理。
http框架会在接收了完整的http头部后（注，不会等待接收到完整的body，因为body可能会很大），将请求分发到http模块中进行处理。
在所有的匹配的http模块处理完成后，会调用所有的http过滤模块。
## 2.基本数据结构
### 1).整型
```cpp
typedef intptr_t ngx_int_t;
typedef uintptr_t ngx_uint_t;
```
### 2).字符串---ngx_str_t
```cpp
typdef struct {
    size_t len;
    u_char* data;
} ngx_str_t;
```
`data`指向字符串起始地址，len表示字符串有效长度。nginx的字符串并不等同于普通的字符串，因为nginx的字符串不一定有结尾符`\0`，而是通过`len`来指示字符串的结尾。

nginx中，对于字符串的比较可以通过`ngx_strcmp`函数实现:
```cpp
#define ngx_strncmp(s1, s2, n) strncmp((const char*)s1, (const char*)s2, n)
```
### 3).链表---ngx_list_t
HTTP的头部就是用`ngx_list_t`来进行存储的。
```cpp
// ngx_list_part_t只是链表中的一个节点, 该节点通过void* elts,可以存放任意类型的数据
typdef struct_ngx_list_part_s ngx_list_part_t;
struct ngx_list_parts {
    // 存放数据的指针
    void* elts;

    // 指针所指向的数据结构所用的元素个数, 不得大于nalloc
    ngx_uint_t nelts;

    // 指向下一个节点
    ngx_list_part_t* next;
};

// 链表结构
typedef struct {
    // 链表的首个节点
    ngx_list_part_t part;

    // 链表的最后一个数组元素
    ngx_list_part_t* last;

    // ngx_list_parts.elts可以由数个数据结构构成，size应为数据结构的大小
    size_t size;
    // ngx_list_parts.elts的容量
    ngx_uint_t nalloc;
    // size*nalloc是一个节点所可以存放的数据量大小。

    // 链表中管理内存分配的内存池对象
    ngx_pool_t* pool;
} ngx_list_t;

// ===================================== 接口 =====================================
// 创建链表结构
// pool:
// n: 节点中的数组可以容纳的最多的元素个数
// size: 节点的数组中，每个元素的大小
// return: 新创建的链表地址，若是失败返回NULL
ngx_list_t* ngx_list_create(ngx_pool_t* pool, ngx_uint_t n, size_t size);

// 初始化链表结构
// pool:
// n:
// size:
// return
ngx_list_init(ngx_list_t* list, ngx_pool_t* pool, ngx_uint_t n, size_t size);

void* ngx_list_push(ngx_list_t* list);
```
### 4).kv对---ngx_table_elt_t
```cpp
typedef struct {
    // key对应的hash
    ngx_uint_t hash;
    ngx_str_t key;
    ngx_str_t value;
    // 全小写key
    u_char* lowcase_key;
} ngx_table_elt_t;
```
这是一个key-value结构，保存了key和对应的value，以及key的hash和全小写key。
### 5).缓冲区
ngx_buf_t
### 6).缓冲区链
ngx_chain_t
## 3.模块编译
开发Nginx的某个第三方模块，需要将源文件统一放在一个目录下。configure时通过--add-module=MODULE_PATH的方式，给出第三方模块目录的路径。
### 1).config
config文件是一个shell脚本，放在第三方模块的目录下，主要给Nginx提供了第三方模块的名称和路径，便于Nginx在Configure时生成对应的makefile文件。
* ngx_addon_name
* HTTP_MODULES
* NGX_ADDON_SRCS
```sh
ngx_addon_name=ngx_http_mytest_module
HTTP_MODULES="$HTTP_MODULES ngx_http_mytest_module"
NGX_ADDON_SRCS="$NGX_ADDON_SRCS /home/dever/pkg/nginx-1.14.0/myself/ngx_http_mytest_module.c"
```
### 2).利用confiure添加模块
写好config后，在configure中添加--add-module=参数，就能自动将第三方模块的makefile生成，并编译进nginx。

## 4.HTTP模块结构
定义一个ngx第三方HT TP模块，需要包括描述如何处理配置文件，如何处理http请求等，这都由下述3种数据结构进行描述。
### 1).*ngx_module_t*
这个数据结构用于定义一个模块，这不仅仅局限于一个http模块:
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
一个模块最重要的是设置其中的`ctx`和`commands`两个字段，前者描述了解析配置文件时，模块进行何种处理，以及用何种数据结构保存解析时的数据，后者描述了解析配置文件到指定的配置项时，如何对配置项进行保存。这两者都能设置对http请求的处理函数。
### 2).*ngx_http_module_t*
这个数据结构主要描述了配置文件解析时的处理方式，以及构造自定义的数据结构用于配置项的保存。
```cpp
typedef struct {
    // 解析配置文件前调用
    ngx_int_t (*preconfiguration)(ngx_conf* cf);
    // 解析配置文件后调用，通常可以在这里设定用何种回调函数对http请求进行处理
    ngx_int_t (*postconfiguration)(ngx_conf* cf);

    // 当需要创建数据结构用于main级别的配置项时
    ngx_int_t (*create_main_conf)(ngx_conf* cf);
    ngx_int_t (*init_main_conf)(ngx_conf* cf);

    // 当需要创建数据结构用于srv级别的配置项时
    ngx_int_t (*create_srv_conf)(ngx_conf* cf);
    ngx_int_t (*merge_srv_conf)(ngx_conf* cf);

    // 当需要创建数据结构用于loc级别的配置项时
    ngx_int_t (*create_loc_conf)(ngx_conf* cf);
    ngx_int_t (*merge_loc_conf)(ngx_conf* cf);
};
```
### 3).*ngx_command_t*
```cpp
typedef struct ngx_command_s ngx_command_t;
struct ngx_command_s {
    // 配置项的名称
    ngx_str_t name;

    // 决定配置项可以在哪些块中出现，并包含了配置项参数个数的信息
    ngx_uint_t type;

    // 当配置文件中出现了指定的配置项后，将会调用该函数。这里会传入conf，这是由`ngx_http_module_t`中创建的自定义配置文件，根据conf字段，传入对应级别的配置文件。
    char* (*set)(ngx_conf_t* cf, ngx_command_t* cmd, void* conf);

    // 指定采用ngx_http_module_t中创建的何种文件对该配置项进行保存
    ngx_uint_t conf;

    // 通常用于自带的set函数，自带的set函数可以自行将参数设定到自定义的配置文件中
    ngx_uint_t offset;

    // 配置项读取后的处理方法，必须是ngx_conf_post_t的结构指针
    void* post;
};
```
## 5.处理用户请求
## 6.处理用户响应
## 7、自定义HTTP模块
### 1).模块定义
```cpp
#include <ngx_config.h>
#include <ngx_core.h>
#include <ngx_http.h>

// 配置项定义
static ngx_command_t ngx_http_mytest_commands[] = {
    {
        ngx_string("my_test"),
        NGX_HTTP_MAIN_CONF|NGX_HTTP_SRV_CONF|NGX_HTTP_LOC_CONF|NGX_HTTP_LMT_CONF|NGX_CONF_NOARGS,
        NULL,
        0,
        NULL
    },
    ngx_null_command
};

// 配置文件解析器定义

```
### 2).处理函数定义
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