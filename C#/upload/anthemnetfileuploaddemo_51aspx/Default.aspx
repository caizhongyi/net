<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Anthem.NET FileUpload Demo</title>
    <style type="text/css">
        *
        {
            margin: 3px;
            font-family: tahoma;
            font-size: 11px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <fieldset>
            <legend>Default ASP.NET FileUpload</legend>
            <asp:FileUpload ID="defaultFileUpload" runat="server" />
            <asp:Button ID="defaultUploadButton" runat="server" OnClick="defaultUploadButton_Click" Text="Upload" />
            <asp:Label ID="defaultResultLabel" runat="server" Text=""></asp:Label>
        </fieldset>
            
        <fieldset>
            <legend>Anthem.NET FileUpload</legend>
            <anthem:FileUpload ID="anthemFileUpload" runat="server" />
            <anthem:Button ID="anthemUploadButton" TextDuringCallBack="uploading..." EnabledDuringCallBack="false"
                runat="server" Text="Upload" OnClick="anthemUploadButton_Click" />
            <anthem:Label ID="anthemResultLabel" runat="server" Text=""></anthem:Label>
        </fieldset>
    </form>
</body>
</html>
