$(document).ready(function() {
	$('#switcher-default').addClass('selected');

	$('#switcher button').on('click', function() {
		var bodyClass = this.id.split('-')[1];
		$('body').removeClass().addClass(bodyClass);
		$('#switcher button').removeClass('selected');
		$(this).addClass('selected');
	});
 
}); 

$(document).ready(function() {
 $('#switcher h3').click(function() {
 	$('#switcher button').toggleClass('hidden');
 });
}); 