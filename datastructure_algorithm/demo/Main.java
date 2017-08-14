package demo;

import demo.sort.Bubble;
import demo.sort.Chosen;
import demo.sort.Insert;
import demo.sort.Quick;

public class Main {
	public static void main(String[] args){
		int[] ar = new int[] {4, 3, 4, 2};
		new Quick().process(ar);
		for(int n : ar) {
			System.out.print(n+" ");
		}
	}
}