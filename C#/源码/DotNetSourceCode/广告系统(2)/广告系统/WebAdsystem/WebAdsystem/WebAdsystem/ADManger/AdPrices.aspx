<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdPrices.aspx.cs" Inherits="ADManger_AdPrices" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" Width="679px" AutoGenerateColumns="False" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing"  OnRowUpdating="GridView1_RowUpdating" OnSelectedIndexChanged="Button2_Click" DataKeyNames="Rk_Id">
            <Columns>
                <asp:BoundField DataField="rk_id" HeaderText="网吧等级ID" ReadOnly="True" />
                <asp:BoundField DataField="adv_type_id" HeaderText="广告位ID" ReadOnly="True" />
                <asp:BoundField DataField="Unit_Price" HeaderText="单价" />
                <asp:BoundField DataField="Remark" HeaderText="备注" />
                <asp:BoundField DataField="modify_time" HeaderText="执行开始时间" />
                <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
        <asp:Button ID="Button1" runat="server" Text="增加广告位" OnClick="Button1_Click" /></div>
        <asp:Panel ID="Panel1" runat="server" Height="124px" Width="677px" Visible="False">
            广告位: &nbsp; &nbsp;&nbsp;
            <asp:DropDownList ID="DrAdv_type" runat="server" DataSourceID="SqlDataSource1" DataTextField="Adv_Type_Name" DataValueField="Adv_Type_Id">
            </asp:DropDownList><asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sq_dbggcmxtConnectionString %>"
                SelectCommand="SELECT * FROM [Adv_Type]"></asp:SqlDataSource>
            <br />
            单价:&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
            <asp:TextBox ID="unitprice" runat="server"></asp:TextBox>
            <br />
            执行开始时间:<asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#999999"
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
            备注: &nbsp; &nbsp; &nbsp; &nbsp;<asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine"></asp:TextBox><br />
            <br />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="增加" />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="取消" /></asp:Panel>
        <br />
        <asp:Panel ID="Panel2" runat="server" Height="124px" Width="677px">
            广告位: &nbsp;&nbsp; &nbsp;
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br />
            单价:&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label><br />
            执行开始时间:<asp:Label ID="Label4" runat="server" Text="Label"></asp:Label><br />
            备注: &nbsp; &nbsp; &nbsp; &nbsp;<asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>&nbsp;<br />
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="确定" />
            <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="取消" /></asp:Panel>
    </form>
</body>
</html>
