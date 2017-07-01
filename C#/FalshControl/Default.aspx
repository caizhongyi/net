<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>无标题页</title>
    <style  type="text/css">
    .list{ margin:0px 0px 0px 0px; padding:0px 0px 0px 0px; list-style-type:none; width:100%}
    .list li{  float:left}
    .form{ width:900px}
    </style>
    
   <script language="JavaScript">
    function GetDoc(movieName) {
        var isIE = navigator.appName.indexOf("Microsoft") != -1;
        return (isIE) ? window[movieName] : document[movieName];
    }
    function $(name) {
        return  document.getElementById(name)
    }
 </script>


</head>
<body>
    <form id="form1" runat="server">
    <div   class="form">

<table width=100% border=0 cellspacing=0 cellpadding=0>
<tr>
	<td width=100%>
    <table width=100% border=0 cellspacing=0 cellpadding=0>
    <tr>
		<td width=100% style='padding:10px 12px 10px 12px; background-position:0 213px' valign=top  class=norepeat>
				<table width=100% border=0 cellspacing=0 cellpadding=0 class=border1>
	
							
				<tr>
					<td style='border-top:solid 1px #7B869A; padding:10px'>
			
				<div align=center>
				

				</div>
						<!--BEGIN:[ content ]--><script language="JavaScript">
<!--import flash.external.ExternalInterface;

    function GetDoc(movieName) {
        var isIE = navigator.appName.indexOf("Microsoft") != -1;
        return (isIE) ? window[movieName] : document[movieName];
    }
// -->
</script>

<table border="0" cellpadding="0" cellspacing="4" style="border-collapse: collapse" bordercolor="#111111" align=center id="AutoNumber1">
  <tr>
    <td>
    <a href="javascript:GetDoc('Print2FlashDoc').setCurrentTool('move');"><img border="0" src="../images/help/dragbyhand.gif" width="26" height="26" alt="Drag"></a>
    <a href="javascript:GetDoc('Print2FlashDoc').setCurrentTool('select');"><img border="0" src="../images/help/selecttext.gif" width="26" height="26" alt="Select Text"></a>

    <a href="javascript:GetDoc('Print2FlashDoc').FitWidth();"><img border="0" src="../images/help/fitwidth.gif" alt=" Fit Width" width="26" height="26"></a>
    <a href="javascript:GetDoc('Print2FlashDoc').FitPage();"><img border="0" src="../images/help/fitpage.gif" alt="Fit Page" width="26" height="26"></a>
    <a href="javascript:GetDoc('Print2FlashDoc').PreviousPage();"><img border="0" src="../images/help/prevpage.gif" alt="Previous Page" width="26" height="26"></a>
    <a href="javascript:GetDoc('Print2FlashDoc').NextPage();"><img border="0" src="../images/help/nextpage.gif" alt="Next Page" width="26" height="26"></a>
    <a href="javascript:GetDoc('Print2FlashDoc').Rotate();"><img border="0" src="../images/help/rotate.gif" alt="Rotate" width="26" height="26"></a>
    <a href="javascript:GetDoc('Print2FlashDoc').printTheDocument();"><img border="0" src="../images/help/print.gif" alt="Print" width="26" height="26"></a>
    <a href="javascript:GetDoc('Print2FlashDoc').OpenInNewWindow();"><img border="0" src="../images/help/newwindow.gif" alt="Open In New Window" width="26" height="26"></a>
    <a href="javascript:GetDoc('Print2FlashDoc').OpenHelpPage();"><img border="0" src="../images/help/help.gif" alt="Help" width="26" height="26"></a></td>
    <td width=10></td>

    <td width="13" style="border-right-style: solid; border-right-width: 1; border-top-style: solid; border-top-width: 1; border-bottom-style: solid; border-bottom-width: 1; ">&nbsp;</td>
    <td>HTML part with buttons</td>
  </tr>
  <tr>
  <td height=4 colspan=4></td>
  </tr>
  <tr>
    <td>

	
