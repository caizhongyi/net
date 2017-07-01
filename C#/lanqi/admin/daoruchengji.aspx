<%@ Page Language="C#" AutoEventWireup="true" CodeFile="daoruchengji.aspx.cs" Inherits="admin_daoruchengji" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
     <link href="style/css.css" rel="stylesheet" type="text/css" /><link href="../style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style=" text-align:left" scrolling="yes">
    <form id="form1" runat="server" >
    <div  style="text-align:center">
        <br />
        &nbsp;<br />
        <br />
      <table width="100%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CEDDE6">
      <tbody>
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td width="22%" height="30" align="right">
              选择专业：<td width="78%" align="left" class="table1" >
                  &nbsp;<asp:DropDownList ID="DropDownList1" runat="server">
                  </asp:DropDownList></td>
          </tr>
        
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="right">
              选择excel文件：<td align="left" class="table1" >
                  <input id="File1" runat=server type="file" /></td>
          </tr>
    
        
       
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="50" align="right">&nbsp;</td>
          <td align="left" class="table1" id="TD1" runat="server" >
              <asp:Button ID="Button1" runat="server" Text="导入成绩" OnClick="Button1_Click" Width="54px" />
              <asp:Button ID="Button2" runat="server" Text="追加成绩" OnClick="Button2_Click"   Width="54px" />
              <asp:Button ID="Button3" runat="server" Text="重新导入" OnClick="Button3_Click"  OnClientClick="return confirm('若重新导入，原先记录将全部丢失，是否继续？')" Width="54px" /></td>
        </tr>
      </tbody>
    </table>
       
       </div>
    </form>
</body>
</html>
