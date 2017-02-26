package com.lsj.util;

public interface LCollection<T> extends LIterable<T>{
	
	int size();
	boolean contains(Object o);
	boolean add(T nv);
	boolean remove(T o);

}
