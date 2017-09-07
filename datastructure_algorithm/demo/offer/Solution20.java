package offer;

import java.util.Stack;

/**
 * 
 * 包含min函数的栈
 * 
 * @author lu
 *
 */
public class Solution20 {

	public static void main(String[] args) {
		Solution20 so = new Solution20();
		int[] nums = new int[] {32, 94, 4, 3, 13, 34, 32, 53};
		for(int i:nums) {
			so.push(i);
		}
		System.out.println(so.min());
		so.pop();so.pop();so.pop();so.pop();so.pop();so.pop();
		System.out.println(so.min());
	}

	Stack<Integer> s = new Stack<>();
	Stack<Integer> min = new Stack<>();
	
	public void push(int node) {
		if(min.isEmpty() || node < min.peek()) {
			min.push(node);
		}
        s.push(node);
    }
    
    public void pop() {
    	if(s.peek() == min.peek()) {
    		min.pop();
    	}
        s.pop();
    }
    
    public int top() {
        return s.peek();
    }
    
    public int min() {
        return min.peek();
    }
}
