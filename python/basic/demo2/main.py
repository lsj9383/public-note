import a, b
import module.c
import imp

print(a.o)
print(b.o)
print(module.c.o)
imp.reload(a);
imp.reload(module.c);
print(a.o)
print(b.o)
print(module.c.o)