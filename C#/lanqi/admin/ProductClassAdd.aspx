<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductClassAdd.aspx.cs" Inherits="admin_ProductClassAdd" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="../style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
<!--
.STYLE1 {color: #FFFFFF}
-->
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <table cellspacing="1" cellpadding="2" width="100%" align="center" bgcolor="#000000" border="0">
        <tbody>
          <tr bgcolor="#ffffff">
            <td height="22" colspan="2" align="center" background="../Images/bg_list.gif" bgcolor="#6699cc"><span class="STYLE1" runat=server id=newtype >二级类别</span></td>
          </tr>
     
          <tr bgcolor="#ffffff" runat=server id=title>
            <td valign="center" height="25" style="width: 283px"><strong>选择所属类别：</strong></td>
            <td width="834" height="25" style="width: 348px">
                &nbsp;<asp:DropDownList ID="DropDownList1" runat="server">
                </asp:DropDownList>
                *</td>
          </tr>
          <tr bgcolor="#ffffff">
            <td height="25" style="width: 283px"><strong>产品类别名称：</strong></td>
            <td height="25" style="width: 348px">
                &nbsp;<input title="这里请输入您的网站地址，最多为50个字符，前面必须带http://" maxlength="100" size="30" runat="server" id="SiteUrl" />
              *</td>
          </tr>
          <tr bgcolor="#ffffff">
            <td style="height: 25px; width: 283px;"><strong>产品类别图片：</strong></td>
            <td style="height: 25px" ><table><tr><td style="height: 105px"><input id="file1" runat=server type="file" onchange="javascript:document.getElementById('Image1').src=this.value" /></td><td style="height: 105px"> <asp:Image ID="Image1" runat="server" Width=100 Height=100 /></td><td style="height: 105px">
                 <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="删除" /></td></tr></table>
			 &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
               </td>
          </tr>

          <tr bgcolor="#ffffff">
            <td align="center" colspan="2" height="40">
                &nbsp;<asp:Button ID="Button1" runat="server" Text="提交" Width="60px" OnClick="Button1_Click" />&nbsp;
                </td>
          </tr>
        </tbody>
      </table>
    </div>
    </form>
</body>
</html>

