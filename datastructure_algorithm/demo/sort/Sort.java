package demo.sort;

public abstract class Sort {
	public abstract void process(int[] ar);
	protected void swap(int i, int j, int[] ar) {
		int tmp = ar[i];
		ar[i] = ar[j];
		ar[j] = tmp;
	}
}
