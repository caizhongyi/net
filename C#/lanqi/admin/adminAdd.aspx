<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adminAdd.aspx.cs" Inherits="admin_adminAdd" %>

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
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
      <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6">
      <tbody>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td width="22%" height="30" align="right">
              用户名：<td width="78%" align="left" class="table1" ><input name="name" type="text" class="formr" size="50" id="name" runat=server style="width: 327px"   />
                  <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label></td>
          </tr>
        
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="right">
              密 码：<td align="left" class="table1" ><input name="pwd" type=password class="formr" id="pwd" runat=server style="width: 327px" /></td>
          </tr>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td align="right" style="height: 29px">
              确认密码：
          <td align="left" class="table1" style="height: 29px" ><input name="checkpwd" type="password" class="formr" id="checkpwd" runat=server style="width: 327px" /></td>
        </tr>
       
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td align="right" style="height: 30px">
              性别：
          <td align="left" class="table1" style="height: 30px" >
              <input id="rdMan" type="radio" value="男" name=rd runat=server checked=true />男
              <input id="rdWoman" type="radio" value="女" name=rd runat=server />女
          </td>
        </tr>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td align="right" style="height: 30px">
              电子邮箱：
          <td align="left" class="table1" style="height: 30px" ><input name="email" type="text" class="formr" size="50" id="email" runat=server style="width: 327px" /></td>
        </tr>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="right">
              地址：
          
          <td align="left" class="table1" ><input name="address" type="text" class="formr" size="50" id="address" runat=server style="width: 327px" /></td>
        </tr>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td align="right" style="height: 30px">
              固定电话：</td>
          <td align="left" class="table1" style="height: 30px" ><input name="ring" type="text" class="forme" id="ring" runat=server style="width: 327px" /></td>
        </tr>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="right">
              移动电话：</td>
          <td align="left" class="table1" ><input name="tel" type="text" class="forme" id="tel" runat=server style="width: 327px" /></td>
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