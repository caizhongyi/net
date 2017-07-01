<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Top.aspx.cs" Inherits="admin_Top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="css/main.css" rel="stylesheet" type="text/css">
<style type="text/css">
<!--
.STYLE1 {color: #009933}
-->
</style>
<script type="text/javascript">
function writeTime() {
  	// 从对象中获得信息
		var today = new Date();
		var day = today.getDay();
		var month = today.getMonth()+1;
		var year = today.getYear();
		var date = today.getDate();
		if(day  == 1)
		{
			day = "星期一";
		}else if(day == 2){
			day = "星期二";
		}else if(day == 3){
			day = "星期三";
		}else if(day == 4){
			day = "星期四";
		}else if(day == 5){
			day = "星期五";
		}else if(day == 6){
			day = "星期六";
		}else if(day == 0){
			day = "星期日";
		}
 		var hours = today.getHours();
 		var minutes = today.getMinutes();
  	var seconds = today.getSeconds();
		// fixTime 使分和秒可以正常显示
  	// 对于小于10的数字则在该数字前加一个0
	
  	minutes = fixTime(minutes);
  	seconds = fixTime(seconds);
  	//将时间字符串组合在一起并写出
 		var the_time = year + "-" + month +"-" + date + "&nbsp;&nbsp;" + hours + ":" + minutes + ":" + seconds + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + day;
  	//每半秒钟执行一次该函数
  	the_timeout= setTimeout('writeTime();',500);
		document.getElementById('showtime').innerHTML = the_time;
}

function fixTime(the_time) {
	if (the_time <10) { the_time = "0" + the_time; } return the_time;
} 
</script>
</head>
<BODY SCROLL=NO onLoad="writeTime()">
    <form id="form1" runat="server">
   <table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td background="images/head_bg.gif"><table border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="181" rowspan="3"><img src="images/hqht1_01.gif" width="181" height="84"></td>
        <td width="609" style="height: 52px" align=Center></td>
        <td width="238" style="height: 52px" ><img src="images/hqht1_03.gif" width="215" height="52" border="0" usemap="#MapMap"></td>
      </tr>
      <tr>
        <td height="23" colspan="2"><table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td width="79"><img src="images/hqht1_05.gif" width="79" height="23"></td>
              <td width="400"  class="site"></td>
              <td width="40"><img src="images/site_02.gif" width="40" height="23"></td>
              <td ><img src="images/icon_02.gif" width="9" height="9"> 欢迎您!&nbsp;&nbsp;  <span class="STYLE1" id="showtime"></span></td>
            </tr>
        </table></td>
      </tr>
      <tr>
        <td colspan="2" bgcolor="#108CE0" background="images/hqht1_07.gif" style="height: 10px"></td>
      </tr>
    </table>
      <map name="MapMap">
        <area shape="circle" coords="65,29,19" href="#" alt="登录">
        <area shape="circle" coords="125,30,18" href="#" alt="退出">
      </map></td>
  </tr>
</table>
<map name="Map">
<area shape="circle" coords="191,26,23" href="#" alt="退出">
<area shape="circle" coords="128,21,20" href="#" alt="注销">
<area shape="circle" coords="67,30,22" href="#" alt="修改密码">
</map>
    </form>
</BODY>
</html>
