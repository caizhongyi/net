<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>网吧后台管理系统</title>

<style type="text/css">
<!--

a:link {
	text-decoration: none;
	color: #009999;
}
a:visited {
	text-decoration: none;
	color: #996600;
}
a:hover {
	text-decoration: none;
	color: #006699;
}
a:active {
	text-decoration: none;
	color: #00CCCC;
}
-->
</style>
    <script type="text/javascript">
    function ChangeUrl(url)
    {   
       
        var Ifram =document.getElementById("Ifra");
        if(url=="AdIssue")
        {
           Ifram.src="ADManger/AdIssue.aspx";
        }
        else if(url=="AdManger")
        {
          Ifram.src="ADManger/AdManger.aspx";
        }
        else if(url=="AdPrices")
        {
          Ifram.src="ADManger/AdPrices.aspx";
        }
        else if(url=="AdType")
        {
          Ifram.src="ADManger/AdType.aspx";
        }
        else   if(url=="AeraManger")
        {
          Ifram.src="AeraManger/AeraManger.aspx";
        }
        else   if(url=="ProvinceManger")
        {
          Ifram.src="AeraManger/ProvinceManger.aspx";
        }
        else   if(url=="RolesManger")
        {
          Ifram.src="UserManger/RolesManger.aspx";
        }
         else  if(url=="UserManger")
        {
         Ifram.src="UserManger/UserManger.aspx";
        }
         else  if(url=="WbManger")
        {
          Ifram.src="WbManger/WbManger.aspx";
        }  
       
    }
    </script>
    
    <link rel="stylesheet" rev="stylesheet" href="js/resources/css/ext-all.css" type="text/css" media="all" />
    <link href="App_Themes/pager.css" rel="stylesheet" type="text/css" />
    <link href="App_Themes/allStyle.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="js/adapter/ext/ext-base.js"></script> 
    <script type="text/javascript" src="js/ext-all.js"></script>
    <script type ="text/javascript" src="js/ext-lang-zh_CN.js"></script>
    
    <script type="text/javascript" src="Javascript/Loading.js"></script>
    <script type="text/javascript" src="Javascript/Navigation.js"></script>

    
