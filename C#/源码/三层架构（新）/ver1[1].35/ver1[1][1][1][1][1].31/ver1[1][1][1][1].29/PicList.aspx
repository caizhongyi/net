<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="PicList.aspx.cs" Inherits="PicList" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPH" Runat="Server">
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MyPictureConnectionString %>"
        SelectCommand="SELECT * FROM [Picture] WHERE ([AlbumsID] = @AlbumsID)">
        <SelectParameters>
            <asp:QueryStringParameter Name="AlbumsID" QueryStringField="id" Type="Int64" />
        </SelectParameters>
    </asp:SqlDataSource>
   <marquee width="400" dir="ltr" scrolldelay="100">
    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
     <ItemTemplate> 
       <a href="?id=<%# Eval("AlbumsID") %>&picId=<%# Eval("ID") %>"><img src="Upload/pic/<%# Eval("path") %>" style="width:80px;height:60px;border:1px dotted #ccc;padding:1px;"  /></a>
     </ItemTemplate>
    </asp:Repeater>
    </marquee>
    <br>
    <asp:Image ID="Image1" Width="400" Height="400" runat="server" />
</asp:Content>

