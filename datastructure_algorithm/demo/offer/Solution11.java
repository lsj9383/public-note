package offer;

/**
 * 数字钟二进制1的个数
 * 
 * @author lu
 *
 */
public class Solution11 {

	public static void main(String[] args) {
		System.out.println(new Solution11().NumberOf1(10));
	}

	public int NumberOf1(int n) {
		int cnt = 0;
		while(n!=0) {
			cnt++;
			n = (n-1) & n;
		}
		return cnt;
    }
}
