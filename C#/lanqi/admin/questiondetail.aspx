<%@ Page Language="C#" AutoEventWireup="true" CodeFile="questiondetail.aspx.cs" Inherits="admin_questiondetail" %>

<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>无标题页</title>
    <link href="style/css.css" rel="stylesheet" type="text/css" />
<link href="../style/StyleSheet.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" type="text/css" href="~/index.css" media="all">
</head>
<body style=" text-align:left; height:1000px">
    <div class="Currently" style=" text-align:center">
        网站详细信息</div>
<div class="user_reg">
  <form id="form3"  runat=server>
  <div style="border-left: 1px solid #dceaf7; border-right: 1px solid #dceaf7; display: block; line-height: 80%;" id="menu1">
	  <div style="padding: 10px 5px;">
	    
		 <table class="table" border="0" cellpadding="0" cellspacing="0" width="100%">
			  <tbody>
	       
			  <tr>
				<td class="td_top_2" width="130"><span class="font_red">*</span>&nbsp;网站名称</td>
				<td class="td_left">
				<input id="siteName" name="siteName" value="" class="textfield_2" type="text" runat="server">		     
				   </td>
			  </tr>
			  <tr>
				<td class="td_top_2" width="130"><span class="font_red">*</span>&nbsp;网站域名</td>
				<td class="td_left"> 
				<input id="siteDomain" name="siteDomain" value="http://" class="textfield_1" runat="server" style="width: 180px;" type="text"> 
								</td>
			  </tr>
			  <tr>
				<td class="td_top_2" width="130"><span class="font_red">*</span>&nbsp;日访问量 </td>
				<td class="td_left">
				<input id="dayVisitAmount" name="dayVisitAmount" value="0" class="textfield_2" runat="server" type="text">	
				IP/天
			</td>
			  </tr>
			  <tr>
				<td class="td_top_2" width="130"><span class="font_red">*</span>&nbsp;网站类型</td>
				<td class="td_left">
						<select name="superCatId" id="superCatId"  runat="server">
				
						</select>
                    &nbsp;<span style="margin-left: 10px;">
				 </span>
				 </td>
			  </tr>
			  	  <tr>
				<td class="td_top_2" width="130"><span class="font_red">*</span>&nbsp;网站所有者</td>
				<td class="td_left" id="userName" runat="server">
					
                    &nbsp;<span style="margin-left: 10px;">
				 </span>
				 </td>
			  </tr>
			  <tr>
				<td class="td_top_2" width="130"><span class="font_red">*</span>&nbsp;留言内容</td>
				<td class="td_left">
			
				<textarea id="siteDesc" name="siteDesc" class="textarea_2" style="margin-bottom: 10px; width: 319px; height: 170px;" runat="server"></textarea>
		
				</td>
			  </tr>
			  <tr>
				<td class="td_top_2" width="130"><span class="font_red">*</span>&nbsp;回复内容</td>
				<td class="td_left">
			
				<textarea id="siteRecordDetail" runat="server" name="siteRecordDetail" class="textarea_2" style="margin-bottom: 10px; width: 320px; height: 140px;"></textarea>
		
				</td>
			  </tr>
			  
			  	  <tr>
				<td class="td_top_2" width="130"><span class="font_red">*</span>&nbsp;状态</td>
				<td class="td_left">
						<select name="superCatId" id="Select1"  runat="server">
				
						</select>
                    &nbsp;<span style="margin-left: 10px;">
				 </span>
				 </td>
			  </tr>
	    </tbody></table>
 <p style="text-align: center; margin-top: 30px;">

     <asp:Button ID="Button1" runat="server" Text="回 复"  name="confirm" class="button_2" OnClick="Button1_Click"/>
     <span style="margin-left: 20px;"><input name="back" class="button_2" value="返 回" onclick="" type="button">
   </span>
 </p>		

      </div>
	</div>
</form>
</div>
</body>

</html>