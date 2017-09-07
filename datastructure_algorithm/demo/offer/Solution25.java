package offer;

import java.util.HashMap;

/**
 * 
 * 复杂链表的复制
 * 
 * @author lu
 *
 */
public class Solution25 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		new Solution25().Clone(new RandomListNode(0, new RandomListNode(1, new RandomListNode(2))));
	}

	public RandomListNode Clone(RandomListNode pHead){
		HashMap<RandomListNode,RandomListNode> map = new HashMap<>();
		RandomListNode nHead = new RandomListNode(0);
		for(RandomListNode p=pHead, n=nHead; p!=null; n=n.next, p=p.next) {
			n.next = new RandomListNode(p.label);
			map.put(p, n.next);
		}
		nHead = nHead.next;
		for(RandomListNode p=pHead; p!=null; p=p.next) {
			RandomListNode n = map.get(p);
			RandomListNode pRandom = p.random;
			RandomListNode nRandom = map.get(pRandom);
			n.random = nRandom;
		}
		return nHead;
    }
	
	static class RandomListNode {
	    int label;
	    RandomListNode next = null;
	    RandomListNode random = null;

	    RandomListNode(int label) {
	        this.label = label;
	    }
	    
	    RandomListNode(int label, RandomListNode next) {
	        this.label = label;
	        this.next = next;
	    }
	}
}
