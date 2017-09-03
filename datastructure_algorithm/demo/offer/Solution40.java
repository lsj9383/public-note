package offer;

import java.util.HashMap;
import java.util.Map.Entry;

/**
 * 
 * 一个整型数组里面有两个数字只出现一次，其他数字出现两次，找到两个只出现一次的数字
 * 
 * @author lu
 *
 */
public class Solution40 {

	public static void main(String[] args) {
		new Solution40().FindNumsAppearOnce(new int[] {2,4,3,6,3,2,5,5}, new int[1], new int[1]);
	}

	public void FindNumsAppearOnce(int [] array,int num1[] , int num2[]) {
        HashMap<Integer, Integer> map = new HashMap<>();
        for(int n : array) {
        	Integer cnt = map.get(n)==null?0:map.get(n);
        	map.put(n, cnt+1);
        }
        int[] ns = new int[2];
        int cnt = 0;
        for(Entry<Integer, Integer> entry : map.entrySet()) {
        	if(entry.getValue()==1) {
        		ns[cnt++] = entry.getKey();
        	}
        }
        num1[0] = ns[0];
        num2[0] = ns[1];
    }
}
