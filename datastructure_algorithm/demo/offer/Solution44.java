package offer;

import java.util.PriorityQueue;

/**
 * 
 * 扑克牌顺子
 * 
 * @author lu
 *
 */
public class Solution44 {

	public static void main(String[] args) {
		System.out.println(new Solution44().isContinuous(new int[] {0,2,3,4,6}));
	}

	public boolean isContinuous(int [] numbers) {
		int king=0;
		PriorityQueue<Integer> q = new PriorityQueue<>();
		for(int n:numbers) {
			if(n==0) {
				king++;
			}else {
				q.add(n);	
			}
		}
		if(q.isEmpty()) {
			return false;
		}
		Integer prev = q.poll();
		while(!q.isEmpty()) {
			Integer c = q.poll();
			while(prev+1!=c) {
				if(king==0) {
					return false;
				}else {
					king--;
					prev++;
				}
			}
			prev=c;
		}
		
		return !q.isEmpty() && true;
    }
}
