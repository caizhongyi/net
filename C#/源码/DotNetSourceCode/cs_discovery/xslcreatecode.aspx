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
			<P align="center"><FONT face="����" size="6">����XSLT�Ĵ�����Ĵ���������</FONT></P>
			<P align="center"><FONT face="����">Ԭ���� 2008-1-15</FONT></P>
			<P><FONT face="����">��ҳ����ʾ��ʹ��C#����Access���ݿ�ṹ��Ȼ��ʹ��XSLT�������ݱ�ṹ���Զ����ɸ���Դ���룬ʹ��XSLT�ļ���Ϊ�����<BR>
					<A href="default.htm">���������ҳ��</A>��</FONT></P>
			<P><FONT face="����">���ݿ�����:
					<asp:Label id="lblDBName" runat="server">Label</asp:Label></FONT></P>
			<P><FONT face="����"> ���ݱ���:
					<asp:DropDownList id="cboTable" runat="server" DESIGNTIMEDRAGDROP="26" Width="224px"></asp:DropDownList>&nbsp;</FONT><FONT face="����">XSLTģ����:
					<asp:DropDownList id="cboXSLT" runat="server" Width="160px"></asp:DropDownList>
					<asp:Button id="cmdRefresh" runat="server" Text="ˢ��ϵͳ"></asp:Button></FONT></P>
			<P><FONT face="����">
					<asp:Button id="cmdCreate" runat="server" Text="��������"></asp:Button></FONT></P>
			<P><FONT face="����">���ɵ�Դ����<BR>
				</FONT>
				<asp:Label id="lblResult" runat="server" EnableViewState="False">Label</asp:Label></P>
		</form>
	</body>
</HTML>
