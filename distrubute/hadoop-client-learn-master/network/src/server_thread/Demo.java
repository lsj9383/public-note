package server_thread;

import java.io.*;
import java.net.*;
import java.util.*;

public class Demo {
	public static void main(String[] args) throws Exception{
		ServerSocket s = new ServerSocket(8189);
		
		while(true){
			Socket thread = s.accept();
			System.out.println("one link...");
			Thread t = new Thread(new ThreadEchoThread(thread));
			t.start();
		}
	} 
}

class ThreadEchoThread implements Runnable{

	private Socket thread = null;
	
	public ThreadEchoThread(Socket s){
		this.thread = s;
	}
	
	@Override
	public void run() {
		// TODO Auto-generated method stub
		
		try {
			
			InputStream thread_input = thread.getInputStream();
			OutputStream thread_output = thread.getOutputStream();
			Scanner in = new Scanner(thread_input);
			PrintWriter out = new PrintWriter(thread_output, true);	//auto flush
			
			while(in.hasNextLine()){
				String sbuffer = in.nextLine();
				out.println(sbuffer);
				if(sbuffer.equals("BYE")){
					thread.close();
					return ;
				}
			}
		} catch (Exception e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		
	}
	
}