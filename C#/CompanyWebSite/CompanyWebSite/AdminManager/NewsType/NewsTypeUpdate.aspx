<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsTypeUpdate.aspx.cs" Inherits="mston.AdminManager.NewsType.NewsTypeUpdate" %>

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
      <div class="fdset_title">类别修改</div>
      <div class="fdset_content"> 
      <asp:Panel ID="PanelBigType" runat="server">
       
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>大类别名称：</div><div class="txt_content"><asp:TextBox ID="TxtBigType" runat="server" CssClass="buttoncss" MaxLength="100"></asp:TextBox></div> </div>
       <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>父节点：</div><div class="txt_content"><asp:DropDownList ID="list_parantType" runat="server" Width="150"></asp:DropDownList>  </div></div>
     <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>页面链接地址：</div><div class="txt_content"><asp:TextBox ID="TxtBigTypeUrl" runat="server" CssClass="buttoncss" MaxLength="100"></asp:TextBox></div> </div>
         <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>排序：</div><div class="txt_content"><asp:TextBox ID="TxtOrder" runat="server" CssClass="buttoncss" MaxLength="100"></asp:TextBox></div> </div>
     <%-- <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>添加子类别：</div><div class="txt_content"> 
        <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click" >添加子类别</asp:LinkButton></div> </div>--%>
    <div class="item"><div class="btn_sideBar">
    <asp:Button ID="BtnAdd" runat="server"   Text="修改" onclick="BtnAdd_Click" CssClass="buttonClass"  />
     &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button2" runat="server"   Text="返回" onclick="Button2_Click"   CssClass="buttonClass" />
    </div>
     </div>
    
        </asp:Panel>
     
    
   <%--   <asp:Panel ID="PanelSmallType" runat="server" >
       
         <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>大类别名称：</div><div class="txt_content">
        <asp:DropDownList ID="DropBigType" runat="server"  Width="90"></asp:DropDownList>
    </div> </div>
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>小类别名称：</div><div class="txt_content"><asp:TextBox ID="TxtSmallType" runat="server" CssClass="buttoncss" MaxLength="100"></asp:TextBox></div> </div>
    <div class="item" ><div  class="btn_sideBar"> 
    <asp:Button ID="Button1" runat="server"   Text="修改" onclick="Button1_Click" CssClass="buttonClass"  />
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="Button3" runat="server"   Text="返回"  onclick="Button2_Click"  CssClass="buttonClass"  />
            </div> </div>
    
        </asp:Panel>--%>
       
       </div>
       </div>
       
       </div>
    </div>
    </form>
</body>
</html>
