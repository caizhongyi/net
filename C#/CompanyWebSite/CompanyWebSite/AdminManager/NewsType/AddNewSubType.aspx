<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddNewSubType.aspx.cs" Inherits="mston.AdminManager.NewsType.AddNewSubType" %>

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
      <div class="fdset_title">子类别添加</div>
      <div class="fdset_content"> 
      
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>大类别名称：</div><div class="txt_content">
        <asp:Label ID="LabBigTypeName" runat="server" Text="Label"></asp:Label><asp:HiddenField ID="hidBigTypeId" runat="server" />
    </div> </div>
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>小类别名称：</div><div class="txt_content"><asp:TextBox ID="TxtSmallType" runat="server" CssClass="buttoncss" MaxLength="100"></asp:TextBox></div> </div>
    <div class="item"><div class="btn_sideBar">
    <asp:Button ID="Button1" runat="server"   Text="添加" onclick="Button1_Click" CssClass="buttonClass"  />
    &nbsp; &nbsp; &nbsp; &nbsp;
     <asp:Button ID="Button2" runat="server"   Text="返回"  OnClick="Button3_Click" CssClass="buttonClass"  />
    </div>
            
    </div>
    </div>
    </div>
    </div>
    </div>
    
    
    </form>
</body>
</html>
