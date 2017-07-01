<%@ Page Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeFile="PicAdd.aspx.cs" Inherits="PicAdd" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPH" Runat="Server">
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


function Button2_onclick() {
  var picObj = document.getElementById("pic");//IE FireFox 
  if(s!="" ){
    var index = picObj.innerHTML.lastIndexOf(s);
    var fIndex = picObj.innerHTML.indexOf(s);
    if(fIndex!=index)
    picObj.innerHTML = picObj.innerHTML.substring(0,index);
  }
}

// ]]>
</script>

    <table style="width: 373px">
        <tr>
            <td style="width: 30px">
            </td>
            <td style="width: 58px">
                <input id="Button1" type="button" value="+" onclick="return Button1_onclick()" />
                <input id="Button2" type="button" value="-" onclick="return Button2_onclick()" /></td>
        </tr>
       
       <tr>
         <td colspan="2" id="pic">
             
            <table >
               <tr>
                    <td style="width: 62px">
                        图片名称:</td>
                    <td style="width: 100px">
                        <input id="Name" name="Name" type="text" /></td>
                </tr>
                <tr>
                    <td style="width: 62px; height: 20px">
                        上传图片</td>
                    <td style="width: 100px; height: 20px">
                        <input id="Path" name="Path" type="file" /></td>
                </tr>
                <tr>
                    <td style="width: 62px">
                        图片描述</td>
                    <td style="width: 100px">
                        <textarea id="Description" name="Description" style="width: 262px; height: 78px"></textarea></td>
                </tr>
            </table>
         </td>
       </tr>
       
        <tr>
            <td style="width: 30px">
            </td>
            <td style="width: 58px">
                <input id="Submit1" type="submit" value="文件上传" />
                <asp:Label ID="Msg" runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
       </table>

</asp:Content>

