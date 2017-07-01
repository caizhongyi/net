<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FuWu.aspx.cs" Inherits="admin_FuWu" validateRequest=false %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="style/css.css" rel="stylesheet" type="text/css" />
<link href="../style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style=" text-align:left">
    <div class="Currently" style=" text-align:center">
        其他信息</div>
<div class="user_reg">
  <form id="form3"  runat=server>
  <ul>
  <table border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6" style="width: 126%">
      <tbody>
           <tr bgcolor="#ffffff">
            <td valign="center" height="25" style="width: 126px"><strong>信息名称：</strong></td>
            <td height="25" style="text-align:left; width: 506px;" ><input title="这里请输入您的网站名称，最多为20个汉字" maxlength="20" size="30"    ID="SiteName" runat="server" />
              *</td>
          </tr>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="40" align="right" style="width: 126px">&nbsp;</td>
          <td align="left" class="table1" style="width: 506px" > <FCKeditorV2:FCKeditor Width="800px" ID="FCKeditor1" runat="server" Height="250px" >
                                  </FCKeditorV2:FCKeditor></td>
        </tr>
        
              <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="50" align="right" style="width: 126px">
              排序：</td>
          <td align="left" class="table1" style="width: 506px" >
              <asp:TextBox ID="TextBox1" runat="server" Width="20px">10</asp:TextBox></td>
        </tr>

        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="50" align="right" style="width: 126px">&nbsp;</td>
          <td align="left" class="table1" style="width: 506px" >
              <asp:Button ID="btnOk" runat="server" Text="确认提交" OnClick="btnOk_Click" /> </td>
        </tr>
      </tbody>
    </table>
  </ul>
</form>
</div>
</body>

</html>

