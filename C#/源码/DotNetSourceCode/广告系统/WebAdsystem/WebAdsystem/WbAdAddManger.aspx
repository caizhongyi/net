<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WbAdAddManger.aspx.cs" Inherits="WbAdAddManger" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" Height="251px" Width="674px">
        <asp:Label ID="Label5" runat="server" Text="广告上传"></asp:Label><br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="广告ID:"></asp:Label>
        &nbsp;&nbsp;
        <asp:TextBox ID="AdID" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="AdID"
            ErrorMessage="*"></asp:RequiredFieldValidator><br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="广告名:"></asp:Label>&nbsp; &nbsp;<asp:TextBox
            ID="AdName" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="AdName"
            ErrorMessage="*"></asp:RequiredFieldValidator><br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="广告路径:"></asp:Label>
        &nbsp; &nbsp;<asp:FileUpload ID="FileUpload1" runat="server" />
        &nbsp;<br />
        <br />
        <asp:Label ID="Label6" runat="server" Text="广告类型:"></asp:Label>
        <asp:DropDownList ID="AdType" runat="server">
            <asp:ListItem Value="1">Type1</asp:ListItem>
            <asp:ListItem Value="2">Type2</asp:ListItem>
            <asp:ListItem Value="3">Type3</asp:ListItem>
            <asp:ListItem Value="4">Type4</asp:ListItem>
            <asp:ListItem Value="0">backgroupWall</asp:ListItem>
        </asp:DropDownList><br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="广告备注:"></asp:Label><br />
        <br />
        <asp:TextBox ID="AdRemark" runat="server" Height="145px" Width="342px" TextMode="MultiLine"></asp:TextBox><br />
        <br />
        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="增加" />
        &nbsp; &nbsp; &nbsp; &nbsp;<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click"
            Text="取消" /></asp:Panel>
</asp:Content>

