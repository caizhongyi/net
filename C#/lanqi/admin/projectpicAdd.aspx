<%@ Page Language="C#" AutoEventWireup="true" CodeFile="projectpicAdd.aspx.cs" Inherits="admin_projectpicAdd" validateRequest="false"  %>


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
            <td height="22" colspan="2" align="center" background="../Images/bg_list.gif" bgcolor="#6699cc"><span class="STYLE1">资料中心</span></td>
          </tr>
     

          
                    <tr bgcolor="#ffffff">
            <td valign="center" height="25" style="width: 183px">
                <strong>资料名称：</strong></td>
            <td width="834" height="25" style="width: 348px" >
                <input id=mingcheng runat=server type="text" style="width: 191px" />
                </td>
          </tr>
                    <tr bgcolor="#ffffff">
            <td valign="center" height="25" style="width: 183px">
                <strong>资料内容：</strong></td>
            <td width="834" height="25" style="width: 348px">
            <input type="hidden" runat=server name="sContent" id="sContent" value="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 脚本爱好者资源网欢迎你的来访，我的激情和动力来自你的支持和鼓励，你是我的客人，更是我的朋友。 你可能感受到这里的简约，但我想你同时也会感受到这里的淡雅，不需要浓妆艳抹，不需要庞大臃肿，简单也是一种美丽。如果你是一位Web开发者，如果你是一位脚本爱好者，这里的很多东西(css/xhtml/javascript)将是你所想看到的。<BR>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 我在尽我最大的努力耕耘着一片小小的田地，希望她能给你带来惊喜，带来一份开心。在这里你可以和我联系,可以一起分享生活中的点点滴滴，品味人生的乐趣。 给自己寻找一个小小的空间，一个没有烦恼的乐园。相信，用心就会做的更好 ... " />
<iframe src="../163editor/editor.html?id=sContent" frameborder="0" scrolling="no" width="670" height="320"></iframe>
           
                </td>
          </tr>
 
          <tr bgcolor="#ffffff">
            <td style=" width: 183px;"><strong>资料附件：</strong></td>
            <td style=" vertical-align:middle" >
			 <table style="width: 513px"><tr><td style=" width: 219px; height: 22px;"><input id="file1" runat=server type="file" onchange="javascript:document.getElementById('fujian').value=this.value" /></td><td style=" width: 133px; height: 22px;" id=fujian runat=server> </td><td style="HEIGHT: 22px">
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
