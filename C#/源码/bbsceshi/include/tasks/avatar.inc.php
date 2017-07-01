<?php

/*
	[Discuz!] (C)2001-2009 Comsenz Inc.
	This is NOT a freeware, use is subject to license terms

	$Id: avatar.inc.php 19605 2009-09-07 06:18:45Z monkey $
*/

if(!defined('IN_DISCUZ')) {
	exit('Access Denied');
}

function task_install() {
	global $db, $tablepre;
}

function task_uninstall() {
	global $db, $tablepre;
}

function task_upgrade() {
	global $db, $tablepre;
}

function task_condition() {
	global $discuz_uid;

	include_once DISCUZ_ROOT.'./uc_client/client.php';
	include language('tasks');
	if(uc_check_avatar($discuz_uid)) {
		showmessage($tasklang['avatar_apply_var_desc_noavatar']);
	}
}

function task_preprocess() {
}

function task_csc($task = array()) {
	global $discuz_uid;

	include_once DISCUZ_ROOT.'./uc_client/client.php';
	if(uc_check_avatar($discuz_uid)) {
		return true;
	}
	return array('csc' => 0, 'remaintime' => 0);
}

function task_sufprocess() {
}

?>