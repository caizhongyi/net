
function DosJscriptShowmenu(e,vmenuobj){
	if (document.readyState=="complete")
	{ 
	var vobj=document.getElementById(vmenuobj);
	if (vobj==null)
	{
		return;
		}
	
	var shimName="Dos_WebSiteDiv";
	DosCreateDiv(shimName);
	var divObj=document.getElementById(shimName);
	if (divObj.openID!="")
	{
		DosHiddenMenu(divObj.openID);
		}
        divObj.openID=vmenuobj;	
        var getlength
        getlength=DosJscriptXoffsetParent(event.srcElement)+0;
        vobj.style.left=getlength;
        getlength=DosJscriptYoffsetParent(event.srcElement)+18;
        vobj.style.top=getlength;
        vobj.style.visibility="visible";
        vobj.style.zIndex=100000;

        divObj.style.left=vobj.style.left;
        divObj.style.top=vobj.style.top;
        divObj.style.width=vobj.offsetWidth;
        divObj.style.height=vobj.offsetHeight;
        divObj.style.visibility="visible";
	

        var iframeName="Dos_WebSiteIframe";
        DosCreateIfrom(iframeName);
        var iframeObj=document.getElementById(iframeName);	  
        iframeObj.style.display='';
        iframeObj.style.left=vobj.style.posLeft;
        iframeObj.style.top=vobj.style.posTop;
        iframeObj.style.width=vobj.offsetWidth;
        iframeObj.style.height=vobj.offsetHeight;
        iframeObj.style.zIndex=divObj.style.zIndex-1;	 
	 }
}
function DosCreateDiv(shimName)
{
	  var divObj=document.getElementById(shimName);
	  if (divObj==null)
	  {
	  	  divObj = document.createElement("<div></div>"); 
		  divObj.name = shimName;
		  divObj.id = shimName;
	  	  divObj.openID="";
	  	  divObj.className="Dos_menuskin";
	      divObj.style.zIndex=10000;	
		  window.document.body.appendChild(divObj);
	  }

}
function DosCreateIfrom(iframeName)
{
    var iframeObj=document.getElementById(iframeName);
    if (iframeObj==null)
    {
    var iframeObj = document.createElement("<iframe scrolling='no' frameborder='1'"+
                                  "style='position:absolute; top:0px;"+
                                  "left:0px; display:none;width:0;height:0'></iframe>"); 
    iframeObj.name = iframeName;
    iframeObj.id = iframeName;
    window.document.body.appendChild(iframeObj);
    }
}
function DosJscriptYoffsetParent(e){  
	var t=e.offsetTop;  
	while(e=e.offsetParent){  
		t+=e.offsetTop;  
	}  
	return t;  
}  
function DosJscriptXoffsetParent(e){  
	var l=e.offsetLeft;  
	while(e=e.offsetParent){  
		l+=e.offsetLeft;  
	}  
	return l;  
}  
function DosJscriptOnMouseOutMenu(e)
{
	var vobj=document.getElementById(e.id);	
	var shimName="Dos_WebSiteDiv";
	  var divObj=document.getElementById(shimName);
	  
	  if ((event.clientX<=eval(parseInt(e.style.left)+1+parseInt(document.body.scrollLeft)))||(event.clientX>=eval(parseInt(e.style.left))+e.offsetWidth+parseInt(document.body.scrollLeft))||(event.clientY<=eval(parseInt(e.style.top))+1-parseInt(document.body.scrollTop))||(event.clientY>=eval(parseInt(e.style.top))+e.offsetHeight-parseInt(document.body.scrollTop)))
	  {
		  DosHiddenMenu(e.id);
		  }
	}
function DosHiddenMenu(shimName)
{
	var vobj=document.getElementById(shimName);
	vobj.style.visibility='hidden';
	shimName="Dos_WebSiteDiv";
	vobj=document.getElementById(shimName);
	vobj.openID="";
	vobj.style.visibility="hidden"
	shimName="Dos_WebSiteIframe";
	vobj=document.getElementById(shimName);
	if (vobj!=null)
	{
	    vobj.style.display='none';
	}
	}

document.onclick=DosDocumentOnclick;

function DosDocumentOnclick()
{
	var shimName="Dos_WebSiteDiv";
	var vobj=document.getElementById(shimName);
	if (vobj!=null)
	{
		if (vobj.style.visibility=="visible")
		{
			DosHiddenMenu(vobj.openID);
			}
		}
	}



