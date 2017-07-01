/*
document 对像的扩展
*/

(function(document)
    {
		document.fn=string.prototype;
		document.fn.loadJS=function(url)
		{
			this.write(unescape("%3Cscript language='javascript' src='" + url + "' %3E%3C/script%3E"));
			return this;
		};
	}
(document))