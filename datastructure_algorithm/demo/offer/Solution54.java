package offer;

import java.util.HashMap;
import java.util.Map.Entry;

/**
 * 
 * 字符流中第一个不重复中的字符
 * 
 * @author lu
 *
 */
public class Solution54 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
	}
	
	HashMap<Character, int[]> map = new HashMap<>();
	int idx = 0;
	//Insert one char from stringstream
    public void Insert(char ch)
    {
        int[] ar = map.get(ch);
        if(ar==null) {
        	ar = new int[] {0, idx};
        	map.put(ch, ar);
        }
        ar[0]++;
        idx++;
    }
    
    //return the first appearence once char in current stringstream
    public char FirstAppearingOnce()
    {
    	char chr = '#';
    	int idx= Integer.MAX_VALUE;
    	for(Entry<Character, int[]> entry : map.entrySet()) {
    		if(entry.getValue()[0]==1 && entry.getValue()[1]<idx) {
    			idx = entry.getValue()[1];
    			chr = entry.getKey();
    		}
    	}
    	return chr;
    }

}