function JavascriptSetStyle(CssUrl)
{
	var url="/UserInfo/SetPageStyle.aspx?CssUrl="+CssUrl.replace(".css","");
	XmlHttpSetResult('0',url);
	document.all.WebSite_System_Css.href='/Style/'+CssUrl+'';
}

function JavaScrptWebSiteUserLogin()
{
        if (document.all.UserName.value=="")
        {
            alert('请输入用户名称！');
            document.all.UserName.focus();
            return;
        }
        if (document.all.UserPassWord.value=="")
        {
            alert('请输入用户密码！');
            document.all.UserPassWord.focus();
            return;
        }
    ScreenConvert();
    document.WebSiteForm.target='WebSiteTarGet';
    document.WebSiteForm.action='/UserInfo/UserLoginAction.aspx';
    document.all.WebSiteForm.submit();
}//

function ScreenConvert()
{
	v_ScreenAlpha = 80;
	var objScreen = document.getElementById("WebSiteHiddenDiv");
    objScreen.innerHTML='<div  style=\"margin-top:'+document.body.scrollWidth/4+'px ;height:'+document.body.scrollHeight/8+'px;\"><div style=\"width:311px; height:25px;\"><div style=\"width:7px; background:url(/images/group141.gif); height:25px; float:left\"></div><div style=\"width:297px; padding-top:2px; text-align:left; background:url(/images/group146.gif); float:left; height:25px\">系统提示！</div><div style=\"width:7px; background:url(/images/group142.gif); height:25px; float:right\"></div></div><div style=\"width:311px; height:146px;\"><div style=\"width:7px; background:url(/images/group147.gif); height:146px; float:left\"></div><div style=\"width:297px; background:url(/images/group145.gif); float:left; height:146px; text-align:center\"><div style=\"width:260px; margin-top:40px;\"><IMG height=12 src=\"/images/load.gif\" width=250></div><div style=\"width:260px; margin-top:8px;\">操作中，请稍候......</div></div><div style=\"width:7px; background:url(/images/group148.gif); height:146px; float:right\"></div></div><div style=\"width:311px; height:7px;\"><div style=\"width:7px; height:7px; float:left\"><IMG height=7 src=\"/images/group144.gif\" width=7></div><div style=\"width:297px; height:7px; float:left; background:url(/images/group149.gif)\"><IMG height=7 src=\"/images/group149.gif\" width=7></div><div style=\"width:7px; height:7px; float:right\"><IMG height=7 src=\"/images/group143.gif\" width=7></div></div></div>'
	objScreen.style.display = '';
	objScreen.style.top = "0px";
	objScreen.style.left = "0px";
	objScreen.style.width = document.body.scrollWidth + "px";
	objScreen.style.height = document.body.scrollHeight + "px";
	objScreen.style.position = "absolute";
	objScreen.style.zIndex = "3000";
	objScreen.style.background = "#cccccc";
	objScreen.style.filter = "alpha(opacity=" + v_ScreenAlpha + ")";
	var allselect = document.getElementsByTagName("select");
	for (var i=0; i<allselect.length; i++)
	{
		allselect[i].style.visibility = "hidden";
	}
}
function ScreenClear()
{
	var objScreen = document.getElementById("WebSiteHiddenDiv");
	objScreen.innerHTML='';
	objScreen.style.top = "0px";
	objScreen.style.left = "0px";
	objScreen.style.width = 0 + "px";
	objScreen.style.height = 0 + "px";
	objScreen.style.display = 'none';
	var allselect = document.getElementsByTagName("select");
	for (var i=0; i<allselect.length; i++)
	{
		allselect[i].style.visibility = "";
	}
}//

function XmlHttpSetResult(IDinnerHTML,XmlHttpPage)
{
ScreenConvert();
var oBao = new ActiveXObject("Microsoft.XMLHTTP");
//特殊字符：+,%,&,=,?等的传输解决办法.字符串先用escape编码的.
//Update:2004-6-1 12:22
oBao.open("POST",XmlHttpPage,true);
oBao.send();

    if (IDinnerHTML!="0")
    {
    //服务器端处理返回的是经过escape编码的字符串.
    var strResult = unescape(oBao.responseText);
    
    eval("document.all."+IDinnerHTML).innerHTML=strResult;
    }
    ScreenClear();
}

