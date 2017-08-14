package demo.sort;

public class Bubble extends Sort{
	@Override
	public void process(int[] ar) {
		boolean flag = false;
		do {
			flag = false;
			for(int i=0; i<ar.length-1; i++) {
				if(ar[i]>ar[i+1]) {
					flag = true;
					int tmp = ar[i+1];
					ar[i+1] = ar[i];
					ar[i] = tmp;
				}
			}
		}while(flag);
	}
}
