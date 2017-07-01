<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewsType.aspx.cs" Inherits="mston.AdminManager.Type.Type" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/main.css" type="text/css" rel="stylesheet"/>
    <link href="../css/AspNetPager.css" type="text/css" rel="stylesheet"/>
    <script type="text/javascript" src="../js/Effect.js"></script>
     <style type="text/css">
     .search{   list-style-type:none; margin:0px; margin-bottom:10px; clear:both;}
     .search li{ float:left;  margin-right:80px; margin-bottom:10px;}
     .search_content{  background-color:#edf0f6; border:solid 1px #9db3e6; margin:18px; overflow:hidden; height:70px; width:960px;}
     .search_subcontent{ margin:10px;}
     .type_title{margin-top:10px; margin-bottom:5px;margin-left:18px;}
     input{ }
     .searchBtn{cursor: pointer; margin-left:20px; width:34px; height:20px;   border-width:0px;  background-image:url(../images/search.gif); background-repeat:no-repeat;}
     </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
   

     <asp:Repeater ID="RepBigType" runat="server" onitemcommand="RepBigType_ItemCommand" 
                onitemdatabound="RepBigType_ItemDataBound"   >
        <HeaderTemplate>
        
       
         <table class="UserTableBorder" cellspacing="1" cellpadding="3" width="96%" align="center" border="0">
         <tr>
            <th>ID</th>
            <th>类别名称</th>
            <th>链接地址</th>
            <th>父级节点ID</th>
            <th>排列顺序</th>
           <%-- <th>添加子类别</th>--%>
            <th>编辑</th>
            <th>删除</th>
        </tr>

        </HeaderTemplate>
        <ItemTemplate>
         <tr align="center" onmouseover="mouseover(this)" onmouseout="mouseout(this)"  onclick="RowClick('NewsTypeUpdate.aspx?type=big&id=<%#Eval("t_id") %>');">
            <td class="UserTableRow2" ><asp:Label ID="LabID" runat="server" Text='<%#Eval("t_id") %>'></asp:Label></td>
            <td class="UserTableRow2" ><asp:Label ID="LabRolesName" runat="server" Text='<%#Eval("t_name") %>'></asp:Label></td>
            <td class="UserTableRow2" ><asp:Label ID="Label2" runat="server" Text='<%#Eval("t_url") %>'></asp:Label></td>
            <td class="UserTableRow2" ><asp:Label ID="Label1" runat="server" Text='<%#Eval("t_parentid") %>'></asp:Label></td>
             <td class="UserTableRow2" ><asp:Label ID="Label3" runat="server" Text='<%#Eval("t_order") %>'></asp:Label></td>
           <%-- <td class="UserTableRow2" ><asp:Literal ID="Literal1" runat="server"></asp:Literal></td>--%>
            <td class="UserTableRow2" ><a href='NewsTypeUpdate.aspx?type=big&id=<%#Eval("t_id") %>'>编辑</a></td>
            <td class="UserTableRow2" >
     
            <asp:LinkButton ID="linkbtnDel" runat="server" CommandName="Del">删除</asp:LinkButton></td>
       
        </tr>
        </ItemTemplate>
        <FooterTemplate>
         </table>
        </FooterTemplate>
        </asp:Repeater>
      <div class="footer"><asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder></div>  
    </div>
    
      <div>
      <%-- <div class="type_title"><img alt="" src="../images/smallType.png" /></div>
    
   <div class="search_content">
        
    <div class="search_subcontent">
    <ul class="search">
    <li>大类别名称:&nbsp;&nbsp;<asp:DropDownList ID="DropBigType"  runat="server"   Width="104px"></asp:DropDownList>
     <asp:Button ID="BtnSearch" runat="server"    CssClass="searchBtn"  onclick="BtnSearch_Click"   />
     </li>
    </ul>
     </div>
     </div>--%>
     
  <%--   <asp:Repeater ID="RepSmallType" runat="server" 
                onitemcommand="RepSmallType_ItemCommand" 
                onitemdatabound="RepSmallType_ItemDataBound"  >
        <HeaderTemplate>
         <table class="UserTableBorder" cellspacing="1" cellpadding="3" width="96%" align="center" border="0">
         <tr>
            <th>ID</th>
            <th>所属大类别</th>
            <th>类别名称</th>
            <th>添加时间</th>
            <th>编辑</th>
            <th>删除</th>
        </tr>

        </HeaderTemplate>
        <ItemTemplate>
         <tr align="center" onmouseover="mouseover(this)" onmouseout="mouseout(this)"  onclick="RowClick('NewsTypeUpdate.aspx?type=small&id=<%#Eval("smallType_id") %>','_self');">
            <td class="UserTableRow2" ><asp:Label ID="LabID" runat="server" Text='<%#Eval("smallType_id") %>'></asp:Label></td>
           <td class="UserTableRow2" ><asp:Label ID="Label1" runat="server" Text='<%#Eval("bigType_name") %>'></asp:Label></td>
            <td class="UserTableRow2" ><asp:Label ID="LabRolesName" runat="server" Text='<%#Eval("smallType_name") %>'></asp:Label></td>
            <td class="UserTableRow2" ><asp:Label ID="LabDateTime" runat="server" Text='<%#Eval("smallType_createDate") %>'></asp:Label></td>
            <td class="UserTableRow2" ><a href='NewsTypeUpdate.aspx?type=small&id=<%#Eval("smallType_id") %>'>编辑</a></td>
            <td class="UserTableRow2" ><asp:LinkButton ID="linkbtnDel" runat="server" CommandName="Del">删除</asp:LinkButton></td>
        </tr>
        </ItemTemplate>
        <FooterTemplate>
         </table>
        </FooterTemplate>
        </asp:Repeater>
         <div class="footer"><asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder></div>  --%>
    </div>
    </form>
</body>
</html>
