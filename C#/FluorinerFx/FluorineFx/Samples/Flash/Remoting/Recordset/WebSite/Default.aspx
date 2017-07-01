<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Recordset Sample</title>
    <!-- SWFObject embed by Geoff Stearns geoff@deconcept.com http://blog.deconcept.com/swfobject/ -->
    <script type="text/javascript" src="swfobject.js"></script>
    <style type="text/css">	
	    /* hide from ie on mac \*/
	    html {
		    height: 100%;
		    overflow: hidden;
	    }	
	    #flashcontent {
		    height: 100%;
	    }
	    /* end hide */
	    body {
		    height: 100%;
		    margin: 0;
		    padding: 0;
		    background-color: #ffffff;
	    }
    </style>  </head>
<body>
	<div id="flashcontent">
        Flash content
    </div>
	<script type="text/javascript">
		var so = new SWFObject("data.swf", "flash", "100%", "100%", "8", "#ffffff");
		so.addParam("allowScriptAccess", "always");
		so.addParam("scale", "noscale");
		so.addParam("menu", "false");
		so.addVariable("remotingGatewayPath", "<%=Request.Url.GetLeftPart(UriPartial.Authority)%>");
		so.write("flashcontent");
	</script>    
</body>
</html>
