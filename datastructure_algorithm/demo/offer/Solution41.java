package offer;

import java.util.ArrayList;
import java.util.LinkedList;
import java.util.Queue;

/**
 * 
 * 和为S的连续正整数的和
 * 
 * @author lu
 *
 */
public class Solution41 {

	public static void main(String[] args) {
		System.out.println(new Solution41().FindContinuousSequence(9));
	}
	
	public ArrayList<ArrayList<Integer> > FindContinuousSequence(int sum) {
		ArrayList<ArrayList<Integer>> result = new ArrayList<>();
		SumQueue sq = new SumQueue();
		for(int i=1; i<=sum/2+1; i++) {
			sq.add(i);
			while(sq.sum()>sum) {
				sq.remove();
			}
			if(sq.size()>1 && sq.sum()==sum) {
				result.add(sq.toList());
			}
		}
		return result;
    }

	class SumQueue{
		int sum = 0;
		Queue<Integer> q = new LinkedList<>();
		public void add(Integer n) {
			q.add(n);
			sum+=n;
		}
		
		public Integer remove() {
			if(!q.isEmpty()) {
				Integer n = q.remove();
				sum-=n;
				return n;
			}else {
				return null;
			}
		}
		
		public int sum() {
			return sum;
		}
		
		public int size() {
			return q.size();
		}
		
		public ArrayList<Integer> toList(){
			return new ArrayList<>(q);
		}
	}
}
