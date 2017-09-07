package offer;

import java.util.ArrayList;

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

	ArrayList<ArrayList<Integer>> paths = new ArrayList<>();
	public ArrayList<ArrayList<Integer>> FindPath(TreeNode root,int target) {
		p(root, target, new ArrayList<Integer>(), 0);
		return paths;
    }
	
	void p(TreeNode root, int target, ArrayList<Integer> path, int val) {
		if(root == null) {
			return ;
		}
		val += root.val;
		path.add(root.val);
		
		if(root.left==null && root.right==null && val == target) {
			paths.add(new ArrayList<Integer>(path));
		}
		p(root.left, target, path, val);
		p(root.right, target, path, val);
		path.remove(path.size()-1);
		val-=root.val;
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
