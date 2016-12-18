$(document).ready(function(){
	$speech = $('div.speech');
	default_number = parseFloat($speech.css('font-size'));
	
	$('#switcher-large').click(function(){
		var num = parseFloat($speech.css('font-size'));
		$speech.css('font-size', num*1.4+'px');
	});
		
	$('#switcher-small').click(function(){
		var num = parseFloat($speech.css('font-size'));
		$speech.css('font-size', num/1.4+'px');
	});
		
	$('#switcher-default').click(function(){
		$speech.css('font-size', default_number+'px')
	});
});