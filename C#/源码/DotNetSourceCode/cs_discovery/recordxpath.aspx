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
			<P><FONT face="����">����XPath�����ڵ���ʾ[<A href="default.htm">������ҳ��</A>]</FONT></P>
			<P><FONT face="����">XPath�ַ���
					<asp:TextBox id="txtXPath" runat="server" Width="528px"></asp:TextBox>
					<asp:Button id="cmdQuery" runat="server" Text="����"></asp:Button></FONT></P>
			<P><FONT face="����">��������"Table/Record[CustomerID='ANATR']"����û����������ʾ����XML�ĵ���XML�ַ�����</FONT></P>
			<P><FONT face="����">�������<BR>
				</FONT><FONT face="����">
					<asp:TextBox id="txtXML" runat="server" Width="664px" TextMode="MultiLine" Height="448px" Wrap="False"
						EnableViewState="False"></asp:TextBox></P>
			</FONT>
		</form>
	</body>
</HTML>
