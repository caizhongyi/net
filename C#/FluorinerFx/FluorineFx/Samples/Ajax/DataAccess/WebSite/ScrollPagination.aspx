<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ScrollPagination.aspx.cs" Inherits="ScrollPagination" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Scrolling Data Pagination Using Ajax(extjs), Json(</title>
    <link rel="stylesheet" type="text/css" href="css/ext-all.css" />
    <style type="text/css">
        a:link {color: #000000;text-decoration: none;border-bottom: 1px dashed #d5ad84;}
        a:visited {color: #000000;text-decoration: none;border-bottom: 1px dashed #d5ad84;}
        a:hover {border-bottom: 1px solid #d5ad84;}
        a:active {color: #000000;text-decoration: none;border-bottom: 1px dashed #d5ad84;}
        h1 {font-size: 18px;font-weight: bold;color: #786652;}
        body {margin: 5px;font-family: Arial;font-size: 12px;}
    </style>
    <!--EXT lib-->
    <!--http://extjs.com/-->
    <script type="text/javascript" src="js/ext-base.js"></script>
    <script type="text/javascript" src="js/ext-all.js"></script>
    
    <script type="text/javascript" src="js/json2.js"></script>
    <script type="text/javascript" src="jsongateway.aspx?proxy&destination=fluorine&source=ServiceLibrary.MyDataService"></script>    
    
    <!--Scrolling Pagination Jscript-->
    <script type="text/javascript" src="js/ScrollPagination.js"></script>
</head>
<body>
    <div>
       <h1>Scrolling Data Pagination Using Ajax(extjs), Json</h1>
       <br />    
       <div id="pagestatus">
           Currently viewing <span id="currentcount"></span>&nbsp;out of&nbsp;<span id="totalcount"></span>
           records.</div>
           <div id="countrygrid">
           </div>
    </div>
</body>
</html>
