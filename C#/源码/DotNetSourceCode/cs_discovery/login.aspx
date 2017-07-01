<%@ Page language="c#" Codebehind="login.aspx.cs" AutoEventWireup="false" Inherits="cs_discovery.login" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>login</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<P align="center"><FONT face="宋体" size="6">ASP.NET使用验证码技术的演示页面</FONT></P>
			<P align="center"><FONT face="宋体">袁永福 2008-1-14</FONT></P>
			<P align="left"><FONT face="宋体">本页面演示了使用C#在ASP.NET中使用图形编程来实现验证码技术。<A href="default.htm">点击返回主页面</A>。</FONT></P>
			<P><FONT face="宋体">请输入用户名:
					<asp:TextBox id="txtUserName" runat="server" Width="224px"></asp:TextBox></P>
			</FONT>
			<P><FONT face="宋体">请输入密码&nbsp; :
					<asp:TextBox id="txtPassword" runat="server" Width="224px" TextMode="Password"></asp:TextBox></FONT></P>
			<P><FONT face="宋体">请输入验证码:
					<asp:TextBox id="txtCheckCode" runat="server" Width="224px"></asp:TextBox>
					<img src="checkimage.aspx" title='看不清楚，双击图片换一张。' ondblclick="this.src = 'checkimage.aspx?flag=' + Math.random() "
						border="1"> 
					<!--
						这里的 ondblclick 代码中采用一个毫无意义的flag参数可有效的防止浏览器缓存图片
						使得显示的验证码图片一定是最新的图片
					-->
					<span>看不清楚，双击图片换一张。</span> </FONT>
			</P>
			<FONT face="宋体">
				<P>
					<asp:Button id="cmdOK" runat="server" Text="确定"></asp:Button></P>
				<P>
					<asp:Label id="lblResult" runat="server">##</asp:Label></P>
			</FONT>
		</form>
	</body>
</HTML>
