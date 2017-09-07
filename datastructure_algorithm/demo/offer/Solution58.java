package offer;

/**
 * 
 * 对称的二叉树
 * 
 * @author lu
 *
 */
public class Solution58 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		System.out.println(new Solution58().isSymmetrical(
				new TreeNode(8, new TreeNode(6, new TreeNode(5),
												new TreeNode(7)),
								new TreeNode(6, new TreeNode(7),
												new TreeNode(5)))));
	}

	boolean isSymmetrical(TreeNode pRoot){
		String s1 = scan(pRoot);
		String s2 = scan(img(pRoot));
        return s1.equals(s2);
    }
	
	String scan(TreeNode root) {
		if(root==null) {
			return "";
		}else {
			return root.val+scan(root.left)+scan(root.right);
		}
	}
	
	TreeNode img(TreeNode tree) {
		if(tree.left!=null) {
			tree.right = img(tree.left);
		}
		if(tree.right!=null) {
			tree.left = img(tree.right);
		}
		return tree;
	}
	
	static class TreeNode {
	    int val = 0;
	    TreeNode left = null;
	    TreeNode right = null;

	    public TreeNode(int val) {
	        this.val = val;

	    }

	    public TreeNode(int val, TreeNode l, TreeNode r) {
	    	this.val = val;
	    	this.left = l;
	    	this.right = r;
	    }
	}
}
