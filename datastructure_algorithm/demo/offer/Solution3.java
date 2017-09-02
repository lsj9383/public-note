package offer;

import java.util.ArrayList;

public class Solution3 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
	}
	
	public ArrayList<Integer> printListFromTailToHead(ListNode listNode) {
		ArrayList<Integer> list = new ArrayList<>();
        if(listNode == null) {
        	return list;
        }else {
        	p(listNode, list);
        }
        return list;
    }
	
	void p(ListNode node, ArrayList<Integer> list) {
		if(node != null) {
			p(node.next, list);
			list.add(node.val);
		}
	}
	
	static class ListNode {
	    int val;
	    ListNode next = null;

	    ListNode(int val) {
	        this.val = val;
	    }
	}
}