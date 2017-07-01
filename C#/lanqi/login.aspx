<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<link href="css/b.css" rel="stylesheet" type="text/css" />

<div class="clr"></div>
  <div class="Login">
  <div class="login2">
  <p class="up_11"><img src="images/index_8.jpg" width="84" height="40" /></p>
  <p class="pp1">已注册用户请从这里登录</p>
  <dl><dd>E-Mail：</dd><dt><input name="" type="text" id=dlUsername runat=server />
  *</dt></dl>
  <dl>
    <dd>密&nbsp; 码：</dd><dt><input name="" type="password" id=dlPassword runat=server />
  *</dt></dl>

  <dl><dd>验证码：</dd><dt class="dtt"><input name="" type="text" size="4" id=yzm runat=server />
  </dt>
  <dt class="dtt"><img src="js/ValidateCode.aspx" style="cursor:pointer; vertical-align: middle;" onclick="this.src=this.src+'?randnum='+Math.random"  alt="点击更换验证码" border="0" /></dt><dt class="denglu "><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/button_dl.gif" OnClick="ImageButton1_Click" />
         
  
     </dt>
  
     
   
   <dt class="dtt">&nbsp;</dt></dl></div>
  
  
  
  
  
  
  </div>
  <div class="up">
    <div class="login2">
      <p class="up_11"><img src="images/index_9.jpg"/></p>
    
      <dl>
        <dd class="ddd">E-Mail：</dd><dt class="ddd"><input name="input" type="text" id=txtName runat=server />
          *</dt></dl>  <p class="pp1">填写有效Email地址作为下次登录的用户名</p>
      <dl>
        <dd>密&nbsp; 码：</dd><dt><input name="input" type="password" id=txtPwd runat=server />
          *</dt></dl>
      
      
       <dl>
        <dd>再次输入密码：</dd><dt><input name="input" type="password" id=txtRePwd runat=server />
          *</dt><dt class="denglu "><asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="images/button_dl_1.gif" OnClick="ImageButton2_Click" />
     </dt>
      </dl>
    </div>
  </div>
  
  <div class="clr"></div>
</asp:Content>

