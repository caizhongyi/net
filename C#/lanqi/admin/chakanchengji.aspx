<%@ Page Language="C#" AutoEventWireup="true" CodeFile="chakanchengji.aspx.cs" Inherits="admin_chakanchengji" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
     <link href="style/css.css" rel="stylesheet" type="text/css" /><link href="../style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        选择班级专业：<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>&nbsp;
        <asp:Button ID="Button1" runat="server" Text="导出数据" OnClick="Button1_Click" /><br />
        <br />
        <p></p>
        <asp:GridView Width="100%" ID="GridView1" DataKeyNames="身份证" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowUpdating="GridView1_RowUpdating"  OnRowEditing="GridView1_RowEditing" runat="server" AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand" OnRowDataBound="GridView1_RowDataBound1">
            <RowStyle HorizontalAlign="Center" />
            <EditRowStyle HorizontalAlign="Center" />
            <Columns>
                  <asp:TemplateField>
                <ItemTemplate>
                <a href='xiugaichengji.aspx?id=<%# Eval("身份证") %>&table=<%=t  %>'> 修改</a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("身份证") %>'
                        CommandName="del"  OnClientClick ="return confirm('您确认要删除吗?');">删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            </Columns>
        </asp:GridView>
          <asp:GridView Width="100%" ID="GridView2" DataKeyNames="身份证" runat="server" AllowPaging="false" Visible="false">
            <RowStyle HorizontalAlign="Center" />
            <EditRowStyle HorizontalAlign="Center" />
            <Columns>
      
            </Columns>
        </asp:GridView>
        <br />
 
         <asp:Repeater ID="rpAdmin" runat="server">
         <HeaderTemplate>
           <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6">
   
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px; width: 123px;" ><strong>用户名</strong></td>
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>密码</b></td>
          <td  background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>性别</b></td>
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>电子邮箱</b></td>
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>地址</b></td>
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>电话</b></td>
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>手机</b></td>
     
        </tr>
         </HeaderTemplate>
          <ItemTemplate>
          <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="left" class="table1" style="text-align:center" ><a href=adminAdd.aspx?id=<%# Eval("id") %>  ><%# Eval("username") %></a></td>
          <td align="center" class="table1" ><%# Eval("userpassword")  %></td>
          <td class="table1" ><%#  Eval("sex") %></td>
          <td align="center" class="table1" ><%# Eval("email") %></td>
          <td align="center" class="table1" ><%# Eval("inaddress") %>
           <td align="center" class="table1" ><%# Eval("ring") %></td>
            <td align="center" class="table1" ><%# Eval("tel") %></td>
     
         </tr>
          </ItemTemplate>
          <FooterTemplate>
          </table>
          </FooterTemplate>
          </asp:Repeater>
    
     
    
    </div>
    </form>
</body>
</html>
