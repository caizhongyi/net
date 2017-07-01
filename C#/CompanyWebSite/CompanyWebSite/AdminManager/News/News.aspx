<%@ Page Language="C#" AutoEventWireup="true" CodeFile="News.aspx.cs" Inherits="mston.AdminManager.News.News" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
    <asp:HiddenField ID="SearchModel" runat="server"  Value="0"/>
    <div>
    <div class="search_content">
    <div class="search_subcontent">
    <ul class="search">
    <li>标题名称:&nbsp;&nbsp;<asp:TextBox ID="TxtTitle" runat="server" Width="138"></asp:TextBox></li>
    <li>添加人:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="TxtAuthor" runat="server" Width="138"></asp:TextBox></li>
    <li>新闻来源:&nbsp;&nbsp;<asp:TextBox ID="TxtSource" runat="server" Width="138"></asp:TextBox></li>
    <li >所属分类:&nbsp;&nbsp;<asp:DropDownList ID="DropBigType" runat="server" Width="138" ></asp:DropDownList></li>
 <%--   <li >所属小类:&nbsp;&nbsp;
    <asp:DropDownList ID="DropSmallType" runat="server"  Width="138">
    <asp:ListItem Value="0">全部</asp:ListItem>
    </asp:DropDownList>
   </li>--%>
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
     </div>
     
     
     <asp:HiddenField ID="n_idOrder" runat="server"  Value="asc"/>
     <asp:HiddenField ID="n_titleOrder" runat="server" Value="asc"/>
     <asp:HiddenField ID="t_nameOrder" runat="server" Value="asc"/>
     <asp:HiddenField ID="smallType_nameOrder" runat="server" Value="asc"/>
     <asp:HiddenField ID="n_sourceOrder" runat="server" Value="asc"/>
     <asp:HiddenField ID="n_authorOrder" runat="server" Value="asc"/>
      <asp:HiddenField ID="n_createDateOrder" runat="server" Value="asc"/>
      <asp:HiddenField ID="curHidden" runat="server" Value=""/>
     <asp:Repeater ID="RepNews" runat="server" onitemcommand="RepNews_ItemCommand"  onitemdatabound="RepNews_ItemDataBound" >
        <HeaderTemplate>
         <br/>
         <table class="UserTableBorder" cellspacing="1" cellpadding="3" width="100%" align="center" border="0">
         <tr>
            <th> <asp:LinkButton ID="LinkButton1" runat="server" CommandName="order" CommandArgument="n_id" >新闻ID</asp:LinkButton></th>
            <th> <asp:LinkButton ID="LinkButton2" runat="server" CommandName="order" CommandArgument="n_title"  >新闻标题</asp:LinkButton></th>
            <th> <asp:LinkButton ID="LinkButton3" runat="server" CommandName="order" CommandArgument="t_name"  >所属类别</asp:LinkButton></th>
            <th> <asp:LinkButton ID="LinkButton5" runat="server" CommandName="order" CommandArgument="n_source"  >新闻来源</asp:LinkButton></th>
            <th> <asp:LinkButton ID="LinkButton6" runat="server" CommandName="order" CommandArgument="n_author"  >添加人</asp:LinkButton></th>
            <th><asp:LinkButton ID="LinkButton7" runat="server" CommandName="order" CommandArgument="n_createDate"  >时间/日期</asp:LinkButton></th>
            <th> 显示状态</th>
            <th> 编辑</th>
            <th> 删除</th>
        </tr>

        </HeaderTemplate>
        <ItemTemplate>
         <tr align="center" onmouseover="mouseover(this)" onmouseout="mouseout(this)"  onclick="RowClick('NewsUpdate.aspx?id=<%#Eval("n_id") %>');">
          
            <td class="UserTableRow2" ><asp:Label ID="LabID" runat="server" Text='<%#Eval("n_id") %>'></asp:Label></td>
            <td class="UserTableRow2" ><asp:Label ID="LabTitle" runat="server" Text='<%#Eval("n_title") %>'></asp:Label></td>
            <td class="UserTableRow2" ><asp:Label ID="LabName" runat="server" Text='<%#Eval("t_name") %>'></asp:Label></td>
            <td class="UserTableRow2" ><asp:Label ID="LabSmallType" runat="server" Text='<%#  Eval("n_source") %>'></asp:Label></td>
            <td class="UserTableRow2" ><asp:Label ID="LabAuthor" runat="server" Text='<%#  Eval("n_author") %>'></asp:Label></td>
            <td class="UserTableRow2" ><asp:Label ID="LabCreateDate" runat="server" Text='<%#  Eval("n_createDate") %>'></asp:Label></td>
            <td class="UserTableRow2" ><asp:LinkButton ID="LinkBtnIsShow" runat="server" CommandName="Show" CommandArgument='<%#Eval("n_isShow") %>' Text='<%#  Eval("n_isShow").ToString()=="True"?"显示":"隐藏" %>'></asp:LinkButton></td>
            <td class="UserTableRow2" ><a href='NewsUpdate.aspx?id=<%#Eval("n_id") %>'>编辑</a></td>
            <td class="UserTableRow2" ><asp:LinkButton ID="linkbtnDel" runat="server" CommandName="Del">删除</asp:LinkButton></td>
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
    </div>
    </form>
</body>
</html>
