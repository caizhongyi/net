/*
String 对像的扩展
functions:toInt()->
		  toFloat()->
		  has(str)->判断字符窜中是否包含某字符
		  lenghtBetween(minLength, maxLength)->对数组中的每个元素都执行一次指定的函数（fn）
		  isNull() ->检查输入字符串是否为空或者全部都是空格
*/
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
		//判断字符窜中是否包含某字符
		string.fn.has=function(str){
			if (this.indexOf(str, 0) != -1) {
				return true;
			}
			else {
				return false;
			}
		};
		//判断字符长度
		string.fn.lenghtBetween=function(minLength, maxLength){
			if (maxLength == 0 && this.length >= minLength) {
				return true;
			}
			else 
				if (this.length >= minLength && this.length <= maxLength) {
					return true;
				}
			eles
			{
				return false;
			}
			
		};
		 /* 
		 用途：检查输入字符串是否为空或者全部都是空格
		 输入：str
		 返回：
		 如果全是空返回true,否则返回false
		 */
		string.fn.isNull=function(){
			if (this == "") 
				return true;
			var regu = "^[ ]+$";
			var re = new RegExp(regu);
			return re.test(this);
		};
	}
(String))

