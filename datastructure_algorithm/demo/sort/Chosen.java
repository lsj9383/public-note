package demo.sort;

public class Chosen extends Sort{
	@Override
	public void process(int[] ar) {
		for(int i=0; i<ar.length-1; i++) {
			int min = ar[i];
			int minPos = i;
			for(int j=i+1; j<ar.length; j++) {
				if(min>ar[j]) {
					minPos = j;
					min = ar[j];
				}
			}
			swap(minPos, i, ar);
		}
	}
}
