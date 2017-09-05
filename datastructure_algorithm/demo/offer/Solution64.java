package offer;

import java.util.ArrayList;
import java.util.LinkedList;
import java.util.Queue;

/**
 * 
 * 滑动窗口的最大值
 * 
 * @author lu
 *
 */
public class Solution64 {

	public static void main(String[] args) {
		System.out.println(new Solution64().maxInWindows(new int[] {10,14,12,11}, 0));
	}
	
	public ArrayList<Integer> maxInWindows(int [] num, int size){
		ArrayList<Integer> list = new ArrayList<>();
		MaxQueue mq = new MaxQueue();
		if(size==0 || size>num.length) {
			return list;
		}
		for(int i=0; i<size; i++) {
			mq.push(num[i]);
		}
		list.add(mq.max());
		for(int i=size; i<num.length; i++) {
			mq.pop();
			mq.push(num[i]);
			list.add(mq.max());
		}
		return list;
    }

	static class MaxQueue{
		LinkedList<Integer> s = new LinkedList<>();
		Queue<Integer> q = new LinkedList<>();
		
		public Integer max() {
			if(s.isEmpty()) {
				return null;
			}
			return s.getLast();
		}
		
		public void push(int n) {
			if(s.size()==0 || n>s.getLast()) {
				s.add(n);
			}
			q.add(n);
		}
		
		public int pop() {
			int n=q.poll();
			if(n==s.getFirst()) {
				s.removeFirst();
			}
			if(s.isEmpty() && !q.isEmpty()) {
				int max =  Integer.MIN_VALUE;
				for(Integer num:q) {
					if(num>max) {
						max = num;
					}
				}
				s.push(max);
			}
			return 0;
		}
		
		@Override
		public String toString() {
			StringBuilder sb = new StringBuilder();
			sb.append(s.toString()).append("\n").append(q.toString());
			return sb.toString();
		}
	}
}
