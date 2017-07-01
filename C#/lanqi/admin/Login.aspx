<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="admin_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<HEAD runat="server">
<TITLE>用户终端信息服务平台</TITLE>

<link href="css/main.css" rel="stylesheet" type="text/css">
<style type="text/css">

</style>
</HEAD>
<BODY LEFTMARGIN=0 TOPMARGIN=0 MARGINWIDTH=0 MARGINHEIGHT=0 scroll="no">
<form runat="server">
<table height="" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td><img src="images/hqht_01.gif" width="313" height="200" /></td>
    <td><img src="images/xxx.jpg" width="396" height="200" /></td>
    <td><img src="images/hqht_03.gif" width="249" height="200" /></td>
  </tr>
  <tr>
    <td><img src="images/hqht_04.gif" width="313" height="325" /></td>
    <td width="396" height="325" align="center" valign="top" background="images/hqht_05.gif"><table width="80%" border="0" cellspacing="10" cellpadding="0">
      <tr>
        <td height="10" colspan="2">&nbsp;</td>
        </tr>
      <tr>
        <td align="right"><span class="STYLE1">用户名：</span></td>
        <td align="left" style="width: 215px"><input name="username" type="text" class="input_b" style="width:150px" /></td>
        </tr>
      <tr>
        <td align="right"><span class="STYLE1">密　码：</span></td>
        <td align="left" style="width: 215px"><input name="pwd" type="password" class="input_b" style="width:150px" /></td>
        </tr>
          <tr>
        <td align="right"><span class="STYLE1">验证码：</span></td>
        <td align="left" style="width: 215px"><input name="yzm" type="text" class="input_b" style="width: 68px"  />&nbsp;
            <img src="js/ValidateCode.aspx" style="cursor:pointer; vertical-align: middle;" onclick="this.src=this.src+'?randnum='+Math.random"  alt="点击更换验证码" border="0" /></td>
        
        </tr>
      <tr>
        <td colspan="2" align="center">
            <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="~/admin/images/btn_login.gif" OnClick="btnLogin_Click" /></td>
        </tr>
    </table>    </td>
    <td><img src="images/hqht_06.gif" width="249" height="325" /></td>
  </tr>
  <tr>
    <td colspan="3" class="copyright">
      建议分辨率：1024x768<br /></td>
  </tr>
</table>
</form>
</BODY>
</html>
