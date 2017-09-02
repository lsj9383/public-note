package offer;

public class Solution2 {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		System.out.println(new Solution2().replaceSpace(new StringBuffer("We Are Happy")));
	}

	
	public String replaceSpace(StringBuffer str) {
    	StringBuilder sb = new StringBuilder();
    	for(int i=0; i<str.length(); i++) {
    		if(str.charAt(i)!=' ') {
    			sb.append(str.charAt(i));
    		}else {
    			sb.append("%20");
    		}
    	}
    	return sb.toString();
    }
    
}
