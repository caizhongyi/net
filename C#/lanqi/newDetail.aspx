<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="newDetail.aspx.cs" Inherits="newDetail" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="css/b.css" rel="stylesheet" type="text/css" />
<div style="display:none">
<embed  src="xyxy.mp3"  loop=true  autostart=true  name=bgss  width="0"  height=0 id=divmusic >  
</embed>

</div>

<input type=hidden id=txtpic value=<%=pic %> />
<script>
var m=document.getElementById("divmusic");
m.src=document.getElementById("txtpic").value;
</script>
  <div id="center" >
    
    <!--<div class="center_up">
      <ul>
    <asp:Repeater ID="Repeater1" runat="server">
          <ItemTemplate>
           <li><a href="newList.aspx?id=<%# Eval("id") %>"><%# Eval("type") %></a></li>
          </ItemTemplate>
          </asp:Repeater>
      </ul>
    </div>-->

    <div class="newsdetail" >
         <div class="liebiao" >
   <a href="/index.aspx">首页</a> -- <span id=liebiao runat=server></span>
   
   
   </div>
     
   <!-- 宗教百典 -->
   <div class="news"   runat="server" id="divNews">
    <div class="neirong" > 

     <div class="left" style="width:700px;">
     <asp:Repeater ID="Repeater2" runat="server">
     <ItemTemplate>
     <div class="newsdetail-title"> <h3  ><%# Eval("title") %> </h3>
     <p  class="newsdetail-remark"><!--点击数：<%# Eval("hot") %>&nbsp; &nbsp;上传时间：<%# Eval("join_date") %>--></p>
     </div><p class="p_1_2"><%# Eval("content") %></p> </div>
    </ItemTemplate>
     </asp:Repeater>
       <asp:Repeater ID="Repeater3" runat="server">
        <ItemTemplate>
    <strong><p class="p_1_1" style=" color:#008080"><%# Eval("title") %> </p></strong>
     <p class="p_1_2"><%# Eval("content") %></p>
     </div>
        </ItemTemplate>
     </asp:Repeater>
     </div>

     <asp:Repeater ID="Repeater4" runat="server">
          <HeaderTemplate><ul class="right center" style="margin-top:70px;"></HeaderTemplate>
          <ItemTemplate>
           <a href="<%# Eval("web_address") %>" target="_blank"><img alt="" src="<%# Eval("pic") %>" style="width:200px; height:130px; padding:5px; border:1px solid #ccc;"/></a>
           <div><%# Eval("web_name")%></div>
          </ItemTemplate>
          <FooterTemplate></ul></FooterTemplate>
     </asp:Repeater>
    
     <div class="clear"></div>
      
       
    
      
  
     </div>
 <div class="newsdetail-footer"><center> <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">上一篇</asp:LinkButton>     <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">下一篇</asp:LinkButton></div> 
   </div>
   
   
   
   
   <!-- 宗教百典 -->
        
  <!-- <div class="comments">
   <div class="comments-bar">评论</div>
   <div  runat="server" id="Comments"></div>
   <ul>
   <asp:Repeater ID="CommentsList" runat="server">
   <ItemTemplate>
    <li>
    <div class="comments-left"><%# Eval("username") %>&nbsp;说:</div>
    <div class="comments-right"><%# Eval("c_content") %></div>
    </li>
    </ItemTemplate>
    </asp:Repeater>
    </ul>

    <dl class="output">
    <dt>我要评论:</dt>
    <dd><textarea style="height: 182px; width: 600px" id="comentsArea" runat="server"></textarea></dd>
    <dd> <asp:Button runat="server" Text="提交" onclick="Unnamed1_Click"></asp:Button></dd>
    </dl>
 
   </div>-->


</div>
   
   
   
   
   
    </div>
  
  <div class="clr"></div>
  
  
<script type="text/javascript">
    //$(document).ready(function () {
    $(".newsdetail a").attr("target", "_blank");

    //  });
   
</script>
  
  
  <div class="clr"></div>
</asp:Content>

