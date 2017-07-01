<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Roles.aspx.cs" Inherits="mston.AdminManager.Roles.Roles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../css/main.css" type="text/css" rel="stylesheet"/>
    <link href="../css/AspNetPager.css" type="text/css" rel="stylesheet"/>
   <script type="text/javascript" src="../js/Effect.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:Repeater ID="RepRoles" runat="server" onitemcommand="RepRoles_ItemCommand" 
            onitemdatabound="RepRoles_ItemDataBound"  >
        <HeaderTemplate>
         <br/>
         <table class="UserTableBorder" cellspacing="1" cellpadding="3" width="96%" align="center" border="0">
         <tr>
            <th>角色ID</th>
            <th>角色名称</th>
            <th>权限值</th>
            <th>编辑</th>
            <th>删除</th>
        </tr>

        </HeaderTemplate>
        <ItemTemplate>
         <tr align="center" onmouseover="mouseover(this)" onmouseout="mouseout(this)"  onclick="RowClick('RolesUpdate.aspx?id=<%#Eval("r_id") %>');">
            <td class="UserTableRow2" ><asp:Label ID="LabID" runat="server" Text='<%#Eval("r_id") %>'></asp:Label></td>
            <td class="UserTableRow2" ><asp:Label ID="LabRolesName" runat="server" Text='<%#Eval("r_name") %>'></asp:Label></td>
            <td class="UserTableRow2" ><asp:Label ID="LabRolesRight" runat="server" Text='<%#Eval("r_right") %>'></asp:Label></td>
            <td class="UserTableRow2" ><a href='RolesUpdate.aspx?id=<%#Eval("r_id") %>'>编辑</a></td>
            <td class="UserTableRow2" ><asp:LinkButton ID="linkbtnDel" runat="server" CommandName="Del">删除</asp:LinkButton></td>
        </tr>
        </ItemTemplate>
        <FooterTemplate>
         </table>
        </FooterTemplate>
        </asp:Repeater>
        
    <div class="footer"><asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder></div> 
    </div>
    </form>
</body>
</html>
