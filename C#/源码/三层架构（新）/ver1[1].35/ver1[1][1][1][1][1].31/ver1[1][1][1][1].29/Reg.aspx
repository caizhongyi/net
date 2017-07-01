<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reg.aspx.cs" Inherits="Reg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Panel ID="AgreePanel" runat="server" Height="50px" Width="125px">
    <div>
        &nbsp;&nbsp;
        <table width="816" height="200" border="0" align="center" cellpadding="0" cellspacing="0">
          <tr>
            <td height="20" bgcolor="#CCCCCC"><div align="center"><strong>同意协议书</strong></div></td>
          </tr>
          <tr>
            <td height="161" bgcolor="#FFFFFF">同意吧</td>
          </tr>
          <tr>
            <td ><div align="center"><asp:Button ID="Button1" runat="server" Text="同意" OnClick="Button1_Click" /></div></td>
          </tr>
        </table>
    </div>
        </asp:Panel>
        <asp:Panel ID="RegPanel" runat="server" Height="221px" Width="815px" Visible="False">
          <table width="820" height="115" border="0" align="center" cellpadding="0" cellspacing="0">
            <tr>
              <td valign="top"><table width="100%" height="22" border="0" cellpadding="0" cellspacing="0">
                <tr>
                  <td width="100%" bgcolor="#CECFCE"><strong>基本信息</strong></td>
                </tr>
              </table>
                <table width="100%" border="0" cellspacing="1" cellpadding="1">
                  <tr>
                    <td width="29%" bgcolor="#EFEFEF" style="height: 26px">用户名：</td>
                    <td width="71%" style="height: 26px">&nbsp;<asp:TextBox ID="Username" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Username"
                            ErrorMessage="请输入用户名"></asp:RequiredFieldValidator></td>
                  </tr>
                  <tr>
                    <td bgcolor="#EFEFEF" style="height: 26px">密码：</td>
                    <td style="height: 26px">&nbsp;<asp:TextBox ID="Password" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Password"
                            ErrorMessage="请输入密码"></asp:RequiredFieldValidator></td>
                  </tr>
                  <tr>
                    <td bgcolor="#EFEFEF" style="height: 26px">确认密码：</td>
                    <td style="height: 26px">&nbsp;<asp:TextBox ID="ConfirmPassword" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ConfirmPassword"
                            ErrorMessage="请输入确认密码"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="Password"
                            ControlToValidate="ConfirmPassword" ErrorMessage="密码与确认密码必须一致"></asp:CompareValidator></td>
                  </tr>
                  <tr>
                    <td bgcolor="#EFEFEF">E-mail：</td>
                    <td>&nbsp;<asp:TextBox ID="Email" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Email"
                            ErrorMessage="请输入邮箱地址"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Email"
                            ErrorMessage="请输入正确格式的邮箱地址" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
                  </tr>
                  <tr>
                    <td bgcolor="#EFEFEF">呢称：</td>
                    <td>&nbsp;<asp:TextBox ID="NaturalName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="NaturalName"
                            ErrorMessage="请输入呢称"></asp:RequiredFieldValidator></td>
                  </tr>
                </table>
                <table width="100%" height="22" border="0" cellpadding="0" cellspacing="0">
                  <tr>
                    <td width="100%" bgcolor="#CECFCE"><strong>高级信息</strong></td>
                  </tr>
                </table>
                <table width="100%" border="0" cellspacing="1" cellpadding="1">
                  <tr>
                    <td width="29%" bgcolor="#EFEFEF" style="height: 26px">地址：</td>
                    <td width="71%" style="height: 26px">&nbsp;
                        <asp:TextBox ID="Address" runat="server"></asp:TextBox></td>
                  </tr>
                  <tr>
                    <td bgcolor="#EFEFEF" style="height: 17px">地址1：</td>
                    <td style="height: 17px">&nbsp;
                        <asp:TextBox ID="Address1" runat="server"></asp:TextBox></td>
                  </tr>
                  <tr>
                    <td bgcolor="#EFEFEF" style="height: 26px">地址2：</td>
                    <td style="height: 26px">&nbsp;
                        <asp:TextBox ID="Address2" runat="server"></asp:TextBox></td>
                  </tr>
                  <tr>
                    <td bgcolor="#EFEFEF">城市：</td>
                    <td>&nbsp;
                        <asp:TextBox ID="City" runat="server"></asp:TextBox></td>
                  </tr>
                  <tr>
                    <td bgcolor="#EFEFEF">省份：</td>
                    <td>&nbsp;
                        <asp:TextBox ID="Provice" runat="server"></asp:TextBox></td>
                  </tr>
                  <tr>
                    <td bgcolor="#EFEFEF">邮编：</td>
                    <td><asp:TextBox ID="Zip" runat="server"></asp:TextBox></td>
                  </tr>
                  <tr>
                    <td bgcolor="#EFEFEF">国家：</td>
                    <td><asp:TextBox ID="Country" runat="server"></asp:TextBox></td>
                  </tr>
                  <tr>
                    <td bgcolor="#EFEFEF">电话：</td>
                    <td><asp:TextBox ID="Telphone" runat="server"></asp:TextBox></td>
                  </tr>
                  <tr>
                    <td bgcolor="#EFEFEF">手机：</td>
                    <td><asp:TextBox ID="Mobile" runat="server"></asp:TextBox></td>
                  </tr>
                  <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;<asp:Button ID="Button2" runat="server" Text="确认" OnClick="Button2_Click" /></td>
                  </tr>
                </table></td>
            </tr>
          </table>
          </asp:Panel>
    </form>
</body>
</html>
