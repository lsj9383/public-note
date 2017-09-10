package offer;

/**
 * 
 * 求1+2+3+...+n
 * 不能用乘除法 for while if else switch A?B:C 
 * 
 * @author lu
 *
 */
public class Solution46 {
	public static void main(String[] args) {
		
	}
	
	public int Sum_Solution(int n) {
		int sum = n;
		boolean ans = (n>0)&&((sum+=Sum_Solution(n-1))>0);
		ans = !ans;
        return sum;
    }
}
