<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebFormLogin.aspx.cs" Inherits="WebFormLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>WebForm Login</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Login ID="Login" runat="server" BorderStyle="Solid" OnAuthenticate="Login_Authenticate">
        </asp:Login>    
    </div>
    </form>
</body>
</html>
