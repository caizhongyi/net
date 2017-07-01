<%@ Page Language="C#" AutoEventWireup="true" CodeFile="zhuangye.aspx.cs" Inherits="admin_zhuangye" %>

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
        &nbsp;<br />
        <br />
      <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6">
     
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
            <td align="center" class="table1" colspan="2">
                &nbsp;专业班级管理</td>
          </tr>
        
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="right" style="width: 50%">
              班级专业：</td><td align="left" class="table1" ><input name="pwd" type=text class="formr" id="pwd" runat=server style="width: 123px" /></td>
          </tr>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
            <td align="center" class="table1" colspan="2" style="height: 29px">
                &nbsp;<asp:Button ID="Button2" runat="server" Text="添加" OnClick="Button2_Click" /></td>
        </tr>
       
      <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
            <td align="center" class="table1" colspan="2">
                &nbsp;专业科目管理</td>
          </tr>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td align="right" style="height: 30px; width: 50%;">
              年级专业：</td>
          <td align="left" class="table1" style="height: 30px" >
              <asp:DropDownList ID="DropDownList1" runat="server">
              </asp:DropDownList></td>
        </tr>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="right" style="width: 50%">
              专业课程：</td>
          
          <td align="left" class="table1" ><input name="address" type="text" class="formr" size="50" id="address" runat=server style="width: 123px" /></td>
        </tr>
           <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
            <td align="center" class="table1" colspan="2" style="height: 29px">
                &nbsp;<asp:Button ID="Button1" runat="server" Text="添加" OnClick="Button1_Click" /></td>
        </tr>
    </table>
       
       </div>
    </form>
</body>
</html>
