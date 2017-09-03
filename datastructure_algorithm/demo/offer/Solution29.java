package offer;

import java.util.ArrayList;
import java.util.PriorityQueue;
import java.util.Queue;

/**
 * 
 * 最小的K个数
 * 
 * @author lu
 *
 */
public class Solution29 {

	public static void main(String[] args) {
		
	}
	
	public ArrayList<Integer> GetLeastNumbers_Solution(int [] input, int k) {
        Queue<Integer> q = new PriorityQueue<>();
        for(int num : input) {
        	q.add(num);
        }
        
        ArrayList<Integer> list = new ArrayList<>();
        for(int i=0; i<k; i++) {
        	if(q.isEmpty()) {
        		list.clear();
        		break;
        	}
        	list.add(q.poll());
        }
        
        return list;
    }
}
