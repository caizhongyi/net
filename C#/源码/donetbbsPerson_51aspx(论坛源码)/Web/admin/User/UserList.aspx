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
<%@ Page Language="C#" Inherits="WebSite.Admin.User.UserList"%>
<%@ OutputCache Duration="1" VaryByParam="none" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>用户列表</title>
    <link href="../style/css.css" type=text/css rel=stylesheet><link />
      <script language = "javaScript" src = "../js/jscript.js" type="text/javascript"></script>
</head>
<IFRAME id="IframeTarGet" name="IframeTarGet" marginWidth="0" marginHeight="0" src="about:blank" frameBorder="0" width="0" scrolling="no" height="0"></IFRAME>
<body>
<base target="_self" />
    <form id="FormLoading" runat="server" name="FormLoading">
    

<table width="688" height="499" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td width="60" valign="middle" class="MinWindowTop"><img src="../images/icon_02.gif" width="8" height="8"> 用户列表</td>
    <td  valign="middle" class="MinWindowTop"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td>用户名称<asp:TextBox ID="searchKey" runat="server" Width="60px"></asp:TextBox> 
          
          性别 
          <asp:DropDownList ID="searchSex" runat="server">            
            </asp:DropDownList>
          注册日期
          <asp:DropDownList ID="searchRegTime" runat="server">
            
            </asp:DropDownList>
          排序
            <asp:DropDownList ID="searchOrderby" runat="server">
            
            </asp:DropDownList>
            </td>
            <td>
            
            <div class="Buttom" style=" margin-bottom:4px; float:left">
                <asp:LinkButton ID="serachButton" runat="server" OnClick="serachButton_Click">搜索</asp:LinkButton>
            </div>
            <div class="Buttom" style=" margin-bottom:4px; float:left; margin-left:4px;">
                <a href="javascript:JsOpenModalDialog('UserInfo.aspx','688','565','0')">增加</a>
            </div>
            
            </td>
      </tr>
    </table></td>
  </tr>
  <tr>
    <td colspan="2" align="center" valign="top" class="MinWindowBody"><table width="98%" border="0" cellspacing="1" cellpadding="0">
      <tr>
        <td width="14%" height="26" class="Contents">登录名称</td>
        <td width="10%" align="center" class="Contents">用户ID</td>
        <td width="16%" align="center" class="Contents">昵称</td>
        <td width="8%" align="center" class="Contents">性别</td>
        <td width="14%" align="center" class="Contents">注册日期</td>
        <td width="16%" align="center" class="Contents">最后活动</td>
        <td width="9%" align="center" class="Contents">登录次数</td>
        <td width="13%" align="right" class="Contents">操作</td>
        </tr>
		
		<asp:Repeater ID="dataRepeater" runat="server" >
		<ItemTemplate>
      <tr>
            
        <td height="24" class="White"><%#DataBinder.Eval(Container.DataItem,"UserName").ToString()%></td>
        <td align="center" class="White"><%#DataBinder.Eval(Container.DataItem,"UserID").ToString()%></td>
        <td align="center" class="White"><%#DataBinder.Eval(Container.DataItem,"UserNickName").ToString()%></td>
        <td align="center" class="White"><%#DataProviders.DataConnectionHepler.Instance().GetResourcesXmlNode("Resource_UserSex"+DataBinder.Eval(Container.DataItem,"UserSex").ToString())%></td>
        <td align="center" class="White"><%#System.Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "UserRegTime")).ToString("yy-MM-dd HH:mm")%></td>
        <td align="center" class="White"><%#System.Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "UserLastActTime")).ToString("yy-MM-dd HH:mm:ss")%></td>
        <td align="center" class="White"><%#DataBinder.Eval(Container.DataItem,"UserLoginNumber").ToString()%></td>
        <td align="right" class="White"><a href='<%# DataBinder.Eval(Container.DataItem,"UserID", "javascript:JsOpenModalDialog(\"UserInfo.aspx?UserID={0}\",\"688\",\"565\",\"0\");") %>'>修改</a></td>
        </tr>
     </ItemTemplate>
		</asp:Repeater>
    </table></td>
  </tr>
  <tr>
    <td colspan="2" class="MinWindowBottom"><table border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="590px" height="17" align="center"> 
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
