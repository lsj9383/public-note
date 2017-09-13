package toutiao;

/**
 * 
 * 堆排序
 * 
 * @author lu
 *
 */
public class Main3 {

	public static void main(String[] args) {
		int[] array = new int[] {4, 3, 2, 4, 5, 9, 6 ,23, 43};
		sort(array);
		for(int n:array) {
			System.out.print(n+" ");
		}
	}
	
	static void sort(int[] array) {
		for(int i=array.length/2; i>=0; i--) {
			percDown(array, i, array.length);
		}
		
		for(int i=array.length-1; i>0; i--) {
			swap(array, 0, i);
			percDown(array, 0, i);
		}
	}
	
	static int leftChild(int i) {
		return 2*i+1;
	}
	
	static void swap(int[] array, int a, int b) {
		int t = array[a];
		array[a] = array[b];
		array[b] = t;
	}
	
	static void percDown(int[] array, int i, int n) {
		int tmp=array[i];
		for(int child=0; leftChild(i)<n; i=child) {
			child = leftChild(i);
			if(child!=n-1 && array[child]<array[child+1]) {
				child+=1;
			}
			if(tmp<array[child]) {
				array[i]=array[child];
			}else {
				
				break;
			}
		}
		array[i] = tmp;
	}
}
