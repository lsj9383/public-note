package offer;

import java.util.ArrayList;

/**
 * 
 * 顺时针打印矩阵
 * 
 * @author lu
 *
 */
public class Solution19 {

	public static void main(String[] args) {
		int[][] m = new int[][] {{1,2,3,4,5}};
		System.out.print(new Solution19().printMatrix(m));
	}
	
	public ArrayList<Integer> printMatrix(int [][] matrix) {
		ArrayList<Integer> list = new ArrayList<>();
		p(matrix, 0, matrix.length-1, 0, matrix[0].length-1, list);
		return list;
    }
	
	public void p(int[][] m, int top, int bottom, int left, int right, ArrayList<Integer> list) {
		if(top>bottom || left>right) {
			return ;
		}
		for(int i=left; i<=right; i++) {
			list.add(m[top][i]);
		}
		for(int i=top+1; i<=bottom; i++) {
			list.add(m[i][right]);
		}
		if(bottom>top) {
			for(int i=right-1; i>=left; i--) {
				list.add(m[bottom][i]);
			}	
		}
		if(right>left) {
			for(int i=bottom-1; i>=top+1; i--) {
				list.add(m[i][left]);
			}	
		}
		p(m, top+1, bottom-1, left+1, right-1, list);
	}

}