function WebSiteCountTimer(IDinnerHTML,XxlHttpPage,Timer)
{
    IDinner=IDinnerHTML;XxlHttp=XxlHttpPage;Timers=Timer;
if (escape(document.referrer)!="")
{
XxlHttp+="?ComeFrom="+escape(document.referrer)
}
    XmlHttpSetResult(IDinner,XxlHttp);
    TimerFunction(IDinner,XxlHttp,Timers);
}
function TimerFunction(IDinnerHTML,XxlHttpPage,Timer)
{
    Timers=Timer;XxlHttp=XxlHttpPage;IDinner=IDinnerHTML;
    timer = window.setInterval("XmlHttpSetResult(IDinner,XxlHttp)",Timer*100);
}
function ShowPostBuyMoney(n1)
{
        msg=confirm("您确认要支付浏览吗？")	
		if (msg==0)
		{
			return; 
		}
		else
		{
		    JavascriptPostXml(''+n1+'',true);
		}
}//
function WebSite_SetMyArray()
{

	if (document.all.TopicParentID.value==0)
	{
	document.all.TopicSubjectID.length=1;
	return;
	}
var j=1;
document.all.TopicSubjectID.disabled=false;
document.all.TopicSubjectID.length=1;
	for (var i=0;i<WebSiteBoardArray.length;i++)
	{
		if ((WebSiteBoardArray[i][1]==document.all.TopicParentID.value)&&(WebSiteBoardArray[i][1]!=0))
		{
			document.all.TopicSubjectID.length=document.all.TopicSubjectID.length+1	;
			document.all.TopicSubjectID.options(j).value=WebSiteBoardArray[i][0];
			document.all.TopicSubjectID.options(j).text=WebSiteBoardArray[i][2];
			j=j+1
		}
	} 
	if (document.all.TopicSubjectID.length==1)
	{
	    document.all.TopicSubjectID.disabled=true;
	}
}
function WebSite_SetParentMyArray(id)
{	
	var j=1;
	for (var i=0;i<WebSiteBoardArray.length;i++)
	{
		if (WebSiteBoardArray[i][1]==0)
		{
			document.all.TopicParentID.length=document.all.TopicParentID.length+1;
			document.all.TopicParentID.options(j).value=WebSiteBoardArray[i][0];
			document.all.TopicParentID.options(j).text=WebSiteBoardArray[i][2];
			if (document.all.TopicParentID.options(j).value==id)
			{
				document.all.TopicParentID.options(j).selected=true;
				WebSite_SetMyArray();
			}//	
			j=j+1;
			//if (id==i)
		}
	}
	
}
function WebSite_SetTypeMyArray(id)
{
    document.all.TopicSubjectID.disabled=false;
	for (var i=0;i<document.all.TopicSubjectID.length;i++)
	{	if (id==document.all.TopicSubjectID.options(i).value)
		{
			document.all.TopicSubjectID.options(i).selected=true;
		}//
		
	}
	if (document.all.TopicSubjectID.length==1)
	{
	    document.all.TopicSubjectID.disabled=true;
	}

}//
function WebSite_SetJavascriptValueInfo(n,info)
{
    eval("document.all."+n).value=info;
}
function WebSite_SetValueTrue(n)
{

}
function WebSite_SetCheckTrue(n)
{
    eval("document.all."+n).checked='true';
}
function WebSite_CheckNub(NumCheck,nb)
{
	nr1=eval("document.all."+NumCheck).value;
	if (nr1=="")
	{
	   eval("document.all."+NumCheck).value=0; 
	}
	flg=0;
	str="";
	spc=""
	arw="";
	if (nb=="0")
	{
		cmp="0123456789."
	}
	else
	{
	    cmp="0123456789"
	}
	for (var i=0;i<nr1.length;i++)
	{
		tst=nr1.substring(i,i+1)
	if (cmp.indexOf(tst)<0)
		{
			flg++;
			str+=" "+tst;
			spc+=tst;
			arw+="^";
		}
	else{arw+="_";}
	}
	if (flg!=0)
	{
		if (spc.indexOf(" ")>-1) 
		{
			str+="和空格";
		}
		alert("只能填数字，你填了"+flg+"个不符合的字符:\n"+str);
		eval("document.all."+NumCheck).select();
		return false;
	}else{return true}
}
function WebSiteJavaScriptCheckAll(n)
{
    try 
    {
        for (var i=0;i<eval("document.all."+n).length;i++)
        {
            if (document.all.WebSiteChkAll.checked)
            {
                eval("document.all."+n)[i].checked=true;
            }
            else
            {
                eval("document.all."+n)[i].checked=false;
            }        
        }
　　} 
　　catch(e) {
　　
　　} 
}//
function WebSiteDeleteTopicInfo(id,type)
{
    msg=confirm("您确认要做此操作？")	
		if (msg==0)
		{
			return; 
		}
		else
		{
		    if (type==0)
		    {
		        JavascriptPostXml('actions=ManageTopic&WebSiteTopicStaticCheckBox='+id+'&actiontype=3',true);
		    }
		    else
		    {
		        JavascriptPostXml('actions=DeleteTopicInfo&TopicInfoID='+id+'',true);
		    }
		}
		
}
function WebSiteJavaScriptActPost()
{
    msg=confirm("您确认要做此操作？")	
		if (msg==0)
		{
			return; 
		}
		else
		{
        ScreenConvert();
	    document.all.WebSiteForm.target='WebSiteTarGet';
        document.all.WebSiteForm.action='TopicManagerAction.aspx';
        document.WebSiteForm.submit();
	    return;
	}
}//
function WebSiteJavaScriptManage(id)
{
    if (id.value=='7')
    {
        document.all.WebSiteMoveBar.style.display='';
    }
    else
    {
        document.all.WebSiteMoveBar.style.display='none';
    }
}
function JavaScriptOpenMidWinow(url,openname,widths,heights,scrolls)
{
	if (scrolls==1){
			newwindow=window.open(''+url+'',''+openname+'','fullscreen=0,toolbar=1,location=1,directories=1,status=1,menubar=1,scrollbars='+scrolls+',resizable=1,width='+widths+',height='+eval(heights-10)+'');
	}
	else
	{
		newwindow=window.open(''+url+'',''+openname+'','fullscreen=0,toolbar=0,location=0,directories=0,status=0,menubar=0,scrollbars='+scrolls+',resizable=0,width='+widths+',height='+heights+'');
	}
	newwindow.focus();
	newwindow.moveTo(''+(screen.width-widths)/2+'',''+(screen.height-heights)/2+'');
}
function JavaScriptWebSiteDiv(id,num)
{
	for (var i=1;i<=num;i++)
	{
		eval("document.all.MiniTableDiv"+i).style.display='none';
	    eval("document.all.MiniTable"+i).className='minitableno';
	}
	eval("document.all.MiniTable"+id).className='minitable';
	eval("document.all.MiniTableDiv"+id).style.display='';
}
function JavaScriptWebSiteUserLoad(n,nb)
{   
    var arr = showModalDialog("/WebSiteSystemInfo/UserUpLoadFile.aspx",window, "dialogWidth:400 px; dialogHeight:160 px; status:0; help:0");
	if ((arr=="undefiend")||(arr==null))
	{
	    return;
	}
		ScreenClear();
	eval("document.all."+n).src=arr;
    eval("document.all."+nb).value=arr; 
    return

}//
function JavaScriptWebSiteOpenModalDialogReturn(url,widths,heights,n,id)
{
    var arr = showModalDialog(''+url+'',window, "dialogWidth:"+widths+" px; dialogHeight:"+heights+" px; status:0; help:0");
    //var MyArray=new Array();
    //MyArray=arr.split("|");
    if ((arr=="undefiend")||(arr==null))
	{
	    return;
	}
    eval("document.all."+n).value=arr.split("|")[0];
    eval("document.all."+id).value=arr.split("|")[1];
}
function WebSiteUserReg()
{
    if (document.all.UserName.value=="")
    {
        JavaScriptWebSiteDiv('1','4');
        alert('请输入用户名称！');
        document.all.UserName.focus();
        return;
    }
    if (document.all.UserPassWord.value=="")
    {
        JavaScriptWebSiteDiv('1','4');
        alert('请输入用户密码！');
        document.all.UserPassWord.focus();
        return;
    }
    if (document.all.UserPassWord.value!=document.all.UserPassWord2.value)
    {
        JavaScriptWebSiteDiv('1','4');
        alert('两次密码输入不一致！');
        document.all.UserPassWord2.focus();
        return;
    }
    if (document.all.UserPassWordAnswer.value=="")
    {
        JavaScriptWebSiteDiv('1','4');
        alert('请输入密码答案！');
        document.all.UserPassWordAnswer.focus();
        return;
    }
    if (document.all.UserEmail.value.replace("@","")==document.all.UserEmail.value)
    {
        JavaScriptWebSiteDiv('1','4');
        alert('请输入有效的Email地址！');
        document.all.UserEmail.focus();
        return;
    }
    
    if (document.all.UserEmail.value!=document.all.UserEmail2.value)
    {
        JavaScriptWebSiteDiv('1','4');
        alert('两次Email地址输入不一致！');
        document.all.UserEmail2.focus();
        return;
    }
    
    if (!WebSite_CheckNub('UserMobile',1)) 
    {   
        JavaScriptWebSiteDiv('3','4');
        return ;
    }
     if (document.all.UserMobile.value=="0")
    {
        document.all.UserMobile.value="";
    }
    if (!WebSite_CheckNub('UserIdCard',1)) 
    {   
        JavaScriptWebSiteDiv('2','4');
        return ;
    }
     if (document.all.UserIdCard.value=="0")
    {
        document.all.UserIdCard.value="";
    }
    if (!WebSite_CheckNub('UserCode',1)) 
    {   
        JavaScriptWebSiteDiv('3','4');
        return ;
    }
    if (document.all.UserCode.value=="0")
    {
        document.all.UserCode.value="";
    }
    if (!WebSite_CheckNub('UserOICQ',1)) 
    {   
        JavaScriptWebSiteDiv('3','4');
        return ;
    }
     if (document.all.UserOICQ.value=="0")
    {
        document.all.UserOICQ.value="";
    }
    ScreenConvert();
    document.all.WebSiteForm.target='WebSiteTarGet';
    document.all.WebSiteForm.action='/UserInfo/UserRegAction.aspx';
    document.WebSiteForm.submit();
	return;

}//
function WebSiteJavaScriptEditUserInfo()
{
    if (document.all.UserPassWord.value=="")
    {
        JavaScriptWebSiteDiv('1','3');
        alert('请输入用户密码！');
        document.all.UserPassWord.focus();
        return;
    }
    
    if (document.all.UserEmail.value.replace("@","")==document.all.UserEmail.value)
    {
        JavaScriptWebSiteDiv('1','3');
        alert('请输入有效的Email地址！');
        document.all.UserEmail.focus();
        return;
    }
    if (!WebSite_CheckNub('UserMobile',1)) 
    {   
        JavaScriptWebSiteDiv('2','3');
        return ;
    }
     if (document.all.UserMobile.value=="0")
    {
        document.all.UserMobile.value="";
    }
    if (!WebSite_CheckNub('UserIdCard',1)) 
    {   
        JavaScriptWebSiteDiv('2','3');
        return ;
    }
     if (document.all.UserIdCard.value=="0")
    {
        document.all.UserIdCard.value="";
    }
    if (!WebSite_CheckNub('UserCode',1)) 
    {   
        JavaScriptWebSiteDiv('2','3');
        return ;
    }
    if (document.all.UserCode.value=="0")
    {
        document.all.UserCode.value="";
    }
    if (!WebSite_CheckNub('UserOICQ',1)) 
    {   
        JavaScriptWebSiteDiv('2','3');
        return ;
    }
     if (document.all.UserOICQ.value=="0")
    {
        document.all.UserOICQ.value="";
    }
    ScreenConvert();
    document.all.WebSiteForm.target='WebSiteTarGet';
    document.all.WebSiteForm.action='/UserInfo/EditUserInfoAction.aspx';
    document.WebSiteForm.submit();
	return;

}//

