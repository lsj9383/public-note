package offer;

/**
 * 
 * 序列化二叉树
 * 
 * @author lu
 *
 */
public class Solution61 {

	public static void main(String[] args) {
		
	}
	
	String Serialize(TreeNode root) {
		if(root==null) {
			return "null";
		}
		return preScan(root)+"#"+inScan(root);
	}
	
	TreeNode Deserialize(String str) {
		if(str.equals("null")) {
			return null;
		}
		String[] parts = str.split("#");
		String[] preParts = parts[0].split(",");
		String[] inParts = parts[1].split(",");
		int[] pre = new int[preParts.length];
		int[] in = new int[preParts.length];
		for(int i=0; i<preParts.length; i++) {
			pre[i] = Integer.parseInt(preParts[i]);
			in[i] = Integer.parseInt(inParts[i]);
		}
		return reConstructure(pre, in);
	}
	
	TreeNode reConstructure(int[] pre, int[] in) {
		return p(pre, in, new int[] {0, pre.length-1}, new int[]{0, in.length-1}) ;
	}
	
	TreeNode p(int[] pre, int[] in, int[] preRange, int[] inRange) {
		if(preRange[0]==preRange[1]) {
			return new TreeNode(pre[preRange[0]]);
		}
		int root = pre[preRange[0]];
		int rootInIdx = -1;
		int cnt=0;
		for(int i=inRange[0]; i<in.length; i++) {
			if(in[i]==root) {
				rootInIdx=i;
				break;
			}
			cnt++;
		}
		TreeNode tn = new TreeNode(root);
		if(preRange[0]+1<=preRange[0]+cnt) {
			tn.left = p(pre, in, new int[] {preRange[0]+1, preRange[0]+cnt}, new int[] {inRange[0], rootInIdx-1});	
		}
		if(preRange[0]+cnt+1<=preRange[1]) {
			tn.right = p(pre, in, new int[] {preRange[0]+cnt+1, preRange[1]}, new int[] {rootInIdx+1, inRange[1]});	
		}
		return tn;
	}
	
	String preScan(TreeNode root) {
		StringBuilder sb = new StringBuilder();
		ps(root, sb);
		sb.deleteCharAt(sb.length()-1);
		return sb.toString();
	}
	
	void ps(TreeNode root, StringBuilder sb) {
		if(root==null) {
			return ;
		}
		sb.append(root.val).append(",");
		ps(root.left, sb);
		ps(root.right, sb);
	}
	
	String inScan(TreeNode root) {
		StringBuilder sb = new StringBuilder();
		is(root, sb);
		sb.deleteCharAt(sb.length()-1);
		return sb.toString();
	}
	
	void is(TreeNode root, StringBuilder sb) {
		if(root==null) {
			return ;
		}
		is(root.left, sb);
		sb.append(root.val).append(",");
		is(root.right, sb);
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
