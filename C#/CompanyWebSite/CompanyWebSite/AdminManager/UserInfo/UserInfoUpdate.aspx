<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserInfoUpdate.aspx.cs" Inherits="mston.AdminManager.UserInfo.UserInfoUpdate" %>

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
      
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">*</span>用户名：</div><div class="txt_content"><asp:TextBox ID="TxtUserName" runat="server" CssClass="buttoncss" MaxLength="100"></asp:TextBox></div> </div>
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">*</span>是否启用：</div><div class="txt_content">
        <asp:RadioButton ID="RadioUse" runat="server"  GroupName="state" Text="启用"/><asp:RadioButton ID="RadioUnUse" runat="server"  GroupName="state" Text="禁用"/></div> </div>
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">*</span>角色：</div><div class="txt_content"><asp:DropDownList ID="DropRoles" runat="server"></asp:DropDownList></div> </div>
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">*</span>密码：</div><div class="txt_content">
                            <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">密码修改</asp:LinkButton></div> </div>
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
