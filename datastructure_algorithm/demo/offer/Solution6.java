package offer;

/**
 * 旋转数字的最小值
 * @author lu
 *
 */
public class Solution6 {

	public static void main(String[] args) {
		System.out.println(new Solution6().minNumberInRotateArray( new int[] {
				6501,6828,6963,7036,7422,7674,8146,8468,8704,8717,9170,9359,9719,9895,9896,9913,9962,154,293,334,492,1323,1479,1539,1727,1870,1943,2383,2392,2996,3282,3812,3903,4465,4605,4665,4772,4828,5142,5437,5448,5668,5706,5725,6300,6335
		}));
		System.out.println(new Solution6().find( new int[] {0, 1}, 1));
	}

	public int minNumberInRotateArray(int [] array) {
		if(array.length==0) {
			return 0;
		}
		int start = 0;
		int end = array.length-1;
		int mid = 0;
		while(array[start]>=array[end]) {
			if(end-start==1) {
				mid = end;
				break;
			}
			mid = (start+end)/2;
			if(array[mid]>=array[start]) {
				start = mid;
			}else if(array[mid]<=array[end]) {
				end = mid;
			}
		}
		return array[mid];
    }
	
	public int find(int[] array, int t) {
		int idx = -1;
		int start = 0;
		int end = array.length-1;
		while(start<=end) {
			int mid = (end+start)/2;
			if(t>array[mid]) {
				start = mid+1;
			}else if(t<array[mid]) {
				end = mid-1;
			}else {
				idx = mid;
				break;
			}
		}
		return idx;
	}
}
