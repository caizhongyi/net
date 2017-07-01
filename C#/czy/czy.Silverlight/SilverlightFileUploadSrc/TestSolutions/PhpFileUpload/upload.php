<?php
//  This is the most basic of scripts with no try catches
$filename = isset($_GET["filename"]) ? $_GET["filename"] : "";
$complete = isset($_GET["Complete"]) ? strtolower($_GET["Complete"]) == "true" ? true : false : true;
$getBytes = isset($_GET["GetBytes"]) ? strtolower($_GET["GetBytes"]) == "true" ? true : false : false;
$startByte = isset($_GET["StartByte"]) ? (int)$_GET["StartByte"] : 0;

$filePath = "C:\\inetpub\\wwwroot\\upload\\" . $filename;
if($getBytes)
{
	if(file_exists($filePath))
	{
		print filesize($filePath);
		
	}
	else
		print "0";
}
else
{
	try
	{
		if($startByte > 0 && file_exists($filePath))
			$file = fopen($filePath,"a");
		else
			$file = fopen($filePath,"w");
		$input = file_get_contents ("php://input");
		fwrite($file,$input);
		fclose($file);
		if($complete)
		{
			// do something with the file if you want, like save to a database or rename/move it
		}
	}
	catch (Exception $e) 
	{
    	echo 'Caught exception: ',  $e->getMessage(), "\n";
	}
}

?>
