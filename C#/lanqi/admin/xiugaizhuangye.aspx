<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xiugaizhuangye.aspx.cs" Inherits="admin_xiugaizhuangye" %>

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
            <td height="22" colspan="2" align="center" background="../Images/bg_list.gif" bgcolor="#6699cc"><span class="STYLE1" id=mc runat=server>修改班级名称</span></td>
          </tr>
          
             <tr bgcolor="#ffffff">
            <td valign="center" height="25" style="width: 183px">
                <strong>专业：</strong></td>
            <td width="834" height="25" style="width: 348px">
                &nbsp;<asp:DropDownList ID="DropDownList2" runat="server" >
              </asp:DropDownList>
                *</td>
          </tr>
     
          <tr bgcolor="#ffffff">
            <td valign="center" height="25" style="width: 183px">
                <strong>修改为：</strong></td>
            <td width="834" height="25" style="width: 348px">
                &nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                *</td>
          </tr>
 
   

          <tr bgcolor="#ffffff">
            <td align="center" colspan="2" style="height: 40px">
                &nbsp;<asp:Button ID="Button1" runat="server" Text="修改" Width="60px" OnClick="Button1_Click" />&nbsp;
                <asp:Button ID="Button2" runat="server" Text="删除" Width="60px" OnClick="Button2_Click" /></td>
          </tr>
        </tbody>
      </table>
    </div>
    </form>
</body>
</html>

