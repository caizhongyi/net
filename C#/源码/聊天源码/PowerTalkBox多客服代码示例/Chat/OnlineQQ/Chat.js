
//ת�����ַ����滻ͼ���Լ�����HtmlEncode(text)
function HtmlEncode(text)
{
return text.replace(/\"/g, '&quot;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/#br#/g,'<br>').replace(/IMGSTART/g,'<IMG style=').replace(/IMGMID/g,' src=').replace(/IMGEND/g,'>').replace(/FONTSTART/g,'<font  style=\"').replace(/FONTEND/g,'</font>').replace(/FONTSTYLEEND/g,'\">').replace(/FORUELSTARTMA/g,'<a target=_blank href=').replace(/FORUELENDMA/g,'</a>').replace(/NIFORPIEHAO/g,'&#39;'); 
}
//��ӡscrolldIV���div����������getcontent()
function getcontent()
{
document.write(document.getElementById("scrolldIV").innerText);
}
//������ѯ��¼����CmdSave()
function CmdSave()
{
var OW = window.open('','','');
var DD = new Date();
OW.document.open();
OW.document.write(document.getElementById("scrolldIV").innerHTML);
OW.document.execCommand ("SaveAs",true,"��ѯ��¼-"+ DD.getYear() + "-" + DD.getMonth() + "-" + DD.getDate() + "-" + DD.getHours() + "-" + DD.getMinutes() +".htm");
OW.close();
}
//���û��б�����뵽scrolldIV����������ֵΪ�û��б�ChatText(Text)
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
//����ѯ��¼�����뵽UserList����������ֵΪ��ѯ�����͵ļ�¼UserListAdd(Text)
function UserListAdd(text,ctext,soundstat)
{

var listinfo=text.toString().replace(/ListUsersStart/g, "<label onmouseover=this.style.cursor='hand'; onclick=window.form1.ToUser.value='").replace(/ListUsersMidA/g,"';document.getElementById('ToUserType').innerHTML='").replace(/ListUsersMid/g, "';onMouseOut=this.style.cursor='default'; >").replace(/ListUsersEnd/g, "</label><br>");
var clistinfo=ctext.toString().replace(/ListUsersStart/g, "<label onmouseover=this.style.cursor='hand'; onclick=window.form1.ToUser.value='").replace(/ListUsersMidA/g,"';document.getElementById('ToUserType').innerHTML='").replace(/ListUsersMid/g, "';onMouseOut=this.style.cursor='default'; >").replace(/ListUsersEnd/g, "</label><br>");
document.getElementById('UserList').innerHTML=listinfo+'---����Ϊ����Ա---<br>'+clistinfo;
if(soundstat==1)
{
Sound.play('sound/come.wav');
}
if(soundstat==2)
{
Sound.play('sound/go.wav');
}
}

//����Cls()
function Cls()
{
document.getElementById('scrolldIV').innerHTML="";

}
// ��������ʾ���ֵĲ㣱Ϊ��ʾ��Ϊ����showOrHide(value)
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

//���������С���Եĺ���changeSize(obj)
function changeSize(obj){ 
document.getElementById("TextBox1").style.fontSize = obj.value; 
window.form1.TextBox1cssText.value=window.form1.TextBox1.style.cssText;

} 
//�����������Եĺ���changeFont(obj)
function changeFont(obj){ 
document.getElementById("TextBox1").style.fontFamily = obj.value; 
window.form1.TextBox1cssText.value=window.form1.TextBox1.style.cssText;

} 
//����������ɫ���Եĺ���changeColors(obj)
function changeColors(obj){ 
document.getElementById("TextBox1").style.color = obj.value; 
window.form1.TextBox1cssText.value=window.form1.TextBox1.style.cssText;

} 

//���ر���ͼ������ֵΪͼ���·��selImg(text)
function selImg(text){
var newElem = document.createElement("IMG");
newElem.src=text; 
newElem.style.cssText="WIDTH:19;HEIGHT:19";

//newElem.onload="javascript:AutoAdjustAtchImgSize(this,600);
//for(var i=0;i<5;i++)
window.form1.TextBox1.appendChild(newElem);

}

//��TextArea(TextBox1)��ֵ�������ؿؼ�(TextBox1Hide)����button1�����¼�
function typechild()
{
if(document.form1.TextBox1.innerHTML.length<1)
	{
	    alert("��ѯ���ݲ���Ϊ��!");
	    return false;
	}
	if(document.form1.ToUser.value.length<1)
	{
	 alert("��ѯ����δѡ��!");
	    return false;
	}

window.form1.TextBox1Hide.value= UrlChange(window.form1.TextBox1.innerHTML);
window.form1.TextBox1cssText.value=window.form1.TextBox1.style.cssText;
document.getElementById('Button1').click();
//alert(window.form1.TextBox1cssText.value);
}
//���뷽ʽonkepressʱ�ж���ѯ�ַ��Ƿ񳬹�1000
	function regInput(obj, reg, inputStr)
	{	  
	
if(document.form1.TextBox1.innerHTML.length>1000 )
	{   
	alert("��ѯ���ݲ��ܳ���1000��!");
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
	//���뷽ʽonpasteʱ�ж���ѯ�ַ��Ƿ񳬹�1000
	function regInputpaste(obj, reg, inputStr)
	{	  
	
if(window.clipboardData.getData('text').length+document.form1.TextBox1.innerHTML.length>1000)//ճ��������ַ�����
	{   
	alert("��ѯ���ݲ��ܳ���1000��!");
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

	
//�����������textarea
function fontin()
{
window.form1.TextBox1.style.cssText=window.form1.TextBox1cssText.value;
}
//urlת��������
//$2��ʾurlֵ
//��=��"�滻

function UrlChange(r)   
{ 
 var re = /(^|[^<=""])(http:(\/\/|\\\\)(([\w\/\\\+\-~`@:%])+\.)+([\w\/\\\.\=\?\+\-~`@\':!%#]|(&amp;)|&)+)/ig; 
 var rewww=/(^|[^\/\\\w\=])((www|bbs)\.(\w)+\.([\w\/\\\.\=\?\+\-~`@\'!%#]|(&amp;))+)/ig;  
 r = r.replace(re, "FORUELSTARTMA$2IMGEND$2FORUELENDMA").replace(rewww, "FORUELSTARTMA$2IMGEND$2FORUELENDMA").replace(/\"/g, '&quot;').replace(/=/g, '&#061;');   
 return r; 
} 
//��������
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
