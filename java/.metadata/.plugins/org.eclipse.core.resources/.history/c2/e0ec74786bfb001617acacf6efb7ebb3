package demo;

public class Main {

	public static void main(String[] args) {
		/*
		//可以存放父类，方法对象可以是子类
		Store<? super Employee> o1 = new Store<Person>();
		o1.set(new Employee());
		
		//可以存放子类，方法参数含有泛型类对象的不可执行
		Store<? extends Person> o2 = new Store<Employee>();
		o2.get();
		o2.set(new Employee());
		*/
		System.out.println(new Person().compareTo(null));
		System.out.println(new Employee().compareTo(null));
	}
}

class Store<T>{
	private T obj;
	public void set(T obj){
		this.obj = obj;
	}
	
	public T get(){
		return obj;
	}
}

class Person implements Comparable<Person>{

	@Override
	public int compareTo(Person o) {
		return 1;
	}
	
}

class Employee extends Person{}
class Singer extends Person{}