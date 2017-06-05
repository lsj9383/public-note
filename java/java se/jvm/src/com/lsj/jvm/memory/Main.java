package com.lsj.jvm.memory;

public class Main {

	public static void main(String[] args) throws InterruptedException {
		String str1 = new StringBuilder("¼ÆËã»ú").append("Èí¼þ").toString();
		System.out.println(str1.intern() == str1);
		
		String str2 = new StringBuilder("ja").append("va").toString();
		System.out.println(str2.intern() == str2);
		while(true){
			Thread.sleep(100);
		}
	}

}
