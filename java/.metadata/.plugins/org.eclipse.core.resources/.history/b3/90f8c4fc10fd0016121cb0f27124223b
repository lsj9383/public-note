package com.lsj.util;

public interface LIterator<T> {
	
	/**
	 * 判断是否存在下一个元
	 * 
	 * @return true or false
	 */
	boolean hasNext();
	
	/**
	 * 返回下一个元素
	 * 
	 * @return
	 */
	T next();
	
	/**
	 * 删除前一个元素
	 * 
	 */
	default void remove(){
		throw new UnsupportedOperationException("remove");
	}

}
