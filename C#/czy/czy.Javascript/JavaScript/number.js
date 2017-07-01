/*
String 对像的扩展
*/

(function(string)
    {
		string.fn=string.prototype;
		string.fn.toInt=function()
		{
			return parseInt(this);
		};
		string.fn.toFloat=function()
		{
			return parseFloat(this);
		};
		string.fn.trim=function()
		{
			return parseFloat(this);
		};
	}
(String))