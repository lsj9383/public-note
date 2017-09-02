package offer;

import java.util.Stack;

public class Solution5 {

	public static void main(String[] args) {
		Solution5 s5 = new Solution5();
		s5.push(1);
		s5.push(2);
		s5.push(3);
		System.out.println(s5.pop());
		System.out.println(s5.pop());
		System.out.println(s5.pop());
	}
	
	Stack<Integer> stack1 = new Stack<Integer>();
	Stack<Integer> stack2 = new Stack<Integer>();
	
	public void push(int node) {
	    stack1.push(node);
	}
	
	public int pop() {
		if(stack2.isEmpty()) {
			while(!stack1.isEmpty()) {
				stack2.push(stack1.pop());
			}
		}
		return stack2.pop();
	}
}
