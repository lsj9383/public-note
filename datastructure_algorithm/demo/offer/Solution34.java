package offer;

import java.util.HashMap;
import java.util.Map.Entry;

/**
 * 
 * 第一个只出现1次的字符
 * 
 * @author lu
 *
 */
public class Solution34 {

	public static void main(String[] args) {
		
	}

	public int FirstNotRepeatingChar(String str) {
		HashMap<Character, int[]> map = new HashMap<>();
		for(int i=0; i<str.length(); i++) {
			Character chr = str.charAt(i);
			int[] r = map.get(chr);
			if(r == null) {
				map.put(chr, new int[] {1, i});
			}else {
				r[0]++;
			}
		}
		int minIdx = Integer.MAX_VALUE;
		for(Entry<Character, int[]> entry : map.entrySet()) {
			if(entry.getValue()[0] == 1) {
				if(entry.getValue()[1]<minIdx) {
					minIdx = entry.getValue()[1];
				}
			}
		}
		if(minIdx==Integer.MAX_VALUE) {
			minIdx = -1;
		}
        return minIdx;
    }
}
