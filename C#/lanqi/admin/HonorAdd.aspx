<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HonorAdd.aspx.cs" Inherits="admin_HonorAdd" validateRequest=false %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
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
            <td height="22" colspan="2" align="center" background="../Images/bg_list.gif" bgcolor="#6699cc"><span class="STYLE1">班级信息</span></td>
          </tr>
     
          <tr bgcolor="#ffffff">
            <td valign="center" height="25" style="width: 183px"><strong>信息名称：</strong></td>
            <td width="834" height="25" style="width: 348px"><table style="width: 380px"><tr><td><input title="这里请输入您的网站名称，最多为20个汉字" maxlength="20" size="30"    ID="SiteName" runat="server" /></td><td>
                <strong>分类：</strong></td><td>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList></td></tr></table>
              </td>
          </tr>
          <tr bgcolor="#ffffff">
            <td height="25" style="width: 183px"><strong>信息内容：</strong></td>
            <td height="25" style=""><FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" Height="250px" >
                                  </FCKeditorV2:FCKeditor>
              </td>
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
              <asp:TextBox ID="TextBox4" runat="server" >99</asp:TextBox></td>
        </tr>
             <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="40" align="center" style="width: 168px">
              来源</td>
          <td align="left" class="table1" style="width: 622px" > 
              <asp:TextBox ID="TextBox5" runat="server" ></asp:TextBox></td>
        </tr>
          <tr bgcolor="#ffffff">
            <td style="height: 25px; width: 183px;"><strong>小图片：</strong></td>
            <td style="height: 25px" >
			 <table style="width: 513px"><tr><td style="height: 104px; width: 219px;"><input id="file1" runat=server type="file" onchange="javascript:document.getElementById('Image1').src=this.value" /></td><td style="height: 104px; width: 133px;"> <asp:Image ID="Image1" runat="server" Width=100 Height=100 /></td><td style="height: 104px">
                 <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="删除" /></td></tr></table>&nbsp; &nbsp;&nbsp;
               </td>
          </tr>
                <tr bgcolor="#ffffff">
            <td style="height: 25px; width: 183px;"><strong>大图片：</strong></td>
            <td style="height: 25px" >
			 <table style="width: 513px"><tr><td style="height: 104px; width: 219px;"><input id="file2" runat=server type="file" onchange="javascript:document.getElementById('Image2').src=this.value" /></td><td style="height: 104px; width: 133px;"> <asp:Image ID="Image2" runat="server" Width=100 Height=100 /></td><td style="height: 104px">
                 <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="删除" /></td></tr></table>&nbsp; &nbsp;&nbsp;
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
