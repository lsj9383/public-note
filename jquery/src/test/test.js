$(document).ready(function(){
	$('body').click(function(event){
		alert(2);
	});
	
	$('input').click(function(event){
		alert(1);
		//event.stopPropagation();	//阻止了事件传播，不会弹窗alert(2)， 但是并不会阻止默认操作，会发送页面跳转。
		//return false;				//阻止了事件传播，也阻止了默认操作。
	});
});

function onsubmitAction(){
	alert(3);
	return true;
}