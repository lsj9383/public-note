package offer;

import java.util.Comparator;
import java.util.Iterator;
import java.util.TreeSet;

/**
 * 
 * 找出二叉搜索树中第k大的节点
 * 
 * @author lu
 *
 */
public class Solution62 {

	public static void main(String[] args) {

	}
	
	TreeSet<TreeNode> q = new TreeSet<>(new Comparator<TreeNode>() {
		@Override
		public int compare(TreeNode o1, TreeNode o2) {
			return o1.val-o2.val;
		}
		
	});
	TreeNode KthNode(TreeNode pRoot, int k){
		if(k==0) {
        	return null;
        }
        scan(pRoot);
        if(k>q.size()) {
        	return null;
        }
        Iterator<TreeNode> it = q.iterator();        
        for(int i=0; i<k-1; i++) {
        	it.next();
        }
        return it.next();
    }
	
	void scan(TreeNode root) {
		if(root!=null) {
			q.add(root);
			scan(root.left);
			scan(root.right);
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
