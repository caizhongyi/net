<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="lxsList.aspx.cs" Inherits="_Default"  %>
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
    <div class="lxs_right_2" style="text-align:center">
    <ul>
   
      
             <asp:Repeater ID="Repeater2" runat="server">
        <ItemTemplate>
           <li>
        <a href="lxs.aspx?id=<%# Eval("id") %>"><img src=<%# Eval("pic") %> width="195" height="172" border=0 /></a>
        <p><a href="lxs.aspx?id=<%# Eval("id") %>"><%# Eval("name") %></a></p>
      </li>
        </ItemTemplate>
        </asp:Repeater>
      
    </ul>
    <div class="clr"></div>
 <div class="zsgg_fy">
 
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
   
  </div>
  <div class="clr"></div>
  
</asp:Content>