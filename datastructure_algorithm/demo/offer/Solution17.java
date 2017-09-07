package offer;

public class Solution17 {

	public static void main(String[] args) {
		System.out.println(new Solution17().HasSubtree(
			 new TreeNode(8,
						null, 
						new TreeNode(9, new TreeNode(3), new TreeNode(2))),
			 null));
	}

	public boolean HasSubtree(TreeNode root1,TreeNode root2) {
		if(root1 == null && root2!=null || root1 != null && root2==null || root1 == null && root2==null) {
			return false;
		}
		if(isSame(root1, root2)) {
			return true;
		}else {
			return HasSubtree(root1.left, root2) || HasSubtree(root1.right, root2);
		}
    }
	
	 public boolean isSame(TreeNode n1, TreeNode n2) {
		 if(n1==null&&n2!=null || n1!=null&&n2==null) {
			 return false;
		 }
		 if(n1==null&&n2==null) {
			 return true;
		 }
		 if(n1.val == n2.val) {
			 if(n2.left == null && n2.right==null) {
				 return true;
			 }
			 return isSame(n1.left, n2.left) && isSame(n1.right, n2.right);
		 }else {
			 return false;
		 }
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
