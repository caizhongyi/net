<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin.aspx.cs" Inherits="admin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>管理员入口
    </title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        管理员入口<br />
        (加入Session后登录)
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="在线客服点击进入" /><br />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="技术支持点击进入" /></div>
    </form>
</body>
</html>
