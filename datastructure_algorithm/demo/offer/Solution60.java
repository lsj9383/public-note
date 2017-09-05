package offer;

import java.util.ArrayList;
import java.util.LinkedList;
import java.util.Queue;
import java.util.TreeMap;

/**
 * 
 *  把二叉树打印成多行
 * 
 * @author lu
 *
 */
public class Solution60 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub

	}
	
	ArrayList<ArrayList<Integer> > Print(TreeNode pRoot) {
	    ArrayList<ArrayList<Integer>> res = new ArrayList<>();
	    TreeMap<Integer, ArrayList<Integer>> tmpRes = new TreeMap<>();
	    Queue<Info> q = new LinkedList<>();
	    if(pRoot == null) {
	    	return res;
	    }
	    q.add(new Info(pRoot, 0));
	    while(!q.isEmpty()) {
	    	Info info = q.poll();
	    	if(info.node.left!=null) {
	    		q.add(new Info(info.node.left, info.depth+1));
	    	}
	    	if(info.node.right!=null) {
	    		q.add(new Info(info.node.right, info.depth+1));
	    	}
	    	ArrayList<Integer> list = tmpRes.get(info.depth);
	    	if(list == null) {
	    		list = new ArrayList<>();
	    		tmpRes.put(info.depth, list);
	    	}
	    	list.add(info.node.val);
	    }
	    for(Integer k : tmpRes.keySet()) {
	    	res.add(tmpRes.get(k));
	    }
	    return res;
    }
	
	static class Info{
		TreeNode node;
		int depth;
		
		public Info(TreeNode node, int depth){
			this.node = node;
			this.depth = depth;
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
