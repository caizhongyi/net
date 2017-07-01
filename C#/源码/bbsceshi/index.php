<?php

/*
	[Discuz!] (C)2001-2009 Comsenz Inc.
	This is NOT a freeware, use is subject to license terms

	$Id: index.php 20430 2009-09-27 06:03:22Z monkey $
*/

define('CURSCRIPT', 'index');

require_once './include/common.inc.php';


if($indextype) {
	$op = empty($op) ? (!empty($_DCOOKIE['indextype']) ? $_DCOOKIE['indextype'] : $indextype) : $op;
	$indexfile = in_array($op, array('classics', 'feeds')) ? $op : 'classics';
	dsetcookie('indextype', $indexfile, 604800);
} else {
	$indexfile = 'classics';
}

if($indexfile == 'classics' || !empty($gid)) {
	require_once DISCUZ_ROOT.'./include/index_classics.inc.php';
} elseif($indexfile == 'feeds') {
	require_once DISCUZ_ROOT.'./include/index_feeds.inc.php';
} else {
	showmessage('undefined_action');
}

?>