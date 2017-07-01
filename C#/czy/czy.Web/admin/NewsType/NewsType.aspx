<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsType.aspx.cs" Inherits="admin_NewsType" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
     <link href="../css/main.css" type="text/css" rel="stylesheet"/>
      <link href="../css/AspNetPager.css" type="text/css" rel="stylesheet"/>
     <script type="text/javascript" src="../js/Effect.js"></script>
 
     <style type="text/css">
     .search{   list-style-type:none; margin:0px; margin-bottom:10px; clear:both;  }
     .search li{ float:left;   width:33%; margin-bottom:5px;   margin-top:5px; }
     .search_content{  background-color:#edf0f6; border:solid 1px #9db3e6; margin:18px; overflow:hidden; height:70px; }
     .search_subcontent{ margin:10px;}
     input{ }
     .searchBtn{ cursor: pointer; margin-left:20px; width:34px; height:20px;   border-width:0px;  background-image:url(../images/search.gif); background-repeat:no-repeat;}
     </style>
</head>
<body>
    <form id="form1" runat="server">
   <%-- <asp:HiddenField ID="SearchModel" runat="server"  Value="0"/>
    <div>
    <div class="search_content">
    <div class="search_subcontent">
    <ul class="search">
    <li>标题名称:&nbsp;&nbsp;<asp:TextBox ID="TxtTitle" runat="server" Width="138"></asp:TextBox></li>
    <li>添加人:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="TxtAuthor" runat="server" Width="138"></asp:TextBox></li>
    <li>新闻来源:&nbsp;&nbsp;<asp:TextBox ID="TxtSource" runat="server" Width="138"></asp:TextBox></li>
    <li >所属分类:&nbsp;&nbsp;<asp:DropDownList ID="DropBigType" runat="server" Width="138" ></asp:DropDownList></li>
  <li >所属小类:&nbsp;&nbsp;
    <asp:DropDownList ID="DropSmallType" runat="server"  Width="138">
    <asp:ListItem Value="0">全部</asp:ListItem>
    </asp:DropDownList>
   </li>
    <li >显示状态:&nbsp;&nbsp;
    <asp:DropDownList ID="DropState" runat="server"  Width="138">
    <asp:ListItem Value="0">全部</asp:ListItem>
    <asp:ListItem Value="1">显示</asp:ListItem>
    <asp:ListItem Value="2">隐藏</asp:ListItem>
    </asp:DropDownList>
       <asp:Button ID="BtnSearch" runat="server"  onclick="BtnSearch_Click"  CssClass="searchBtn"  />
 </li>
   
    </ul>

   
     
     </div>
     </div>--%>
     
     
 <%--    <asp:HiddenField ID="n_idOrder" runat="server"  Value="asc"/>
     <asp:HiddenField ID="n_titleOrder" runat="server" Value="asc"/>
     <asp:HiddenField ID="t_nameOrder" runat="server" Value="asc"/>
     <asp:HiddenField ID="smallType_nameOrder" runat="server" Value="asc"/>
     <asp:HiddenField ID="n_sourceOrder" runat="server" Value="asc"/>
     <asp:HiddenField ID="n_authorOrder" runat="server" Value="asc"/>
      <asp:HiddenField ID="n_createDateOrder" runat="server" Value="asc"/>
      <asp:HiddenField ID="curHidden" runat="server" Value=""/>--%>
     <asp:Repeater ID="Menu" runat="server" onitemcommand="Menu_ItemCommand" 
        onitemdatabound="Menu_ItemDataBound"  >
        <HeaderTemplate>
         <br/>
         <table class="UserTableBorder" cellspacing="1" cellpadding="3" width="100%" align="center" border="0">
         <tr>
            <th> <asp:LinkButton ID="LinkButton1" runat="server" CommandName="order" CommandArgument="nt_id" >ID</asp:LinkButton></th>
            <th> <asp:LinkButton ID="LinkButton2" runat="server" CommandName="order" CommandArgument="nt_name"  >名称</asp:LinkButton></th>
            <th> <asp:LinkButton ID="LinkButton3" runat="server" CommandName="order" CommandArgument="nt_remark"  >备注</asp:LinkButton></th>
            <th> <asp:LinkButton ID="LinkButton5" runat="server" CommandName="order" CommandArgument="nt_parentId"  >父级ID</asp:LinkButton></th>
    
            <th> 编辑</th>
            <th> 删除</th>
        </tr>

        </HeaderTemplate>
        <ItemTemplate>
         <tr align="center" onmouseover="mouseover(this)" onmouseout="mouseout(this)"  onclick="RowClick('NewsTypeEdit.aspx?id=<%#Eval("nt_id") %>');">
          
            <td class="UserTableRow2 ellipsis" style="width:15%"><asp:Label ID="LabID" runat="server" Text='<%#Eval("nt_id") %>'></asp:Label></td>
            <td class="UserTableRow2 ellipsis" style="width:15%"><asp:Label ID="LabTitle" runat="server" Text='<%#Eval("nt_name") %>'></asp:Label></td>
            <td class="UserTableRow2 ellipsis" style="width:15%"><asp:Label ID="LabName" runat="server" Text='<%#Eval("nt_remark") %>'></asp:Label></td>
            <td class="UserTableRow2 ellipsis" style="width:15%"><asp:Label ID="LabSmallType" runat="server" Text='<%#  Eval("nt_parentId") %>'></asp:Label></td>
            <td class="UserTableRow2" style="width:20%"><a href='NewsTypeEdit.aspx?id=<%#Eval("nt_id") %>'>编辑</a></td>
            <td class="UserTableRow2" style="width:20%"><asp:LinkButton ID="linkbtnDel" runat="server" CommandName="Del" CommandArgument='<%#Eval("nt_id") %>'>删除</asp:LinkButton></td>
        </tr>
        </ItemTemplate>
        <FooterTemplate>
         </table>
        </FooterTemplate>
        </asp:Repeater>
        <div class="footer">
            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" FirstPageText="首页" LastPageText="尾页"
            NextPageText="下一页" PrevPageText="上一页" ShowInputBox="Never" OnPageChanging="AspNetPager1_PageChanging" PageSize="20">
        </webdiyer:AspNetPager>
          
        
        </div>
    <%--</div>--%>
    </form>
</body>
</html>
