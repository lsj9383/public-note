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
		// TODO Auto-generated method stub

	}

	Stack<Integer> s = new Stack<>();
	Stack<Integer> min = new Stack<>();
	
	public void push(int node) {
		if(node < min.peek()) {
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
