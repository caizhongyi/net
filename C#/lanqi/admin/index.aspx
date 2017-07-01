<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="admin_index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    
<link href="css/main.css" rel="stylesheet" type="text/css"><link href="../style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body SCROLL=No>
    <form id="form1" runat="server">
<table width="100%" height="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td height="84" colspan="3"><iframe name="topWin" id="topWin" marginwidth=0 marginheight=0 src="Top.aspx" valign="middle" frameborder=0 width="100%" height="100%" scrolling="no" resize="no"></iframe></td>
  </tr>
  <tr>
    <td width="181" id="menuArea"><iframe name="menuWin" id="menuWin" marginwidth=0 marginheight=0 src="Left.aspx" valign="middle" frameborder=0 width="100%" height="800px" scrolling="no" resize="no"></iframe></td>
    <td width="14"><iframe name="lineWin" id="lineWin" marginwidth=0 marginheight=0 src="line.html" valign="middle" frameborder=0 width="100%" height="800px" scrolling="no" resize="no"></iframe></td>
    <td class="body"><iframe name="bodyWin" id="bodyWin" marginwidth=0 marginheight=0 src=adminManager.aspx valign="middle" frameborder=0 width="100%" height="800px" scrolling="yes" resize="no"></iframe></td>
  </tr>
  

 
</table>

 

    </form>
</body>
</html>
