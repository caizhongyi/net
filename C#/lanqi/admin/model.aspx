<%@ Page Language="C#" AutoEventWireup="true" CodeFile="model.aspx.cs" Inherits="admin_model" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link id="mastercss" rel="stylesheet" href="images/style.css" type="text/css" media="screen" />
<link href="~/images/edit/edit.css" rel="stylesheet" type="text/css" />
</head>
<body style="height:1500px">
    <form id="form1" runat="server">
                        <div class="colorarea01">

<table cellspacing="0" cellpadding="0" width="100%"  class="maintable" style="text-align:left">

<tr id="tr_subject">
<th>姓名*</th>
<td><input name="subject" type="text" id="subject" size="60" value="" runat=server /></td>
</tr>

<tr id="tr_subjectimage">
<th>个人照片</th>
<td id=geren runat=server>
    &nbsp;</td>
</tr>

	<tr id="tr_catid">
<th>性别*</th>
<td><select name="catid" id="catid"  runat=server>

<option value="男性">男性</option>
<option value="女性">女性</option>
</select>
</td>
</tr>

<tr id="tr_message"><th>爱好/特长/演出经历</th><td><table style="width: 100%"><tr><td style="text-align:left"><textarea name="join_phone1" style="width:98%;" rows="10" id="join_phone1" runat=server  ></textarea>
			
</td></tr></table></td></tr></table>

</div>

<div class="colorarea02">

<table cellspacing="0" cellpadding="0" width="100%"  class="maintable" style=" text-align:left">

<tr id="tr_join_agree">
<th>您是否同意将本资料信息放于网络<span style="color: #F00">*</span><p></p></th>
<td><input runat=server id=j1 name="join_agree" type="radio" value="同意" checked />同意&nbsp;&nbsp;<input runat=server id=j2 name="join_agree" type="radio" value="不同意" />不同意&nbsp;&nbsp;</td>
</tr>

<tr id="tr_join_type">
<th>能够做哪种类型的模特<span style="color: #F00">*</span><p></p></th>
<td><span id=leixing runat=server></span></td>
</tr>

<tr id="tr_join_size">
<th>身高/体重/三围/鞋码<span style="color: #F00">*</span><p>长度在100个字符之内.</p></th>
<td><textarea name="join_size" style="width:98%;" rows="10" id="join_size" runat=server></textarea></td>
</tr>

<tr id="tr_join_phone">
<th>联系电话/QQ号/电子邮件<span style="color: #F00">*</span><p></p></th>
<td><textarea name="join_phone" style="width:98%;" rows="10" id="join_phone" runat=server></textarea></td>
</tr>

<tr id="tr_join_job">
<th>职业<span style="color: #F00">*</span><p>长度在20个字符之内.</p></th>
<td><input name="join_job" type="text" id="join_job" size="60" value="" runat=server /></td>
</tr>

<tr id="tr_join_show">
<th>比赛经历<span style="color: #F00">*</span><p>长度在200个字符之内.</p></th>
<td><textarea name="join_show" style="width:98%;" rows="10" id="join_show" runat=server>参加过何种比赛或演出</textarea></td>
</tr>

<tr id="tr_join_train">
<th>专业培训经历<span style="color: #F00">*</span><p>长度在200个字符之内.</p></th>
<td><textarea name="join_train" style="width:98%;" rows="10" id="join_train" runat=server>受过培训嘛？ 是否接受培训？</textarea></td>
</tr>

<tr id="tr_join_photo1">
<th>生活照<p></p></th>
<td id=shenghuo runat=server>
    &nbsp;</td>
</tr>

<tr id="tr_join_photo2">
<th>艺术照<p></p></th>
<td id=yishu runat=server>
    &nbsp;</td>
</tr>

</table>

</div>
    </form>
</body>
</html>
