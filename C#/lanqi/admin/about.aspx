<%@ Page Language="C#" AutoEventWireup="true" CodeFile="about.aspx.cs" Inherits="admin_about" validateRequest=false  %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="style/css.css" rel="stylesheet" type="text/css" />
<link href="../style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style=" text-align:left; height:1000px">
    <div class="Currently" style=" text-align:center">公司简介</div>
<div class="user_reg">
  <form id="form3"  runat=server>
  <ul>
  <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6">
      <tbody>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="40" align="right" style="width: 112px">&nbsp;</td>
          <td width="85%" align="left" class="table1" > <FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" Height="250px" >
                                  </FCKeditorV2:FCKeditor></td>
        </tr>
        
              <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="50" align="right" style="width: 112px">公司图片：</td>
          <td align="left" class="table1" >
              <table style="width: 513px"><tr><td style="height: 104px; width: 219px;"><input id="file1" runat=server type="file" onchange="javascript:document.getElementById('Image1').src=this.value" /></td><td style="height: 104px; width: 133px;"> <asp:Image ID="Image1" runat="server" Width=100 Height=100 /></td><td style="height: 104px">
                 <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="删除" /></td></tr></table> </td>
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

