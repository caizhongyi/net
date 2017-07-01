<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>网吧广告系统</title>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" /><style type="text/css">
<!--
body {
	
	background-repeat: repeat;
	background-position:center;
	background-color: #18244D;
	text-align:center;
	
}
-->
</style>
<link href="App_Themes/login.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
a:link {
	text-decoration: none;
	color: #009999;
}
a:visited {
	text-decoration: none;
	color: #996600;
}
a:hover {
	text-decoration: none;
	color: #006699;
}
a:active {
	text-decoration: none;
	color: #00CCCC;
}
-->
</style>
<link href="App_Themes/pager.css" rel="stylesheet" type="text/css" />

<link href="Subpage.css" rel="stylesheet" type="text/css" />

</head>
<body>
<form runat="server" method="post" >
<div class="login_all" >
<div class="loginhead"></div>
    <div class="loginLeft" style="float:left" ></div>
    <div class="Login" style="float:left" >
	   
    
	<div  style=" font-weight:normal; margin-left:180px; margin-top:145px; text-align:left">
    <p>
        &nbsp;
	用户名：<asp:TextBox ID="LoginName" runat="server" Width="193px"></asp:TextBox>
        </p> 
	</div>
	
	<div  style="font-weight:normal; margin-left:180px; margin-top:60px;text-align:left">
	<p>
        &nbsp; 密码:&nbsp; &nbsp; &nbsp;
        <asp:TextBox ID="LoginPwd" runat="server" TextMode="Password" Width="193px" ></asp:TextBox>
        </p>
      
	</div>
	<div class="loginBt">
        &nbsp;
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="登入"
            Width="63px" Height="32px" />
        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="Button2" runat="server" Height="32px"
            OnClick="Button2_Click" Text="清空" Width="63px" />
        &nbsp; &nbsp;&nbsp;
	</div>
	  

  
</div>
    <div class="loginRight" style="float:left"></div>
	<div class="loginfoot" style="float:left"></div>
</div>
</form>
</body>
</html>
