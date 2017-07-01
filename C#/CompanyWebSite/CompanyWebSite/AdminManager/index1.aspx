<%@ Page Language="C#" AutoEventWireup="true" Inherits="index" CodeFile="index1.aspx.cs" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>网站管理</title>
    <link href="css/style.css" type="text/css" rel="stylesheet"/>
    <link href="css/default.css" type="text/css" rel="stylesheet"/>
    <script language="javascript" src="js/menu.js" type="text/javascript"></script>
    <script language="javascript" src="js/BaseClass.js" type="text/javascript"></script>
    <script language="javascript" src="js/DataTimeClock.js" type="text/javascript"></script>
    <style type="text/css">
    
    #TimeClock{ float:right;}
    </style>
</head>
<body onload="javascript:border_left('left_tab1','left_menu_cnt1');">
    <form id="form1" runat="server">
    
 
    
        <table id="indextablebody" cellpadding="0">
            <thead>
                <tr>
                    <th>
                        <div id="logo" title="用户管理后台">
                        </div>
                    </th>
                    <th>
                    <div id="TimeClock"></div>
                    <div style=" float:right;">
                        <a style="color: #16547E">用户 ：<asp:Label ID="LabUserName" runat="server" ></asp:Label></a>&nbsp;&nbsp; <a style="color: #16547E">身份 ：<asp:Label ID="LabRoles" runat="server" ></asp:Label></a>&nbsp;&nbsp;&nbsp;&nbsp;
                        <!--<a href="javascript:window.location.reload()" target="content3">隐藏工作台</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                        <a href="javascript:window.location.reload()" target="content3">管理首页</a>&nbsp;&nbsp;|&nbsp;&nbsp;
                        <a href="help" target="_blank">使用帮助</a>-->
                        </div>
                        
                      <script type="text/javascript">new DataTimeClock({"ContenterObj":document.getElementById ("TimeClock")});</script>  
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class="menu">
                        <ul class="bigbtu">
                            <li id="now01"><asp:LinkButton ID="LoginOut" runat="server" 
                                    onclick="LoginOut_Click">安全退出</asp:LinkButton></li>
                            <li id="now02">
                              <a href="javascript:void(0)" onclick="document.getElementById('content3').src='UserInfo/UserSelf.aspx?id=<%=id %>'" ></a>
                                </li>
                        </ul>
                    </td>
                    <td class="tab">
                        <ul id="tabpage1">
                            <li id="tab1" title="管理首页"><span id="spanTitle">信息管理</span></li>
                        </ul>
                    </td>
                </tr>
                <tr>
                    <td class="t1">
                        <div id="contents">
                            <table cellpadding="0">
                                <tr class="t1">
                                    <td>
                                        <div class="menu_top">
                                        </div>
                                    </td>
                                </tr>
                                <tr class="t2">
                                    <td>
                                        <div id="menu" class="menu">
                                            <ul class="tabpage2">
                                                <li id="left_tab1" title="信息" onclick="javascript:border_left('left_tab1','left_menu_cnt1');"><span>信息</span></li>
                                            </ul>
                                            <div id="left_menu_cnt1" class="left_menu_cnt">
                                                <ul id="dleft_tab1">
                                                
                                                    <li id="now11"><a title="新闻信息" href="News/News.aspx" target="content3"><span>新闻信息</span></a></li>
                                                    <li id="now12"><a title="新闻添加" href="News/NewsEdit.aspx" target="content3"><span>新闻添加</span></a></li>
                                                    <li id="now13"><a title="类别信息" href="NewsType/NewsType.aspx" target="content3"><span>类别信息</span></a></li>
                                                    <li id="now16"><a title="大类别添加" href="NewsType/NewsTypeEdit.aspx?type=big" target="content3"><span>大类别添加</span></a></li>
                                                    <li id="now15"><a title="小类别添加" href="NewsType/NewsTypeEdit.aspx?type=small" target="content3"><span>小类别添加</span></a></li>
                                                  
                                                </ul>
                                            </div>
                                            <div class="clear">
                                            </div>
                                            
                                            <ul class="tabpage2">
                                                <li id="left_tab2" onclick="javascript:border_left('left_tab2','left_menu_cnt2');" title="用户"><span>用户</span></li>
                                            </ul>
                                            <div id="left_menu_cnt2" class="left_menu_cnt">
                                                <ul id="dleft_tab2">
                                                    <li id="now18"><a title="用户信息" href="UserInfo/UserInfo.aspx" target="content3"><span>用户信息</span></a></li>
                                                    <li id="now19"><a title="用户添加" href="UserInfo/UserInfoEdit.aspx" target="content3"><span>用户添加</span></a></li>
                                                     
                                                </ul>
                                            </div>
                                            <div class="clear">
                                            </div>
                                            
                                            <ul class="tabpage2">
                                                <li id="left_tab3" onclick="javascript:border_left('left_tab3','left_menu_cnt3');" title="操作菜单"><span>权限</span></li>
                                            </ul>
                                            <div id="left_menu_cnt3" class="left_menu_cnt">
                                                <ul id="dleft_tab3">
                                                  <!--  <li id="now11"><a title="系统设置" href="#" target="content3"><span>系统设置2</span></a></li>-->
                                                    <li id="now17"><a title="角色信息" href="Roles/Roles.aspx" target="content3"><span>角色信息</span></a></li>
                                                    <li id="now14"><a title="角色添加" href="Roles/RolesEdit.aspx" target="content3"><span>角色添加</span></a></li>
                                                       
                                                </ul>
                                            </div>
                                            <div class="clear">
                                            </div>
                                        </div>
                                        </td>
                                        </tr>
                                        <tr class="t3">
                                            <td>
                                                <div class="menu_end">
                                                </div>
                                            </td>
                                        </tr>
                            </table>
                        </div>
                    </td>
                    <td class="t2">
                        <div id="cnt">
                            <div id="dtab1">
                                <iframe id="content3" name="content3" src="News/News.aspx" frameborder="0"></iframe>
                            </div>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>

        <script>
        //修改标题
        function show_title(str){
	        document.getElementById("spanTitle").innerHTML=str;
        }
        </script>
    </form>
</body>
</html>
