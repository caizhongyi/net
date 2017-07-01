<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserInfoEdit.aspx.cs" Inherits="mston.AdminManger.UserManger.UserInfoEdit" %>

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
      <div class="fdset_title">用户添加</div>
      <div class="fdset_content">
      
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">*</span>用户名：</div><div class="txt_content"><asp:TextBox ID="TxtUserName" runat="server" CssClass="buttoncss" MaxLength="100"></asp:TextBox></div> </div>
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">*</span>输入密码：</div><div class="txt_content"><asp:TextBox ID="TxtPwd" runat="server" CssClass="buttoncss" TextMode="Password" MaxLength="100"></asp:TextBox></div> </div>
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">*</span>再一次输入密码：</div><div class="txt_content"><asp:TextBox ID="TxtPwd1" runat="server" CssClass="buttoncss" TextMode="Password" MaxLength="100"></asp:TextBox></div> </div>
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">*</span>角色：</div><div class="txt_content">
                    <asp:DropDownList ID="DropRoles" runat="server" Width="93px"></asp:DropDownList></div> </div>
    <div class="item"><div class="btn_sideBar">
    <asp:Button ID="BtnAdd" runat="server"   Text="添加" onclick="BtnAdd_Click1" CssClass="buttonClass"  />
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button1" runat="server"   Text="返回" onclick="Button1_Click"  CssClass="buttonClass"  />
    </div> </div>
     
       </div>
    </div>
     
    </div>
      
    </div>
    </form>
</body>
</html>
