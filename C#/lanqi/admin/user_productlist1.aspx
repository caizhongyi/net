<%@ Page Language="C#" AutoEventWireup="true" CodeFile="user_productlist1.aspx.cs" Inherits="admin_user_productlist1" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=gb2312" />
<title></title>
<link href="style/css.css" rel="stylesheet" type="text/css" />
 <link href="style/PageButton.css" rel="stylesheet" type="text/css" />
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

<div class="wapper">

<div class="left">

</div>
<div >

<div >
  <form id="Form1" action=""  runat=server>
      <br />
  
  <table style="width: 639px">
  <tr>
  <td style="height: 22px">�����Ʒ</td><td style="height: 22px">
          <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged" AutoPostBack="True">
          </asp:DropDownList></td><td style="height: 22px">
              <asp:DropDownList ID="DropDownList3" runat="server">
              </asp:DropDownList></td><td style="height: 22px">
              <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td><td style="width: 58px; height: 22px;">
              <asp:Button ID="Button2" runat="server" Text="����" OnClick="Button2_Click" /></td>
  </tr>
  </table>
  <ul><br />
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6">
      <tbody>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px; width: 28%;" ><strong>��Ʒ����</strong></td>
          <td width="21%" align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>���</b></td>
          <td width="15%" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>���ʱ��</b></td>
          <td width="9%" align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>�۸�</b></td>
          <td width="9%" align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>����</b></td>
          <td width="9%" align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><b>�Ƿ��Ƽ�</b></td>
          <td width="9%" align="center" background="images/tablethbg.gif" class="ff00" style="height: 30px" ><strong>����</strong></td>
        </tr>
          <asp:Repeater ID="rpProduct" runat="server">
          <ItemTemplate>
          <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="left" class="table1" ><a href=ProductAdd1.aspx?id=<%# Eval("id") %>><%# Eval("name") %></a></td>
          <td align="center" class="table1" ><%#  getTypeById2( Eval("typeid").ToString()) %>>><%# getTypeById3( Eval("typeid").ToString()) %></td>
          <td class="table1" ><%#  Eval("join_date") %></td>
          <td align="center" class="table1" ><%# Eval("newprice") %></td>
          <td align="center" class="table1" >
             <asp:Label ID="lblId" runat="server" Text='<%# Eval("id") %>' Visible="false"></asp:Label><asp:TextBox ID="txtPaiXu" runat="server"  Text='<%# Eval("paixu") %>' Width="20px"></asp:TextBox></td>
          <td align="center" class="table1" ><%# Eval("istj") %></td>
          <td align="center" class="table1" ><a href="#"></a>
            <input type="checkbox" name="ch" value=<%# Eval("id") %> /></td>
         </tr>
          </ItemTemplate>
          </asp:Repeater>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" colspan="6" align="right" class="table1" ><input type="button" name="Submit5" value="ȫѡ" onclick="checkall()" />
              <asp:Button ID="Button3" runat="server" Text="�޸�����" OnClick="Button3_Click" />
              <asp:Button ID="Button1" runat="server" Text="ɾ��" OnClick="Button1_Click" OnClientClick="return confirm('��ȷ��Ҫɾ����')" />
              &nbsp;
          </td>
          </tr>
      </tbody>
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
  </ul>
</form>
</div>
</div>
<!-- right -->

</div>
<!-- wapperEND -->
</body>
</html>
