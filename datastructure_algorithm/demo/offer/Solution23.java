package offer;

/**
 * 
 * 判断输入的数列是否为二叉搜索树的后序遍历结果。
 * 
 * @author lu
 *
 */
public class Solution23 {

	public static void main(String[] args) {
		new Solution23().VerifySquenceOfBST(new int[] {5,4,3,2,1});

	}

	public boolean VerifySquenceOfBST(int [] sequence) {
		if(sequence.length==0) {
			return false;
		}
        return p(sequence, 0, sequence.length-1);
    }
	
	boolean p(int[] array, int start, int end) {
		if(start>=end) {
			return true;
		}
		int rootVal = array[end];
		int leftEnd = -1;
		for(int i=start; array[i]<=rootVal && i<end; i++) {
			leftEnd=i;
		}
		for(int i=leftEnd+1; i<end; i++) {
			if(array[i]<=rootVal) {
				return false;
			}
		}
		return p(array, start, leftEnd) && p(array, leftEnd+1, end-1);
	}
}
