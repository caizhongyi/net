<?php

/*
	[Discuz!] (C)2001-2009 Comsenz Inc.
	This is NOT a freeware, use is subject to license terms

	$Id: magic.inc.php 16697 2008-11-14 07:36:51Z monkey $
*/

if(!defined('IN_DISCUZ')) {
        exit('Access Denied');
}

if($requestrun) {

	$limit = !empty($settings['limit']) ? intval($settings['limit']) : 10;

	$cachefile = DISCUZ_ROOT.'./forumdata/cache/requestscript_magic.php';
	if((@!include($cachefile)) || $limit != $limitcache) {
		$query = $db->query("SELECT magicid, name, identifier, description FROM {$tablepre}magics WHERE recommend='1' AND available='1' ORDER BY displayorder DESC LIMIT $limit");

		$recommendmagic = array();
		while($magic = $db->fetch_array($query)) {
			$magic['identifier'] = strtolower($magic['identifier']).'.gif';			
			$recommendmagic[] = $magic;
		}
		writetorequestcache($cachefile, 0, "\$limitcache = $limit;\n\$recommendmagic = ".var_export($recommendmagic, 1).';');
	}
	
	$recommendmagic = $recommendmagic[array_rand($recommendmagic)];

	include template('request_magic');

} else {

	$request_version = '1.0';
	$request_name = '�Ƽ�����';
	$request_description = '����ҳ����ʾվ���Ƽ��ĵ���';
	$request_copyright = '<a href="http://www.comsenz.com" target="_blank">Comsenz Inc.</a>';

}

?>