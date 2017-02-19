package FlowSum.mapreduce;

import java.io.DataInput;
import java.io.DataOutput;
import java.io.IOException;

import org.apache.hadoop.io.Writable;
import org.apache.hadoop.io.WritableComparable;

public class FlowBean implements WritableComparable<FlowBean>{

	private String phoneNumber;
	private long upflow;
	private long downflow;
	private long sumflow;
	
	public FlowBean(){}
	
	public FlowBean(String newNumber, long newUpFlow, long newDownFlow){
		phoneNumber = newNumber;
		upflow = newUpFlow;
		downflow = newDownFlow;
		sumflow = upflow+downflow;
	}
	
	public String getPhoeNumber(){
		return phoneNumber;
	}
	public long getUpFlow(){
		return upflow;
	}
	public long getDownFlow(){
		return downflow;
	}
	public long getSumFlow(){
		return sumflow;
	}
	
	public void setPhoeNumber(String newNumber){
		phoneNumber = newNumber;
	}
	public void setUpFlow(long newFlow){
		upflow = newFlow;
	}
	public void setDownFlow(long newFlow){
		downflow = newFlow;
	}
	public void setSumFlow(long newFlow){
		sumflow = newFlow;
	}
	
	//serailze from object
	@Override
	public void readFields(DataInput in) throws IOException {
		phoneNumber = in.readUTF();
		upflow = in.readLong();
		downflow = in.readLong();
		sumflow = in.readLong();
	}

	//
	@Override
	public void write(DataOutput out) throws IOException {
		//sequence
		out.writeUTF(phoneNumber);
		out.writeLong(upflow);
		out.writeLong(downflow);
		out.writeLong(sumflow);
	}
	
	@Override
	public String toString(){
		return phoneNumber+"\t"+upflow+"\t"+downflow+"\t"+sumflow;
	}

	@Override
	public int compareTo(FlowBean o) {
		return sumflow>o.getSumFlow() ? -1 : 1;
	}

}
