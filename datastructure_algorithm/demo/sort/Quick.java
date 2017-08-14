package demo.sort;

public class Quick extends Sort{

	@Override
	public void process(int[] ar) {
		_p(ar, 0, ar.length-1);
	}
	
	private void _p(int[] ar, int left, int right) {
		if(left>=right) {
			return ;
		}
		
		int mid = median3(ar, left, right);
		
		int i = left;
		int j = right-1;
		while(true) {
			while(ar[++i] < mid);
			while(ar[--j] > mid);
			if(i<j) {
				swap(i, j, ar);
			}else {
				break;
			}
		}
		swap(i, right-1, ar);
		
		_p(ar, left, i-1);
		_p(ar, i+1, right);
	}
	
	// 先将最小值放在左边，再将右边的两个元素进行判断和交换
	private int median3(int[] ar, int left, int right) {
		int center = (left+right)/2;
		if(ar[center]<ar[left]) {
			swap(center, left, ar);
		}
		
		if(ar[right]<ar[left]) {
			swap(right, left, ar);
		}
		
		if(ar[right]<ar[center]) {
			swap(right, center, ar);
		}
		
		swap(center, right-1, ar);			//将中间元素放在倒数第二个位置
		return ar[right-1];
	}
}