function WebSiteJavaScrptFortGotPassWord()
{
    if (document.all.UserName.value=="")
    {
        alert('请输入用户名称！');
        document.all.UserName.focus();
        return;
    }
    if (document.all.FortGotPassWord.value=="")
    {          
            if (document.all.UserPassWordAnswer.value=="")
            {
                alert('请输入密码答案！');
                document.all.UserPassWordAnswer.focus();
                return;
            }
    }
    else
    {
            if (document.all.UserPassWord.value=="")
            {
                alert('请输入新密码！');
                document.all.UserPassWord.focus();
                return;
            }
            if (document.all.UserPassWord.value!=document.all.UserPassWord2.value)
            {
                alert('两次密码输入不一致！');
                document.all.UserPassWord2.focus();
                return;
            }
    }//
    document.all.WebSiteForm.target='WebSiteTarGet';
    document.all.WebSiteForm.action='/UserInfo/ForgotPasswordAction.aspx';
    document.WebSiteForm.submit();
	return;
}//
function WebSiteJavaScriptSetPage(n,nb,id)
{
//if (typeof(vBobjects[n]) == "undefined")
if (eval("document.all."+n).length==null)
{
    if (id==1)
    {
    eval("document.all."+n).className='postieed';
    eval("document.all."+nb).style.display='none';
    }
    else
    {
    eval("document.all."+n).className='postie';
    eval("document.all."+nb).style.display='';
    }
}
else
{
 for (var i=0;i<eval("document.all."+n).length;i++)
    {
        if (id==1)
        {
        eval("document.all."+n+"["+i+"]").className='postieed';
        eval("document.all."+nb+"["+i+"]").style.display='none';
        }
        else
        {
        eval("document.all."+n+"["+i+"]").className='postie';
        eval("document.all."+nb+"["+i+"]").style.display='';
        }
    }
}

    


}

