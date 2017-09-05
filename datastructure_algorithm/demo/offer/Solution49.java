package offer;

import java.util.HashMap;

public class Solution49 {

	public static void main(String[] args) {
		new Solution49().duplicate(new int[] {}, 0, new int[1]);
	}

	public boolean duplicate(int numbers[],int length,int [] duplication) {
		HashMap<Integer, Integer> map = new HashMap<>();
		if(numbers!=null) {
			for(int n : numbers) {
				Integer cnt = map.get(n);
				cnt = cnt==null?0:cnt;
				cnt++;
				if(cnt==2) {
					duplication[0] = n;
					return true;
				}
				map.put(n, cnt);
			}	
		}
		duplication[0]=-1;
		return false;
    }
}
