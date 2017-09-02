package offer;

/**
 * 跳台阶
 * 
 * @author lu
 *
 */
public class Solution8 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		System.out.println(new Solution8().JumpFloor(5));
	}
	
	public int JumpFloor(int target) {
		if(target==1) {
			return 1;
		}else if(target==2) {
			return 2;
		}
		return JumpFloor(target-1)+JumpFloor(target-2);
    }

}
