package offer;

import java.util.LinkedHashMap;
import java.util.Map.Entry;

/**
 * 
 * 删除链表中的重复节点
 * 
 * @author lu
 *
 */
public class Solution56 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		new Solution56().deleteDuplication(new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(3, new ListNode(4, new ListNode(4, new ListNode(5, null))))))));
	}
	
	public ListNode deleteDuplication(ListNode pHead){
		LinkedHashMap<Integer, Integer> map = new LinkedHashMap<>();
		for(ListNode n = pHead; n!=null; n=n.next) {
			Integer cnt = map.get(n.val);
			if(cnt==null) {
				cnt=0;
			}
			map.put(n.val, cnt+1);
		}
		ListNode head = null;
		ListNode node = null;
		for(Entry<Integer, Integer> entry : map.entrySet()) {
			if(entry.getValue()==1) {
				if(head == null) {
					head = new ListNode(entry.getKey());
					node = head;
				}else {
					node.next = new ListNode(entry.getKey());
					node = node.next;
					node.next = null;
				}
			}
		}
		
		return head;
    }

	 static class ListNode {
		    int val;
		    ListNode next = null;

		    ListNode(int val) {
		        this.val = val;
		    }
		    
		    ListNode(int val, ListNode nxt) {
		        this.val = val;
		        next = nxt;
		    }
		    
		    @Override
		    public String toString() {
		    	return val+"";
		    }
		}
}
