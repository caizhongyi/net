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
<%@ Page Language="C#" Inherits="WebSite.Admin.Board.TreeBoard"%>
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
    

<table width="688" height="499" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td valign="middle" class="MinWindowTop"><img src="images/icon_02.gif" width="8" height="8"> 
        <asp:Label ID="LabelType1" runat="server" Text="增加论坛"></asp:Label>
        <asp:Label ID="LabelType2" runat="server" Text="修改论坛" Visible="false"></asp:Label>
    </td>
  </tr>
  <tr>
    <td align="center" valign="top" class="MinWindowBody"><table width="98%" border="0" cellspacing="1" cellpadding="0">
      <tr>
        <td width="22%" height="24">论坛名称</td>
        <td colspan="3" class="White">
        <asp:TextBox ID="BoardID" runat="server" Width="1px" Visible="false" Text="0"></asp:TextBox>
            <asp:TextBox ID="BoardName" runat="server" Width="420px"></asp:TextBox>
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
        </td>
      </tr>
      
      <tr>
        <td width="22%" height="24">能否发布主题</td>
        <td colspan="3" class="White">
        <asp:RadioButtonList ID="BoardTypeID" runat="server" RepeatDirection="Horizontal">
                <asp:ListItem Value="0">否</asp:ListItem>
                <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
            </asp:RadioButtonList>
        </td>
      </tr>
      
      <tr>
        <td width="22%" height="24">论坛专题</td>
        <td colspan="3" class="White">
        <asp:TextBox ID="BoardSubject" runat="server" Width="420px"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td width="22%" height="24">最后发表</td>
        <td colspan="3" class="White">
        <asp:TextBox ID="BoardLastTopicTitle" runat="server" Width="420px"></asp:TextBox>
        </td>
        </tr>
      <tr>
        <td height="24">发表总数</td>
        <td class="White"><asp:TextBox ID="BoardPostNumber" runat="server" Width="40px" Text="0"></asp:TextBox></td>
        <td>主题总数</td>
        <td class="White"><asp:TextBox ID="BoardTopicNumber" runat="server" Width="40px" Text="0"></asp:TextBox></td>
      </tr>
      <tr>
        <td height="24">今日发表数</td>
        <td class="White"><asp:TextBox ID="BoardTodayPostNumber" runat="server" Width="40px" Text="0"></asp:TextBox></td>
        <td>删除帖子扣积分</td>
        <td class="White"><asp:TextBox ID="BoardDelPoint" runat="server" Width="40px" Text="0"></asp:TextBox></td>
      </tr>
      
      <tr>
        <td height="24">最后发表人</td>
        <td class="White">
        <asp:TextBox ID="BoardLastTopicUserNickName" runat="server" Width="150px"></asp:TextBox>
        </td>
        <td>最后发表人ID</td>
        <td class="White">
        <asp:TextBox ID="BoardLastTopicUserID" runat="server" Width="150px" Text="0"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td height="24">最后发表时间</td>
        <td class="White">
        <asp:TextBox ID="BoardLastTopicTime" runat="server" Width="150px"></asp:TextBox>
        </td>
        <td>最后主题ID</td>
        <td class="White"><asp:TextBox ID="BoardLastTopicID" runat="server" Width="150px" Text="0"></asp:TextBox></td>
      </tr>
      
      <tr>
        <td height="24">论坛斑竹</td>
        <td colspan="3" class="White"><asp:TextBox ID="BoardMaster" runat="server" Width="420px"></asp:TextBox></td>
      </tr>
      <tr>
        <td height="24">浏览角色</td>
        <td colspan="3" class="White"><asp:TextBox ID="BoardViewRole" runat="server" Width="420px"></asp:TextBox> <a href="javascript:WebSiteShowViewPower(0,'BoardViewRole')">选择角色</a></td>
      </tr>
      <tr>
        <td height="24">回复角色</td>
        <td colspan="3" class="White"><asp:TextBox ID="BoardRePostRole" runat="server" Width="420px"></asp:TextBox> <a href="javascript:WebSiteShowViewPower(0,'BoardRePostRole')">选择角色</a></td>
      </tr>
      <tr>
        <td height="24">发表角色</td>
        <td colspan="3" class="White"><asp:TextBox ID="BoardPostRole" runat="server" Width="420px"></asp:TextBox> <a href="javascript:WebSiteShowViewPower(0,'BoardPostRole')">选择角色</a></td>
      </tr>
      <tr>
        <td height="24">论坛图片</td>
        <td colspan="3" class="White"><asp:TextBox ID="BoardImages" runat="server" Width="420px"></asp:TextBox> </td>
      </tr>
      
      
      <tr>
        <td height="107">基本介绍</td>
        <td colspan="3" valign="top" class="White">
        <asp:TextBox ID="BoardAbout" runat="server" Width="488px" Height="110px" TextMode="MultiLine"></asp:TextBox>
        </td>
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
