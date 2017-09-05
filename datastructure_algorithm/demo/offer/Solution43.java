package offer;


/**
 * 
 * 反转单词顺序
 * 
 * @author lu
 *
 */
public class Solution43 {

	public static void main(String[] args) {
		System.out.println(new Solution43().ReverseSentence("a b c"));
	}
	
	 public String ReverseSentence(String str) {
		 String[] parts = str.split(" ");
		 StringBuilder sb = new StringBuilder();
		 if(str.trim().equals("")) {
			 return str;
		 }
		 for(int i=parts.length-1; i>=0 ;i--) {
			 sb.append(parts[i]).append(" ");
		 }
		 if(sb.length()!=0) {
			 sb.deleteCharAt(sb.length()-1);
		 }
		 return sb.toString();
	 }

}
