package offer;

/**
 * 反转链表
 * 
 * @author lu
 *
 */
public class Solution15 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub

	}
	
	public ListNode ReverseList(ListNode head) {
		return p(head, null);
    }
	
	public ListNode p(ListNode root, ListNode prev) {
		if(root==null) {
			return null;
		}
		if(root.next==null) {
			root.next = prev;
			return root;
		}
		ListNode node = p(root.next, root);
		root.next = prev;
		return node;
	}
	
	static public class ListNode {
	    int val;
	    ListNode next = null;

	    ListNode(int val) {
	        this.val = val;
	    }
	}
}
