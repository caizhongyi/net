﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductClass.aspx.cs" Inherits="admin_ProductClass" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
  
     <link href="style/PageButton.css" rel="stylesheet" type="text/css" />
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
<body style="text-align:center; height:1300px">
   <form id="Form1" action=""  runat=server>
       <br />
       <div class="page">
       <asp:Label ID="Label3" runat="server" Text="产品一级类别"></asp:Label><br />
           &nbsp;<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>&nbsp;<asp:Button ID="Button2"
            runat="server" OnClick="Button2_Click" Text="添加" /></div>
  <ul><br />
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6">
      <tbody>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 28px; width: 227px;" ><strong>编号</strong></td>
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 28px" ><b>类别</b></td>
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 28px" ><b>修改</b></td>
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 28px" ><strong>管理</strong></td>
        </tr>
         <asp:Repeater ID="rpNewClass" runat="server">
          <ItemTemplate>
          <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td width="25%" height="30" align="left"  style="text-align:center" ><a  ><%# Eval("id") %></a></td>
          <td  width="25%" align="center"  ><%# Eval("type")  %></td>
         <td  width="25%" align="center"  ><a href=productClassAdd.aspx?id=<%# Eval("id")%>&type=1>修改</a></td>
            
          <td align="center"  ><a href="#"></a>
            <input type="checkbox" name="ch" value=<%# Eval("id") %> /></td>
         </tr>
          </ItemTemplate>
          </asp:Repeater>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" colspan="8" align="right" class="table1" ><input type="button" name="Submit5" value="全选" onclick='checkall("ch")' />
              <asp:Button ID="Button1" runat="server" Text="删除" OnClick="Button1_Click" OnClientClick="return confirm('删除该分类将同时删除所有子类和属于该分类的产品,是否继续？')" />
              &nbsp;
          </td>	
          </tr>
      </tbody>
    </table>
    <div class="nextPagesBox" style="text-align: center; width:100%">
           <webdiyer:aspnetpager id="AspNetPager1" runat="server" cssclass="nextPage" currentpagebuttontextformatstring="<label>{0}</label>"
                        custominfohtml="第%CurrentPageIndex% 页 / 共%PageCount%页" custominfosectionwidth=""
                        enabletheming="true" firstpagetext="<span>首页</span>" lastpagetext="<span>末页</span>"
                        nextpagetext="<span>下一页</span>" onpagechanged="AspNetPager1_PageChanged" pagesize="1"
                        pagingbuttonspacing="" prevpagetext="<span>上一页</span>" showcustominfosection="Left"
                        showpageindexbox="Always" Font-Size="12px" TextAfterPageIndexBox="  页  " 
                        TextBeforePageIndexBox="跳到第 "  BorderStyle="NotSet" 
                        ></webdiyer:aspnetpager></div>
	
  </ul>
  
                        
                        <div class="page">
       <asp:Label ID="Label1" runat="server" Text="产品二级类别"></asp:Label><br />
                            <asp:DropDownList ID="DropDownList1" runat="server">
                            </asp:DropDownList>
           &nbsp;<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>&nbsp;<asp:Button ID="Button3"
            runat="server" OnClick="Button3_Click" Text="添加" /></div>
  <ul><br />
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6">
      <tbody>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 28px; width: 227px;" ><strong>编号</strong></td>
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 28px" ><b>类别</b></td>
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 28px" ><b>修改</b></td>
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 28px" ><strong>管理</strong></td>
        </tr>
         <asp:Repeater ID="rpNewClass2" runat="server">
          <ItemTemplate>
          <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td  width="25%" height="30" align="left" class="table1" style="text-align:center" ><a  ><%# Eval("id") %></a></td>
          <td  width="25%" align="center" class="table1" ><%# Eval("type")  %></td>
          <td  width="25%" align="center" class="table1" ><a href=productClassAdd.aspx?id=<%# Eval("id") %>&type=2>修改</a></td>
            
          <td align="center" class="table1" ><a href="#"></a>
            <input type="checkbox" name="ch2" value=<%# Eval("id") %> /></td>
         </tr>
          </ItemTemplate>
          </asp:Repeater>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" colspan="8" align="right" class="table1" ><input type="button" name="Submit5" value="全选" onclick="checkall('ch2')" />
              <asp:Button ID="Button4" runat="server" Text="删除" OnClick="Button4_Click" OnClientClick="return confirm('删除该分类将同时删除所有子类和属于该分类的产品,是否继续？')" />
              &nbsp;
          </td>
          </tr>
      </tbody>
    </table>
	
  </ul>
  	<div class="nextPagesBox" style="text-align: center; width:100%">
           <webdiyer:aspnetpager id="AspNetPager2" runat="server" cssclass="nextPage" currentpagebuttontextformatstring="<label>{0}</label>"
                        custominfohtml="第%CurrentPageIndex% 页 / 共%PageCount%页" custominfosectionwidth=""
                        enabletheming="true" firstpagetext="<span>首页</span>" lastpagetext="<span>末页</span>"
                        nextpagetext="<span>下一页</span>" onpagechanged="AspNetPager2_PageChanged" pagesize="1"
                        pagingbuttonspacing="" prevpagetext="<span>上一页</span>" showcustominfosection="Left"
                        showpageindexbox="Always" Font-Size="12px" TextAfterPageIndexBox="  页  " 
                        TextBeforePageIndexBox="跳到第 "  BorderStyle="NotSet" 
                        ></webdiyer:aspnetpager></div>
                        
                        <div class="page">
       <asp:Label ID="Label2" runat="server" Text="产品三级类别"></asp:Label><br />
                            <asp:DropDownList ID="DropDownList2" runat="server">
                            </asp:DropDownList>&nbsp;
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>&nbsp;<asp:Button ID="Button5"
            runat="server" OnClick="Button5_Click" Text="添加" /></div>
  <ul><br />
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6">
      <tbody>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 28px; width: 227px;" ><strong>编号</strong></td>
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 28px" ><b>类别</b></td>
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 28px" ><b>修改</b></td>
          <td  align="center" background="images/tablethbg.gif" class="ff00" style="height: 28px" ><strong>管理</strong></td>
        </tr>
         <asp:Repeater ID="rpNewClass3" runat="server">
          <ItemTemplate>
          <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td  width="25%" height="30" align="left" class="table1" style="text-align:center" ><a  ><%# Eval("id") %></a></td>
          <td  width="25%" align="center" class="table1" ><%# Eval("type")  %></td>
          <td  width="25%" align="center" class="table1" ><a href=productClassAdd.aspx?id=<%# Eval("id")%>&type=3 >修改</a></td>
            
          <td align="center" class="table1" ><a href="#"></a>
            <input type="checkbox" name="ch3" value=<%# Eval("id") %> /></td>
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
	
  </ul>
  	<div class="nextPagesBox" style="text-align: center; width:100%">
           <webdiyer:aspnetpager id="AspNetPager3" runat="server" cssclass="nextPage" currentpagebuttontextformatstring="<label>{0}</label>"
                        custominfohtml="第%CurrentPageIndex% 页 / 共%PageCount%页" custominfosectionwidth=""
                        enabletheming="true" firstpagetext="<span>首页</span>" lastpagetext="<span>末页</span>"
                        nextpagetext="<span>下一页</span>" onpagechanged="AspNetPager3_PageChanged" pagesize="1"
                        pagingbuttonspacing="" prevpagetext="<span>上一页</span>" showcustominfosection="Left"
                        showpageindexbox="Always" Font-Size="12px" TextAfterPageIndexBox="  页  " 
                        TextBeforePageIndexBox="跳到第 "  BorderStyle="NotSet" 
                        ></webdiyer:aspnetpager></div>
</form>
</body>
</html>

