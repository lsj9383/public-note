package offer;

/**
 * 
 * 数值在排序数组中出现的次数
 * 
 * @author lu
 *
 */
public class Solution37 {

	public static void main(String[] args) {
		System.out.println(new Solution37().GetNumberOfK(new int[] {1}, 2));
	}

	public int GetNumberOfK(int [] array , int k) {
		int cnt = 0;
		int idx = find(array, k, 0, array.length-1);
		if(idx != -1) {
			int ptEnd = idx;
			int ptStart = idx;
			while(ptStart>=0 && array[ptStart]==k) ptStart--;
			ptStart++;
			while(ptEnd<array.length && array[ptEnd]==k) ptEnd++;
			ptEnd--;
			cnt = ptEnd-ptStart+1;
		}
		return cnt;
	       
    }
	
	int find(int[] array, int target, int start, int end) {
		if(start>end) {
			return -1;
		}
		if(start==end) {
			if(array[start] == target) {
				return start;
			}else {
				return -1;
			}
		}
		if(start+1==end) {
			if(array[start] == target) {
				return start;
			}else if(array[end] == target) {
				return end;
			}else {
				return -1;
			}
		}
		int center = (start+end)/2;
		int mid = array[center];
		if(target>mid) {
			return find(array, target, center, end);
		}else if(target<mid) {
			return find(array, target, start, center);
		}else {
			return center;
		}
	}
}
