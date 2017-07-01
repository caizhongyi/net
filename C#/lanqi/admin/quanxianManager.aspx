<%@ Page Language="C#" AutoEventWireup="true" CodeFile="quanxianManager.aspx.cs" Inherits="quanxianManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
   <link href="style/css.css" rel="stylesheet" type="text/css" />
     <link href="style/PageButton.css" rel="stylesheet" type="text/css" /><link href="../style/StyleSheet.css" rel="stylesheet" type="text/css" />
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
<body>
    <div >
  <form id="Form1" action=""  runat=server>
  <p></p>
      <asp:DropDownList ID="DropDownList1" runat="server">
      </asp:DropDownList>&nbsp;
      <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="添加权限" />&nbsp;
      管理员：<span runat=server id=adminname></span><br />
  
 
     
          <br />
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6" style="text-align:center" >
      <tbody>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px; width: 14%;" ><strong>管理员</strong></td>
          <td width="21%" align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>权限</b></td>
         
           

          <td width="9%" align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><strong>管理</strong></td>
        </tr>
       <asp:Repeater ID="rpNews" runat="server"  >
          <ItemTemplate>
          <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="left" class="table1" style="text-align:center"  ><%# fun.getById(Eval("userid").ToString(),"id","admin","username")%></td>
          <td height="30" align="left" class="table1" style="text-align:center"  ><%# fun.getById(Eval("quanxianid").ToString(),"id","quanxian","quanxianname")%></td>
          
       
         
          
          <td align="center" class="table1" ><a href="#"></a>
            <input type="checkbox" name="ch" value=<%# Eval("id") %> id="ch" /></td>
         </tr>
          </ItemTemplate>
          </asp:Repeater>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td colspan="8" align="right" class="table1" style="height: 30px" ><input type="button" name="Submit5" value="全选" onclick="checkall()" id="Button2" />&nbsp;
              <asp:Button ID="Button1" runat="server" Text="取消权限" OnClick="Button1_Click" OnClientClick="return confirm('您确定要删取消吗？')" />
              &nbsp;
          </td>
          </tr>
      </tbody>
    </table>

      
</form>
</div>
    <br />
   


    
</body>
</html>

