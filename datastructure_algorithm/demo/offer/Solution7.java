package offer;

/**
 * 斐波那契数列
 * 
 * @author lu
 *
 */
public class Solution7 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub

	}

	public int Fibonacci(int n) {
		int[] array = new int[n+2];
		array[0] = 0;
		array[1] = 1;
		for(int i=2; i<=n; i++) {
			array[i] = array[i-1] + array[i-2];
		}
		return array[n];
    }
}
