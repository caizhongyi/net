// JScript 文件
function  DoNetBbsSetTopPage()  
{     
	if  (self.location!=top.location)  
	{  
		parent.window.document.all(self.name).style.height=document.body.scrollHeight  +  5; 
		parent.document.all.DoNetBbsParentTableID.height=document.body.scrollHeight; 
		if (parent.document.all.DoNetBbsParentTableID.height<window.screen.height-184)
		{
			parent.document.all.DoNetBbsParentTableID.height=window.screen.height-184; 
		}
	}  
} 

function JsOpenModalDialog(url,widths,heights,less)
{
	if (less)
	{
	var arr = showModelessDialog(''+url+'',window, "dialogWidth:"+widths+" px; dialogHeight:"+heights+" px; help:no;scroll:no;status:no");
	}
	else
	{
		var arr = showModalDialog(''+url+'',window, "dialogWidth:"+widths+" px; dialogHeight:"+heights+" px; help:no;scroll:no;status:no");
		}
}
function JsPost()
{
    document.all.FormLoading.target='IframeTarGet';
    document.FormLoading.submit();
}

function JsDeleteInfo(n1)
{
     msg=confirm("您确认要删除该吗？")	
		if (msg==0)
		{
			return; 
		}
		else
		{
		    window.open(''+n1+'','IframeTarGet');
		}
}
//新增加

function JsXmlHttp(n1,n2)
{
var XmlRequestHttp = new ActiveXObject("Microsoft.XMLHTTP");
//特殊字符：+,%,&,=,?等的传输解决办法.字符串先用escape编码的.
//Update:2004-6-1 12:22
XmlRequestHttp.open("GET",n1,false);
XmlRequestHttp.send(null);

    if (n2!="")
    {
        var strResult = unescape(XmlRequestHttp.responseText);
        eval("document.all."+n2).innerHTML=strResult;
    }
}
function JsRefreshBoard()
{
    //var obj=window.opener;
    window.opener.JsXmlHttp('Board/BoardTree.aspx','BoardTreeTable');
}
function WebSiteShowViewPower(n1,n2)
{
    JsXmlQueryHttp('/SelectGroupRole.aspx?type='+n1+'&Value='+eval("document.all."+n2).value+'&Name='+n2+'','XmlQueryID');
    SetlistAssignedRoles();
    
}//
function JsXmlQueryHttp(n1,n2)
{
    ScreenConvert("document");
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


function ScreenConvert(doc)
{
return
}
function ScreenClear(doc)
{
return
}//

function JsUserInfoTable(n1)
{
    for(var i=1;i<=2;i++)
    {
        eval("document.all.UserInfoTitle"+i).style.backgroundColor='fff'
        eval("document.all.UserInfoTable"+i).style.display='none';
    }//
    eval("document.all.UserInfoTable"+n1).style.display='';
    eval("document.all.UserInfoTitle"+n1).style.backgroundColor='ccc'
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