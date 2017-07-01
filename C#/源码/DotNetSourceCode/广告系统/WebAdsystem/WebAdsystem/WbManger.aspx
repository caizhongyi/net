<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WbManger.aspx.cs" Inherits="WbManger" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div style="width:660px;"><asp:Label ID="Label2" runat="server" Text="网吧管理:"></asp:Label><br />
    <br />
   
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="660px" OnRowEditing="GridView1_RowEditing" DataKeyNames="WB_ID" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowUpdating="GridView1_RowUpdating" CellPadding="1" ForeColor="#333333" GridLines="None">
        <Columns>
            <asp:BoundField DataField="wb_id"  HeaderText="网吧ID" ReadOnly="True" />
            <asp:BoundField DataField="wb_name" HeaderText="网吧名称" />
            <asp:BoundField DataField="wb_ip" HeaderText="网吧IP" />
            <asp:BoundField DataField="wb_address" HeaderText="网吧地址" />
            <asp:BoundField DataField="wb_phone" HeaderText="网吧电话" />
            <asp:BoundField DataField="wb_Register_Time" HeaderText="修改时间" >
                <HeaderStyle Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="wb_remark" HeaderText="备注" />
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="20px" Width="80px" Wrap="False" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" Wrap="False" />
        <EditRowStyle BackColor="#999999" Width="660px" Wrap="False" ForeColor="#FFC0C0" Height="20px" HorizontalAlign="Left" VerticalAlign="Middle" />
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
    </asp:GridView>
    </div>
    <br />
</asp:Content>
