<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="gc.aspx.cs" Inherits="gc" Title="无标题页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link href="css/b.css" rel="stylesheet" type="text/css" />

   <style type="text/css">
   .carlist{clear:both; list-style:none; text-align:center;}
   .carlist li{}
   .carlist li img{ padding:10px; border:1px solid #666; width:800px; height:400px;}
   </style>
  <div id="center" >
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
      
     
   <!-- 宗教百典 -->
   <div class="news"   runat=server id="divNews" style="background-color: #eee890;">
    <div class="neirong" style="text-align:left; font-size:13px" > 
   
   
        <div class="box_1">
            <div style="height:30px;line-height: 30px; padding-left:10px">
                注：<span style="color:#FF0000; margin-left:10px">*为必填选项 <strong>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 在 线 订 车 （福州五区八县）本站预约报名 、 购车可享受优惠价</strong></span>
                <div>
                </div>
            </div>
<!---->
            <div>
                <table border="0" cellpadding="0" cellspacing="0" 
                    style="background-color:#deecf6">
                    <tr>
                        <td>
                            <span style="color:#F00; margin-left:10px">*</span>选择品牌：</td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" 
                                DataSourceID="SqlDataSource1" DataTextField="cname" DataValueField="cid" 
                                onselectedindexchanged="DropDownList1_SelectedIndexChanged">
                          
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span style="color:#F00; margin-left:10px">*</span>选择车系：</td>
                        <td>
                            <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True" 
                                DataSourceID="SqlDataSource2" DataTextField="xname" DataValueField="xid" 
                                onselectedindexchanged="DropDownList2_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span style="color:#F00; margin-left:10px">*</span>选择车型：</td>
                        <td>
                            <asp:DropDownList ID="DropDownList3" runat="server" 
                                DataSourceID="SqlDataSource3" DataTextField="colorname" 
                                DataValueField="colorid">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" width="99%">
                    <tr>
                        <td>
                            <span style="color:#F00; margin-left:10px">*</span>您的姓名：</td>
                        <td>
                            <asp:TextBox ID="txt_username" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <span style="color:#F00; margin-left:10px">*</span>电子邮件：</td>
                        <td>
                            <asp:TextBox ID="txt_email" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span style="color:#F00; margin-left:10px">*</span>移动电话：</td>
                        <td>
                            <asp:TextBox ID="txt_mobile" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <span style="margin-left:16px">家庭电话：</span></td>
                        <td>
                            <asp:TextBox ID="txt_del" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <span style="color:#F00; margin-left:10px">*</span>家庭住址：</td>
                        <td>
                            <asp:TextBox ID="txt_address" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <span style="margin-left:16px">邮编：</span></td>
                        <td>
                            <asp:TextBox ID="txt_code" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 134px">
                                        <span style="color:#F00; margin-left:10px">*</span>是否曾到过展厅：</td>
                                    <td>
                                        <asp:RadioButton ID="r1" runat="server" Text="去过" GroupName="sex" Checked=true />
                                        <asp:RadioButton ID="r2" runat="server" Text="未去过" GroupName="sex"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 134px">
                                        <span style="margin-left:16px">其他要求及买车的具体时间：</span></td>
                                    <td>
                                        <textarea cols="50" name="content" id="txt_cont" rows="8" runat="server"></textarea></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <input id="group_code" name="group_code" type="hidden" 
                    value="c2ea1c3d966616476d74a56cc17c3a69" />
                <input name="return" type="hidden" 
                    value="http://www.faw-vw.com/dealer/faw-vw01.htm" />
                <input id="tmpl" name="tmpl" type="hidden" value="interface" />
                <input id="option" name="option" type="hidden" value="com_nurun_interface" />
                <input id="task" name="task" type="hidden" value="submitOrder" />
                <input id="is_loan" name="is_loan" type="hidden" value="0" />
                <input id="is_accept_info" name="is_accept_info" type="hidden" value="0" />
                <div style="height:35px; line-height:35px; text-align:right; margin-right:20px">
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                        SelectCommand="pro_car_no_SelectAll" SelectCommandType="StoredProcedure">
                    </asp:SqlDataSource>
                    <asp:ImageButton ID="ImageButton1" runat="server"  ImageUrl="images/1.jpg" 
                        onclick="ImageButton1_Click"/>
                  
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                        SelectCommand="pro_car_type_SelectDynamic" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="1=1" Name="WhereCondition" 
                                SessionField="cid" Type="String" />
                            <asp:Parameter DefaultValue="xid" Name="OrderByExpression" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
                        SelectCommand="pro_car_color_SelectDynamic" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="1=1" Name="WhereCondition" 
                                SessionField="xid" Type="String" />
                            <asp:Parameter DefaultValue="colorid" Name="OrderByExpression" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                  
                </div>
            </div>
        </div>
    
  
     </div>
 
   </div>
   
   
   
   
   <!-- 宗教百典 -->

      <ul class="carlist">
    <li><img alt="" src="images/cars/car1.jpg"/></li>
    <li><img alt="" src="images/cars/car2.jpg" /></li>
    <li><img alt="" src="images/cars/car3.jpg" /></li>
    <li><img alt="" src="images/cars/car4.jpg" /></li>
    <li><img alt="" src="images/cars/car5.jpg"/></li>
    <li><img alt="" src="images/cars/car6.jpg"/></li>
    </ul>
    </div>
 
  </div>
  <div class="clr"></div>
  
  
  
  
  <div class="clr"></div>
</asp:Content>

