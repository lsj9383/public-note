package offer;

import java.util.ArrayList;
import java.util.List;

/**
 * 两个排序链表的合并
 * 
 * @author lu
 *
 */
public class Solution16 {

	public static void main(String[] args) {
		ListNode n1 = new ListNode(1);
		ListNode n3 = new ListNode(3);
		ListNode n5 = new ListNode(5);
		ListNode n2 = new ListNode(2);
		ListNode n4 = new ListNode(4);
		ListNode n6 = new ListNode(6);
		n1.next=n3;
		n3.next=n5;
		n2.next=n4;
		n4.next=n6;
		ListNode node = new Solution16().Merge(n1, n2);
		System.out.println(node);
	}

	public ListNode Merge(ListNode list1,ListNode list2) {
		if(list1==null) {
			return list2;
		}
		if(list2==null) {
			return list1;
		}
		ListNode p1 = list1;
		ListNode p2 = list2;
		List<ListNode> li = new ArrayList<>();
		while(true) {
			if(p1.val<p2.val) {
				li.add(new ListNode(p1.val));
				p1 = p1.next;
				if(p1==null) {
					break;
				}
			}else {
				li.add(new ListNode(p2.val));
				p2 = p2.next;
				if(p2==null) {
					break;
				}
			}
		}
		for(ListNode h=p1; h!=null; h=h.next) {
			li.add(new ListNode(p1.val));
		}
		
		for(ListNode h=p2; h!=null; h=h.next) {
			li.add(new ListNode(p2.val));
		}
		
		for(int i=0; i<li.size()-1; i++) {
			li.get(i).next=li.get(i+1);
		}
		
		return li.get(0);
	}
	
	static class ListNode {
	    int val;
	    ListNode next = null;

	    ListNode(int val) {
	        this.val = val;
	    }
	    
	    @Override
	    public String toString() {
	    	return val+"";
	    }
	}
}
