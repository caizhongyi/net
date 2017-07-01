<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="Upload" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
     <script language="javascript" type="text/javascript">
// <!CDATA[
var s = "";
function Button1_onclick() {
  var picObj = document.getElementById("pic");//IE FireFox 
  if(s==""){
   s = picObj.innerHTML;
  }
  picObj.innerHTML +=s;
}


// ]]>
</script>
  <form action="Upload.aspx" method="post" enctype="multipart/form-data">
    <table style="width: 373px">
        <tr>
            <td style="width: 62px">
            </td>
            <td style="width: 58px">
                <input id="Button1" type="button" value="+" onclick="return Button1_onclick()" /></td>
        </tr>
       
       <tr>
         <td colspan="2" id="pic">
            <table >
               <tr>
                    <td style="width: 62px">
                        图片名称:</td>
                    <td style="width: 100px">
                        <input id="Text1" type="text" /></td>
                </tr>
                <tr>
                    <td style="width: 62px; height: 20px">
                        上传图片</td>
                    <td style="width: 100px; height: 20px">
                        <input id="File1" name="File1" type="file" /></td>
                </tr>
                <tr>
                    <td style="width: 62px">
                        图片描述</td>
                    <td style="width: 100px">
                        <textarea id="TextArea1" style="width: 262px; height: 78px"></textarea></td>
                </tr>
            </table>
         </td>
       </tr>
       
        <tr>
            <td style="width: 62px">
            </td>
            <td style="width: 58px">
                <input id="Submit1" type="submit" value="文件上传" />
               </td>
        </tr>
       </table>
   </form>
</body>
</html>
