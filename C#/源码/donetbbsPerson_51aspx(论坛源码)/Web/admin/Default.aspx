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
<%@ Page Language="C#" Inherits="WebSite.Admin.DeFault"%>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>系统管理</title><LINK 
href="style/css.css" type=text/css rel=stylesheet>
      <script language = "javaScript" src = "js/jscript.js" type="text/javascript"></script>
</head>
<IFRAME id="WebSiteTarGet" name="WebSiteTarGet" marginWidth="0" marginHeight="0" src="about:blank" frameBorder="0" width="0" scrolling="no" height="0"></IFRAME>
<body>
    <form id="FormLoading" runat="server" name="FormLoading">
    <TABLE width="100%" height=32 border=0 cellPadding=0 cellSpacing=0 class="Topbg">
  
  <TR>
    <TD width=147><A 
      href="http://www.donetbbs.com" target=_blank><IMG height=32 src="images/top_logo.gif" width=147 border=0></A></TD>
  <TD align=right><A 
      href="../Default.aspx"><img src="images/list.gif" width="10" height="12" border="0"> 返回主页</A>&nbsp;</TD>
  </TR></TABLE>
<TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>
  
  <TR>
    <TD   height=1></TD></TR>
    </TABLE>
<TABLE 
width="100%" height="580" border=0 cellPadding=0 cellSpacing=0>
  
  <TR>
    <TD width=180 height=553 align=middle
    vAlign=top class="Left">
      <TABLE cellSpacing=0 cellPadding=0 width=158 border=0>
        
        <TR>
          <TD align=middle width=158 background=images/title.gif 
          height=41>&nbsp;</TD></TR></TABLE>
      <TABLE cellSpacing=0 cellPadding=0 width=158 border=0>
        
        <TR>
          <TD width=158 align=left vAlign=top class="Content">
          
              <asp:Panel ID="ManageBoardPanel" runat="server" Visible="false">
          <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>
              <TR>
                <TD height=24 align=left class="Leftmenu"><img src="images/icon_02.gif" width="8" height="8" border="0"> 论坛管理 (<a href="javascript:JsOpenModalDialog('Board/BigBoard.aspx','454','396',0)">增加大类</a>) </TD>
              </TR>
            </TABLE>
			<div style=" height:250px;overflow:auto;" class="White">
			
			<TABLE cellSpacing=0 cellPadding=0 width="100%" >
              <TR>
                <TD height=24 align=left id="BoardTreeTable">
                </TD>
                </TR>
                </TABLE>
			 <script language = "javaScript" src = "js/tree.js" type="text/javascript"></script>
			<script>JsXmlHttp('Board/BoardTree.aspx','BoardTreeTable');</script>
			</div>
			
              </asp:Panel>
			
			<asp:Panel ID="ManageUserPanel" runat="server" Visible="false">
            <TABLE cellSpacing=0 cellPadding=0 width="100%" >
              <TR>
                <TD height=24 align=left class="Leftmenu"><img src="images/icon_02.gif" width="8" height="8" border="0"> 会员管理</TD>
              </TR>
            </TABLE>
            <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0 class="White">
              <TR>
                <TD height=24 align=left><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td height="20"><img src="images/noexpand.gif" width="9" height="9"><img src="images/bullet.gif" width="9" height="9"> <a href="javascript:JavaScriptOpenMidWinow('User/UserList.aspx','UserList','690','500',0)">会员列表</a></td>
                  </tr>
                </table>
                  <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                      <td height="20"><img src="images/noexpand.gif" width="9" height="9"><img src="images/bullet.gif" width="9" height="9"> <a href="javascript:JavaScriptOpenMidWinow('User/UserOnlineList.aspx','UserOnlineList','790','500',0)">在线记录列表</a></td>
                    </tr>
                  </table></TD>
              </TR>
            </TABLE>
            
              </asp:Panel>
            
            <asp:Panel ID="ManageSystemPanel" runat="server" Visible="false">
            <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>
              <TR>
                <TD height=24 align=left class="Leftmenu"><img src="images/icon_02.gif" width="8" height="8" border="0"> 系统配置</TD>
              </TR>
            </TABLE>
            <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0 class="White">
              <TR>
                <TD height=24 align=left><table width="100%" border="0" cellspacing="0" cellpadding="0">
                  <tr>
                    <td height="20"><img src="images/noexpand.gif" width="9" height="9"><img src="images/bullet.gif" width="9" height="9"> <a href="javascript:JsOpenModalDialog('WebSiteSitting.aspx','505','414',0)">系统参数</a></td>
                  </tr>
                </table>
                  <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                      <td height="20"><img src="images/noexpand.gif" width="9" height="9"><img src="images/bullet.gif" width="9" height="9"> <a href="javascript:JavaScriptOpenMidWinow('User/UserRoleList.aspx','UserRoleList','455','316',0)">角色管理</a></td>
                    </tr>
                  </table>
                  <table width="100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                      <td height="20"><img src="images/noexpand.gif" width="9" height="9"><img src="images/bullet.gif" width="9" height="9"> <a href="javascript:JavaScriptOpenMidWinow('User/UserLevelList.aspx','UserLevelList','455','316',0)">等级管理</a></td>
                    </tr>
                  </table>
                  </TD>
              </TR>
            </TABLE>
            </asp:Panel>
            
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
              <tr>
                <td height="67">版权所有：<BR>
<A href="http://www.donetbbs.com/Forums" target=_blank>东网技术服务有限公司</A><BR>
支持论坛：<BR>
<A href="http://www.donetbbs.com/Forums" target=_blank>东网论坛</A></td>
              </tr>
            </table></TD>
        </TR></TABLE></TD>
    <TD  vAlign=top>
      <TABLE cellSpacing=0 cellPadding=0 width="98%" align=center border=0>
        
        <TR>
          <TH vAlign=top width="100%" colSpan=2 
          height=4></TH></TR></TABLE>
      <TABLE width="98%" height="584" border=0 align=center cellPadding=0 cellSpacing=0 class="Left">
        
        <TR>
          <TD width="93%" height=24 align="left" vAlign=middle>&nbsp;<img src="images/icon_02.gif" width="8" height="8"> 在线记录 (即时汇报) </TD>
          <TD width="7%" height=24 align="center"> </TD>
        </TR>
        <TR>
          <TD height=549 colSpan=2 align="center" vAlign=top class="Contents" id="RefreshUserOnlineList">
         
          <script>
          function JsRefreshUserOnlineList()
          {
              if (window.setTimeoutRefreshUserOnlineList)
                {
		            clearTimeout(setTimeoutRefreshUserOnlineList)
		        }
		  JsXmlHttp('user/RefreshUserOnlineList.aspx','RefreshUserOnlineList');
          setTimeoutRefreshUserOnlineList=setTimeout("JsRefreshUserOnlineList()",10000)
          }
          JsRefreshUserOnlineList();
          </script>
          </TD>
        </TR>
      </TABLE>
    </TD>
  </TR>
	  
</TABLE>
<TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>
  
  <TR>
    <TD  
  height=1></TD></TR></TABLE>
    </form>
</body>
</html>
