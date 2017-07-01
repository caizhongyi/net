﻿<%@ Page Language="C#" MasterPageFile="admin.master" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="admin_default" Title="无标题页" ValidateRequest="false" %>
<%@ Import Namespace="Loachs.Common" %>
<%@ Import Namespace="Loachs.Entity" %>
<%@ Import Namespace="Loachs.Business" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h2>管理中心</h2>
<%=ResponseMessage%>
<div  class="right">
    <h4>重新统计数据</h4>
    
    <p><asp:Button ID="btnCategory" runat="server"  onclick="btnCategory_Click"  Text="重新统计分类文章数" Width="180" Font-Bold="false" /></p>
    <p><asp:Button ID="btnTag" runat="server"  onclick="btnTag_Click"  Text="重新统计标签文章数"   Width="180" Font-Bold="false"  /></p>
    <p><asp:Button ID="btnUser" runat="server"  onclick="btnUser_Click"  Text="重新统计作者文章和评论数" Width="180"   Font-Bold="false" /></p>
    <p class="notice">小提示:这些操作比较耗时.</p>
</div>
<div class="left" >
    <table width="100%">
        <tr class="category">
            <td>最新待审核评论</td>
            <td ></td>
        </tr>
       <%foreach (CommentInfo comment in commentlist)
         { %>
                <tr class="row">
                   
                    <td colspan="2" >
                     <span style="float:right;">
                        <a href="commentlist.aspx?operate=update&commentid=<%=comment.CommentId%>">审核</a>
                        <a href="commentlist.aspx?operate=delete&commentid=<%=comment.CommentId%>" onclick="return confirm('确定要删除吗?')">删除</a>   
                     </span>
                        [<%=comment.AuthorLink%>] <span style="cursor:help;" title="<%=comment.Content%>"><%=StringHelper.CutString( StringHelper.RemoveHtml(comment.Content ) ,50,"...")%></span>
                   
                    </td>
                </tr>
        <%} %>
        <%if (commentlist.Count == 0)
          { %>
        <tr class="rowend">
                   
                    <td colspan="2" >
                     暂无待审核评论
                    </td>
                </tr>
        <%} %>
        
        <tr class="category">
            <td>网站信息</td>
            <td style="width:70%;"></td>
        </tr>
        <tr class="row">
            <td>文章:</td>
            <td><%= StatisticsManager.GetStatistics().PostCount %> 篇</td>
        </tr>
        <tr class="row">
            <td>评论:</td>
            <td><%=StatisticsManager.GetStatistics().CommentCount %> 条</td>
        </tr>
        
        <tr class="row">
            <td>标签:</td>
            <td><%=StatisticsManager.GetStatistics().TagCount %> 个</td>
        </tr>
         <tr class="row">
            <td>访问量:</td>
            <td><%=StatisticsManager.GetStatistics().VisitCount %> 次</td>
        </tr>
        
        <tr class="row">
            <td>数据库:</td>
            <td><%=DbPath %> (<%=DbSize%>)</td>
        </tr>
        <tr class="row">
            <td>附件:</td>
            <td><%=UpfilePath %> (共<%=UpfileCount%> 个, <%=UpfileSize%>)</td>
        </tr>
         <tr class="row">
            <td>程序目录:</td>
            <td><%= Request.PhysicalApplicationPath%></td>
        </tr>
        <tr class="rowend">
            <td>程序版本:</td>
            <td><%= setting.Version %> <a href="http://www.loachs.com" target="_blank">去官网查看新版</a></td>
        </tr>
        <tr class="category">
            <td>服务器信息</td>
            <td ></td>
        </tr>       
         <tr class="row">
            <td>CPU:</td>
            <td><%=Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER") %> (<%=Environment.ProcessorCount%> 核)</td>
        </tr>
         <tr class="row">
            <td>操作系统:</td>
            <td><%=Environment.OSVersion %></td>
        </tr>
         <tr class="row">
            <td>.NET 版本:</td>
            <td><%=Environment.Version%></td>
        </tr>
         <tr class="row">
            <td>IIS 版本:</td>
            <td><%=Request.ServerVariables["SERVER_SOFTWARE"] %></td>
        </tr>
        <tr class="rowend">
            <td>服务器IP:</td>
            <td><%=Request.ServerVariables["LOCAl_ADDR"]%></td>
        </tr>
        
       
    </table>
</div>
</asp:Content>

