package FlowSum.mapreduce;

import java.io.IOException;

import org.apache.commons.lang.StringUtils;
import org.apache.hadoop.io.LongWritable;
import org.apache.hadoop.io.Text;
import org.apache.hadoop.mapreduce.Mapper;

public class FlowSumMapper extends Mapper<LongWritable, Text, Text, FlowBean> {
	@Override
	protected void map(LongWritable key, Text value, Context context) throws IOException, InterruptedException{
		String line = value.toString();
		String[] fields = StringUtils.split(line, '\t');
		
		String phoneNB = fields[1];
		long upFlow = Long.parseLong(fields[7]);
		long downFlow = Long.parseLong(fields[8]);
		
		//
		context.write(new Text(phoneNB), new FlowBean(phoneNB, upFlow, downFlow));
	}
}