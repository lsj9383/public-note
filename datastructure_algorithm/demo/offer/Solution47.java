package offer;

import java.math.BigInteger;

/**
 * 
 * 不用加减乘除做加法
 * 
 * @author lu
 *
 */
public class Solution47 {

	public static void main(String[] args) {
	
	}

	public int Add(int num1,int num2) {
		BigInteger bi1=new BigInteger(String.valueOf(num1));
        BigInteger bi2=new BigInteger(String.valueOf(num2));
        return bi1.add(bi2).intValue();
    }
}
