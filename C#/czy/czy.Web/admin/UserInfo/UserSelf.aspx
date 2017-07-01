<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserSelf.aspx.cs" Inherits="mston.AdminManager.UserInfo.UserSelf" %>

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
      <div class="fdset_title">用户修改</div>
      <div class="fdset_content">
      
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">*</span>用户名：</div><div class="txt_content"><asp:TextBox ID="TxtUserName" runat="server" CssClass="buttoncss" MaxLength="100" Enabled="false"></asp:TextBox></div> </div>
    <div class="item"><div class="txt_sideBar1"><span style="color:Red"></span>启用状态：</div><div class="txt_content"> <asp:Label ID="LabState" runat="server" Text="Label"></asp:Label></div> </div>
    <div class="item"><div class="txt_sideBar1"><span style="color:Red"></span>角色：</div><div class="txt_content"><asp:Label ID="LabRoles" runat="server" Text="Label"></asp:Label></div> </div>
     <div class="item"><div class="txt_sideBar1"><span style="color:Red"></span>密码：</div><div class="txt_content"> 
         <asp:Button ID="Button2" runat="server" Text="点击修改密码"  CssClass="buttonClass" 
                                 onclick="Button2_Click" /> </div> </div>
    <div class="item"><div class="btn_sideBar">
    <asp:Button ID="BtnUpdate" runat="server"   Text="修改" onclick="BtnAdd_Click1" CssClass="buttonClass"  />
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server"   Text="返回" 
            onclick="Button1_Click" CssClass="buttonClass"   />
    <div>
    </div> </div>
    </div>
    
    
      <div>
    </div>
    
    </div>
    </form>
</body>
</html>
