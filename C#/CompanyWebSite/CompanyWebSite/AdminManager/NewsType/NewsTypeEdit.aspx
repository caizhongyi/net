<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsTypeEdit.aspx.cs" Inherits="mston.AdminManager.NewsType.NewsTypeEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link  href="../css/comon.css" rel="Stylesheet"/>
    <script type="text/javascript">
    function ChangeType()
    {
       var bigTypeObj=document.getElementById ("bigType");
       var smallTypeObj=document.getElementById ("smallType");
       
       var rBigTypeObj=document.getElementById ("RadioBigType");
        var rsmallTypeObj=document.getElementById ("RadioBigType");
       
       if(rBigTypeObj.checked)
       {
           bigTypeObj.style.display="block";
           smallTypeObj.style .display ="none";
       }
       else
       {
           bigTypeObj.style.display="none";
           smallTypeObj.style .display ="block";
       }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server" >
    <center> 


    <div class="contenter">
  

       <div class="content">
       
       <div class="fdset" >
      <div class="fdset_title">类别添加</div>
      <div class="fdset_content"> 
       
   <%-- <div class="item"><div class="txt_sideBar1"><span style="color:Red"> </span>类别选择：</div>
    <div>
      
      <asp:RadioButton ID="RadioBigType" runat="server" Text="大类别"  
            GroupName ="newsType" Checked="True"/>
     
                &nbsp; &nbsp;
        <asp:RadioButton ID="RadioSmallType" runat="server"   GroupName ="newsType"   
            Text="小类别"/>
           
    </div>
    <hr />
     </div>--%>
    
        
        <div id="bigType" runat="server">
       
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>类别名称：</div><div class="txt_content"><asp:TextBox ID="TxtBigType" runat="server" CssClass="buttoncss" MaxLength="100"></asp:TextBox></div> </div>
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>父节点：</div><div class="txt_content"><asp:DropDownList ID="list_paranType" runat="server" Width="150"></asp:DropDownList>  </div></div>
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>页面链接地址：</div><div class="txt_content"><asp:TextBox ID="TxtBigTypeUrl" runat="server" CssClass="buttoncss" MaxLength="100"></asp:TextBox></div> </div>
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>排序：</div><div class="txt_content"><asp:TextBox ID="TxtOrder" runat="server" CssClass="buttoncss" MaxLength="100"></asp:TextBox></div> </div>
    <div class="item"><div class="btn_sideBar">
    <asp:Button ID="BtnAdd" runat="server"   Text="添加" onclick="BtnAdd_Click" CssClass="buttonClass"  />
     
     &nbsp; &nbsp; &nbsp; &nbsp;
     <asp:Button ID="Button3" runat="server"   Text="返回" onclick="Button3_Click"  CssClass="buttonClass"  />
    </div> </div>
    
  </div>
    
    
<%--      <div id="smallType" style=" display:none;" runat="server">
        
         <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>大类别名称：</div><div class="txt_content">
        <asp:DropDownList ID="DropBigType" runat="server"  Width="90"></asp:DropDownList>
    </div> </div>
    <div class="item"><div class="txt_sideBar1"><span style="color:Red">* </span>小类别名称：</div><div class="txt_content"><asp:TextBox ID="TxtSmallType" runat="server" CssClass="buttoncss" MaxLength="100"></asp:TextBox></div> </div>
    <div class="item"><div class="btn_sideBar">
    <asp:Button ID="Button1" runat="server"   Text="添加" onclick="Button1_Click" CssClass="buttonClass"  />
    &nbsp; &nbsp; &nbsp; &nbsp;
     <asp:Button ID="Button2" runat="server"   Text="返回"  OnClick="Button3_Click" CssClass="buttonClass"  />
    </div>
            
    </div>
   
        </div>--%>
        
          
    </div>
    </div>
        
    </div>
  </div>
  <center/> 
    </form>
</body>
</html>