function WebSiteJavaScriptWindowsClose()
{
    window.opener=null;
    parent.window.close();
}

function WebSiteJavaScriptJudgeEmptyValue(html)
{
    html = html.toLowerCase();
    var re = / /g; 
    html = html.replace(re,"");
      re = /'/g; 
    html = html.replace(re,""); 
    re = /&nbsp;/g; 
    html = html.replace(re,""); 
    re = /<p><\/p>/g; 
    html = html.replace(re,"");
    html = html.replace(/\r\n/g, '');
	return html;
}
function JsSearchTopic()
{
    if (document.all.KeyWord.value=="")
        {
            alert('请输入查询关键字！');
            document.all.KeyWord.focus();
            return;
        }
    var url="/Forums/DeFault.aspx?type=7&KeyWord="+escape(document.all.KeyWord.value);
    url +="&boardid="+document.all.TopicParentID.value;
    url +="&topicmode="+document.all.TopicSubjectID.value;
    url +="&ActAtrickTime="+document.all.ActAtrickTime.value;
        if (document.all.s1.checked==true)
        url +="&s1="+document.all.s1.value;
        if (document.all.s2.checked==true)
        url +="&s2="+document.all.s2.value;
        if (document.all.s3.checked==true)
        url +="&s3="+document.all.s3.value;
        if (document.all.s4.checked==true)
        url +="&s4="+document.all.s4.value;
        if (document.all.s5.checked==true)
        url +="&s5="+document.all.s5.value;
        if (document.all.s6.checked==true)
        url +="&s6="+document.all.s6.value;
        if (document.all.s7.checked==true)
        url +="&s7="+document.all.s7.value;
        if (document.all.s8.checked==true)
        url +="&s8="+document.all.s8.value;
        //alert();
    window.location=''+url+'';
}





