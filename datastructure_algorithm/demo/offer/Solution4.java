package offer;

public class Solution4 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		TreeNode root = new Solution4().reConstructBinaryTree(
				new int[] {1,2,4,7,3,5,6,8}, 
				new int[] {4,7,2,1,5,3,8,6});
		System.out.println(root);
	}

	public TreeNode reConstructBinaryTree(int [] pre,int [] in) {
		return p(pre, in, 0, pre.length, 0, in.length-1);
    }
	
	TreeNode p(int [] pre,int [] in, int pres, int pree, int ins, int ine) {
		int rootVal = pre[pres];
		int inLeftStart = -1;
		int inLeftEnd = -1;
		int inRightStart = -1;
		int inRightEnd = -1;
		int preLeftStart = -1;
		int preLeftEnd = -1;
		int preRightStart = -1;
		int preRightEnd = -1;
		if(ine<ins) {
			return null;
		}
		for(int idx=ins; idx<=ine; idx++) {
			if(in[idx]==rootVal) {
				inLeftStart = ins;
				inLeftEnd = idx-1;
				inRightStart = idx+1;
				inRightEnd = ine;
				preLeftStart = pres; 
				break;
			}
		}
		TreeNode root = new TreeNode(rootVal);
//		root.left = p(pre, in, leftStart, leftEnd);
//		root.right = p(pre, in, rightStart, rightEnd);
		return root;
	}
	
	static class TreeNode {
	     int val;
	     TreeNode left;
	     TreeNode right;
	     TreeNode(int x) { val = x; }
	}
}

