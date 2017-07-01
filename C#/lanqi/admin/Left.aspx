<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Left.aspx.cs" Inherits="Left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
    <link href="css/main.css" rel="stylesheet" type="text/css">
    <script type="text/javascript">
//隐藏所有菜单

function hideAll() { 
  for(i=0;i <odiv.length;i++) { 
    odiv[i].style.display="none"; 
  } 
}  

//显示下拉菜单
var beforeMenuId = 0;
function showSubMenu(num){ 
		var beforeMenu = document.getElementById('odiv' + beforeMenuId);

		if(beforeMenuId == num){
			if(beforeMenu.style.display == 'none'){
				beforeMenu.style.display = 'inline';
			}else{
				beforeMenu.style.display = 'none';
			}
		}else{
			if(beforeMenu.style.display != 'none'){
					beforeMenu.style.display = 'none';
			}
	
			var odiv = document.getElementById('odiv' + num);
		  if (odiv.style.display=='none'){ 
		   		odiv.style.display='inline'; 
		 	} else { 
		    	odiv.style.display='none'; 
		  } 
		}
		
  	beforeMenuId = num;
}

//改变导航文字
function showNavigator(html){
		var navtext = top.document.frames["topWin"].document.getElementById('navtext');
		navtext.innerHTML = html;
}
</script>
</head>
<body style="background-color:#6FB8FC">
    <form id="form1" runat="server">
    <table width="100%" height="100%" border="0" cellpadding="0" cellspacing="0" class="menu">
  <tr>
    <td valign="top"><table border="0" cellspacing="1" cellpadding="0">
      <tr>
        <td height="5"></td>
      </tr>

      <tr>
        <td height="25px" background="images/Login1_r12_c15.jpg"  style="cursor:hand; " onClick="javascript:showSubMenu(0);" width="167" >管理员</td>
      </tr>
      <tr id="odiv0" style="display:none">
        <td class="submenu">
            <div class="menut2"><img src="images/icon_22.gif"> <a href="adminManager.aspx" target="bodyWin" >&nbsp;管理员列表</a></div>
              <div class="menut2"><img src="images/icon_22.gif"> <a href="adminAdd.aspx" target="bodyWin" >&nbsp;添加管理员</a></div>    
               
                </td>
      </tr>
       <tr>
        <td height="25px" background="images/Login1_r12_c15.jpg"  style="cursor:hand; " onClick="javascript:showSubMenu(9);" width="167" >会员管理</td>
      </tr>
      <tr id="odiv9" style="display:none">
        <td class="submenu">
            <div class="menut2"><img src="images/icon_22.gif"/> <a href="UserManager.aspx"target="bodyWin" >&nbsp;会员列表</a></div>
              <div class="menut2"><img src="images/icon_22.gif"/> <a href="UserAdd.aspx" target="bodyWin" >&nbsp;添加会员</a></div>    
                <div class="menut2"><img src="images/icon_22.gif"/> <a href="usernote.aspx" target="bodyWin" >&nbsp;在线订车管理</a></div>    
                <div class="menut2"><img src="images/icon_22.gif"/> <a href="car.aspx" target="bodyWin" >&nbsp;汽车品牌管理</a></div>   
                <div class="menut2"><img src="images/icon_22.gif"/> <a href="car_type.aspx" target="bodyWin" >&nbsp;汽车型号管理</a></div>   
                 <div class="menut2"><img src="images/icon_22.gif"/> <a href="car_color.aspx" target="bodyWin" >&nbsp;汽车颜色管理</a></div> 
                </td>
      </tr>
     <tr>
        <td height="25px" background="images/Login1_r12_c15.jpg"  style="cursor:hand; " onClick="javascript:showSubMenu(1);" width="167" >产品管理</td>
      </tr>
         <tr id="odiv1" style="display:none">
        <td class="submenu" >
        
        <div class="menut2"><img src="images/icon_22.gif"> <a href="ProductAdd2.aspx"target="bodyWin" >&nbsp;产品添加</a></div>
        <div class="menut2"><img src="images/icon_22.gif"> <a href="user_productList2.aspx"target="bodyWin" >&nbsp;产品管理</a></div>
         <div class="menut2"><img src="images/icon_22.gif"> <a href="productclass2.aspx"target="bodyWin" >&nbsp;产品分类</a></div>
          <div class="menut2"><img src="images/icon_22.gif"> <a href="productclass1.aspx"target="bodyWin" >&nbsp;产品地域</a></div>
  

        	
      
        	</td>
       	</tr>

     
      
      <tr>
        <td background="images/Login1_r12_c15.jpg" style="cursor:hand" onClick="javascript:showSubMenu(2);" width="167" height="25px">网站信息</td>
      </tr>
      <tr id="odiv2" style="display:none">
        <td class="submenu" >
        	
        	
        	
        	<div class="menut2"><img src="images/icon_22.gif"> <a href=questionmanager.aspx target="bodyWin" >&nbsp;查看留言</a></div>
        	<div class="menut2"><img src="images/icon_22.gif"> <a href=about.aspx target="bodyWin" >&nbsp;广告服务</a></div>
        	<div class="menut2"><img src="images/icon_22.gif"> <a href=contact.aspx target="bodyWin" >&nbsp;联系我们</a></div>
        	<div class="menut2"><img src="images/icon_22.gif"> <a href=siteMessage.aspx target="bodyWin" >&nbsp;设置网站</a></div>
        	<div class="menut2"><img src="images/icon_22.gif"> <a href=link.aspx target="bodyWin" >&nbsp;友情链接</a></div>
        	</td>
       	</tr>
   
       	       	      <tr>
        <td background="images/Login1_r12_c15.jpg" style="cursor:hand" onClick="javascript:showSubMenu(6);" width="167" height="25px">旅游风景</td>
      </tr>
      <tr id="odiv6" style="display:none">
        <td class="submenu" >
        	
        	
        	<div class="menut2"><img src="images/icon_22.gif"> <a href="culturemanager.aspx" target="bodyWin" >&nbsp;风景列表</a></div>
        	<div class="menut2"><img src="images/icon_22.gif"> <a href="culture.aspx" target="bodyWin" >&nbsp;添加风景</a></div>
        	<div class="menut2"><img src="images/icon_22.gif"> <a href="culturetype.aspx" target="bodyWin" >&nbsp;分类管理</a></div>
        	</td>
       	</tr>
       	  	       	      <tr>
        <td background="images/Login1_r12_c15.jpg" style="cursor:hand" onClick="javascript:showSubMenu(7);" width="167" height="25px">信息文章管理</td>
      </tr>
      <tr id="odiv7" style="display:none">
        <td class="submenu" >
        	
        	
        	<div class="menut2"><img src="images/icon_22.gif"> <a href="newmanager2.aspx" target="bodyWin" >&nbsp;信息文章列表</a></div>
        	<div class="menut2"><img src="images/icon_22.gif"> <a href="newadd2.aspx" target="bodyWin" >&nbsp;添加信息文章</a></div>
        	<div class="menut2"><img src="images/icon_22.gif"> <a href="newclass2.aspx" target="bodyWin" >&nbsp;信息文章分类</a></div>
        	<div class="menut2"><img src="images/icon_22.gif"> <a href="sytype.aspx" target="bodyWin" >&nbsp;摄影类型管理</a></div>
        	<div class="menut2"><img src="images/icon_22.gif"> <a href=ygfcManager.aspx target="bodyWin" >&nbsp;旅行广告招商</a></div>
        	</td>
       	</tr>
       	       	  	       	      <tr>
        <td background="images/Login1_r12_c15.jpg" style="cursor:hand" onClick="javascript:showSubMenu(8);" width="167" height="25px">广告管理</td>
      </tr>
      <tr id="odiv8" style="display:none">
        <td class="submenu" >
        	
        	
         	<div class="menut2"><img src="images/icon_22.gif"> <a href=flashPicManager.aspx target="bodyWin" >&nbsp;广告图片</a></div>
        	<div class="menut2"><img src="images/icon_22.gif"> <a href=flashPicAdd.aspx target="bodyWin" >&nbsp;添加广告</a></div>
        	</td>
       	</tr>

             <td background="images/Login1_r12_c15.jpg" style="cursor:hand" onClick="javascript:showSubMenu(10);" width="167" height="25px">留言管理</td>
      </tr>
      <tr id="odiv10" style="display:none">
        <td class="submenu" >
         	<div class="menut2"><img src="images/icon_22.gif"> <a href=Comments.aspx target="bodyWin" >&nbsp;留言管理</a></div>
        	</td>
       	</tr>
         
      <tr>
        <td background="images/Login1_r12_c15.jpg" style="cursor:hand" onClick="javascript:showSubMenu(3);" width="167" height="25px">公告管理</td>
      </tr>
      <tr id="odiv3" style="display:none">
        <td class="submenu">
        	
        	<div class="menut2"><img src="images/icon_22.gif"> <a href="advice.aspx" target="bodyWin" >&nbsp;公告列表</a></div>
        	<div class="menut2"><img src="images/icon_22.gif"> <a href="adviceadd.aspx" target="bodyWin" >&nbsp;公告添加</a></div>
        
        	
        	
        	
        
        	
    
        	</td>
       	    </tr>
       	    
    
      
      

    </table></td>
  </tr>
</table>
    </form>
</body>
</html>
