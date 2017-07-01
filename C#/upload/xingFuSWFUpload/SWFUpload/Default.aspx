<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SWFUpload._Default" %>

<%@ Register Src="SWFUpload/UC_SWFUpload.ascx" TagName="UC_SWFUpload" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SWFUpload Demo</title>
    <link href="/SWFUpload/css/swfupload.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="/SWFUpload/js/swfupload.js"></script>

    <script type="text/javascript" src="/SWFUpload/js/swfupload.queue.js"></script>

    <script type="text/javascript" src="/SWFUpload/js/handlers.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div style="text-align: center;">
        <h2>
            SWFUpload上传控件 Beta1</h2>
    </div>
    <br />
    <div style="border: #d6d6d6 1px solid;">
        <h5>
            Author:ZhangPan<br />
            Date:2010-05-18<br />
            Link:www.cnblogs.com/immensity<br />
            Email:15811350125@163.com<br />
            注意:本用户控件为测试版,大家使用中如遇到什么问题可以随时提出,以后会制作成自定义控件,谢谢
        </h5>
    </div>
    <div style="border-bottom: #d6d6d6 1px solid;">
    <h3>
        列表上传模式</h3>
    <uc1:UC_SWFUpload ID="UC_SWFUpload1" runat="server" />
    <br />
    <br />
    <br />
    文件路径集合:<asp:ListBox ID="lbPath" runat="server" Height="100px" Width="245px"></asp:ListBox>
    <br />
    文&nbsp;件&nbsp;名&nbsp;集&nbsp;合:<asp:ListBox ID="lbName" runat="server" Height="100px"
        Width="245px"></asp:ListBox>
    <br />
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="获取文件路径和文件名" OnClick="Button1_Click" />
    <br />
    <br />
    
    </div>
    <br />
    <br />
    <h3>
        按钮上传模式</h3>
    <uc1:UC_SWFUpload ID="UC_SWFUpload2" runat="server" />
    <br />
    <br />
    <br />
    文件路径集合:<asp:ListBox ID="lbPath2" runat="server" Height="100px" Width="245px"></asp:ListBox>
    <br />
    文&nbsp;件&nbsp;名&nbsp;集&nbsp;合:<asp:ListBox ID="lbName2" runat="server" Height="100px"
        Width="245px"></asp:ListBox>
    <br />
    <br />
    <br />
    <asp:Button ID="Button2" runat="server" Text="获取文件路径和文件名" OnClick="Button2_Click" />
    </form>
</body>
</html>
