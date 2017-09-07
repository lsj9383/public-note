package offer;

import java.util.Stack;

/**
 * 栈的压入、弹出序列
 * 
 * @author lu
 *
 */
public class Solution21 {

	public static void main(String[] args) {
		System.out.println(new Solution21().IsPopOrder(new int[] {1,2,3,4,5}, new int[] {4,5,3,1,2}));
	}

	public boolean IsPopOrder(int [] pushA,int [] popA) {
	     Stack<Integer> s = new Stack<>();
	     int cnt = 0;
	     for(int i=0; i<popA.length; i++) {
	    	 while(s.isEmpty() || s.peek()!=popA[i]) {
	    		 if(cnt==pushA.length) {
	    			 return false;
	    		 }
	    		 s.push(pushA[cnt++]);
	    	 }
	    	 s.pop();
	     }
	     return true;
    }
}
