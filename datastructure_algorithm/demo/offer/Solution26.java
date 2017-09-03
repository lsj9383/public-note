package offer;

/**
 * 
 * 二叉搜索树转双向链表
 * 
 * @author lu
 *
 */
public class Solution26 {

	public static void main(String[] args) {
		TreeNode node = new Solution26().Convert(
				new TreeNode(5, 
						new TreeNode(3, 
								new TreeNode(2, null, null), 
								new TreeNode(4, null, null)),
						new TreeNode(8,
								new TreeNode(6, null, null),
								new TreeNode(10, null, null))));
		System.out.println(node);
	}

	public TreeNode Convert(TreeNode pRootOfTree) {
		if(pRootOfTree==null) {
			return null;
		}
		if(pRootOfTree.left!=null) {
			TreeNode node = Convert(pRootOfTree.left);
			while(node.right!=null) {
				node=node.right;
			}
			node.right = pRootOfTree;
			pRootOfTree.left = node;
		}
		
		if(pRootOfTree.right!=null) {
			TreeNode node = Convert(pRootOfTree.right);
			node.left = pRootOfTree;
			pRootOfTree.right = node;
		}
		
		// 返回最小的节点
		TreeNode p = pRootOfTree;
		while(p.left!=null) {
			p = p.left;
		}
		return p;
	}
	
	static class TreeNode {
	    int val = 0;
	    TreeNode left = null;
	    TreeNode right = null;

	    public TreeNode(int val) {
	        this.val = val;
	    }
	    
	    public TreeNode(int val, TreeNode left, TreeNode right) {
	        this.val = val;
	        this.left = left;
	        this.right = right;
	    }

	}
}
