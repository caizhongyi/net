<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WbclientManger.aspx.cs" Inherits="WbclientManger" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label6" runat="server" Text="客户信息:"></asp:Label><br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="669px" CellPadding="4" ForeColor="#333333" GridLines="None">
        <Columns>
            <asp:BoundField DataField="client_ID" HeaderText="客户ID" />
            <asp:BoundField DataField="client_name" HeaderText="客户名称" />
            <asp:BoundField DataField="client_sex" HeaderText="客户性别" />
            <asp:BoundField DataField="client_tel" HeaderText="客户电话" />
            <asp:BoundField DataField="client_address" HeaderText="客户地址" />
            <asp:BoundField DataField="client_time" HeaderText="修改时间" />
            <asp:BoundField DataField="client_remark" HeaderText="备注" />
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <EditRowStyle BackColor="#999999" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    <br />
    <br />
    <br />
    <asp:Panel ID="Panel1" runat="server" Height="318px" Width="573px">
        <asp:Label ID="Label1" runat="server" Text="客户姓名:"></asp:Label>
        &nbsp;&nbsp; &nbsp;<asp:TextBox ID="ClientName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ClientName"
            ErrorMessage="*"></asp:RequiredFieldValidator><br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="客户性别:"></asp:Label>&nbsp;&nbsp;&nbsp;
        &nbsp;<asp:DropDownList ID="ClientSex" runat="server">
            <asp:ListItem>男</asp:ListItem>
            <asp:ListItem>女</asp:ListItem>
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ClientSex"
            ErrorMessage="*"></asp:RequiredFieldValidator><br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="客户联系电话:"></asp:Label>
        <asp:TextBox ID="ClientTel" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="客户联系地址:"></asp:Label>
        <asp:TextBox ID="ClientAddress"
            runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="备注:"></asp:Label><br />
        <br />
        <asp:TextBox ID="clientRemark" runat="server" Height="143px" Width="277px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="增加" />
        <asp:Button ID="Button2" runat="server" Text="取消" /></asp:Panel>
</asp:Content>