<!-- Start of document code -->
<script language="JavaScript1.1" type="text/javascript">
 <!-- 
 var requiredMajorVersion = 8;
 var requiredMinorVersion = 0;
 var requiredRevision = 0;

 var isIE  = (navigator.appVersion.indexOf("MSIE") != -1) ? true : false;
 var isWin = (navigator.appVersion.toLowerCase().indexOf("win") != -1) ? true : false;
 
 function JSGetSwfVer(i){
             if (navigator.plugins != null && navigator.plugins.length > 0) {
                         if (navigator.plugins["Shockwave Flash 2.0"] || navigator.plugins["Shockwave Flash"]) {
                                    var swVer2 = navigator.plugins["Shockwave Flash 2.0"] ? " 2.0" : "";
                         var flashDescription = navigator.plugins["Shockwave Flash" + swVer2].description;
                                    descArray = flashDescription.split(" ");
                                    tempArrayMajor = descArray[2].split(".");
                                    versionMajor = tempArrayMajor[0];
                                    versionMinor = tempArrayMajor[1];
                                    if ( descArray[3] != "" ) {
                                                tempArrayMinor = descArray[3].split("r");
                                    } else {
                                                tempArrayMinor = descArray[4].split("r");
                                    }
                         versionRevision = tempArrayMinor[1] > 0 ? tempArrayMinor[1] : 0;
             flashVer = versionMajor + "." + versionMinor + "." + versionRevision;
             } else {
                                    flashVer = -1;
                         }
             }
             else if (navigator.userAgent.toLowerCase().indexOf("webtv/2.6") != -1) flashVer = 4;
             else if (navigator.userAgent.toLowerCase().indexOf("webtv/2.5") != -1) flashVer = 3;
             else if (navigator.userAgent.toLowerCase().indexOf("webtv") != -1) flashVer = 2;
             else {                       
                         flashVer = -1;
             }
             return flashVer;
 } 
 
 function DetectFlashVer(reqMajorVer, reqMinorVer, reqRevision) 
 {
             reqVer = parseFloat(reqMajorVer + "." + reqRevision);
             for (i=25;i>0;i--) {       
                         versionStr = JSGetSwfVer(i);              
                         if (versionStr == -1 ) { 
                                    return false;
                         } else if (versionStr != 0) {
                                    versionArray      = versionStr.split(".");
                                    
                                    versionMajor      = versionArray[0];
                                    versionMinor      = versionArray[1];
                                    versionRevision   = versionArray[2];
                                    
                                    versionString     = versionMajor + "." + versionRevision;   
                                    versionNum        = parseFloat(versionString);
                                    if ( (versionMajor > reqMajorVer) && (versionNum >= reqVer) ) {
                                                return true;
                                    } else {
                                                return ((versionNum >= reqVer && versionMinor >= reqMinorVer) ? true : false );    
                                    }
                         }
             }          
             return (reqVer ? false : 0.0);
 }
 // -->
</script>

<!-- Start of document placement code -->
<CENTER>
<script language="JavaScript" type="text/javascript">
 <!--
    var width = 710;
    var height = 500;
    var align = "center";
    var name = "Print2FlashDoc";
    var url = "nc_go.swf";
 if(isIE && isWin) {  
     var oeTags = '<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"'
     + 'width="'+width+'" height="'+height+'" align="'+align+'" id="'+name+'"'
     + 'codebase="http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version='+requiredMajorVersion+','+requiredMinorVersion+','+requiredRevision+',0">'
     + '<param name="movie" value="'+url+'" /><param name="quality" value="best" />'
     + '<param name="allowScriptAccess" value="sameDomain" />'
     + '<\/object>';
     document.write(oeTags);   
   } else if(DetectFlashVer(requiredMajorVersion, requiredMinorVersion, requiredRevision)) {
     var oeTags = '<embed src="'+url+'" quality="best" '
     + 'width="'+width+'" height="'+height+'" align="'+align+'" name="'+name+'"'
     + 'play="true"'
     + 'loop="false"'
     + 'quality="best"'
     + 'allowScriptAccess="sameDomain"'
     + 'type="application/x-shockwave-flash"'
     + 'pluginspage="http://www.macromedia.com/go/getflashplayer">'
     + '<\/embed>'
     document.write(oeTags);   
   } else {
     var alternateContent = 'This content requires the Adobe Flash Player. '
             + '<a href="http://www.macromedia.com/go/getflash/">Get Flash</a>';
     document.write(alternateContent);  
   }
  -->
 </script>
 <noscript>
             This content requires the Adobe Flash Player.
             <a href="http://www.macromedia.com/go/getflash/">Get Flash</a></noscript>
</CENTER>
<!-- End of document placement code -->

<!-- End of document code -->
	
    </td>

    <td width=10></td>
    <td style="border-top-color: black; border-right-style: solid; border-right-width: 1; border-top-style: solid; border-top-width: 1; border-bottom-style: solid; border-bottom-width: 1" width="13">&nbsp;</td>
    <td>Print2Flash document</td>
  </tr>
</table>




</td>
	</tr>

</table>						<p class=bright>
						&nbsp;Copyright &copy; Print2Flash Software 2005-2009. All rights reserved. <br><br>
						<a href='http://print2flash.com/support.php'>Support</a> | <a href='http://print2flash.com/privpolicy.php'>Privacy policy</a>
						<!--END:[ content ]-->
					</td>

				</tr>
				</table>	
			</td>
    </tr>
    </table>	
	</td>
</tr>
</table>	
    </div>
    </form>

</body>
</html>
