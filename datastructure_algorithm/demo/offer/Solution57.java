package offer;

/**
 * 
 * 二叉树的下一个节点
 * 
 * @author lu
 *
 */
public class Solution57 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub

	}

	public TreeLinkNode GetNext(TreeLinkNode pNode){
		if(pNode==null) {return null;}
		if(pNode.right!=null) {
			TreeLinkNode resNode = pNode.right;
			while(resNode.left!=null) {
				resNode = resNode.left;
			}
			return resNode;
		}
		if(pNode.next!=null) {
			TreeLinkNode resNode = pNode;
			while(resNode.next!=null && resNode.next.left != resNode) {
				resNode = resNode.next;
			}
			return resNode.next;
		}
		return null;
    }
	
	static class TreeLinkNode {
	    int val;
	    TreeLinkNode left = null;
	    TreeLinkNode right = null;
	    TreeLinkNode next = null;

	    TreeLinkNode(int val) {
	        this.val = val;
	    }
	}
}
