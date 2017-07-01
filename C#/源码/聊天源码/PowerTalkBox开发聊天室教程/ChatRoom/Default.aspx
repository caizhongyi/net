<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>聊天市示例代码</title>
</head>
<body bgColor=#000000>
    <form id="form1" runat="server">
   <div>
  <TABLE class=tableblack height="100%" cellSpacing=0 cellPadding=0 width="100%" border=0>
<tr>

<td style="height: 749px" >
    <table style="width: 680px" >
        <tr>
            <td style="width: 848px"> 
                <asp:LinkButton ID="LinkButton1" runat="server" Font-Underline="True" ForeColor="White"
                    OnClick="LinkButton1_Click">以游客身份进入聊天室</asp:LinkButton>
                <img src="images/Index.jpg" /></td>
        </tr>
    </table>
   </td>

</tr>
</TABLE>          
</div>
    </form>
</body>
</html>
