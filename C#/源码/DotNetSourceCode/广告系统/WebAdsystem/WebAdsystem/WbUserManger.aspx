<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WbUserManger.aspx.cs" Inherits="WbUserManger" Title="Untitled Page" %>

  <asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div><p><asp:Label ID="Label1" runat="server" Text="帐号管理"></asp:Label></p>&nbsp;<br /></div>
      <br />
      <asp:Label ID="Label13" runat="server" Text="帐号名称:"></asp:Label>
      <asp:DropDownList ID="DropDownList1" runat="server">
      </asp:DropDownList>
      &nbsp;
      <asp:Label ID="Label12" runat="server" Text="帐号权限:"></asp:Label>
      &nbsp;<asp:DropDownList ID="DropDownList2" runat="server">
      </asp:DropDownList><br />
      <br />
      <div style=" width:671px">
      <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="671px" CellPadding="4" ForeColor="#333333" GridLines="None" DataKeyNames="User_ID" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
          <Columns>
              <asp:BoundField DataField="user_id" HeaderText="用户ID" ReadOnly="True" />
              <asp:BoundField DataField="login_Name" HeaderText="登入名" />
              <asp:BoundField DataField="user_name" HeaderText="用户姓名" />
              <asp:BoundField DataField="user_right" HeaderText="用户权限" />
              <asp:BoundField DataField="User_Register_Time" HeaderText="用户记录时间" ReadOnly="True" />
              <asp:BoundField DataField="user_remark" HeaderText="备注" />
              <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
          </Columns>
          <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
          <RowStyle BackColor="#F7F6F3" ForeColor="#333333" Height="20px" Width="80px" />
          <EditRowStyle BackColor="#999999" BorderStyle="Inset" Width="671px" Wrap="False" />
          <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
          <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
          <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
          <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
      </asp:GridView>
      </div>
      &nbsp;<br />
      <br />
      &nbsp;
  </asp:Content>

