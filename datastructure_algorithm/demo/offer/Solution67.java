package offer;

import java.util.ArrayList;

/**
 * 
 * 
 * 和为s的两个数字
 * 
 * @author lu
 *
 */
public class Solution67 {

	public static void main(String[] args) {

	}

	public ArrayList<Integer> FindNumbersWithSum(int [] array,int sum) {
        ArrayList<Integer> res = new ArrayList<>();
        int pt1 = 0;
        int pt2 = array.length-1;
        while(pt1<pt2) {
        	if(array[pt1]+array[pt2]<sum) {
        		pt1++;
        	}else if(array[pt1]+array[pt2]>sum) {
        		pt2--;
        	}else {
        		res.add(array[pt1]);
        		res.add(array[pt2]);
        		pt1++;
        	}
        }
        if(res.size()==0) {
        	return res;
        }
        int id0=0;
        int id1=0;
        int mul=Integer.MAX_VALUE;
        for(int i=0; i<res.size(); i+=2) {
        	if(res.get(i)*res.get(i+1)<mul) {
        		if(res.get(i)<res.get(i+1)) {
        			id0=res.get(i);
            		id1=res.get(i+1);
        		}else {
        			id0=res.get(i+1);
            		id1=res.get(i);	
        		}
        		mul=res.get(i)*res.get(i+1);
        	}
        }
        res.clear();
        res.add(id0);
        res.add(id1);
        return res;
    }
}
