<%@ Page Language="C#" AutoEventWireup="true" CodeFile="honorTypeAdd.aspx.cs" Inherits="admin_honorTypeAdd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="../style/StyleSheet.css" rel="stylesheet" type="text/css" /><link href="../style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
<!--
.STYLE1 {color: #FFFFFF}
-->
    </style>
<script language="javascript" type="text/javascript">
// <!CDATA[

function Reset1_onclick() {

}

// ]]>
</script>
</head>
<body style="height:1200px">
    <form id="form1" runat="server">
    <div>
      <table cellspacing="1" cellpadding="2" width="100%" align="center" bgcolor="#000000" border="0">
        <tbody>
          <tr bgcolor="#ffffff">
            <td height="22" colspan="2" align="center" background="../Images/bg_list.gif" bgcolor="#6699cc"><span class="STYLE1">班级专业</span></td>
          </tr>
     
          <tr bgcolor="#ffffff">
            <td valign="center" height="25" style="width: 183px">
                <strong>名称：</strong></td>
            <td width="834" height="25" style="width: 348px">
                &nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
          </tr>
             
          <tr bgcolor="#ffffff">
            <td valign="center" height="25" style="width: 183px">
                <strong>学制：</strong></td>
            <td width="834" height="25" style="width: 348px">
                &nbsp;<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
          </tr>
             
          <tr bgcolor="#ffffff">
            <td valign="center" height="25" style="width: 183px">
                <strong>学费：</strong></td>
            <td width="834" height="25" style="width: 348px">
                &nbsp;<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
          </tr>
             
          <tr bgcolor="#ffffff">
            <td valign="center" height="25" style="width: 183px">
                <strong>说明：</strong></td>
            <td width="834" height="25" style="width: 348px">
                &nbsp;<asp:TextBox ID="TextBox4" runat="server" Height="119px" TextMode="MultiLine" Width="279px"></asp:TextBox>
                </td>
          </tr>
 
          <tr bgcolor="#ffffff">
            <td style="height: 25px; width: 183px;"><strong>图片(没有可不加)：</strong></td>
            <td style="height: 25px" >
			 <table style="width: 513px"><tr><td style="height: 104px; width: 219px;"><input id="file1" runat=server type="file" onchange="javascript:document.getElementById('Image1').src=this.value" /></td><td style="height: 104px; width: 133px;"> <asp:Image ID="Image1" runat="server" Width=100 Height=100 /></td><td style="height: 104px">
                 <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="删除" /></td></tr></table>&nbsp; &nbsp;&nbsp;
               </td>
          </tr>

          <tr bgcolor="#ffffff">
            <td align="center" colspan="2" style="height: 40px">
                &nbsp;<asp:Button ID="Button1" runat="server" Text="提交" Width="60px" OnClick="Button1_Click" />
                <input type="reset" value=" 重 填 " name="cmdReset" id="Reset1" onclick="return Reset1_onclick()" />
                </td>
          </tr>
        </tbody>
      </table>
    </div>
    </form>
</body>
</html>

