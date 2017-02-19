package com.lsj.jvm.classfile;

public class TestClass{
	
	private int m;
	public int fun(){
		privMethod();
		publicMethod();
		staticPublicMethod();
		return m+1;
	}
	
	private void privMethod(){
		System.out.println("private");
	}
	
	public void publicMethod(){
		System.out.println("public");
	}
	
	static public void staticPublicMethod(){
		System.out.println("static public");
	}
}