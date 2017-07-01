<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NewAdd.aspx.cs" Inherits="admin_NewAdd"  validateRequest=false Debug="true" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
     <link href="style/css.css" rel="stylesheet" type="text/css" />
</head>
<body style=" text-align:left; height:1000px" >
   <div class="Currently" style=" text-align:center">
       <br />
       <asp:Label ID="Label1" runat="server"></asp:Label>&nbsp;</div>
<div class="user_reg">
  <form id="form3"  runat=server>
  <ul>
  <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6">
      <tbody>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="40" align="right" style="width: 112px">
              新闻标题：&nbsp;</td>
          <td align="left" class="table1" style="width: 622px" > 
              <asp:TextBox ID="txtName" runat="server" Width="462px"></asp:TextBox></td>
        </tr>
         <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td align="right" style="width: 112px; height: 40px;">
              新闻类别：&nbsp;</td>
          <td align="left" class="table1" style="width: 622px; height: 40px;" > &nbsp;<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                  Width="119px">
              </asp:DropDownList>&nbsp;<asp:DropDownList ID="DropDownList2" runat="server" Width="111px">
              </asp:DropDownList>
              &nbsp;是否推荐：
              <input id="rdy" name=rdtj type="radio" value="是" runat=server  />
              是
              <input id="rdn" name=rdtj type="radio" value="否"  runat=server />
              否&nbsp; &nbsp;排序：<asp:TextBox ID="TextBox1" runat="server" Width="24px"></asp:TextBox></td>
        </tr>
        
              <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td align="right" style="width: 112px; height: 315px;">
              新闻内容：</td>
          <td align="left" class="table1" style="width: 622px; height: 315px;" >
              &nbsp;<FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" Height="300px" >
                                  </FCKeditorV2:FCKeditor>  </td>
        </tr>

        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="50" align="right" style="width: 112px">&nbsp;</td>
          <td align="left" class="table1" style="width: 622px" >
              <asp:Button ID="btnOk" runat="server" Text="确认提交" OnClick="btnOk_Click" /> </td>
        </tr>
      </tbody>
    </table>
  </ul>
</form>
</div>
</body>

</html>
