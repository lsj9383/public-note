package demo;

import com.lsj.util.LArrayList;
import com.lsj.util.LIterator;

public class Main {

	public static void main(String[] args) {
		
		LArrayList<Integer> list = new LArrayList<>();
		for(int i=0; i<50; i++){
			list.add(i);
		}
		
		list.remove(20);
		list.remove(new Integer(30));
		
		for(LIterator<Integer> it = list.iterator(); it.hasNext();){
			System.out.println(it.next());
		}
	}
}