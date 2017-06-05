import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.DatagramPacket;
import java.net.DatagramSocket;
import java.net.InetAddress;
import java.net.NetworkInterface;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.SocketAddress;
import java.net.SocketException;
import java.net.UnknownHostException;
import java.util.Enumeration;

public class EchoUdp {
	
	public static void Server() throws Exception{
		DatagramSocket socket = new DatagramSocket(1030);
		DatagramPacket recPacket = new DatagramPacket(new byte[1024], 1024);
		
		while(true){
			socket.receive(recPacket);
			System.out.println("Handling client at " + recPacket.getAddress().getHostAddress());
			socket.send(recPacket);			//
			recPacket.setLength(1024);
		}
	}
	
	public static void main(String[] args) throws Exception{
		Server();
		System.out.println("Main Done");
	}
}
