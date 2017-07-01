<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WbAdManger.aspx.cs" Inherits="WbAdManger" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <asp:Label ID="Label7" runat="server" Text="广告查询"></asp:Label><br />
        <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="669px" CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="Ad_ID" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
        <Columns>
            <asp:BoundField DataField="ad_ID" HeaderText="广告ID" ReadOnly="True" />
            <asp:BoundField DataField="ad_Type_Name" HeaderText="广告类型" />
            <asp:BoundField DataField="ad_name" HeaderText="广告名称" />
            <asp:BoundField DataField="ad_url" DataFormatString="&lt;img height=40 width=40 src=Adpictures\{0}&gt;" HeaderText="广告图片" />
            <asp:BoundField DataField="Ad_ClickNum" HeaderText="点击次数" />
            <asp:BoundField DataField="Client_Name" HeaderText="客户名称" />
            <asp:BoundField DataField="ad_time" HeaderText="修改时间" />
            <asp:BoundField DataField="ad_remark" HeaderText="备注" />
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
    
 
        
</asp:Content>

