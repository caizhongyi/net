<?php

/*
	[Discuz!] (C)2001-2009 Comsenz Inc.
	This is NOT a freeware, use is subject to license terms

	$Id: MiniBlog.php 19605 2009-09-07 06:18:45Z monkey $
*/

if(!defined('IN_DISCUZ')) {
	exit('Access Denied');
}

class MiniBlog {

	function post($uId, $message, $clientIdentify, $ip = '') {
		return new APIResponse(0);
	}

	function get($uId, $num) {
		return new APIResponse(0);
	}

}

?>