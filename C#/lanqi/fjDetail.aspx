<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="fjDetail.aspx.cs" Inherits="fjDetail"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link href="css/b.css" rel="stylesheet" type="text/css" />

  <div id="center" >
    <div class="center_up">
      <ul>
    <asp:Repeater ID="Repeater1" runat="server">
          <ItemTemplate>
           <li><a href="newList.aspx?id=<%# Eval("id") %>"><%# Eval("type") %></a></li>
          </ItemTemplate>
          </asp:Repeater>
      </ul>
    </div>
    <div class="center_in"  >
      
     
   <!-- 宗教百典 -->
   <div class="news"   runat="server" id="divNews" style="background-color: #eee890;">
    <div class="neirong" style="text-align:left; font-size:13px" > 
   
     
        <asp:Repeater ID="Repeater2" runat="server">
        <ItemTemplate>
    <strong><p class="p_1_1" style="font-size:15px; color:#008080"><%# Eval("title") %> </p></strong>
  <%--  <p class="p_1_1">点击数：<%# Eval("hot") %></p>--%>
  
     <p class="p_1_2"><%# Eval("content") %></p></div>
        </ItemTemplate>
     </asp:Repeater>
    
  
     </div>
 
   </div>
   
   
   
   
   <!-- 宗教百典 -->

   
   
   
   
   
    </div>
  </div>
  <div class="clr"></div>
  
  
  
  
  <div class="clr"></div>

<script type="text/javascript" src="js/jquery-1.4.3.min.js"></script>
<script type="text/javascript">
    //$(document).ready(function () {
    $(".neirong a").attr("target", "_blank");
   
    //  });
   
</script>

</asp:Content>

