package offer;

/**
 * 
 * 孩子们的游戏
 * 
 * @author lu
 *
 */
public class Solution45 {

	public static void main(String[] args) {
		System.out.println(new Solution45().LastRemaining_Solution(5, 3));
	}

	public int LastRemaining_Solution(int n, int m) {
		if(n==0 || m==0) {
			return -1;
		}
        int[] ids = new int[n];
        for(int i=0; i<n; i++) {
        	ids[i]=i;
        }
        int remain = n;
        int c = 0;
        while(remain>1) {
        	for(int M=0; M<m-1; M++) {
        		do {c = (c+1)%n;}while(ids[c]==-1);
        	}
        	ids[c]=-1;
        	remain--;
        	do {c = (c+1)%n;}while(ids[c]==-1);
        }
        return c;
    }
}
