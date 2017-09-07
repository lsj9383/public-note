package offer;

import java.util.Arrays;
import java.util.Comparator;

/**
 * 
 * 把数组排成最小的数
 * 
 * @author lu
 *
 */
public class Solution32 {

	public static void main(String[] args) {
		
	}

	public String PrintMinNumber(int [] numbers) {
		String[] strNums = new String[numbers.length];
		for(int i=0; i<numbers.length; i++) {
			strNums[i] = numbers[i]+"";
		}
		Arrays.sort(strNums, new Com());
		StringBuilder res = new StringBuilder();
		for(String n:strNums) {
			res.append(n);
		}
		return res.toString();
    }
	
	static class Com implements Comparator<String>{
		@Override
		public int compare(String o1, String o2) {
			String s1=o1+o2;
			String s2=o2+o1;
			return s1.compareTo(s2);
		}
	}
}
