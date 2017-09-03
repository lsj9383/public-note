package offer;

/**
 * 
 * 数组中超过半数的数
 * 
 * @author lu
 *
 */
public class Solution28 {

	public static void main(String[] args) {
		System.out.println(new Solution28().MoreThanHalfNum_Solution(new int[] {1,2,3,2,4,2,5,2,3}));
	}
	
	public int MoreThanHalfNum_Solution(int [] array) {
		Integer key = null;
		Integer cnt = null;
		for(int num : array) {
			if(key == null) {
				key = num;
				cnt = 1;
				continue ;
			}
			if(key == num) {
				cnt++;
			}else {
				if(cnt==0) {
					key = num;
				}else {
					cnt--;
				}
			}
		}
		if(key==null || cnt==0) {
			key=0;
		}
		return key;
	}
}
