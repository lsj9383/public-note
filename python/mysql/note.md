# 一、Python的MySQL解析
很多语言都有自己的MySQL的框架，Python易读，以小见大，了解MySQL框架的工作机制。
## 1.相关库
```py
import mysql.connector                      # 数据库单个连接对象
from DBUtils.PooledDB import PooledDB       # 数据库连接池
```
## 2.使用
```py
# 建立单个连接
conn = mysql.connector.connect(user='root', password='password', database='test', use_unicode=True)
# 建立多个连接(连接池)
connect_pool = PooledDB(mysql.connector, pool_size,  host=host, user=user, passwd=password, db=db, port=3306)
connect_pool.connection()   # 获得某个连接
```
每个数据库连接都对应了一个TCP连接，一个连接可以复用，但是为了保障线程安全，会申请独占锁，当数据库操作线程过多时，将会导致性能问题。

# 二、建立连接池
```py
class PooledDB:
    """Set up the DB-API 2 connection pool.
    creator: 可调用参数，用于返回一个新的连接对象，该连接对象应该满足DB-API接口
    mincached: 初始化空连接的个数, 0表示不会初始化任何连接
    maxcached: 最大的空闲连接个数，若空闲连接个数大于该值，则会将多余的关闭
    maxshared: 共享连接的最大个数, 0或None表示不会进行仍和共享，最大值为maxcached，表示只要需要就进行共享(可以认为所有连接都是共享连接, 共享连接指的是这个连接可以用来共享)
    maxconnections： 最多的连接个数, 0表示任意数量的连接
    blocking: 当连接达到最大的连接数时，若又有新的连接请求时，会阻塞直到有多余的连接
    maxusage: 单个连接的最大重用个数，当达到使用次数会释放掉该链接，并创建新连接，0或None表示无限重用
    reset: 当连接回到连接池中的时候的重置方式。False或None回滚到begin()处，True总是回滚。
    ping: 连接检测数据库连接的时机:
        0 or None从不检测,
        1, 从连接池中拿去的时候检测,
        2, cursor被创建时检测,
        4, 当执行query时检测
        7, 1+2+4，所有情况都进行检测
    args, kwargs: 用于creator创建连接时传入的参数
    setsession： 
        setsession: optional list of SQL commands that may serve to prepare
            the session, e.g. ["set datestyle to ...", "set time zone ..."]
    failures: an optional exception class or a tuple of exception classes
        for which the connection failover mechanism shall be applied,
        if the default (OperationalError, InternalError) is not adequate
    """
    def __init__(
            self, creator, mincached=0, maxcached=0,
            maxshared=0, maxconnections=0, blocking=False,
            maxusage=None, setsession=None, reset=True,
            failures=None, ping=1,
            *args, **kwargs):
        try:
            threadsafety = creator.threadsafety
        except AttributeError:
            try:
                if not callable(creator.connect):
                    raise AttributeError
            except AttributeError:
                threadsafety = 2
            else:
                threadsafety = 0
        # 保存传入的参数
        self._creator = creator
        self._args, self._kwargs = args, kwargs
        self._blocking = blocking
        ...

        # 共享连接缓存
        if threadsafety > 1 and maxshared:
            self._maxshared = maxshared
            self._shared_cache = []  # the cache for shared connections
        else:
            self._maxshared = 0

        # _idle_cache, 空连接缓存, 其实就是连接池，保存空连接对象的。空连接都会放在这个缓存中。
        self._idle_cache = []
        self._lock = Condition()
        self._connections = 0
        # 创建到数据库的连接，注意这些都不是空连接，因为不在_idle_cache中
        idle = [self.dedicated_connection() for i in range(mincached)]
        # 连接关闭，主要是为了放入_idle_cache，成为空连接
        while idle:
            idle.pop().close()

    # 建立连接, 并且这些连接不会进行共享
    def dedicated_connection(self):
        return self.connection(shareable=False)
```

