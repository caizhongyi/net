/*
@author : czy
@about  : core
@time   : 2011/5/17
*/

/* 实现setTimeout传参调用 */
window.setTimeout = function(callback,timeout,param) 　
{
	var args = Array.prototype.slice.call(arguments,2); 
　 　var _cb = function() 　
    { 　
	　 callback.apply(null,args); 
	} 
　 　__sto(_cb,timeout); 　
} 