<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WbAddManger.aspx.cs" Inherits="WbAddManger" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label7" runat="server" Text="网吧ID:"></asp:Label>
    <asp:TextBox ID="WB_ID" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="WB_ID"
        ErrorMessage="*"></asp:RequiredFieldValidator><br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="网吧名:"></asp:Label>
    <asp:TextBox ID="WbName" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="WbName"
        ErrorMessage="*"></asp:RequiredFieldValidator><br />
    <br />
    <asp:Label ID="Label3" runat="server" Text="网吧IP:"></asp:Label>
    <asp:TextBox ID="WbIP" runat="server" Width="152px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="WbIP"
        ErrorMessage="*"></asp:RequiredFieldValidator>&nbsp;
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="WbIP"
        ErrorMessage="IP格式不正确" ValidationExpression="([0-9]{1,3}[.]){3}[0-9]{1,3}"></asp:RegularExpressionValidator><br />
    <br />
    <asp:Label ID="Label4" runat="server" Text="网吧所属地:"></asp:Label>
    <asp:DropDownList ID="WbArea" runat="server">
    </asp:DropDownList><br />
    <br />
    <asp:Label ID="Label5" runat="server" Text="网吧联系电话:"></asp:Label>
    <asp:TextBox ID="WbTel" runat="server"></asp:TextBox><br />
    <br />
    <asp:Label ID="Label6" runat="server" Text="备注:"></asp:Label><br />
    <br />
    <asp:TextBox ID="WbRemark" runat="server" Height="109px" Width="249px"></asp:TextBox><br />
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="增加" />
    <asp:Button ID="Button2" runat="server" Text="取消" />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
</asp:Content>

