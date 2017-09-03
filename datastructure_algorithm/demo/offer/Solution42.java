package offer;

/**
 * 
 * 左旋转字符串
 * 
 * @author lu
 *
 */
public class Solution42 {

	public static void main(String[] args) {
		System.out.println(new Solution42().LeftRotateString("abcd", 6));
	}
	
	public String LeftRotateString(String str,int n) {
		if(str.length() == 0) {
			return "";
		}
		n = n%str.length();
		return str.substring(n)+str.substring(0, n);
    }
}
