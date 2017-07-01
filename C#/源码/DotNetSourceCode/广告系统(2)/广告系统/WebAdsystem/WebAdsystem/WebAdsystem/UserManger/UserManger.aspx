<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserManger.aspx.cs" Inherits="UserManger_UserManger" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" Height="160px" AutoGenerateColumns="False" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating" DataKeyNames="user_id">
            <Columns>
                <asp:BoundField DataField="user_id" HeaderText="用户ID" ReadOnly="True" />
                <asp:BoundField DataField="user_nickname" HeaderText="用户昵称" />
                <asp:BoundField DataField="user_name" HeaderText="用户姓名" />
                <asp:BoundField DataField="user_pwd" HeaderText="用户密码" />
                <asp:BoundField DataField="user_type_id" HeaderText="用户类别id（用户身份表）" />
                <asp:BoundField DataField="user_sex" HeaderText="用户性别" />
                <asp:BoundField DataField="user_birthday" HeaderText="用户出生日期" />
                <asp:BoundField DataField="user_time" HeaderText="用户记录时间" ReadOnly="True" />
                <asp:BoundField DataField="user_remark" HeaderText="备注" />
                <asp:BoundField DataField="user_postalcode" HeaderText="邮编" />
                <asp:BoundField DataField="user_address" HeaderText="地址" />
                <asp:BoundField DataField="user_tel1" HeaderText="联系电话" />
                <asp:BoundField DataField="user_tel2" HeaderText="联系电话" />
                <asp:BoundField DataField="user_fax" HeaderText="传真号码" />
                <asp:BoundField DataField="user_email1" HeaderText="邮件地址" />
                <asp:BoundField DataField="user_email2" HeaderText="邮件地址" />
                <asp:BoundField DataField="user_qq1" HeaderText="QQ号" />
                <asp:BoundField DataField="user_qq2" HeaderText="QQ号" />
                <asp:BoundField DataField="wb_connect" HeaderText="紧急联络方式" />
                <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    
    </div>
        <asp:Button ID="Button1" runat="server" Text="用户增加" OnClick="Button1_Click" />
        <asp:Panel ID="Panel1" runat="server" Height="468px" Width="686px" Visible="False">
            用户登录名:<asp:TextBox ID="LoginName" runat="server"></asp:TextBox><br />
            用户昵称:<asp:TextBox ID="user_nickname" runat="server"></asp:TextBox><br />
            用户姓名:<asp:TextBox ID="user_name" runat="server"></asp:TextBox><br />
            用户密码:<asp:TextBox ID="user_pwd" runat="server" TextMode="Password" Width="149px"></asp:TextBox><br />
            确认密码:<asp:TextBox ID="user_pwdChack" runat="server" TextMode="Password" Width="148px"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="user_pwdChack"
                ControlToValidate="user_pwd" ErrorMessage=" 密码不一致"></asp:CompareValidator><br />
            用户性别:<asp:DropDownList ID="user_sex" runat="server">
                <asp:ListItem Value="1">男</asp:ListItem>
                <asp:ListItem Value="2">女</asp:ListItem>
            </asp:DropDownList><br />
            用户类别:<asp:DropDownList ID="DrUser_type" runat="server" DataSourceID="SqlDataSource1" DataTextField="User_Type_Name" DataValueField="User_Type_Id">
            </asp:DropDownList><asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sq_dbggcmxtConnectionString %>"
                SelectCommand="SELECT * FROM [User_Type]"></asp:SqlDataSource>
            &nbsp;
            <br />
            用户出生日期:<asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999"
                CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                ForeColor="Black" Height="180px" Width="200px">
                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                <SelectorStyle BackColor="#CCCCCC" />
                <WeekendDayStyle BackColor="#FFFFCC" />
                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                <OtherMonthDayStyle ForeColor="#808080" />
                <NextPrevStyle VerticalAlign="Bottom" />
                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
            </asp:Calendar>
            <br />
            &nbsp;<br />
            邮编: &nbsp;&nbsp; &nbsp;<asp:TextBox ID="user_postalcode" runat="server" OnTextChanged="TextBox4_TextChanged"></asp:TextBox><br />
            地址: &nbsp;&nbsp; &nbsp;<asp:TextBox ID="user_address" runat="server" OnTextChanged="TextBox4_TextChanged"></asp:TextBox><br />
            联系电话:
            <asp:TextBox ID="user_tel" runat="server" OnTextChanged="TextBox4_TextChanged"></asp:TextBox><br />
            联系电话1:<asp:TextBox ID="user_tel1" runat="server" OnTextChanged="TextBox4_TextChanged"></asp:TextBox><br />
            传真号码:
            <asp:TextBox ID="user_fax" runat="server" OnTextChanged="TextBox4_TextChanged"></asp:TextBox><br />
            邮件地址:
            <asp:TextBox ID="user_email" runat="server" OnTextChanged="TextBox4_TextChanged"></asp:TextBox><br />
            邮件地址1:<asp:TextBox ID="user_email1" runat="server" OnTextChanged="TextBox4_TextChanged"></asp:TextBox><br />
            QQ: &nbsp; &nbsp; &nbsp;
            <asp:TextBox ID="user_qq" runat="server" OnTextChanged="TextBox4_TextChanged"></asp:TextBox><br />
            QQ1: &nbsp; &nbsp;&nbsp;
            <asp:TextBox ID="user_qq1" runat="server" OnTextChanged="TextBox4_TextChanged"></asp:TextBox><br />
            紧急联络方式:<br />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:TextBox ID="wb_connect" runat="server" OnTextChanged="TextBox4_TextChanged"></asp:TextBox><br />
            备注: &nbsp; &nbsp;
            <asp:TextBox ID="user_remark" runat="server" OnTextChanged="TextBox4_TextChanged" TextMode="MultiLine"></asp:TextBox><br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="增加" />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="取消" /><br />
        </asp:Panel>
        <asp:Panel ID="Panel2" runat="server" Height="468px" Width="686px">
            用户登录名:<asp:Label ID="Label3" runat="server" Text="Label"></asp:Label><br />
            用户昵称: &nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br />
            用户姓名: &nbsp;&nbsp;
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label><br />
            用户性别: &nbsp;&nbsp;
            <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
            <br />
            用户类别: &nbsp;&nbsp;
            <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="Label19" runat="server" Visible="false" Text="Label"></asp:Label><br />
            用户出生日期:<asp:Label ID="Label7" runat="server" Text="Label"></asp:Label><br />
            邮编: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Label ID="Label8" runat="server" Text="Label"></asp:Label><br />
            地址: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Label ID="Label9" runat="server" Text="Label"></asp:Label><br />
            联系电话: &nbsp; &nbsp; &nbsp;<asp:Label ID="Label10" runat="server" Text="Label"></asp:Label><br />
            联系电话1:&nbsp; &nbsp;<asp:Label ID="Label11" runat="server" Text="Label"></asp:Label><br />
            传真号码: &nbsp;&nbsp;
            <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label><br />
            邮件地址: &nbsp; &nbsp;<asp:Label ID="Label13" runat="server" Text="Label"></asp:Label><br />
            邮件地址1: &nbsp;
            <asp:Label ID="Label14" runat="server" Text="Label"></asp:Label><br />
            QQ: &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Label ID="Label15" runat="server" Text="Label"></asp:Label>&nbsp;
            <br />
            QQ1: &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:Label ID="Label16" runat="server" Text="Label"></asp:Label><br />
            紧急联络方式:<asp:Label ID="Label17" runat="server" Text="Label"></asp:Label><br />
            备注: &nbsp;&nbsp; &nbsp; &nbsp;
            <asp:Label ID="Label18" runat="server" Text="Label"></asp:Label>
            &nbsp;
            <br />
            <asp:Button ID="Button4" runat="server" Text="确定" OnClick="Button4_Click" />
            <asp:Button ID="Button5" runat="server" Text="取消" OnClick="Button5_Click" /><br />
        </asp:Panel>
        <br />
        <br />
    </form>
</body>
</html>
