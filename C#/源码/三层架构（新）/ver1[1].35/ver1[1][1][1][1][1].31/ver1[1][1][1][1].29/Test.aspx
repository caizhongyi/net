<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<script language="javascript" type="text/javascript">
// <!CDATA[
var s = "";
function Button1_onclick() {
  var fileUpload = document.all("fileUpload");
  if(s==""){
    s =fileUpload.innerHTML;
  }
  fileUpload.innerHTML = fileUpload.innerHTML+s;
}

// ]]>
</script>
</head>
<body>
    <form id="form1" action="Test.aspx" method="post" enctype="multipart/form-data">
    <div>
        &nbsp;<table style="width: 600px">
            <tr>
                <td style="width: 35px">
                    <input id="Button1" type="button" value="+" onclick="return Button1_onclick()" /></td>
                <td style="width: 100px">
                    <div id="fileUpload"><input id="File" name="File" type="file" /></div>
                 </td>
            </tr>
            <tr>
                <td style="width: 35px; height: 26px;">
                </td>
                <td style="width: 100px; height: 26px;">
                    <input id="Submit1" type="submit" value="submit" /></td>
            </tr>
        </table>
        &nbsp; &nbsp;
    
    </div>
    </form>
</body>
</html>
