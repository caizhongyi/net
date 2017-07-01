<?php

$UploadPage = "http://localhost/upload.php";
$UploadChunkSize = 0;
$MaximumTotalUpload =-1;
$MaximumUpload = -1;
$MaxConcurrentUploads = 2;
$ResizeImage = "false";
$ImageSize = 300;
$Multiselect = "true";
// filter the file selection
$Filter = "Images (*.jpg;*.gif)|*.jpg;*.gif|All Files (*.*)|*.*";
// display files in the uploader
$AllowThumbnail = "false";
// javascript function to call when all files have uploaded
$JavascriptCompleteFunction = "UploadComplete";
$MaxNumberToUpload = -1;

	
$args ="UploadPage=" . urlencode($UploadPage) . ",UploadChunkSize=$UploadChunkSize,MaximumTotalUpload=$MaximumTotalUpload,MaximumUpload=$MaximumUpload,MaxConcurrentUploads=$MaxConcurrentUploads,ResizeImage=$ResizeImage,ImageSize=$ImageSize,Multiselect=$Multiselect,Filter=$Filter,AllowThumbnail=$AllowThumbnail,JavascriptCompleteFunction=" . urlencode($JavascriptCompleteFunction) . ",MaxNumberToUpload=$MaxNumberToUpload";
?>

<html>
<head>
	<script type="text/javascript">
		function UploadComplete()
		{
			alert("uploads complete");
            window.location.reload();
		}
	</script>
</head>
<body>

    <div style="width:600px;height:300px;">
        <object data="data:application/x-silverlight-2," type="application/x-silverlight-2" width="100%" height="100%">
			<param name="source" value="ClientBin/FileUpload.xap"/>
			<param name="background" value="white" />
			<param name="minRuntimeVersion" value="2.0.31005.0" />
			<param name="autoUpgrade" value="true" />
			<param name="initParams" value="<?php print $args?>" />
			<a href="http://go.microsoft.com/fwlink/?LinkID=124807" style="text-decoration: none;">
     			<img src="http://go.microsoft.com/fwlink/?LinkId=108181" alt="Get Microsoft Silverlight" style="border-style: none"/>
			</a>
		</object>
	</div>
</body>
</html>