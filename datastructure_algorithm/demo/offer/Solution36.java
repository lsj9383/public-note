package offer;

import java.util.Stack;

/**
 * 
 * 两个链表的第一个公共节点
 * 
 * @author lu
 *
 */
public class Solution36 {

	public static void main(String[] args) {
		
	}

	public ListNode FindFirstCommonNode(ListNode pHead1, ListNode pHead2) {
		Stack<ListNode> s1 = new Stack<>();
		Stack<ListNode> s2 = new Stack<>();
		for(ListNode c=pHead1; c!=null; c=c.next) {
			s1.push(c);
		}
		
		for(ListNode c=pHead2; c!=null; c=c.next) {
			s2.push(c);
		}
		
		ListNode commonNode = null;
		while(!s1.isEmpty() && !s2.isEmpty()) {
			ListNode n1 = s1.pop();
			ListNode n2 = s2.pop();
			if(n1.val != n2.val) {
				break;
			}
			commonNode = n1;
		}
		return commonNode;
    }
	
	static class ListNode {
	    int val;
	    ListNode next = null;

	    ListNode(int val) {
	        this.val = val;
	    }
	}
}
