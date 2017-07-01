<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="fengJi.aspx.cs" Inherits="fengJi" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link href="css/b.css" rel="stylesheet" type="text/css" />
<style>
*{margin:0; padding:0}
.clear{clear:both;height:0;overflow:hidden;}
#box .neirong table a{ font-size:14px;}
#box .neirong table tr{ border-bottom:1px dashed #ccc; line-height:25px;}
</style>


<script type="text/javascript" src="js/myfocus-1.1.0.min.js"></script><!--引入myFocus库-->
<script type="text/javascript" src="js/mF_fscreen_tb.js"></script><!--引入风格应用js-->
<link id="mf-css" rel="stylesheet" href="css/mF_fscreen_tb.css" /><!--引入风格应用css-->
<script type="text/javascript">
myFocus.set({
    id:'myFocus-wrap',//焦点图盒子ID
    pattern:'mF_fscreen_tb',//风格应用的名称
    time:3,//切换时间间隔(秒)，省略设置即不自动切换
    width:750,//设置宽度(主图区)
    height:563//设置高度(主图区)
});



</script>
 <div id="center">
   <div class="center_up">
      <ul>
    <asp:Repeater ID="Repeater2" runat="server">
          <ItemTemplate>
           <li><a href="newList.aspx?id=<%# Eval("id") %>"><%# Eval("type") %></a></li>
          </ItemTemplate>
          </asp:Repeater>
      </ul>
    </div>
    <div class="center_in_2">
    
     <div class="news" id=xw runat=server>
    <div class="neirong" style="text-align:left"> 
    <table width=100% >
   
        <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
        <tr height=20px>
        <td width=80%><a href="fjDetail.aspx?id=<%# Eval("id") %>" target="_blank"><%# Eval("title") %></a></td><td><%# Eval("join_date") %></td>
        </tr>
        </ItemTemplate>
     </asp:Repeater>
    
    </table>
    
       <div  style="width:100%; text-align:center"><div class="nextPagesBox" style="text-align: center; width:100%">
           <webdiyer:aspnetpager id="AspNetPager1" runat="server" cssclass="nextPage" currentpagebuttontextformatstring="<label>{0}</label>"
                        custominfohtml="第%CurrentPageIndex% 页 / 共%PageCount%页" custominfosectionwidth=""
                        enabletheming="true" firstpagetext="<span>首页</span>" lastpagetext="<span>末页</span>"
                        nextpagetext="<span>下一页</span>" onpagechanged="AspNetPager1_PageChanged" pagesize="1"
                        pagingbuttonspacing="" prevpagetext="<span>上一页</span>" showcustominfosection="Left"
                        showpageindexbox="Always" Font-Size="12px" TextAfterPageIndexBox="  页  " 
                        TextBeforePageIndexBox="跳到第 "  BorderStyle="NotSet" 
                        ></webdiyer:aspnetpager></div></div>
     </div>
      
 
   </div>
                        
                        <div id="lunbo" style="margin:30px 0 0 25px; z-index:0;" runat=server visible=false>
<!-- 轮播 -->
  <div id="myFocus-wrap" style="padding-top:135px">
    <div id="myFocus"><!--焦点图盒子-->
     <!--载入画面-->
      <ul class="pic"><!--内容列表-->
   <asp:Repeater ID="Repeater3" runat="server">
          <ItemTemplate>
            <li ><a href="#"><img src=<%# Eval("picture") %> width="750" height="563" thumb="" alt='<%# Eval("title") %>' text='<%# Eval("content") %>'  /></a></li>
          </ItemTemplate>
          </asp:Repeater>
      </ul>
    </div>
  </div>



<!-- 轮播 -->


</div>

  <div  style="width:100%; text-align:center"><div class="nextPagesBox" style="text-align: center; width:100%">
           <webdiyer:aspnetpager id="AspNetPager2" runat="server" cssclass="nextPage" currentpagebuttontextformatstring="<label>{0}</label>"
                        custominfohtml="第%CurrentPageIndex% 页 / 共%PageCount%页" custominfosectionwidth=""
                        enabletheming="true" firstpagetext="<span>首页</span>" lastpagetext="<span>末页</span>"
                        nextpagetext="<span>下一页</span>" onpagechanged="AspNetPager2_PageChanged" pagesize="1"
                        pagingbuttonspacing="" prevpagetext="<span>上一页</span>" showcustominfosection="Left"
                        showpageindexbox="Always" Font-Size="12px" TextAfterPageIndexBox="  页  " 
                        TextBeforePageIndexBox="跳到第 "  BorderStyle="NotSet" 
                        ></webdiyer:aspnetpager></div></div>
     

    </div>
  </div>
  <div class="clr"></div>
  
</asp:Content>

