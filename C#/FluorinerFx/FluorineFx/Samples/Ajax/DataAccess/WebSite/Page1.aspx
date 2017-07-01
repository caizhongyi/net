<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Page1.aspx.cs" Inherits="Page1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="js/json2.js"></script>
    <script type="text/javascript" src="jsongateway.aspx?proxy&destination=fluorine&source=ServiceLibrary.MyDataService"></script>
    <script type="text/javascript" src="jsongateway.aspx?proxy&destination=secure"></script>
    <script type="text/javascript">
    /* <![CDATA[ */
    window.onload = function() 
    {
        var s = new MyDataService();
        var jsonString = '{"productId":1234,"price":24.5,"inStock":true,"name":"bananas"}';
        var myObject = JSON.parse(jsonString);
        alert(s.EchoProduct(myObject).name);
        
        var records = [
            {id:1, name: "Bob",   age: 47, favorite_color: "blue"},   
            {id:2, name: "Sally", age: 30, favorite_color: "mauve"},   
            {id:3, name: "Tommy", age: 13, favorite_color: "black"},   
            {id:4, name: "Chaz",  age: 26, favorite_color: "plaid"}
            ];  
        alert(s.EchoRecords(records)[0].name);
        

        //alert("sync:" + s.SayHello());
        //s.SayHello(function(response) { alert("async:" + response.result) });
        //alert(s.Echo("some text"));
        //alert(s.GetCustomers("415"));
        //alert(s.GetCustomersJson("415"));
        
        
        var s = new SecureService();
        s.setCredentials('admin', 'admin');
        try
        {
            alert(s.GetSecureData());
        }
        catch(err)
        {
            alert(err.message);
        }
        //clear credentials, go with the auth cookie
        s.setCredentials(null, null);
        alert(s.GetSecureData());
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
