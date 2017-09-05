package offer;

/**
 * 
 * 构建乘积数组
 * 
 * @author lu
 *
 */
public class Solution50 {

	public static void main(String[] args) {
		new Solution50().multiply(new int[] {1,2,0,4,5});
	}
	
	public int[] multiply(int[] A) {
		int s = 1;
		int zeroSize = 0;
		int zeroIdx = -1;
		for(int i=0; i<A.length; i++) {
			int a = A[i];
			if(a!=0) {
				s*=a;
			}else {
				zeroSize++;
				zeroIdx=i;
			}
		}
		int[] B = new int[A.length];
		if(zeroSize>=2) {
			return B;
		}
		if(zeroSize==1) {
			B[zeroIdx] = s;
			return B;
		}
		for(int i=0; i<A.length; i++) {
			B[i] = s/A[i];
		}
		return B;
    }
}
