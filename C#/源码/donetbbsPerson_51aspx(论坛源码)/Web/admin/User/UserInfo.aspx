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
<%@ Page Language="C#" Inherits="WebSite.Admin.User.UserInfo"%>
<%@ OutputCache Duration="1" VaryByParam="none" %>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>用户管理</title>
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
    <td  valign="middle" class="MinWindowTop"><div style="float:left"><img src="../images/icon_02.gif" width="8" height="8"> 
    
    <asp:Label ID="LabelType1" runat="server" Text="修改用户"></asp:Label>
        <asp:Label ID="LabelType2" runat="server" Text="增加用户"></asp:Label>
        <asp:Label ID="userID" runat="server" Text="0" Visible="false"></asp:Label>
         </div>
        <div style="float:right; width:168">
   <table border="0" cellspacing="0" cellpadding="0" width="168">
      <tr>
        <td width="80" height="24" align="center" id="UserInfoTitle1" style=" background-color:#CCCCCC" onclick="JsUserInfoTable(1)">基本信息</td>
        <td width="8"></td>
        <td width="80" align="center"  id="UserInfoTitle2" style=" background-color:#ffffff"  onclick="JsUserInfoTable(2)">详细信息</td>
      </tr>
    </table>
    </div>
    </td>
  </tr>
  <tr>
    <td align="center" valign="top" class="MinWindowBody"><table width="98%" border="0" cellspacing="1" cellpadding="0" id="UserInfoTable1">
      <tr>
        <td height="24">会员名称</td>
        <td width="24%" class="White"><asp:TextBox ID="UserName" runat="server" Width="140px"></asp:TextBox></td>
        <td width="16%">会员昵称</td>
        <td width="38%" class="White"><asp:TextBox ID="UserNickName" runat="server" Width="140px"></asp:TextBox></td>
      </tr>
      <tr>
        <td width="22%" height="24">用户密码</td>
        <td class="White"><asp:TextBox ID="UserPassWord" runat="server" Width="140px"></asp:TextBox></td>
        <td>密码答案</td>
        <td class="White"><asp:TextBox ID="UserPassWordAnswer" runat="server" Width="140px"></asp:TextBox></td>
      </tr>
      <tr>
        <td height="24">身份证号码</td>
        <td class="White"><asp:TextBox ID="UserIdCard" runat="server" Width="140px"></asp:TextBox></td>
        <td>是否禁闭</td>
        <td class="White">
            <asp:RadioButtonList ID="UserFalse" runat="server" RepeatDirection="Horizontal">
            </asp:RadioButtonList></td>
        </tr>
      <tr>
        <td height="24">保密方式</td>
        <td class="White">
            <asp:DropDownList ID="UserPrivacy" runat="server">
            </asp:DropDownList>
        </td>
        <td>接收短信</td>
        <td class="White">
            <asp:DropDownList ID="UserReceiveType" runat="server">
            </asp:DropDownList></td>
        </tr>
      <tr><td height="24">婚姻状况</td>
        <td class="White" colspan="3">
            <asp:RadioButtonList ID="UserMaritalStatus" runat="server" RepeatDirection="Horizontal">
            </asp:RadioButtonList></td>
        </tr>
      <tr>
        <td height="24">会员积分</td>
        <td class="White"><asp:TextBox ID="UserPoint" runat="server" Width="40px">0</asp:TextBox></td>
        <td>个人信誉</td>
        <td class="White"><asp:TextBox ID="UserPrestige" runat="server" Width="40px">100</asp:TextBox></td>
      </tr>
      <tr>
        <td height="24">会员等级</td>
        <td class="White">
            <asp:DropDownList ID="UserLevelID" runat="server">
            </asp:DropDownList></td>
        <td>在线状态</td>
        <td class="White"><asp:DropDownList ID="UserOnLineStatic" runat="server">
            </asp:DropDownList></td>
      </tr>
      <tr>
        <td height="24">来自那里</td>
        <td class="White"><asp:TextBox ID="UserComeFrom" runat="server" Width="140px">上海市市区</asp:TextBox></td>
        <td>手机号码</td>
        <td class="White"><asp:TextBox ID="UserMobile" runat="server" Width="140px"></asp:TextBox></td>
      </tr>
      <tr>
        <td height="24">真实姓名</td>
        <td class="White"><asp:TextBox ID="UserTrueName" runat="server" Width="140px"></asp:TextBox></td>
        <td>性 别</td>
        <td class="White">
            <asp:RadioButtonList ID="UserSex" runat="server" RepeatDirection="Horizontal">
            </asp:RadioButtonList></td>
      </tr>
      <tr>
        <td height="24">毕业院校</td>
        <td colspan="3" class="White"><asp:TextBox ID="UserSchool" runat="server" Width="200px"></asp:TextBox></td>
        </tr>
      <tr>
        <td height="24">年 龄</td>
        <td class="White"><asp:TextBox ID="UserBirthday" runat="server" Width="140px">1980-02-08</asp:TextBox></td>
        <td>推 荐 人</td>
        <td class="White"><asp:TextBox ID="UserRecommendUser" runat="server" Width="140px"></asp:TextBox></td>
      </tr>
      <tr>
        <td height="24">邮件地址</td>
        <td class="White"><asp:TextBox ID="UserEmail" runat="server" Width="140px"></asp:TextBox></td>
        
        <td>个性头像</td>
        <td class="White"><asp:TextBox ID="UserFace" runat="server" Width="236px">/images/userface/Image43.gif</asp:TextBox></td>
      </tr>
      <tr>
        <td height="48">个性签名</td>
        <td colspan="3" class="White"><asp:TextBox ID="UserSign" runat="server" Width="410px" Height="37px" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
      
      
      
      <tr>
        <td height="107">个人说明</td>
        <td colspan="3" valign="top" class="White"><asp:TextBox ID="UserAbout" runat="server" Width="410px" Height="101px" TextMode="MultiLine">这家伙很赖，什么都没有留下！</asp:TextBox></td>
      </tr>
    </table>
      <table width="98%" border="0" cellspacing="1" cellpadding="0" id="UserInfoTable2" style="display:none">
        <tr>
          <td height="24">最后登录</td>
          <td width="24%" class="White"><asp:TextBox ID="UserLastLoginTime" runat="server" Width="140px"></asp:TextBox></td>
          <td width="16%">注册时间</td>
          <td width="38%" class="White"><asp:TextBox ID="UserRegTime" runat="server" Width="140px"></asp:TextBox></td>
        </tr>
        <tr>
          <td width="22%" height="24">俱 乐 部</td>
          <td colspan="3" class="White"><asp:TextBox ID="UserGroup" runat="server" Width="210px"></asp:TextBox>
          
          
      <a href="javascript:WebSiteShowViewPower(1,'UserGroup')">俱乐部</a>
          </td>
        </tr>
        <tr>
          <td height="24">所属角色</td>
          <td colspan="3" class="White"><asp:TextBox ID="UserRole" runat="server" Width="210px"></asp:TextBox>
          
           <a href="javascript:WebSiteShowViewPower(0,'UserRole')">选择角色</a>
          
          </td>
        </tr>
        <tr>
          <td height="24">用户经验</td>
          <td class="White"><asp:TextBox ID="UserExp" runat="server" Width="40px">0</asp:TextBox></td>
          <td>用户魅力</td>
          <td class="White"><asp:TextBox ID="UserCP" runat="server" Width="40px">0</asp:TextBox></td>
        </tr>
        <tr>
          <td height="24">虚拟货币</td>
          <td class="White"><asp:TextBox ID="UserMoney" runat="server" Width="40px">0</asp:TextBox></td>
          <td>真实金钱</td>
          <td class="White"><asp:TextBox ID="UserTrueMoney" runat="server" Width="40px">0</asp:TextBox></td>
        </tr>
        <tr>
          <td height="24">拥有点券</td>
          <td class="White"><asp:TextBox ID="UserTicket" runat="server" Width="40px">0</asp:TextBox></td>
          <td>OICQ号码</td>
          <td class="White"><asp:TextBox ID="UserOICQ" runat="server" Width="140px"></asp:TextBox></td>
        </tr>
        <tr>
          <td height="24">发表总数</td>
          <td class="White"><asp:TextBox ID="UserPostNumber" runat="server" Width="40px">0</asp:TextBox></td>
          <td>最后活动</td>
          <td class="White"><asp:TextBox ID="UserLastActTime" runat="server" Width="140px"></asp:TextBox></td>
        </tr>
        <tr>
          <td height="24">发表主题</td>
          <td class="White"><asp:TextBox ID="UserTopicNumber" runat="server" Width="40px">0</asp:TextBox></td>
          <td>回复主题</td>
          <td class="White"><asp:TextBox ID="UserReTopicNumber" runat="server" Width="40px">0</asp:TextBox></td>
        </tr>
        <tr>
          <td height="24">在线时间</td>
          <td class="White"><asp:TextBox ID="UserOnlineTime" runat="server" Width="40px">0</asp:TextBox></td>
          <td>联系电话</td>
          <td class="White"><asp:TextBox ID="UserContactTel" runat="server" Width="140px"></asp:TextBox></td>
        </tr>
        <tr>
          <td height="24">邮编号码</td>
          <td class="White"><asp:TextBox ID="UserCode" runat="server" Width="140px"></asp:TextBox></td>
          <td height="24">登录次数</td>
          <td width="24%" class="White"><asp:TextBox ID="UserLoginNumber" runat="server" Width="40px">0</asp:TextBox></td>
        </tr>
        <tr>
          <td height="24">个人网站</td>
          <td colspan="3" class="White"><asp:TextBox ID="UserWebAddress" runat="server" Width="210px"></asp:TextBox></td>
        </tr>
        <tr>
          <td height="24">个人博客</td>
          <td colspan="3" class="White"><asp:TextBox ID="UserWebLog" runat="server" Width="210px"></asp:TextBox></td>
        </tr>
        <tr>
          <td height="24">个人相册</td>
          <td colspan="3" class="White"><asp:TextBox ID="UserWebGallery" runat="server" Width="210px"></asp:TextBox></td>
        </tr>
        <tr>
          <td height="24">工作单位</td>
          <td colspan="3" valign="top" class="White"><asp:TextBox ID="UserWorkUnit" runat="server" Width="210px"></asp:TextBox></td>
        </tr>
        <tr>
          <td height="24">联系地址</td>
          <td colspan="3" valign="top" class="White"><asp:TextBox ID="UserContactAddress" runat="server" Width="210px"></asp:TextBox></td>
        </tr>
        <tr>
          <td height="83">
              个人爱好</td>
          <td colspan="3" valign="top" class="White">
          <asp:TextBox ID="UserInterests" runat="server" Width="410px" Height="75px" TextMode="MultiLine"></asp:TextBox>
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
