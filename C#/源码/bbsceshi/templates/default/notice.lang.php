<?php

$language = array
(

	'reason_moderate' => '<div class=\"f_manage\">�������� <a href=\"{boardurl}viewthread.php?from=notice&tid={$thread[tid]}\">{$thread[subject]}</a> �� <a href=\"{boardurl}space.php?from=notice&uid={$discuz_uid}\">{$discuz_userss}</a> {$modaction} {time}
<fieldset><ins>{$reason}</ins></fieldset></div>',

	'reason_merge' => '<div class=\"f_manage\">�������� <a href=\"{boardurl}viewthread.php?from=notice&tid={$thread[tid]}\">{$thread[subject]}</a> �� <a href=\"{boardurl}space.php?from=notice&uid={$discuz_uid}\">{$discuz_userss}</a> {$modaction} {time}
<fieldset><ins>{$reason}</ins></fieldset></div>',

	'reason_delete_post' => '<div class=\"f_manage\">�������� <a href=\"{boardurl}viewthread.php?from=notice&tid={$thread[tid]}\">{$thread[subject]}</a> �� <a href=\"{boardurl}space.php?from=notice&uid={$discuz_uid}\">{$discuz_userss}</a> {$modaction} {time}
<fieldset><ins>{$reason}</ins></fieldset></div>',

	'reason_ban_post' => '<div class=\"f_manage\">�������� <a href=\"{boardurl}viewthread.php?from=notice&tid={$thread[tid]}\">{$thread[subject]}</a> �� <a href=\"{boardurl}space.php?from=notice&uid={$discuz_uid}\">{$discuz_userss}</a> {$modaction} {time}
<fieldset><ins>{$reason}</ins></fieldset></div>',

	'reason_warn_post' => '<div class=\"f_manage\">�������� <a href=\"{boardurl}viewthread.php?from=notice&tid={$thread[tid]}\">{$thread[subject]}</a> �� <a href=\"{boardurl}space.php?from=notice&uid={$discuz_uid}\">{$discuz_userss}</a> {$modaction} {time}<br />
���� $warningexpiration �����ۼ� $warninglimit �ξ��棬�������Զ���ֹ���� $warningexpiration �졣<br />
����Ŀǰ�����ѱ����� $authorwarnings �Σ���ע�⣡
<fieldset><ins>{$reason}</ins></fieldset></div>',

	'reason_move' => '<div class=\"f_manage\">�������� <a href=\"{boardurl}viewthread.php?from=notice&tid={$thread[tid]}\">{$thread[subject]}</a> �� <a href=\"{boardurl}space.php?from=notice&uid={$discuz_uid}\">{$discuz_userss}</a> �ƶ��� <a href=\"{boardurl}forumdisplay.php?from=notice&fid={$toforum[fid]}\">{$toforum[name]}</a> {time}
<fieldset><ins>{$reason}</ins></fieldset></div>',

	'reason_copy' => '<div class=\"f_manage\">�������� <a href=\"{boardurl}viewthread.php?from=notice&tid={$thread[tid]}\">{$thread[subject]}</a> �� <a href=\"{boardurl}space.php?from=notice&uid={$discuz_uid}\">{$discuz_userss}</a> ����Ϊ <a href=\"{boardurl}viewthread.php?from=notice&tid=$threadid\">{$thread[subject]}</a> {time}
<fieldset><ins>{$reason}</ins></fieldset></div>',

	'modthreads_delete' => '<div class=\"f_manage\">����������� {$threadsubject} û��ͨ����ˣ����ѱ�ɾ���� {time}
<fieldset><ins>{$reason}</ins></fieldset></div>',

	'modthreads_validate' => '<div class=\"f_manage\">����������� <a href=\"{boardurl}viewthread.php?from=notice&tid={$tid}\">{$threadsubject}</a> �Ѿ����ͨ���� {time}
<a href=\"{boardurl}viewthread.php?from=notice&tid={$tid}\" class=\"il to\">�鿴</a>
<fieldset><ins>{$reason}</ins></fieldset></div>',

	'modreplies_delete' => '<div class=\"f_manage\">������ظ�û��ͨ����ˣ����ѱ�ɾ���� {time}
<dl class=\"summary\"><dt>�ظ����ݣ�</dt><dd>$post</dd></dl>
<fieldset><ins>{$reason}</ins></fieldset></div>',

	'modreplies_validate' => '<div class=\"f_manage\">������Ļظ��Ѿ����ͨ���� {time}
<a href=\"{boardurl}viewthread.php?from=notice&tid={$tid}\" class=\"il to\">�鿴</a>
<dl class=\"summary\"><dt>�ظ����ݣ�</dt><dd>$post</dd></dl>
<fieldset><ins>{$reason}</ins></fieldset></div>',

	'reportpost' => '<div><a href=\"{boardurl}space.php?from=notice&uid={$discuz_uid}\">{$discuz_userss}</a> �������� {time}
<a href=\"{boardurl}{$posturl}\" class=\"il to\">�鿴</a>
<fieldset><ins>{$reason}</ins></fieldset></div>',

	'transfer' => '<div class=\"f_credit\">���յ�һ������ <a href=\"{boardurl}space.php?from=notice&uid={$discuz_uid}\">{$discuz_userss}</a> �Ļ���ת�� {$extcredits[$creditstrans][title]} {$netamount} {$extcredits[$creditstrans][unit]} {time}
<a href=\"{boardurl}memcp.php?from=notice&action=creditslog\" class=\"il to\">�鿴</a>
<fieldset><ins>{$transfermessage}</ins></fieldset></div>',

	'addfunds' => '<div class=\"f_credit\">���ύ�Ļ��ֳ�ֵ�����ѳɹ���ɣ���Ӧ����Ļ����Ѿ��������Ļ����˻� {time}
<a href=\"{boardurl}memcp.php?from=notice&action=creditslog\" class=\"il to\">�鿴</a>
<dl class=\"summary\"><dt>�����ţ�</dt><dd>{$order[orderid]}<dt>֧����</dt><dd>����� {$order[price]} Ԫ</dd><dt>���룺</dt><dd>{$extcredits[$creditstrans][title]} {$order[amount]} {$extcredits[$creditstrans][unit]}</dd></dl></div>',

	'rate_reason' => '<div class=\"f_rate\">�������� <a href=\"{boardurl}viewthread.php?from=notice&tid={$thread[tid]}\">{$thread[subject]}</a> �� <a href=\"{boardurl}space.php?from=notice&uid={$discuz_uid}\">{$discuz_userss}</a> ���� {$ratescore} {time}
<fieldset><ins>{$reason}</ins></fieldset></div>',

	'rate_removereason' => '<div class=\"f_rate\"><a href=\"{boardurl}space.php?from=notice&uid={$discuz_uid}\">{$discuz_userss}</a> �����˶������� <a href=\"{boardurl}viewthread.php?from=notice&tid={$thread[tid]}\">{$thread[subject]}</a> ������ {$ratescore} {time}
<fieldset><ins>{$reason}</ins></fieldset></div>',

	'trade_seller_send' => '<div class=\"f_trade\"><a href=\"{boardurl}space.php?from=notice&uid={$userid}\">{$user}</a> ����������Ʒ <a href=\"{boardurl}trade.php?from=notice&orderid={$orderid}\">{$itemsubject}</a>���Է��Ѿ�����ȴ������� {time}
<a href=\"{boardurl}trade.php?from=notice&orderid={$orderid}\" class=\"il to\">�鿴</a></div>',

	'trade_buyer_confirm' => '<div class=\"f_trade\">���������Ʒ <a href=\"{boardurl}trade.php?from=notice&orderid={$orderid}\">{$itemsubject}</a>��<a href=\"{boardurl}space.php?from=notice&uid={$userid}\">{$user}</a> �ѷ������ȴ���ȷ�� {time}
<a href=\"{boardurl}trade.php?from=notice&orderid={$orderid}\" class=\"il to\">�鿴</a></div>',

	'trade_fefund_success' => '<div class=\"f_trade\">��Ʒ <a href=\"{boardurl}trade.php?from=notice&orderid={$orderid}\">{$itemsubject}</a> ���˿�ɹ� {time}
<a href=\"{boardurl}trade.php?from=notice&orderid={$orderid}\" class=\"il to\">����</a></div>',

	'trade_success' => '<div class=\"f_trade\">��Ʒ <a href=\"{boardurl}trade.php?from=notice&orderid={$orderid}\">{$itemsubject}</a> �ѽ��׳ɹ� {time}
<a href=\"{boardurl}trade.php?from=notice&orderid={$orderid}\" class=\"il to\">����</a></div>',

	'eccredit' => '<div class=\"f_trade\">�������׵� <a href=\"{boardurl}space.php?from=notice&uid={$discuz_uid}\">{$discuz_userss}</a> �Ѿ������������� {time}
<a href=\"{boardurl}trade.php?from=notice&orderid={$orderid}\" class=\"il to\">����</a></div>',

	'activity_apply' => '<div class=\"f_activity\">� <a href=\"{boardurl}viewthread.php?from=notice&tid={$tid}\">{$activity_subject}</a> �ķ���������׼���μӴ˻ {time}
<a href=\"{boardurl}viewthread.php?from=notice&tid={$tid}\" class=\"il to\">�鿴</a></div>',

	'activity_delete' => '<div class=\"f_activity\">� <a href=\"{boardurl}viewthread.php?from=notice&tid={$tid}\">{$activity_subject}</a> �ķ����߾ܾ����μӴ˻ {time}
<a href=\"{boardurl}viewthread.php?from=notice&tid={$tid}\" class=\"il to\">�鿴</a></div>',

	'reward_question' => '<div class=\"f_reward\">������������ <a href=\"{boardurl}viewthread.php?from=notice&tid={$thread[tid]}\">{$thread[subject]}</a> �� <a href=\"{boardurl}space.php?from=notice&uid={$discuz_uid}\">{$discuz_userss}</a> ��������Ѵ� {time}
<a href=\"{boardurl}viewthread.php?from=notice&tid={$thread[tid]}\" class=\"il to\">�鿴</a></div>',

	'reward_bestanswer' => '<div class=\"f_reward\">���Ļظ������������� <a href=\"{boardurl}viewthread.php?from=notice&tid={$thread[tid]}\">{$thread[subject]}</a> ������ <a href=\"{boardurl}space.php?from=notice&uid={$discuz_uid}\">{$discuz_userss}</a> ѡΪ������Ѵ� {time}
<a href=\"{boardurl}viewthread.php?from=notice&tid={$thread[tid]}\" class=\"il to\">�鿴</a></div>',

	'favoritethreads_notice' => '<div class=\"f_thread\">{actor}�ظ�������ע������ <a href=\"{boardurl}redirect.php?from=notice&goto=findpost&pid={$pid}&ptid={$thread[tid]}\">{$thread[subject]}</a> {time}
<a href=\"{boardurl}redirect.php?from=notice&goto=findpost&pid={$pid}&ptid={$thread[tid]}\" class=\"il to\">�鿴</a>
<dfn><a href=\"my.php?from=notice&item=attention&action=remove&tid={$thread[tid]}\" onclick=\"ajaxmenu(this, 3000);doane(event);\" class=\"deloption\" title=\"ȡ������\">ȡ������</a></dfn></div>',

	'repquote_noticeauthor' => '<div class=\"f_quote\"><a href=\"{boardurl}space.php?from=notice&uid={$discuz_uid}\">{$discuz_userss}</a> ������������������ <a href=\"{boardurl}viewthread.php?from=notice&tid={$thread[tid]}\">{$thread[subject]}</a> ��������� {time}
<dl class=\"summary\"><dt>�������ӣ�<dt><dd>{$noticeauthormsg}</dd><dt><a href=\"{boardurl}space.php?from=notice&uid={$discuz_uid}\">{$discuz_userss}</a> ˵��</dt><dd>{$postmsg}</dd></dl>
<p><a href=\"{boardurl}post.php?from=notice&action=reply&fid={$fid}&tid={$thread[tid]}&reppost={$pid}\">�ظ�</a><i>|</i><a href=\"{boardurl}redirect.php?from=notice&goto=findpost&pid={$pid}&ptid={$thread[tid]}\">�鿴</a></p></div>',

	'reppost_noticeauthor' => '<div class=\"f_reply\"><a href=\"{boardurl}space.php?from=notice&uid={$discuz_uid}\">{$discuz_userss}</a> ���������������� <a href=\"{boardurl}viewthread.php?from=notice&tid={$thread[tid]}\">{$thread[subject]}</a> ��������� {time}
<dl class=\"summary\"><dt>�������ӣ�<dt><dd>{$noticeauthormsg}</dd><dt><a href=\"{boardurl}space.php?from=notice&uid={$discuz_uid}\">{$discuz_userss}</a> ˵��</dt><dd>{$postmsg}</dd></dl>
<p><a href=\"{boardurl}post.php?from=notice&action=reply&fid={$fid}&tid={$thread[tid]}&reppost={$pid}\">�ظ�</a><i>|</i><a href=\"{boardurl}redirect.php?from=notice&goto=findpost&pid={$pid}&ptid={$thread[tid]}\">�鿴</a></p></div>',

	'magics_sell' => '<div class=\"f_magic\">���ĵ��� {$magic[name]} �� <a href=\"{boardurl}space.php?from=notice&uid={$discuz_uid}\">{$discuz_userss}</a> ���򣬻������ {$totalcredit} {time}</div>',

	'magics_receive' => '<div class=\"f_magic\">���յ� <a href=\"{boardurl}space.php?from=notice&uid={$discuz_uid}\">{$discuz_userss}</a> �͸����ĵ��� {$magicarray[$magicid][name]} {time}
<fieldset><ins>{$givemessage}</ins></fieldset>
<p><a href=\"{boardurl}magic.php\">��������</a><i>|</i><a href=\"{boardurl}magic.php?from=notice&action=mybox\" class=\"to\">ȥ�ҵĵ�����</a></p></div>',

	'magic_thread' => '<div class=\"f_magic\">������� {$thread[subject]} �� <a href=\"{boardurl}space.php?from=notice&uid=$discuz_uid\">{$discuz_user}</a> ʹ���� {$magic[name]} {time}
<a href=\"{boardurl}viewthread.php?from=notice&tid={$thread[tid]}\" class=\"il to\">��ȥ�����ɣ�</a></div>',

	'magic_thread_anonymous' => '<div class=\"f_magic\">������� {$thread[subject]} ��������ʹ���� {$magic[name]} {time}
<a href=\"{boardurl}viewthread.php?from=notice&tid={$thread[tid]}\" class=\"il to\">��ȥ�����ɣ�</a></div>',

	'magic_user' => '<div class=\"f_magic\">{$discuz_user} ����ʹ���� {$magic[name]} {time}
<a href=\"{boardurl}space.php?from=notice&uid=$discuz_uid]\" class=\"il to\">��ȥ�����ɣ�</a></div>',

	'magic_user_anonymous' => '<div class=\"f_magic\">�㱻������ʹ���� {$magic[name]}�� {time}</div>',

	'buddy_new' => '<div class=\"f_buddy\"><a href=\"{boardurl}space.php?from=notice&uid=$discuz_uid\">{$discuz_userss}</a> �����Ϊ���� {time}
<a href=\"{boardurl}my.php?from=notice&item=buddylist&newbuddyid={$discuz_uid}&buddysubmit=yes\" class=\"il to\" onclick=\"ajaxmenu(this, 3000);doane(event);\">�� {$discuz_userss} Ϊ����</a></div>',

	'buddy_new_uch' => '<div class=\"f_buddy\"><a href=\"{boardurl}space.php?from=notice&uid=$discuz_uid\">{$discuz_userss}</a> �����Ϊ���� {time}
<p><a href=\"{boardurl}my.php?from=notice&item=buddylist&newbuddyid={$discuz_uid}&buddysubmit=yes\" onclick=\"ajaxmenu(this, 3000);doane(event);\">�� {$discuz_userss} Ϊ����</a><i>|</i>
<a href=\"{$uchomeurl}/space.php?from=notice&uid={$discuz_uid}\" class=\"to\">�鿴 {$discuz_userss} �ĸ��˿ռ�</a></p></div>',

	'task_reward_credit' => '<div class=\"f_task\">��ϲ���������<a href=\"{boardurl}task.php?from=notice&action=view&id={$task[taskid]}\">{$task[name]}</a>����û��� {$extcredits[$task[prize]][title]} {$task[bonus]} {$extcredits[$task[prize]][unit]} {time}
<p><a href=\"{boardurl}memcp.php?from=notice&action=credits\">�鿴�ҵĻ���</a><i>|</i><a href=\"{boardurl}memcp.php?from=notice&action=creditslog&operation=creditslog\" class=\"il to\">�鿴���������¼</a></p></div>',

	'task_reward_magic' => '<div class=\"f_task\">��ϲ���������<a href=\"{boardurl}task.php?from=notice&action=view&id={$task[taskid]}\">{$task[name]}</a>����õ��� <a href=\"{boardurl}magic.php\">{$magicname}</a> {$task[bonus]} ö {time}</div>',

	'task_reward_medal' => '<div class=\"f_task\">��ϲ���������<a href=\"{boardurl}task.php?from=notice&action=view&id={$task[taskid]}\">{$task[name]}</a>�����ѫ�� <a href=\"{boardurl}medal.php\">{$medalname}</a> ��Ч�� {$task[bonus]} �� {time}</div>',

	'task_reward_invite' => '<div class=\"f_task\">��ϲ���������<a href=\"{boardurl}task.php?from=notice&action=view&id={$task[taskid]}\">{$task[name]}</a>����������� <a href=\"{boardurl}invite.php\">{$task[prize]}</a> ����Ч�� {$task[bonus]} �� {time}
<dl class=\"summary\"><dt>�����룺</dt><dd>{$rewards}</dd></dl></div>',

	'task_reward_group' => '<div class=\"f_task\">��ϲ���������<a href=\"{boardurl}task.php?from=notice&action=view&id={$task[taskid]}\">{$task[name]}</a>������û��� {$grouptitle} ��Ч�� {$task[bonus]} �� {time}
<a href=\"{boardurl}faq.php?from=notice&action=grouppermission\" class=\"il to\">����������ʲô</a></div>',

	'thread_views' => '<div>�������� {subject} �鿴�������� {count} {time}</div>',

	'thread_replies' => '<div>�������� {subject} �ظ��������� {count} {time}</div>',

	'thread_rate' => '<div>�������� {subject} ���ֳ����� {count} {time}</div>',

	'post_rate' => '<div>���� {thread} �Ļظ����ֳ�����{count} {time}</div>',

	'user_usergroup' => '<div>�����û�������Ϊ {usergroup} {time}
<a href=\"{boardurl}faq.php?from=notice&action=grouppermission\" class=\"il to\">����������ʲô</a></div>',

	'user_credit' => '<div>�����ܻ��ִﵽ {count} {time}</div>',

	'user_threads' => '<div>��������������ﵽ {count} {time}</div>',

	'user_posts' =>	'<div>���ķ������ﵽ {count} {time}</div>',

	'user_digest' => '<div>���ľ��������ﵽ {count} {time}</div>',

);

?>