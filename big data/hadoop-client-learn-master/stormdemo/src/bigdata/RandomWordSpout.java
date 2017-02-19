package bigdata;

import java.util.Map;
import java.util.Random;

import backtype.storm.spout.SpoutOutputCollector;
import backtype.storm.task.TopologyContext;
import backtype.storm.topology.OutputFieldsDeclarer;
import backtype.storm.topology.base.BaseRichSpout;
import backtype.storm.tuple.Fields;
import backtype.storm.tuple.Values;

public class RandomWordSpout extends BaseRichSpout{

	private SpoutOutputCollector collector;
	String[] words = {"iphone", "xiaomi","mate","sony","sumsung","moto","meizu"};
	
	@Override
	public void nextTuple() {
		// TODO Auto-generated method stub
		Random random = new Random();
		int index = random.nextInt(words.length);
		
		String godName = words[index];
		
		collector.emit(new Values(godName));
	}

	@Override
	public void open(Map arg0, TopologyContext arg1, SpoutOutputCollector collector) {
		// TODO Auto-generated method stub
		this.collector = collector;
	}

	@Override
	public void declareOutputFields(OutputFieldsDeclarer declarer) {
		// TODO Auto-generated method stub
		declarer.declare(new Fields("orignname"));		//
	}
}
