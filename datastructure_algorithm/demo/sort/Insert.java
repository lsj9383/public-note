package demo.sort;

public class Insert extends Sort{

	@Override
	public void process(int[] ar) {
		for(int i=1; i<ar.length; i++) {
			int val = ar[i];
			int j=0;
			for(j=i-1; j>=0; j--) {
				if(ar[j]>val) {			// 比新值的时候，将元素往后面移动一格
					ar[j+1]=ar[j];
				}else {					// 比新值小的时候停止，新值将放在该元素的后面
					break;
				}
			}
			ar[j+1]=val;				// 放在找到的j的后面
		}
	}

}
