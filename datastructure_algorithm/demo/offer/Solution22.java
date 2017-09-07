package offer;

import java.util.ArrayDeque;
import java.util.ArrayList;
import java.util.Queue;

/**
 * 
 * 从上往下打印二叉树
 * 
 * @author lu
 *
 */
public class Solution22 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub

	}
	
	public ArrayList<Integer> PrintFromTopToBottom(TreeNode root) {
        ArrayList<Integer> list = new ArrayList<>();
        Queue<TreeNode> q = new ArrayDeque<>();
        
        if(root==null) {
        	return list;
        }
        
        q.add(root);
        while(!q.isEmpty()) {
        	TreeNode node = q.poll();
        	list.add(node.val);
        	if(node.left!=null) {
        		q.add(node.left);
        	}
        	if(node.right!=null) {
        		q.add(node.right);
        	}
        }
        
        return list;
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
