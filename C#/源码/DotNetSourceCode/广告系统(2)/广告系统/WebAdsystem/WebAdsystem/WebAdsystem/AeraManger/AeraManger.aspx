<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AeraManger.aspx.cs" Inherits="AeraManger_AeraManger" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" Width="669px" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="Area_Id" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" PageSize="5">
            <Columns>
                <asp:BoundField DataField="Area_Id" HeaderText="地区ID" />
                <asp:BoundField DataField="Area_Name" HeaderText="地区名" />
                <asp:BoundField DataField="Province_Id" HeaderText="省份ID" />
                <asp:BoundField DataField="Area_Remark" HeaderText="备注" />
                <asp:CommandField ShowEditButton="True" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    
    </div>
        <asp:Button ID="Button1" runat="server" Text="增加地区" OnClick="Button1_Click" />
        <asp:Panel ID="Panel1" runat="server" Height="248px" Width="667px">
            地区名:<asp:TextBox ID="area_name" runat="server"></asp:TextBox><br />
            省份:&nbsp;
            <asp:DropDownList ID="DrProvince" runat="server">
            </asp:DropDownList><br />
            备注:&nbsp;
            <asp:TextBox ID="area_remark" runat="server" TextMode="MultiLine"></asp:TextBox><br />
            &nbsp;<asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="增加" />
            <asp:Button ID="Button2" runat="server" Text="取消" /></asp:Panel>
        <asp:Panel ID="Panel2" runat="server" Height="295px" Width="665px">
            地区名:<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br />
            省份:
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label><br />
            备注:
            <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            <br />
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="确定" />
            <asp:Button ID="Button5" runat="server" Text="取消" /></asp:Panel>
    </form>
</body>
</html>
