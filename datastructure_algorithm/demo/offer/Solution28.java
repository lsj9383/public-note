package offer;

import java.util.Random;

/**
 * 
 * 数组中超过半数的数
 * 
 * @author lu
 *
 */
public class Solution28 {

	public static void main(String[] args) {
		System.out.println(new Solution28().MoreThanHalfNum_Solution(new int[] {1}));
	}
	
	/*
	public int MoreThanHalfNum_Solution(int [] array) {
		Integer key = null;
		Integer cnt = null;
		for(int num : array) {
			if(key == null) {
				key = num;
				cnt = 1;
				continue ;
			}
			if(key == num) {
				cnt++;
			}else {
				if(cnt==0) {
					key = num;
				}else {
					cnt--;
				}
			}
		}
		if(key==null || cnt==0) {
			key=0;
		}
		return key;
	}
	*/
	
	public int MoreThanHalfNum_Solution(int [] array) {
		int start = 0;
		int end = array.length-1;
		int mid = array.length/2;
        int idx = Partition(array, start, end);
        while(idx!=mid) {
        	if(idx>mid) {
        		end = idx-1;
        		idx = Partition(array, start, end);
        	}else {
        		start = idx+1;
        		idx = Partition(array, start, end);
        	}
        }
        int cnt = 0;
        for(int n : array) {
        	if(n==array[idx]) {
        		cnt++;
        	}
        }
        if(cnt*2>array.length) {
        	return array[idx];
        }else {
        	return 0;
        }
    }

	int Partition(int[] array, int start, int end) {
		int r = start;
		if(start!=end) {
			r = new Random().nextInt(end)%(end-start+1) + start;
		}
		int choseIdx = r%array.length;
		int cval = array[choseIdx];
		swap(array, choseIdx, end);
		int smallIdx = start-1;
		for(int i=start; i<=end-1; i++) {
			if(array[i]<cval) {
				smallIdx++;
				swap(array, smallIdx, i);
			}
		}
		swap(array, smallIdx+1, end);
		return smallIdx+1;
	}
	
	void swap(int[] array, int left, int right) {
		int tmp = array[left];
		array[left] = array[right];
		array[right] = tmp;
	}
}
