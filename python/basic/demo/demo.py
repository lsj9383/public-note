

from __future__ import print_function

class FrozenJson(object):
    def __new__(cls, arg):
        print("start")
        if arg == 1:
            print("=== 1 ===")
            return super().__new__(cls)
        if arg == 2:
            print("=== 2 ===")
            return [1,2,3,4]
        if arg == 3:
            print("=== 3 ===")
            return 4
        print("=== other ===")
        return object()

    def __init__(self, arg):
        print(self)
        print(arg)

FrozenJson(1)