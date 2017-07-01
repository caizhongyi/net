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
<%@ Page Language="C#" Inherits="WebSite.Admin.Board.BigBoard"%>
<%@ OutputCache Duration="1" VaryByParam="none" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>论坛管理</title>
    <link href="../style/css.css" type=text/css rel=stylesheet><link />
      <script language = "javaScript" src = "../js/jscript.js" type="text/javascript"></script>
      <script language = "javaScript" src = "../../JScript/WebSiteJScriptSeletGroup.js" type="text/javascript"></script>
</head>
<IFRAME id="IframeTarGet" name="IframeTarGet" marginWidth="0" marginHeight="0" src="about:blank" frameBorder="0" width="0" scrolling="no" height="0"></IFRAME>
<body>
<base target="_self" />
    <form id="FormLoading" runat="server" name="FormLoading">
    
<table width="447" height="302" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td valign="middle" class="MinWindowTop"><img src="../images/icon_02.gif" width="8" height="8"> 
        <asp:Label ID="TitleType" runat="server" Text="增加论坛"></asp:Label>
        <asp:Label ID="TitleType1" runat="server" Text="修改论坛" Visible="false"></asp:Label>
        <asp:Label ID="BoardID" runat="server" Text="0" Visible="false"></asp:Label>
    </td>
  </tr>
  <tr>
    <td align="center" valign="top" class="MinWindowBody"><table width="98%" border="0" cellspacing="1" cellpadding="0">
      <tr>
        <td width="22%" height="24">大类名称</td>
        <td colspan="3" class="White">
            <asp:TextBox ID="BoardName" runat="server" Width="220px"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td width="22%" height="24">启动</td>
        <td width="24%" class="White">
            <asp:RadioButtonList ID="BoardFalse" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True" Value="0">是</asp:ListItem>
                <asp:ListItem Value="1">否</asp:ListItem>
            </asp:RadioButtonList>
          </td>
        <td width="16%">排序</td>
        <td width="38%" class="White">
            <asp:TextBox ID="BoardOrders" runat="server" style="width:40px;" Text="0"></asp:TextBox>
      </tr>
      
      <tr>
        <td width="22%" height="24">能否发布主题</td>
        <td colspan="3" class="White">
        <asp:RadioButtonList ID="BoardTypeID" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Selected="True" Value="0">否</asp:ListItem>
                <asp:ListItem Value="1">是</asp:ListItem>
            </asp:RadioButtonList>
        </td>
      </tr>
       <tr>
        <td width="22%" height="24">论坛斑竹</td>
        <td colspan="3" class="White">
        <asp:TextBox ID="BoardMaster" runat="server" Width="220px"></asp:TextBox> 如 1,2,3
        </td>
      </tr>
      <tr>
        <td width="22%" height="24">浏览角色</td>
        <td colspan="3" class="White">
        <asp:TextBox ID="BoardViewRole" runat="server" style="width:220px;"></asp:TextBox> <a href="javascript:WebSiteShowViewPower(0,'BoardViewRole')" >选择角色</a>
        </td>
      </tr>
      <tr>
        <td width="22%" height="24">回复角色</td>
        <td colspan="3" class="White"><asp:TextBox ID="BoardRePostRole" runat="server" style="width:220px;"></asp:TextBox> <a href="javascript:WebSiteShowViewPower(0,'BoardRePostRole')">选择角色</a></td>
      </tr>
      <tr>
        <td width="22%" height="24">发表角色</td>
        <td colspan="3" class="White">
        <asp:TextBox ID="BoardPostRole" runat="server" style="width:220px;"></asp:TextBox> <a href="javascript:WebSiteShowViewPower(0,'BoardPostRole')">选择角色</a>
        </td>
      </tr>
      <tr>
        <td height="107">基本介绍</td>
        <td colspan="3" valign="top" class="White">
            <asp:TextBox ID="BoardAbout" runat="server" TextMode="MultiLine" style="width:320px; height:110px;"></asp:TextBox></td>
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
