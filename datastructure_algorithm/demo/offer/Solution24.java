package offer;

import java.util.ArrayList;
import java.util.Stack;

/**
 * 
 * 二叉树中和为某一值的路径
 * 
 * @author lu
 *
 */
public class Solution24 {

	public static void main(String[] args) {
		
	}

	public ArrayList<ArrayList<Integer>> FindPath(TreeNode root,int target) {
		ArrayList<ArrayList<Integer>> paths = new ArrayList<>();
		Stack<Info> s = new Stack<>();
		
		s.push(new Info(root, root.val));
		while(!s.isEmpty()) {
			Info info = s.pop();
			if(info.cost == target) {
				
			}
			if(info.tn.left!=null) {
				s.push(new Info(info.tn.left, info.cost+info.tn.left.val));
			}
			
			if(info.tn.right!=null) {
				s.push(new Info(info.tn.right, info.cost+info.tn.right.val));
			}
		}
		return paths;
    }
	
	static class Info{
		TreeNode tn;
		int cost;
		
		public Info(TreeNode tn, int cost) {
			this.tn = tn;
			this.cost = cost;
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
