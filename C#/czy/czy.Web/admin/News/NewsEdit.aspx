<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsEdit.aspx.cs" Inherits="admin_Menu_MenuEdit" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2"  %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>   
    <link  href="../css/comon.css" rel="Stylesheet"/>
    <link rel="stylesheet" type="text/css" media="screen" href="jquery-validate/css/screen.css" />
    <script src="jquery-validate/lib/jquery.js" type="text/javascript"></script>
    <script src="jquery-validate/jquery.validate.js" type="text/javascript"></script>
    <script src="jquery-validate/localization/messages_cn.js" type="text/javascript"></script>
    <style  type="text/css">
    .c{ font-size:10px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <%--<div class="new_content" >--%>
      <div class="fdset" >
      <div class="fdset_title">导航</div>
      <div class="fdset_content">
      
      <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>标题：</div>
      <div class="txt_content"><asp:TextBox ID="TxtName" runat="server" 
              CssClass="buttoncss" MaxLength="100" Width="250px"></asp:TextBox></div> </div>
                  <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>类别：</div><div class="txt_content"><asp:DropDownList ID="DropType" runat="server" Width="150" ></asp:DropDownList></div> </div>
   <div class="item"><div class="txt_sideBar1"><span style="color:Red">&nbsp;</span>内容：</div><div class="txt_content">  
     <FCKeditorV2:FCKeditor  EditorAreaCSS="c" ID="FCKeditor1" runat="server" Height="300" Width="750" SkinPath="skins/office2003/"></FCKeditorV2:FCKeditor></div> </div>


    <div class="item">
    <div  class="btn_sideBar">
    <asp:Button ID="BtnAdd" runat="server"   Text="添加" onclick="BtnAdd_Click"  CssClass="buttonClass" />
    &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;  &nbsp; &nbsp; &nbsp; &nbsp;
     <asp:Button ID="Button1" runat="server"   Text="返回" onclick="Button1_Click" CssClass="buttonClass"   />
    </div> 
    
    </div>
      
      </div>
      </div>
    
   
    <%--
    </div>--%>
    </form>
</body>
</html>
