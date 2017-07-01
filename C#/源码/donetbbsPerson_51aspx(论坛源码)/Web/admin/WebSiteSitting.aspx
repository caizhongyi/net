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
<%@ Page Language="C#" Inherits="WebSite.Admin.WebSiteSitting"%>
<%@ OutputCache Duration="1" VaryByParam="none" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>系统参数</title>
    <link href="style/css.css" type=text/css rel=stylesheet><link />
      <script language = "javaScript" src = "js/jscript.js" type="text/javascript"></script>
</head>
<IFRAME id="IframeTarGet" name="IframeTarGet" marginWidth="0" marginHeight="0" src="about:blank" frameBorder="0" width="0" scrolling="no" height="0"></IFRAME>
<body>
<base target="_self" />
    <form id="FormLoading" runat="server" name="FormLoading">
    

<table width="505" height="286" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td valign="middle" class="MinWindowTop"><img src="images/icon_02.gif" width="8" height="8"> 系统配置</td>
  </tr>
  <tr>
    <td align="center" valign="top" class="MinWindowBody"><TABLE width="100%" border=0 align=center cellPadding=3 cellSpacing=1>
        <TR>
          <TD width="98" align=middle>会员总数</TD>
          <TD width="126" height="24" align=left>
              <asp:TextBox ID="Forum_UserNumber" runat="server" Width="80px"></asp:TextBox></TD>
          <TD width="100" align=middle>在线人数</TD>
          <TD width="154" height="24" align=left><asp:TextBox ID="Forum_AllOnline" runat="server" Width="80px"></asp:TextBox></TD>
        </TR>
        <TR>
          <TD align=middle>在线会员</TD>
          <TD height=24 align=left><asp:TextBox ID="Forum_UserOnline" runat="server" Width="80px"></asp:TextBox></TD>
          <TD align=middle>在线游客</TD>
          <TD height="24" align=left><asp:TextBox ID="Forum_GuestOnline" runat="server" Width="80px"></asp:TextBox></TD>
        </TR>
        <TR>
          <TD align=middle>最高在线人数</TD>
          <TD height=24 align=left><asp:TextBox ID="Forum_MaxOnline" runat="server" Width="80px"></asp:TextBox></TD>
          <TD align=middle>最高在线日</TD>
          <TD height="24" align=left><asp:TextBox ID="Forum_MaxOnlineDate" runat="server" Width="80px"></asp:TextBox></TD>
        </TR>
        <TR>
          <TD align=middle>帖子总数</TD>
          <TD height=24 align=left><asp:TextBox ID="Forum_PostNumber" runat="server" Width="80px"></asp:TextBox></TD>
          <TD align=middle>主题总数</TD>
          <TD height="24" align=left><asp:TextBox ID="Forum_TopicNumber" runat="server" Width="80px"></asp:TextBox></TD>
        </TR>
        <TR>
          <TD align=middle>今日发表数</TD>
          <TD height=24 align=left><asp:TextBox ID="Forum_TodayNumber" runat="server" Width="80px"></asp:TextBox></TD>
          <TD align=middle>昨日发表数</TD>
          <TD height="24" align=left><asp:TextBox ID="Forum_YesTerdayNumber" runat="server" Width="80px"></asp:TextBox></TD>
        </TR>
        <TR>
          <TD align=middle>最高发表数</TD>
          <TD height=24 align=left><asp:TextBox ID="Forum_MaxPostNumber" runat="server" Width="80px"></asp:TextBox></TD>
          <TD align=middle>最高发表日</TD>
          <TD height="24" align=left><asp:TextBox ID="Forum_MaxPostDate" runat="server" Width="80px"></asp:TextBox></TD>
        </TR>
       
        
        <TR>
          <TD align=middle>最新会员</TD>
          <TD height=24 align=left><asp:TextBox ID="Forum_LastUserNickName" runat="server" Width="115px"></asp:TextBox></TD>
          <TD align=middle>最新会员ID</TD>
          <TD height="24" align=left><asp:TextBox ID="Forum_LastUserID" runat="server" Width="80px"></asp:TextBox></TD>
        </TR>
        
        
        <TR>
          <TD align=middle>建站日期</TD>
          <TD height=24 align=left><asp:TextBox ID="Forum_StartDate" runat="server" Width="115px"></asp:TextBox></TD>
          <TD align=middle>系统当前日期</TD>
          <TD height="24" align=left><asp:TextBox ID="Forum_TodyDate" runat="server" Width="115px"></asp:TextBox></TD>
        </TR>
        <TR>
          <TD height=24 align=middle>禁闭IP关键字</TD>
          <TD height=24 colspan="3" align=left><asp:TextBox ID="Forum_LockIP" runat="server" Width="355px"></asp:TextBox></TD>
        </TR>
        <TR>
          <TD height=24 align=middle>禁闭用户关键字</TD>
          <TD height=24 colspan="3" align=left><asp:TextBox ID="Forum_UserIllegal" runat="server" Width="355px"></asp:TextBox></TD>
        </TR>
        <TR>
          <TD height=24 align=middle>禁闭关键字</TD>
          <TD height=24 colspan="3" align=left><asp:TextBox ID="Forum_SystemIllegal" runat="server" Width="354px"></asp:TextBox></TD>
        </TR>
    </TABLE></td>
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
