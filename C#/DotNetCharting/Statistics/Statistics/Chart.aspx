<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Chart.aspx.cs" Inherits="Statistics.Chart" %>
<%@ Register assembly="dotnetCHARTING" namespace="dotnetCHARTING" tagprefix="dotnetCHARTING" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dotnetCHARTING:Chart ID="Chart1" runat="server">
    </dotnetCHARTING:Chart>
    </div>
    </form>
</body>
</html>
<script type="text/javascript"> 
 var	obj = document.getElementsByTagName("map")[0];
			obj.parentNode.removeChild(obj); //屏蔽隐藏的链接
</script>
