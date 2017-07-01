
//转意义字符与替换图象以及字体HtmlEncode(text)
function HtmlEncode(text)
{
return text.replace(/\"/g, '&quot;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/#br#/g,'<br>').replace(/IMGSTART/g,'<IMG style=').replace(/IMGMID/g,' src=').replace(/IMGEND/g,'>').replace(/FONTSTART/g,'<font  style=\"').replace(/FONTEND/g,'</font>').replace(/FONTSTYLEEND/g,'\">').replace(/FORUELSTARTMA/g,'<a target=_blank href=').replace(/FORUELENDMA/g,'</a>').replace(/NIFORPIEHAO/g,'&#39;'); 
}
//打印scrolldIV这个div的内码内容getcontent()
function getcontent()
{
document.write(document.getElementById("scrolldIV").innerText);
}
//保存咨询记录函数CmdSave()
function CmdSave()
{
var OW = window.open('','','');
var DD = new Date();
OW.document.open();
OW.document.write(document.getElementById("scrolldIV").innerHTML);
OW.document.execCommand ("SaveAs",true,"咨询记录-"+ DD.getYear() + "-" + DD.getMonth() + "-" + DD.getDate() + "-" + DD.getHours() + "-" + DD.getMinutes() +".htm");
OW.close();
}
//把用户列表加载入到scrolldIV，函数输入值为用户列表ChatText(Text)
function ChatText(Text)
{
if(Text!="")
    {
     document.getElementById('scrolldIV').innerHTML=document.getElementById('scrolldIV').innerHTML+ HtmlEncode(Text);
     window.form1.ScrolldNeirong.value=document.getElementById('scrolldIV').innerHTML;
    document.getElementById('scrolldIV').scrollTop = document.getElementById('scrolldIV').scrollHeight;document.form1.TextBox1.focus();
    }
    if(document.getElementById('scrolldIV').scrollTop>=2000)
    {
    Cls();
    }
}
//UserList
//把咨询记录加载入到UserList，函数输入值为咨询所发送的记录UserListAdd(Text)
function UserListAdd(text,ctext,soundstat)
{

var listinfo=text.toString().replace(/ListUsersStart/g, "<label onmouseover=this.style.cursor='hand'; onclick=window.form1.ToUser.value='").replace(/ListUsersMidA/g,"';document.getElementById('ToUserType').innerHTML='").replace(/ListUsersMid/g, "';onMouseOut=this.style.cursor='default'; >").replace(/ListUsersEnd/g, "</label><br>");
var clistinfo=ctext.toString().replace(/ListUsersStart/g, "<label onmouseover=this.style.cursor='hand'; onclick=window.form1.ToUser.value='").replace(/ListUsersMidA/g,"';document.getElementById('ToUserType').innerHTML='").replace(/ListUsersMid/g, "';onMouseOut=this.style.cursor='default'; >").replace(/ListUsersEnd/g, "</label><br>");
document.getElementById('UserList').innerHTML=listinfo+'---以上为管理员---<br>'+clistinfo;
if(soundstat==1)
{
Sound.play('sound/come.wav');
}
if(soundstat==2)
{
Sound.play('sound/go.wav');
}
}

//清屏Cls()
function Cls()
{
document.getElementById('scrolldIV').innerHTML="";

}
// 隐藏与显示部分的层１为显示０为隐藏showOrHide(value)
function showOrHide(value) {
    if (value==0) {
        if (document.layers)
           document.layers["Layer1"].visibility='hide';
        else
           document.all["Layer1"].style.visibility='hidden';
   }
   else if (value==1) {
       if (document.layers)
          document.layers["Layer1"].visibility='show';
       else
          document.all["Layer1"].style.visibility='visible';
   }
}
function showOrHide1(value) {
    if (value==0) {
        if (document.layers)
           document.layers["Layer2"].visibility='hide';
        else
           document.all["Layer2"].style.visibility='hidden';
   }
   else if (value==1) {
       if (document.layers)
          document.layers["Layer2"].visibility='show';
       else
          document.all["Layer2"].style.visibility='visible';
   }
}

function showOrHide2(value) {
    if (value==0) {
        if (document.layers)
           document.layers["Layer3"].visibility='hide';
        else
           document.all["Layer3"].style.visibility='hidden';
   }
   else if (value==1) {
       if (document.layers)
          document.layers["Layer3"].visibility='show';
       else
          document.all["Layer3"].style.visibility='visible';
   }
}

//更改字体大小属性的函数changeSize(obj)
function changeSize(obj){ 
document.getElementById("TextBox1").style.fontSize = obj.value; 
window.form1.TextBox1cssText.value=window.form1.TextBox1.style.cssText;

} 
//更改字体属性的函数changeFont(obj)
function changeFont(obj){ 
document.getElementById("TextBox1").style.fontFamily = obj.value; 
window.form1.TextBox1cssText.value=window.form1.TextBox1.style.cssText;

} 
//更改字体颜色属性的函数changeColors(obj)
function changeColors(obj){ 
document.getElementById("TextBox1").style.color = obj.value; 
window.form1.TextBox1cssText.value=window.form1.TextBox1.style.cssText;

} 

//加载表情图象，输入值为图象的路径selImg(text)
function selImg(text){
var newElem = document.createElement("IMG");
newElem.src=text; 
newElem.style.cssText="WIDTH:19;HEIGHT:19";

//newElem.onload="javascript:AutoAdjustAtchImgSize(this,600);
//for(var i=0;i<5;i++)
window.form1.TextBox1.appendChild(newElem);

}

//把TextArea(TextBox1)的值赋个隐藏控件(TextBox1Hide)激发button1单击事件
function typechild()
{
if(document.form1.TextBox1.innerHTML.length<1)
	{
	    alert("咨询内容不能为空!");
	    return false;
	}
	if(document.form1.ToUser.value.length<1)
	{
	 alert("咨询对象未选中!");
	    return false;
	}

window.form1.TextBox1Hide.value= UrlChange(window.form1.TextBox1.innerHTML);
window.form1.TextBox1cssText.value=window.form1.TextBox1.style.cssText;
document.getElementById('Button1').click();
//alert(window.form1.TextBox1cssText.value);
}
//输入方式onkepress时判断咨询字符是否超过1000
	function regInput(obj, reg, inputStr)
	{	  
	
if(document.form1.TextBox1.innerHTML.length>1000 )
	{   
	alert("咨询内容不能超过1000字!");
	var docSel	= document.selection.createRange()
		if (docSel.parentElement().tagName != "INPUT")	return false		
		oSel = docSel.duplicate()
		oSel.text = ""
		var srcRange	= obj.createTextRange()
		oSel.setEndPoint("StartToStart", srcRange)
		var str = oSel.text + inputStr + srcRange.text.substr(oSel.text.length)
		return reg.test(str)
		}
	}
	//输入方式onpaste时判断咨询字符是否超过1000
	function regInputpaste(obj, reg, inputStr)
	{	  
	
if(window.clipboardData.getData('text').length+document.form1.TextBox1.innerHTML.length>1000)//粘贴板里的字符长度
	{   
	alert("咨询内容不能超过1000字!");
	var docSel	= document.selection.createRange()
		if (docSel.parentElement().tagName != "INPUT")	return false		
		oSel = docSel.duplicate()
		oSel.text = ""
		var srcRange	= obj.createTextRange()
		oSel.setEndPoint("StartToStart", srcRange)
		var str = oSel.text + inputStr + srcRange.text.substr(oSel.text.length)
		return reg.test(str)
		}
	}

	
//加载字体进入textarea
function fontin()
{
window.form1.TextBox1.style.cssText=window.form1.TextBox1cssText.value;
}
//url转超级连接
//$2表示url值
//把=和"替换

function UrlChange(r)   
{ 
 var re = /(^|[^<=""])(http:(\/\/|\\\\)(([\w\/\\\+\-~`@:%])+\.)+([\w\/\\\.\=\?\+\-~`@\':!%#]|(&amp;)|&)+)/ig; 
 var rewww=/(^|[^\/\\\w\=])((www|bbs)\.(\w)+\.([\w\/\\\.\=\?\+\-~`@\'!%#]|(&amp;))+)/ig;  
 r = r.replace(re, "FORUELSTARTMA$2IMGEND$2FORUELENDMA").replace(rewww, "FORUELSTARTMA$2IMGEND$2FORUELENDMA").replace(/\"/g, '&quot;').replace(/=/g, '&#061;');   
 return r; 
} 
//播放声音
var Sound = new Object();
Sound.play = function Sound_play(src) {
if (!src) return false;
this.stop();
var elm;
if (typeof document.all != "undefined") {
elm = document.createElement("bgsound");
elm.src = src;
}
else {
elm = document.createElement("object");
elm.setAttribute("data",src);
elm.setAttribute("type","audio/x-wav");
elm.setAttribute("controller","true");
}
document.body.appendChild(elm);
this.elm = elm;
return true;
};

Sound.stop = function Sound_stop() {
if (this.elm) {
this.elm.parentNode.removeChild(this.elm);
this.elm = null;
}
};
