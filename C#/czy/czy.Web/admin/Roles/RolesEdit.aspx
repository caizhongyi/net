<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RolesEdit.aspx.cs" Inherits="mston.AdminManager.Roles.RolesEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link  href="../css/comon.css" rel="Stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div class="contenter">
    <div class="content">
    
       <div class="fdset" >
      <div class="fdset_title">角色添加</div>
      <div class="fdset_content">
    
    
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">*</span>角色名称：</div><div class="txt_content"><asp:TextBox ID="TxtRolesName" runat="server" CssClass="buttoncss" MaxLength="100"></asp:TextBox></div> </div>
   <div class="item"><div class="txt_sideBar1"><span style="color:Red">*</span>权限值：</div><div class="txt_content">
                            <asp:TextBox ID="TxtRightValue" runat="server" CssClass="buttoncss" 
                                MaxLength="100" Width="29px">1</asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:Red">(注:权限0为最大权限!)</span></div> </div>
    <div class="item">
    <div class="btn_sideBar">
    <asp:Button ID="BtnAdd" runat="server"   Text="添加" onclick="BtnAdd_Click"  CssClass="buttonClass" />
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
