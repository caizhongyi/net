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
			<P align="center"><FONT face="����" size="6">ASP.NETʹ����֤�뼼������ʾҳ��</FONT></P>
			<P align="center"><FONT face="����">Ԭ���� 2008-1-14</FONT></P>
			<P align="left"><FONT face="����">��ҳ����ʾ��ʹ��C#��ASP.NET��ʹ��ͼ�α����ʵ����֤�뼼����<A href="default.htm">���������ҳ��</A>��</FONT></P>
			<P><FONT face="����">�������û���:
					<asp:TextBox id="txtUserName" runat="server" Width="224px"></asp:TextBox></P>
			</FONT>
			<P><FONT face="����">����������&nbsp; :
					<asp:TextBox id="txtPassword" runat="server" Width="224px" TextMode="Password"></asp:TextBox></FONT></P>
			<P><FONT face="����">��������֤��:
					<asp:TextBox id="txtCheckCode" runat="server" Width="224px"></asp:TextBox>
					<img src="checkimage.aspx" title='���������˫��ͼƬ��һ�š�' ondblclick="this.src = 'checkimage.aspx?flag=' + Math.random() "
						border="1"> 
					<!--
						����� ondblclick �����в���һ�����������flag��������Ч�ķ�ֹ���������ͼƬ
						ʹ����ʾ����֤��ͼƬһ�������µ�ͼƬ
					-->
					<span>���������˫��ͼƬ��һ�š�</span> </FONT>
			</P>
			<FONT face="����">
				<P>
					<asp:Button id="cmdOK" runat="server" Text="ȷ��"></asp:Button></P>
				<P>
					<asp:Label id="lblResult" runat="server">##</asp:Label></P>
			</FONT>
		</form>
	</body>
</HTML>
