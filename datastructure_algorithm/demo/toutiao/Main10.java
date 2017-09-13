package toutiao;

import java.util.Iterator;
import java.util.LinkedList;

/**
 * 
 * S形BFS二叉树
 * 
 * @author lu
 *
 */
public class Main10 {

	public static void main(String[] args) {

	}
	
	static public String p(TreeNode root) {
		StringBuilder sb = new StringBuilder();
		LinkedList<TreeNode> q = new LinkedList<>();
		boolean fg = true;
		q.add(null);
		q.add(root);
		while(!q.isEmpty()) {
			TreeNode node = q.removeFirst();
			if(node==null) {
				Iterator<TreeNode> it = fg?q.iterator():q.descendingIterator();
				while(it.hasNext()) {
					sb.append(it.next().val);
				}
				sb.append("\n");
				continue;
			}
			if(node.lft!=null) {
				q.add(node.lft);
			}
			if(node.rgt!=null) {
				q.add(node.rgt);
			}
		}
		return sb.toString();
	}

	static class TreeNode{
		int val;
		TreeNode lft;
		TreeNode rgt;
	}
}
