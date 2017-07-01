function $(id)
{ 
  if (document.getElementById(id)) {
		return document.getElementById(id);
	} else if(document.all) {
		return document.all[id];
	} else if(document.layers) {
		return document.layers[id];
	} 
}
//select控件附值
function SelectSetValue(obj,value) { 
   
   for(var i=0;i <obj.options.length;i++)
   {
       if (obj.options[i].text == value)
       {
       
           obj.options[i].selected = true;  
       }
   }
}
//禁用滚动条
function   DisableScroll(){
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
function  ableScroll(){
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

////事件触发对象 
function EventGetObj()
{
	obj=event.srcElement;     //事件触发对象 
	obj.setCapture();         //设置属于当前对象的鼠标捕捉 
	return obj;
}
//释放当前对象的鼠标捕捉 
function ReleaseCapture(obj)
{
	obj.releaseCapture(); //释放当前对象的鼠标捕捉 
}

//提交按扭的控制
function btnOperation(checkBox,subButton)
{   
   if(chebox.checked)
   {
      subButton.disabled="disabled";
   }
   else 
   {
      subButton.disabled="";
   }

}
// 获取selectTxt中的值
function getSelectTxt(obj){
var txt=obj.options[select.options.selectedIndex].text;
return txt;
}
///获取selectValue中的值
function getSelectValue(obj){
var value=obj.options[select.options.selectedIndex].value;
return value;
}

//去左空格;
function ltrim(s){
return s.replace( /^[" "|"　"]*/, "");
}
//去右空格;
function rtrim(s){
return s.replace( /[" "|"　"]*$/, "");
}
//左右空格;
function trim(s){
return rtrim(ltrim(s));

}
//截字符窜
function subString(str,index,lenght)
{
	return str.substring(index,lenght);
}
//获取对像style的px值
function GetStylePxValue(str)
{
  return parseFloat(str.substring(0,str.indexOf("px")));
}
//小写转为大写
function UpperCaseStr(str)
{ 
  return str.toUpperCase();
}
//大写转为小写
function LowerCaseStr(str)
{ 
   return str.toLowerCase();
}
//弹出子窗体
function  OpenModalDialog(dialogUrl,width,height)
{  
   window.showModalDialog(dialogUrl,window,'dialogWidth:'+width+'px;dialogHeight:'+height+'px;scroll:no;center:yes;help:no;resizable:no;status:no');
}
//转为float 型
function toFloat(n){
 if(parseFloat(n)){
  n = n;
 }
 else{
  alert('类型转换出错');
 }
 return parseFloat(n);
}
//转为int型
function toInt(n){
 if(parseInt(n)){
  n = n;
 }
 else{
  lert('类型转换出错');
 }
 return parseInt(n);
} 
//获取父级元素
function getParent(obj)
{
      return obj.parentElement.parentElement;
}
//获取子级元素
function getChildren(obj){
      return obj.children[0];
 }


 //设为首页
 function setFistPage(url) {
 	var strHref = window.location.href;
   this.style.behavior = 'url(#default#homepage)';
   this.setHomePage(url);
  }
  //加入收藏夹
  function AddFavorite(url,title) {

  	window.external.addFavorite(url, title)
}

//刷新注册码
function refreshCode(img,imgUrl)
{
   img.src=imgUrl+"?rnd="+ Math.random() * 10000;
}
//页面载入后js操作
window.onload=function(){}
function document.onreadystatechange()
{
if (document.readyState=="complete") {
     //你要做的操作。
   }
}
/*js实现sleep功能 单位：毫秒*/
function sleep(numberMillis) {
    var now = new Date();
    var exitTime = now.getTime() + numberMillis;
    while (true) {
        now = new Date();
        if (now.getTime() > exitTime)
            return;
    }
}
//设置锚点
function SetHash()
{
  window.location.hash="top"
}
function encodeUri(str)
{
  return encodeURI(str);
}

function GetXMLTagList(TagName)
{
    var hotels_list = xml.documentElement.getElementsByTagName((TagName));
    shop_idx = hotels_list[i].selectSingleNode("NcNo").text; //id
}

//创建div对像
function CreateElement(idName,elementTag)
{
     if(!document.createElement(elementTag))
     {
     var bgObj=document.createElement(elementTag);//
     document.body.appendChild(bgObj);//在body内添加该div对象
     msgObj.setAttribute("id",idName);
     }
}
//删除对像
function DeleteElement(obj)
{ 
  if(obj)
  {
  obj.parentNode.removeChild(obj);
  }
}

//window.location.host  获取主机域名

var exp = null;
if (!exp && typeof(exp)!="undefined" && exp!=0)
{
    alert("is null");
}　

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

function setFloat(obj)
{

    obj.style.cssFloat="left"; //横向OR纵向
    obj.style.styleFloat="left"; //横向OR纵向
 }