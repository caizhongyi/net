<%@ Page Language="C#" AutoEventWireup="true" CodeFile="questionmanager.aspx.cs" Inherits="admin_questionmanager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
   <link href="style/css.css" rel="stylesheet" type="text/css" />
<link href="../style/StyleSheet.css" rel="stylesheet" type="text/css" />
      <script>
    var flag=false;
function checkall( id )
{
if(flag==false){
var str= document.getElementsByName(id);
for(var i=0;i<str.length;i++){
str[i].checked=true;
}
flag=true;
}else{
var str= document.getElementsByName(id);
for(var i=0;i<str.length;i++){
str[i].checked=false;
}
flag=false;
}
}
    </script>
</head>
<body>
    <form id="form1" runat="server">
<table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6">
      <tbody>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td  width=10% align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px; width: 123px;" ><strong>标题</strong></td>
          
          
          <td  width=10% align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>留言人</b></td>
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>留言内容</b></td>
            <td width=10%  align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>添加时间</b></td>
         
     
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 28px" ><strong></strong></td>
        </tr>
         <asp:Repeater ID="rpAdmin" runat="server">
          <ItemTemplate>
          <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="left" class="table1" style="text-align:center" ><%# Eval("name") %></td>
         
        
          <td align="center" class="table1" ><%#  Eval("username") %></td>
          <td align="center" class="table1" ><%#  Eval("question_content") %></td>
           <td align="center" class="table1" ><%# Eval("join_date") %></td>
         
        
              <td align="center" class="table1" ><a href="#"></a>
            <input type="checkbox" name="ch" value=<%# Eval("id") %> /></td>
            
       
         </tr>
          </ItemTemplate>
          </asp:Repeater>
               <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" colspan="8" align="right" class="table1" ><input type="button" name="Submit5" value="全选" onclick="checkall('ch3')" />
              <asp:Button ID="Button6" runat="server" Text="删除" OnClick="Button6_Click" OnClientClick="return confirm('删除该分类将同时删除所有子类和属于该分类的产品,是否继续？')" />
              &nbsp;
          </td>
          </tr>

      </tbody>
    </table>
    <table width="560" border="0" cellpadding="3" cellspacing="0" style="text-align:center" >
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
            </table>
    </form>
</body>
</html>
