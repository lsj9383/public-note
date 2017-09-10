package offer;

import java.util.ArrayList;

/**
 * 
 * 丑数
 * 
 * @author lu
 *
 */
public class Solution33 {

	public static void main(String[] args) {
		System.out.println(new Solution33().GetUglyNumber_Solution(1));
	}

	public int GetUglyNumber_Solution(int index) {
        UglyQueue ug = new UglyQueue();
        if(index==0) {
        	return 0;
        }
        for(int i=0; i<index-1; i++) {
        	ug.next();
        }
        return ug.last();
    }
	
	static class UglyQueue{
		ArrayList<Integer> list = new ArrayList<>();
		int t2 = 0;
		int t3 = 0;
		int t5 = 0;
		
		public UglyQueue() {
			list.add(1);
		}
		
		public int next() {
			int tmp2 = list.get(t2)*2;
			int tmp3 = list.get(t3)*3;
			int tmp5 = list.get(t5)*5;
			int min = tmp2<tmp3 ? tmp2 : tmp3;
			min = min<tmp5 ? min : tmp5;
			list.add(min);
			while(list.get(t2)*2<=min) {t2++;}
			while(list.get(t3)*3<=min) {t3++;}
			while(list.get(t5)*5<=min) {t5++;}
			return min;
		}
		
		public int last() {
			return list.get(list.size()-1);
		}
	}
}
