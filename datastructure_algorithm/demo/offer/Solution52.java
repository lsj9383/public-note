package offer;

/**
 * 
 * 表示数值的字符串
 * 
 * @author lu
 *
 */
public class Solution52 {

	public static void main(String[] args) {
		System.out.println(new Solution52().p("+-5"));
		
	}

	public boolean isNumeric(char[] str) {
        String s = new String();
        for(char chr : str) {
        	s+=chr;
        }
        s = s.trim();
        return p(s);
    }
	
	boolean p(String s) {
		if(s.length()==0) {
			return false;
		}else if(s.contains("e")) {
        	String[] parts = s.split("e");
        	if(parts.length!=2) {
        		return false;
        	}
        	if(p(parts[0]) && p(parts[1])) {
        		return true;
        	}
        }else if(s.contains("E")) {
        	String[] parts = s.split("E");
        	if(parts.length!=2) {
        		return false;
        	}
        	if(p(parts[0]) && p(parts[1])) {
        		return true;
        	}
        }else if(s.contains("+")) {
        	String[] parts = s.split("\\+");
        	if(parts[1].startsWith("+") || parts[1].startsWith("-")) {
        		return false;
        	}
        	if(parts[0].length()==0 && toNumber(parts[1])!=null) {
        		return true;
        	}
        }else if(s.contains("-")) {
        	String[] parts = s.split("-");
        	if(parts[1].startsWith("+") || parts[1].startsWith("-")) {
        		return false;
        	}
        	if(parts[0].length()==0 && p(parts[1])) {
        		return true;
        	}
        }else if(s.contains(".")) {
        	String[] parts = s.split("\\.");
        	if(parts.length>=3) {
        		return false;
        	}
        	if(parts[0].length()==0 && toNumber(parts[1])!=null) {
        		return true;
        	}
        	if(p(parts[0]) && toNumber(parts[1])!=null) {
        		return true;
        	}
        }else {
        	if(toNumber(s)!=null) {
        		return true;
        	}
        }
        return false;
	}
	
	Long toNumber(String s) {
		try {
			Long n = Long.parseLong(s);
			return n;
		}catch(Exception e) {
			return null;
		}
	}
}