function WebSiteJavascriptCheckBoard(id,div)
{
    if (eval("document.all."+id).checked==true)
    {
        eval("document.all."+div).style.display=''
    }
    else
    {
        eval("document.all."+div).style.display='none'
    }
}//
function DosJavaScriptPost(iframe,url)
{
    ScreenConvert();
    var framsForm=document.frames[iframe].document.forms[0];
    framsForm.target=iframe;
    framsForm.action="/UserUploadFile.aspx?"+''+url+'';
    framsForm.submit();
}
function DosJavaScriptUpLoadFile(paraMeter1,paraMeter2,paraMeter3,paraMeter4,paraMeter5,paraMeter6,paraMeter7)
{
    var url="paraMeter2="+''+paraMeter2+''+"&paraMeter3="+''+paraMeter3+''+"&paraMeter4="+''+paraMeter4+''+"&paraMeter5="+''+paraMeter5+''+"&paraMeter6="+''+paraMeter6+''+"&paraMeter7="+''+paraMeter7+'';
    DosJavaScriptPost(paraMeter1,url);
}

//onclick="JavaScriptUpLoadFile('UploadFileIframe','TopicImages','','CheckBoxUploadFile','CheckBoxUserFile','InputFileUrlDiv','UserUpLoadFileDiv'\
function DosJavaScriptUpLoadFileOk(paraMeter1,paraMeter2,paraMeter3,paraMeter4,paraMeter5,paraMeter6,paraMeter7)
{
    ScreenClear();
   // if (paraMeter1!='')
    //{
      //  alert(paraMeter1);
      //  return;
   // }0','CheckBoxUploadFile','CheckBoxUserFile','InputFileUrlDiv','UserUpLoadFileDiv'
    if (paraMeter2!='')
    {
        eval("document.all."+paraMeter2).value=''+paraMeter1+'';
    }    
    if (paraMeter3!='')
    {
        eval("document.all."+paraMeter3).style.src=''+paraMeter1+'';
    }
    
    if (paraMeter5!='')
    {
        JsCheckUpLoadFile(1,''+paraMeter4+'',''+paraMeter5+'',''+paraMeter6+'',''+paraMeter7+'');
    }
}
function JsCheckUpLoadFile(n1,n2,n3,n4,n5)
{
    if (n1==0)
    {
        eval("document.all."+n2).checked=null;
        eval("document.all."+n3).checked='true';
        eval("document.all."+n4).style.display='none';
        eval("document.all."+n5).style.display='';
    }
    else
    {
        eval("document.all."+n2).checked=null;
        eval("document.all."+n3).checked=null;
        eval("document.all."+n4).style.display='';
        eval("document.all."+n5).style.display='none';
    }//
}
function JscriptUserFaceDiv(n1,n2)
{
    if (n1==1)
    {
        eval("document.all."+n2).style.display='';
        eval("document.all."+n2).style.left=(document.body.scrollWidth/2)-200 + "px";
        eval("document.all."+n2).style.top=(document.body.scrollHeight/2)-200 + "px";
        
        var allselect = document.getElementsByTagName("select");
	    for (var i=0; i<allselect.length; i++)
	    {
		    allselect[i].style.visibility = "hidden";
	    }
    }
    else
    {
     eval("document.all."+n2).style.display='none';
         var allselect = document.getElementsByTagName("select");
	    for (var i=0; i<allselect.length; i++)
	    {
		    allselect[i].style.visibility = "";
	    }
    }
}
	
