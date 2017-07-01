<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="lxs.aspx.cs" Inherits="lxs"  %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link href="css/b.css" rel="stylesheet" type="text/css" />
<script>
//onselectstart="return false" onpaste="return false" oncopy="return false" oncut="return false" oncontextmenu='return false'
document.body.setAttribute("onselectstart","return true");
document.body.setAttribute("onpaste","return true");
document.body.setAttribute("oncopy","return true");
document.body.setAttribute("oncut","return true");
document.body.setAttribute("oncontextmenu","return true");

</script>


  <div id="center" >
    <div class="center_up">
      <ul>
    <asp:Repeater ID="Repeater1" runat="server">
          <ItemTemplate>
           <li><a href="lxs.aspx?id=<%# Eval("id") %>"><%# Eval("type") %></a></li>
          </ItemTemplate>
          </asp:Repeater>
      </ul>
    </div>
    <div class="center_in"  >
   
     
   <!-- 宗教百典 -->
   <div class="news"   runat=server id="divNews" style="background-color: #eee890;">
    <div class="neirong" style="text-align:left; font-size:13px" > 

   
        <asp:Repeater ID="Repeater2" runat="server">
        <ItemTemplate>
    <strong><p class="p_1_1" style="font-size:15px; color:#008080"><%# Eval("name") %> </p></strong>

    
     <p class="p_1_2"><%# Eval("honor_content") %></p></div>
        </ItemTemplate>
     </asp:Repeater>
     
    
       
    
     
  
     </div>
 
   </div>   
   
   
   
   <!-- 宗教百典 -->



</div>
   
   
   
   
   
    </div>
  
  <div class="clr"></div>
  
  
  
  
  <div class="clr"></div>
</asp:Content>

