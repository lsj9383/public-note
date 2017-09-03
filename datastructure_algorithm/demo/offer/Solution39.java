package offer;

/**
 * 
 * 判断二叉树是否为平衡二叉树
 * 
 * @author lu
 *
 */
public class Solution39 {

	public static void main(String[] args) {
		
	}

	public boolean IsBalanced_Solution(TreeNode root) {
		return p(root).fg;
    }
	
	Info p(TreeNode root) {
		if(root == null) {
			return new Info(true, 0);
		}
		Info leftInfo = p(root.left);
		Info rightInfo = p(root.right);
		boolean fg = false;
		if(leftInfo.fg && rightInfo.fg) {
			if(Math.abs(leftInfo.depth-rightInfo.depth)<=1) {
				fg = true;
			}
		}
		int depth = (leftInfo.depth > rightInfo.depth ? leftInfo.depth : rightInfo.depth) + 1;
		return new Info(fg, depth);
	}
	
	static class Info{
		boolean fg;
		int depth;
		
		public Info(boolean fg, int depth) {
			this.fg = fg;
			this.depth = depth;
		}
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
