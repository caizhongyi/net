<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="newList.aspx.cs" Inherits="newList"  %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link href="css/b.css" rel="stylesheet" type="text/css" />

  <div id="center">
    <div class="center_up">
      <ul>
    <asp:Repeater ID="Repeater1" runat="server">
          <ItemTemplate>
           <li><a href="newList.aspx?id=<%# Eval("id") %>"><%# Eval("type") %></a></li>
          </ItemTemplate>
          </asp:Repeater>
      </ul>
    </div>
    <div class="center_in"  >
      
      <div class="in_next">
        
      </div>
   <!-- 宗教百典 -->
   <div class="news" id=xw runat=server style="height:745px">
    <div class="neirong" style="text-align:left"> 
    <table width=100% >
   
        <asp:Repeater ID="Repeater2" runat="server">
        <ItemTemplate>
        <tr height=20px>
        <td width=80%><a href="newDetail.aspx?id=<%# Eval("id") %>"><%# Eval("title") %></a></td><td><!--<%# Eval("join_date") %>--></td>
        </tr>
        </ItemTemplate>
     </asp:Repeater>
    
    </table>
     <div  style="width:100%; text-align:center"><div class="nextPagesBox" style="text-align: center; width:100%">
           <webdiyer:aspnetpager id="AspNetPager1" AlwaysShow="true" runat="server" cssclass="nextPage" currentpagebuttontextformatstring="<label>{0}</label>"
                        custominfohtml="第%CurrentPageIndex% 页 / 共%PageCount%页" custominfosectionwidth=""
                        enabletheming="true" firstpagetext="<span>首页</span>" lastpagetext="<span>末页</span>"
                        nextpagetext="<span>下一页</span>" onpagechanged="AspNetPager1_PageChanged" pagesize="1"
                        pagingbuttonspacing="" prevpagetext="<span>上一页</span>" showcustominfosection="Left"
                        showpageindexbox="Always" Font-Size="12px" TextAfterPageIndexBox="  页  " 
                        TextBeforePageIndexBox="跳到第 "  BorderStyle="NotSet" 
                        ></webdiyer:aspnetpager></div></div>
     </div>
 
   </div>
   
      <div class="news" id=DivNews runat=server visible="false" style="height:745px">
    <div class="neirong" style="text-align:left"> 
    <table width=100% style="font-size:small">
   
        <asp:Repeater ID="Repeater3" runat="server">
        <ItemTemplate>
        <tr height="20px">
        <td width="80%"><a href="syDetail.aspx?id=<%# Eval("id") %>"><%# Eval("class1") %></a></td><td></td>
        </tr>
        </ItemTemplate>
     </asp:Repeater>
    
    </table>
   
     </div>
 
   </div>
   
   
   
   
   <!-- 宗教百典 --><div class="zongjiao" id=zj runat=server visible="false">
  <dl>
  <dd><a href=newList.aspx?sjid=6><img src="images/zongjiao_01.jpg" width="197" height="331" /></a></dd>
  <dt><a href="#">道教</a></dt>
  
  
  </dl>
  
  <dl>
  <dd><a href="newList.aspx?sjid=7"><img src="images/zongjiao_03.jpg" width="197" height="331" /></a></dd>
  <dt><a href="#">基督教</a></dt>
  
  
  </dl>
   
   
   <dl>
  <dd><a href="newList.aspx?sjid=8"><img src="images/zongjiao_02.jpg" width="197" height="331" /></a></dd>
  <dt><a href="#">佛教</a></dt>
  
  
  </dl>
   
   
   <dl>
  <dd><a href="newList.aspx?sjid=9"><img src="images/zongjiao_04.jpg" width="197" height="331" /></a></dd>
  <dt><a href="#">天主教</a></dt>
  
  
  </dl>

   </div>
   
      <!-- yuandi --><div class="yuandi" id=yd runat=server visible="false">
   <ul>
   <li><a href="newList.aspx?sjid=1"><img src="images/xxyd_1.jpg" width="162" height="364" /></a></li>
   <li><a href="newList.aspx?sjid=2"><img src="images/xxyd_2.jpg" width="162" height="364" /></a></li>
   <li><a href="newList.aspx?sjid=3"><img src="images/xxyd_3.jpg" width="162" height="364" /></a></li>
   <li><a href="newList.aspx?sjid=4"><img src="images/xxyd_4.jpg" width="162" height="364" /></a></li>
   <li><a href="newList.aspx?sjid=5"><img src="images/xxyd_5.jpg" width="162" height="364" /></a></li>
   
   
   </ul>
   
   
   
   
   
   
   </div><!-- yuandi -->
   
   
   
   
   
   
    </div>
  </div>
  <div class="clr"></div>
  
  
  
  
  <div class="clr"></div>
</asp:Content>

