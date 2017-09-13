package toutiao;

import java.util.Comparator;
import java.util.PriorityQueue;

/**
 * 
 * 可以直接返回中位数的数据结构
 * 
 * @author lu
 *
 */
public class Main9 {

	public static void main(String[] args) {
	}
	
	static class Mid{
		PriorityQueue<Integer> minQ = new PriorityQueue<>();
		PriorityQueue<Integer> maxQ = new PriorityQueue<>(new Comparator<Integer>() {
		    @Override
		    public int compare(Integer o1, Integer o2) {
		        return o2 - o1;
		    }
		});
		int size = 0;
		
		public void add(Integer n) {
			if((size&1)==0) {
				maxQ.add(n);
			}else {
				minQ.add(n);
			}
		}
		
		public int mid() {
			if((size&1)==0) {
				return (maxQ.peek()+minQ.peek())/2;
			}else {
				return minQ.peek();
			}
		}
	}
}
