package offer;

/**
 * 
 * 对称的二叉树
 * 
 * 若一个二叉树和他的镜像二叉树相同，那么该二叉树就是对称的。
 * 
 * 假设函数boolean f(t1, t2) 用于判断t2 是否为 t1的镜像二叉树，那么f(root, root)会在root为root的镜像二叉树时返回true
 * 
 * t1 = A B(lft) C(rgt)
 * t2 = a c(lft) b(rgt)
 * 
 * 若t2是t1的镜像二叉树，那么A==a，b为B的镜像二叉树，c为C的镜像二叉树，形成递归
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
        return f(pRoot, pRoot);
    }
	
	boolean f(TreeNode t1, TreeNode t2) {
		if(t1==null && t2==null){
			return true;
		}
		if(t1!=null && t2!=null)
			return t1.val==t2.val && f(t1.left, t2.right) && f(t1.right, t2.left);
		else
			return false;
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
