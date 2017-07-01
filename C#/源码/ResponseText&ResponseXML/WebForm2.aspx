<%@ Page language="c#" Inherits="WebApplication2.WebForm2" CodeFile="WebForm2.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		
		<script language="javascript" type="text/javascript">
		function test()
		{
		alert('llll');
		return false;		
		}
		</script>
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<FONT face="ËÎÌו">
                <asp:Button ID="Button1" runat="server" Text="Button" OnClientClick="return test();" /></FONT>
		</form>
	</body>
</HTML>
