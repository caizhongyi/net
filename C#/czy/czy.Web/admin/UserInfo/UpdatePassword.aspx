<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePassword.aspx.cs" Inherits="mston.AdminManager.UserInfo.UpdatePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link  href="../css/comon.css" rel="Stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
       <div class="contenter">
    <div class="content">
    
        <div class="fdset" >
      <div class="fdset_title">密码修改</div>
      <div class="fdset_content">
    
    <div class="item"><div class="txt_sideBar1"><span style="color:Red"></span>用户名：</div><div class="txt_content"><asp:Label ID="LabUserName" runat="server"></asp:Label></div> </div>
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>输入密码：</div><div class="txt_content"><asp:TextBox ID="TxtPwd" runat="server" CssClass="buttoncss" TextMode="Password" MaxLength="100"></asp:TextBox></div> </div>
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>再一次输入密码：</div><div class="txt_content"><asp:TextBox ID="TxtPwd1" runat="server" CssClass="buttoncss" TextMode="Password" MaxLength="100"></asp:TextBox></div> </div>
    <div class="item"><div class="txt_sideBar1"></div><div>
    <asp:Button ID="BtnAdd" runat="server"   Text="修改" onclick="BtnAdd_Click"  CssClass="buttonClass"  />
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server"   Text="返回" onclick="Button1_Click"   CssClass="buttonClass" />
    </div> </div>
    
      </div>
    </div>
    
    </div>
    </div>
    </form>
</body>
</html>
