from multiprocessing import Process
import os

age = 13

def run_proc():
    global age
    age = 20

if __name__=='__main__':
    p = Process(target=run_proc)
    p.start()
    p.join()
    print(age)