package offer;

/**
 * 
 * 二叉树深度
 * 
 * @author lu
 *
 */
public class Solution38 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub

	}

	public int TreeDepth(TreeNode root) {
		if(root == null) {
			return 0;
		}
		int lftDepth = TreeDepth(root.left);
		int rgtDepth = TreeDepth(root.right);
		int depth = (lftDepth>rgtDepth ? lftDepth : rgtDepth) + 1;
		return depth;
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
