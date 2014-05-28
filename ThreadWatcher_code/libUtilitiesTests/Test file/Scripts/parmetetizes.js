
function init(args){
	for(var key in args)
		console.log("key :" +key+ ", value : "+ args[key]);
	return true;
}
result = init(args);