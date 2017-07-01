<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserInfo.aspx.cs" Inherits="mston.AdminManger.UserManger.UserInfo" %>

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
    <div >
    
        <asp:Repeater ID="RepUserInfo" runat="server" 
            onitemcommand="RepUserInfo_ItemCommand" 
            onitemdatabound="RepUserInfo_ItemDataBound">
        <HeaderTemplate>
         <br/>
         <table class="UserTableBorder" cellspacing="1" cellpadding="3" width="96%" align="center" border="0">
         <tr>
            <th>用户ID</th>
            <th>用户名称</th>
            <th>用户角色</th>
            <th>用户状态</th>
            <th>最后登陆时间</th>
            <th>密码修改</th>
            <th>编辑</th>
            <th>删除</th>
        </tr>

        </HeaderTemplate>
        <ItemTemplate>
         <tr align="center" onmouseover="mouseover(this)" onmouseout="mouseout(this)"  onclick="RowClick('UserInfoUpdate.aspx?id=<%#Eval("u_id") %>');">
            <td class="UserTableRow2" ><asp:Label ID="LabID" runat="server" Text='<%#Eval("u_id") %>'></asp:Label></td>
            <td class="UserTableRow2" ><asp:Label ID="LabUserName" runat="server" Text='<%#Eval("u_name") %>'></asp:Label></td>
            <td class="UserTableRow2" ><asp:Label ID="LabRolesName" runat="server" Text='<%#Eval("r_name") %>'></asp:Label></td>
            <td class="UserTableRow2" ><asp:Label ID="LabState" runat="server" Text='<%#  Eval("u_state").ToString()=="True"?"启用":"禁用" %>'></asp:Label></td>
            <td class="UserTableRow2" ><asp:Label ID="Label1" runat="server" Text='<%#  Eval("u_loginDate") %>'></asp:Label></td>
            <td class="UserTableRow2" ><a href='UpdatePassword.aspx?id=<%#Eval("u_id") %>'>密码修改</a></td>
            <td class="UserTableRow2" ><a href='UserInfoUpdate.aspx?id=<%#Eval("u_id") %>'>编辑</a></td>
            <td class="UserTableRow2" ><asp:LinkButton ID="linkbtnDel" runat="server" CommandName="Del">删除</asp:LinkButton></td>
        </tr>
        </ItemTemplate>
        <FooterTemplate>
         </table>
        </FooterTemplate>
        </asp:Repeater>
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
    </div>
    </form>
</body>
</html>
