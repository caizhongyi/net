<%@ Page Language="C#" AutoEventWireup="true" CodeFile="siteMessage.aspx.cs" Inherits="admin_siteMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
     <link href="style/css.css" rel="stylesheet" type="text/css" /><link href="../style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style=" text-align:left" scrolling="yes">
    <form id="form1" runat="server" >
    <div  style="text-align:center">
        <br />
        &nbsp;网站设置<br />
        <br />
      <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6">
      <tbody>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td width="22%" height="30" align="right">
              关键字：<td width="78%" align="left" class="table1" >
                  &nbsp;<textarea id="txtKey" style="width: 542px; height: 184px" runat=server></textarea></td>
          </tr>
        
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="right">
              描述：<td align="left" class="table1" >
                  &nbsp;<textarea id="txtDes" style="width: 540px; height: 182px" runat=server></textarea></td>
          </tr>
       
        
       
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="50" align="right">&nbsp;</td>
          <td align="left" class="table1" >
              <asp:Button ID="Button1" runat="server" Text="确认提交" OnClick="Button1_Click" /></td>
        </tr>
      </tbody>
    </table>
       
       </div>
    </form>
</body>
</html>
