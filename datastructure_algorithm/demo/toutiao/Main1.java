package toutiao;

/**
 * 
 * 快排
 * 
 * @author lu
 *
 */
public class Main1 {

	public static void main(String[] args) {
		int[] array = new int[] {4, 3, 2, 4, 5, 9, 6 ,23, 43};
		sort(array);
		for(int n:array) {
			System.out.print(n+" ");
		}
	}
	
	static void sort(int[] array) {
		p(array, 0, array.length-1);
	}
	
	static void p(int[] array, int start, int end) {
		if(start>=end) {
			return ;
		}
		int idx = parition(array, start, end);
		p(array, start, idx-1);
		p(array, idx+1, end);
	}
	
	static int parition(int[] array, int start, int end) {
		swap(array, start, end);
		int small = start - 1;
		for(int i=start; i<end; i++) {
			if(array[i]<array[end]) {
				small++;
				swap(array, i, small);
			}
		}
		swap(array, ++small, end);
		return small;
	}

	static void swap(int[] array, int idx1, int idx2) {
		int tmp = array[idx1];
		array[idx1] = array[idx2];
		array[idx2] = tmp;
	}
}
