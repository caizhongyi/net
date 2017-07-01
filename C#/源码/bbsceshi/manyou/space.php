<?php

/*
	[Discuz!] (C)2001-2009 Comsenz Inc.
	This is NOT a freeware, use is subject to license terms

	$Id: space.php 19605 2009-09-07 06:18:45Z monkey $
*/

if(!empty($_GET['uid'])) {
	header('location: ../space.php?uid='.intval($_GET['uid']));
} elseif(!empty($_GET['username'])) {
	header('location: ../space.php?uid='.rawurlencode($_GET['username']));
}

?>