function JavascriptSetUserFace(n1,n2,n3,n4)
{
//alert(n1);
 eval("document.all."+n2).style.display='none';
 eval("document.all."+n3).value=n1;
 var allselect = document.getElementsByTagName("select");
	for (var i=0; i<allselect.length; i++)
	{
		allselect[i].style.visibility = "";
	}
}
function JavaScriptSearchUser()
{
var url="/UserInfo/UserList.aspx?SearchUserName="+escape(document.all.SearchUserName.value);
if (document.all.UserSexRad[1].checked)
{
    url +="&UserSexRad=M";
    }
    if (document.all.UserSexRad[2].checked)
{
    url +="&UserSexRad=F";
    }
    url +="&SearchUserOld="+document.all.SearchUserOld.value;
    window.location=''+url+'';
}

//新增加
var XmlPostHttp = new ActiveXObject("Microsoft.XMLHTTP");
function JavaScrptWebSitePost()
{
//    document.all.FormLoading.target='WebSiteTarGet2';
//    document.all.FormLoading.action='/PostAction.aspx';
//    document.FormLoading.submit();
//	return;
    ScreenConvert();
    XmlPostHttp.open("POST",'/PostAction.aspx?',true);
    XmlPostHttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded"); 
    var aParams=new Array();
    var sends;
    for (var i=0;i<document.all.FormLoading.elements.length;i++)
    {       
        if ((document.all.FormLoading.elements[i].type=="radio")||(document.all.FormLoading.elements[i].type=="checkbox"))
        {
            if (document.all.FormLoading.elements[i].checked)
            {
                var sParam=encodeURIComponent(document.all.FormLoading.elements[i].name);
                sParam+="=";
                sParam+=encodeURIComponent(document.all.FormLoading.elements[i].value);
                aParams.push(sParam);
            }
        }
        else
        {
            var sParam=encodeURIComponent(document.all.FormLoading.elements[i].name);
            sParam+="=";
            sParam+=encodeURIComponent(document.all.FormLoading.elements[i].value);
            aParams.push(sParam);
        }
    }
    sends=aParams.join("&");
    //alert(sends)
    XmlPostHttp.setrequestheader("content-length",sends.length);
    XmlPostHttp.send(sends);
    XmlPostHttp.onreadystatechange = JavascriptReadyStatechange;//指定响应函数
}
function JavascriptReadyStatechange()
{
    if (XmlPostHttp.readyState==4)
    {
        if (XmlPostHttp.status==200)
        {
            if (XmlPostHttp.responseText!='')
            {
            eval(unescape(XmlPostHttp.responseText));
            }
            ScreenClear();
        }
        else
        {
            alert('系统错误！');
            ScreenClear();
        }
    }
}
function JavascriptPostXml(sends,n2)
{
    XmlPostHttp.open("POST",'/PostAction.aspx?',true);
    XmlPostHttp.setRequestHeader("Content-Type", "application/x-www-form-urlencoded"); 
    XmlPostHttp.setrequestheader("content-length",sends.length);
    XmlPostHttp.send(sends);
    if (n2==true)
    {
        XmlPostHttp.onreadystatechange = JavascriptReadyStatechange;//指定响应函数
    }
}//

