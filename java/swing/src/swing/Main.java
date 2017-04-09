package swing;

import java.awt.EventQueue;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.concurrent.FutureTask;

import javax.swing.JButton;
import javax.swing.SwingUtilities;

public class Main {

	public static void main(String[] args) {
		EventQueue.isDispatchThread();
		SwingUtilities.isEventDispatchThread();
		JButton jbutton = new JButton();
		jbutton.addActionListener(new ActionListener() {
			
			@Override
			public void actionPerformed(ActionEvent e) {
				
			}
		});
	}

}
