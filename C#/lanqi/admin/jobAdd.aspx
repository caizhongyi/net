<%@ Page Language="C#" AutoEventWireup="true" CodeFile="jobAdd.aspx.cs" Inherits="admin_jobAdd" validateRequest=false %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
      <link href="../style/StyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
 <form id="form1" runat="server">
    
      <table  cellspacing="0" cellpadding="0" width="100%" >
      
          <tr>
            <td valign="top" ><br />
                <table width="800" border="1" bordercolor="#6699cc" align="center" cellpadding="2" cellspacing="1">
                  <tbody>
                    <tr>
                      <td height="25" bgcolor="#6699cc" style="width: 800px"><div align="center"><strong>发布招聘信息 <br />
                      </strong></div></td>
                    </tr>
                    <tr>
                      <td style="width: 800px; height: 414px;"><div align="center">
                          <table cellspacing="1" cellpadding="0" width="100%" border="0">
                            <tbody>
                              <tr bgcolor="#ecf5ff">
                                <td width="19%" height="25"><div align="center">岗位职称</div></td>
                                <td width="81%" align="left">
                                    <asp:TextBox ID="TextBox_inviter" runat="server"></asp:TextBox></td>
                              </tr>
                              <tr bgcolor="#ecf5ff">
                                <td height="22"><div align="center">需求人数</div></td>
                                <td align="left">
                                    <asp:TextBox ID="TextBox_shu" runat="server" Width="36px"></asp:TextBox>&nbsp; 人</td>
                              </tr>
                              <tr bgcolor="#ecf5ff">
                                <td height="22"><div align="center">工作地点</div></td>
                                <td height="-4" align="left">
                                    <asp:TextBox ID="TextBox_address" runat="server"></asp:TextBox></td>
                              </tr>
                              <tr bgcolor="#ecf5ff">
                                <td height="22"><div align="center">薪资水平</div></td>
                                <td height="-1" align="left" bgcolor="#ecf5ff">
                                    <asp:TextBox ID="TextBox_deal" runat="server"></asp:TextBox></td>
                              </tr>
                          
                              <tr bgcolor="#ecf5ff">
                                <td height="22"><div align="center">
                                    到期时间</div></td>
                                <td height="0" align="left">
                                    <asp:TextBox ID="TextBox_qixian" runat="server" Width="122px"></asp:TextBox>
                                    &nbsp;&nbsp;</td>
                              </tr>
                              <tr bgcolor="#ecf5ff">
                                <td height="22"><div align="center">素质需求</div></td>
                                <td height="10" align="left">
                                    <fckeditorv2:fckeditor id="FCKeditor1" runat="server" basepath="../CnxihuFCKeditorbj/" Height="250px" ></fckeditorv2:fckeditor>
                                </td>
                              </tr>
                              <tr bgcolor="#ecf5ff">
                                <td colspan="2" height="25"><div align="center">
                                    <asp:Button ID="Button1" runat="server" Text="提交" OnClick="Button1_Click" />&nbsp;
                                    <input id="Reset1" type="reset" value="取消" onclick="return Reset1_onclick()" />
                                    </div></td>
                              </tr>
                            </tbody>
                        </table>
                      </div></td>
                    </tr>
                  </tbody>
              </table>
              <br />
              <br /></td>
          </tr>
        
      </table>
   
    </form>
</body>
</html>
