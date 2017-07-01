<%@ Page language="c#" Codebehind="xslcreatecode.aspx.cs" AutoEventWireup="false" Inherits="cs_discovery.xslcreatecode" 
 validateRequest="false"
%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>xslcreatecode</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<P align="center"><FONT face="宋体" size="6">基于XSLT的带插件的代码生成器</FONT></P>
			<P align="center"><FONT face="宋体">袁永福 2008-1-15</FONT></P>
			<P><FONT face="宋体">本页面演示了使用C#分析Access数据库结构，然后使用XSLT技术根据表结构来自动生成各种源代码，使用XSLT文件作为插件。<BR>
					<A href="default.htm">点击返回主页面</A>。</FONT></P>
			<P><FONT face="宋体">数据库名称:
					<asp:Label id="lblDBName" runat="server">Label</asp:Label></FONT></P>
			<P><FONT face="宋体"> 数据表名:
					<asp:DropDownList id="cboTable" runat="server" DESIGNTIMEDRAGDROP="26" Width="224px"></asp:DropDownList>&nbsp;</FONT><FONT face="宋体">XSLT模板名:
					<asp:DropDownList id="cboXSLT" runat="server" Width="160px"></asp:DropDownList>
					<asp:Button id="cmdRefresh" runat="server" Text="刷新系统"></asp:Button></FONT></P>
			<P><FONT face="宋体">
					<asp:Button id="cmdCreate" runat="server" Text="创建代码"></asp:Button></FONT></P>
			<P><FONT face="宋体">生成的源代码<BR>
				</FONT>
				<asp:Label id="lblResult" runat="server" EnableViewState="False">Label</asp:Label></P>
		</form>
	</body>
</HTML>
