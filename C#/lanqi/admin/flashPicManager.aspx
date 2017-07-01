<%@ Page Language="C#" AutoEventWireup="true" CodeFile="flashPicManager.aspx.cs" Inherits="admin_flashPicManager" %>

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
      <br />
  
 
      <ul>
          <br />
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6" style="text-align:center" >
      <tbody>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px; width: 14%;" ><strong>图片名称</strong></td>
          <td width="21%" align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>链接网址</b></td>
         
           <td width="9%" align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>广告类型</b></td>
          <td width="9%" align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>添加时间</b></td>
         
          <td width="9%" align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>查看详细</b></td>
          <td width="9%" align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><strong>管理</strong></td>
        </tr>
       <asp:Repeater ID="rpNews" runat="server"  >
          <ItemTemplate>
          <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="left" class="table1" style="text-align:center"  ><%# Eval("pic")%></td>
          <td height="30" align="left" class="table1" style="text-align:center"  ><%# fun.Left(Eval("web_address").ToString(), 8)%></td>
          
       
        <td align="center" class="table1" style="text-align:center"  ><%# Eval("type").ToString()=="0"?"Flash广告":"商家广告" %></td>
          <td align="center" class="table1" style="text-align:center"  ><%# Eval("addtime") %></td>
          
           <td align="center" class="table1" ><a href=flashpicadd.aspx?id=<%# Eval("id") %>>查看详细</a></td>
          <td align="center" class="table1" ><a href="#"></a>
            <input type="checkbox" name="ch" value=<%# Eval("id") %> id="ch" /></td>
         </tr>
          </ItemTemplate>
          </asp:Repeater>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td colspan="8" align="right" class="table1" style="height: 30px" ><input type="button" name="Submit5" value="全选" onclick="checkall()" id="Button2" />&nbsp;
              <asp:Button ID="Button1" runat="server" Text="删除" OnClick="Button1_Click" OnClientClick="return confirm('您确定要删除吗？')" />
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
</form>
</div>
    <br />
   


    
</body>
</html>
