<!--
//===============================================
//　　　　　　　　　　\\\|///
//　　　　　　　　　　\\　- -　//
//　　　　　　　　　　  ( @ @ )
//┏━━━━━━━━━oOOo-(_)-oOOo━━━┓
//┃                                     ┃
//┃             东 网 原 创！           ┃
//┃      lenlong 作品，请保留此信息！   ┃
//┃      ** lenlenlong@hotmail.com **   ┃
//┃                                     ┃
//┃　　　　　　　　　　　　　Dooo　     ┃
//┗━━━━━━━━━ oooD━-(　 )━━━┛
//　　　　　　　　　　 (  )　  ) /
//　　　　　　　　　　　\ (　 (_/
//　　　　　　　　　　　 \_)
//===============================================
-->
<%@ Page Language="C#" Inherits="WebSite.Admin.Board.MoveTo"%>
<%@ OutputCache Duration="1" VaryByParam="none" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>转移论坛</title>
    <link href="../style/css.css" type=text/css rel=stylesheet><link />
      <script language = "javaScript" src = "../js/jscript.js" type="text/javascript"></script>
</head>
<IFRAME id="IframeTarGet" name="IframeTarGet" marginWidth="0" marginHeight="0" src="about:blank" frameBorder="0" width="0" scrolling="no" height="0"></IFRAME>
<body>
<base target="_self" />
    <form id="FormLoading" runat="server" name="FormLoading">
    

<table width="308" height="141" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td valign="middle" class="MinWindowTop"><img src="images/icon_02.gif" width="8" height="8"> 转移论坛</td>
  </tr>
  <tr>
    <td align="center" valign="top" class="MinWindowBody"><table width="98%" border="0" cellspacing="1" cellpadding="0">
      <tr>
        <td height="24" colspan="2" class="White">&nbsp;</td>
        </tr>
      <tr>
        <td width="30%" height="24" align="center">转移论坛到</td>
        <td width="70%" class="White">
            <asp:TextBox ID="boardID" runat="server" Text="0" Visible="false"></asp:TextBox>
            <asp:DropDownList ID="parentBoardID" runat="server">
            </asp:DropDownList>
            </td>
      </tr>
      <tr>
        <td height="24" colspan="2" class="White">&nbsp;</td>
        </tr>
    </table></td>
  </tr>
  <tr>
    <td class="MinWindowBottom"><table border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="60" height="17" align="center" class="Buttom"> <a href="javascript:JsPost();">确定</a> </td>
        <td width="12" align="center"></td>
        <td width="60" align="center" class="Buttom"> <a href="javascript:window.close();">取消</a> </td>
      </tr>
    </table></td>
  </tr>
</table>
    </form>
</body>
</html>
