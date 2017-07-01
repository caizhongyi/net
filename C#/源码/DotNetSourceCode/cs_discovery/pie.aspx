<%@ Page language="c#" Codebehind="pie.aspx.cs" AutoEventWireup="false" Inherits="cs_discovery.pie_customers" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>pie_customers</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<P align="center"><FONT face="宋体" size="6"><STRONG>ASP.NET中使用图形编程实现带超链接的饼图</STRONG></FONT></P>
			<P align="center"><FONT face="宋体">袁永福 2008-1-9</FONT></P>
			<P><FONT face="宋体">点击返回<A href="default.htm">主页面</A>。</FONT></P>
			<P><FONT face="宋体"> 本页面查询数据库获得若干个客户的订单统计信息，根据获得的数据生成一个饼图，饼图中的每一个项目带有超链接，能链接到指定客户的详细订单信息。</FONT></P>
			<P><FONT face="宋体"><FONT face="宋体">饼图形状________________________________________________________________</FONT></FONT></P>
			<P>
				<asp:Label id="lblResult" runat="server">Label</asp:Label></P>
			<P><FONT face="宋体">展示的数据______________________________________________________________</FONT></P>
			<P>
				<asp:DataGrid id="DataGrid1" runat="server" Width="100%"></asp:DataGrid></P>
		</form>
	</body>
</HTML>
