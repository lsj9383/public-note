package com.lsj.jvm.memory;

import java.util.ArrayList;
import java.util.List;

public class HeapOOM {
	
	static class OOMObject{
		
	}
	
	/*
	 * -Xms20m -Xmx20m -XX:+HeapDumpOnOutOfMemoryError
	 */
	
	public static void main(String[] args) {
		List<OOMObject> list = new ArrayList<>();
		while(true){
			list.add(new OOMObject());
		}

	}

}
