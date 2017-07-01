<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN"
        "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Page Language="C#" AutoEventWireup="false" Src="DBWrite.aspx.cs" Inherits="UploaderTest.DBWrite" %>
<%@ Register TagPrefix="Upload" Namespace="Brettle.Web.NeatUpload" Assembly="Brettle.Web.NeatUpload" %>
<%@ Register TagPrefix="SqlUpload" Namespace="Hitone.Web.SqlServerUploader" Assembly="Hitone.Web.SqlServerUploader" %>
<html>
<head runat="server">
    <title>NeatUpload Demo</title>
    <style type="text/css">
<!--
		.ProgressBar {
			margin: 0px;
			border: 0px;
			padding: 0px;
			width: 100%;
			height: 2em;
		}UploaderTest
-->
		</style>
</head>
<body>
    <form id="uploadForm" runat="server">
        <h1>
            NeatUpload SqlServerUploader Demo</h1>
        <p>
            This page demonstrates the basic functionality of <a href="http://www.brettle.com/neatupload">
                NeatUpload</a> and the plugin SqlServerUploadStorageProvider that uploads files directly into
                a SQL Server database. Start by selecting the progress bar location and submit button
            type you'd like to see demonstrated.
        </p>
        <p>
            Progress bar location:
            <asp:DropDownList ID="progressBarLocationDropDown" runat="server" AutoPostBack="True"
                CausesValidation="False">
                <asp:ListItem Value="Popup">Popup</asp:ListItem>
                <asp:ListItem Value="Inline" Selected="True">Inline</asp:ListItem>
            </asp:DropDownList>
            Submit button type:
            <asp:DropDownList ID="buttonTypeDropDown" runat="server" AutoPostBack="True" CausesValidation="False">
                <asp:ListItem Value="Button" Selected="True">Button</asp:ListItem>
                <asp:ListItem Value="LinkButton">LinkButton</asp:ListItem>
                <asp:ListItem Value="CommandButton">CommandButton</asp:ListItem>
            </asp:DropDownList>
        </p>
        <p>
            Now select some files and click Submit.
        </p>
        <p>
            Pick file #1:
            <SqlUpload:SqlServerInputFile ID="inputFile" runat="server" />
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="inputFile"
                ValidationExpression=".*([^e]|[^x]e|[^e]xe|[^.]exe)$" Display="Static" ErrorMessage="No EXEs allowed"
                EnableClientScript="True" runat="server" /><br />
            Pick file #2:
            <SqlUpload:SqlServerInputFile ID="inputFile2" runat="server" />
            <br />
            <span id="submitButtonSpan" runat="server">
                <asp:Button ID="submitButton" runat="server" Text="Submit" />
                <asp:Button ID="cancelButton" runat="server" Text="Cancel" CausesValidation="False" /><br />
            </span><span id="commandButtonSpan" runat="server">
                <asp:Button ID="commandButton" runat="server" Text="Submit Command" Command="Submit" />
                <asp:Button ID="cancelCommandButton" runat="server" Text="Cancel Command" CausesValidation="False"
                    Command="Cancel" /><br />
            </span><span id="linkButtonSpan" runat="server">
                <asp:LinkButton ID="linkButton" runat="server" Text="Submit" />
                <asp:LinkButton ID="cancelLinkButton" runat="server" Text="Cancel" CausesValidation="False" /><br />
            </span>
            <p>
                SqlServerUploadStorageProvider streams files directly into a SQL Server database table,
                using minimal resources on the web server (i.e. the file is never cached on the web server).<br />
                It also has the ability to create a hash of the file while uploading (functionality taken from Dean's
                HashedFilesystemUploiadStorageProvider). This page demonstrates the upload capabilities and computes 
                a hash.<br /><br />
                The SqlServerUpload package also includes a stream reader for SqlServer which allows you to download files
                from the SQL Server in a streaming fashion. This page and "DBRead.aspx" demonstrates how to use this functionality
                (just click the download link when finished uploading)
            </p>
            <div id="bodyPre" runat="server">
			
			</div>
        </p>
        <div id="inlineProgressBarDiv" runat="server">
            <h2>
                Inline Progress Bar</h2>
            <p>
                Here's the inline progress bar:
                <Upload:ProgressBar ID="inlineProgressBar" runat="server" Inline="true" Triggers="submitButton linkButton commandButton" />
                The inline progress bar is an IFRAME. If your browser doesn't support IFRAMEs, you'll
                see a link to display the progress bar in a new window. The text of that link is
                configurable.
            </p>
        </div>
        <div id="popupProgressBarDiv" runat="server">
            <h2>
                Popup Progress Bar</h2>
            <p>
                Here's what is generated by a ProgressBar control configured to display a popup:
            </p>
            <Upload:ProgressBar ID="progressBar" runat="server" Triggers="submitButton linkButton commandButton">
                <asp:Label ID="label" runat="server" Text="Check Progress" />
            </Upload:ProgressBar>
            <p>
                <strong>What? You don't see anything?</strong> That's because you have JavaScript
                enabled. With JavaScript enabled, the popup is shown automatically when you submit
                the form, so there is no need to display anything extra on this page. If you disable
                JavaScript, you will see a "Check Progress" link which you can click on to display
                the progress bar in a new window. The text of that link is configurable.
            </p>
        </div>
        <h2>
            Cancelling Uploads</h2>
        <p>
            You can cancel your upload by either clicking your browser's Stop button or clicking
            the Cancel button that is displayed to the right of the progress bar when the upload
            is in progress.
        </p>
    </form>
</body>
</html>
