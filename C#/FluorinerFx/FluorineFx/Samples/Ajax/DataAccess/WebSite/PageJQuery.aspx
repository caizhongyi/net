<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PageJQuery.aspx.cs" Inherits="PageJQuery" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="js/json2.js"></script>
    <script type="text/javascript" src="js/jquery-1.2.6.js"></script>
    <script type="text/javascript" src="jsongateway.aspx?proxy&generator=jquery&destination=fluorine&source=ServiceLibrary.MyDataService"></script>
    <script type="text/javascript" src="jsongateway.aspx?proxy&generator=jquery&destination=secure"></script>
    <script type="text/javascript">
    /* <![CDATA[ */
    
    function jQueryChannel() 
    {
        this.rpc = function(call) 
        {
            if (!call.callback)
                throw new Error('Synchronous calls not supported.');
            $.ajax({
                type: "POST",
                url: call.url,
                data: JSON.stringify(call.request),
                beforeSend: function(xhr) {
                    xhr.setRequestHeader("X-JSON-RPC", call.request.method);
                },
                success: function(s) {
                    call.callback(JSON.parse(s));
                }
            });    
        }
    }
    window.onload = function() 
    {
        var s = new MyDataService();
        var jsonString = '{"productId":1234,"price":24.5,"inStock":true,"name":"bananas"}';
        var myObject = JSON.parse(jsonString);
        s.channel = new jQueryChannel(); // Overrides default Jayrock Proxy Channel
        s.EchoProduct(myObject, function(response) { alert(response.result.name) });
    }
    /* ]]> */
    </script>    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
