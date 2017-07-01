<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xHTML1/DTD/xHTML1-transitional.dtd"> 
<html XMLns="http://www.w3.org/1999/xHTML" lang="gb2312"> 
<head> 
<meta http-equiv="Content-Type" content="text/HTML; charset=gb2312" /> 
<meta name="精投联盟" content="" />
<meta http-equiv="Page-Enter" content="revealTrans(duration=5.0, transition=12)" />
<meta http-equiv="Page-Exit" content="revealTrans(duration=5.0, transition=12)" />
<meta name ="keywords" content="车辆买卖与服务, 培训,婚恋交友,求职,宠物/宠物用品，二手物品交易, 招聘, 房屋租售" />
<meta name="description" content="最全的资讯网站" />
<title>精投联盟</title> 
<style  type="text/css">

A:visited{TEXT-DECORATION:none;COLOR:#009999} 
A:link{text-decoration:none;color:#858787} 
A:hover{TEXT-DECORATION:COLOR: #eef2fa;FONT-WEIGHT:bold; 
A.1:link{text-decoration:none} 
A.1:visited{TEXT-DECORATION:none;COLOR:#000000} 
A.1:hover{TEXT-DECORATION:none;COLOR:#eef2fa;FONT-STYLE:italic} 

.borderStyle
{
      
      border-style:solid;
      border-width:2px;
      border-color:#eef2fa;
}

.fontcolor
{
     color:#858787;
}


</style>
<script language="javascript" type="text/javascript">
// <!CDATA[

function DIV1_onclick() {

}

// ]]>
</script>
</head> 
<body style=" font-size:smaller ; text-align:center;  " > 
<form runat="server">
<div id="form" style=" width:1200px;  ">

<div id="head" style="width:1200px; height :187px; text-align :left" >
<div style=" width:100%; height: 20px">今天是:<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    &nbsp;
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></div>
<div style=" width:100%; height: 78px;">
<div id="logo" style=" float :left">
<img src ="image/logo.gif" style="width: 85px; height: 85px"/>
</div>
<div id="top_flash">
<object  codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0" id="图片变换" style="width: 744px; height: 88px">
<param name="allowScriptAccess" value="sameDomain" />
<param name="movie" value="http://localhost:3875/MediaSystem/图片变换.swf" />
<param name="quality" value="high" />
<param name="bgcolor" value="#ffffff" />
<embed src="http://localhost:3875/MediaSystem/图片变换.swf" quality="high" bgcolor="#ffffff" width="600" height="100" name="图片变换" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />
</object>
</div>
</div>
<div style=" width:100%; height: 18px; text-align :right" class="fontcolor">
<a href ="#" onclick="var strHref=window.location.href;this.style.behavior='url(#default#homepage)';this.setHomePage('http://www.baidu.com');" style="CURSOR: hand">设为首页</a>|
<a href="#" onclick="window.external.addFavorite('http://www/wb66.cn','精投联盟')" title="你的公司名称">加入收藏</a></div>
<div style=" width:100%; height: 50px"></div>
</div>

<div id="main">
<div id="left" style="float :left; height: 728px; width:432px">
<div style=" width:432px;  height:200px;  ">
<div id="login" style=" width:100%;  height:115px; background-color:#eef2fa; border-bottom-style:hidden ">
    &nbsp;<br>
    &nbsp;
    <table>
        <tr>
            <td align="center" rowspan="2" style="width: 99px; height: 25px" class="fontcolor">
                用户名:</td>
            <td rowspan="2" style="width: 125px; height: 25px">
      <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
            <td align="center" rowspan="3" style="width: 124px">
                <asp:ImageButton ID="ImageButton1" runat="server" Width="93px" ImageUrl="~/image/LoginButton.gif" Height="43px" /></td>
        </tr>
        <tr>
        </tr>
        <tr>
            <td align="center" style="width: 99px; height: 17px" class="fontcolor">
           密码:</td>
            <td style="width: 125px; height: 17px">
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
        </tr>
    </table>
             
</div>

<div style=" width:100%;border-bottom-color: #eef2fa; border-bottom-width:2px; border-bottom-style:solid">
</div>

<div style =" text-align :right; margin-top:5px"><a href ="#">用户注册</a>|<a href ="#"> 忘记密码</a></div>
</div>


<div id="advMessage" style=" width:434px; height: 263px;"></div>
<div id="advMessage1" style=" width:434px; height: 284px;"> </div>

</div>

<div id="right" style=" width:750px; height :726px ; text-align:left ">

<div id="right_01"  style=" width:100%; height :194px;border-style:solid;  border-width:2px; border-color:#eef2fa;" >
<div style=" background-image:url(image/head_01.gif) ; width:100%; height :48px; background-repeat:no-repeat" ></div>

<div id="right_flash"><object  codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0" id="Object1" style="width: 749px; height: 144px">
<param name="allowScriptAccess" value="sameDomain" />
<param name="movie" value="图片变换.swf" />
<param name="quality" value="high" />
<param name="bgcolor" value="#ffffff" />
<embed src="图片变换.swf" quality="high" bgcolor="#ffffff" width="600" height="100" name="图片变换" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />
</object>
</div>
</div>

<div id="scroe" style="width:760px; height: 267px;"></div>
<div id="messageCenter" style="width:760px; height: 285px;"></div>


</div>


<div id="end " style=" width:100%; height :49px; background-color:f0f2f2">
<div id="Advertising" style=" width:100%; height :97px ; border-bottom-style:solid ; border-bottom-color:#858787; border-bottom-width:1px;"><img src ="image/logo.gif" style="width: 85px; height: 85px"/>
   <a href ="http://www.baidu.com"><img  src ="image/logo.gif" style="width: 85px; height: 85px;border-width:0" /></a> 
  <a href ="http://www.baidu.com"><img  src ="image/logo.gif" style="width: 85px; height: 85px;border-width:0" /></a> 
  <a href ="http://www.baidu.com"><img  src ="image/logo.gif" style="width: 85px; height: 85px;border-width:0" /></a> 
  <a href ="http://www.baidu.com"><img  src ="image/logo.gif" style="width: 85px; height: 85px;border-width:0" /></a> 
  <a href ="http://www.baidu.com"><img  src ="image/logo.gif" style="width: 85px; height: 85px;border-width:0" /></a> 
  <a href ="http://www.baidu.com"><img  src ="image/logo.gif" style="width: 85px; height: 85px;border-width:0" /></a> 
  <a href ="http://www.baidu.com"><img  src ="image/logo.gif" style="width: 85px; height: 85px;border-width:0" /></a> 
  <a href ="http://www.baidu.com"><img  src ="image/logo.gif" style="width: 85px; height: 85px;border-width:0" /></a> 
  <a href ="http://www.baidu.com"><img  src ="image/logo.gif" style="width: 85px; height: 85px;border-width:0" /></a> 
  <a href ="http://www.baidu.com"><img  src ="image/logo.gif" style="width: 85px; height: 85px;border-width:0" /></a>
  <a href ="http://www.baidu.com"><img  src ="image/logo.gif" style="width: 85px; height: 85px;border-width:0" /></a> 
   </div>
<div  style=" width:100%; height :30px;  padding-top:10px">
<a href ="#">关于我们</a>  | 
<a href ="#"> 广告合作 </a> |
<a href ="#"> 法律声明 </a> |
<a href ="#">联系站长</a>   |
<a href ="#">网站地图</a>  | 
<a href ="#"> 网站搜索</a> |
<a href ="#">简 繁</a>  | 
<a href ="#"> Top ↑</a>
</div>
<div  style=" width:100%; height :30px ;  color:#858787">Copyright ©2008-2009 精投联盟  </div>

</div>

</div>
</div>
</form>

</body> 
</html> 
