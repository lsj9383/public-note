package toutiao;

/**
 * 
 * 给定一个完全二叉树的根节点指针，返回所有节点的个数。
 * 
 * 判断该树是否为满二叉树，若是满二叉树则直接返回，否则计算左右子节点再返回，左右子节点的计算是一个递归。
 * 判断是否为满二叉树的方式是root->left->...->left和root->right->...->right是否相等，相等就是满二叉树，否则为非满二叉树
 * 
 * @author lu
 *
 */
public class Main8 {

	public static void main(String[] args) {
	}
	
	static int count(TreeNode root) {
		if(root == null) {
			return 0;
		}
		int leftDeep = 1;
		int rightDeep= 1;
		for(TreeNode n=root; n!=null; n=n.left, leftDeep++) {}
		for(TreeNode n=root; n!=null; n=n.right, rightDeep++) {}
		if(leftDeep==rightDeep) {
			return (int) (Math.pow(2, leftDeep)-1);
		}else {
			return 1+count(root.left)+count(root.right);
		}
	}

	static class TreeNode{
		TreeNode left;
		TreeNode right;
	}
}
