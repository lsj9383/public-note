package offer;

/**
 * 链表中倒数第k个节点
 * 
 * @author lu
 *
 */
public class Solution14 {

	public static void main(String[] args) {
		System.out.print(new Solution14().FindKthToTail(
				new ListNode(1, new ListNode(2, new ListNode(3, new ListNode(4, new ListNode(5))))), 5));
	}

	public ListNode FindKthToTail(ListNode head,int k) {
		ListNode p1 = head;
		ListNode p2 = head;
		int i=0;
		for(i=0; i<k && p2!=null; i++, p2=p2.next) ;
		if(p2==null && i!=k) {
			return null;
		}
		for(;p2!=null;p2=p2.next, p1=p1.next);
		return p1;
	}
	
	static class ListNode {
	    int val;
	    ListNode next = null;

	    ListNode(int val) {
	        this.val = val;
	    }
	    
	    ListNode(int val, ListNode node){
	    	this.val = val;
	    	this.next = node;
	    }
	    
	    @Override
	    public String toString() {
	    	return val+"";
	    }
	}
}