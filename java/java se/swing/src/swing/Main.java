package swing;

import java.util.concurrent.Callable;
import java.util.concurrent.CompletionService;
import java.util.concurrent.ExecutorCompletionService;
import java.util.concurrent.Executors;

public class Main {
	public static void main(String[] args) throws InterruptedException {
		CompletionService<Object> pool = new ExecutorCompletionService<Object>(Executors.newFixedThreadPool(4));
		pool.submit(new Callable<Object>(){
			@Override
			public Object call() throws Exception {
				return null;
			}
		});
	}
}
