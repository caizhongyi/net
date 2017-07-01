<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Chat.aspx.cs" Inherits="PowerChatRoom_Chat" %>

<%@ Register Assembly="PowerTalkBox" Namespace="PowerTalkBox" TagPrefix="PTB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>聊天室</title>
    <link href="css.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <PTB:PowerTalkBox ID="PowerTalkBox1" runat="server" AllowSendFile="False" CMode="OneToMore" />
    
    </div>
    </form>
</body>
</html>
