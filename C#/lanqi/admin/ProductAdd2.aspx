<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductAdd2.aspx.cs" Inherits="admin_ProductAdd2" validateRequest=false %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
     <link href="style/css.css" rel="stylesheet" type="text/css" /><link href="../style/StyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body style=" text-align:left; height:1500px">
    <div class="Currently" style="text-align:center">
        
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>&nbsp;</div>
<div class="user_reg">
  <form id="Form1" action="" runat=server>
  <br />
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6">
      <tbody>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="right" style="width: 126px">
              产品名称：</td><td width="78%" align="left" class="table1" ><input name="name" type="text" class="formr" size="50" id="name" runat=server /></td>
          </tr>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td align="right" style="height: 30px; width: 126px;">
              产品类别：</td><td align="left" class="table1" style="height: 30px" >
              <asp:DropDownList ID="DropDownList3" runat="server">
              </asp:DropDownList> 产品地域：<select id="slDy" runat=server>
                      <option></option>
                  </select> 产品大类：<select id="slType" runat=server>
                      <option value=1>琅岐旅游服务</option>
                      <option value=2>中医养生保健</option>
                      <option value=3>书法</option>
                      <option value=4>商家区</option>
<option value=4>百货区</option>
<option value=4>18张滚动图</option>
<option value=4>顶部滚动图</option>
                  </select></td>
          </tr>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="right" style="width: 126px">规格：<td align="left" class="table1" ><input name="spec" type="text" class="formr" size="50" id="spec" runat=server /></td>
          </tr>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="right" style="width: 126px">库存：        
          <td align="left" class="table1" >
              <input id="number" style="width: 42px" type="text" runat=server value=0 />&nbsp; 是否推荐：
              <input id="rdy" runat="server" name="rdtj" type="radio" value="是" />
              是
              <input id="rdn" runat="server" name="rdtj" type="radio" value="否" />
              否</td>
        </tr>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="right" style="width: 126px">
              计费类型：</td>
          <td align="left" class="table1" >
              <select id="oldprice" runat="server">
                  <option  value="cpa" >cpa</option>
                  <option value="cps" >cps</option>
              </select>
          </td>
        </tr>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="right" style="width: 126px">
              价格：</td>
          <td align="left" class="table1" ><input name="newprice" type="text" class="forme" size="10" id="newprice" runat=server /></td>
          </tr>
    
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td align="right" style="width: 126px">
              产品详细说明：<td align="left" valign="top" class="table1" >
             <FCKeditorV2:FCKeditor ID="explain" runat="server" Height="300px" >
                                  </FCKeditorV2:FCKeditor>  </td>
        </tr>
            <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="right" style="width: 126px">
              产品大图片：<td align="left" class="table1" ><table><tr><td style="height: 100px"><input runat=server name="fileBig" type="file" size="40" id="fileBig" onchange="javascript:document.getElementById('ImageBig').src=this.value"  /></td><td style="height: 100px"> <asp:Image ID="ImageBig" runat="server" Height="100px" Width="100px" /></td><td style="height: 100px">
              </td></tr></table>
             </td>
        </tr>
        
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="right" style="width: 126px">
              产品小图片：<td align="left" class="table1" ><table><tr><td><input runat=server name="fileSmal" type="file" size="40" id="fileSmal" onchange="javascript:document.getElementById('ImageSmall').src=this.value"  /></td><td>  <asp:Image ID="ImageSmall" runat="server" Height="100px" Width="100px" /></td><td>
              </td></tr></table></td>
        </tr>
         <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td align="right" style="height: 30px; width: 126px;">链接地址：        
          <td align="left" class="table1" style="height: 30px" ><input name="address" type="text" class="formr" size="50" id="address" runat=server /></td>
        </tr>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="right" style="width: 126px">商户名称：        
          <td align="left" class="table1" ><input name="makername" type="text" class="formr" size="50" id="makername" runat=server /></td>
        </tr>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="right" style="width: 126px">商户电话：        
          
          <td align="left" class="table1" ><input name="tel" type="text" class="formr" size="50" id="tel" runat=server /></td>
        </tr>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="50" align="right" style="width: 126px">&nbsp;</td>
          <td align="left" class="table1" >
              <asp:Button ID="Button1" runat="server" Text="确认提交" OnClick="Button1_Click" /></td>
        </tr>
      </tbody>
    </table>
    
</form>
</div>
</body>
</html>
