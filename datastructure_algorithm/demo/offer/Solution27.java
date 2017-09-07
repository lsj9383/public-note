package offer;

import java.util.ArrayList;
import java.util.TreeSet;

/**
 * 
 * 字符串的排列
 * 
 * @author lu
 *
 */
public class Solution27 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		System.out.println(new Solution27().Permutation(""));
	}

	public ArrayList<String> Permutation(String str) {
		TreeSet<String> result = new TreeSet<>();
		p(new StringBuilder(str), 0, result);
		return new ArrayList<>(result);
    }
	
	void p(StringBuilder sb, int start, TreeSet<String> r) {
		if(start==sb.length()) {
			if(start!=0) {
				r.add(sb.toString());	
			}
		}else {
			for(int i=start; i<sb.length(); i++) {
				swap(sb, i, start);
				p(sb, start+1, r);
				swap(sb, i, start);
			}
		}
	}
	
	void swap(StringBuilder sb, int idx1, int idx2) {
		char chr = sb.charAt(idx2);
		sb.setCharAt(idx2, sb.charAt(idx1));
		sb.setCharAt(idx1, chr);
	}
	
	/*
	public ArrayList<String> Permutation(String str) {
		List<Character> list = new LinkedList<>();
		for(int i=0; i<str.length(); i++) {
			list.add(str.charAt(i));
		}
		ArrayList<String> res = p(list);
		TreeSet<String> set = new TreeSet<>(res);
		return new ArrayList<String>(set);
    }
	
	ArrayList<String> p(List<Character> list){
		ArrayList<String> result = new ArrayList<>();
		if(list.size() == 0) {
			return result;
		}
		if(list.size() == 1) {
			result.add(list.get(0)+"");
			return result;
		}
		for(int i=0; i<list.size(); i++) {
			Character chr = list.get(i);
			list.remove(chr);
			ArrayList<String> tmpList = p(list);
			for(int j=0; j<tmpList.size(); j++) {
				tmpList.set(j, chr+tmpList.get(j));
			}
			result.addAll(tmpList);
			list.add(i, chr);
		}
		return result;
	}
	*/
}
