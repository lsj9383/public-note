$(document).ready(function(){
	$('<a href="#top">back to top</a>').insertAfter('div.chapter p');
	$('<a id="top"></a>').prependTo('body');
	/*
	$('span.footnote')
		.insertBefore('#footer')
		.wrapAll('<ol id="notes"></ol>')
		.wrap('<li></li>');
	*/
	/*
	//显示迭代
	var $notes = $('<ol id=notes></ol>').insertBefore('#footer');
	$('span.footnote').each(function(index){
		$('<sup>'+(index+1)+'</sup>').insertBefore(this);	//sup是在this移动前放置的
		$(this).appendTo($notes).wrap('<li></li>');			//this并不包含sup的，因此只会移动this，不会移动sup
	});
	*/
	/*
	//反向操作
	var $notes = $('<ol id=notes></ol>').insertBefore('#footer');
	$('span.footnote').each(function(index){
		$(this).before([
			'<sup>',
			(index+1),
			'</sup>'].join(''));
		.appendTo($notes)
		.wrap('<li></li>');
	});
	*/
	var $notes = $('<ol id=notes></ol>').insertBefore('#footer');
	$('span.footnote').each(function(index){
		$(this).before([
			'<a href="#footnote-',
	        index + 1,
	        '" id="context-', 
	        index + 1, 
        	'" class="context">', 
			'<sup>',
			(index+1),
			'</sup></a>'].join(''))
		.appendTo($notes)
		.append([
			'&nbsp;<a href="#context-',
			index+1,
			'">context</a>'].join(''))
		.wrap('<li id="footnote-'+(index+1)+'"></li>');
	});
});