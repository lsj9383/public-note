package offer;

public class Solution1 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		int[][] array = new int[][] {{1, 2, 3}, {4, 5, 6}, {7, 8, 9}};
		System.out.println(new Solution1().Find(0, array));
	}

	public boolean Find(int target, int [][] array) {
		return Find(target, array, 0, array[0].length-1);
    }
	
	boolean Find(int target, int[][] array, int rowStart, int colEnd) {
		if(colEnd<0 || rowStart>=array.length) {
			return false;
		}
		if(target>array[rowStart][colEnd]) {
			return Find(target, array, rowStart+1, colEnd);
		}else if(target<array[rowStart][colEnd]) {
			return Find(target, array, rowStart, colEnd-1);
		}else {
			return true;
		}
	}
}
