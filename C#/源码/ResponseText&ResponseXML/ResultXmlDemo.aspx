<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResultXmlDemo.aspx.cs" Inherits="ResultXmlDemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    
    <%--不同浏览器的XMLHttpRequest.responseXML的操作方法
现在最常用的web浏览器有 IE和FF
IE就不用说了
FF还是蛮不错的支持页面调试功能非常的强大
美中不足就是吃内存太多
 
用于处理xml文档的DOM元素属性
childNodes 返回当前元素所有子元素的数组
firstChild  返回当前元素的第一个下级子元素
lastChild  返回当前元素的最后一个子元素
nextSibling 返回紧跟在当前元素后面的子元素
nodeValue 指定表示元素值的读/写属性
parentNode 返回元素的父结点
previousSibling 返回紧邻当前元素之前的元素
 
用于遍历xml文档的DOM元素方法
getElementById(id) 获取有指定唯一ID属性值文档中的元素
getElementsByTagName(name) 返回当前元素中有指定标记名的子元素的数组
hasChildNodes() 返回以各布尔值 只是元素是否有子元素
getAttribute(name) 返回元素的属性值 属性由name指定--%>


    <script language="javascript" type="text/javascript">
		
		//创建XMLHttpReqest对象ajax的核心对象
		//导步调用
      var xmlHttp;
      function CreateXMLHttpRequest()
      {
      //如果是ie
         if(window.ActiveXObject)
         {
            xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");    
            
         }
         //如果产火狐等其它浏览器在ie7.0以上版本中也有此对象
         else
         {
           xmlHttp=new XMLHttpRequest();
         }
      }      
      function updateTotal()
      {
        //frm =document.forms[0];   
        url="Handler2.ashx";

 
      //初始化对服务器的请求
        xmlHttp.open("GET",url,true);//true表示要异步调用
        xmlHttp.onreadystatechange=doUpdate;//服务器完成后,客户端处理事件
        //发送请求
        xmlHttp.send();   
        return false;
      }      
      function doUpdate()
      {
        //表示请示处理完成 
        if(xmlHttp.readyState==4)
        {         
          var xmlDom=xmlHttp.ResponseXML;
          //xmlDom.childNodes[0]是XML节点  
          var tot=document.forms[0].elements["TOT"];       
         // tot.value=xmlDom.childNodes[1].text; //ok
         
 //ok2 
//         var root=xmlDom.childNodes[1];
//         var student1=root.childNodes[0]; 
//         var wah=student1.childNodes[0];
//         tot.value=wah.childNodes[0].nodeValue;
// ok2
          
          
        //ok3          
          var root=xmlDom.childNodes[1];//root节点
          
          var ss=xmlDom.getElementsByTagName('student');
          var out="";
           for (var i = 0; i < ss.length; i++)
             {
             out += "" + ss[i].getElementsByTagName("name")[0].childNodes[0].nodeValue + "   ";
             }
         
         tot.value=out;

//ok3
         
         
      
 
         
       
        }
        if(xmlHttp.readyState==0)
        {         
          document.forms[0].elements["TOT"].value="没有初始化";
        }
        if(xmlHttp.readyState==1)
        {         
          document.forms[0].elements["TOT"].value="装载中";
        }
        if(xmlHttp.readyState==3)
        {         
          document.forms[0].elements["TOT"].value="交互中";
        }
        
       
        
      }
    </script>

</head>
<body onload="CreateXMLHttpRequest();">
    <form id="form1" runat="server">
        <div>
            &nbsp;
            <asp:Label id="Label3"  runat="server">Result</asp:Label>
            
				<input type="text" id="TOT"
				size="30">
				<div id="nameList" >
                    &nbsp;</div>
            &nbsp;
			
            &nbsp;
			<hr />
			<input type="button" value="click me" onclick="updateTotal()" /></div>
    </form>
</body>
</html>
