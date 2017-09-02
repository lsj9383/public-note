package offer;

/**
 * 
 * 二叉树的镜像
 * 
 * @author lu
 *
 */
public class Solution18 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub

	}
	
	public void Mirror(TreeNode root) {
        if(root == null) {
        	return ;
        }
        TreeNode tmp = root.left;
        root.left = root.right;
        root.right = tmp;
        Mirror(root.left);
        Mirror(root.right);
    }

	static class TreeNode {
	    int val = 0;
	    TreeNode left = null;
	    TreeNode right = null;

	    public TreeNode(int val) {
	        this.val = val;

	    }

	}
}
