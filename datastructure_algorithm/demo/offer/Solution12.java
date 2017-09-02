package offer;

/**
 * 数值的整数次方
 * 
 * @author lu
 *
 */
public class Solution12 {

	public static void main(String[] args) {
		System.out.println(new Solution12().Power(2, -3));
	}

	public double Power(double base, int exponent) {
		double res = 1;
		for(int i=0; i<Math.abs(exponent); i++) {
			res*=base;
		}
		if(exponent<0) {
			return 1/res;
		}
		return res;
	}
}
