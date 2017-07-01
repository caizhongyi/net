<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BookDetail.aspx.cs" Inherits="admin_BookDetail" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
     <link href="style/css.css" rel="stylesheet" type="text/css" /><link href="../style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style=" text-align:left; height:1000px">
   <div class="Currently" style=" text-align:center">
       <asp:Label ID="Label1" runat="server">查看留言</asp:Label>&nbsp;</div>
<div class="user_reg">
  <form id="form3"  runat=server>
  <ul>
  <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6">
      <tbody>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="40" align="right" style="width: 112px">
              留言标题：&nbsp;</td>
          <td width="85%" align="left" class="table1" > 
              <asp:Label ID="lblTitle" runat="server"></asp:Label></td>
        </tr>
        
              <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="50" align="right" style="width: 112px">
              留言内容：</td>
          <td align="left" class="table1" >
              &nbsp;<FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" Height="250px" >
                                  </FCKeditorV2:FCKeditor>  </td>
        </tr>
         <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="40" align="right" style="width: 112px">
              留言人：&nbsp;</td>
          <td width="85%" align="left" class="table1" > 
              <asp:Label ID="lblName" runat="server"></asp:Label></td>
        </tr>
         <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="40" align="right" style="width: 112px">
              性别：&nbsp;</td>
          <td width="85%" align="left" class="table1" > 
              <asp:Label ID="lblSex" runat="server"></asp:Label></td>
        </tr>
         <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="40" align="right" style="width: 112px">
              电子邮箱：&nbsp;</td>
          <td width="85%" align="left" class="table1" > 
              <asp:Label ID="lblEmail" runat="server"></asp:Label></td>
        </tr>
         <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="40" align="right" style="width: 112px">
              联系电话：&nbsp;</td>
          <td width="85%" align="left" class="table1" > 
              <asp:Label ID="lblTel" runat="server"></asp:Label></td>
        </tr>
         <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="40" align="right" style="width: 112px">
              留言时间：&nbsp;</td>
          <td width="85%" align="left" class="table1" > 
              <asp:Label ID="lblTime" runat="server"></asp:Label></td>
        </tr>
         <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="40" align="right" style="width: 112px">
              地址：&nbsp;</td>
          <td width="85%" align="left" class="table1" > 
              <asp:Label ID="lblAddress" runat="server"></asp:Label></td>
        </tr>
        
             <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="40" align="right" style="width: 112px">
              传真：&nbsp;</td>
          <td width="85%" align="left" class="table1" > 
              <asp:Label ID="Label2" runat="server"></asp:Label></td>
        </tr>

        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="50" align="right" style="width: 112px">&nbsp;</td>
          <td align="left" class="table1" >
              <asp:Button ID="btnOk" runat="server" Text="返回" OnClick="btnOk_Click" /> </td>
        </tr>
      </tbody>
    </table>
  </ul>
</form>
</div>
</body>
</html>
