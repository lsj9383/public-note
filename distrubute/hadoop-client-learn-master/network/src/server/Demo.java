package server;

import java.io.*;
import java.net.*;
import java.util.*;

public class Demo {
	public static void main(String[] args) throws Exception{
		ServerSocket s = new ServerSocket(8189);
		
		Socket thread = s.accept();
		
		InputStream thread_input = thread.getInputStream();
		OutputStream thread_output = thread.getOutputStream();
		Scanner in = new Scanner(thread_input);
		PrintWriter out = new PrintWriter(thread_output, true);	//auto flush
		
		while(in.hasNextLine()){
			String sbuffer = in.nextLine();
			out.println(sbuffer);
			if(sbuffer.equals("BYE")){
//				out.close();
//				in.close();
				thread.close();
			}
		}
	} 
}
