
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.InetAddress;
import java.net.NetworkInterface;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.SocketAddress;
import java.net.SocketException;
import java.net.UnknownHostException;
import java.util.Enumeration;

public class EchoTcp {
	public static void Server() throws Exception{
		ServerSocket servSock = new ServerSocket(1020);
		
		while(true){
			Socket clntSock  = servSock.accept();
			
			SocketAddress clientAddress = clntSock.getRemoteSocketAddress();
			System.out.println("Handling client at : " + clientAddress);
			InputStream in = clntSock.getInputStream();
			OutputStream out = clntSock.getOutputStream();
			byte[] receive = new byte[1024];
			int recSize = 0;
			while((recSize = in.read(receive)) != -1){
				out.write(receive, 0, recSize);
			}
			System.out.println(clientAddress+" close");
			clntSock.close();
		}
	}
	
	public static void main(String[] args) throws Exception{
		Server();
		System.out.println("Main Done");
	}
}
