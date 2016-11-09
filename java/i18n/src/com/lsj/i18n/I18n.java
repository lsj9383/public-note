package com.lsj.i18n;

import java.text.MessageFormat;
import java.util.Locale;
import java.util.ResourceBundle;

public class I18n {
	public static void main(String[] args){
//		Locale defaultLocale = Locale.getDefault();
		Locale defaultLocale = new Locale("en", "US"); 
		System.out.println("country =" + defaultLocale.getCountry());
		System.out.println("language=" + defaultLocale.getLanguage());
		
		ResourceBundle rb = ResourceBundle.getBundle("MessageBundle", defaultLocale);
		MessageFormat mf = new MessageFormat(rb.getString("k1"));
		
		
		System.out.println(String.format(rb.getString("k1"), new Object[]{"����", "tom"}));
		System.out.println(String.format(rb.getString("k2")));
	} 
}
