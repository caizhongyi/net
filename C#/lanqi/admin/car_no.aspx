<%@ Page Language="C#" AutoEventWireup="true" CodeFile="car_no.aspx.cs" Inherits="admin_car_no" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v8.1, Version=8.1.1.0, Culture=neutral, PublicKeyToken=e0b364db0ebed33f" namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v8.1, Version=8.1.1.0, Culture=neutral, PublicKeyToken=e0b364db0ebed33f" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
      
    </div>
    <dxwgv:ASPxGridView ID="ASPxGridView1" runat="server" 
        AutoGenerateColumns="False" DataSourceID="SqlDataSource1" Width="100%" 
        KeyFieldName="cid" CssFilePath="~/App_Themes/Glass/{0}/styles.css" 
        CssPostfix="Glass">
        <Styles CssFilePath="~/App_Themes/Glass/{0}/styles.css" CssPostfix="Glass">
            <Header ImageSpacing="5px" SortingImageSpacing="5px">
            </Header>
        </Styles>
        <Images ImageFolder="~/App_Themes/Glass/{0}/">
            <CollapsedButton Height="12px" Width="11px" />
            <DetailCollapsedButton Height="9px" Width="9px" />
            <PopupEditFormWindowClose Height="17px" Width="17px" />
        </Images>
        <SettingsText CommandCancel="取消" CommandDelete="删除" CommandEdit="编辑" 
            CommandNew="添加" CommandUpdate="更新" EmptyDataRow="没有数据" />
        <Columns>
            <dxwgv:GridViewCommandColumn VisibleIndex="0">
                <EditButton Visible="True">
                </EditButton>
                <NewButton Visible="True">
                </NewButton>
                <DeleteButton Visible="True">
                </DeleteButton>
            </dxwgv:GridViewCommandColumn>
           
            <dxwgv:GridViewDataTextColumn Caption="品牌名称" FieldName="cname" VisibleIndex="2">
            </dxwgv:GridViewDataTextColumn>
        </Columns>
    </dxwgv:ASPxGridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        DeleteCommand="pro_car_no_Delete" DeleteCommandType="StoredProcedure" 
        InsertCommand="pro_car_no_Insert" InsertCommandType="StoredProcedure" 
        SelectCommand="pro_car_no_SelectAll" SelectCommandType="StoredProcedure" 
        UpdateCommand="pro_car_no_Update" UpdateCommandType="StoredProcedure">
        <DeleteParameters>
            <asp:Parameter Name="cid" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="cid" Type="Int32" />
            <asp:Parameter Name="cname" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="cname" Type="String" />
        </InsertParameters>
    </asp:SqlDataSource>
    <br />
    <dxwgv:ASPxGridView ID="ASPxGridView2" runat="server" 
        AutoGenerateColumns="False" DataSourceID="SqlDataSource2" Width="100%" 
        KeyFieldName="xid" CssFilePath="~/App_Themes/Glass/{0}/styles.css" 
        CssPostfix="Glass">
        <Styles CssFilePath="~/App_Themes/Glass/{0}/styles.css" CssPostfix="Glass">
            <Header ImageSpacing="5px" SortingImageSpacing="5px">
            </Header>
        </Styles>
        <Images ImageFolder="~/App_Themes/Glass/{0}/">
            <CollapsedButton Height="12px" Width="11px" />
            <DetailCollapsedButton Height="9px" Width="9px" />
            <PopupEditFormWindowClose Height="17px" Width="17px" />
        </Images>
        <SettingsText CommandCancel="取消" CommandDelete="删除" CommandEdit="编辑" 
            CommandNew="添加" CommandUpdate="更新" EmptyDataRow="没有数据" />
        <Columns>
            <dxwgv:GridViewCommandColumn VisibleIndex="0">
                <EditButton Visible="True">
                </EditButton>
                <NewButton Visible="True">
                </NewButton>
                <DeleteButton Visible="True">
                </DeleteButton>
            </dxwgv:GridViewCommandColumn>
           
            <dxwgv:GridViewDataTextColumn Caption="汽车型号" FieldName="xname" VisibleIndex="2">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataComboBoxColumn Caption="所属品牌" FieldName="cid" 
                VisibleIndex="2">
                <PropertiesComboBox DataSourceID="SqlDataSource1" TextField="cname" 
                    ValueField="cid" ValueType="System.String">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
        </Columns>
    </dxwgv:ASPxGridView>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        DeleteCommand="pro_car_type_Delete" DeleteCommandType="StoredProcedure" 
        InsertCommand="pro_car_type_Insert" InsertCommandType="StoredProcedure" 
        SelectCommand="pro_car_type_SelectAll" SelectCommandType="StoredProcedure" 
        UpdateCommand="pro_car_type_Update" UpdateCommandType="StoredProcedure">
        <DeleteParameters>
            <asp:Parameter Name="xid" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="xid" Type="Int32" />
            <asp:Parameter Name="xname" Type="String" />
            <asp:Parameter Name="cid" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="xname" Type="String" />
            <asp:Parameter Name="cid" Type="Int32" />
            <asp:Parameter Direction="InputOutput" Name="xid" Type="Int32" />
        </InsertParameters>
    </asp:SqlDataSource>
    <br />
    <dxwgv:ASPxGridView ID="ASPxGridView3" runat="server" 
        AutoGenerateColumns="False" DataSourceID="SqlDataSource3" Width="100%" 
        KeyFieldName="colorid" CssFilePath="~/App_Themes/Glass/{0}/styles.css" 
        CssPostfix="Glass">
        <Styles CssFilePath="~/App_Themes/Glass/{0}/styles.css" CssPostfix="Glass">
            <Header ImageSpacing="5px" SortingImageSpacing="5px">
            </Header>
        </Styles>
        <Images ImageFolder="~/App_Themes/Glass/{0}/">
            <CollapsedButton Height="12px" Width="11px" />
            <DetailCollapsedButton Height="9px" Width="9px" />
            <PopupEditFormWindowClose Height="17px" Width="17px" />
        </Images>
        <SettingsText CommandCancel="取消" CommandDelete="删除" CommandEdit="编辑" 
            CommandNew="添加" CommandUpdate="更新" EmptyDataRow="没有数据" />
        <Columns>
            <dxwgv:GridViewCommandColumn VisibleIndex="0">
                <EditButton Visible="True">
                </EditButton>
                <NewButton Visible="True">
                </NewButton>
                <DeleteButton Visible="True">
                </DeleteButton>
            </dxwgv:GridViewCommandColumn>
           
            <dxwgv:GridViewDataTextColumn Caption="汽车颜色" FieldName="colorname" 
                VisibleIndex="2">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataComboBoxColumn Caption="所属汽车型号" FieldName="cid" 
                VisibleIndex="2">
                <PropertiesComboBox DataSourceID="SqlDataSource2" TextField="xname" 
                    ValueField="xid" ValueType="System.String">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
        </Columns>
    </dxwgv:ASPxGridView>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
        DeleteCommand="pro_car_color_Delete" DeleteCommandType="StoredProcedure" 
        InsertCommand="pro_car_color_Insert" InsertCommandType="StoredProcedure" 
        SelectCommand="pro_car_color_SelectAll" SelectCommandType="StoredProcedure" 
        UpdateCommand="pro_car_color_Update" UpdateCommandType="StoredProcedure">
        <DeleteParameters>
            <asp:Parameter Name="colorid" Type="Int32" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="colorid" Type="Int32" />
            <asp:Parameter Name="colorname" Type="String" />
            <asp:Parameter Name="cid" Type="Int32" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="colorname" Type="String" />
            <asp:Parameter Name="cid" Type="Int32" />
        </InsertParameters>
    </asp:SqlDataSource>
    </form>
</body>
</html>
