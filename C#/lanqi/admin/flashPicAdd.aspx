<%@ Page Language="C#" AutoEventWireup="true" CodeFile="flashPicAdd.aspx.cs" Inherits="admin_flashPicAdd" %>

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
            <td height="22" colspan="2" align="center" background="../Images/bg_list.gif" bgcolor="#6699cc"><span class="STYLE1">flash图片</span></td>
          </tr>
          <tr bgcolor="#ffffff"  >
            <td valign="center" width="283" height="25"><strong>广告类型：</strong></td>
            <td width="834" height="25" style="width: 348px"><select id="flaType" runat="server">
            <option value="0">Flash广告</option>
            <option value="1">商家广告</option>
            </select>
              *</td>
          </tr>
          <tr bgcolor="#ffffff"  >
            <td valign="center" width="283" height="25"><strong>图片名称：</strong></td>
            <td width="834" height="25" style="width: 348px"><input title="这里请输入您的网站名称，最多为20个汉字" maxlength="20" size="30"    ID="SiteName" runat="server" value="111" />
              *</td>
          </tr>
          <tr bgcolor="#ffffff">
            <td width="283" height="25"><strong>连接地址：</strong></td>
            <td height="25" style="width: 348px"><input title="这里请输入您的网站地址，最多为50个字符，前面必须带http://" maxlength="100" size="30" runat="server" id="SiteUrl" value="#" />
              *</td>
          </tr>
          <tr bgcolor="#ffffff">
            <td width="283" style="height: 25px"><strong>图片：</strong></td>
            <td style="height: 25px" >
			 <table><tr><td style="height: 105px"><input id="file1" runat=server type="file" onchange="javascript:document.getElementById('Image1').src=this.value" /></td><td style="height: 105px"> <asp:Image ID="Image1" runat="server" Width=100 Height=100 /></td><td style="height: 105px">
                 <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="删除" /></td></tr></table>&nbsp; &nbsp;&nbsp;
               </td>
          </tr>

          <tr bgcolor="#ffffff">
            <td align="center" colspan="2" style="height: 40px">
                &nbsp;<asp:Button ID="Button1" runat="server" Text="提交" Width="60px" OnClick="Button1_Click" />
                <input type="reset" value=" 重 填 " name="cmdReset" />
                </td>
          </tr>
        </tbody>
      </table>
    </div>
    </form>
</body>
</html>
