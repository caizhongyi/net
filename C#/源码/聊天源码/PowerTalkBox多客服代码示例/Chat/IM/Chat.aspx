<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Chat.aspx.cs" Inherits="PowerChatRoom_Chat" %>

<%@ Register Assembly="PowerTalkBox" Namespace="PowerTalkBox" TagPrefix="PTB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>XXXX公司在线咨询系统</title>
    <link href="css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <PTB:PowerTalkBox ID="PowerTalkBox1" runat="server" AllowSendFile="False" CMode="OneToOne" ChatContrlHtml="客服:XXXXX为您服务<br>XXXX公司<br>电话：XXXXXXX<br>QQ:282602809" />
    
    </div>
    </form>
</body>
</html>
