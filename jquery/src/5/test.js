$(document).ready(function(){
	$('div.chapter a').attr({
		rel : 'external',
		title : function(){
					return 'Learn more about ' + $(this).text() + 'at Wikipedia.';
				},
		id: function(index, oldValue){
				return 'wikilink-'+index;
			}
	});
});