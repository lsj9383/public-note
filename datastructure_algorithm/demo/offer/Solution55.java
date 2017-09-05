package offer;

import java.util.HashMap;

public class Solution55 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub

	}

	public ListNode EntryNodeOfLoop(ListNode pHead){
        HashMap<ListNode, Integer> map = new HashMap<>();
        for(ListNode node=pHead; node!=null; node=node.next) {
        	Integer cnt = map.get(node);
        	if(cnt==null) {
        		cnt=0;
        	}
        	if(cnt==1) {
        		return node;
        	}
        	map.put(node, cnt+1);
        }
		return null;
    }
	
	static class ListNode {
	    int val;
	    ListNode next = null;

	    ListNode(int val) {
	        this.val = val;
	    }
	}
}
