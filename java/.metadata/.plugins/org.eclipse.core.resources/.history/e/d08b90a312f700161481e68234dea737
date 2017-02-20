package demo;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.ListIterator;

public class Main {

	public static void main(String[] args) {
		List<Integer> list = new ArrayList<>();
		list.add(1);
		list.add(2);
		list.add(3);
		list.add(4);
		list.add(5);
	
		
		ListIterator<Integer> it = list.listIterator();
		it.add(new Integer(0));
		it.next();
		while(it.hasNext()){
			it.set(1);
			System.out.println(it.next());
		}
	}

}
