package offer;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.LinkedList;

/**
 * 
 * 按之字形打印二叉树
 * 
 * 
 * @author lu
 *
 */
public class Solution59 {

	public static void main(String[] args) {
	}

	public ArrayList<ArrayList<Integer>> Print(TreeNode pRoot) {
		boolean fg = true;
		ArrayList<ArrayList<Integer> > ret = new ArrayList<>();
		LinkedList<TreeNode> q = new LinkedList<>();
		if(pRoot==null) {
			return ret;
		}
		q.add(null);
		q.add(pRoot);
		while(q.size()!=1) {
			TreeNode n = q.removeFirst();
			if(n == null) {
				Iterator<TreeNode> it = null;
				if(fg) {
					it = q.iterator();
				}else {
					it = q.descendingIterator();
				}
				fg = !fg;
				ArrayList<Integer> list = new ArrayList<>();
				while(it.hasNext()) {
					list.add(it.next().val);
				}
				ret.add(list);
				q.add(null);
			}else {
				if(n.left != null) {
					q.add(n.left);
				}
				if(n.right != null) {
					q.add(n.right);
				}
			}
		}
		return ret;
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
