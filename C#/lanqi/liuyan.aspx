<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="liuyan.aspx.cs" Inherits="liuyan"  %>
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
    <div class="liuyan">
      <p class="ddd_2">帐户管理 > 我要提问：</p>
      
        <dl>
          <dd>您的提问:</dd><dt><input name="" type="text" value="" size="80" id=txtTitle runat=server />
          </dt><dt><span>*</span></dt></dl>
        
        <div class="clr"></div>
         <dl>
          <dd>问题说明:</dd><dt><textarea name="" cols="68" rows="20" id=txtContent runat=server></textarea>
      </dl>
        <div class="clr"></div>
        <div class="ti_1">
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/twfb__.jpg" OnClick="ImageButton1_Click" /></div>
    </div>
    <!-- gl_right -->
  </div>
  <div class="clr"></div>
</asp:Content>

