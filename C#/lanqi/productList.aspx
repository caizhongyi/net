	<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="productList.aspx.cs" Inherits="productList"  %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link href="css/b.css" rel="stylesheet" type="text/css" />

 <div class="clr"></div>
  <div class="clr"></div>
  <div class="more_left">
    
      <div class="center_up">
      <ul>
          <asp:Repeater ID="Repeater1" runat="server">
          <ItemTemplate>
           <li><a href="newList.aspx?id=<%# Eval("id") %>" target="_blank"><%# Eval("type") %></a></li>
          </ItemTemplate>
          </asp:Repeater>
      </ul>
      <style type="text/css">
 	   .center_up {width:auto; float:none;}
           .center_up li { padding: 10px 0; height: auto; }
      </style>
    </div>
  </div>
  <div class="More">
  <div style="margin:10px 10px 0 0; float:right">
      选择地域：<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
      </asp:DropDownList></div>
      <div class="clr"></div>
      <asp:Repeater ID="Repeater2" runat="server">
      <ItemTemplate>
      <dl>
      <dd><a target=_blank href="<%# Eval("maker_address") %>"><img src="<%# Eval("picture") %>" width="154" height="155" border=0 /></a></dd><dt><a target=_blank href="<%# Eval("maker_address") %>"><%# fun.Left( Eval("name").ToString(),10) %></a></dt><dt><!--<span class="ds_1">现价<%# Eval("newprice") %>元</span>--></dt></dl >
      
      </ItemTemplate>
      </asp:Repeater>
   
    <div class="clr"></div>
    <br />
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
<div class="clr" style=" clear:both"></div>
</asp:Content>

