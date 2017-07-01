<%@ Page validateRequest="false" language="c#" Codebehind="recordxpath.aspx.cs" AutoEventWireup="false" Inherits="cs_discovery.recordxpath"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>recordxpath</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<P><FONT face="宋体">根据XPath搜索节点演示[<A href="default.htm">返回主页面</A>]</FONT></P>
			<P><FONT face="宋体">XPath字符串
					<asp:TextBox id="txtXPath" runat="server" Width="528px"></asp:TextBox>
					<asp:Button id="cmdQuery" runat="server" Text="检索"></asp:Button></FONT></P>
			<P><FONT face="宋体">例如输入"Table/Record[CustomerID='ANATR']"，当没有输入则显示整个XML文档的XML字符串。</FONT></P>
			<P><FONT face="宋体">检索结果<BR>
				</FONT><FONT face="宋体">
					<asp:TextBox id="txtXML" runat="server" Width="664px" TextMode="MultiLine" Height="448px" Wrap="False"
						EnableViewState="False"></asp:TextBox></P>
			</FONT>
		</form>
	</body>
</HTML>
