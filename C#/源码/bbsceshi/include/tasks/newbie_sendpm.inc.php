<?php

/*
	[Discuz!] (C)2001-2009 Comsenz Inc.
	This is NOT a freeware, use is subject to license terms

	$Id: newbie_sendpm.inc.php 19605 2009-09-07 06:18:45Z monkey $
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
}

function task_preprocess() {
}

function task_csc($task = array()) {
	global $db, $tablepre, $msgto;
	return intval($msgto) == $db->result_first("SELECT value FROM {$tablepre}taskvars WHERE taskid='$task[taskid]' AND variable='authorid'") ? TRUE : array('csc' => 0, 'remaintime' => 0);
}

function task_sufprocess() {
}

?>