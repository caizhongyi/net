<?php

/*
	[Discuz!] (C)2001-2009 Comsenz Inc.
	This is NOT a freeware, use is subject to license terms

	$Id: secqaa_daily.inc.php 19605 2009-09-07 06:18:45Z monkey $
*/

if(!defined('IN_DISCUZ')) {
	exit('Access Denied');
}

if($secqaa['status'] > 0) {
	require_once DISCUZ_ROOT.'./include/cache.func.php';
	updatecache('secqaa');
}

?>