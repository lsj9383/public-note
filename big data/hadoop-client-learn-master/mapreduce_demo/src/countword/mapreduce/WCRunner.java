package countword.mapreduce;

import java.io.IOException;

import org.apache.hadoop.conf.Configuration;
import org.apache.hadoop.fs.Path;
import org.apache.hadoop.io.LongWritable;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.mapreduce.Job;
import org.apache.hadoop.mapreduce.lib.input.FileInputFormat;
import org.apache.hadoop.mapreduce.lib.output.FileOutputFormat;

public class WCRunner {
	public static void main(String[] args) throws IOException, ClassNotFoundException, InterruptedException{
		Configuration conf = new Configuration();

		Job job = Job.getInstance(conf);
	
		//set position of jar which class of job
		job.setJarByClass(WCRunner.class);
		
		//
		job.setMapperClass(WCMapper.class);
		job.setReducerClass(WCReducer.class);
		
		job.setOutputKeyClass(Text.class);				//reducer and map output key class
		job.setOutputValueClass(LongWritable.class);	//reducer and mao output value class
		
		job.setMapOutputKeyClass(Text.class);			//mapper output key class
		job.setMapOutputValueClass(LongWritable.class);	//mapperr output value class
	
		//origin data
		FileInputFormat.setInputPaths(job, new Path("/wc/srcdata/"));
//		FileInputFormat.setInputPaths(job, new Path("/home/hadoop/wc/srcdata/words.txt"));
//		FileInputFormat.setInputPaths(job, new Path("/home/hadoop/wc/srcdata/"));
//		FileInputFormat.setInputPaths(job, new Path("hdfs://weekend110:9000/wc/srcdata/"));
		//target data
		FileOutputFormat.setOutputPath(job, new Path("/wc/output/"));
//		FileOutputFormat.setOutputPath(job, new Path("/home/hadoop/wc/output/ok5"));
//		FileOutputFormat.setOutputPath(job, new Path("hdfs://weekend110:9000/wc/output2/"));
		
		//submit to hadoop to run
		job.waitForCompletion(true);
	} 
}
