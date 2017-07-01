<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AlbumnEdit.aspx.cs" Inherits="AlbumnEdit" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPH" Runat="Server">
   <table>
        <tr>
            <td style="width: 591px;">
                <b>当前所在位置:首页-&gt;修改相册</b></td>
        </tr>
    </table>
   
   
    <table>
        <tr>
            <td style="width: 200px">
                相册名称:</td>
            <td style="width: 525px">
                <asp:TextBox ID="Name" runat="server" EnableViewState="False"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Name"
                    ErrorMessage="请输入相册名称"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="width: 200px; height: 24px;">
                封面:</td>
            <td style="width: 525px; height: 24px;">
                <asp:FileUpload ID="Cover" runat="server" />
                <asp:Image ID="Image1" runat="server" BorderWidth="1px" Height="40px" Width="40px" /></td>
        </tr>
        <tr>
            <td style="width: 200px; height: 174px;">
                相册描述:</td>
            <td style="width: 525px; height: 174px;">
                <asp:TextBox ID="Description" runat="server" Height="164px" TextMode="MultiLine"
                    Width="378px"></asp:TextBox></td>
        </tr>
        <tr>
            <td style="width: 200px; height: 17px">
                权限:</td>
            <td style="width: 525px; height: 17px">
                <asp:RadioButtonList ID="Power" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="0">公开</asp:ListItem>
                    <asp:ListItem Value="1">好友可见</asp:ListItem>
                    <asp:ListItem Value="2">私有</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr>
            <td style="width: 200px; height: 26px">
            </td>
            <td style="width: 525px; height: 26px">
                <asp:Button ID="Button1" runat="server" Text="修改相册" OnClick="Button1_Click"  />
                <asp:Label ID="Label1" runat="server" ForeColor="OrangeRed" Visible="False"></asp:Label></td>
        </tr>
    </table>
</asp:Content>

