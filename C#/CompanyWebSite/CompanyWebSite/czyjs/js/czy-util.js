if(typeof(czyjs.Util!="undefined"))
{
	czyjs.Util={};
}
function $(id)
{
   return document.getElementById(id);   
}
/*  

  * 根据元素clsssName得到元素集合  

  * @param fatherId 父元素的ID，默认为document  

  * @tagName 子元素的标签名  

  * @className 用空格分开的className字符串  

  */ 

 function getElementsByClassName(fatherId,tagName,className){  

     node = fatherId&&document.getElementById(fatherId) || document;  

     tagName = tagName || "*";  

     className = className.split(" ");  

     var classNameLength = className.length;  

     for(var i=0,j=classNameLength;i<j;i++){  

         //创建匹配类名的正则  

         className[i]= new RegExp("(^|\\s)" + className[i].replace(/\-/g, "\\-") + "(\\s|$)");   

     }  

     var elements = node.getElementsByTagName(tagName);  

     var result = [];  

     for(var i=0,j=elements.length,k=0;i<j;i++){//缓存length属性  

         var element = elements[i];  

         while(className[k++].test(element.className)){//优化循环  

             if(k === classNameLength){  

                 result[result.length] = element;  

                 break;  

             }     

         }  

        k = 0;  

     }  

     return result;  

} 

/*
 * 判断浏览器
 * exploer{ ns,ff,ie6,ie7}
 */
czyjs.Util.CheckNavigator = function(exploer){
	var navi = navigator.appName + navigator.appVersion;
	var param = '';
	if (navi.toLowerCase().indexOf('netscape') != -1) {
		param = 'ns';
	}
	else 
		if (navi.toLowerCase().indexOf('msie 6') != -1) {
			param = 'ie6';
		}
		else 
			if (navi.toLowerCase().indexOf('msie 7') != -1) {
				param = 'ie7';
			}
			else 
				if (navi.toLowerCase().indexOf('firfox') != -1) {
					param = 'ff';
			}

	  if (param == exploer.toLowerCase()) {
	  
	  	return true;
	  }
	  else {
	  	return false;
	  }
}


//select控件附值
czyjs.Util.SetSelectValue=function(id,value) { 
   
   var obj= document.getElementById(id);
   for(var i=0;i <obj.options.length;i++)
   {
       if (obj.options[i].text == value)
       {
           obj.options[i].selected = true;  
       }
   }
}
//禁用滚动条
czyjs.Util.DisableScroll=function (){
 //判断页面是否符合XHTML标准
         var isXhtml=true;
		 if(document.documentElement == null || document.documentElement.clientHeight <= 0)
		 {
		 	if(document.body.clientHeight>0 )
			{isXhtml = false;}
		 }
 var htmlbody = isXhtml?document.documentElement:document.body;
   htmlbody.style.overflow = "hidden";
}

//可用滚动条
czyjs.Util.AbleScroll=function (){
 //判断页面是否符合XHTML标准
         var isXhtml=true;
		 if(document.documentElement == null || document.documentElement.clientHeight <= 0)
		 {
		 	if(document.body.clientHeight>0 )
			{isXhtml = false;}
		 }
 var htmlbody = isXhtml?document.documentElement:document.body;
   htmlbody.style.overflow = "auto";
}


// 获取selectTxt中的值
czyjs.Util.GetSelectTxt=function(id){
var obj=document.getElementById(id);
var txt=obj.options[select.options.selectedIndex].text;
return txt;
}
///获取selectValue中的值
czyjs.Util.GetSelectValue=function(id){
var obj=document.getElementById(id);
var value=obj.options[select.options.selectedIndex].value;
return value;
}


//弹出子窗体
czyjs.Util.OpenModalDialog=function(dialogUrl,width,height)
{  
   window.showModalDialog(dialogUrl,window,'dialogWidth:'+width+'px;dialogHeight:'+height+'px;scroll:no;center:yes;help:no;resizable:no;status:no');
}

//去左空格;
czyjs.Util.ltrim=function(s){
return s.replace( /^[" "|"　"]*/, "");
}
//去右空格;
czyjs.Util.rtrim=function(s){
return s.replace( /[" "|"　"]*$/, "");
}
//左右空格;
czyjs.Util.trim=function(s){
return rtrim(ltrim(s));

}


 //设为首页
czyjs.Util.SetFistPage=function(url) {
   var strHref = window.location.href;
   window.behavior = 'url(#default#homepage)';
   window.setHomePage(url);
  }
 //加入收藏夹
czyjs.Util.AddFavorite=function(url,title) {

  	window.external.addFavorite(url, title)
}

//刷新注册码
czyjs.Util.refreshCode=function (img,imgUrl)
{
   img.src=imgUrl+"?rnd="+ Math.random() * 10000;
}

/*js实现sleep功能 单位：毫秒*/
czyjs.Util.Sleep=function(numberMillis) {
    var now = new Date();
    var exitTime = now.getTime() + numberMillis;
    while (true) {
        now = new Date();
        if (now.getTime() > exitTime)
            return;
    }
}
//设置锚点
czyjs.Util.SetHash=function(id)
{
  window.location.hash=id;
}
//URL中文传参时间编码
czyjs.Util.UrlEncode=function(str)
{
  return encodeURI(str);
}
czyjs.Util.PixConvertToInt=function(property)
{
  return parseInt(property.replace("px",""));
}
czyjs.Util.PixConvertToFloat=function(property)
{
  return parseFloat(property.replace("px",""));
}
//window.location.host  获取主机域名



/* 
*  方法:Array.remove(dx) 通过遍历,重构数组 
*  功能:删除数组元素. 
*  参数:dx删除元素的下标. 
*/ 
Array.prototype.remove=function(dx) 
{ 
    if(isNaN(dx)||dx>this.length){return false;} 
    for(var i=0,n=0;i<this.length;i++) 
    { 
        if(this[i]!=this[dx]) 
        { 
            this[n++]=this[i] 
        } 
    } 
    this.length-=1 
} 
//a = ['1','2','3','4','5']; 
//alert("elements: "+a+"\nLength: "+a.length); 
//a.remove(1); //删除下标为1的元素 
//alert("elements: "+a+"\nLength: "+a.length); 


/* 
*  方法:Array.baoremove(dx) 
*  功能:删除数组元素. 
*  参数:dx删除元素的下标. 
*  返回:在原数组上修改数组. 
* splice方法见http://www.w3school.com.cn/js/jsref_slice_array.asp 
*/ 



Array.prototype.baoremove = function(dx) 
{ 
    if(isNaN(dx)||dx>this.length){return false;} 
    this.splice(dx,1); 
} 
//b = ['1','2','3','4','5']; 
//alert("elements: "+b+"\nLength: "+b.length); 
//b.baoremove(1); //删除下标为1的元素 
//alert("elements: "+b+"\nLength: "+b.length);

 
/*对时间控件的设定和清除用法
var inveralObj=setInterval(this.autoFocusChange.bind(this), this.interval);
var timeObj=setTimeout(this.autoFocusChange.bind(this), this.interval);
clearInterval(inveralObj);
clearTimeout(timeObj);
*/