</head>
<body >
<div style=" width:1024;">
  <form id="Form1" runat ="server">
  <div style=" float:left">
     
       <div class="pageleft">
        <asp:Panel ID="AdminPanel" runat="server" Width="183px">
            <ul id="UlMainNav" class="mainNav">
            <li class="mainNav_List" onclick="MainNavClick1(this);"  style="background-image:url(Images/bar1.gif);background-repeat: no-repeat">   
              
               <a href="#">用户管理</a>
                <ul class="subNav" style="display: none;" >
                    <li class="subNav_List"  style="background-image:url(Images/subbar.gif);background-repeat: no-repeat" onclick="ChangeUrl('RolesManger');"><a href="#">角色管理</a>
                    </li><li class="subNav_List"  style="background-image:url(Images/subbar.gif);background-repeat: no-repeat" onclick="ChangeUrl('UserManger');"><a href="#">帐号管理</a></li></ul>
               
                </li>
                
                 <li class="mainNav_List"  style=" background-image:url(Images/line.gif); background-repeat:no-repeat;">
                 <ul class="subNav" style="display: none">
                 <li class="subNav_List" ></li></ul>
                 </li>
               

            <li class="mainNav_List" onclick="MainNavClick1(this);" style="background-image:url(Images/bar.gif);background-repeat: no-repeat"><a href="#">广告管理</a>
                <ul class="subNav" style="display: none">
                    <li class="subNav_List" style="background-image:url(Images/subbar.gif);background-repeat: no-repeat" onclick="ChangeUrl('AdIssue');"><a href="#">广告发布</a></li>
                    <li class="subNav_List" style="background-image:url(Images/subbar.gif);background-repeat: no-repeat" onclick="ChangeUrl('AdManger');"><a href="#">广告管理</a></li>
                    <li class="subNav_List" style="background-image:url(Images/subbar.gif);background-repeat: no-repeat" onclick="ChangeUrl('AdType');"><a href="#">广告类型</a></li>
                    <li class="subNav_List" style="background-image:url(Images/subbar.gif);background-repeat: no-repeat" onclick="ChangeUrl('AdPrices');"><a href="#">广告价格</a></li></ul>  
            </li>
           
             <li class="mainNav_List"  style=" background-image:url(Images/line.gif); background-repeat:no-repeat" >
                 <ul class="subNav" style="display: none">
                 <li class="subNav_List" ></li></ul>
                 </li>
              
               <li class="mainNav_List" onclick="MainNavClick1(this);" style="background-image:url(Images/bar.gif);background-repeat: no-repeat"><a href="#">网吧管理</a>
                <ul class="subNav" style="display: none">
                    <li class="subNav_List" style="background-image:url(Images/subbar.gif);background-repeat: no-repeat" onclick="ChangeUrl('WbManger');"><a href="#">网吧信息管理</a></li>
                   </ul>
                
            </li>
            
                <li class="mainNav_List"  style=" background-image:url(Images/line.gif); background-repeat:no-repeat">
                 <ul class="subNav" style="display: none">
               <li class="subNav_List" ></li></ul>
                 </li>
              
              
          
           
        </ul>
        
         </asp:Panel>
          <asp:Panel ID="WbPanel" runat="server" Width="181px">
           <ul id="Ul1" class="mainNav">
           <li class="mainNav_List" onclick="MainNavClick1(this);" style="background-image:url(Images/bar.gif);background-repeat: no-repeat"><a href="#">网吧操作</a>
                <ul class="subNav" style="display: none">
                    <li class="subNav_List" style="background-image:url(Images/subbar.gif);background-repeat: no-repeat" onclick="ChangeUrl('AdManger');"><a href="#">广告查询</a></li></ul>
               
                     <li class="mainNav_List"  style=" background-image:url(Images/line.gif); background-repeat:no-repeat">
                 <ul class="subNav" style="display: none">
               <li class="subNav_List" ></li></ul>
                 </li>
               
                    <li class="mainNav_List" onclick="MainNavClick1(this);" style="background-image:url(Images/bar.gif);background-repeat: no-repeat"><a href="#">个人信息管理</a>
                  <ul class="subNav" style="display: none">
                    <li class="subNav_List" style="background-image:url(Images/subbar.gif);background-repeat: no-repeat" onclick="ChangeUrl('UserManger');"><a href="#">个人信息修改</a></li><li class="subNav_List" style="background-image:url(Images/subbar.gif);background-repeat: no-repeat"><a href="#">个人信息查看</a></li><li class="subNav_List" style="background-image:url(Images/subbar.gif);background-repeat: no-repeat"><a href="#">修改</a><a href="#">密码</a></li></ul>
                
            </li>
            
             <li class="mainNav_List"  style=" background-image:url(Images/line.gif); background-repeat:no-repeat">
                 <ul class="subNav" style="display: none">
                   <li class="subNav_List"></li></ul>
                 </li>
                 </ul>
          </asp:Panel>
          
           <asp:Panel ID="ClientPanel" runat="server" Width="181px">
           
           
               <ul id="Ul2" class="mainNav">
               <li class="mainNav_List" onclick="MainNavClick1(this);" style="background-image:url(Images/bar.gif);background-repeat: no-repeat"><a href="#">客户操作</a>
                <ul class="subNav" style="display: none">

                    <li class="subNav_List" style="background-image:url(Images/subbar.gif);background-repeat: no-repeat" onclick="ChangeUrl('WbAdMessage');"><a href="#">广告查询</a></li></ul>  
                 
                   <li class="mainNav_List"  style=" background-image:url(Images/line.gif); background-repeat:no-repeat">
                 <ul class="subNav" style="display: none">
                   <li class="subNav_List"></li></ul>
                 </li>
                 
                    <li class="mainNav_List" onclick="MainNavClick1(this);" style="background-image:url(Images/bar.gif);background-repeat: no-repeat"><a href="#">个人信息管理</a>
                  <ul class="subNav" style="display: none">
                    <li class="subNav_List" style="background-image:url(Images/subbar.gif);background-repeat: no-repeat" onclick="ChangeUrl('UserManger');"><a href="#">个人信息修改</a></li><li class="subNav_List" style="background-image:url(Images/subbar.gif);background-repeat: no-repeat"><a href="#">个人信息查看</a></li><li class="subNav_List" style="background-image:url(Images/subbar.gif);background-repeat: no-repeat"><a href="#">修改</a><a href="#">密码</a></li></ul>
                
            </li>
                </ul>
           </asp:Panel>
            <ul id="Ul4" class="mainNav">
            <li class="mainNav_List"   style=" background-image:url(Images/line.gif); background-repeat:no-repeat">
            
                 <ul class="subNav" style="display: none">
                 <li class="subNav_List" ></li></ul>
              <ul id="Ul3" class="mainNav">
            <li class="mainNav_List" onclick="MainNavClick(this)" >
                <asp:HyperLink ID="HlLoginOut" runat="server" NavigateUrl="~/index.aspx" style="background-image:url(Images/subbar.gif);background-repeat: no-repeat" Width="179px">用户注销</asp:HyperLink>
            </li>
           
     
             
              <li class="mainNav_List"   style=" background-image:url(Images/line.gif); background-repeat:no-repeat">
                 <ul class="subNav" style="display: none">
                 <li class="subNav_List" ></li></ul>
                 </li>
               </ul> 
            
                 </li>
      </div>
     </div>
      <div>
      <iframe id="Ifra"  style="height: 722px; width: 1001px;" width="1200"></iframe>
      </div>
        <div class="pagefoot" style=" width:100%; height: 100px">
        <div></div>
     
          

</div> 
  </form>
  </div>
</body>
</html>
