package toutiao;

/**
 * 
 * 二分查找
 * 
 * @author lu
 *
 */
public class Main4 {

	public static void main(String[] args) {
		int[] array = new int[] {2, 3, 4, 4, 5, 6, 9, 23, 43 };
		System.out.println(find(array, 43));
	}
	
	static int find(int[] array, int target) {
		return p(array, target, 0, array.length);
	}

	static int p(int[] array, int target, int start, int end) {
		int mid = (start+end)/2;
		int center = array[mid];
		
		if(start>=end) {
			return -1;
		}
		
		if(center>target) {
			return p(array, target, start, mid-1);
		}else if(center<target) {
			return p(array, target, mid+1, end);
		}else {
			return mid;
		}
	}
}
