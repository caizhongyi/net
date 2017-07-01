<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProvinceManger.aspx.cs" Inherits="AeraManger_ProvinceManger" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" Width="679px" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Province_Id" HeaderText="省份ID" />
                <asp:BoundField DataField="Province_Name" HeaderText="名称" />
                <asp:BoundField DataField="Province_Remark" HeaderText="备注" />
                <asp:CommandField ShowEditButton="True" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    
    </div>
        <asp:Button ID="Button1" runat="server" Text="增加省份" />
        <asp:Panel ID="Panel1" runat="server" Height="162px" Width="681px">
            省份名:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
            备注:&nbsp;
            <asp:TextBox ID="TextBox2" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br />
            <asp:Button ID="Button2" runat="server" Text="增加" />
            <asp:Button ID="Button3" runat="server" Text="取消" /></asp:Panel>
        <asp:Panel ID="Panel2" runat="server" Height="248px" Width="679px">
            省份名:<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br />
            备注:
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label><br />
            <asp:Button ID="Button4" runat="server" Text="确定" />
            <asp:Button ID="Button5" runat="server" Text="取消" /></asp:Panel>
    </form>
</body>
</html>
