<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pwdUpdate.aspx.cs" Inherits="pwdUpdate"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link href="css/b.css" rel="stylesheet" type="text/css" />
 <div class="clr"></div>
  <div class="guanli">
       <h2>您好，<%=username %>  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">退出登陆</asp:LinkButton></h2>
    <!-- gl_left -->
    <div class="gl_left">
      <div class="huiyuan_1">
        <dl>
          <dd><%=username %></dd><dt>您好！欢迎光临东海旅游购物网。</dt><dt>会员级别：<%=xueli %></dt></dl>
      </div>
      <p class="p_1">商品管理</p>
      <div class="dingdan"><a href="productManager.aspx">暂存架</a></div>
      <p class="p_1">个人信息管理</p>
      <div class="dingdan_2">
        <p><a href="userInfo.aspx">编辑个人档案</a></p>
        <p><a href="pwdupdate.aspx">修改密码</a></p>
      </div>
    </div>
    <!-- gl_left -->
    <!-- gl_right -->
    <div class="xiugai">
      <p class="ttd_2">帐户管理 >修改密码</p>
 <dl>
 <dd>我的旧密码：</dd><dt><input name="" type="password" id=oldpwd runat=server /></dt><dt>*</dt></dl>
   <div class="clr"></div>  
   
   
   <dl>
 <dd>请输入新密码：</dd><dt><input name="" type="password"  id=newpwd runat=server/></dt><dt>*必须大于六位数</dt></dl>
   <div class="clr"></div>   
        
   <dl>
 <dd>请再次输入新密码：</dd><dt><input name="" type="password"  id=renewpwd runat=server/></dt><dt>*必须大于六位数</dt></dl>
   <div class="clr"></div>   
   
   
   <div class="an_niu">
     
       <label>
           <asp:Button ID="Button1" runat="server" Text="修改密码" OnClick="Button1_Click" />
       </label>
    
   </div>
        
    </div>
    <!-- gl_right -->
  </div>
  <div class="clr"></div>
</asp:Content>

