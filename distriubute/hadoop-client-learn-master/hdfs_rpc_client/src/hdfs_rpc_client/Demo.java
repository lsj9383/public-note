package hdfs_rpc_client;

import java.io.IOException;
import java.net.InetSocketAddress;

import org.apache.hadoop.conf.Configuration;
import org.apache.hadoop.ipc.RPC;

public class Demo {
	public static void main(String[] args) throws IOException{
		Interface proxy = (Interface) RPC.getProxy(Interface.class, 1L, 
							new InetSocketAddress("weekend110", 10000),
							new Configuration());
		
		String res = proxy.login("lsj", "123456");
		
		System.out.println(res);
	} 
}
