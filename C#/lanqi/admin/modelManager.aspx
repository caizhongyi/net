<%@ Page Language="C#" AutoEventWireup="true" CodeFile="modelManager.aspx.cs" Inherits="admin_modelManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="style/css.css" rel="stylesheet" type="text/css" /><link href="../style/StyleSheet.css" rel="stylesheet" type="text/css" />
     <script>
    var flag=false;
function checkall()
{
if(flag==false){
var str= document.getElementsByName("ch");
for(var i=0;i<str.length;i++){
str[i].checked=true;
}
flag=true;
}else{
var str= document.getElementsByName("ch");
for(var i=0;i<str.length;i++){
str[i].checked=false;
}
flag=false;
}
}
    </script>
</head>
<body style="text-align:center;"> 
   <form id="Form1" action=""  runat=server>
       <br />
       <asp:Label ID="Label3" runat="server" Text="模特加盟列表"></asp:Label>
  <ul><br />
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6">
      <tbody>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px; width: 123px;" ><strong>姓名</strong></td>
         
          <td  background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>性别</b></td>
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>加盟时间</b></td>
           <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>查看详细</b></td>

          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><strong>管理</strong></td>
        </tr>
         <asp:Repeater ID="rpAdmin" runat="server">
          <ItemTemplate>
          <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="left" class="table1" style="text-align:center" ><%# Eval("name") %></td>
         
          <td class="table1" ><%#  Eval("sex") %></td>
          <td align="center" class="table1" ><%# Eval("join_date") %></td>
          <td align="center" class="table1" ><a href=model.aspx?id=<%# Eval("id") %>>查看详细</a></td>
           
            
          <td align="center" class="table1" ><a href="#"></a>
            <input type="checkbox" name="ch" value=<%# Eval("id") %> /></td>
         </tr>
          </ItemTemplate>
          </asp:Repeater>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" colspan="8" align="right" class="table1" ><input type="button" name="Submit5" value="全选" onclick="checkall()" />
              <asp:Button ID="Button1" runat="server" Text="删除" OnClick="Button1_Click" OnClientClick="return confirm('您确定要删除吗？')" />
              &nbsp;
          </td>
          </tr>
      </tbody>
    </table>
	<div class="page">	<table width="560" border="0" cellpadding="3" cellspacing="0" style="text-align:center" >
              <tr>
                <td align="right" style="height: 30px"><asp:Label ID="Label1" CssClass="font1" runat="server" Text="Label"></asp:Label>
                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                     <asp:HyperLink ID="start" runat="server" ForeColor="#000000">首　页</asp:HyperLink>
                      &nbsp;
                    <asp:HyperLink ID="prev" runat="server" ForeColor="#000000"> 上一页</asp:HyperLink>
                    &nbsp;
                      <asp:HyperLink ID="next" runat="server" ForeColor="#000000">下一页</asp:HyperLink>
                     &nbsp;
                      <asp:HyperLink ID="max" runat="server" ForeColor="#000000">末　页</asp:HyperLink></td>
              </tr>
            </table></div>
  </ul>
</form>
</body>
</html>

