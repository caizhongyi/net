<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div><asp:RadioButton ID="NotRBProcType" runat="server"  GroupName ="CreateType" Checked="True" Text="非存储过程"/><asp:RadioButton ID="IsRBProcType" runat="server" GroupName ="CreateType" Text="存储过程"/></div>
     <div> 数据库名:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><asp:Button ID="Button1"
            runat="server" OnClick="Button1_Click" Text="生成" />
            </div>
            </div>
    </form>
</body>
</html>
