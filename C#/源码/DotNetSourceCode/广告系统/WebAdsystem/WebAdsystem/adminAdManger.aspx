<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdminAdManger.aspx.cs" Inherits="adminAdManger" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="669px" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
        <Columns>
            <asp:BoundField DataField="ad_ID" HeaderText="广告ID" />
            <asp:BoundField DataField="ad_name" HeaderText="广告名称" />
            <asp:BoundField DataField="ad_url" DataFormatString="&lt;img height=40 width=40 src=Adpictures\{0}&gt;"
                HeaderText="广告图片" />
            <asp:BoundField DataField="ad_TypeName" HeaderText="广告类型" />
            <asp:BoundField DataField="ad_opearation" HeaderText="广告发布" />
            <asp:BoundField DataField="ad_clickNumber" HeaderText="点击次数" />
            <asp:BoundField DataField="ad_time" HeaderText="修改时间" />
            <asp:BoundField DataField="ad_remark" HeaderText="备注" />
            <asp:CommandField ButtonType="Button" SelectText="发布" ShowSelectButton="True" />
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
    <asp:Panel ID="Panel1" runat="server" Height="85px" Width="660px">
        <br />
        <asp:Label ID="Label3" runat="server" Text="广告名称:"></asp:Label>
        <asp:Label ID="ad_name" runat="server" Text="Null"></asp:Label><br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="广告类型:"></asp:Label>
        <asp:Label ID="ad_type" runat="server" Text="Null"></asp:Label><br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="发布的网吧名称:"></asp:Label>
        &nbsp;&nbsp;
        <asp:DropDownList ID="wb_name" runat="server">
        </asp:DropDownList><br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="发布的网吧分机ID:"></asp:Label>
        &nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList1" runat="server">
        </asp:DropDownList>
        -<asp:DropDownList ID="DropDownList2" runat="server">
        </asp:DropDownList><br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="发布" OnClick="Button1_Click" />
        &nbsp; &nbsp;<asp:Button ID="Button2" runat="server" Text="取消" OnClick="Button2_Click" /><br />
    </asp:Panel>
</asp:Content>

