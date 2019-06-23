<!-- TOC -->

- [一、安装](#一安装)
- [二、入门](#二入门)
    - [1.文档](#1文档)
    - [2.索引(index)](#2索引index)
    - [3.检索](#3检索)
- [三、分布式集群](#三分布式集群)
    - [1.集群](#1集群)
        - [1).*节点*](#1节点)
        - [2).*分片*](#2分片)
        - [3).*客户端请求*](#3客户端请求)
    - [3.故障转移](#3故障转移)
    - [4.横向扩展](#4横向扩展)
- [四、数据存储](#四数据存储)
    - [1.文档](#1文档-1)
    - [2.文档存储](#2文档存储)
    - [3.检索文档](#3检索文档)
    - [4.更新文档](#4更新文档)
- [五、搜索](#五搜索)
- [六、映射与分析](#六映射与分析)
    - [1.数据类型](#1数据类型)
    - [2.分析器](#2分析器)
    - [3.映射](#3映射)
        - [1).es的索引基本类型](#1es的索引基本类型)
        - [2).*JSON文档的对应的es数据类型*](#2json文档的对应的es数据类型)
        - [3).*自定义映射*](#3自定义映射)
- [七、结构化查询](#七结构化查询)
    - [1.Query DSL](#1query-dsl)
    - [2.常用的查询子句](#2常用的查询子句)
        - [1).*term*](#1term)
        - [2).*terms*](#2terms)
        - [3).*range*](#3range)
        - [4).*exists和missing*](#4exists和missing)
        - [5).*match*](#5match)
        - [6).*match_phrase*](#6match_phrase)
        - [7).*multi_match*](#7multi_match)
        - [8).*match_all*](#8match_all)
        - [9).*bool复合子句*](#9bool复合子句)
    - [3.验证查询](#3验证查询)
- [八、排序](#八排序)
    - [1.排序](#1排序)
    - [2.相关性](#2相关性)
- [九、索引管理](#九索引管理)
- [十、深入结构化搜索](#十深入结构化搜索)
- [附录、常用命令](#附录常用命令)

<!-- /TOC -->

# 一、安装
ElasticSearch的安装依赖Java，因此需要先安装Java并配置JAVA_HOME的环境变量，本文档默认已经安装好了JAVA，并且JAVA和ElasticSearch对应以下版本:
* jdk 8.181
* es 6.4.2

在获取到elasticsearch的安装包后, 进行解压, 直接允许es进程即可启动。
```sh
# 前台启动
$ ./elasticsearch-6.4.2/bin/elasticsearch

# 后台启动(守护进程)
$ ./elasticsearch-6.4.2/bin/elasticsearch -d
```
启动后默认只能监听localhost的9200端口，可以在`./elasticsearch-6.4.2/config/elasticsearch.yml`配置es服务器信息:
```conf
cluster.name: my-application
node.name: node-1
network.host: 0.0.0.0
http.port: 9200
```
es的启动可能会遇到一些错误情况, 以下是常见错误情况及解决:
* max file descriptors [4096] for elasticsearch process is too low, increase to at least [65536]
    * 原因, 操作系统支持的最大文件描述符太少
    * 解决, `vi /etc/security/limits.conf`
        * soft nofile 65536
        * hard nofile 65536
* max number of threads [3818] for user [es] is too low, increase to at least [4096]
    * 原因, 操作系统配置的最大线程个数太少
    * 解决, `vim etc/security/limits.conf`
        * soft nproc 4096
        * hard nproc 4096
* system call filters failed to install; check the logs and fix your configuration or disable system call filters at your own risk
    * 原因, 因为Centos6不支持SecComp，而ES5.2.1默认bootstrap.system_call_filter为true进行检测，所以导致检测失败，失败后直接导致ES不能启动
    * 解决, `vim elasticsearch.yml`
        * bootstrap.memory_lock: false
        * bootstrap.system_call_filter: false
* max virtual memory areas vm.max_map_count [65530] is too low, increase to at least [262144]
    * 原因, 虚拟存储区域太小
    * 解决, `vim /etc/sysctl.conf`
        * vm.max_map_count=262144
        * sysctl -p
* 已杀死
    * 原因, es是由Java启动的，通常是非配给Java的内存不足导致的。
    * 解决, `vim config/jvm.options`
        * -Xms512m, Xms是JVM初始化时的内存大小
        * -Xmx512m, Xmx是JVM最大可用内存, 和Xms分别表示JVM的最小和最大地址。

# 二、入门
## 1.文档
一个文档就是一个对象，有多个字段，并且有对应的数据。es存储文档，并通过value建立反向索引，并通过value来查询文档。在es中，一个文档本质上就是一个JSON对象:
```json
{
    "email":      "john@smith.com",
    "first_name": "John",
    "last_name":  "Smith",
    "info": {
        "bio":         "Eco-warrior and defender of the weak",
        "age":         25,
        "interests": [ "dolphins", "whales" ]
    },
    "join_date": "2014/05/01"
}
```

## 2.索引(index)
es集群中包含多个index(dbs), 每个index包含了多个type(tables), 每个type包含了多个documents(rows)。`索引`一词在es中存在两种含义:
* 动词 indexing: 表示把一个文档存储到es中。以便它可以被检索或者查询。这很像SQL中的INSERT关键字。如果文档已经存在，新的文档将覆盖旧的文档。默认情况下，文档中的所有字段都会被索引，只有这样他们才是可被搜索的。
* 名次 index: 一个索引(index)就像是传统关系数据库中的数据库，它是相关文档存储的地方

可以在一次存储中指定index, type, documents, 但凡有一个不存在, 就会创建。
```sh
# 下面这个数据中 索引名为:megacorp, 类型名为:employee, 文档名为:1
curl -XPUT -H "Content-Type:application/json"  "http://localhost:9200/megacorp/employee/1" -d '
{
    "first_name" : "John",
    "last_name" :  "Smith",
    "age" :        25,
    "about" :      "I love to go rock climbing",
    "interests": [ "sports", "music" ]
}'
```

## 3.检索
最简单的检索是通过index, type, document的名称将文档查询出来，类似mysql中通过主键来进行查询。
```sh
# 检索指定的文档
curl "http://localhost:9200/megacorp/employee/1"
curl "http://localhost:9200/<index>/<type>/<doc>"

# 检索type下的所有文档, 默认返回十个
curl "http://localhost:9200/megacorp/employee/_search"
curl "http://localhost:9200/<index>/<type>/_search"

# 检索index下的所有文档, 默认返回十个
curl "http://localhost:9200/megacorp/_search"
curl "http://localhost:9200/<index>/_search"
```
其实通过这个方式, 也可以实现简单的全文检索:
```sh
# 对index下面的数据进行全文检索
curl "http://localhost:9200/megacorp/employee/_search?q=last_name:Smith"
curl "http://localhost:9200/<index>/type/_search?q=<field>:<value>"
```
最常用和强大的是通过DSL来进行查询:
```sh
# 全文检索
curl -XGET -H "Content-Type:application/json" "http://localhost:9200/megacorp/employee/_search" -d '
{
    "query" : {
        "match" : {
            "about" : "rock climbing"
        }
    }
}'

# 短语检索
curl -XGET -H "Content-Type:application/json" "http://localhost:9200/megacorp/employee/_search" -d '
{
    "query" : {
        "match_phrase" : {
            "about" : "rock climbing"
        }
    }
}'
```

这里需要强调以下两种搜索:
* 全文检索, 使用match, 会对短语进行拆分(中文句子也会拆分), 然后进行全文检索。
* 短语检索, 使用match_phrase, 是不会对短语进行拆分的，类似sql中的`SELECT * FROM tbl WHERE field like '%term%s'`

# 三、分布式集群
es可以横向扩展来分摊负载并增加可用性。
## 1.集群
### 1).*节点*
一个es进程就是一个es实例，也是es节点。一个es集群则是由多个es节点组成。
* 主节点: 集群中任何节点都可能会被选举为主节点(master),它将临时管理集群级别的一些变更，例如新建或删除索引、增加或移除节点等。
* 空节点: 一个节点上没有存储任何的
* 空集群: 一个集群只有空节点

### 2).*分片*
数据都是存储在索引中的，其实索引只是一个逻辑命名空间, 实际的文档数据都是存储在分片中的。一个分片就是一个`Lucene实例`。es将一个索引的数据划分为多个分片，多个分片均摊的分布在多个节点上。分片分为两种:
* 主分片(primary shard), 主分片的存储量完全受硬件和操作系统限制。主分片的个数是可以配置的, 主分片多则可以对数据进行更大的分摊。
* 复制分片(replica shard), 复制分片只是主分片的一个副本，它可以防止硬件故障导致的数据丢失，同时可以提供读请求，比如搜索或者从别的shard取回文档。

### 3).*客户端请求*
每个节点都知道每一个文档在集群中存储于哪个节点，因此客户端访问集群中的任何节点均可, 收到客户端请求的节点负责收集客户端所需要的数据并返回给客户端。换而言之，任何节点都是客户端的访问代理。

```sh
# 查询集群健康
curl "http://localhost:9200/_cluster/health"
# 以下字段需要关注:
# "status" : "yellow", 节点状况状态
# "number_of_nodes" : 1, (节点个数)
# "number_of_data_nodes" : 1,
# "active_primary_shards" : 5, (活动的主分片个数)
# "active_shards" : 5, (活动的复制分片个数)
# "unassigned_shards" : 5,


# 配置一个索引的分片信息
curl -XGET -H "Content-Type:application/json" "http://localhost:9200/megacorp" -d'
{
   "settings" : {
      "number_of_shards" : 10,
      "number_of_replicas" : 0
   }
}'
```
集群健康的status状态:
* green, 所有主要分片和复制分片都可用
* yellow, 所有主要分片可用，但不是所有复制分片都可用
* red, 不是所有的主要分片都可用

## 3.故障转移
## 4.横向扩展

# 四、数据存储
## 1.文档
在Elasticsearch中, 文档(document)这个术语有着特殊含义。它特指最顶层结构或者根对象(root object)序列化成的JSON数据(以唯一ID标识并存储于Elasticsearch中)。文档包含以下元数据:
元数据|描述
-|-
_index|文档存储的地方, 类似于SQL中的DB
_type|文档代表的对象的类, 对象的分类。类似于SQL中的TABLE，一个TABLE中的对象的含义和构造往往是相同的。
_id|文档的唯一标识，仅仅是一个字符串，它与_index和_type组合时，就可以在Elasticsearch中唯一标识一个文档。当创建一个文档，你可以自定义_id，也可以让Elasticsearch帮你自动生成。

## 2.文档存储
```sh
# 指定一个文档的id进行存储
curl -H "Content-Type:application/json" "http://localhost:9200/{index}/{type}/{id}" -d '
{
  "field": "value",
  ...
}'

# es分配id, 分配的id在该请求的响应中
curl -H "Content-Type:application/json" "http://localhost:9200/{index}/{type}" -d '
{
  "field": "value",
  ...
}'

# 显式的创建文档, 若文档已经存在会返回失败
curl -H "Content-Type:application/json" "http://localhost:9200/<index>/<type>/<id>/_create" -d '{ ... }'

# 删除文档
curl -XDELETE "http://localhost:9200/<index>/<type>/<id>"
```

## 3.检索文档
这里只给出如何在已知index, type, id时如何读取文档和过滤某些字段。全文检索参考其他章节信息。
```sh
curl -i -XGET "http://localhost:9200/<index>/<type>/<id>"

# 筛选出文档需要返回的字段
curl -i -XGET "http://localhost:9200/<index>/<type>/<id>?_source=<field1>,<field2>"

# 检查文档是否存在
curl -i -XHEAD "http://localhost:9200/<index>/<type>/<id>"
```

## 4.更新文档
文档在Elasticsearch中是不可变的。如果需要更新已存在的文档，可以使用index API 重建索引(reindex) 或者替换掉它。一次变更，文档的version会递增。
```sh
# REQUEST:
curl -H "Content-Type:application/json" "http://localhost:9200/<index>/<type>/<id>" -d'
{
    "field": <field>,
    ...
}'

# RESPONSE:
{
    "_index" :   <index>,
    "_type" :    <type>,
    "_id" :      <id>,
    "_version" : 2,                 # 版本号递增
    "created":   false              # 是否为新创建
}

# 指定版本号的更新, 若es中文档的version不一致，不会更新成功
curl -XPUT -H "Content-Type:application/json" "http://localhost:9200/<index>/<type>/<id>?version=<version>" -d'
{
  "title": "My first blog entry",
  "text":  "Starting to get the hang of this..."
}'
```
在内部，Elasticsearch已经标记旧文档为删除并添加了一个完整的新文档。旧版本文档不会立即消失，但你也不能去访问它。Elasticsearch会在你继续索引更多数据时清理被删除的文档。

# 五、搜索
搜索分为两种:
* 结构化搜索, 类似MySQL, 直接对字段进行搜索精确的搜索
* 全文搜索, 对任意字段进行关键词搜索, 根据评分返回结果

搜索涉及三种概念和技术:
概念|描述
-|-
映射(Mapping)|数据在每个字段中的解释说明
分析(Analysis)|全文是如何处理的可以被搜索的
领域特定语言查询(Query DSL)|Elasticsearch使用的灵活的、强大的查询语言

空搜索是不指定任何的搜索条件:
* 如果对集群执行空搜索, 会返回集群中的所有文档, `curl "http://localhost:9200/_search"`
* 如果对index执行空搜索, 会返回index中的所有文档, `curl "http://localhost:9200/index/_search"`
* 如果对type执行空搜索, 会返回type中的所有文档, `curl "http://localhost:9200/index/type/_search"`
* 如果对id执行空搜索, 会返回指定id的文档, `curl "http://localhost:9200/index/type/id_search"`

响应中需要关注的字段:
* hits, 返回了指定目标中的所有文档个数, 以及其中的前十个文档, 每个文档都有个相关性得分, 得分搞的排在前面，对于空搜素，所有的得分都是1.0.
* took, 搜索花的时间
* shards, 参与查询的分片个数
* timeout, 查询超时与否, 在查询中指定`?timeout=10ms`, es将会返回在超时时间内可以收集到的结果。

在进行空搜索时，我们可以通过逗号分割以及通配符的方式来指定多个索引和多个类型, 例如
```sh
# 对g和u开头的index进行空搜索
GET /g*,u*/_search

# 对gb和us的index进行空搜索
GET /gb,us/_search

# 对gb和us的index以及user,tweet的type进行空搜索
GET /gb,us/user,tweet/_search
```

搜索中常用的查询字符串参数:
* 空搜索参数
    * timeout, es将会返回在超时时间内可以收集到的结果。
    * from, 查询偏移, 用于分页
    * size, 查询个数, 用于分页
* 简易查询参数(更长用的是JSON BODY的方式)
    * q, 简易搜索条件`?q=<expression>`
        * `<expression>`可以是一个单纯的值, 会对指定index/type下的文档的所有字段搜索包含值value的文档。 `?q=value`
        * `<expression>`可以是一个k:v, 会对指定index/type下的所有文档的k字段搜索value文档。`?q=k:v`
        * `<expression>`可以是一批k:v, 并且包含+-前缀。
            * +前缀, 表示必须满足的k:v条件
            * -前缀, 表示必须不满足的k:v条件
            * 不带前缀, 表示k:v条件可选, 若满足增加评分
    * 注意: 
        * 这个搜索是会进行分词的! 
        * 若有中文需要进行urlencode!
        * 对于?q=v的形式, 其实es是把所有字段的信息都统一存放到了一个_all字段, 这个字段对于用户而言是透明的, 当使用这种搜索的时候, 就是查询的_all字段

几个Demo
```sh
curl -XGET -H "Content-Type:application/json" "http://localhost:9200/megacorp/employee/_search?q=last_name:adyen"

curl -XGET -H "Content-Type:application/json" "http://localhost:9200/megacorp/employee/_search?q=last_name:%e5%be%ae%e4%bf%a1"
```

# 六、映射与分析
* 映射(mapping)机制用于进行字段类型确认，将每个字段匹配为一种确定的数据类型(string, number, booleans, date等)
* 分析(analysis)机制用于进行全文文本(Full Text)的分词，以建立供搜索用的反向索引。

## 1.数据类型
存储在文档中的字段有数据类型的概念, 我们需谨慎对待, 否则查询不出我们需要的结果, 甚至还会存储文档失败。字段的数据类型由mapping机制进行管理
```sh
# 会查询出指定index下所有type的所有字段映射
curl "http://localhost:9200/<index>/_mapping"

# 会查询出指定type下所有字段映射
curl "http://localhost:9200/<index>/<type>/_mapping"
```
对于_all字段是不会显示的, 该字段是string类型。

## 2.分析器
一个分析器由三种组件：
* 字符过滤器(character filter), 字符过滤器能够去除HTML标记，或者转换"&"为"and"。
* 分词器(tokenizer), 判断如何将一句话划分为多个token
* 标记过滤(token filters), 将大小写、同义词、同根词等进行统一处理，并且也会去掉停顿次, 例如 a/and/the 等等。

一个es内建了几种分析器:
* 标准分析器, 标准分析器是Elasticsearch默认使用的分析器。对于文本分析，它对于任何语言都是最佳选择。根据Unicode Consortium的定义的单词边界(word boundaries)来切分文本，然后去掉大部分标点符号。最后，把所有词转为小写。
* 简单分析器, 非字母作为分隔符, 分割出来的单词转化为小写。
* 空格分析器, 依据空格对文本进行拆分, 不做大小写转化。


测试分析器对于某个单词的分析，可以利用analyze API来查看。
```sh
# 测试分析器
curl -H "Content-Type:application/json" "http://localhost:9200/_analyze?pretty" -d '{"analyzer": "standard", "text": "中华人民共和国"}'
```

其中analyzer参数选项如下:
分析器|符号|描述
-|-|-
standard analyzer|standard|标准分析器
simple analyzer|simple|简单分析器, 转换为小写, 会通过非字母进行分割
keyword analyzer|keyword|不分词，内容整体作为一个token(not_analyzed)

## 3.映射
映射用于管理和记录文档每个字段的类型。众所周知，一个type类似于mysql中的表，一个type中的document的结构应该是一样的，因此每个type都由一个映射管理。当往一个type中添加document时，若新的doc的结构(主要是字段类型)和type的映射冲突, 则添加doc会失败。

### 1).es的索引基本类型
es的可以通过_mapping api来查询字段的映射, 映射结果中的type记录着数据类型。存在以下的数据类型:
类型|表示的数据类型
-|-
会分析的String|text
不分析的String|keyword
Whole number|byte, short, integer, long
Floating point|float, double
Boolean|boolean
Date|date

```sh
# 查询一个type的映射
curl "http://localhost:9200/megacorp/employee/_mapping"
```

### 2).*JSON文档的对应的es数据类型*
JSON type|ElasticSearch Field type
-|-
Boolean: true or false|"boolean"
Whole number: 123|"long"
Floating point: 123.45|"double"
String, valid date: "2014-09-15"|"date"
String: "foo bar"|"text"

如果映射中某个字段是long, 新增的文档该字段是text, 会进行string to long, 若失败则抛出异常。若新增的文档中有新增的字段, 则在映射中会添加该字段。

### 3).*自定义映射*
映射中除了包含字段的类型信息，还包括了字段的索引信息:
* index, 参数控制字符串以何种方式被索引
    * true, 首先分析这个字符串，然后索引。换言之，以全文形式索引此字段。
    * false, 该字段不进行索引
* analyzer, 只有当index为analyzed时才有效, 指定该字段的分析方式。默认是standard
* type, 若为keyword, 则该字段不进行分词

可以在第一次创建索引的时候指定映射的类型。此外，你也可以晚些时候为新类型添加映射。但是已经存在的映射不能变更。
```sh
# 给一个【新的索引】配置type的映射
curl -XPUT -H "Content-Type:application/json" "http://localhost:9200/<index>" -d '
{
  "mappings": {
    "<type>" : {
      "properties" : {
        "<field-1>" : {
          "type" :    "string",
          "analyzer": "simple"
        },
        "<field-2>" : {
          "type" :   "date"
          "index": "false"
        }
      }
    }
  }
}'

# 给一个type添加一个字段映射
q
# 以指定字段映射的分析器来分析文本
curl "http://localhost:9200/megacorp/_mapping/_analyze?field=tag&text=Black-cats"
```

# 七、结构化查询
通过在查询请求中携带body, 可以更好的描述查询行为, 明确查询参数。需要注意, 查询通常是GET请求, 这需要在GET请求中携带body, RFC并没有规定GET请求中不能携带body, 所以这是可以的。但是get请求中携带body并不被很多http库广泛支持, 所以查询用POST请求也是可以的。请求体查询允许我们使用结构化查询Query DSL(Query Domain Specific Language)。
```sh
# 一个分页的body
{
  "from": 30,
  "size": 10
}
```
## 1.Query DSL
Query DSL主要是在body的query中编写具体的查询条件:
```sh
curl -XGET -H "Content-Type:application/json" "http://localhost/<index>/<type>/_search" -d'
{
    "query": <QUERY_EXPRESSION>
}'
```
其中`<QUERY_EXPRESSION>`可以有很多查询子句构成, 如:
```json
{
    "query": {
        "QUERY_NAME": {
            "FIELD": VALUE,
        }
    }
}
```
上面json中的`QUERY_NAME`就是查询子句的名称。一个query里面只能有一个查询子句, 但是多个查询子句可以组合为一个查询子句.
```sh
# 单个查询子句
curl -XGET -H "Content-Type:application/json" "http://localhost:9200/_search" -d'
{
    "query": {
        "match": {"last_name": "adyen"},
    }
}'
```
子句分为两种:
* 叶子子句: 用以在将查询字符串与一个字段(或多字段)进行比较, 例如match子句
* 复合子句: 可以将其他叶子子句和复合子句进行复合，得到一个查询子句。

## 2.常用的查询子句
下面介绍常用的查询子句, 这些都是`叶子子句`
### 1).*term*
term主要用于精确匹配哪些值，比如数字，日期，布尔值或not_analyzed的字符串。对于analyzed的字符串最好别用term, 否则会有未定义行为。
```sh
curl -XGET -H "Content-Type:application/json" "http://localhost:9200/_search" -d'
{
    "query": {
        "term": {"age": 25}
    }
}'
```
### 2).*terms*
terms 跟 term 有点类似，但 terms 允许指定多个匹配条件。 如果某个字段指定了多个值，那么文档需要一起去做匹配。对于analyzed的字符串最好别用term, 否则会有未定义行为。
```sh
curl -XGET -H "Content-Type:application/json" "http://localhost:9200/_search" -d'
{
    "query": {
        "terms": {"age": ["25", "26"]}
    }
}'
```
### 3).*range*
适用于字段类型为数字的, 查询出字段处于某个范围的文档。
```sh
curl -XGET -H "Content-Type:application/json" "http://localhost:9200/_search" -d'
{
    "query": {
        "range": {"age": {"gte": "20", "lte":"30"}}
    }
}'
```
### 4).*exists和missing*
用于查询出某个字段存在或不存在的文档。
```sh
curl -XGET -H "Content-Type:application/json" "http://localhost:9200/_search" -d'
{
    "query": {
        "exists": {"field": "last_name"}
    }
}'
```
### 5).*match*
match查询是一个标准查询，不管你需要全文本查询还是精确查询基本上都要用到它。
如果你使用 match 查询一个全文本字段，它会在真正查询之前用分析器先分析match一下查询字符
```sh
curl -XGET -H "Content-Type:application/json" "http://localhost:9200/_search" -d'
{
    "query": {
        "match": {"last_name": "adyen"}
    }
}'
```
### 6).*match_phrase*
相比于match查询, match_phrase【不会】对查询的字符串分词。
```sh
curl -XGET -H "Content-Type:application/json" "http://localhost:9200/_search" -d'
{
    "query": {
        "match_phrase": {"last_name": "adyen"}
    }
}'
```
### 7).*multi_match*
对多个字段执行match请求
```sh
curl -XGET -H "Content-Type:application/json" "http://localhost:9200/_search" -d'
{
    "query": {
        "multi_match": {
            "query": "adyen",
            "fields": ["first_name", "last_name"]
        }
    }
}'
```

### 8).*match_all*
这个是个特殊的match，里面不会有任何数据，本质上是一个空查询.
```sh
curl -XGET -H "Content-Type:application/json" "http://localhost:9200/_search" -d'
{
    "query": {
        "match_all": {}
    }
}'
```

### 9).*bool复合子句*
上述的叶子节点都类似于SQL命令中的WHERE语句中的单一条件，bool可以将那些单一的条件通过OR和AND进行组合。bool可以支持的子查询语句有:
* must, 对叶子查询语句提供了AND的功能
* must_not, 对叶子查询语句提供了 AND NOT 功能
* should, 对叶子查询语句提供了OR功能

bool通过must,must_not和should, 将这些AND和OR的复合语句进一步复合, 交给query。
```sh
curl -XGET -H "Content-Type:application/json" "http://localhost:9200/_search" -d'
{
    "query": {
        "bool": {
            "must": [
                {"match": {"last_name": "adyen"}},
                {"term": {"age":  25}}
            ],
            "should":[
                {"match": {"first_name": "hello"}}
            ]
        }
    }
}'
```
must/must_not/should通过数组来进行AND和OR的组合。

## 3.验证查询
通过验证查询API，可以知道一个查询的BODY是否合法，并且会给出若不合法的原因。
```sh
curl -XGET -H "Content-Type:application/json" "http://localhost:9200/_validate/query?explain" -d '
{
   "query": {
      "match" : {
         "last_name" : "微小"
      }
   }
}'
```
如果是合法语句的话，使用 explain 参数可以返回一个带有查询语句的可阅读描述， 可以帮助了解查询语句在ES中是如何执行的。

# 八、排序
## 1.排序
默认情况下，结果集会按照相关性进行排序, 排名越靠前(即默认以降序排列)。我们也可以人为的选择按某一个字段进行排序
```sh
# 单一字段排序
curl -XGET -H "Content-Type:application/json" "http://localhost:9200/_search" -d '
{
    "query" : {
        "match_all" : {}
    },
    "sort": { "age": { "order": "desc" }}
}'
```
order的参数可以是:
* desc, 降序.对于_score的相关性排序默认以降序排列
* asc, 升序.对于指定的字段 默认以升序排列

当选定了某个字段进行排序后, max_score, _score等信息将会无效。es还支持多级排序, 即首先按第一个字段排序，对于该字段相等的按第二个字段排序。
```sh
# 多级排序
curl -XGET -H "Content-Type:application/json" "http://localhost:9200/_search" -d '
{
    "query" : {
        "match_all" : {}
    },
    "sort": [
        { "age": { "order": "desc" }},
        { "_score": { "order": "desc" }},
    ]
}'
```

## 2.相关性
每个文档都有相关性评分，用一个相对的浮点数字段 _score 来表示 -- _score 的评分越高，相关性越高。查询语句会为每个文档添加一个 _score 字段。评分的计算方式取决于不同的查询语句。ElasticSearch的相似度算法被定义为 TF/IDF，即检索词频率/反向文档频率:
* 检索词频率: 检索词在该字段出现频率越高，则文档的相关性也越高。
* 反向文档频率: 检索词在文档(除了指定的字段)出现的频率越高，则文档的相关性越低。es中用IDF表示在文档中出现的次数, 1/IDF用于计算相关性。
* 字段长度准则: 字段的长度越长，相关性越低。

通过在查询语句中使用explain，可以返回时带上_score的计算依据。(好像在现在最新的版本里面不是这么传的)
```sh
curl -XGET -H "Content-Type:application/json" "http://localhost:9200/_search?explain" -d '
{
   "query": {
      "match" : {
         "last_name" : "微小"
      }
   }
}'
```

# 九、索引管理

# 十、深入结构化搜索

# 附录、常用命令
```sh
# 查询健康状况
curl "http://localhost:9200/_cat/heath?v"

# 创建索引
$ curl -XPUT -d '{"settings" : {"number_of_shards" : 3,"number_of_replicas" : 1}}' "http://localhost:9200/<index>/?pretty"

# 查看全部索引
$ curl "http://localhost:9200/_cat/indices?v"

# 查看一个索引的信息
$ curl -XGET "http://localhost:9200/<index>?pretty"

# 测试分词器
$ curl -H "Content-Type:application/json" -XPOST -d '{"analyzer": "ik_smart","text": "中华人民共和国"}' "http://localhost:9200/_analyze?pretty"
```