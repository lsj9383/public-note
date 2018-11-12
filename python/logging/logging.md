# 一、日志模块
日志模块有四块基本组件:
* 日志对象 - Logger
* 日志处理器对象 - Handler
* 日志格式化对象 - Formatter
* 日志过滤器 - Filters

现在对每个对象的源码进行一一分析

# 二、Logger
## 1.*初始化日志对象*
应用层通过`g_logger = logging.getLogger(<logger-name>)`获取日志对象
```py
# 根日志对象
root = RootLogger(WARNING)
Logger.root = root
Logger.manager = Manager(Logger.root)
# 默认的日志对象类
_loggerClass = Logger
# 默认的日志处理器，当日志对象不存在处理器时就用这个日志处理器
_defaultLastResort = _StderrHandler(WARNING)
lastResort = _defaultLastResort

# 从Manager中获取日志对象
def getLogger(name=None):
    if name:
        # 当设定了Logger的名字，则到manager中获取日志
        return Logger.manager.getLogger(name)
    else:
        # 否则使用默认的root-logger
        return root

# Manager类，管理了系统中所有的日志
class Manager(object):
    def __init__(self, rootnode):
        self.root = rootnode
        self.disable = 0
        self.emittedNoHandlerWarning = False
        # loggerDict 缓存了所有的日志对象
        self.loggerDict = {}
        self.loggerClass = None
        self.logRecordFactory = None

    def getLogger(self, name):
        rv = None
        if not isinstance(name, str):
            raise TypeError('A logger name must be a string')
        _acquireLock()
        try:
            if name in self.loggerDict:
                # 若日志对象已经存在，则直接取出使用
                rv = self.loggerDict[name]
                if isinstance(rv, PlaceHolder):
                    ph = rv
                    rv = (self.loggerClass or _loggerClass)(name)
                    rv.manager = self
                    self.loggerDict[name] = rv
                    self._fixupChildren(ph, rv)
                    self._fixupParents(rv)
            else:
                # 若日志对象不存在, 则通过loggerClass和_loggerClass获取一个新的日志对象
                rv = (self.loggerClass or _loggerClass)(name)
                rv.manager = self
                self.loggerDict[name] = rv
                self._fixupParents(rv)
        finally:
            _releaseLock()
        return rv

# 根日志对象类, 该类和Logger类没有任何区别，除了名字叫root外
class RootLogger(Logger):
    def __init__(self, level):
        Logger.__init__(self, "root", level)

# 日志对象
class Logger(Filterer):
    """
    Instances of the Logger class represent a single logging channel. A
    "logging channel" indicates an area of an application. Exactly how an
    "area" is defined is up to the application developer. Since an
    application can have any number of areas, logging channels are identified
    by a unique string. Application areas can be nested (e.g. an area
    of "input processing" might include sub-areas "read CSV files", "read
    XLS files" and "read Gnumeric files"). To cater for this natural nesting,
    channel names are organized into a namespace hierarchy where levels are
    separated by periods, much like the Java or Python package namespace. So
    in the instance given above, channel names might be "input" for the upper
    level, and "input.csv", "input.xls" and "input.gnu" for the sub-levels.
    There is no arbitrary limit to the depth of nesting.
    """
    def __init__(self, name, level=NOTSET):
        Filterer.__init__(self)
        # 日志对象的基本属性，包括名称，级别，父日志对象，处理器，有效无效标识等
        self.name = name
        self.level = _checkLevel(level)
        self.parent = None
        # 默认允许子日志对象调用该对象进行日志输出
        self.propagate = True
        self.handlers = []
        self.disabled = False

    def setLevel(self, level):
        self.level = _checkLevel(level)

    def findCaller(self, stack_info=False):
        f = currentframe()
        #On some versions of IronPython, currentframe() returns None if
        #IronPython isn't run with -X:Frames.
        if f is not None:
            f = f.f_back
        rv = "(unknown file)", 0, "(unknown function)", None
        while hasattr(f, "f_code"):
            co = f.f_code
            filename = os.path.normcase(co.co_filename)
            if filename == _srcfile:
                f = f.f_back
                continue
            sinfo = None
            if stack_info:
                sio = io.StringIO()
                sio.write('Stack (most recent call last):\n')
                traceback.print_stack(f, file=sio)
                sinfo = sio.getvalue()
                if sinfo[-1] == '\n':
                    sinfo = sinfo[:-1]
                sio.close()
            rv = (co.co_filename, f.f_lineno, co.co_name, sinfo)
            break
        return rv

    # 生成记录对象
    def makeRecord(self, name, level, fn, lno, msg, args, exc_info,
                   func=None, extra=None, sinfo=None):
        rv = _logRecordFactory(name, level, fn, lno, msg, args, exc_info, func,
                             sinfo)
        if extra is not None:
            for key in extra:
                if (key in ["message", "asctime"]) or (key in rv.__dict__):
                    raise KeyError("Attempt to overwrite %r in LogRecord" % key)
                rv.__dict__[key] = extra[key]
        return rv

    # 底层日志驱动， 创建日志记录，并调用所有的handler对象进行记录
    def _log(self, level, msg, args, exc_info=None, extra=None, stack_info=False):
        sinfo = None
        if _srcfile:
            try:
                fn, lno, func, sinfo = self.findCaller(stack_info)
            except ValueError: # pragma: no cover
                fn, lno, func = "(unknown file)", 0, "(unknown function)"
        else: # pragma: no cover
            fn, lno, func = "(unknown file)", 0, "(unknown function)"
        if exc_info:
            if isinstance(exc_info, BaseException):
                exc_info = (type(exc_info), exc_info, exc_info.__traceback__)
            elif not isinstance(exc_info, tuple):
                exc_info = sys.exc_info()
        # 生成日志对象
        record = self.makeRecord(self.name, level, fn, lno, msg, args,
                                 exc_info, func, extra, sinfo)
        # 用处理器输出日志
        self.handle(record)

    # 当disable为false，并且filter运行打日志时，调用handlers进行日志输出
    def handle(self, record):
        if (not self.disabled) and self.filter(record):
            self.callHandlers(record)

    # 添加处理器
    def addHandler(self, hdlr):
        _acquireLock()
        try:
            if not (hdlr in self.handlers):
                self.handlers.append(hdlr)
        finally:
            _releaseLock()

    # 移除处理器
    def removeHandler(self, hdlr):
        _acquireLock()
        try:
            if hdlr in self.handlers:
                self.handlers.remove(hdlr)
        finally:
            _releaseLock()

    # 判断日志对象是否有日志处理器，查询范围包含parent
    def hasHandlers(self):
        c = self
        rv = False
        while c:
            if c.handlers:
                rv = True
                break
            if not c.propagate:
                break
            else:
                c = c.parent
        return rv

    # 调用所有的handler对象进行记录
    # handler记录完成后，会调用父日志对象进行记录
    # 如果没有一个handler，则输出一个错误信息到sys.stderr
    def callHandlers(self, record):
        c = self
        found = 0
        # 一直遍历到祖先日志对象，若父日志对象propagate为空，则停止
        while c:
            # 调用处理器进行记录
            for hdlr in c.handlers:
                found = found + 1
                if record.levelno >= hdlr.level:
                    hdlr.handle(record)
            if not c.propagate:
                c = None    #break out
            else:
                c = c.parent
        # 如果没有发现一个处理器，采用lastResort日志处理器进行输出，如果这都没有，直接输出无处理器异常
        if (found == 0):
            if lastResort:
                if record.levelno >= lastResort.level:
                    lastResort.handle(record)
            elif raiseExceptions and not self.manager.emittedNoHandlerWarning:
                sys.stderr.write("No handlers could be found for logger"
                                 " \"%s\"\n" % self.name)
                self.manager.emittedNoHandlerWarning = True

    # 判断某个级别是否有效
    def isEnabledFor(self, level):
        if self.manager.disable >= level:
            return False
        return level >= self.getEffectiveLevel()

    # 获取这个日志对象的子日志对象，如果没有则会新建
    def getChild(self, suffix):
        if self.root is not self:
            suffix = '.'.join((self.name, suffix))
        return self.manager.getLogger(suffix)

    def __repr__(self):
        level = getLevelName(self.getEffectiveLevel())
        return '<%s %s (%s)>' % (self.__class__.__name__, self.name, level)
```


# 三、Handler

# 四、Formatter