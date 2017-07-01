<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DefaultAdd.aspx.cs" Inherits="DefaultAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       num1 <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <br />
       num2 <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        <br />
        <hr />
        result<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" /></div>
    </form>
</body>
</html>
