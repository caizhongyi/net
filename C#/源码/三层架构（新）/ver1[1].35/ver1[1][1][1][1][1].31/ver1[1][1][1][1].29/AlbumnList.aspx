<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="AlbumnList.aspx.cs" Inherits="AlbumnList" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPH" Runat="Server">
   
    <script type="text/javascript">
       function DelAlbumn(id)
       {
         if(confirm("是否要删除该记录"))
         {
            location.href = "?id="+id+"&action=del";
         }
       }
    </script>

   
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="ID"
        DataSourceID="MyPictureDS" Width="446px"  AllowPaging="True" AllowSorting="True" PageSize="5">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="ID" HeaderText="编号" InsertVisible="False" ReadOnly="True"
                SortExpression="ID" />
          
            <asp:BoundField DataField="Name"  HeaderText="相册名称" SortExpression="Name" />
          
            <asp:BoundField DataField="Cover" DataFormatString="&lt;img src={0} width=50 height=50 /&gt;" HeaderText="封面" SortExpression="Cover" />
         
            <asp:BoundField DataField="CreateTime" HeaderText="创建时间" SortExpression="CreateTime" />
            
            <asp:BoundField DataField="ID" HeaderText="删除" DataFormatString="<a href=javascript:DelAlbumn({0})>删除</a>" SortExpression="CreateTime" />
       
        <asp:BoundField DataField="ID" HeaderText="修改" DataFormatString="<a href=AlbumnEdit.aspx?id={0}>修改</a>"  />
        <asp:BoundField DataField="ID" HeaderText="上传图片" DataFormatString="<a href=PicAdd.aspx?id={0}>上传图片</a>"  />
        </Columns>
    </asp:GridView>
    
    <asp:SqlDataSource ID="MyPictureDS" runat="server" ConnectionString="<%$ ConnectionStrings:MyPictureConnectionString %>"
        SelectCommand="SELECT * FROM [Albums] WHERE ([UserName] = @UserName)">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="admin" Name="UserName" SessionField="UserName"
                Type="String" />
    
        </SelectParameters>
    </asp:SqlDataSource>
 
</asp:Content>
