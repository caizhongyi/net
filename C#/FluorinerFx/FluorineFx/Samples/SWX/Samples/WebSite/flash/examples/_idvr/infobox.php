<!--
THE INFO BOX
The contents of this file are what is loaded into the "InfoBox", if it has been enabled in config.php.
To format the text, you can use normal HTML tags like:
<h2>text</h2> - produces a heading
<h3>text</h3> - produces a sub-heading
<strong>text</strong> - produces bold text
<em>text</em> - produces italic text
<u>text</u> - produces underlined text
<p>text</p> - a text paragraph
<a href="http://www.mylink.here">text</a> - produces a link to another page
<img src="http://www.mysite.here/image.jpg" /> - displays an image

These instructions will not appear in the info box.
-->
<h2><?php echo getcwd();?></h2>

<p><strong>The Flash Examples show you how to use SWX RPC.</strong> The path to the examples folder on your machine is shown above (it has also been automatically copied to your system clipboard for your convenience). Click on the HTML files to see the examples in the browser. The FLA files have been saved in Flash 8 format.</p>

<p>The examples all use the <a href="http://swxformat.org/documentation/#92" title="Documentation at  SWX: SWF Data Format" target="_blank">Public SWX Gateway</a> and require an active Internet connection to work. To make them use your local SWX PHP gateway, modify the gateway URLs in the FLAs to point to your local gateway at 

<?php
	$pathToScript = $_SERVER['HTTP_HOST'].$_SERVER['SCRIPT_NAME'];
	$pathToGateway = 'http://'.substr($pathToScript, 0, -24).'php/swx.php';
	echo '<a href="'.$pathToGateway.'" target="_blank">'.$pathToGateway.'</a>';
?>.</p>