<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QQscript.aspx.cs" Inherits="Ni_QQ_QQscript" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>在线咨询列表</title>
    <script src="OnlineQQ/prototype.lite.js" type="text/javascript"></script>
<script src="OnlineQQ/moo.fx.js" type="text/javascript"></script>
<script src="OnlineQQ/moo.fx.pack.js" type="text/javascript"></script>
<style>
body {
	font:12px Arial, Helvetica, sans-serif;
	color: #000;
}
#container {
	width: 500px;
}
H1 {
	font-size: 11px;
	margin: 0px;
	width: 91px;
	cursor: pointer;
	height: 22px;
	line-height: 20px;	
}
H1 a {
	display: block;
	padding-top: 1px;
	padding-right: 8px;
	padding-bottom: 0px;
	padding-left: 8px;	
	width: 91px;
	color: #000;
	height: 22px;
	text-decoration: none;	
	moz-outline-style: none;
	background-image: url(OnlineQQ/title.gif);
	background-repeat: repeat-x;
}
.content{
	padding-left: 8px;
}
</style>
</head>
<body>

		<H1 class="title" align="center" ><A href="javascript:void(0)">即时聊天</a></H1>
		<div class="content" align="left" id="ZiXun" runat=server >

 		</div>

		
		<H1 class="title" align="center"><a href="javascript:void(0)">更多了解</a></H1>
		<div class="content" id="NiYeWu" runat=server>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="http://www.cnblogs.com/powertalkbox" target=_blank><img border="0" src="OnlineQQ/zi-head.gif" onmousemove="this.border=1" onmouseout="this.border=0"></a><br>
&nbsp;&nbsp;PowerTalkBox
		&nbsp;&nbsp;</div>

	<script type="text/javascript">
		var contents = document.getElementsByClassName('content');
		var toggles = document.getElementsByClassName('title');
	
		var myAccordion = new fx.Accordion(
			toggles, contents, {opacity: true, duration: 400}
		);
		myAccordion.showThisHideOpen(contents[0]);
	</script>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
