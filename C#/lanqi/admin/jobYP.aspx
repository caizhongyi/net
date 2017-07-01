<%@ Page Language="C#" AutoEventWireup="true" CodeFile="jobYP.aspx.cs" Inherits="jobYP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
     <link href="../style/StyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="height:1200px">
    <form id="form1" runat="server">
    <div>
    <table height="144" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tbody>
          <tr>
            
            <td align="left" valign="top" class="xiabian"><table class="just" cellspacing="0" bordercolordark="#ffffff" cellpadding="1" 
      width="100%" align="center" bordercolorlight="#cccccc" border="1">
                    <tr>
                      <td align="middle" bgcolor="#e8e8e8" height="7">姓名</td>
                      <td align="left" width="124" height="7"><input id="txtName" style="border:0; WIDTH: 80px;" name="txtName" runat="server"/></td>
                      <td align="middle" bgcolor="#e8e8e8" height="7">性别</td>
                      <td align="left" width="143" height="7"><input id="Gender" style="border:0; WIDTH: 50%;" name="Gender" runat="server"/> </td>
                      <td width="78" height="7" align="right" bgcolor="#e8e8e8">出生年月</td>
                      <td align="left" height="7" style="width: 290px">
					  <select id="ddlyear" style="WIDTH: 55px" name="ddlyear" runat="server">
                          <option value="1950">1950</option>
                          <option value="1951">1951</option>
                          <option value="1952">1952</option>
                          <option value="1953">1953</option>
                          <option value="1954">1954</option>
                          <option value="1955">1955</option>
                          <option value="1956">1956</option>
                          <option value="1957">1957</option>
                          <option value="1958">1958</option>
                          <option value="1959">1959</option>
                          <option value="1960">1960</option>
                          <option value="1961">1961</option>
                          <option value="1962">1962</option>
                          <option value="1963">1963</option>
                          <option value="1964">1964</option>
                          <option value="1965">1965</option>
                          <option value="1966">1966</option>
                          <option value="1967">1967</option>
                          <option value="1968">1968</option>
                          <option value="1969">1969</option>
                          <option value="1970">1970</option>
                          <option value="1971">1971</option>
                          <option value="1972">1972</option>
                          <option value="1973">1973</option>
                          <option value="1974">1974</option>
                          <option value="1975">1975</option>
                          <option value="1976">1976</option>
                          <option value="1977">1977</option>
                          <option value="1978">1978</option>
                          <option value="1979">1979</option>
                          <option value="1980">1980</option>
                          <option value="1981">1981</option>
                          <option value="1982">1982</option>
                          <option value="1983">1983</option>
                          <option value="1984">1984</option>
                          <option value="1985">1985</option>
                          <option value="1986">1986</option>
                          <option value="1987">1987</option>
                          <option value="1988">1988</option>
                          <option value="1989" >1989</option>
                          <option value="1990">1990</option>
                          <option value="1991">1991</option>
                          <option value="1992">1992</option>
                          <option value="1993">1993</option>
                          <option value="1994">1994</option>
                          <option value="1995">1995</option>
                          <option value="1996">1996</option>
                          <option value="1997">1997</option>
                          <option value="1998">1998</option>
                          <option value="1999">1999</option>
                          <option value="2000">2000</option>
                          <option value="2001">2001</option>
                          <option value="2002">2002</option>
                          <option value="2003">2003</option>
                          <option value="2004">2004</option>
                          <option value="2005">2005</option>
                          <option value="2006">2006</option>
                          <option value="2007">2007</option>
                          <option value="2008">2008</option>
                          <option value="2009">2009</option>
                      </select>
                        年
                        <select id="ddlmonth" style="WIDTH: 40px" name="ddlmonth" runat="server">
                          <option value="01" >01</option>
                          <option value="02">02</option>
                          <option value="03">03</option>
                          <option value="04">04</option>
                          <option value="05">05</option>
                          <option value="06">06</option>
                          <option value="07">07</option>
                          <option value="08">08</option>
                          <option value="09">09</option>
                          <option value="10">10</option>
                          <option value="11">11</option>
                          <option value="12">12</option>
                        </select>
                        月
                        <select id="ddlday" style="WIDTH: 40px" name="ddlday" runat="server">
                          <option value="01" selected="selected">01</option>
                          <option value="02">02</option>
                          <option value="03">03</option>
                          <option value="04">04</option>
                          <option value="05">05</option>
                          <option value="06">06</option>
                          <option value="07">07</option>
                          <option value="08">08</option>
                          <option value="09">09</option>
                          <option value="10">10</option>
                          <option value="11">11</option>
                          <option value="12">12</option>
                          <option value="13">13</option>
                          <option value="14">14</option>
                          <option value="15">15</option>
                          <option value="16">16</option>
                          <option value="17">17</option>
                          <option value="18">18</option>
                          <option value="19">19</option>
                          <option value="20">20</option>
                          <option value="21">21</option>
                          <option value="22">22</option>
                          <option value="23">23</option>
                          <option value="24">24</option>
                          <option value="25">25</option>
                          <option value="26">26</option>
                          <option value="27">27</option>
                          <option value="28">28</option>
                          <option value="29">29</option>
                          <option value="30">30</option>
                          <option value="31">31</option>
                        </select>
                        日</td>
                    </tr>
                    <tr>
                      <td align="center" bgcolor="#e8e8e8">籍贯</td>
                      <td valign="center" align="left">

					  <input id="txtFrom" style="border:0; WIDTH: 80px;" name="txtFrom" runat="server"/></td>
                      <td  align="right" width="40" bgcolor="#e8e8e8">&nbsp;&nbsp;婚否</td>
                      <td valign="center" align="left" width="143">
					  <input id="Marry" style="border:0; WIDTH: 50%;" name="Marry" runat="server"/>
					 </td>
                      <td valign="center" align="center" bgcolor="#e8e8e8">身份证</td>
                      <td valign="center" align="left" style="width: 290px">
					  <input id="txtIdentyCard" name="txtIdentyCard" runat="server"/></td>
                    </tr>
                    <tr>
                      <td align="center" bgcolor="#e8e8e8">身高/体重</td>
                      <td align="left" colspan="3"><input id="txtcSA" style="border:0; WIDTH: 99%;" name="txtcSA" runat="server"/></td>
                      <td align="center" bgcolor="#e8e8e8">联系电话</td>
                      <td valign="center" align="left" style="width: 290px">
					  <input id="txtTelephone" style="border:0; WIDTH: 90%;" name="txtTelephone" runat="server"/></td>
                    </tr>
                    <tr>
                      <td align="center" bgcolor="#e8e8e8">居住地址</td>
                      <td valign="center" align="left" colspan="3">
					  <input id="txtAddress" style="border:0; WIDTH: 99%;" name="txtAddress" runat="server"/></td>
                      <td valign="center" align="center" bgcolor="#e8e8e8">Email地址</td>
                      <td valign="center" align="left" style="width: 290px">
					  <input id="txtEmail" style="border:0; WIDTH: 90%;" name="txtEmail" runat="server"/></td>
                    </tr>
                    <tr>
                      <td align="center" bgcolor="#e8e8e8">最高学历</td>
                      <td valign="center" align="left" colspan="3">
					  <select id="ddlStudyProcess" name="ddlStudyProcess" runat="server">
                          <option value="大专">大专</option>
                          <option value="本科">本科</option>
                          <option value="硕士">硕士</option>
                          <option value="博士">博士</option>
                          <option value="其它">其它</option>
                      </select></td>
                      <td valign="center" align="center" bgcolor="#e8e8e8">专业</td>
                      <td valign="center" align="left" style="width: 290px">
					  <input id="txtMajor" style="border:0; WIDTH: 90%;" name="txtMajor" runat="server"/></td>
                    </tr>
                    <tr>
                      <td align="center" bgcolor="#e8e8e8">毕业学校</td>
                      <td valign="center" align="left" colspan="3">
					  <input id="txtGraduateSchool" style="border:0; WIDTH: 92%;" name="txtGraduateSchool" runat="server"/></td>
                      <td valign="center" align="center" bgcolor="#e8e8e8">毕业时间</td>
                      <td valign="center" align="left" style="width: 290px">
					  <select id="ddlGraduateyear" style="WIDTH: 55px" name="ddlGraduateyear" runat="server">
                          <option value="1950">1950</option>
                          <option value="1951">1951</option>
                          <option value="1952">1952</option>
                          <option value="1953">1953</option>
                          <option value="1954">1954</option>
                          <option value="1955">1955</option>
                          <option value="1956">1956</option>
                          <option value="1957">1957</option>
                          <option value="1958">1958</option>
                          <option value="1959">1959</option>
                          <option value="1960">1960</option>
                          <option value="1961">1961</option>
                          <option value="1962">1962</option>
                          <option value="1963">1963</option>
                          <option value="1964">1964</option>
                          <option value="1965">1965</option>
                          <option value="1966">1966</option>
                          <option value="1967">1967</option>
                          <option value="1968">1968</option>
                          <option value="1969">1969</option>
                          <option value="1970">1970</option>
                          <option value="1971">1971</option>
                          <option value="1972">1972</option>
                          <option value="1973">1973</option>
                          <option value="1974">1974</option>
                          <option value="1975">1975</option>
                          <option value="1976">1976</option>
                          <option value="1977">1977</option>
                          <option value="1978">1978</option>
                          <option value="1979">1979</option>
                          <option value="1980">1980</option>
                          <option value="1981">1981</option>
                          <option value="1982">1982</option>
                          <option value="1983">1983</option>
                          <option value="1984">1984</option>
                          <option value="1985">1985</option>
                          <option value="1986">1986</option>
                          <option value="1987">1987</option>
                          <option value="1988">1988</option>
                          <option value="1989">1989</option>
                          <option value="1990">1990</option>
                          <option value="1991">1991</option>
                          <option value="1992">1992</option>
                          <option value="1993">1993</option>
                          <option value="1994">1994</option>
                          <option value="1995">1995</option>
                          <option value="1996">1996</option>
                          <option value="1997">1997</option>
                          <option value="1998">1998</option>
                          <option value="1999">1999</option>
                          <option value="2000">2000</option>
                          <option value="2001">2001</option>
                          <option value="2002">2002</option>
                          <option value="2003">2003</option>
                          <option value="2004">2004</option>
                          <option value="2005">2005</option>
                          <option value="2006">2006</option>
                          <option value="2007">2007</option>
                          <option value="2008">2008</option>
                          <option value="2009" >2009</option>
                      </select>
                        年
                        <select id="ddlGraduatemonth" style="WIDTH: 40px" name="ddlGraduatemonth" runat="server">
                          <option value="01" >01</option>
                          <option value="02">02</option>
                          <option value="03">03</option>
                          <option value="04">04</option>
                          <option value="05">05</option>
                          <option value="06">06</option>
                          <option value="07">07</option>
                          <option value="08">08</option>
                          <option value="09">09</option>
                          <option value="10">10</option>
                          <option value="11">11</option>
                          <option value="12">12</option>
                        </select>
                        月
                        <select id="ddlGraduateday" style="WIDTH: 40px" name="ddlGraduateday" runat="server">
                          <option value="01" >01</option>
                          <option value="02">02</option>
                          <option value="03">03</option>
                          <option value="04">04</option>
                          <option value="05">05</option>
                          <option value="06">06</option>
                          <option value="07">07</option>
                          <option value="08">08</option>
                          <option value="09">09</option>
                          <option value="10">10</option>
                          <option value="11">11</option>
                          <option value="12">12</option>
                          <option value="13">13</option>
                          <option value="14">14</option>
                          <option value="15">15</option>
                          <option value="16">16</option>
                          <option value="17">17</option>
                          <option value="18">18</option>
                          <option value="19">19</option>
                          <option value="20">20</option>
                          <option value="21">21</option>
                          <option value="22">22</option>
                          <option value="23">23</option>
                          <option value="24">24</option>
                          <option value="25">25</option>
                          <option value="26">26</option>
                          <option value="27">27</option>
                          <option value="28">28</option>
                          <option value="29">29</option>
                          <option value="30">30</option>
                          <option value="31">31</option>
                        </select>
                        日</td>
                    </tr>
                    <tr>
                      <td align="center" bgcolor="#e8e8e8">应聘岗位</td>
                      <td valign="center" align="left" colspan="5">
					   <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
					</td>
                    </tr>
                    <tr>
                      <td align="center" bgcolor="#e8e8e8">接受外派</td>
                      <td valign="center" align="left" colspan="5">
					  <input id="WaiPai" style="border:0; WIDTH: 50%;" name="WaiPai" runat="server"/>
					 </td>
                    </tr>
                    <tr>
                      <td align="center" bgcolor="#e8e8e8" height="4">薪金要求</td>
                      <td valign="center" align="left" colspan="5" height="4"><input onkeypress="return(tbIntKeepInt());" id="txtDemandSalary" 
            onkeyup="ontbIntKeyUp(this);" style="border:0; WIDTH: 70px;" maxlength="6" onchange="ontbIntKeyUp(this);" name="txtDemandSalary"  runat="server"/>元/月</td>
                    </tr>
                    <tr>
                      <td align="middle" bgcolor="#e8e8e8" colspan="6">主要项目/任务经历（包括担任的角色；项目期间；项目所使用工具：操作系统、数据库及数据库开发语言、编程软件、网络开发工具等；熟练程度等）</td>
                    </tr>
                    <tr>
                      <td valign="center" align="left" colspan="6"><textarea id="txtInfo" style="border:0;WIDTH: 99%; HEIGHT: 194px;" name="txtInfo" rows="10" runat="server"></textarea></td>
                    </tr>
                    <tr>
                      <td align="middle" bgcolor="#e8e8e8" colspan="6">自我介绍及工作经历</td>
                    </tr>
                    <tr>
                      <td valign="center" align="left" colspan="6"><textarea id="txtProfile" style="border:0; WIDTH: 99%; " name="txtProfile" rows="10" runat="server"></textarea></td>
                    </tr>
                    <tr>
                      <td align="middle" colspan="6" height="30">
                         <input name="btnBack" type="button" id="btnBack" onClick="javascript:history.go(-1)"  value="返 回"></td>
                    </tr>
              </table></td>
          </tr>
        </tbody>
      </table>
    </div>
        
    </form>
</body>
</html>
