package bigdata;

import backtype.storm.Config;
import backtype.storm.StormSubmitter;
import backtype.storm.generated.AlreadyAliveException;
import backtype.storm.generated.InvalidTopologyException;
import backtype.storm.generated.StormTopology;
import backtype.storm.topology.TopologyBuilder;

public class TopoMain {
	public static void main(String[] args) throws Exception
	{
		TopologyBuilder builder = new TopologyBuilder();
		
		builder.setSpout("randomsoput", new RandomWordSpout(), 4);
		builder.setBolt("upperbolt", new UpperBolt(), 4).shuffleGrouping("randomspout");	//receive whose information
		builder.setBolt("suffixbolt", new SuffixBolt(), 4).shuffleGrouping("upperbolt");
		
		StormTopology topology = builder.createTopology();
		
		Config conf = new Config();
		conf.setNumWorkers(4);
		conf.setDebug(true);
		conf.setNumAckers(0);
		
		StormSubmitter.submitTopology("demotopo", conf, topology);
	}
}
