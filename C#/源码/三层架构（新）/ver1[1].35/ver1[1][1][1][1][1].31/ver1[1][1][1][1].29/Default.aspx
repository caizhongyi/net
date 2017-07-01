<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" Title="Untitled Page" %>
<asp:Content ContentPlaceHolderID="MainContentPH" ID="DefaultContent" runat="server">
    <asp:GridView ID="GridView1" ShowHeader=false runat="server" AllowPaging="True" AllowSorting="True"
        AutoGenerateColumns="False" BorderWidth=0 DataKeyNames="ID" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:TemplateField>
              <ItemTemplate>
                 <table width="400" align="left" height="121" border="0" cellpadding="2" cellspacing="2">
                      <tr>
                        <td  align="left" height="17" colspan="2" style="font-size:20px; font-weight:bold; border-bottom:dotted 2px #ccc;">&nbsp;<%# Eval("Name")%></td>
                      </tr>
                      <tr>
                        <td align="left" width="20%" height="86"><img width=119 height=84 src="<%# Eval("Cover")%>" /></td>
                        <td align="left" width="80%" valign="top">&nbsp;<%# Eval("Description")%></td>
                      </tr>
                      <tr>
                        <td align="left" height="18" colspan="2"><b>创建时间:</b><%# Eval("CreateTime")%>&nbsp;&nbsp;<b>人气:</b><%# Eval("Hits")%>  &nbsp;&nbsp;<a href="PicList.aspx?id=<%# Eval("id") %>" target=_blank>查看相册图片</a></td>
                      </tr>
                    </table>
              </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:MyPictureConnectionString %>"
        SelectCommand="SELECT * FROM [Albums] WHERE ([Power] = @Power)">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="Power" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
