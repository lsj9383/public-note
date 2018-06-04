def deractor(func):
    def inner(*args, **kw):
        print("start")
        func(args, kw)		# 调用被装饰的函数
        print("end")
    return inner

@deractor
def hello(name):
    print("hello", name)

print(hello.__name__)