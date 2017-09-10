package offer;

import java.util.ArrayList;
import java.util.Arrays;

/**
 * 
 * 数据流中的中位数
 * 
 * @author lu
 *
 */
public class Solution63 {

	
	
	public static void main(String[] args) {
	}
	
	ArrayList<Integer> list = new ArrayList<>();

	public void Insert(Integer num) {
		list.add(num);
	}

    public Double GetMedian() {
    	Integer[] array = new Integer[list.size()];
    	list.toArray(array);
    	Arrays.sort(array);
    	if(list.size()%2==0) {
    		return (array[list.size()/2]+array[list.size()/2-1])/2.0;
    	}else {
    		return (double)array[list.size()/2];
    	}
    }
	
}
