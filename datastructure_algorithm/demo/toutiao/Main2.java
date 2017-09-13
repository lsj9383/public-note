package toutiao;

/**
 * 
 * 归并排序
 * 
 * @author lu
 *
 */
public class Main2 {

	public static void main(String[] args) {
		int[] array = new int[] {4, 3, 2, 4, 5, 9, 6 ,23, 43};
		sort(array);
		for(int n:array) {
			System.out.print(n+" ");
		}
	}
	
	static void sort(int[] array) {
		int[] tmp = new int[array.length];
		p(array, 0, array.length-1, tmp);
	}
	
	static void p(int[] array, int start, int end, int[] tmp) {
		if(start>=end) {
			return ;
		}
		
		int mid = (start+end)/2;
		p(array, start, mid, tmp);
		p(array, mid+1, end, tmp);
		merge(array, start, end, tmp);
	}

	static void merge(int[] array, int start, int end, int[] tmp) {
		int mid = (start+end)/2;
		int pt1 = start;
		int pt2 = mid+1;
		
		int idx = 0;
		while(pt1<=mid && pt2<=end) {
			if(array[pt1]<array[pt2]) {
				tmp[idx++] = array[pt1++];
			}else{
				tmp[idx++] = array[pt2++];
			}
		}
		
		while(pt1<=mid) {tmp[idx++]=array[pt1++];}
		while(pt2<=mid) {tmp[idx++]=array[pt2++];}
		
		for(int i=0; i<idx; i++) {
			array[start+i] = tmp[i];
		}
	}
}
