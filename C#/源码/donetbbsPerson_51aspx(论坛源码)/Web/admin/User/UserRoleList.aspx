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
<%@ Page Language="C#" Inherits="WebSite.Admin.User.UserRoleList"%>
<%@ OutputCache Duration="1" VaryByParam="none" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>角色列表</title>
    <link href="../style/css.css" type=text/css rel=stylesheet><link />
      <script language = "javaScript" src = "../js/jscript.js" type="text/javascript"></script>
</head>
<IFRAME id="IframeTarGet" name="IframeTarGet" marginWidth="0" marginHeight="0" src="about:blank" frameBorder="0" width="0" scrolling="no" height="0"></IFRAME>
<body>
<base target="_self" />
    <form id="FormLoading" runat="server" name="FormLoading">
    
<table width="447" height="314" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td valign="middle" class="MinWindowTop">
    <div style="float:left"><img src="../images/icon_02.gif" width="8" height="8"> 角色管理</div>
    
   <div class="Buttom" style=" margin-bottom:0px; float:right">
               <a href="javascript:JsOpenModalDialog('UserRole.aspx','447','318','0');">增加</a>
            </div>
    </td>
  </tr>
  <tr>
    <td align="center" valign="top" class="MinWindowBody"><table width="98%" border="0" cellspacing="1" cellpadding="0">
      <tr>
        <td height="24" align="left" class="Contents">角色名称</td>
        <td width="22%" align="center" class="Contents">开启</td>
        <td width="22%" align="right" class="Contents">操作</td>
      </tr>
	  <asp:Repeater ID="dataRepeater" runat="server" >
		<ItemTemplate>
      <tr>
        <td height="24" class="White"><%#DataBinder.Eval(Container.DataItem, "UserRoleTitle").ToString()%></td>
        <td align="center" class="White"><%#int.Parse(DataBinder.Eval(Container.DataItem, "UserRoleFalse").ToString()) == 0 ? "是" : "否"%></td>
        <td align="right" class="White"><a href='<%# DataBinder.Eval(Container.DataItem,"UserRoleID", "javascript:JsDeleteInfo(\"UserRole.aspx?deleteUserRoleID={0}\");") %>'>删除</a> <a href='<%# DataBinder.Eval(Container.DataItem,"UserRoleID", "javascript:JsOpenModalDialog(\"UserRole.aspx?UserRoleID={0}\",\"447\",\"318\",\"0\");") %>'>修改</a></td>
      </tr>
	  </ItemTemplate>
		</asp:Repeater>
    </table></td>
  </tr>
  <tr>
    <td class="MinWindowBottom"><table border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="350px" height="17" align="center"> 
        <div style=" float:right; padding-right:60px;"><asp:Label ID="PageListText" runat="server" Text=""></asp:Label></div>
         </td>
    <td><div class="Buttom" style=" margin-bottom:4px;">
               <a href="javascript:window.close();">确定</a>
            </div></td>
      </tr>
    </table>
    </td>
  </tr>
</table>

    </form>
</body>
</html>
