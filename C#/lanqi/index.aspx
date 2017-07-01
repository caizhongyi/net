<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="css/a.css" rel="stylesheet" type="text/css" />
<div id="center">
    <div class="center_up">
      <ul>
          <asp:Repeater ID="Repeater1" runat="server">
          <ItemTemplate>
           <li><a href="newList.aspx?id=<%# Eval("id") %>" target="_blank"><%# Eval("type") %></a></li>
          </ItemTemplate>
          </asp:Repeater>
      </ul>
      <style type="text/css">
           .center_up li { padding: 10px 0; height: auto}
      </style>
    </div>
    <div class="center_in">
      <div class="in_left">
      <input id="Hidden1" type="hidden" value=<%=str %> />
          <input id="Hidden2" type="hidden" value=<%=lin %> />
     <script type="text/javascript">
         imgUrl1 = 'images/banner1.jpg';
         imgtext1 = ""
         imgLink1 = escape("Index.aspx");
         imgUrl2 = 'images/banner2.jpg';
         imgtext2 = ""
         imgLink2 = escape("Index.aspx");
         imgUrl3 = 'images/banner3.jpg';
         imgtext3 = ""
         imgLink3 = escape("Index.aspx");
         imgUrl4 = 'images/banner4.jpg';
         imgtext4 = ""
         imgLink4 = escape("Index.aspx");
         imgUrl5 = 'images/banner5.jpg';
         imgtext5 = ""
         imgLink5 = escape("Index.aspx");

         var str = document.getElementById("Hidden1").value;
         var lin = document.getElementById("Hidden2").value;
         var focus_width = 653;
         var focus_height = 409;
         var text_height = 0;
         var swf_height = focus_height + text_height

         var pics = imgUrl1 + "|" + imgUrl2 + "|" + imgUrl3 + "|" + imgUrl4 + "|" + imgUrl5
         var links = imgLink1 + "|" + imgLink2 + "|" + imgLink3 + "|" + imgLink4 + "|" + imgLink5
         var texts = imgtext1 + "|" + imgtext2 + "|" + imgtext3 + "|" + imgtext4 + "|" + imgtext5

         document.write('<object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase="http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0" width="' + focus_width + '" height="' + swf_height + '">');
         document.write('<param name="allowScriptAccess" value="sameDomain"><param name="movie" value="focus.swf"><param name="quality" value="high"><param name="bgcolor" value="#F0F0F0">');
         document.write('<param name="menu" value="false"><param name=wmode value="opaque">');
         document.write('<param name="FlashVars" value="pics=' + str + '&links=' + lin + '&texts=' + texts + '&borderwidth=' + focus_width + '&borderheight=' + focus_height + '&textheight=' + text_height + '">');
         document.write('<embed src="focus.swf" wmode="opaque" FlashVars="pics=' + str + '&links=' + lin + '&texts=' + texts + '&borderwidth=' + focus_width + '&borderheight=' + focus_height + '&textheight=' + text_height + '" menu="false" bgcolor="#F0F0F0" quality="high" width="' + focus_width + '" height="' + focus_height + '" allowScriptAccess="sameDomain" type="application/x-shockwave-flash" pluginspage="http://www.macromedia.com/go/getflashplayer" />'); document.write('</object>');
	 </script>
      </div>
      <div class="in_right">
        <div class="xinwen"><a href="news.html"><img src="images/dx_13.jpg"/></a></div>
        <div class="xinwen2">
          
          <h1>
              &nbsp;<asp:Repeater ID="Repeater2" runat="server">
          <ItemTemplate>
          <p style="text-align:left"><a href="newDetail.aspx?id=<%# Eval("id") %>&&type=advice"><%# fun.Left( Eval("title").ToString(),6) %></a></p>
          </ItemTemplate>
          </asp:Repeater>
          </h1>
        </div>
        <ul>
          <li><a href="newdetail.aspx?type=ggfw"><img src="images/dx_19.jpg" width="165" height="54" border="0"/></a></li>
          <li><a href="liuyan.aspx"><img src="images/dx_21.jpg" width="165" height="55" border="0"/></a></li>
          <li><a href="newdetail.aspx?type=lxwm"><img src="images/dx_23.jpg" width="165" height="55" border="0"/></a></li>
        </ul>
      </div>
      <div class="clr"></div>
      <style type="text/css">
         .huadong .up a{ display:inline-block; padding:10px 40px; font-weight:bold; background:#ddd; border-radius:5px 5px 0 0;}
      </style>
      <div class="in_next">
        <div class="huadong">
          <div class="up">
            <ul>
              <li id="left" onMouseOver="show1()"><a href="javascript:;">琅岐旅游服务</a></li>
              <li id="right" onMouseOver="show2()"><a href="javascript:;">中医养生保健</a></li>
              <li id="right" onMouseOver="show3()"><a href="javascript:;">福州导购</a></li>
            </ul>
          </div>
          <div class="clr"></div>
          <div class="bo1"><img src="images/dx_1_1.jpg" width="20" height="360" /></div>
          <div class="bo2">
            <!-- content1 -->
            <div id="content1">
              <div class="cont_1">
                  <asp:Repeater ID="Repeater3" runat="server">
                  <ItemTemplate>
                  
                   <dl>
                  <dd><a href="<%# Eval("maker_address") %>" target=_blank><img src=<%# Eval("picture") %> border="0" width="150" height="150"/></a></dd>
                  <dt class="dt_1" style=" margin-left:150px;"><a target=_blank href="<%# Eval("maker_address") %>"><%# Eval("name") %></a></dt>
                 
                 <dt class="bt_2"  style=" margin-left:150px;"> <!--现价<%# Eval("newprice") %><span class="ds">元</span>--></dt>
                  </dl >
                  </ItemTemplate>
                  </asp:Repeater>
               
              </div>
              <div class="cont_2"><a href="productList.aspx?sjid=1">更多>></a></div>
            </div>
            <!-- content1 -->


            <!-- content2 -->
            <div id="content2">
              <div class="cont_1">
                            <asp:Repeater ID="Repeater4" runat="server">
                  <ItemTemplate>
                  
                   <dl>
                  <dd><a href="<%# Eval("maker_address") %>" target=_blank><img src=<%# Eval("picture") %> border=0 width=150 height=150/></a></dd>
                  <dt class="dt_1" style=" margin-left:150px;"><a target=_blank href="<%# Eval("maker_address") %>"><%# Eval("name") %></a></dt>
                 
                  <dt class="bt_2"  style=" margin-left:150px;"><!--现价<%# Eval("newprice") %><span class="ds">元</span>--></dt>
                  </dl >
                  </ItemTemplate>
                  </asp:Repeater>
              </div>
              <div class="cont_2"><a href=productList.aspx?sjid=2>更多>></a></div>
            </div>
            <!-- content2 -->
            <!-- content3 -->
            <div id="content3">
              <div class="cont_1">
                <asp:Repeater ID="Repeater5" runat="server">
                  <ItemTemplate>
                  
                   <dl>
                  <dd><a href="<%# Eval("maker_address") %>" target=_blank><img src=<%# Eval("picture") %> border=0 width=150 height=150/></a></dd>
                  <dt class="dt_1" style=" margin-left:150px;"><a target=_blank href="<%# Eval("maker_address") %>"><%# Eval("name") %></a></dt>
                 
                  <dt class="bt_2"  style=" margin-left:150px;"><!--现价<%# Eval("newprice") %><span class="ds">元</span>--></dt>
                  </dl >
                  </ItemTemplate>
                  </asp:Repeater>
              </div>
              <div class="cont_2"><a href=productList.aspx?sjid=3>更多>></a></div>
            </div>
            <!-- content3 -->
          </div>
          <div class="bo3"><img src="images/dx_1_3.jpg" width="14" height="360" /></div>
        </div>
        <!-- advs -->
        <div class="clear"></div>
       
        <div class="advs">
           <asp:Repeater ID="Repeater7" runat="server">
              <ItemTemplate>
                  <dl>
                    <dd><a href="<%# Eval("maker_address") %>" target=_blank><img src=<%# Eval("picture") %> width="154" height="155" border=0 /></a></dd>
                    <dt><a target=_blank href="<%# Eval("maker_address") %>"><%# Eval("name") %></a></dt>
                    <dt class="ds"></dt>
                 </dl>
              </ItemTemplate>
          </asp:Repeater> 
       </div>


      </div>
    </div>
  </div>
  <div class="clr"></div>

  <div class="fenlan" style="margin-top:20px">
      <div class="fenlan-inner clearfix">
          <div class="fenlan_left" style=" float:left; width:49%;  ">
              <a href="http://www.琅岐123.com/newDetail.aspx?id=1871" target="_blank">琅岐商品预约订购</a>
          </div>
          <div class="fenlan_right" style=" float:right; width:50% ; ">
            <a href="http://www.琅岐123.com/newDetail.aspx?id=1975" target="_blank">广告招商</a>
          </div>
     </div>

    <div class="lights">
        <img src="images/light.png" alt=""/><img src="images/light.png" alt=""/><img src="images/light.png"
         alt=""/><img src="images/light.png" alt=""/><img src="images/light.png" alt=""/><img src="images/light.png" alt=""/><img src="images/light.png"
         alt=""/><img src="images/light.png" alt=""/><img src="images/light.png" alt=""/><img src="images/light.png" alt=""/><img src="images/light.png"
         alt=""/><img src="images/light.png" alt=""/><img src="images/light.png" alt=""/><img src="images/light.png" alt=""/><img src="images/light.png"
         alt=""/><img src="images/light.png" alt=""/>
    </div>

    <style type="text/css">
        .hotinfomation , .hotinfomation li{ list-style: none; margin: 0; padding: 0;}
        .hotinfomation { margin-left: -20px;}
        .hotinfomation li { float: left; width: 470px; margin: 10px 0 10px 20px; height: 200px; background: #eee; border: 1px solid #ddd; }
        .hotinfomation li img{ width: 100%; height: 100%;}
    </style>

   

    <ul class="hotinfomation clearfix">
 <asp:Repeater ID="hotProduct" runat="server">
             <ItemTemplate>
               <li><a href="<%# Eval("maker_address") %>"><img src="<%# Eval("picture") %>" alt=""/></a></li>
            </ItemTemplate>
        </asp:Repeater>
        <!--<li><a href=""><img src="images/xxx." alt=""/></a></li>
        <li><a href=""><img src="images/xxx." alt=""/></a></li>
        <li><a href=""><img src="images/xxx." alt=""/></a></li>
        <li><a href=""><img src="images/xxx." alt=""/></a></li>
        <li><a href=""><img src="images/xxx." alt=""/></a></li>
        <li><a href=""><img src="images/xxx." alt=""/></a></li>
        <li><a href=""><img src="images/xxx." alt=""/></a></li>
        <li><a href=""><img src="images/xxx." alt=""/></a></li>
        <li><a href=""><img src="images/xxx." alt=""/></a></li>
        <li><a href=""><img src="images/xxx." alt=""/></a></li>
        <li><a href=""><img src="images/xxx." alt=""/></a></li>
        <li><a href=""><img src="images/xxx." alt=""/></a></li>
        <li><a href=""><img src="images/xxx." alt=""/></a></li>
        <li><a href=""><img src="images/xxx." alt=""/></a></li>
        <li><a href=""><img src="images/xxx." alt=""/></a></li>
        <li><a href=""><img src="images/xxx." alt=""/></a></li>
        <li><a href=""><img src="images/xxx." alt=""/></a></li>
        <li><a href=""><img src="images/xxx." alt=""/></a></li>-->
    </ul>
  
  <div class="section row clearfix">
        <div class="mod mod1 ">
            <div class="hd">
              <div class="tl"></div>
              <div class="tr"><a href="newList.aspx?id=22" class="right">更多</a></div>
              <div class="tc"><h3 class="left">奇文共赏 </h3></div>
            </div>
            <div class="bd">
                 <!--<div  runat="server"  id="HotFirst" class="fisrtinfo"></div>
                  <hr  class="hor_dashed"/>-->
                  <asp:Repeater ID="HotList" runat="server">
                 <ItemTemplate>

                 <p class="box-item ">
                  <span><a href="newdetail.aspx?id=<%# Eval("id") %>">· <%# fun.Left( Eval("title").ToString(),25) %></a></span>
                 </p>

                </ItemTemplate>
                </asp:Repeater>

                <div class="clear"></div>
      	  </div>
          </div>
        <div class="mod mod2 ">
        <div class="hd">
          <div class="tl"></div>
          <div class="tr"><a href="newList.aspx?id=26" class="right">更多</a></div>
          <div class="tc"><h3 class="left">养生保健</h3></div>
        </div>
        <div class="bd">
            <!-- <div  runat="server"  id="TravelFirst" class="fisrtinfo"></div>
              <hr  class="hor_dashed"/>-->
              <asp:Repeater ID="TravelList" runat="server">
             <ItemTemplate>

             <p class="box-item ">
              <span><a href="newdetail.aspx?id=<%# Eval("id") %>">· <%# fun.Left( Eval("title").ToString(),25) %></a></span>
             </p>

            </ItemTemplate>
            </asp:Repeater>
            <div class="clear"></div>

  	  </div>
      </div>
        <div class=" mod mod3">
      <div class="hd">
        <div class="tl"></div>
        <div class="tr"><a href="newList.aspx?id=14" class="right">更多</a></div>
        <div class="tc"><h3 class="left">商品邮购</h3></div>
      </div>
        <div class="bd">
         
           <asp:Repeater ID="CommnuityList" runat="server">
           <ItemTemplate>
    
           <p class="box-item ">
            <span><a href="newdetail.aspx?id=<%# Eval("id") %>">· <%# fun.Left( Eval("title").ToString(),25) %></a></span>
           </p>
      
          </ItemTemplate>
          </asp:Repeater>
          <div class="clear"></div>
          </div>
   
   </div>
 </div>

  
   <!--- 新加的 -->
  
  <div class="clr"></div>
  
  
  <div class="ruzhu">
    <div class="p_1">商家专区</div>
    <div class="ruzhu_2"><a href="productList.aspx?sjid=4&id=24">更多>></a></div>
  </div>
  <p class="center_next1"><img src="images/dx_1_33_1.jpg" width="18" height="448"/></p>
  
  
  
  <div class="center_next2">
      <asp:Repeater ID="Repeater6Repeater6" runat="server">
      <ItemTemplate>
          <dl>
      <dd><a href="<%# Eval("maker_address") %>" target=_blank><img src=<%# Eval("picture") %> width="154" height="155" border=0 /></a></dd>
      <dt><a target=_blank href="<%# Eval("maker_address") %>"><%# Eval("name") %></a></dt>
      
     <%-- <dt class="ds">现价<%# Eval("newprice") %>元</dt>--%>
     <dt class="ds"></dt>
    </dl>
      
      
      </ItemTemplate>
      </asp:Repeater>
  </div>
  <p class="center_next3"><img src="images/dx_1_33_2.jpg" width="18" height="448" /></p>
  <div class="clr"></div>
  
  <div class="guanggao_2" style="padding-bottom:10px; margin-top:10px; height:400px" >

   <div class="p_1" style="margin-top:7px">百货区</div>  <p style="float:right; margin :7px 50px 0 0; font-size:13px; font-weight:bold"><a href="productList.aspx?sjid=4&id=38" target="_blank" style=" color:#FFF">更多>></a></p>
    
 <div class="clr"></div>
 <ul>

     <asp:Repeater ID="Repeater9" runat="server">
     <ItemTemplate>
      <li><a href="<%# Eval("maker_address") %>"><img src=<%# Eval("picture") %> width="190" height="171" /></a></li>
     </ItemTemplate>
     </asp:Repeater>
 </ul>
  </div>

<%--<div class="clr"></div>  
    <div id="next_1"><DIV id="scroll_div">
<div id="scroll_begin">
<table><tr id="mytr">
    <asp:Repeater ID="Repeater7" runat="server">
    <ItemTemplate>
    <td><a href="#"><img src="<%# Eval("picture") %>" width="200" height="137"  ><br><%# fun.Left(Eval("title").ToString(),13) %></a></td>
    
    </ItemTemplate>
    </asp:Repeater>


</tr></table>
</div>
  <div id="scroll_end">
  </div>
</DIV>
  <script>
var speed=20
var scroll_end = document.getElementById("scroll_end");
var scroll_div = document.getElementById("scroll_div");
scroll_end.innerHTML=scroll_begin.innerHTML
function Marquee(){
if(scroll_div.scrollLeft<=0)
scroll_div.scrollLeft+=scroll_begin.offsetWidth
else{
scroll_div.scrollLeft--
}
}
var MyMar=setInterval(Marquee,speed)
scroll_div.onmouseover=function() {clearInterval(MyMar)}
scroll_div.onmouseout=function() {MyMar=setInterval(Marquee,speed)}
</script>
  </div>--%> <div class="clr"></div>
 <div style=" text-align:left; font-size:12px; margin:10px 0;"><b>友情链接</b></div>
<div class="youqing_2">
<ul>
    <asp:Repeater ID="Repeater8" runat="server">
    <ItemTemplate>
    <li><a href="<%# Eval("web_address") %>" target=_blank><%# Eval("web_name") %></a></li>
    </ItemTemplate>
    </asp:Repeater>
    

</ul>


</div>
<br />
<div class="clr"></div>

</asp:Content>

