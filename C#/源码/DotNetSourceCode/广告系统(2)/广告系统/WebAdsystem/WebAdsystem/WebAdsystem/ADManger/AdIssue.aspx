<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdIssue.aspx.cs" Inherits="ADManger_AdIssue" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" Width="712px" OnRowCancelingEdit="GridView1_RowCancelingEdit" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Adv_Id" HeaderText="广告ID" />
                <asp:BoundField DataField="Wb_Id" HeaderText="网吧ID" />
                <asp:BoundField DataField="Adv_Type_Id" HeaderText="广告位ID" />
                <asp:BoundField DataField="Adv_Startday" HeaderText="广告开始时间" />
                <asp:BoundField DataField="Adv_Endday" HeaderText="广告结束时间" />
            </Columns>
        </asp:GridView>
        <asp:Button ID="Button1" runat="server" Text="广告发布" /></div>
        <asp:Panel ID="Panel1" runat="server" Height="461px" Width="711px" Visible="False">
            网吧:&nbsp;
            <asp:DropDownList ID="DrWb" runat="server">
            </asp:DropDownList><br />
            广告位:<asp:DropDownList ID="DrAdv_type" runat="server">
            </asp:DropDownList><br />
            广告开始时间:<asp:Calendar ID="CaAdv_startday" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
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
            广告结束时间:<asp:Calendar ID="CaAdv_endday" runat="server" BackColor="White" BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" Height="180px" Width="200px">
                <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                <SelectorStyle BackColor="#CCCCCC" />
                <WeekendDayStyle BackColor="#FFFFCC" />
                <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                <OtherMonthDayStyle ForeColor="#808080" />
                <NextPrevStyle VerticalAlign="Bottom" />
                <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
            </asp:Calendar>
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text=" 发布" />
            <asp:Button ID="Button3" runat="server" Text="取消" /></asp:Panel>
        <asp:Panel ID="Panel2" runat="server" Height="133px" Width="710px">
            网吧: &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br />
            广告位: &nbsp; &nbsp;&nbsp;
            <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label><br />
            广告开始时间:<asp:Label ID="Label3" runat="server" Text="Label"></asp:Label><br />
            广告结束时间:<asp:Label ID="Label4" runat="server" Text="Label"></asp:Label><br />
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="确定" />
            <asp:Button ID="Button5" runat="server" Text="取消" /></asp:Panel>
    </form>
</body>
</html>
