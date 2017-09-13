package toutiao;

import java.util.LinkedList;

/**
 * 
 * 一个数组，只有1和0，一个给定k，返回长度为k的小数组中包含个数为1的最多的数组中1的个数。
 * 
 * @author lu
 *
 */
public class Main7 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub

	}
	
	static int p(int[] array, int k) {
		LinkedList<Integer> q = new LinkedList<>();
		int cnt = 0;
		if(array.length>k) {
			return -1;
		}
		for(int i=0; i<k; i++) {
			if(array[i]==1) {
				cnt++;
			}
		}
		
		for(int i=k; i<array.length; i++) {
			q.add(array[i]);
			int first = q.removeFirst();
			if(first==1) {
				cnt--;
			}
			if(q.getLast()==1) {
				cnt++;
			}
		}
		return cnt;
	}
}
