<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PwdChange.aspx.cs" Inherits="AdminPwdChange" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="Panel3" runat="server" Height="93px" Width="624px">
            <asp:Label ID="Label9" runat="server" Text="旧密码："></asp:Label>
            <asp:TextBox ID="OldPwd" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="OldPwd"
                ErrorMessage="*"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="Label8" runat="server" Text="新密码："></asp:Label>
            <asp:TextBox ID="NewPwd" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="NewPwd"
                ErrorMessage="*"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="Label7" runat="server" Text="确定密码："></asp:Label>
            <asp:TextBox ID="MNewPwd" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="MNewPwd"
                ErrorMessage="*"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="NewPwd"
                ControlToValidate="MNewPwd" ErrorMessage="密码不一致"></asp:CompareValidator>
            <br />
            <asp:Button ID="Button3" runat="server" Text="修改" />&nbsp; &nbsp;<asp:Button ID="Button4"
                runat="server" Text="取消" />
        </asp:Panel>
    
    </div>
    </form>
</body>
</html>
