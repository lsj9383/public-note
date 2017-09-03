package offer;

/**
 * 
 * 连续子数组的最大和
 * 
 * @author lu
 *
 */
public class Solution30 {

	public static void main(String[] args) {
		
	}

	public int FindGreatestSumOfSubArray(int[] array) {
		int max = Integer.MIN_VALUE;
		int buf = 0;
        for(int i=0; i<array.length; i++) {
        	buf+=array[i];
        	if(buf>max) {
        		max = buf;
        	}
        	if(buf<0) {
        		buf=0;
        	}
        }
        return max;
    }
}
