package countword.mapreduce;

import java.io.IOException;

import org.apache.hadoop.io.LongWritable;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.mapreduce.Mapper;
import org.jboss.netty.util.internal.StringUtil;

public class WCMapper extends Mapper<LongWritable, Text, Text, LongWritable>{
	@Override
	protected void map(LongWritable key, Text value, Context context) 
			throws IOException, InterruptedException{
		//mapreduce read one line and translate to this method
		//key 
		//value - one line string
		//context - tools about translating to reduce
		String line = value.toString();
		String[] words = StringUtil.split(line, ' ');
		
		for(String word : words){
			context.write(new Text(word), new LongWritable(1L));
		}
	}
}
