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
<%@ Page Language="C#" Inherits="WebSite.Admin.User.UserOnlineList"%>
<%@ OutputCache Duration="1" VaryByParam="none" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>在线记录</title>
    <link href="../style/css.css" type=text/css rel=stylesheet><link />
      <script language = "javaScript" src = "../js/jscript.js" type="text/javascript"></script>
</head>
<IFRAME id="IframeTarGet" name="IframeTarGet" marginWidth="0" marginHeight="0" src="about:blank" frameBorder="0" width="0" scrolling="no" height="0"></IFRAME>
<body>
<base target="_self" />
    <form id="FormLoading" runat="server" name="FormLoading">
    

<table width="788" height="499" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td width="97" valign="middle" class="MinWindowTop"><img src="../images/icon_02.gif" width="8" height="8"> 记录列表</td>
    <td width="691" valign="middle" class="MinWindowTop"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="601">用户名称<asp:TextBox ID="searchKey" runat="server" Width="60px"></asp:TextBox> 
          
          时间
          <asp:DropDownList ID="searchRegTime" runat="server">
            
            </asp:DropDownList>
          排序
            <asp:DropDownList ID="searchOrderby" runat="server">
            
            </asp:DropDownList>
            </td>
            <td><div class="Buttom" style=" margin-bottom:4px;">
                <asp:LinkButton ID="serachButton" runat="server" OnClick="serachButton_Click">搜索</asp:LinkButton>
            </div></td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td colspan="2" align="center" valign="top" class="MinWindowBody"><table width="98%" border="0" cellspacing="1" cellpadding="0">
      <tr>
        <td width="10%" >用户名称</td>
        <td align="center"  height="26">来源</td>
        <td width="10%" align="center" >IP</td>
        <td  width="15%" align="center">所在位置</td>
        <td width="10%" align="center" >操作系统</td>
        <td width="14%" align="center" >时间</td>
        <td width="6%" align="right" >操作</td>
        </tr>
		
		<asp:Repeater ID="dataRepeater" runat="server" >
		<ItemTemplate>
      <tr>
            
        <td align="left" class="White" height="24px"><%#DataBinder.Eval(Container.DataItem, "UserOnLineUserNickName").ToString()%></td>
        <td align="left" class="White"><a href="<%#DataBinder.Eval(Container.DataItem, "UserOnLineComeFromPath").ToString()%>" target=_blank><%#DoNetBbs.DoNetBbsClassHepler.Instance().GetFewChars(DataBinder.Eval(Container.DataItem, "UserOnLineComeFromPath").ToString(), 45)%></a></td>
        <td align="left" class="White"><%#DataBinder.Eval(Container.DataItem, "UserOnLineIP").ToString()%></td>
        <td align="left" class="White"><a href="<%#DataBinder.Eval(Container.DataItem, "UserOnLineBrowserPath").ToString()%>" target=_blank><%#DoNetBbs.DoNetBbsClassHepler.Instance().GetFewChars(DataBinder.Eval(Container.DataItem, "UserOnLineBrowserTitle").ToString(), 18)%></a></td>
        <td align="left" class="White"><%#DataBinder.Eval(Container.DataItem, "UserOnLineSystem").ToString()%></td>
        <td align="left" class="White"><%#DataBinder.Eval(Container.DataItem, "UserOnLineLastTime").ToString()%></td>
        <td align="right" class="White"><a href='<%# DataBinder.Eval(Container.DataItem,"UserOnLineID", "javascript:JsDeleteInfo(\"UserOnlineList.aspx?deleteUserOnLineID={0}\");") %>'>删除</a></td>
        </tr>
     </ItemTemplate>
		</asp:Repeater>
    </table></td>
  </tr>
  <tr>
    <td colspan="2" class="MinWindowBottom"><table border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="690px" height="17" align="center"> 
        <div style=" float:right; padding-right:100px;"><asp:Label ID="PageListText" runat="server" Text=""></asp:Label></div>
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
