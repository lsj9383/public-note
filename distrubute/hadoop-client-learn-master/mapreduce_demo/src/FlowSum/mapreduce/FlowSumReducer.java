package FlowSum.mapreduce;

import java.io.IOException;

import org.apache.hadoop.io.Text;
import org.apache.hadoop.mapreduce.Reducer;

public class FlowSumReducer extends Reducer<Text, FlowBean, Text, FlowBean>{
	@Override
	protected void reduce(Text key, Iterable<FlowBean> values, Context context) throws IOException, InterruptedException{
		long upFlow=0;
		long downFlow=0;
		
		for(FlowBean bean : values){
			upFlow += bean.getUpFlow();
			downFlow += bean.getDownFlow();
		}
		
		context.write(key, new FlowBean(key.toString(), upFlow, downFlow));
		
	}
}
