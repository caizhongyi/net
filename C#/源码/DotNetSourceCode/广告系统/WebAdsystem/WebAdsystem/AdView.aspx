<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AdView.aspx.cs" Inherits="AdView" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    &nbsp;

    <asp:Label ID="Label4" runat="server" Text="发布的广告信息:"></asp:Label>&nbsp;<br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="669px">
        <Columns>
            <asp:BoundField DataField="wb_name" HeaderText="网吧名称" />
            <asp:BoundField DataField="wb_ip" HeaderText="网吧IP" />
            <asp:BoundField DataField="wb_area" HeaderText="网吧地址" />
            <asp:BoundField DataField="wb_tel" HeaderText="网吧电话" />
            <asp:BoundField DataField="wb_time" HeaderText="修改时间" />
            <asp:BoundField DataField="wb_remark" HeaderText="备注" />
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:Label ID="Label1" runat="server" Text="发布的广告名称:"></asp:Label>&nbsp;<asp:DropDownList ID="DropDownList1" runat="server">
    </asp:DropDownList><br />
    <br />
    <asp:Label ID="Label7" runat="server" Text="所属类别:"></asp:Label>&nbsp;&nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp;
    <asp:Label ID="Label8" runat="server" Text="Null"></asp:Label><br />
    <br />
    <asp:Label ID="Label5" runat="server" Text="图片预览:"></asp:Label>&nbsp;<br />
    &nbsp; &nbsp; &nbsp;&nbsp;<br />
    <asp:Image ID="Image1" runat="server" /><br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="发布的网吧名称:"></asp:Label>&nbsp;<asp:DropDownList ID="DropDownList2" runat="server">
    </asp:DropDownList>
    <br />
    <br />
    <asp:Label ID="Label3" runat="server" Text="网吧ip:"></asp:Label>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
    <asp:Label ID="Label6" runat="server" Text="0.0.0.0"></asp:Label><br />
    <br />
    <asp:Button ID="Button2" runat="server" Text="发布" />
    &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:Button ID="Button1" runat="server" Text="取消" /><br />
</asp:Content>

