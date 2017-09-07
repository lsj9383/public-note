package offer;

public class Solution4 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		TreeNode root = new Solution4().reConstructBinaryTree(
				new int[] {1,2,3,4,5,6,7}, 
				new int[] {3,2,4,1,6,5,7});
		System.out.println(root);
	}

	int[] pre;
	int[] in;
	public TreeNode reConstructBinaryTree(int [] pre,int [] in) {
		this.pre = pre;
		this.in = in;
		return p(0, pre.length-1, 0, in.length-1);
    }
	
	TreeNode p(int preStart, int preEnd, int inStart, int inEnd) {
		int rootVal = pre[preStart];
		TreeNode root = new TreeNode(rootVal);
		if(preStart==preEnd) {
			return root;
		}
		
		int inIdx = 0;
		int cnt=0;
		for(inIdx=inStart; inIdx<=inEnd; inIdx++) {
			if(in[inIdx]==rootVal) {
				break;
			}
			cnt++;
		}
		int pend = preStart+cnt;
		int pstart = pend+1;
		if(inStart<=inIdx-1 && preStart+1<=pend) {
			root.left  = p(preStart+1, pend, inStart, inIdx-1);	
		}
		if(inIdx+1<=inEnd && pstart<=preEnd) {
			root.right = p(pstart, preEnd, inIdx+1, inEnd);	
		}
		return root;
	}
	
	static class TreeNode {
	     int val;
	     TreeNode left;
	     TreeNode right;
	     TreeNode(int x) { val = x; }
	}
}

