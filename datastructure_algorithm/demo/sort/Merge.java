package demo.sort;

public class Merge extends Sort{

	private int[] tmp;
	
	@Override
	public void process(int[] ar) {
		tmp = new int[ar.length];
		sort(ar, 0, ar.length-1);
	}
	
	private void sort(int[] ar, int start, int end) {
		int mid = (start+end)/2;
		
		if(start>=end) {
			return ;
		}
		
		sort(ar, start, mid);
		sort(ar, mid+1, end);
		merge(ar, new int[] {start, mid}, new int[] {mid+1, end});
	}
	
	private void merge(int[] ar, int[] range1, int[] range2) {
		int end1 = range1[1];
		int end2 = range2[1];
		
		
		int idx = 0;
		int p1 = 0;
		int p2 = 0;
		for(p1 = range1[0], p2 = range2[0], idx = 0; p1<=end1 &&p2<=end2;) {
			if(ar[p1]<ar[p2]) {
				tmp[idx++] = ar[p1++];
			}else if(ar[p1]>ar[p2]) {
				tmp[idx++] = ar[p2++];
			}else {
				tmp[idx++] = ar[p1++];
				tmp[idx++] = ar[p2++];
			}
		}
		
		while(p1<=end1) {
			tmp[idx++]=ar[p1++];
		}
		
		while(p2<=end2) {
			tmp[idx++]=ar[p2++];
		}
		
		while(idx>0) {
			ar[range1[0]+idx-1] = tmp[idx-1];
			idx--;
		}
	}
}
