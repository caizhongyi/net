<%@ Page Language="C#" AutoEventWireup="true"  EnableEventValidation="false"   CodeFile="RolesManger.aspx.cs" Inherits="UserManger_RolesManger" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" DataKeyNames="user_type_id">
            <Columns>
                <asp:BoundField DataField="user_type_id" HeaderText="用户身份ID" ReadOnly="True" />
                <asp:BoundField DataField="user_type_name" HeaderText="用户身份名称" />
                <asp:BoundField DataField="user_type_remark" HeaderText="备注" />
                <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
        <asp:Button ID="Button1" runat="server" Text="用户类别增加" OnClick="Button1_Click" /><br />
    
    </div>
        <asp:Panel ID="Panel1" runat="server" Height="155px" Width="822px">
            用户身份名称:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
            备注: &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine"></asp:TextBox>&nbsp;<br />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="增加" />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="取消" /></asp:Panel>
        <asp:Panel ID="Panel2" runat="server" Height="164px" Width="819px">
            用户身份名称:<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br />
            备注: &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            <br />
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="确定" />
            <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="取消 " /><br />
        </asp:Panel>
    </form>
</body>
</html>
