<?php

/*
	[Discuz!] (C)2001-2009 Comsenz Inc.
	This is NOT a freeware, use is subject to license terms

	$Id: cleanup_monthly.inc.php 19605 2009-09-07 06:18:45Z monkey $
*/

if(!defined('IN_DISCUZ')) {
	exit('Access Denied');
}

$myrecordtimes = $timestamp - $_DCACHE['settings']['myrecorddays'] * 86400;

$db->query("DELETE FROM {$tablepre}invites WHERE dateline<'$timestamp'-2592000 AND status='4'", 'UNBUFFERED');
$db->query("TRUNCATE {$tablepre}relatedthreads");
$db->query("DELETE FROM {$tablepre}mytasks WHERE status='-1' AND dateline<'$timestamp'-2592000", 'UNBUFFERED');

?>