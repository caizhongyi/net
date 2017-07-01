<?php

/*
	[Discuz!] (C)2001-2009 Comsenz Inc.
	This is NOT a freeware, use is subject to license terms

	$Id: birthdays_daily.inc.php 19605 2009-09-07 06:18:45Z monkey $
*/

if(!defined('IN_DISCUZ')) {
	exit('Access Denied');
}

if($maxbdays) {
	require_once DISCUZ_ROOT.'./include/cache.func.php';
	updatecache('birthdays');
	updatecache('birthdays_index');
}

if($bdaystatus) {
	$today = gmdate('m-d', $timestamp + $_DCACHE['settings']['timeoffset'] * 3600);
	$query = $db->query("SELECT uid, username, email, bday FROM {$tablepre}members WHERE RIGHT(bday, 5)='$today' ORDER BY bday");
	global $member;
	while($member = $db->fetch_array($query)) {
		sendmail("$member[username] <$member[email]>", 'birthday_subject', 'birthday_message');
	}
}

?>