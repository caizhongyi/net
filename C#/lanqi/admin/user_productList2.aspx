<%@ Page Language="C#" AutoEventWireup="true" CodeFile="user_productList2.aspx.cs" Inherits="user_productList2" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title>רҵ���ϵ�������--�����̳�</title>

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
<body style="">

<div class="wapper">

<div class="left">

</div>
<div >

<div >
  <form action=""  runat=server>
      <br />
  
  <table style="">
  <tr>
  <td style="height: 22px; width: 105px;">
      ��Ʒ���</td><td style="height: 22px">
      </td><td style="height: 22px">
          </td><td style="height: 22px">
              <asp:DropDownList ID="DropDownList3" runat="server">
              </asp:DropDownList></td><td style="height: 22px">
              <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td><td style="width: 58px; height: 22px;">
              <asp:Button ID="Button2" runat="server" Text="����" OnClick="Button2_Click" /></td>
  </tr>
  </table>
  
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6">
      
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px; width: 5%;" ><strong>��Ʒ����</strong></td>
          <td align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px; width: 5%;" ><b>���</b></td>
           
          <td background="images/tablethbg.gif" class="ff00" style="height: 30px; width: 5%;" ><b>���ʱ��</b></td>
          
         
          
         
          <td align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px; width: 5%;" ><strong>����</strong></td>
        </tr>
          <asp:Repeater ID="rpProduct" runat="server">
          <ItemTemplate>
          <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="left" class="table1" style="text-align:center" ><a href=ProductAdd2.aspx?id=<%# Eval("id") %>><%# Eval("name") %></a></td>
          <td align="center" class="table1" ><%# getTypeById3( Eval("typeid").ToString()) %></td>
        
          <td class="table1" ><%#  Eval("join_date") %></td>
           
          

          
          <td align="center" class="table1" ><a href="#"></a>
            <input type="checkbox" name="ch" value=<%# Eval("id") %> /></td>
         </tr>
          </ItemTemplate>
          </asp:Repeater>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" colspan="6" align="right" class="table1" ><input type="button" name="Submit5" value="ȫѡ" onclick="checkall()" />&nbsp;
              <asp:Button ID="Button1" runat="server" Text="ɾ��" OnClick="Button1_Click" OnClientClick="return confirm('��ȷ��Ҫɾ����')" />
              &nbsp;
          </td>
          </tr>
      
    </table>
	<div class="nextPagesBox" style="text-align: center; width:100%">
           <webdiyer:aspnetpager id="AspNetPager1" runat="server" cssclass="nextPage" currentpagebuttontextformatstring="<label>{0}</label>"
                        custominfohtml="��%CurrentPageIndex% ҳ / ��%PageCount%ҳ" custominfosectionwidth=""
                        enabletheming="true" firstpagetext="<span>��ҳ</span>" lastpagetext="<span>ĩҳ</span>"
                        nextpagetext="<span>��һҳ</span>" onpagechanged="AspNetPager1_PageChanged" pagesize="1"
                        pagingbuttonspacing="" prevpagetext="<span>��һҳ</span>" showcustominfosection="Left"
                        showpageindexbox="Always" Font-Size="12px" TextAfterPageIndexBox="  ҳ  " 
                        TextBeforePageIndexBox="������ "  BorderStyle="NotSet" 
                        ></webdiyer:aspnetpager></div>
  
</form>
</div>
</div>
<!-- right -->

</div>
<!-- wapperEND -->
</body>
</html>