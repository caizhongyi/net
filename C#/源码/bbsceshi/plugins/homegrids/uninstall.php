<?php

/*
	[Discuz!] (C)2001-2009 Comsenz Inc.
	This is NOT a freeware, use is subject to license terms

	$Id: uninstall.php 20529 2009-10-04 07:15:45Z monkey $
*/

if(!defined('IN_DISCUZ')) {
	exit('Access Denied');
}

if($installlang['homegrids']['requestpre']) {

	$sql = "DELETE FROM cdb_request WHERE variable like '".$installlang['homegrids']['requestpre']."%'";
	runquery($sql);

}

$finish = TRUE;