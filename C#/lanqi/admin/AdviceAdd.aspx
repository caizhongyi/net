<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdviceAdd.aspx.cs" Inherits="admin_AdviceAdd" validateRequest=false %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
 <link href="style/css.css" rel="stylesheet" type="text/css" /><link href="../style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <title>无标题页</title>
</head>
<body style=" text-align:left; height:1000px">
   <div class="Currently" style=" text-align:center">
       <br />
       <asp:Label ID="Label1" runat="server"></asp:Label>&nbsp;</div>
<div class="user_reg">
  <form id="form3"  runat=server>
  <ul>
  <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6">
      <tbody>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="40" align="right" style="width: 112px">
              简介标题：&nbsp;</td>
          <td width="85%" align="left" class="table1" > 
              <asp:TextBox ID="txtName" runat="server" Width="462px"></asp:TextBox></td>
        </tr>
        
              <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="50" align="right" style="width: 112px">
              简介内容：</td>
          <td align="left" class="table1" >
              &nbsp;<FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" Height="250px" >
                                  </FCKeditorV2:FCKeditor>  </td>
        </tr>
         <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="40" align="center" style="width: 168px">
              作者</td>
          <td align="left" class="table1" style="width: 622px" > 
              <asp:TextBox ID="TextBox3" runat="server" ></asp:TextBox></td>
        </tr>
             <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="40" align="center" style="width: 168px">
              点击数</td>
          <td align="left" class="table1" style="width: 622px" > 
              <asp:TextBox ID="TextBox4" runat="server" ></asp:TextBox></td>
        </tr>
             <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="40" align="center" style="width: 168px">
              来源</td>
          <td align="left" class="table1" style="width: 622px" > 
              <asp:TextBox ID="TextBox5" runat="server" ></asp:TextBox></td>
        </tr>

        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="50" align="right" style="width: 112px">&nbsp;</td>
          <td align="left" class="table1" >
              <asp:Button ID="btnOk" runat="server" Text="确认提交" OnClick="btnOk_Click" /> </td>
        </tr>
      </tbody>
    </table>
  </ul>
</form>
</div>
</body>

</html>