# 三、建立和获取连接
```py
# 从连接池中获得一个稳定的，具有缓存的DB-API2的连接对象
def connection(self, shareable=True):
    if shareable and self._maxshared:
        # 获得共享连接
        self._lock.acquire()
        try:
            while (not self._shared_cache and self._maxconnections
                    and self._connections >= self._maxconnections):
                self._wait_lock()
            if len(self._shared_cache) < self._maxshared:
                # shared cache is not full, get a dedicated connection
                try:  # first try to get it from the idle cache
                    con = self._idle_cache.pop(0)
                except IndexError:  # else get a fresh connection
                    con = self.steady_connection()
                else:
                    con._ping_check()  # check this connection
                con = SharedDBConnection(con)
                self._connections += 1
            else:  # shared cache full or no more connections allowed
                self._shared_cache.sort()  # least shared connection first
                con = self._shared_cache.pop(0)  # get it
                while con.con._transaction:
                    # do not share connections which are in a transaction
                    self._shared_cache.insert(0, con)
                    self._wait_lock()
                    self._shared_cache.sort()
                    con = self._shared_cache.pop(0)
                con.con._ping_check()  # check the underlying connection
                con.share()  # increase share of this connection
            # put the connection (back) into the shared cache
            self._shared_cache.append(con)
            self._lock.notify()
        finally:
            self._lock.release()
        # 给当前的连接做一个共享连接代理
        con = PooledSharedDBConnection(self, con)
    else: 
        # 获得专用连接
        self._lock.acquire()
        try:
            # 正在使用的连接数达到上线，线程进入组塞队列(线程条件队列)
            while (self._maxconnections
                    and self._connections >= self._maxconnections):
                self._wait_lock()
            # 连接可用
            try:
                # 尝试获从连接池中取空连接
                con = self._idle_cache.pop(0)
            except IndexError:  # else get a fresh connection
                # 连接获取失败，则创建一个新连接
                con = self.steady_connection()
            else:
                # 连接获得成功，检查连接情况
                con._ping_check()  # check connection
            # 给当前的连接做一个专用连接代理
            con = PooledDedicatedDBConnection(self, con)
            # 正在使用的连接个数+1
            self._connections += 1
        finally:
            self._lock.release()
    return con

# 创建一个连接，并且该链接不会在连接池中
def steady_connection(self):
    return connect(
        self._creator, self._maxusage, self._setsession,
        self._failures, self._ping, True, *self._args, **self._kwargs)

# 工厂连接函数
def connect(
        creator, maxusage=None, setsession=None,
        failures=None, ping=1, closeable=True, *args, **kwargs):
    return SteadyDBConnection(
        creator, maxusage, setsession,
        failures, ping, closeable, *args, **kwargs)

class SteadyDBConnection:
    def __init__(
            self, creator, maxusage=None, setsession=None,
            failures=None, ping=1, closeable=True, *args, **kwargs):
        self._con = None
        self._closed = True
        # proper initialization of the connection
        try:
            self._creator = creator.connect
            self._dbapi = creator
        except AttributeError:
            # try finding the DB-API 2 module via the connection creator
            self._creator = creator
            try:
                self._dbapi = creator.dbapi
            except AttributeError:
                try:
                    self._dbapi = sys.modules[creator.__module__]
                    if self._dbapi.connect != creator:
                        raise AttributeError
                except (AttributeError, KeyError):
                    self._dbapi = None
        try:
            self._threadsafety = creator.threadsafety
        except AttributeError:
            try:
                self._threadsafety = self._dbapi.threadsafety
            except AttributeError:
                self._threadsafety = None
        if not callable(self._creator):
            raise TypeError("%r is not a connection provider." % (creator,))
        if maxusage is None:
            maxusage = 0
        if not isinstance(maxusage, baseint):
            raise TypeError("'maxusage' must be an integer value.")
        self._maxusage = maxusage
        self._setsession_sql = setsession
        if failures is not None and not isinstance(
                failures, tuple) and not issubclass(failures, Exception):
            raise TypeError("'failures' must be a tuple of exceptions.")
        self._failures = failures
        self._ping = ping if isinstance(ping, int) else 0
        self._closeable = closeable
        self._args, self._kwargs = args, kwargs
        self._store(self._create())
```

# 四、关闭连接
```py
# 这是专用连接的代理
class PooledDedicatedDBConnection:
    # 代理用与关闭连接
    def close(self):
        if self._con:
            # 归还连接给连接池
            self._pool.cache(self._con)
            self._con = None

def cache(self, con):
    self._lock.acquire()
    try:
        # 如果空闲连接没有超过最大空闲连接，则不会关闭多余的连接
        if not self._maxcached or len(self._idle_cache) < self._maxcached:
            # 重置连接
            con._reset(force=self._reset)
            # 连接池中添加
            self._idle_cache.append(con)
        else:
            # 关闭多余的连接
            con.close()
        self._connections -= 1
        # 唤醒申请连接的线程
        self._lock.notify()
    finally:
        self._lock.release()
```