function DosJavascriptDivHtml(id,value)
{
    var vobj=document.getElementById(id);
	if (vobj!=null)
	{
	    vobj.innerHTML=''+value+'';
	}
}

var Dos_UserOnlineRefresh=0;
function DosJscriptRefreshWebSiteInfo(time,comeFrom,currentUrl,browserTitle)
{
	if (window.RefreshWebSiteInfo)
		clearTimeout(RefreshWebSiteInfo)
    RefreshWebSiteInfo=setTimeout("DosJscriptRefreshWebSiteInfo('"+time+"','"+comeFrom+"','"+currentUrl+"','"+browserTitle+"')",time);
    JavascriptPostXml("actions=RefreshWebSiteInfo&comeFrom="+comeFrom+"&currentUrl="+currentUrl+"&browserTitle="+browserTitle+"&UserOnlineRefresh="+Dos_UserOnlineRefresh+"",true);
    Dos_UserOnlineRefresh++;
    //alert();
}//

function WebSiteShowViewPower(n1,n2)
{
    JsXmlQueryHttp('/SelectGroupRole.aspx?type='+n1+'&Value='+eval("document.all."+n2).value+'&Name='+n2+'','XmlQueryID');
    SetlistAssignedRoles();
    
}//
function JsXmlQueryHttp(n1,n2)
{
    ScreenConvert();
    var XmlQueryHttp = new ActiveXObject("Microsoft.XMLHTTP");
    XmlQueryHttp.open("GET",''+n1+'',false);
    XmlQueryHttp.send(null);


    if (!document.getElementById(n2))
		{
			var newNode = document.createElement("<div id="+n2+" style=\"z-index:3333;position:absolute;\"></div>");
			document.body.appendChild(newNode);
		}   
		eval("document.all."+n2).style.display='';
		eval("document.all."+n2).style.left=(document.body.scrollWidth-400)/2;
		eval("document.all."+n2).style.top=(document.body.scrollHeight-300)/2;
    eval("document.all."+n2).innerHTML=unescape(XmlQueryHttp.responseText);
}


function Dos_DeleteMessages()
{
var shimName="Dos_RefreshDiv";
	var divObj=document.getElementById(shimName);
	if (divObj!=null)
{
	var shimNames="Dos_RefreshDiv"+divObj.openID;
	var divObjs=document.getElementById(shimNames);
divObjs.style.display='none';
	divObjs.removeAttribute("id");
	divObj.openID=parseInt(divObj.openID)-1;

	if (parseInt(divObj.openID)!=0)
	{
		shimNames="Dos_RefreshDiv"+divObj.openID;
		divObjs=document.getElementById(shimNames);
		divObjs.style.display='';
	}
	else
	{
		divObj.style.display='none';
var iframeName="Dos_RefreshIframe"
		var iframeObj=document.getElementById(iframeName);
		iframeObj.style.display='none';
	}
}
}//

function Dos_RefreshMessagesIDV()
{
if (window.DosRefreshMessages)
		clearTimeout(DosRefreshMessage)
var shimName="Dos_RefreshDiv";
	var divObj=document.getElementById(shimName);
	if (divObj!=null)
{
	if (divObj.style.display=='')
{
	var DosRefreshMessages=setTimeout("Dos_RefreshMessagesIDV()",200);


divObj.style.left=parseInt(document.body.clientWidth)-200
divObj.style.top=parseInt(document.body.clientHeight)-150+parseInt(document.body.scrollTop)


var iframeName="Dos_RefreshIframe"
		var iframeObj=document.getElementById(iframeName);
	iframeObj.style.left=divObj.style.left;
iframeObj.style.top=divObj.style.top;
}
}
}//