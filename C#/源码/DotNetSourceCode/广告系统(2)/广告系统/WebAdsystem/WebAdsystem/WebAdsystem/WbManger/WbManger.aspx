<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WbManger.aspx.cs" Inherits="WbManger_WbManger" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" Width="672px" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Wb_Id" HeaderText="网吧ID" />
                <asp:BoundField DataField="Wb_Name" HeaderText="网吧名" />
                <asp:BoundField DataField="Wb_C_Number" HeaderText="网吧电脑数" />
                <asp:BoundField DataField="Wb_Ip" HeaderText="网吧IP" />
                <asp:BoundField DataField="Wb_Area_Id" HeaderText="地区ID" />
                <asp:BoundField DataField="Rk_Id" HeaderText="网吧等级ID" />
                <asp:BoundField DataField="User_Id" HeaderText="发展该网吧业务人员ID" />
                <asp:BoundField DataField="Wb_Master_Id" HeaderText="网吧吧主ID" />
                <asp:BoundField DataField="Wb_Time" HeaderText="网吧记录时间" />
                <asp:BoundField DataField="Wb_Remark" HeaderText="备注" />
                <asp:BoundField DataField="Wb_Postalcode" HeaderText="邮编" />
                <asp:BoundField DataField="Wb_Address" HeaderText="地址" />
                <asp:BoundField DataField="Wb_Tel1" HeaderText="联系电话" />
                <asp:BoundField DataField="Wb_Tel2" HeaderText="联系电话" />
                <asp:BoundField DataField="Wb_Fax" HeaderText="传真" />
                <asp:BoundField DataField="Wb_Email1" HeaderText="邮件地址" />
                <asp:BoundField DataField="Wb_Email2" HeaderText="邮件地址" />
                <asp:BoundField DataField="Wb_QQ1" HeaderText="QQ号" />
                <asp:BoundField DataField="Wb_QQ2" HeaderText="QQ号" />
                <asp:BoundField DataField="Wb_Manager" HeaderText="负责人" />
                <asp:BoundField DataField="Wb_Connect" HeaderText="紧急联络方式" />
                <asp:CommandField ShowEditButton="True" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    
    </div>
        <asp:Button ID="Button1" runat="server" Text="网吧信息增加" OnClick="Button1_Click" />
        <asp:Panel ID="Panel1" runat="server" Height="504px" Width="672px">
            网吧ID: &nbsp;&nbsp;
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
            网吧名: &nbsp;&nbsp;
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br />
            网吧电脑数:<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><br />
            网吧IP: &nbsp;&nbsp;
            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox><br />
            地区: &nbsp; &nbsp;&nbsp;
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList><br />
            网吧等级:&nbsp;
            <asp:DropDownList ID="DropDownList2" runat="server">
            </asp:DropDownList><br />
            发展该网吧业务员:<br />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:DropDownList ID="DropDownList3" runat="server">
            </asp:DropDownList><br />
            网吧吧主:&nbsp;
            <asp:DropDownList ID="DropDownList4" runat="server" OnSelectedIndexChanged="DropDownList4_SelectedIndexChanged">
            </asp:DropDownList><br />
            邮编: &nbsp; &nbsp;&nbsp;
            <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox><br />
            地址: &nbsp; &nbsp;&nbsp;
            <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox><br />
            联系电话:&nbsp;
            <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox><br />
            联系电话1:
            <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox><br />
            传真号码:&nbsp;
            <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox><br />
            邮件地址:&nbsp;
            <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox><br />
            邮件地址1:
            <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox><br />
            QQ: &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox><br />
            QQ1: &nbsp; &nbsp; &nbsp;
            <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox><br />
            负责人: &nbsp;&nbsp;
            <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox><br />
            紧急联络方式:<br />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox><br />
            备注: &nbsp; &nbsp;&nbsp;
            <asp:TextBox ID="TextBox5" runat="server" TextMode="MultiLine"></asp:TextBox>
            <br />
            <asp:Button ID="Button2" runat="server" Text="增加" OnClick="Button2_Click" />
            <asp:Button ID="Button3" runat="server" Text="取消" /><br />
        </asp:Panel>
        <br />
        <asp:Panel ID="Panel2" runat="server" Height="456px" Width="672px">
            网吧ID: &nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br />
            网吧名: &nbsp;&nbsp;
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label><br />
            网吧电脑数:<asp:Label ID="Label3" runat="server" Text="Label"></asp:Label><br />
            网吧IP: &nbsp;&nbsp;
            <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label><br />
            地区: &nbsp; &nbsp;&nbsp;
            <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label><br />
            网吧等级:&nbsp;
            <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label><br />
            发展该网吧业务员:<br />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label><br />
            网吧吧主:&nbsp;
            <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label><br />
            邮编: &nbsp; &nbsp;&nbsp;
            <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label><br />
            地址: &nbsp; &nbsp;&nbsp;
            <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label><br />
            联系电话:&nbsp;
            <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
            <br />
            联系电话1:
            <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label><br />
            传真号码:&nbsp;
            <asp:Label ID="Label13" runat="server" Text="Label"></asp:Label><br />
            邮件地址:&nbsp;
            <asp:Label ID="Label14" runat="server" Text="Label"></asp:Label><br />
            邮件地址1:
            <asp:Label ID="Label15" runat="server" Text="Label"></asp:Label><br />
            QQ: &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:Label ID="Label16" runat="server" Text="Label"></asp:Label><br />
            QQ1: &nbsp; &nbsp; &nbsp;
            <asp:Label ID="Label17" runat="server" Text="Label"></asp:Label><br />
            负责人: &nbsp;&nbsp;
            <asp:Label ID="Label18" runat="server" Text="Label"></asp:Label><br />
            紧急联络方式:<br />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:Label ID="Label19" runat="server" Text="Label"></asp:Label><br />
            备注: &nbsp; &nbsp;&nbsp;
            <asp:Label ID="Label20" runat="server" Text="Label"></asp:Label><br />
            <asp:Button ID="Button4" runat="server" Text="确定" OnClick="Button4_Click" />
            <asp:Button ID="Button5" runat="server" Text="取消" /><br />
        </asp:Panel>
        <br />
    </form>
</body>
</html>
