<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="productAdd.aspx.cs" Inherits="productAdd"  validateRequest="false"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link href="css/b.css" rel="stylesheet" type="text/css" />
<link href="css/c.css" rel="stylesheet" type="text/css" />
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
    <div class="gl_right_2">
      <h1>产品录入</h1>
      <dl>
        <dd>分类：</dd><dt></dt><select name="select" id="slTYpe" runat=server>
            <option selected="selected">男     装</option>
            <option>女     装</option>
          </select>
       
      </dl>
         <dl>
        <dd>地域：</dd><dt></dt> <select id=slDy runat=server></select>
       
      </dl>
      <dl>
        <dd>价格：</dd><dt><input name="input" type="text" id=txtPrice runat=server />
        </dt>
      </dl>
      <dl>
        <dd>名称：</dd><dt><input name="" type="text" id=txtName runat=server />
        </dt>
      </dl>
     <dl>
        <dd>展示图片：</dd><dt class="dtt_1"><input id="file1" type="file" runat=server />
        </dt>
       
         
      
      </dl>
      <dl>
        <dd>产品链接：</dd><dt class="dtt_1"><input name="" type="text" id=txtAddress runat=server />
        </dt>
       
         <dt>
            
         </dt>
      
      </dl>
      <dl>
      <div class="clr"></div>
        <dd>说明：</dd><dt class="dtt_1"><textarea name="textarea" id="textarea"  cols="60" rows="10" runat=server></textarea>
          
      </dt>
       
      
      </dl>
      <div class="clr"></div>
      <p>        
            
          <asp:Button ID="Button1" runat="server" Text="提交" OnClick="Button1_Click" />
</p>
      
    </div>
    <!-- gl_right -->
  </div>
  <div class="clr"></div>
</asp:Content>

