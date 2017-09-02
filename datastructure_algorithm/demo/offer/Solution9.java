package offer;

/**
 * 变态跳台阶
 * 
 * @author lu
 *
 */
public class Solution9 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		System.out.println(new Solution9().JumpFloorII(4));
	}

	 public int JumpFloorII(int target) {
		 int sum = 0;
		 if(target==1){
			 return 1;
		 }
		 for(int i=1; i<=target-1; i++) {
			 sum += JumpFloorII(i);
		 }
		 return sum+1;
	 }
}
