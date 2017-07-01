<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="AddUser1" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
        <asp:Panel ID="Panel1" runat="server" Height="104px" Width="622px">
            <asp:Label ID="Label2" runat="server" Text="用户名:"></asp:Label>&nbsp; &nbsp;<asp:TextBox
                ID="LoginName" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="UserName"
                ErrorMessage="*"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="姓名:"></asp:Label>
            &nbsp; &nbsp;
            <asp:TextBox ID="UserName" runat="server"></asp:TextBox><br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="密码:"></asp:Label>
            &nbsp; &nbsp;
            <asp:TextBox ID="UserPwd" runat="server" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="UserPwd"
                ErrorMessage="*"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="确认密码:"></asp:Label>
            <asp:TextBox ID="MUserPwd" runat="server" TextMode="Password"></asp:TextBox>
            &nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="MUserPwd"
                ErrorMessage="*"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToCompare="UserPwd"
                ControlToValidate="MUserPwd" ErrorMessage="密码不一致"></asp:CompareValidator>
            <br />
            <br />
            <asp:Label ID="Label10" runat="server" Text="用户权限:"></asp:Label>
            <asp:DropDownList ID="Right" runat="server">
                <asp:ListItem Value="1">Admin</asp:ListItem>
                <asp:ListItem Value="2">普通用户</asp:ListItem>
            </asp:DropDownList>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="Right"
                ErrorMessage="*"></asp:RequiredFieldValidator>
            <br />
            <br />
            <asp:Label ID="Label11" runat="server" Text="备注:"></asp:Label>&nbsp;<br />
            <br />
            <asp:TextBox ID="ReMark" runat="server" Height="157px" Width="264px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="增加" />
            &nbsp;
            <asp:Button ID="Button6" runat="server" Text="取消" />
        </asp:Panel>
    
    </div>
</asp:Content>

