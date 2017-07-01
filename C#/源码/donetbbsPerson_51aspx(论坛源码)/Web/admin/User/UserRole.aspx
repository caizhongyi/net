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
<%@ Page Language="C#" Inherits="WebSite.Admin.User.UserRole"%>
<%@ OutputCache Duration="1" VaryByParam="none" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>角色管理</title>
    <link href="../style/css.css" type=text/css rel=stylesheet><link />
      <script language = "javaScript" src = "../js/jscript.js" type="text/javascript"></script>
      <script language = "javaScript" src = "../../JScript/WebSiteJScriptSeletGroup.js" type="text/javascript"></script>
</head>
<IFRAME id="IframeTarGet" name="IframeTarGet" marginWidth="0" marginHeight="0" src="about:blank" frameBorder="0" width="0" scrolling="no" height="0"></IFRAME>
<body>
<base target="_self" />
    <form id="FormLoading" runat="server" name="FormLoading">
    
<table width="447" height="236" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td valign="middle" class="MinWindowTop"><img src="../images/icon_02.gif" width="8" height="8"> 
        <asp:Label ID="LabelType1" runat="server" Text="修改角色"></asp:Label>
        <asp:Label ID="LabelType2" runat="server" Text="增加角色"></asp:Label>
        <asp:Label ID="userRoleID" runat="server" Text="0" Visible="false"></asp:Label>
        </td>
  </tr>
  <tr>
    <td align="center" valign="top" class="MinWindowBody"><table width="98%" border="0" cellspacing="1" cellpadding="0">
      <tr>
        <td width="21%" height="24">角色名称</td>
        <td colspan="3" class="White">
            <asp:TextBox ID="UserRoleTitle" runat="server" Width="258px"></asp:TextBox>
            
            </td>
      </tr>
      <tr>
        <td width="21%" height="24">启动</td>
        <td colspan="3" class="White">
           <asp:RadioButtonList ID="UserRoleFalse" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True" Value="0">是</asp:ListItem>
                <asp:ListItem Value="1">否</asp:ListItem>
            </asp:RadioButtonList>
            </td>
        </tr>
      <tr>
        <td width="21%" height="24">角色配置</td>
        <td colspan="3" class="White">
        <asp:TextBox ID="UserRoles" runat="server" Width="259px"></asp:TextBox>
        <a href="javascript:WebSiteShowViewPower(2,'UserRoles')">角色权限</a>
        </td>
      </tr>
      <tr>
        <td height="24">发表积分</td>
        <td width="32%" class="White"><asp:TextBox ID="UserRolePostPoint" runat="server" Width="50px" Text="0"></asp:TextBox></td>
        <td width="16%">回复积分</td>
        <td width="31%" class="White"><asp:TextBox ID="UserRoleRePostPoint" runat="server" Width="50px" Text="0"></asp:TextBox></td>
      </tr>
      <tr>
        <td height="24">发表金钱</td>
        <td class="White"><asp:TextBox ID="UserRolePostMoney" runat="server" Width="50px" Text="0"></asp:TextBox></td>
        <td>回复金钱</td>
        <td class="White"><asp:TextBox ID="UserRoleRePostMoney" runat="server" Width="50px" Text="0"></asp:TextBox></td>
      </tr>
      <tr>
        <td height="24">发表魅力</td>
        <td class="White"><asp:TextBox ID="UserRolePostCP" runat="server" Width="50px" Text="0"></asp:TextBox></td>
        <td>回复魅力</td>
        <td class="White"><asp:TextBox ID="UserRoleRePostCP" runat="server" Width="50px" Text="0"></asp:TextBox></td>
      </tr>
      <tr>
        <td height="24">发表经验</td>
        <td class="White"><asp:TextBox ID="UserRolePostExp" runat="server" Width="50px" Text="0"></asp:TextBox></td>
        <td>回复经验</td>
        <td class="White"><asp:TextBox ID="UserRoleRePostExp" runat="server" Width="50px" Text="0"></asp:TextBox></td>
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
