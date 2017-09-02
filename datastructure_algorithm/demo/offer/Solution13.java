package offer;

/**
 * 调整数组顺序使奇数位于偶数前面
 * 
 * @author lu
 *
 */
public class Solution13 {

	public static void main(String[] args) {
		int[] array = new int[] {1,2,3,4,5};
		new Solution13().reOrderArray(array);
		System.out.println(array);
	}

	public void reOrderArray(int [] array) {
		if(array.length==1) {
			return ;
		}
		int[] a1 = new int[array.length];
		int[] a2 = new int[array.length];
		int id1 = 0;
		int id2 = 0;
		for(int i=0; i<array.length; i++) {
			if(array[i]%2!=0) {
				a1[id2++]=array[i];
			}
			if(array[i]%2==0) {
				a2[id1++]=array[i];
			}
		}
		for(int i=0; i<array.length; i++) {
			if(i<=id1) {
				array[i] = a1[i];
			}else {
				array[i] = a2[i-id1-1];
			}
		}
    }
	
	public void reOrderArray2(int [] array) {
        int fp1 = 0;
        int fp2 = array.length-1;
        
        while(true) {
        	while(array[fp1]%2!=0) fp1++;
        	while(array[fp2]%2==0) fp2--;
        	if(fp1>=fp2) {
        		break;
        	}else {
        		int tmp = array[fp1];
        		array[fp1] = array[fp2];
        		array[fp2] = tmp;
        	}
        }
    }
}
