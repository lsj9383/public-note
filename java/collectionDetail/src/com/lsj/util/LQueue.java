package com.lsj.util;

public interface LQueue<T> {
	int size();				//返回队列大小
	boolean isEmpty();		//判断队列是否空
	boolean add(T element);	//入队
	T poll();				//出queue，若queue为空，则返回null
	T remove();				//和poll一样，但是若queue为空，则抛出异常
}
