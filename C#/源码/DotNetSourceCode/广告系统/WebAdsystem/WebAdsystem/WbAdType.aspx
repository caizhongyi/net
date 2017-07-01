<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WbAdType.aspx.cs" Inherits="WbAdType" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel2" runat="server" Height="50px" Width="125px">
    <asp:Label ID="Label2" runat="server" Text="广告类型:"></asp:Label><br />
        <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="669px" CellPadding="4" ForeColor="#333333" GridLines="None">
        <Columns>
            <asp:BoundField DataField="ad_typeID" HeaderText="广告类型ID" />
            <asp:BoundField DataField="ad_typename" HeaderText="广告类型名称" />
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
    </asp:Panel>
    <br />
    <br />
    <asp:Panel ID="Panel1" runat="server" Height="69px" Width="666px">
        &nbsp;<asp:Label ID="Label1" runat="server" Text="广告类型"></asp:Label>
        <asp:TextBox ID="AdType" runat="server"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
            ID="RequiredFieldValidator8" runat="server" ControlToValidate="AdType" ErrorMessage="*"></asp:RequiredFieldValidator><br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="增加" />
        &nbsp;
        <asp:Button ID="Button2" runat="server" Text="取消" /></asp:Panel>
       
</asp:Content>

