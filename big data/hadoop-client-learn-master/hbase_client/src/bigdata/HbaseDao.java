package bigdata;

import java.io.IOException;
import java.util.ArrayList;

import org.apache.hadoop.conf.Configuration;
import org.apache.hadoop.hbase.HBaseConfiguration;
import org.apache.hadoop.hbase.HColumnDescriptor;
import org.apache.hadoop.hbase.HTableDescriptor;
import org.apache.hadoop.hbase.MasterNotRunningException;
import org.apache.hadoop.hbase.TableName;
import org.apache.hadoop.hbase.ZooKeeperConnectionException;
import org.apache.hadoop.hbase.client.HBaseAdmin;
import org.apache.hadoop.hbase.client.HTable;
import org.apache.hadoop.hbase.client.Put;
import org.apache.hadoop.hbase.util.Bytes;

public class HbaseDao {
	
	
	static public void insertTest() throws Exception{
		Configuration conf = HBaseConfiguration.create();
		conf.set("hbase.zookeeper.quorum", "zk1:2181,zk2:2181,zk3:2181");
		
		HTable nvshen = new HTable(conf, "nvshen");	//link to table
		Put name = new Put(Bytes.toBytes("rk0001"));	//row
		name.add(Bytes.toBytes("base_info"), Bytes.toBytes("name"), Bytes.toBytes("baby"));
		Put age = new Put(Bytes.toBytes("rk0001"));	//row
		age.add(Bytes.toBytes("base_info"), Bytes.toBytes("age"), Bytes.toBytes(18));
		ArrayList<Put> puts = new ArrayList<>();
		puts.add(name);
		puts.add(age);
		nvshen.put(puts);
		
	}
	
	static public void createTableTest() throws Exception{
		Configuration conf = HBaseConfiguration.create();
		conf.set("hbase.zookeeper.quorum", "zk1:2181,zk2:2181,zk3:2181");
		HBaseAdmin admin = new HBaseAdmin(conf);
		TableName name = TableName.valueOf("nvshen3");
		HTableDescriptor desc = new HTableDescriptor(name);
		
		//one column family
		HColumnDescriptor base_info = new HColumnDescriptor("base_info");	//add a columnFamily
		base_info.setMaxVersions(5);
		desc.addFamily(base_info);
		
		//one column family
		HColumnDescriptor extra_info = new HColumnDescriptor("extra_info");	//add a columnFamily
		base_info.setMaxVersions(5);
		desc.addFamily(extra_info);
		
		admin.createTable(desc);
	}
	
	
	
	public static void main(String[] args) throws Exception{
		
//		insertTest();
		createTableTest();
		System.out.println("done!");
	}
}
