<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BWCheck.aspx.cs" Inherits="BWCheck" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>BWCheck</title>
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
    </style>
</head>
<body>
	<div id="flashcontent">
        <p>Flash content</p>
    </div>
    
	<script type="text/javascript">
		var so = new SWFObject("bwcheck.swf", "swf", "100%", "100%", "9,0,115,0", "#ffffff");
		so.addParam("allowScriptAccess", "always");
		so.addParam("scale", "noscale");
		so.addParam("menu", "false");
		so.write("flashcontent");
	</script>    
</body>
</html>
