<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserInfoEdit.aspx.cs" Inherits="admin_UserInfo_UserInfoEdit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
      
      <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>名称：</div>
      <div class="txt_content"><asp:TextBox ID="TxtName" runat="server" 
              CssClass="buttoncss" MaxLength="100" Width="250px"></asp:TextBox></div> </div>
               

     <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>密码：</div><div class="txt_content">
    <asp:TextBox ID="TxtPassword" runat="server" CssClass="buttoncss" MaxLength="100" TextMode="Password"
             Width="250px" ></asp:TextBox>
     </div> </div>
  
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

