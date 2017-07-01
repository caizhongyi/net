<%@ Page Language="C#" AutoEventWireup="true"  EnableEventValidation="false"   CodeFile="AdManger.aspx.cs" Inherits="ADManger_AdManger" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" Height="152px" Width="718px" AutoGenerateColumns="False" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" DataKeyNames="Adv_Id" OnRowUpdating="GridView1_RowUpdating">
            <Columns>
                <asp:BoundField DataField="Adv_Id" HeaderText="广告ID" />
                <asp:BoundField DataField="Adv_Name" HeaderText="广告名" />
                <asp:BoundField DataField="Adv_Content" HeaderText="广告内容" />
                <asp:BoundField DataField="Adv_Url" HeaderText="广告图片地址" />
                <asp:BoundField DataField="Adv_Master_Id" HeaderText="广告主ID" />
                <asp:BoundField DataField="User_Id" HeaderText="广告业务员" />
                <asp:BoundField DataField="Adv_Operation" HeaderText="是否发布" />
                <asp:BoundField DataField="Adv_ClickNumber" HeaderText="广告点击次数" />
                <asp:BoundField DataField="Adv_Time" HeaderText="发布广告的时间" />
                <asp:BoundField DataField="Adv_Discount" HeaderText="折扣率" />
                <asp:BoundField DataField="Pay_State" HeaderText="是否已付款" />
                <asp:CommandField ButtonType="Button" ShowEditButton="True" />
                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    
    </div>
        <asp:Button ID="Button1" runat="server" Text="增加广告" OnClick="Button1_Click" />
        <asp:Panel ID="Panel1" runat="server" Height="286px" Width="704px">
            广告名:&nbsp;
            <asp:TextBox ID="adv_name" runat="server"></asp:TextBox><br />
            广告图片地址:<br />
            <asp:FileUpload ID="FileUpload1" runat="server" /><br />
            广告主:&nbsp;&nbsp; &nbsp;<asp:DropDownList ID="DrAdv_master" runat="server" DataSourceID="SqlDataSource1" DataTextField="User_Name" DataValueField="User_Id">
            </asp:DropDownList><asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:sq_dbggcmxtConnectionString %>"
                SelectCommand="SELECT * FROM [User_Info]"></asp:SqlDataSource>
            <br />
            广告业务员:<asp:DropDownList ID="DrUser" runat="server" DataSourceID="SqlDataSource1" DataTextField="User_Name" DataValueField="User_Id">
            </asp:DropDownList><br />
            &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
            <br />
            折扣率:&nbsp;&nbsp; &nbsp;<asp:TextBox ID="adv_discount" runat="server"></asp:TextBox><br />
            是否已付款:<asp:DropDownList ID="DrPay_state" runat="server">
                <asp:ListItem Value="0">否</asp:ListItem>
                <asp:ListItem Value="1">是</asp:ListItem>
            </asp:DropDownList><br />
            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
            <br />
            广告内容:&nbsp;
            <asp:TextBox ID="adv_content" runat="server" TextMode="MultiLine"></asp:TextBox><br />
            <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="增加" />
            <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="取消" /></asp:Panel>
        <br />
        <asp:Panel ID="Panel2" runat="server" Height="183px" Width="707px">
            广告名: &nbsp; &nbsp; &nbsp;<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label><br />
            广告图片: &nbsp;&nbsp;
            <asp:Image ID="Image1" runat="server" /><br />
            广告主: &nbsp; &nbsp; &nbsp;<asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="Label2"  Visible="false" runat="server" Text="Label"></asp:Label><br />
            广告业务员:&nbsp;
            <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
            <asp:Label ID="Label8" Visible="false" runat="server" Text="Label"></asp:Label><br />
            折扣率: &nbsp; &nbsp;&nbsp;
            <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label><br />
            是否已付款:&nbsp;
            <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label><br />
            广告内容: &nbsp;&nbsp;
            <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label><br />
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="确定" />&nbsp;
            <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="取消" /></asp:Panel>
        <br />
        <asp:Panel ID="Panel3" runat="server" Height="80px" Width="706px">
            广告名:&nbsp;
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><br />
            广告图片地址:<br />
            <asp:FileUpload ID="FileUpload2" runat="server" /><br />
            广告主: &nbsp; &nbsp;<asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1"
                DataTextField="User_Name" DataValueField="User_Id">
            </asp:DropDownList><asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:sq_dbggcmxtConnectionString %>"
                SelectCommand="SELECT * FROM [User_Info]"></asp:SqlDataSource>
            <br />
            广告业务员:<asp:DropDownList ID="DropDownList2" runat="server" DataSourceID="SqlDataSource1"
                DataTextField="User_Name" DataValueField="User_Id">
            </asp:DropDownList><br />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <br />
            折扣率: &nbsp; &nbsp;<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox><br />
            是否已付款:<asp:DropDownList ID="DropDownList3" runat="server">
                <asp:ListItem Value="0">否</asp:ListItem>
                <asp:ListItem Value="1">是</asp:ListItem>
            </asp:DropDownList><br />
            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
            <br />
            广告内容:&nbsp;
            <asp:TextBox ID="TextBox3" runat="server" TextMode="MultiLine"></asp:TextBox><br />
            <asp:Button ID="Button6" runat="server" OnClick="Button3_Click" Text="增加" />
            <asp:Button ID="Button7" runat="server" OnClick="Button2_Click" Text="取消" /></asp:Panel>
        <br />
        <asp:Panel ID="Panel4" runat="server" Height="50px" Width="703px">
            <font style="color: red"><span style="color: #000000">广告名:
                <asp:Label ID="Label9" runat="server" Font-Bold="True" ForeColor="#0000C0" Text="Label"></asp:Label></span><br />
                <asp:Image ID="Image2" runat="server" Height="95px" Width="185px" /><br />
                <br />
                你确定要删除这条记录吗？<br />
                &nbsp;&nbsp; (删除完，不可复原)</font><br />
            &nbsp; &nbsp; &nbsp;
            <asp:Button ID="Button8" runat="server" OnClick="Button8_Click" Text="确定" />
            &nbsp; &nbsp; &nbsp;<asp:Button ID="Button9" runat="server" OnClick="Button9_Click"
                Text="取消" /></asp:Panel>
    </form>
</body>
</html>
