<%@ Page Language="C#" AutoEventWireup="true" CodeFile="xiugaichengji.aspx.cs" Inherits="admin_xiugaichengji" %>

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
     
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
            <td align="center" class="table1" colspan="2">
                &nbsp;学生成绩修改</td>
          </tr>
        
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="right" style="width: 50%">
              姓名：</td><td align="left" class="table1" ><input name="pwd" type=text class="formr" id="pwd" runat=server style="width: 123px" /></td>
          </tr>
      <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="right" style="width: 50%">
              学号：</td><td align="left" class="table1" ><input name="pwd" type=text class="formr" id="Text1" runat=server style="width: 123px" /></td>
          </tr>
            <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="right" style="width: 50%">
              身份证：</td><td align="left" class="table1" ><input name="pwd" type=text class="formr" id="Text2" runat=server style="width: 123px" /></td>
          </tr>
       
   
        <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td align="right" style="height: 30px; width: 50%;">
              年级专业：</td>
          <td align="left" class="table1" style="height: 30px" >
              <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" >
              </asp:DropDownList></td>
        </tr>
      <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td align="right" style="height: 30px; width: 50%;">
              课程名称：</td>
          <td align="left" class="table1" style="height: 30px" >
              <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
              </asp:DropDownList></td>
        </tr>
               <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
          <td height="30" align="right" style="width: 50%">
              课程成绩：</td><td align="left" class="table1" ><input name="pwd" type=text class="formr" id="Text3" runat=server style="width: 123px" /></td>
          </tr>
           <tr align="center" bgcolor="#ffffff" onmouseover="this.style.backgroundColor='#ECF4F9'" onmouseout="this.style.backgroundColor='#fff'">
            <td align="center" class="table1" colspan="2" style="height: 29px">
                &nbsp;<asp:Button ID="Button1" runat="server" Text="提交" OnClick="Button1_Click" />&nbsp;</td>
        </tr>
    </table>
       
       </div>
    </form>
</body>
</html>
