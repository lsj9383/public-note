package toutiao;


/**
 * 
 * 01背包问题
 * 
 * amt	背包载重
 * v[i] 第i个物体的价值
 * w[i] 第i个物体的重量
 * 
 * @author lu
 *
 */
public class Main5 {

	public static void main(String[] args) {
		int amt = 12;
		int[] v = new int[] {4, 3, 6, 4, 9};
		int[] w = new int[] {4, 9, 9, 3, 2};
		int[][] f = new int[v.length+1][amt+1];
		int[][] p = new int[v.length+1][amt+1];
		// 初始化
		for(int i=0; i<v.length+1; i++) {
			f[i][0] = 0;
		}
		for(int j=0; j<amt+1; j++) {
			f[0][j] = 0;
		}
		// 迭代
		for(int i=1; i<v.length+1; i++) {
			for(int j=1; j<amt+1; j++) {
				if(j<w[i-1]) {
					f[i][j] = f[i-1][j];
				}else {
					int n = f[i-1][j];
					int m = f[i-1][j-w[i-1]]+v[i-1];
					f[i][j] = m>n?m:n;
					p[i][j] = m>n?1:0;
				}
			}
		}
		System.out.println(f[f.length-1][amt]);
		 for(int i=0;i<f.length;i++){  
	            for(int j=0;j<f[0].length;j++){  
	                System.out.print(p[i][j]+" ");  
	            }  
	            System.out.println();  
	        }  
		int i=f.length-1;  
        int j=f[0].length-1;  
        while(i>0&&j>0){  
            if(p[i][j] == 1){  
                System.out.print("第"+i+"个物品装入 ");  
                j -= w[i-1];
            }  
            i--;  
        }  
	}

}
