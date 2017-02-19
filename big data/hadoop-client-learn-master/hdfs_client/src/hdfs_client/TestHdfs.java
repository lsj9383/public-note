package hdfs_client;

import java.io.*;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.net.URI;

import org.apache.commons.io.IOUtils;
import org.apache.hadoop.conf.Configuration;
import org.apache.hadoop.fs.FSDataInputStream;
import org.apache.hadoop.fs.FSDataOutputStream;
import org.apache.hadoop.fs.FileStatus;
import org.apache.hadoop.fs.FileSystem;
import org.apache.hadoop.fs.LocatedFileStatus;
import org.apache.hadoop.fs.Path;
import org.apache.hadoop.fs.RemoteIterator;

public class TestHdfs {
	
	private static Configuration conf = new Configuration();
	private static FileSystem fs = null;
	
	static void Init() throws Exception{
		conf.set("fs.defaultFS", "hdfs://weekend110:9000/");
		fs = FileSystem.get(conf);
//		fs = FileSystem.get(new URI("hdfs://weekend110:9000/"), conf, "hadoop");
	}
	
	static void upload() throws Exception{
		
		Path dst = new Path("hdfs://weekend110:9000/aa/qingshu2.txt");
		FSDataOutputStream os = fs.create(dst);
		FileInputStream is = new FileInputStream("/home/hadoop/qingshu.txt");
		IOUtils.copy(is, os);
	}

	static void upload2() throws Exception{
		
		fs.copyFromLocalFile(new Path("/home/hadoop/qingshu.txt"), 
							new Path("/aa/qingshu5.txt"));
	}
	
	public static void download() throws Exception{
		fs.copyToLocalFile(new Path("/aa/qingshu5.txt"), 
				new Path("/home/hadoop/Downloads/qq.txt"));
	}
	
	static void listFiles() throws Exception{
		RemoteIterator<LocatedFileStatus> files = fs.listFiles(new Path("/"),
																true);
		
		while(files.hasNext()){
			LocatedFileStatus file = files.next();
			Path path = file.getPath();
			System.out.println(path.getName());
		}
		
		System.out.println();
		
		FileStatus[] listStatus = fs.listStatus(new Path("/"));
		for(FileStatus stat : listStatus){
			String name = stat.getPath().getName();
			System.out.println(name + (stat.isDirectory()?" dir ":" file "));
		}
	}
	
	static void mkdir() throws Exception{
		fs.mkdirs(new Path("/aaa/bbb/ccc"));
	}
	
	static void rm() throws Exception{
		fs.delete(new Path("/aa"), true);
	}
	
	static void trace() throws Exception{
		Configuration conf = new Configuration();
		conf.set("fs.defaultFS", "hdfs://weekend110:9000/");
		
		FileSystem fs = FileSystem.get(conf);
		
		FSDataInputStream is = fs.open(new Path("/jdk-7u65-linux-i586.tar.gz"));
		FileOutputStream os = new FileOutputStream("/home/hadoop/Downloads/jdk7.gz");
		//IOUtils.copy(is, os);
	}
	
	public static void main(String[] args) throws Exception{
//		Init();
//		upload();
//		upload2();
//		download();
//		mkdir();
//		rm();
//		listFiles();
		trace();
		System.out.println("done!");
	}
}

