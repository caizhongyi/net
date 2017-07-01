<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="userInfo.aspx.cs" Inherits="userInfo"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link href="css/b.css" rel="stylesheet" type="text/css" />
<div class="clr"></div>
  <div class="guanli">
       <h2>您好，<%=username %> 
           <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">退出登陆</asp:LinkButton></h2>
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
    <div class="gl_right_xinxi">
      <p class="ddd_2">帐户管理 >修改个人信息</p>
   <div class="xinxi">
   <p>个人信息：</p>
   <dl>
   <dd>姓名：</dd><dt><input name="input2" type="text" id=txtName runat=server readonly=readonly />
     <span>*</span></dt></dl>
   <dl>
   <dd>性别：</dd><dt>男<input name="sex" type="radio" value="男" checked="true" id=sexMan runat=server />女<input name="sex" type="radio" value="女" id=sexWomen  runat=server/></dt></dl>
   <dl>
   <dd>地址：</dd><dt class="dt_4"><input name="input" type="text" id=txtAddress runat=server />
   </dt>

   
   
   </dl>
   
   <div class="clr"></div>
   
   <dl>
   <dd>详细信息：</dd><dt><input name="input" type="text" id=txtMessage runat=server />
   </dt>
   
   
   </dl>
   <dl>
   <dd>邮编：</dd><dt><input name="input" type="text" id=txtZip runat=server />
   <span>*</span></dt></dl>
    <p><strong>联系方式</strong>：</p>
    <dl>
   <dd>普通电话：</dd><dt><input name="input" type="text" id=txtPhone runat=server />
   </dt>
   
   
   </dl>
   <dl>
   <dd>手机号码：</dd><dt><input name="input2" type="text" id=txtMobil runat=server />
     <span>*</span></dt></dl>
   
   <div class="tijiao">
       <asp:Button ID="Button1" runat="server" Text="提交" OnClick="Button1_Click" /></div>
   </div>
    </div>
    <!-- gl_right -->
  </div>
  <div class="clr"></div>
</asp:Content>

