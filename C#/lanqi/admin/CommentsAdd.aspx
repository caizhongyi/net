<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CommentsAdd.aspx.cs" Inherits="admin_NewAdd2"  validateRequest=false  %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title><link href="../style/StyleSheet.css" rel="stylesheet" type="text/css" />
     
</head>
<body style=" text-align:left; height:1000px" >
   <div class="Currently" style=" text-align:center">
       <br />
       <asp:Label ID="Label1" runat="server"></asp:Label>&nbsp;</div>
<div class="user_reg">
  <form id="form3"  runat=server>
  
  <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6">
      <tbody>
          <tr>
          <td><div runat="server" id="id"></div></td>
          </tr>
          <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td align="right" style="width: 168px; height: 315px;">
              内容</td>
           <td align="left" class="table1" style="width: 622px; height: 315px;" >
              &nbsp;<FCKeditorV2:FCKeditor ID="FCKeditor1" runat="server" Height="300px"  Width="800px">
                                  </FCKeditorV2:FCKeditor>  </td>
          </tr>
         
        <!-- <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
        <td height="50" align="right" style="width: 168px">&nbsp;</td>
         <td align="left" class="table1" style="width: 622px" >
              <asp:Button ID="btnOk" runat="server" Text="确认提交" OnClick="btnOk_Click" /> 
          </td>
        </tr>-->
      </tbody>
    </table>
  
</form>
</div>
</body>

</html>
