package FlowSort.mapreduce;

import java.io.IOException;

import org.apache.hadoop.conf.Configuration;
import org.apache.hadoop.fs.Path;
import org.apache.hadoop.io.LongWritable;
import org.apache.hadoop.io.NullWritable;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.mapreduce.Job;
import org.apache.hadoop.mapreduce.Mapper;
import org.apache.hadoop.mapreduce.Reducer;
import org.apache.hadoop.mapreduce.Mapper.Context;
import org.apache.hadoop.mapreduce.lib.input.FileInputFormat;
import org.apache.hadoop.mapreduce.lib.output.FileOutputFormat;
import org.apache.hadoop.util.StringUtils;

import FlowSum.mapreduce.FlowBean;

public class SortMR{
	
	public static class SortMapper extends Mapper<LongWritable, Text, FlowBean, NullWritable> {
		@Override
		protected void map(LongWritable key, Text value, Context context) throws IOException, InterruptedException{
			String line = value.toString();
			String[] fields = StringUtils.split(line, '\t');
			
			String phoneNB = fields[1];
			long upFlow = Long.parseLong(fields[7]);
			long downFlow = Long.parseLong(fields[8]);
			
			//
			context.write(new FlowBean(phoneNB, upFlow, downFlow), NullWritable.get());
		}
	}
	
	public static class SortReducer extends Reducer<FlowBean, NullWritable, FlowBean, NullWritable>{
		@Override
		protected void reduce(FlowBean key, Iterable<NullWritable> values, Context context) throws IOException, InterruptedException{
			context.write(key, NullWritable.get());
		}
	}
	
	public static void main(String[] args) throws Exception{
		Job job = Job.getInstance(new Configuration());
		job.setJarByClass(SortMR.class);
		
		job.setMapperClass(SortMapper.class);
		job.setReducerClass(SortReducer.class);
		
		job.setMapOutputKeyClass(FlowBean.class);
		job.setMapOutputValueClass(NullWritable.class);
		
		job.setOutputKeyClass(Text.class);
		job.setOutputValueClass(FlowBean.class);
		
		FileInputFormat.setInputPaths(job, new Path(args[0]));
		FileOutputFormat.setOutputPath(job, new Path(args[1]));
		
		System.out.println( job.waitForCompletion(true)?0:1 );
	}
}
