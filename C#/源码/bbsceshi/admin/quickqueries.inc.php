<?php

/*
	[Discuz!] (C)2001-2009 Comsenz Inc.
	This is NOT a freeware, use is subject to license terms

	$Id: quickqueries.inc.php 19605 2009-09-07 06:18:45Z monkey $
*/

if(!defined('IN_DISCUZ') || !defined('IN_ADMINCP')) {
        exit('Access Denied');
}

$simplequeries = array(
	array('comment' => '���ٿ�����̳��鹦��', 'sql' => ''),
	array('comment' => '���� ���а�� �������վ', 'sql' => 'UPDATE {tablepre}forums SET recyclebin=\'1\''),
	array('comment' => '���� ���а�� Discuz! ���롱', 'sql' => 'UPDATE {tablepre}forums SET allowbbcode=\'1\''),
	array('comment' => '���� ���а�� [IMG] ���롱', 'sql' => 'UPDATE {tablepre}forums SET allowimgcode=\'1\''),
	array('comment' => '���� ���а�� Smilies ����', 'sql' => 'UPDATE {tablepre}forums SET allowsmilies=\'1\''),
	array('comment' => '���� ���а�� ���ݸ�����', 'sql' => 'UPDATE {tablepre}forums SET jammer=\'1\''),
	array('comment' => '���� ���а�� ��������������', 'sql' => 'UPDATE {tablepre}forums SET allowanonymous=\'1\''),

	array('comment' => '���ٹر���̳��鹦��', 'sql' => ''),
	array('comment' => '�ر� ���а�� �������վ', 'sql' => 'UPDATE {tablepre}forums SET recyclebin=\'0\''),
	array('comment' => '�ر� ���а�� HTML ����', 'sql' => 'UPDATE {tablepre}forums SET allowhtml=\'0\''),
	array('comment' => '�ر� ���а�� Discuz! ����', 'sql' => 'UPDATE {tablepre}forums SET allowbbcode=\'0\''),
	array('comment' => '�ر� ���а�� [IMG] ����', 'sql' => 'UPDATE {tablepre}forums SET allowimgcode=\'0\''),
	array('comment' => '�ر� ���а�� Smilies ����', 'sql' => 'UPDATE {tablepre}forums SET allowsmilies=\'0\''),
	array('comment' => '�ر� ���а�� ���ݸ�����', 'sql' => 'UPDATE {tablepre}forums SET jammer=\'0\''),
	array('comment' => '�ر� ���а�� ������������', 'sql' => 'UPDATE {tablepre}forums SET allowanonymous=\'0\''),

	array('comment' => '��Ա�������', 'sql' => ''),
	array('comment' => '��� ���л�Ա �Զ�����', 'sql' => 'UPDATE {tablepre}members SET styleid=\'0\''),
	array('comment' => '��� ���л�Ա ���ֽ��׼�¼', 'sql' => 'TRUNCATE {tablepre}creditslog;'),
	array('comment' => '��� ���л�Ա �ղؼ�', 'sql' => 'TRUNCATE {tablepre}favorites;'),
);

?>