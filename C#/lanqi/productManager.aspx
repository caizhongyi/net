<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="productManager.aspx.cs" Inherits="productManager"%>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link href="css/b.css" rel="stylesheet" type="text/css" />
 <div class="clr"></div>
  <div class="guanli">
       <h2>您好，<%=username %>  <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">退出登陆</asp:LinkButton></h2>
    <!-- gl_left -->
    <div class="gl_left">
      <div class="huiyuan_1">
        <dl>
          <dd><%=username %></dd><dt>您好！欢迎光临东海旅游购物网。</dt><dt>会员级别：<%=xueli %></dt></dl>
      </div>
      <p class="p_1">商品管理</p>
      <div class="dingdan"><a href="productManager.aspx">暂存架</a></div>
      <p class="p_1">个人信息管理</p>
      <div class="dingdan_2">
        <p><a href="userInfo.aspx">编辑个人档案</a></p>
        <p><a href="pwdupdate.aspx">修改密码</a></p>
      </div>
    </div>
    <!-- gl_left -->
    <!-- gl_right -->
    <div class="gl_right">
      <p class="ddd_2">帐户管理 > 暂存架  <a href=productAdd.aspx>添加产品</a></p>
    
     <table width=100% style="font-size:13px">
     <tr><td>商品名称</td><td>分类</td><td>价格</td><td>链接</td><td>修改</td></tr>
         <asp:Repeater ID="Repeater1" runat="server">
         <ItemTemplate>
         <tr><td><%# Eval("name") %></td><td><%# Eval("type") %></td><td><%# Eval("newprice") %></td><td><%# Eval("maker_address") %></td><td><a href="productAdd.aspx?id=<%# Eval("pid") %>">修改</a></td></tr>
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
</asp:Content>

