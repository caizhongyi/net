<?php

define('UC_VERNAME', '���İ�');

$lang = array(

	'SC_GBK' => '�������İ�',
	'TC_BIG5' => '�������İ�',
	'SC_UTF8' => '�������� UTF8 ��',
	'TC_UTF8' => '�������� UTF8 ��',
	'EN_ISO' => 'ENGLISH ISO8859',
	'EN_UTF8' => 'ENGLIST UTF-8',

	'title_install' => SOFT_NAME.' ��װ��',
	'agreement_yes' => '��ͬ��',
	'agreement_no' => '�Ҳ�ͬ��',
	'notset' => '������',

	'message_title' => '��ʾ��Ϣ',
	'error_message' => '������Ϣ',
	'message_return' => '����',
	'return' => '����',
	'install_wizard' => '��װ��',
	'config_nonexistence' => '�����ļ�������',
	'nodir' => 'Ŀ¼������',
	'short_open_tag_invalid' => '�Բ����뽫 php.ini �е� short_open_tag ����Ϊ On�������޷�������װ��',
	'redirect' => '��������Զ���תҳ�棬�����˹���Ԥ��<br>���ǵ����������û���Զ���תʱ����������',
	'auto_redirect' => '��������Զ���תҳ�棬�����˹���Ԥ',
	'database_errno_2003' => '�޷��������ݿ⣬�������ݿ��Ƿ����������ݿ��������ַ�Ƿ���ȷ',
	'database_errno_1044' => '�޷������µ����ݿ⣬�������ݿ�������д�Ƿ���ȷ',
	'database_errno_1045' => '�޷��������ݿ⣬�������ݿ��û������������Ƿ���ȷ',
	'database_errno_1064' => 'SQL �﷨����',

	'dbpriv_createtable' => 'û��CREATE TABLEȨ�ޣ��޷�������װ',
	'dbpriv_insert' => 'û��INSERTȨ�ޣ��޷�������װ',
	'dbpriv_select' => 'û��SELECTȨ�ޣ��޷�������װ',
	'dbpriv_update' => 'û��UPDATEȨ�ޣ��޷�������װ',
	'dbpriv_delete' => 'û��DELETEȨ�ޣ��޷�������װ',
	'dbpriv_droptable' => 'û��DROP TABLEȨ�ޣ��޷���װ',

	'db_not_null' => '���ݿ����Ѿ���װ�� UCenter, ������װ�����ԭ�����ݡ�',
	'db_drop_table_confirm' => '������װ�����ȫ��ԭ�����ݣ���ȷ��Ҫ������?',

	'writeable' => '��д',
	'unwriteable' => '����д',
	'old_step' => '��һ��',
	'new_step' => '��һ��',

	'database_errno_2003' => '�޷��������ݿ⣬�������ݿ��Ƿ����������ݿ��������ַ�Ƿ���ȷ',
	'database_errno_1044' => '�޷������µ����ݿ⣬�������ݿ�������д�Ƿ���ȷ',
	'database_errno_1045' => '�޷��������ݿ⣬�������ݿ��û������������Ƿ���ȷ',
	'database_connect_error' => '���ݿ����Ӵ���',

	'step_env_check_title' => '��ʼ��װ',
	'step_env_check_desc' => '�����Լ��ļ�Ŀ¼Ȩ�޼��',
	'step_db_init_title' => '��װ���ݿ�',
	'step_db_init_desc' => '����ִ�����ݿⰲװ',

	'step1_file' => 'Ŀ¼�ļ�',
	'step1_need_status' => '����״̬',
	'step1_status' => '��ǰ״̬',
	'not_continue' => '�뽫���Ϻ�沿����������',

	'tips_dbinfo' => '��д���ݿ���Ϣ',
	'tips_dbinfo_comment' => '',
	'tips_admininfo' => '��д����Ա��Ϣ',
	'step_ext_info_title' => '��װ�ɹ�',
	'step_ext_info_comment' => '��������½',

	'ext_info_succ' => '��װ�ɹ�',
	'install_submit' => '�ύ',
	'install_locked' => '��װ�������Ѿ���װ���ˣ������ȷ��Ҫ���°�װ���뵽��������ɾ��<br /> '.str_replace(ROOT_PATH, '', $lockfile),
	'error_quit_msg' => '���������������⣬��װ�ſ��Լ���',

	'step_app_reg_title' => '�������л���',
	'step_app_reg_desc' => '�������������Լ����� UCenter',
	'tips_ucenter' => '����д UCenter �����Ϣ',
	'tips_ucenter_comment' => 'UCenter �� Comsenz ��˾��Ʒ�ĺ��ķ������Discuz! Board �İ�װ�����������˳���������Ѿ���װ�� UCenter������д������Ϣ�������뵽 <a href="http://www.discuz.com/" target="blank">Comsenz ��Ʒ����</a> ���ز��Ұ�װ��Ȼ���ټ�����',

	'advice_mysql_connect' => '���� mysql ģ���Ƿ���ȷ����',
	'advice_fsockopen' => '�ú�����Ҫ php.ini �� allow_url_fopen ѡ���������ϵ�ռ��̣�ȷ�������˴����',
	'advice_gethostbyname' => '�Ƿ�php�����н�ֹ��gethostbyname����������ϵ�ռ��̣�ȷ�������˴����',
	'advice_file_get_contents' => '�ú�����Ҫ php.ini �� allow_url_fopen ѡ���������ϵ�ռ��̣�ȷ�������˴����',
	'advice_xml_parser_create' => '�ú�����Ҫ PHP ֧�� XML������ϵ�ռ��̣�ȷ�������˴����',

	'ucurl' => 'UCenter �� URL',
	'ucpw' => 'UCenter ��ʼ������',
	'ucip' => 'UCenter ��IP��ַ',
	'ucenter_ucip_invalid' => '��ʽ��������д��ȷ�� IP ��ַ',
	'ucip_comment' => '�����������������Բ���',

	'tips_siteinfo' => '����дվ����Ϣ',
	'sitename' => 'վ������',
	'siteurl' => 'վ�� URL',

	'forceinstall' => 'ǿ�ư�װ',
	'dbinfo_forceinstall_invalid' => '��ǰ���ݿ⵱���Ѿ�����ͬ����ǰ׺�����ݱ��������޸ġ�����ǰ׺��������ɾ���ɵ����ݣ�����ѡ��ǿ�ư�װ��ǿ�ư�װ��ɾ�������ݣ����޷��ָ�',

	'click_to_back' => '���������һ��',
	'adminemail' => 'ϵͳ���� Email',
	'adminemail_comment' => '���ڷ��ͳ�����󱨸�',
	'dbhost_comment' => '���ݿ��������ַ, һ��Ϊ localhost',
	'tablepre_comment' => 'ͬһ���ݿ����ж����̳ʱ�����޸�ǰ׺',
	'forceinstall_check_label' => '��Ҫɾ�����ݣ�ǿ�ư�װ !!!',

	'uc_url_empty' => '��û����д UCenter �� URL���뷵����д',
	'uc_url_invalid' => 'URL ��ʽ����',
	'uc_url_unreachable' => 'UCenter �� URL ��ַ������д��������',
	'uc_ip_invalid' => '�޷�����������������дվ��� IP',
	'uc_admin_invalid' => 'UCenter ��ʼ�����������������д',
	'uc_data_invalid' => 'ͨ��ʧ�ܣ����� UCenter ��URL ��ַ�Ƿ���ȷ ',
	'uc_dbcharset_incorrect' => 'UCenter ���ݿ��ַ����뵱ǰӦ���ַ�����һ��',
	'uc_api_add_app_error' => '�� UCenter ���Ӧ�ô���',
	'uc_dns_error' => 'UCenter DNS���������뷵����дһ�� UCenter �� IP��ַ',

	'ucenter_ucurl_invalid' => 'UCenter ��URLΪ�գ����߸�ʽ��������',
	'ucenter_ucpw_invalid' => 'UCenter �Ĵ�ʼ������Ϊ�գ����߸�ʽ��������',
	'siteinfo_siteurl_invalid' => 'վ��URLΪ�գ����߸�ʽ��������',
	'siteinfo_sitename_invalid' => 'վ������Ϊ�գ����߸�ʽ��������',
	'dbinfo_dbhost_invalid' => '���ݿ������Ϊ�գ����߸�ʽ��������',
	'dbinfo_dbname_invalid' => '���ݿ���Ϊ�գ����߸�ʽ��������',
	'dbinfo_dbuser_invalid' => '���ݿ��û���Ϊ�գ����߸�ʽ��������',
	'dbinfo_dbpw_invalid' => '���ݿ�����Ϊ�գ����߸�ʽ��������',
	'dbinfo_adminemail_invalid' => 'ϵͳ����Ϊ�գ����߸�ʽ��������',
	'dbinfo_tablepre_invalid' => '���ݱ�ǰ׺Ϊ�գ����߸�ʽ��������',
	'admininfo_username_invalid' => '����Ա�û���Ϊ�գ����߸�ʽ��������',
	'admininfo_email_invalid' => '����ԱEmailΪ�գ����߸�ʽ��������',
	'admininfo_password_invalid' => '����Ա����Ϊ�գ�����д',
	'admininfo_password2_invalid' => '�������벻һ�£�����',

	'username' => '����Ա�˺�',
	'email' => '����Ա Email',
	'password' => '����Ա����',
	'password_comment' => '����Ա���벻��Ϊ��',
	'password2' => '�ظ�����',

	'admininfo_invalid' => '����Ա��Ϣ���������������Ա�˺ţ����룬����',
	'dbname_invalid' => '���ݿ���Ϊ�գ�����д���ݿ�����',
	'tablepre_invalid' => '���ݱ�ǰ׺Ϊ�գ����߸�ʽ��������',
	'admin_username_invalid' => '�Ƿ��û������û������Ȳ�Ӧ������ 15 ��Ӣ���ַ����Ҳ��ܰ��������ַ���һ�������ģ���ĸ��������',
	'admin_password_invalid' => '��������治һ�£�����������',
	'admin_email_invalid' => 'Email ��ַ���󣬴��ʼ���ַ�Ѿ���ʹ�û��߸�ʽ��Ч�������Ϊ������ַ',
	'admin_invalid' => '������Ϣ����Ա��Ϣû����д����������ϸ��дÿ����Ŀ',
	'admin_exist_password_error' => '���û��Ѿ����ڣ������Ҫ���ô��û�Ϊ��̳�Ĺ���Ա������ȷ������û������룬�����������̳����Ա������',

	'tagtemplates_subject' => '����',
	'tagtemplates_uid' => '�û� ID',
	'tagtemplates_username' => '������',
	'tagtemplates_dateline' => '����',
	'tagtemplates_url' => '�����ַ',

	'uc_version_incorrect' => '���� UCenter ����˰汾���ͣ������� UCenter ����˵����°汾���������������ص�ַ��http://www.comsenz.com/ ��',
	'config_unwriteable' => '��װ���޷�д�������ļ�, ������ config.inc.php ��������Ϊ��д״̬(777)',

	'install_in_processed' => '���ڰ�װ...',
	'install_succeed' => '��װ�ɹ����������',
	'install_founder_contact' => '������һ����д��ϵ��ʽ',

	'init_credits_karma' => '����',
	'init_credits_money' => '��Ǯ',

	'init_group_0' => '��Ա',
	'init_group_1' => '����Ա',
	'init_group_2' => '��������',
	'init_group_3' => '����',
	'init_group_4' => '��ֹ����',
	'init_group_5' => '��ֹ����',
	'init_group_6' => '��ֹ IP',
	'init_group_7' => '�ο�',
	'init_group_8' => '�ȴ���֤��Ա',
	'init_group_9' => '��ؤ',
	'init_group_10' => '������·',
	'init_group_11' => 'ע���Ա',
	'init_group_12' => '�м���Ա',
	'init_group_13' => '�߼���Ա',
	'init_group_14' => '���ƻ�Ա',
	'init_group_15' => '��̳Ԫ��',

	'init_rank_1' => '������ѧ',
	'init_rank_2' => 'С��ţ��',
	'init_rank_3' => 'ʵϰ����',
	'init_rank_4' => '����׫����',
	'init_rank_5' => '��Ƹ����',

	'init_cron_1' => '��ս��շ�����',
	'init_cron_2' => '��ձ�������ʱ��',
	'init_cron_3' => 'ÿ����������',
	'init_cron_4' => '����ͳ�����ʼ�ף��',
	'init_cron_5' => '����ظ�֪ͨ',
	'init_cron_6' => 'ÿ�չ�������',
	'init_cron_7' => '��ʱ��������',
	'init_cron_8' => '��̳�ƹ�����',
	'init_cron_9' => 'ÿ����������',
	'init_cron_10' => 'ÿ�� X-Space�����û�',
	'init_cron_11' => 'ÿ���������',

	'init_bbcode_1' => 'ʹ���ݺ�����������Ч������ HTML �� marquee ��ǩ��ע�⣺���Ч��ֻ�� Internet Explorer ���������Ч��',
	'init_bbcode_2' => 'Ƕ�� Flash ����',
	'init_bbcode_3' => '��ʾ QQ ����״̬�������ͼ����Ժ�������������',
	'init_bbcode_4' => '�ϱ�',
	'init_bbcode_5' => '�±�',
	'init_bbcode_6' => 'Ƕ�� Windows media ��Ƶ',
	'init_bbcode_7' => 'Ƕ�� Windows media ��Ƶ����Ƶ',

	'init_qihoo_searchboxtxt' =>'����ؼ���,������������̳',
	'init_threadsticky' =>'ȫ���ö�,�����ö�,�����ö�',

	'init_default_style' => 'Ĭ�Ϸ��',
	'init_default_forum' => 'Ĭ�ϰ��',
	'init_default_template' => 'Ĭ��ģ����ϵ',
	'init_default_template_copyright' => '��ʢ���루�������Ƽ����޹�˾',

	'init_dataformat' => 'Y-n-j',
	'init_modreasons' => '���/SPAM\r\n�����ˮ\r\nΥ������\r\n�Ĳ�����\r\n�ظ�����\r\n\r\n�Һ���ͬ\r\n��Ʒ����\r\nԭ������',
	'init_link' => 'Discuz! �ٷ���̳',
	'init_link_note' => '�ṩ���� Discuz! ��Ʒ���š���������뼼������',

	'license' => '<div class="license"><h1>���İ���ȨЭ�� �����������û�</h1>

<p>��Ȩ���� (c) 2001-2009����ʢ���루�������Ƽ����޹�˾��������Ȩ����</p>

<p>��л��ѡ�� Discuz! ��̳��Ʒ��ϣ�����ǵ�Ŭ����Ϊ���ṩһ����Ч���ٺ�ǿ���������̳���������</p>

<p>Discuz! Ӣ��ȫ��Ϊ Crossday Discuz! Board������ȫ��Ϊ Discuz! ��̳�����¼�� Discuz!��</p>

<p>��ʢ���루�������Ƽ����޹�˾Ϊ Discuz! ��Ʒ�Ŀ����̣���������ӵ�� Discuz! ��Ʒ����Ȩ���й����Ұ�Ȩ������Ȩ�ǼǺ� 2006SR11895������ʢ���루�������Ƽ����޹�˾��ַΪ http://www.comsenz.com��Discuz! �ٷ���վ��ַΪ http://www.discuz.com��Discuz! �ٷ���������ַΪ http://www.discuz.net��</p>

<p>Discuz! ����Ȩ�����л����񹲺͹����Ұ�Ȩ��ע�ᣬ����Ȩ�ܵ����ɺ͹��ʹ�Լ������ʹ���ߣ����۸��˻���֯��ӯ�������;��Σ�������ѧϰ���о�ΪĿ�ģ���������ϸ�Ķ���Э�飬����⡢ͬ�⡢�����ر�Э���ȫ������󣬷��ɿ�ʼʹ�� Discuz! �����</p>

<p>����ȨЭ�������ҽ������� Discuz! 7.x.x �汾����ʢ���루�������Ƽ����޹�˾ӵ�жԱ���ȨЭ������ս���Ȩ��</p>

<h3>I. Э����ɵ�Ȩ��</h3>
<ol>
<li>����������ȫ���ر������û���ȨЭ��Ļ����ϣ��������Ӧ���ڷ���ҵ��;��������֧�������Ȩ��Ȩ���á�</li>
<li>��������Э��涨��Լ�������Ʒ�Χ���޸� Discuz! Դ����(������ṩ�Ļ�)�����������Ӧ������վҪ��</li>
<li>��ӵ��ʹ�ñ������������̳��ȫ����Ա���ϡ����¼������Ϣ������Ȩ���������е����������ݵ���ط�������</li>
<li>�����ҵ��Ȩ֮�������Խ������Ӧ������ҵ��;��ͬʱ�������������Ȩ������ȷ���ļ���֧�����ޡ�����֧�ַ�ʽ�ͼ���֧�����ݣ��Թ���ʱ�����ڼ���֧��������ӵ��ͨ��ָ���ķ�ʽ���ָ����Χ�ڵļ���֧�ַ�����ҵ��Ȩ�û����з�ӳ����������Ȩ����������������Ϊ��Ҫ���ǣ���û��һ�������ɵĳ�ŵ��֤��</li>
</ol>

<h3>II. Э��涨��Լ��������</h3>
<ol>
<li>δ����ҵ��Ȩ֮ǰ�����ý������������ҵ��;����������������ҵ��վ����Ӫ����վ����Ӫ��ΪĿ��ʵ��ӯ������վ����������ҵ��Ȩ���½http://www.discuz.com�ο����˵����Ҳ�����µ�8610-51657885�˽����顣</li>
<li>���öԱ��������֮��������ҵ��Ȩ���г��⡢���ۡ���Ѻ�򷢷������֤��</li>
<li>������Σ���������;��Ρ��Ƿ񾭹��޸Ļ��������޸ĳ̶���Σ�ֻҪʹ�� Discuz! ��������κβ��֣�δ��������ɣ���̳ҳ��ҳ�Ŵ��� Discuz! ���ƺͿ�ʢ���루�������Ƽ����޹�˾������վ��http://www.comsenz.com��http://www.discuz.com �� http://www.discuz.net�� �����Ӷ����뱣����������������޸ġ�</li>
<li>��ֹ�� Discuz! ��������κβ��ֻ������Է�չ�κ������汾���޸İ汾��������汾�������·ַ���</li>
<li>�����δ�����ر�Э������������Ȩ������ֹ��������ɵ�Ȩ�������ջأ����е���Ӧ�������Ρ�</li>
</ol>

<h3>III. ���޵�������������</h3>
<ol>
<li>����������������ļ�����Ϊ���ṩ�κ���ȷ�Ļ��������⳥�򵣱�����ʽ�ṩ�ġ�</li>
<li>�û�������Ը��ʹ�ñ�������������˽�ʹ�ñ�����ķ��գ�����δ�����Ʒ��������֮ǰ�����ǲ���ŵ�ṩ�κ���ʽ�ļ���֧�֡�ʹ�õ�����Ҳ���е��κ���ʹ�ñ���������������������Ρ�</li>
<li>��ʢ���루�������Ƽ����޹�˾����ʹ�ñ������������̳�е����»���Ϣ�е����Ρ�</li>
</ol>

<p>�й� Discuz! �����û���ȨЭ�顢��ҵ��Ȩ�뼼���������ϸ���ݣ����� Discuz! �ٷ���վ�����ṩ����ʢ���루�������Ƽ����޹�˾ӵ���ڲ�����֪ͨ������£��޸���ȨЭ��ͷ����Ŀ���Ȩ�����޸ĺ��Э����Ŀ����Ըı�֮���������Ȩ�û���Ч��</p>

<p>�����ı���ʽ����ȨЭ����ͬ˫������ǩ���Э��һ����������ȫ�ĺ͵�ͬ�ķ���Ч������һ����ʼ��װ Discuz!��������Ϊ��ȫ��Ⲣ���ܱ�Э��ĸ�������������������������Ȩ����ͬʱ���ܵ���ص�Լ�������ơ�Э����ɷ�Χ�������Ϊ����ֱ��Υ������ȨЭ�鲢������Ȩ��������Ȩ��ʱ��ֹ��Ȩ������ֹͣ�𺦣�������׷��������ε�Ȩ����</p></div>',

	'uc_installed' => '���Ѿ���װ�� UCenter�������Ҫ���°�װ����ɾ�� data/install.lock �ļ�',
	'i_agree' => '������ϸ�Ķ�����ͬ�����������е���������',
	'supportted' => '֧��',
	'unsupportted' => '��֧��',
	'max_size' => '֧��/���ߴ�',
	'project' => '��Ŀ',
	'ucenter_required' => 'Discuz! ��������',
	'ucenter_best' => 'Discuz! ���',
	'curr_server' => '��ǰ������',
	'env_check' => '�������',
	'os' => '����ϵͳ',
	'php' => 'PHP �汾',
	'attachmentupload' => '�����ϴ�',
	'unlimit' => '������',
	'version' => '�汾',
	'gdversion' => 'GD ��',
	'allow' => '����',
	'unix' => '��Unix',
	'diskspace' => '���̿ռ�',
	'priv_check' => 'Ŀ¼���ļ�Ȩ�޼��',
	'func_depend' => '���������Լ��',
	'func_name' => '��������',
	'check_result' => '�����',
	'suggestion' => '����',
	'advice_mysql' => '���� mysql ģ���Ƿ���ȷ����',
	'advice_fopen' => '�ú�����Ҫ php.ini �� allow_url_fopen ѡ���������ϵ�ռ��̣�ȷ�������˴����',
	'advice_file_get_contents' => '�ú�����Ҫ php.ini �� allow_url_fopen ѡ���������ϵ�ռ��̣�ȷ�������˴����',
	'advice_xml' => '�ú�����Ҫ PHP ֧�� XML������ϵ�ռ��̣�ȷ�������˴����',
	'none' => '��',

	'dbhost' => '���ݿ������',
	'dbuser' => '���ݿ��û���',
	'dbpw' => '���ݿ�����',
	'dbname' => '���ݿ���',
	'tablepre' => '���ݱ�ǰ׺',

	'ucfounderpw' => '��ʼ������',
	'ucfounderpw2' => '�ظ���ʼ������',

	'init_log' => '��ʼ����¼',
	'clear_dir' => '���Ŀ¼',
	'select_db' => 'ѡ�����ݿ�',
	'create_table' => '�������ݱ�',
	'succeed' => '�ɹ�',

	'testdata' => '��װ��������',
	'testdata_check_label' => '��',
	'install_test_data' => '���ڰ�װ��������',

	'method_undefined' => 'δ���巽��',
	'database_nonexistence' => '���ݿ�������󲻴���',
	'founder_contact' => '<h4>���ڡ���ʢ���Ƽƻ�����˵��</h4>

	Ϊ�˲��ϸĽ���Ʒ�����������û����飬Discuz!7.1����ʢ���Ƽƻ�������ϵͳ���������Ƿ����û�����̳�Ĳ���ϰ�ߣ���������������δ���İ汾�жԲ�Ʒ���иĽ�����Ƴ��������û�������¹��ܡ�

	��ϵͳ�����ռ�վ��������Ϣ�����ռ��û����ϣ������ڰ�ȫ���գ����Ҿ���ʵ�ʲ��Բ���Ӱ����̳������Ч�ʡ�

	����װʹ�ñ��汾��ʾ��ͬ����롶��ʢ���Ƽƻ�����Discuz!��Ӫ���Ż�ͨ����վ��ķ���Ϊ���ṩ��Ӫָ�����飬���ǽ���ʾ����θ���վ���������������̳���ܣ���ν��к���Ĺ������ã��Լ��ṩ������һЩ��Ӫ����ȡ�

	Ϊ�˷������Ǻ�����ͨ��Ӫ���ԣ��������³��õ�������ϵ��ʽ',
	'skip_current' => '��������',

);

$msglang = array(

	'config_nonexistence' => '���� config.inc.php ������, �޷�������װ, ���� FTP �����ļ��ϴ������ԡ�',
);

$optionlist = array (
	8 => array (
		'classid' => '1',
		'displayorder' => '2',
		'title' => '�Ա�',
		'identifier' => 'gender',
		'type' => 'radio',
		'rules' => array (
			      'required' => '0',
			      'unchangeable' => '0',
			      'choices' => "1=��\r\n2=Ů",
			   ),
		),
	16 => array (
		'classid' => '2',
		'displayorder' => '0',
		'title' => '��������',
		'identifier' => 'property',
		'type' => 'select',
		'rules' => array (
			      'choices' => "1=д��¥\r\n2=��Ԣ\r\n3=С��\r\n4=ƽ��\r\n5=����\r\n6=������",
			   ),
		),
	17 => array (
		'classid' => '2',
		'displayorder' => '0',
		'title' => '����',
		'identifier' => 'face',
		'type' => 'radio',
	    	'rules' => array (
	      			'required' => '0',
	      			'unchangeable' => '0',
	      			'choices' => "1=����\r\n2=����\r\n3=����\r\n4=����",
	    		),
	  	),
      18 => array (
        	'classid' => '2',
        	'displayorder' => '0',
        	'title' => 'װ�����',
        	'identifier' => 'makes',
        	'type' => 'radio',
        	'rules' => array (
          			'required' => '0',
          			'unchangeable' => '0',
          			'choices' => "1=��װ��\r\n2=��װ��\r\n3=��װ��",
        		),
      	),
      19 => array (
        	'classid' => '2',
        	'displayorder' => '0',
        	'title' => '����',
        	'identifier' => 'mode',
        	'type' => 'select',
        	'rules' => array (
          			'choices' => "1=����\r\n2=������\r\n3=������\r\n4=�ľ���\r\n5=����",
        		),
      	),
      23 => array (
        	'classid' => '2',
        	'displayorder' => '0',
        	'title' => '������ʩ',
        	'identifier' => 'equipment',
        	'type' => 'checkbox',
        	'rules' => array (
          			'required' => '0',
          			'unchangeable' => '0',
          			'choices' => "1=ˮ��\r\n2=���\r\n3=�ܵ���\r\n4=���ߵ���\r\n5=����\r\n6=�绰\r\n7=����\r\n8=ϴ�»�\r\n9=��ˮ��\r\n10=�յ�\r\n11=ů��\r\n12=΢��¯\r\n13=���̻�\r\n14=��ˮ��",
       		),
      	),
      25 => array (
        	'classid' => '2',
        	'displayorder' => '0',
        	'title' => '�Ƿ��н�',
        	'identifier' => 'bool',
        	'type' => 'radio',
        	'rules' => array (
          			'required' => '0',
          			'unchangeable' => '0',
          			'choices' => "1=��\r\n2=��",
        		),
      	),
      27 => array (
        	'classid' => '3',
       	'displayorder' => '0',
        	'title' => '����',
        	'identifier' => 'Horoscope',
        	'type' => 'select',
        	'rules' => array (
          			'choices' => "1=������\r\n2=��ţ��\r\n3=˫����\r\n4=��з��\r\n5=ʨ����\r\n6=��Ů��\r\n7=�����\r\n8=��Ы��\r\n9=������\r\n10=Ħ����\r\n11=ˮƿ��\r\n12=˫����",
        		),
      	),
      30 => array (
        	'classid' => '3',
        	'displayorder' => '0',
        	'title' => '����״��',
        	'identifier' => 'marrige',
        	'type' => 'radio',
        	'rules' => array (
          			'choices' => "1=�ѻ�\r\n2=δ��",
        		),
      	),
      31 => array (
        	'classid' => '3',
        	'displayorder' => '0',
        	'title' => '����',
        	'identifier' => 'hobby',
        	'type' => 'checkbox',
        	'rules' => array (
          			'choices' => "1=��ʳ\r\n2=����\r\n3=����\r\n4=��Ӱ\r\n5=����\r\n6=Ϸ��\r\n7=����\r\n8=����\r\n9=����\r\n10=����\r\n11=��Ϸ\r\n12=�滭\r\n13=�鷨\r\n14=����\r\n15=����\r\n16=�Ķ�\r\n17=�˶�\r\n18=����\r\n19=����\r\n20=����\r\n21=׬Ǯ\r\n22=����\r\n23=��Ӱ",
        		),
      	),
      32 => array (
        	'classid' => '3',
        	'displayorder' => '0',
        	'title' => '���뷶Χ',
        	'identifier' => 'salary',
        	'type' => 'select',
        	'rules' => array (
          			'required' => '0',
          			'unchangeable' => '0',
          			'choices' => "1=����\r\n2=800Ԫ����\r\n3=1500Ԫ����\r\n4=2000Ԫ����\r\n5=3000Ԫ����\r\n6=5000Ԫ����\r\n7=8000Ԫ����",
        		),
      	),
      34 => array (
        	'classid' => '1',
        	'displayorder' => '0',
        	'title' => 'ѧ��',
        	'identifier' => 'education',
        	'type' => 'radio',
        	'rules' => array (
          			'required' => '0',
          			'unchangeable' => '0',
          			'choices' => "1=��ä\r\n2=Сѧ\r\n3=����\r\n4=����\r\n5=��ר\r\n6=��ר\r\n7=����\r\n8=�о���\r\n9=��ʿ",
        		),
      	),
      38 => array (
        	'classid' => '5',
        	'displayorder' => '0',
        	'title' => 'ϯ��',
        	'identifier' => 'seats',
        	'type' => 'select',
        	'rules' => array (
          			'choices' => "1=վƱ\r\n2=Ӳ��\r\n3=����\r\n4=Ӳ��\r\n5=����",
        		),
      	),
      44 => array (
        	'classid' => '4',
        	'displayorder' => '0',
        	'title' => '�Ƿ�Ӧ��',
        	'identifier' => 'recr_term',
        	'type' => 'radio',
        	'rules' => array (
    		      	'required' => '0',
    		      	'unchangeable' => '0',
    		      	'choices' => "1=Ӧ��\r\n2=��Ӧ��",
        		),
      	),
      48 => array (
        	'classid' => '4',
        	'displayorder' => '0',
        	'title' => 'н��',
        	'identifier' => 'recr_salary',
        	'type' => 'select',
        	'rules' => array (
          			'choices' => "1=����\r\n2=1000����\r\n3=1000~1500\r\n4=1500~2000\r\n5=2000~3000\r\n6=3000~4000\r\n7=4000~6000\r\n8=6000~8000\r\n9=8000����",
        		),
      	),
      50 => array (
        	'classid' => '4',
        	'displayorder' => '0',
        	'title' => '��������',
        	'identifier' => 'recr_work',
        	'type' => 'radio',
        	'rules' => array (
          			'required' => '0',
          			'unchangeable' => '0',
          			'choices' => "1=ȫְ\r\n2=��ְ",
        		),
      	),
      53 => array (
        	'classid' => '4',
        	'displayorder' => '0',
        	'title' => '�Ա�Ҫ��',
        	'identifier' => 'recr_sex',
        	'type' => 'checkbox',
        	'rules' => array (
          			'required' => '0',
          			'unchangeable' => '0',
          			'choices' => "1=��\r\n2=Ů",
        		),
      	),
      62 => array (
        	'classid' => '5',
        	'displayorder' => '0',
        	'title' => '���ʽ',
        	'identifier' => 'pay_type',
        	'type' => 'checkbox',
        	'rules' => array (
          			'required' => '0',
          			'unchangeable' => '0',
          			'choices' => "1=���\r\n2=֧����\r\n3=�ֽ�\r\n4=����",
        		),
      	),
);

$request_data = array (
  '����ģ��_��������б�' =>
  array (
    'url' => 'function=module&module=forumtree.inc.php&settings=N%3B&jscharset=0&cachelife=864000',
    'parameter' =>
    array (
      'module' => 'forumtree.inc.php',
      'cachelife' => '864000',
      'jscharset' => '0',
    ),
    'comment' => '������������б�ģ��',
    'type' => '5',
  ),
  '����ģ��_��������' =>
  array (
    'url' => 'function=module&module=modlist.inc.php&settings=N%3B&jscharset=0&cachelife=3600',
    'parameter' =>
    array (
      'module' => 'modlist.inc.php',
      'cachelife' => '3600',
      'jscharset' => '0',
    ),
    'comment' => '������������ģ��',
    'type' => '5',
  ),
  '�ۺ�ģ��_����б�' =>
  array (
    'url' => 'function=module&module=rowcombine.inc.php&settings=a%3A1%3A%7Bs%3A4%3A%22data%22%3Bs%3A58%3A%22%B1%DF%C0%B8%C4%A3%BF%E9_%B0%E6%BF%E9%C5%C5%D0%D0%2C%B0%E6%BF%E9%C5%C5%D0%D0%0D%0A%B1%DF%C0%B8%C4%A3%BF%E9_%B0%E6%BF%E9%CA%F7%D0%CE%C1%D0%B1%ED%2C%B0%E6%BF%E9%C1%D0%B1%ED%22%3B%7D&jscharset=0&cachelife=864000',
    'parameter' =>
    array (
      'module' => 'rowcombine.inc.php',
      'cachelife' => '864000',
      'settings' =>
      array (
        'data' => '����ģ��_�������,�������
����ģ��_��������б�,����б�',
      ),
      'jscharset' => '0',
    ),
    'comment' => '���Ű�顢������ξۺ�ģ��',
    'type' => '5',
  ),
  '����ģ��_�������' =>
  array (
    'url' => 'function=forums&startrow=0&items=0&newwindow=1&orderby=posts&jscharset=0&cachelife=43200&jstemplate=%3Cdiv%20class%3D%5C%22sidebox%5C%22%3E%0D%0A%3Ch4%3E%B0%E6%BF%E9%C5%C5%D0%D0%3C%2Fh4%3E%0D%0A%3Cul%20class%3D%5C%22textinfolist%5C%22%3E%0D%0A%5Bnode%5D%3Cli%3E%3Cimg%20style%3D%5C%22vertical-align%3Amiddle%5C%22%20src%3D%5C%22images%2Fdefault%2Ftree_file.gif%5C%22%20%2F%3E%20%7Bforumname%7D%28%7Bposts%7D%29%3C%2Fli%3E%5B%2Fnode%5D%0D%0A%3C%2Ful%3E%0D%0A%3C%2Fdiv%3E',
    'parameter' =>
    array (
      'jstemplate' => '<div class=\\"sidebox\\">
<h4>�������</h4>
<ul class=\\"textinfolist\\">
[node]<li><img style=\\"vertical-align:middle\\" src=\\"images/default/tree_file.gif\\" /> {forumname}({posts})</li>[/node]
</ul>
</div>',
      'cachelife' => '43200',
      'startrow' => '0',
      'items' => '0',
      'newwindow' => 1,
      'orderby' => 'posts',
      'jscharset' => '0',
    ),
    'comment' => '�����������ģ��',
    'type' => '1',
  ),
  '�ۺ�ģ��_��������' =>
  array (
    'url' => 'function=module&module=rowcombine.inc.php&settings=a%3A2%3A%7Bs%3A5%3A%22title%22%3Bs%3A8%3A%22%C8%C8%C3%C5%D6%F7%CC%E2%22%3Bs%3A4%3A%22data%22%3Bs%3A79%3A%22%B1%DF%C0%B8%C4%A3%BF%E9_%C8%C8%C3%C5%D6%F7%CC%E2_%BD%F1%C8%D5%2C%C8%D5%0D%0A%B1%DF%C0%B8%C4%A3%BF%E9_%C8%C8%C3%C5%D6%F7%CC%E2_%B1%BE%D6%DC%2C%D6%DC%0D%0A%B1%DF%C0%B8%C4%A3%BF%E9_%C8%C8%C3%C5%D6%F7%CC%E2_%B1%BE%D4%C2%2C%D4%C2%22%3B%7D&jscharset=0&cachelife=1800',
    'parameter' =>
    array (
      'module' => 'rowcombine.inc.php',
      'cachelife' => '1800',
      'settings' =>
      array (
        'title' => '��������',
        'data' => '����ģ��_��������_����,��
����ģ��_��������_����,��
����ģ��_��������_����,��',
      ),
      'jscharset' => '0',
    ),
    'comment' => '���ա����ܡ�������������ۺ�ģ��',
    'type' => '5',
  ),
  '����ģ��_��������_����' =>
  array (
    'url' => 'function=threads&sidestatus=0&maxlength=20&fnamelength=0&messagelength=&startrow=0&picpre=images%2Fcommon%2Fslisticon.gif&items=5&tag=&tids=&special=0&rewardstatus=&digest=0&stick=0&recommend=0&newwindow=1&threadtype=0&highlight=0&orderby=hourviews&hours=720&jscharset=0&cachelife=86400&jstemplate=%3Cdiv%20class%3D%5C%22sidebox%5C%22%3E%0D%0A%3Ch4%3E%B1%BE%D4%C2%C8%C8%C3%C5%3C%2Fh4%3E%0D%0A%3Cul%20class%3D%5C%22textinfolist%5C%22%3E%0D%0A%5Bnode%5D%3Cli%3E%7Bprefix%7D%7Bsubject%7D%3C%2Fli%3E%5B%2Fnode%5D%0D%0A%3C%2Ful%3E%0D%0A%3C%2Fdiv%3E',
    'parameter' =>
    array (
      'jstemplate' => '<div class=\\"sidebox\\">
<h4>��������</h4>
<ul class=\\"textinfolist\\">
[node]<li>{prefix}{subject}</li>[/node]
</ul>
</div>',
      'cachelife' => '86400',
      'sidestatus' => '0',
      'startrow' => '0',
      'items' => '5',
      'maxlength' => '20',
      'fnamelength' => '0',
      'messagelength' => '',
      'picpre' => 'images/common/slisticon.gif',
      'tids' => '',
      'keyword' => '',
      'tag' => '',
      'threadtype' => '0',
      'highlight' => '0',
      'recommend' => '0',
      'newwindow' => 1,
      'orderby' => 'hourviews',
      'hours' => '720',
      'jscharset' => '0',
    ),
    'comment' => '����������������ģ��',
    'type' => '0',
  ),
  '�ۺ�ģ��_��Ա����' =>
  array (
    'url' => 'function=module&module=rowcombine.inc.php&settings=a%3A2%3A%7Bs%3A5%3A%22title%22%3Bs%3A8%3A%22%BB%E1%D4%B1%C5%C5%D0%D0%22%3Bs%3A4%3A%22data%22%3Bs%3A79%3A%22%B1%DF%C0%B8%C4%A3%BF%E9_%BB%E1%D4%B1%C5%C5%D0%D0_%BD%F1%C8%D5%2C%C8%D5%0D%0A%B1%DF%C0%B8%C4%A3%BF%E9_%BB%E1%D4%B1%C5%C5%D0%D0_%B1%BE%D6%DC%2C%D6%DC%0D%0A%B1%DF%C0%B8%C4%A3%BF%E9_%BB%E1%D4%B1%C5%C5%D0%D0_%B1%BE%D4%C2%2C%D4%C2%22%3B%7D&jscharset=0&cachelife=3600',
    'parameter' =>
    array (
      'module' => 'rowcombine.inc.php',
      'cachelife' => '3600',
      'settings' =>
      array (
        'title' => '��Ա����',
        'data' => '����ģ��_��Ա����_����,��
����ģ��_��Ա����_����,��
����ģ��_��Ա����_����,��',
      ),
      'jscharset' => '0',
    ),
    'comment' => '���ա����ܡ����»�Ա���оۺ�ģ��',
    'type' => '5',
  ),
  '����ģ��_�Ƽ�����' =>
  array (
    'url' => 'function=threads&sidestatus=0&maxlength=20&fnamelength=0&messagelength=&startrow=0&picpre=images%2Fcommon%2Fslisticon.gif&items=5&tag=&tids=&special=0&rewardstatus=&digest=0&stick=0&recommend=1&newwindow=1&threadtype=0&highlight=0&orderby=lastpost&hours=48&jscharset=0&cachelife=3600&jstemplate=%3Cdiv%20class%3D%5C%22sidebox%5C%22%3E%0D%0A%3Ch4%3E%CD%C6%BC%F6%D6%F7%CC%E2%3C%2Fh4%3E%0D%0A%3Cul%20class%3D%5C%22textinfolist%5C%22%3E%0D%0A%5Bnode%5D%3Cli%3E%7Bprefix%7D%7Bsubject%7D%3C%2Fli%3E%5B%2Fnode%5D%0D%0A%3C%2Ful%3E%0D%0A%3C%2Fdiv%3E',
    'parameter' =>
    array (
      'jstemplate' => '<div class=\\"sidebox\\">
<h4>�Ƽ�����</h4>
<ul class=\\"textinfolist\\">
[node]<li>{prefix}{subject}</li>[/node]
</ul>
</div>',
      'cachelife' => '3600',
      'sidestatus' => '0',
      'startrow' => '0',
      'items' => '5',
      'maxlength' => '20',
      'fnamelength' => '0',
      'messagelength' => '',
      'picpre' => 'images/common/slisticon.gif',
      'tids' => '',
      'keyword' => '',
      'tag' => '',
      'threadtype' => '0',
      'highlight' => '0',
      'recommend' => '1',
      'newwindow' => 1,
      'orderby' => 'lastpost',
      'hours' => '48',
      'jscharset' => '0',
    ),
    'comment' => '�����Ƽ�����ģ��',
    'type' => '0',
  ),
  '����ģ��_����ͼƬ' =>
  array (
    'url' => 'function=images&sidestatus=0&isimage=1&threadmethod=1&maxwidth=140&maxheight=140&startrow=0&items=5&orderby=dateline&hours=0&digest=0&newwindow=1&jscharset=0&jstemplate=%3Cdiv%20%20class%3D%5C%22sidebox%5C%22%3E%0D%0A%3Ch4%3E%D7%EE%D0%C2%CD%BC%C6%AC%3C%2Fh4%3E%0D%0A%3Cscript%20type%3D%5C%22text%2Fjavascript%5C%22%3E%0D%0Avar%20slideSpeed%20%3D%202500%3B%0D%0Avar%20slideImgsize%20%3D%20%5B140%2C140%5D%3B%0D%0Avar%20slideTextBar%20%3D%200%3B%0D%0Avar%20slideBorderColor%20%3D%20%5C%27%23C8DCEC%5C%27%3B%0D%0Avar%20slideBgColor%20%3D%20%5C%27%23FFF%5C%27%3B%0D%0Avar%20slideImgs%20%3D%20new%20Array%28%29%3B%0D%0Avar%20slideImgLinks%20%3D%20new%20Array%28%29%3B%0D%0Avar%20slideImgTexts%20%3D%20new%20Array%28%29%3B%0D%0Avar%20slideSwitchBar%20%3D%201%3B%0D%0Avar%20slideSwitchColor%20%3D%20%5C%27black%5C%27%3B%0D%0Avar%20slideSwitchbgColor%20%3D%20%5C%27white%5C%27%3B%0D%0Avar%20slideSwitchHiColor%20%3D%20%5C%27%23C8DCEC%5C%27%3B%0D%0A%5Bnode%5D%0D%0AslideImgs%5B%7Border%7D%5D%20%3D%20%5C%22%7Bimgfile%7D%5C%22%3B%0D%0AslideImgLinks%5B%7Border%7D%5D%20%3D%20%5C%22%7Blink%7D%5C%22%3B%0D%0AslideImgTexts%5B%7Border%7D%5D%20%3D%20%5C%22%7Bsubject%7D%5C%22%3B%0D%0A%5B%2Fnode%5D%0D%0A%3C%2Fscript%3E%0D%0A%3Cscript%20language%3D%5C%22javascript%5C%22%20type%3D%5C%22text%2Fjavascript%5C%22%20src%3D%5C%22include%2Fjs%2Fslide.js%5C%22%3E%3C%2Fscript%3E%0D%0A%3C%2Fdiv%3E',
    'parameter' =>
    array (
      'jstemplate' => '<div  class=\\"sidebox\\">
<h4>����ͼƬ</h4>
<script type=\\"text/javascript\\">
var slideSpeed = 2500;
var slideImgsize = [140,140];
var slideTextBar = 0;
var slideBorderColor = \\\'#C8DCEC\\\';
var slideBgColor = \\\'#FFF\\\';
var slideImgs = new Array();
var slideImgLinks = new Array();
var slideImgTexts = new Array();
var slideSwitchBar = 1;
var slideSwitchColor = \\\'black\\\';
var slideSwitchbgColor = \\\'white\\\';
var slideSwitchHiColor = \\\'#C8DCEC\\\';
[node]
slideImgs[{order}] = \\"{imgfile}\\";
slideImgLinks[{order}] = \\"{link}\\";
slideImgTexts[{order}] = \\"{subject}\\";
[/node]
</script>
<script language=\\"javascript\\" type=\\"text/javascript\\" src=\\"include/js/slide.js\\"></script>
</div>',
      'cachelife' => '',
      'sidestatus' => '0',
      'startrow' => '0',
      'items' => '5',
      'isimage' => '1',
      'maxwidth' => '140',
      'maxheight' => '140',
      'threadmethod' => '1',
      'newwindow' => 1,
      'orderby' => 'dateline',
      'hours' => '',
      'jscharset' => '0',
    ),
    'comment' => '��������ͼƬչʾģ��',
    'type' => '4',
  ),
  '����ģ��_��������' =>
  array (
    'url' => 'function=threads&sidestatus=0&maxlength=20&fnamelength=0&messagelength=&startrow=0&picpre=images%2Fcommon%2Fslisticon.gif&items=5&tag=&tids=&special=0&rewardstatus=&digest=0&stick=0&recommend=0&newwindow=1&threadtype=0&highlight=0&orderby=dateline&hours=0&jscharset=0&jstemplate=%3Cdiv%20class%3D%5C%22sidebox%5C%22%3E%0D%0A%3Ch4%3E%D7%EE%D0%C2%D6%F7%CC%E2%3C%2Fh4%3E%0D%0A%3Cul%20class%3D%5C%22textinfolist%5C%22%3E%0D%0A%5Bnode%5D%3Cli%3E%7Bprefix%7D%7Bsubject%7D%3C%2Fli%3E%5B%2Fnode%5D%0D%0A%3C%2Ful%3E%0D%0A%3C%2Fdiv%3E',
    'parameter' =>
    array (
      'jstemplate' => '<div class=\\"sidebox\\">
<h4>��������</h4>
<ul class=\\"textinfolist\\">
[node]<li>{prefix}{subject}</li>[/node]
</ul>
</div>',
      'cachelife' => '',
      'sidestatus' => '0',
      'startrow' => '0',
      'items' => '5',
      'maxlength' => '20',
      'fnamelength' => '0',
      'messagelength' => '',
      'picpre' => 'images/common/slisticon.gif',
      'tids' => '',
      'keyword' => '',
      'tag' => '',
      'threadtype' => '0',
      'highlight' => '0',
      'recommend' => '0',
      'newwindow' => 1,
      'orderby' => 'dateline',
      'hours' => '',
      'jscharset' => '0',
    ),
    'comment' => '������������ģ��',
    'type' => '0',
  ),
  '����ģ��_��Ծ��Ա' =>
  array (
    'url' => 'function=memberrank&startrow=0&items=12&newwindow=1&extcredit=1&orderby=posts&hours=0&jscharset=0&cachelife=43200&jstemplate=%3Cdiv%20class%3D%5C%22sidebox%5C%22%3E%0D%0A%3Ch4%3E%BB%EE%D4%BE%BB%E1%D4%B1%3C%2Fh4%3E%0D%0A%3Cul%20class%3D%5C%22avt_list%20s_clear%5C%22%3E%0D%0A%5Bnode%5D%3Cli%3E%7Bavatarsmall%7D%3C%2Fli%3E%5B%2Fnode%5D%0D%0A%3C%2Ful%3E%0D%0A%3C%2Fdiv%3E',
    'parameter' =>
    array (
      'jstemplate' => '<div class=\\"sidebox\\">
<h4>��Ծ��Ա</h4>
<ul class=\\"avt_list s_clear\\">
[node]<li>{avatarsmall}</li>[/node]
</ul>
</div>',
      'cachelife' => '43200',
      'startrow' => '0',
      'items' => '12',
      'newwindow' => 1,
      'extcredit' => '1',
      'orderby' => 'posts',
      'hours' => '',
      'jscharset' => '0',
    ),
    'comment' => '������Ծ��Աģ��',
    'type' => '2',
  ),
  '����ģ��_��������_����' =>
  array (
    'url' => 'function=threads&sidestatus=1&maxlength=20&fnamelength=0&messagelength=&startrow=0&picpre=images%2Fcommon%2Fslisticon.gif&items=5&tag=&tids=&special=0&rewardstatus=&digest=0&stick=0&recommend=0&newwindow=1&threadtype=0&highlight=0&orderby=replies&hours=0&jscharset=0&cachelife=1800&jstemplate=%3Cdiv%20class%3D%5C%22sidebox%5C%22%3E%0D%0A%3Ch4%3E%B1%BE%B0%E6%C8%C8%C3%C5%D6%F7%CC%E2%3C%2Fh4%3E%0D%0A%3Cul%20class%3D%5C%22textinfolist%5C%22%3E%0D%0A%5Bnode%5D%3Cli%3E%7Bprefix%7D%7Bsubject%7D%3C%2Fli%3E%5B%2Fnode%5D%0D%0A%3C%2Ful%3E%0D%0A%3C%2Fdiv%3E',
    'parameter' =>
    array (
      'jstemplate' => '<div class=\\"sidebox\\">
<h4>������������</h4>
<ul class=\\"textinfolist\\">
[node]<li>{prefix}{subject}</li>[/node]
</ul>
</div>',
      'cachelife' => '1800',
      'sidestatus' => '1',
      'startrow' => '0',
      'items' => '5',
      'maxlength' => '20',
      'fnamelength' => '0',
      'messagelength' => '',
      'picpre' => 'images/common/slisticon.gif',
      'tids' => '',
      'keyword' => '',
      'tag' => '',
      'threadtype' => '0',
      'highlight' => '0',
      'recommend' => '0',
      'newwindow' => 1,
      'orderby' => 'replies',
      'hours' => '',
      'jscharset' => '0',
    ),
    'comment' => '����������������ģ��',
    'type' => '0',
  ),
  '����ģ��_��������_����' =>
  array (
    'url' => 'function=threads&sidestatus=0&maxlength=20&fnamelength=0&messagelength=&startrow=0&picpre=images%2Fcommon%2Fslisticon.gif&items=5&tag=&tids=&special=0&rewardstatus=&digest=0&stick=0&recommend=0&newwindow=1&threadtype=0&highlight=0&orderby=hourviews&hours=24&jscharset=0&cachelife=1800&jstemplate=%3Cdiv%20class%3D%5C%22sidebox%5C%22%3E%0D%0A%3Ch4%3E%BD%F1%C8%D5%C8%C8%C3%C5%3C%2Fh4%3E%0D%0A%3Cul%20class%3D%5C%22textinfolist%5C%22%3E%0D%0A%5Bnode%5D%3Cli%3E%7Bprefix%7D%7Bsubject%7D%3C%2Fli%3E%5B%2Fnode%5D%0D%0A%3C%2Ful%3E%0D%0A%3C%2Fdiv%3E',
    'parameter' =>
    array (
      'jstemplate' => '<div class=\\"sidebox\\">
<h4>��������</h4>
<ul class=\\"textinfolist\\">
[node]<li>{prefix}{subject}</li>[/node]
</ul>
</div>',
      'cachelife' => '1800',
      'sidestatus' => '0',
      'startrow' => '0',
      'items' => '5',
      'maxlength' => '20',
      'fnamelength' => '0',
      'messagelength' => '',
      'picpre' => 'images/common/slisticon.gif',
      'tids' => '',
      'keyword' => '',
      'tag' => '',
      'threadtype' => '0',
      'highlight' => '0',
      'recommend' => '0',
      'newwindow' => 1,
      'orderby' => 'hourviews',
      'hours' => '24',
      'jscharset' => '0',
    ),
    'comment' => '����������������ģ��',
    'type' => '0',
  ),
  '����ģ��_���»ظ�' =>
  array (
    'url' => 'function=threads&sidestatus=0&maxlength=20&fnamelength=0&messagelength=&startrow=0&picpre=images%2Fcommon%2Fslisticon.gif&items=5&tag=&tids=&special=0&rewardstatus=&digest=0&stick=0&recommend=0&newwindow=1&threadtype=0&highlight=0&orderby=lastpost&hours=0&jscharset=0&jstemplate=%3Cdiv%20class%3D%5C%22sidebox%5C%22%3E%0D%0A%3Ch4%3E%D7%EE%D0%C2%BB%D8%B8%B4%3C%2Fh4%3E%0D%0A%3Cul%20class%3D%5C%22textinfolist%5C%22%3E%0D%0A%5Bnode%5D%3Cli%3E%7Bprefix%7D%7Bsubject%7D%3C%2Fli%3E%5B%2Fnode%5D%0D%0A%3C%2Ful%3E%0D%0A%3C%2Fdiv%3E',
    'parameter' =>
    array (
      'jstemplate' => '<div class=\\"sidebox\\">
<h4>���»ظ�</h4>
<ul class=\\"textinfolist\\">
[node]<li>{prefix}{subject}</li>[/node]
</ul>
</div>',
      'cachelife' => '',
      'sidestatus' => '0',
      'startrow' => '0',
      'items' => '5',
      'maxlength' => '20',
      'fnamelength' => '0',
      'messagelength' => '',
      'picpre' => 'images/common/slisticon.gif',
      'tids' => '',
      'keyword' => '',
      'tag' => '',
      'threadtype' => '0',
      'highlight' => '0',
      'recommend' => '0',
      'newwindow' => 1,
      'orderby' => 'lastpost',
      'hours' => '',
      'jscharset' => '0',
    ),
    'comment' => '�������»ظ�ģ��',
    'type' => '0',
  ),
  '����ģ��_����ͼƬ_����' =>
  array (
    'url' => 'function=images&sidestatus=1&isimage=1&threadmethod=1&maxwidth=140&maxheight=140&startrow=0&items=5&orderby=dateline&hours=0&digest=0&newwindow=1&jscharset=0&jstemplate=%3Cdiv%20%20class%3D%5C%22sidebox%5C%22%3E%0D%0A%3Ch4%3E%D7%EE%D0%C2%CD%BC%C6%AC%3C%2Fh4%3E%0D%0A%3Cscript%20type%3D%5C%22text%2Fjavascript%5C%22%3E%0D%0Avar%20slideSpeed%20%3D%202500%3B%0D%0Avar%20slideImgsize%20%3D%20%5B140%2C140%5D%3B%0D%0Avar%20slideTextBar%20%3D%200%3B%0D%0Avar%20slideBorderColor%20%3D%20%5C%27%23C8DCEC%5C%27%3B%0D%0Avar%20slideBgColor%20%3D%20%5C%27%23FFF%5C%27%3B%0D%0Avar%20slideImgs%20%3D%20new%20Array%28%29%3B%0D%0Avar%20slideImgLinks%20%3D%20new%20Array%28%29%3B%0D%0Avar%20slideImgTexts%20%3D%20new%20Array%28%29%3B%0D%0Avar%20slideSwitchBar%20%3D%201%3B%0D%0Avar%20slideSwitchColor%20%3D%20%5C%27black%5C%27%3B%0D%0Avar%20slideSwitchbgColor%20%3D%20%5C%27white%5C%27%3B%0D%0Avar%20slideSwitchHiColor%20%3D%20%5C%27%23C8DCEC%5C%27%3B%0D%0A%5Bnode%5D%0D%0AslideImgs%5B%7Border%7D%5D%20%3D%20%5C%22%7Bimgfile%7D%5C%22%3B%0D%0AslideImgLinks%5B%7Border%7D%5D%20%3D%20%5C%22%7Blink%7D%5C%22%3B%0D%0AslideImgTexts%5B%7Border%7D%5D%20%3D%20%5C%22%7Bsubject%7D%5C%22%3B%0D%0A%5B%2Fnode%5D%0D%0A%3C%2Fscript%3E%0D%0A%3Cscript%20language%3D%5C%22javascript%5C%22%20type%3D%5C%22text%2Fjavascript%5C%22%20src%3D%5C%22include%2Fjs%2Fslide.js%5C%22%3E%3C%2Fscript%3E%0D%0A%3C%2Fdiv%3E',
    'parameter' =>
    array (
      'jstemplate' => '<div  class=\\"sidebox\\">
<h4>����ͼƬ</h4>
<script type=\\"text/javascript\\">
var slideSpeed = 2500;
var slideImgsize = [140,140];
var slideTextBar = 0;
var slideBorderColor = \\\'#C8DCEC\\\';
var slideBgColor = \\\'#FFF\\\';
var slideImgs = new Array();
var slideImgLinks = new Array();
var slideImgTexts = new Array();
var slideSwitchBar = 1;
var slideSwitchColor = \\\'black\\\';
var slideSwitchbgColor = \\\'white\\\';
var slideSwitchHiColor = \\\'#C8DCEC\\\';
[node]
slideImgs[{order}] = \\"{imgfile}\\";
slideImgLinks[{order}] = \\"{link}\\";
slideImgTexts[{order}] = \\"{subject}\\";
[/node]
</script>
<script language=\\"javascript\\" type=\\"text/javascript\\" src=\\"include/js/slide.js\\"></script>
</div>',
      'cachelife' => '',
      'sidestatus' => '1',
      'startrow' => '0',
      'items' => '5',
      'isimage' => '1',
      'maxwidth' => '140',
      'maxheight' => '140',
      'threadmethod' => '1',
      'newwindow' => 1,
      'orderby' => 'dateline',
      'hours' => '',
      'jscharset' => '0',
    ),
    'comment' => '������������ͼƬչʾģ��',
    'type' => '4',
  ),
  '����ģ��_��ǩ' =>
  array (
    'url' => 'function=module&module=tag.inc.php&settings=a%3A1%3A%7Bs%3A5%3A%22limit%22%3Bs%3A2%3A%2220%22%3B%7D&jscharset=0&cachelife=900',
    'parameter' =>
    array (
      'module' => 'tag.inc.php',
      'cachelife' => '900',
      'settings' =>
      array (
        'limit' => '20',
      ),
      'jscharset' => '0',
    ),
    'comment' => '������ǩģ��',
    'type' => '5',
  ),
  '����ģ��_��Ա����_����' =>
  array (
    'url' => 'function=memberrank&startrow=0&items=5&newwindow=1&extcredit=1&orderby=hourposts&hours=720&jscharset=0&cachelife=86400&jstemplate=%3Cdiv%20class%3D%5C%22sidebox%20s_clear%5C%22%3E%0D%0A%3Ch4%3E%B1%BE%D4%C2%C5%C5%D0%D0%3C%2Fh4%3E%0D%0A%5Bnode%5D%3Cdiv%20style%3D%5C%22clear%3Aboth%5C%22%3E%3Cdiv%20style%3D%5C%22float%3Aleft%3Bmargin%3A%200%2016px%205px%200%5C%22%3E%7Bavatarsmall%7D%3C%2Fdiv%3E%7Bmember%7D%3Cbr%20%2F%3E%B7%A2%CC%FB%20%7Bvalue%7D%20%C6%AA%3C%2Fdiv%3E%5B%2Fnode%5D%0D%0A%3C%2Fdiv%3E',
    'parameter' =>
    array (
      'jstemplate' => '<div class=\\"sidebox\\">
<h4>��������</h4>
[node]<div class=\\"s_clear\\" style=\\"margin-bottom: 5px;\\"><div style=\\"margin-right: 10px; float: left;\\">{avatarsmall}</div><p>{member}</p><p>���� {value} ƪ</p></div>[/node]
</div>',
      'cachelife' => '86400',
      'startrow' => '0',
      'items' => '5',
      'newwindow' => 1,
      'extcredit' => '1',
      'orderby' => 'hourposts',
      'hours' => '720',
      'jscharset' => '0',
    ),
    'comment' => '������Ա���·�������ģ��',
    'type' => '2',
  ),
  '����ģ��_��Ա����_����' =>
  array (
    'url' => 'function=memberrank&startrow=0&items=5&newwindow=1&extcredit=1&orderby=hourposts&hours=168&jscharset=0&cachelife=43200&jstemplate=%3Cdiv%20class%3D%5C%22sidebox%20s_clear%5C%22%3E%0D%0A%3Ch4%3E%B1%BE%D6%DC%C5%C5%D0%D0%3C%2Fh4%3E%0D%0A%5Bnode%5D%3Cdiv%20style%3D%5C%22clear%3Aboth%5C%22%3E%3Cdiv%20style%3D%5C%22float%3Aleft%3Bmargin%3A%200%2016px%205px%200%5C%22%3E%7Bavatarsmall%7D%3C%2Fdiv%3E%7Bmember%7D%3Cbr%20%2F%3E%B7%A2%CC%FB%20%7Bvalue%7D%20%C6%AA%3C%2Fdiv%3E%5B%2Fnode%5D%0D%0A%3C%2Fdiv%3E',
    'parameter' =>
    array (
      'jstemplate' => '<div class=\\"sidebox\\">
<h4>��������</h4>
[node]<div class=\\"s_clear\\" style=\\"margin-bottom: 5px;\\"><div style=\\"margin-right: 10px; float: left;\\">{avatarsmall}</div><p>{member}</p><p>���� {value} ƪ</p></div>[/node]
</div>',
      'cachelife' => '43200',
      'startrow' => '0',
      'items' => '5',
      'newwindow' => 1,
      'extcredit' => '1',
      'orderby' => 'hourposts',
      'hours' => '168',
      'jscharset' => '0',
    ),
    'comment' => '������Ա���ܷ�������ģ��',
    'type' => '2',
  ),
   '��������_�����б�ҳĬ��' =>
  array (
    'url' => 'function=side&jscharset=&jstemplate=%5Bmodule%5D%B1%DF%C0%B8%C4%A3%BF%E9_%CE%D2%B5%C4%D6%FA%CA%D6%5B%2Fmodule%5D%3Chr%20class%3D%22shadowline%22%2F%3E%5Bmodule%5D%B1%DF%C0%B8%C4%A3%BF%E9_%C8%C8%C3%C5%D6%F7%CC%E2_%B1%BE%B0%E6%5B%2Fmodule%5D%3Chr%20class%3D%22shadowline%22%2F%3E%5Bmodule%5D%B1%DF%C0%B8%C4%A3%BF%E9_%B0%E6%BF%E9%C5%C5%D0%D0%5B%2Fmodule%5D',
    'parameter' =>
    array (
      'selectmodule' =>
      array (
        1 => '����ģ��_�ҵ�����',
        2 => '����ģ��_��������_����',
        3 => '����ģ��_�������',
      ),
      'cachelife' => 0,
      'jstemplate' => '[module]����ģ��_�ҵ�����[/module]<hr class="shadowline"/>[module]����ģ��_��������_����[/module]<hr class="shadowline"/>[module]����ģ��_�������[/module]',
    ),
    'comment' => NULL,
    'type' => '-2',
  ),
  '��������_��ҳĬ��' =>
  array (
    'url' => 'function=side&jscharset=&jstemplate=%5Bmodule%5D%B1%DF%C0%B8%C4%A3%BF%E9_%CE%D2%B5%C4%D6%FA%CA%D6%5B%2Fmodule%5D%3Chr%20class%3D%22shadowline%22%2F%3E%5Bmodule%5D%BE%DB%BA%CF%C4%A3%BF%E9_%D0%C2%CC%FB%5B%2Fmodule%5D%3Chr%20class%3D%22shadowline%22%2F%3E%5Bmodule%5D%BE%DB%BA%CF%C4%A3%BF%E9_%C8%C8%C3%C5%D6%F7%CC%E2%5B%2Fmodule%5D%3Chr%20class%3D%22shadowline%22%2F%3E%5Bmodule%5D%B1%DF%C0%B8%C4%A3%BF%E9_%BB%EE%D4%BE%BB%E1%D4%B1%5B%2Fmodule%5D',
    'parameter' =>
    array (
      'selectmodule' =>
      array (
        1 => '����ģ��_�ҵ�����',
        2 => '�ۺ�ģ��_����',
        3 => '�ۺ�ģ��_��������',
        4 => '����ģ��_��Ծ��Ա',
      ),
      'cachelife' => 0,
      'jstemplate' => '[module]����ģ��_�ҵ�����[/module]<hr class="shadowline"/>[module]�ۺ�ģ��_����[/module]<hr class="shadowline"/>[module]�ۺ�ģ��_��������[/module]<hr class="shadowline"/>[module]����ģ��_��Ծ��Ա[/module]',
    ),
    'comment' => NULL,
    'type' => '-2',
  ),
  '�ۺ�ģ��_����' =>
  array (
    'url' => 'function=module&module=rowcombine.inc.php&settings=a%3A2%3A%7Bs%3A5%3A%22title%22%3Bs%3A8%3A%22%D7%EE%D0%C2%CC%FB%D7%D3%22%3Bs%3A4%3A%22data%22%3Bs%3A46%3A%22%B1%DF%C0%B8%C4%A3%BF%E9_%D7%EE%D0%C2%D6%F7%CC%E2%2C%D6%F7%CC%E2%0D%0A%B1%DF%C0%B8%C4%A3%BF%E9_%D7%EE%D0%C2%BB%D8%B8%B4%2C%BB%D8%B8%B4%22%3B%7D&jscharset=0',
    'parameter' =>
    array (
      'module' => 'rowcombine.inc.php',
      'cachelife' => '',
      'settings' =>
      array (
        'title' => '��������',
        'data' => '����ģ��_��������,����
����ģ��_���»ظ�,�ظ�',
      ),
      'jscharset' => '0',
    ),
    'comment' => '�������⡢���»ظ��ۺ�ģ��',
    'type' => '5',
  ),
  '����ģ��_��������_����' =>
  array (
    'url' => 'function=threads&sidestatus=0&maxlength=20&fnamelength=0&messagelength=&startrow=0&picpre=images%2Fcommon%2Fslisticon.gif&items=5&tag=&tids=&special=0&rewardstatus=&digest=0&stick=0&recommend=0&newwindow=1&threadtype=0&highlight=0&orderby=hourviews&hours=168&jscharset=0&cachelife=43200&jstemplate=%3Cdiv%20class%3D%5C%22sidebox%5C%22%3E%0D%0A%3Ch4%3E%B1%BE%D6%DC%C8%C8%C3%C5%3C%2Fh4%3E%0D%0A%3Cul%20class%3D%5C%22textinfolist%5C%22%3E%0D%0A%5Bnode%5D%3Cli%3E%7Bprefix%7D%7Bsubject%7D%3C%2Fli%3E%5B%2Fnode%5D%0D%0A%3C%2Ful%3E%0D%0A%3C%2Fdiv%3E',
    'parameter' =>
    array (
      'jstemplate' => '<div class=\\"sidebox\\">
<h4>��������</h4>
<ul class=\\"textinfolist\\">
[node]<li>{prefix}{subject}</li>[/node]
</ul>
</div>',
      'cachelife' => '43200',
      'sidestatus' => '0',
      'startrow' => '0',
      'items' => '5',
      'maxlength' => '20',
      'fnamelength' => '0',
      'messagelength' => '',
      'picpre' => 'images/common/slisticon.gif',
      'tids' => '',
      'keyword' => '',
      'tag' => '',
      'threadtype' => '0',
      'highlight' => '0',
      'recommend' => '0',
      'newwindow' => 1,
      'orderby' => 'hourviews',
      'hours' => '168',
      'jscharset' => '0',
    ),
    'comment' => '����������������ģ��',
    'type' => '0',
  ),
  '����ģ��_��Ա����_����' =>
  array (
    'url' => 'function=memberrank&startrow=0&items=5&newwindow=1&extcredit=1&orderby=hourposts&hours=24&jscharset=0&cachelife=3600&jstemplate=%3Cdiv%20class%3D%5C%22sidebox%20s_clear%5C%22%3E%0D%0A%3Ch4%3E%BD%F1%C8%D5%C5%C5%D0%D0%3C%2Fh4%3E%0D%0A%5Bnode%5D%3Cdiv%20style%3D%5C%22clear%3Aboth%5C%22%3E%3Cdiv%20style%3D%5C%22float%3Aleft%3Bmargin%3A%200%2016px%205px%200%5C%22%3E%7Bavatarsmall%7D%3C%2Fdiv%3E%7Bmember%7D%3Cbr%20%2F%3E%B7%A2%CC%FB%20%7Bvalue%7D%20%C6%AA%3C%2Fdiv%3E%5B%2Fnode%5D%0D%0A%3C%2Fdiv%3E',
    'parameter' =>
    array (
      'jstemplate' => '<div class=\\"sidebox\\">
<h4>��������</h4>
[node]<div class=\\"s_clear\\" style=\\"margin-bottom: 5px;\\"><div style=\\"margin-right: 10px; float: left;\\">{avatarsmall}</div><p>{member}</p><p>���� {value} ƪ</p></div>[/node]
</div>',
      'cachelife' => '3600',
      'startrow' => '0',
      'items' => '5',
      'newwindow' => 1,
      'extcredit' => '1',
      'orderby' => 'hourposts',
      'hours' => '24',
      'jscharset' => '0',
    ),
    'comment' => '������Ա���շ�������ģ��',
    'type' => '2',
  ),
  '����ģ��_��̳֮��' =>
  array (
    'url' => 'function=memberrank&startrow=0&items=3&newwindow=1&extcredit=1&orderby=hourposts&hours=168&jscharset=0&cachelife=43200&jstemplate=%3Cdiv%20class%3D%5C%22sidebox%20s_clear%5C%22%3E%0D%0A%3Ch4%3E%B1%BE%D6%DC%D6%AE%D0%C7%3C%2Fh4%3E%0D%0A%5Bnode%5D%0D%0A%5Bshow%3D1%5D%3Cdiv%20style%3D%5C%22clear%3Aboth%5C%22%3E%3Cdiv%20style%3D%5C%22float%3Aleft%3B%20margin-right%3A%2016px%3B%5C%22%3E%7Bavatarsmall%7D%3C%2Fdiv%3E%5B%2Fshow%5D%7Bmember%7D%20%5Bshow%3D1%5D%3Cbr%20%2F%3E%B7%A2%CC%FB%20%7Bvalue%7D%20%C6%AA%3C%2Fdiv%3E%3Cdiv%20style%3D%5C%22clear%3Aboth%3Bmargin-top%3A2px%5C%22%20%2F%3E%3C%2Fdiv%3E%5B%2Fshow%5D%0D%0A%5B%2Fnode%5D%0D%0A%3C%2Fdiv%3E',
    'parameter' =>
    array (
      'jstemplate' => '<div class=\\"sidebox s_clear\\">
<h4>����֮��</h4>
[node]
[show=1]<div style=\\"clear:both\\"><div style=\\"float:left; margin-right: 16px;\\">{avatarsmall}</div>[/show]{member} [show=1]<br />���� {value} ƪ</div><div style=\\"clear:both;margin-top:2px\\" /></div>[/show]
[/node]
</div>',
      'cachelife' => '43200',
      'startrow' => '0',
      'items' => '3',
      'newwindow' => 1,
      'extcredit' => '1',
      'orderby' => 'hourposts',
      'hours' => '168',
      'jscharset' => '0',
    ),
    'comment' => '������̳֮��ģ��',
    'type' => '2',
  ),
  '����ģ��_�ҵ�����' =>
  array (
    'url' => 'function=module&module=assistant.inc.php&settings=N%3B&jscharset=0&cachelife=0',
    'parameter' =>
    array (
      'module' => 'assistant.inc.php',
      'cachelife' => '0',
      'jscharset' => '0',
    ),
    'comment' => '�����ҵ�����ģ��',
    'type' => '5',
  ),
  '����ģ��_Google����' =>
  array (
    'url' => 'function=module&module=google.inc.php&settings=a%3A2%3A%7Bs%3A4%3A%22lang%22%3Bs%3A0%3A%22%22%3Bs%3A7%3A%22default%22%3Bs%3A1%3A%221%22%3B%7D&jscharset=0&cachelife=864000',
    'parameter' =>
    array (
      'module' => 'google.inc.php',
      'cachelife' => '864000',
      'settings' =>
      array (
        'lang' => '',
        'default' => '1',
      ),
      'jscharset' => '0',
    ),
    'comment' => '���� Google ����ģ��',
    'type' => '5',
  ),
  'UCHome_���¶�̬' =>
  array (
    'url' => 'function=module&module=feed.inc.php&settings=a%3A6%3A%7Bs%3A5%3A%22title%22%3Bs%3A8%3A%22%D7%EE%D0%C2%B6%AF%CC%AC%22%3Bs%3A4%3A%22uids%22%3Bs%3A0%3A%22%22%3Bs%3A6%3A%22friend%22%3Bs%3A1%3A%220%22%3Bs%3A5%3A%22start%22%3Bs%3A1%3A%220%22%3Bs%3A5%3A%22limit%22%3Bs%3A2%3A%2210%22%3Bs%3A8%3A%22template%22%3Bs%3A54%3A%22%3Cdiv%20style%3D%5C%22padding-left%3A2px%5C%22%3E%7Btitle_template%7D%3C%2Fdiv%3E%22%3B%7D&jscharset=0&cachelife=0',
    'parameter' =>
    array (
      'module' => 'feed.inc.php',
      'cachelife' => '0',
      'settings' =>
      array (
        'title' => '���¶�̬',
        'uids' => '',
        'friend' => '0',
        'start' => '0',
        'limit' => '10',
        'template' => '<div style=\\"padding-left:2px\\">{title_template}</div>',
      ),
      'jscharset' => '0',
    ),
    'comment' => '��ȡUCHome�����¶�̬',
    'type' => '5',
  ),
  'UCHome_���¸��¿ռ�' =>
  array (
    'url' => 'function=module&module=space.inc.php&settings=a%3A17%3A%7Bs%3A5%3A%22title%22%3Bs%3A12%3A%22%D7%EE%D0%C2%B8%FC%D0%C2%BF%D5%BC%E4%22%3Bs%3A3%3A%22uid%22%3Bs%3A0%3A%22%22%3Bs%3A14%3A%22startfriendnum%22%3Bs%3A0%3A%22%22%3Bs%3A12%3A%22endfriendnum%22%3Bs%3A0%3A%22%22%3Bs%3A12%3A%22startviewnum%22%3Bs%3A0%3A%22%22%3Bs%3A10%3A%22endviewnum%22%3Bs%3A0%3A%22%22%3Bs%3A11%3A%22startcredit%22%3Bs%3A0%3A%22%22%3Bs%3A9%3A%22endcredit%22%3Bs%3A0%3A%22%22%3Bs%3A6%3A%22avatar%22%3Bs%3A2%3A%22-1%22%3Bs%3A10%3A%22namestatus%22%3Bs%3A2%3A%22-1%22%3Bs%3A8%3A%22dateline%22%3Bs%3A1%3A%220%22%3Bs%3A10%3A%22updatetime%22%3Bs%3A1%3A%220%22%3Bs%3A5%3A%22order%22%3Bs%3A10%3A%22updatetime%22%3Bs%3A2%3A%22sc%22%3Bs%3A4%3A%22DESC%22%3Bs%3A5%3A%22start%22%3Bs%3A1%3A%220%22%3Bs%3A5%3A%22limit%22%3Bs%3A2%3A%2210%22%3Bs%3A8%3A%22template%22%3Bs%3A267%3A%22%3Ctable%3E%0D%0A%3Ctr%3E%0D%0A%3Ctd%20width%3D%5C%2250%5C%22%20rowspan%3D%5C%222%5C%22%3E%3Ca%20href%3D%5C%22%7Buserlink%7D%5C%22%20target%3D%5C%22_blank%5C%22%3E%3Cimg%20src%3D%5C%22%7Bphoto%7D%5C%22%20%2F%3E%3C%2Fa%3E%3C%2Ftd%3E%0D%0A%3Ctd%3E%3Ca%20href%3D%5C%22%7Buserlink%7D%5C%22%20%20target%3D%5C%22_blank%5C%22%20style%3D%5C%22text-decoration%3Anone%3B%5C%22%3E%7Busername%7D%3C%2Fa%3E%3C%2Ftd%3E%0D%0A%3C%2Ftr%3E%0D%0A%3Ctr%3E%3Ctd%3E%7Bupdatetime%7D%3C%2Ftd%3E%3C%2Ftr%3E%0D%0A%3C%2Ftable%3E%22%3B%7D&jscharset=0&cachelife=0',
    'parameter' =>
    array (
      'module' => 'space.inc.php',
      'cachelife' => '0',
      'settings' =>
      array (
        'title' => '���¸��¿ռ�',
        'uid' => '',
        'startfriendnum' => '',
        'endfriendnum' => '',
        'startviewnum' => '',
        'endviewnum' => '',
        'startcredit' => '',
        'endcredit' => '',
        'avatar' => '-1',
        'namestatus' => '-1',
        'dateline' => '0',
        'updatetime' => '0',
        'order' => 'updatetime',
        'sc' => 'DESC',
        'start' => '0',
        'limit' => '10',
        'template' => '<table>
<tr>
<td width=\\"50\\" rowspan=\\"2\\"><a href=\\"{userlink}\\" target=\\"_blank\\"><img src=\\"{photo}\\" /></a></td>
<td><a href=\\"{userlink}\\"  target=\\"_blank\\" style=\\"text-decoration:none;\\">{username}</a></td>
</tr>
<tr><td>{updatetime}</td></tr>
</table>',
      ),
      'jscharset' => '0',
    ),
    'comment' => '��ȡUCHome���¸��»�Ա�ռ�',
    'type' => '5',
  ),
  'UCHome_���¼�¼' =>
  array (
    'url' => 'function=module&module=doing.inc.php&settings=a%3A6%3A%7Bs%3A5%3A%22title%22%3Bs%3A8%3A%22%D7%EE%D0%C2%BC%C7%C2%BC%22%3Bs%3A3%3A%22uid%22%3Bs%3A0%3A%22%22%3Bs%3A4%3A%22mood%22%3Bs%3A1%3A%220%22%3Bs%3A5%3A%22start%22%3Bs%3A1%3A%220%22%3Bs%3A5%3A%22limit%22%3Bs%3A2%3A%2210%22%3Bs%3A8%3A%22template%22%3Bs%3A360%3A%22%0D%0A%3Cdiv%20style%3D%5C%22padding%3A0%200%205px%200%3B%5C%22%3E%0D%0A%3Ca%20href%3D%5C%22%7Buserlink%7D%5C%22%20target%3D%5C%22_blank%5C%22%3E%3Cimg%20src%3D%5C%22%7Bphoto%7D%5C%22%20width%3D%5C%2218%5C%22%20height%3D%5C%2218%5C%22%20align%3D%5C%22absmiddle%5C%22%3E%3C%2Fa%3E%20%3Ca%20href%3D%5C%22%7Buserlink%7D%5C%22%20%20target%3D%5C%22_blank%5C%22%3E%7Busername%7D%3C%2Fa%3E%A3%BA%0D%0A%3C%2Fdiv%3E%0D%0A%3Cdiv%20style%3D%5C%22padding%3A0%200%205px%2020px%3B%5C%22%3E%0D%0A%3Ca%20href%3D%5C%22%7Blink%7D%5C%22%20style%3D%5C%22color%3A%23333%3Btext-decoration%3Anone%3B%5C%22%20target%3D%5C%22_blank%5C%22%3E%7Bmessage%7D%3C%2Fa%3E%0D%0A%3C%2Fdiv%3E%22%3B%7D&jscharset=0&cachelife=0',
    'parameter' =>
    array (
      'module' => 'doing.inc.php',
      'cachelife' => '0',
      'settings' =>
      array (
        'title' => '���¼�¼',
        'uid' => '',
        'mood' => '0',
        'start' => '0',
        'limit' => '10',
        'template' => '
<div style=\\"padding:0 0 5px 0;\\">
<a href=\\"{userlink}\\" target=\\"_blank\\"><img src=\\"{photo}\\" width=\\"18\\" height=\\"18\\" align=\\"absmiddle\\"></a> <a href=\\"{userlink}\\"  target=\\"_blank\\">{username}</a>��
</div>
<div style=\\"padding:0 0 5px 20px;\\">
<a href=\\"{link}\\" style=\\"color:#333;text-decoration:none;\\" target=\\"_blank\\">{message}</a>
</div>',
      ),
      'jscharset' => '0',
    ),
    'comment' => '��ȡUCHome�����¼�¼',
    'type' => '5',
  ),
  'UCHome_��������' =>
  array (
    'url' => 'function=module&module=html.inc.php&settings=a%3A3%3A%7Bs%3A4%3A%22type%22%3Bs%3A1%3A%220%22%3Bs%3A4%3A%22code%22%3Bs%3A27%3A%22%3Cdiv%20id%3D%5C%22sidefeed%5C%22%3E%3C%2Fdiv%3E%22%3Bs%3A4%3A%22side%22%3Bs%3A1%3A%220%22%3B%7D&jscharset=0&cachelife=864000',
    'parameter' =>
    array (
      'module' => 'html.inc.php',
      'cachelife' => '864000',
      'settings' =>
      array (
        'type' => '0',
        'code' => '<div id=\\"sidefeed\\"></div>',
        'side' => '0',
      ),
      'jscharset' => '0',
    ),
    'comment' => '��ȡUCHome�ľ���������Ϣ',
    'type' => '5',
  ),
);

$tasktypes = array(
  'promotion' =>
  array (
    'name' => '��̳�ƹ�����',
    'version' => '1.0',
  ),
  'gift' =>
  array (
    'name' => '���������',
    'version' => '1.0',
  ),
  'avatar' =>
  array (
    'name' => 'ͷ��������',
    'version' => '1.0',
  )
);

$newbietask = array(
	1 => array(
		'name' => '������һ������',
		'task' => "1, 0, '������һ������', 'ѧϰ����������������һ�����£�BS������������', '', 0, 0, 0, 'all', 'newbie_post_reply', 0, 0, 0, 'credit', '2', 10, -1, ''",
		'vars' => array(
			"'complete', '�ظ�ָ������', '".addslashes('���û�Աֻ�лظ���������������������д����� tid(����һ������ĵ�ַ�� http://localhost/viewthread.php?tid=8 ��ô������� tid ���� 8)������Ϊ������')."', 'threadid', 'text', '0', ''",
			"'setting', '', '', 'entrance', 'text', 'viewthread', ''"
		)
	),
	2 => array(
		'name' => '�ҵĵ�һ��',
		'task' => "1, 0, '�ҵĵ�һ��', 'ѧ�ᷢ����������Ϊ�����Ľ���', '', 0, 0, 0, 'all', 'newbie_post_newthread', 0, 0, 0, 'credit', '2', 10, -1, ''",
		'vars' => array(
			"'complete', '��ָ����鷢��������', '".addslashes('���û�Ա������ĳ����鷢������һƪ����������������')."', 'forumid', 'text', '', ''",

			"'setting', '', '', 'entrance', 'text', 'forumdisplay', ''"
		)
	),
	3 => array(
		'name' => '���ڲ�ͬ',
		'task' => "1, 0, '���ڲ�ͬ', '�޸ĸ������ϣ�����ͱ������ڲ�ͬ', '', 0, 0, 0, 'all', 'newbie_modifyprofile', 0, 0, 0, 'credit', '2', 10, -1, ''",
		'vars' => array(
			"'complete', '���Ƹ�������', '".addslashes('���������ֻҪ���Լ��ĸ���������д���������������')."', '', '', '', ''",
			"'setting', '', '', 'entrance', 'text', 'memcp', ''"
		)
	),
	4 => array(
		'name' => '��������',
		'task' => "1, 0, '��������', '�ϴ�ͷ���ô����ʶһ��ȫ�µ���', '', 0, 0, 0, 'all', 'newbie_uploadavatar', 0, 0, 0, 'credit', '2', 10, -1, ''",
		'vars' => array(
			"'complete', '�ϴ�ͷ��', '".addslashes('���������ֻҪ�ɹ��ϴ�ͷ�񼴿��������')."', '', '', '', ''",
			"'setting', '', '', 'entrance', 'text', 'memcp', ''"
		)
	),
	5 => array(
		'name' => '�������',
		'task' => "1, 0, '�������', '�������û�����������Ϣ���������һ�¸���', '', 0, 0, 0, 'all', 'newbie_sendpm', 0, 0, 0, 'credit', '2', 10, -1, ''",
		'vars' => array(
			"'complete', '��ָ����Ա���Ͷ���Ϣ', '".addslashes('ֻ�и��û�Ա�ɹ����Ͷ���Ϣ���������������д�û�Ա���û���')."', 'authorid', 'text', '', ''",
			"'setting', '', '', 'entrance', 'text', 'space', ''"
		)
	),
	6 => array(
		'name' => 'һ���ú�������',
		'task' => "1, 0, 'һ���ú�������', '������ģ�û����������ô�У��Ӹ����Ѱ�', '', 0, 0, 0, 'all', 'newbie_addbuddy', 0, 0, 0, 'credit', '2', 10, -1, ''",
		'vars' => array(
			"'complete', '��ָ����Ա��Ϊ����', '".addslashes('ֻ�н��û�Ա��Ϊ���Ѳ��������������д�û�Ա���û���')."', 'authorid', 'text', '', ''",
			"'setting', '', '', 'entrance', 'text', 'space', ''"
		)
	),
	7 => array(
		'name' => '��Ϣʱ��',
		'task' => "1, 0, '��Ϣʱ��', '��Ϣʱ����ȱ��ʲô������', '', 0, 0, 0, 'all', 'newbie_search', 0, 0, 0, 'credit', '2', 10, -1, ''",
		'vars' => array(
			"'complete', 'ѧ������', '".addslashes('���������ֻҪ�ɹ�ʹ����̳�������ܼ����������')."', '', '', '', ''",
			"'setting', '', '', 'entrance', 'text', 'search', ''"
		)
	)
);


$testdatacontent = array();
$testdatacontent[0]['subject'] = '��̳��Ӫ�ؼ� - Discuz! 7.1 �¹��ܵ����� ';
$testdatacontent[0]['message'] = <<<EOD

Discuz! 7.1 ���Ƴ��ļ������ܣ�����Χ���������̳�Ļ����Զ���Ƶģ��ú�������Щ�¹��ܣ����������̳�û�ճ�ԣ���ǿ��Ա֮��Ļ����ԣ�����Ƣ�档����������˵˵����θ�����̳��ʵ��������������������Щ�¹��ܡ�[p=30, 2, left]
[b][size=4]һ����̳��̬����ҳ��ʾ���[/size][/b] [/p][p=30, 2, left]
[b]���Ŀ�꣺[/b] [/p]
ͨ������̳�¼�չʾ��ʽ���Ż�����ǿ��̳��Ϣ�Ĵ��ݹ��ܣ������̳��Ա֮��Ĺ�ͨЧ�ʡ�[b]����ָ����[/b]
�ܶ���ϲ�� UCHome �к��Ѷ�̬���ܣ�Discuz! ����һ����������̳ϵͳ��ͨ��ָ������������̳��̬��Ϣ���ٽ���Ա֮�以���Ĳ���������Ŀ��ֵӦ�ø��ݵ�ǰ��̳��Ӫ״����ϸ���ö����� ���磺��̳�շ�������100���ϵģ����á�����ظ����ﵽһ��ֵ���Ͷ�̬��ʱ������������ ��10, 30, 80��  �����������ⱻ�ظ���10�Σ�30�Σ�80�ε�ʱ������̳��̬ҳ����һ����̬��Ϣ���շ�������1000���ϵ���̳���Ϳ������á�30��100��200�����ܽ�������̳С����Ծ�û��٣��շ�����������ôӦ�ý�����Ŀ����ֵ���ͣ���������̳��̬�����ײ������෴����̳�󣬻�Ծ�û��࣬�շ������ܴ���ôӦ�ý�����Ŀ����ֵ���ߣ�������̳��̬���ģ�Ӱ���û����顣


[img]http://faq.comsenz.com/attachments/2009/10/26_200910091741481w8rg.thumb.jpg[/img]

��̳��ҳ֧��������ʾ����ˣ�һ���Ǵ�ͳ����̳����б����ʽ��������ʽ��ʷ�ƾã�������û�����Ϥ��������һ�����Ŀ¼���û����Ը��ݰ�������ٵ�ȷ���Լ�����Ȥ�Ļ�����ʲô�ط����ڶ�����̳��ҳ��ʽ����7.1���Ƴ�����̳��̬����������ʽ�£���̳�ڵĸ����¼���̬��Ϣ���㼯����̳��̬�б��У�����ĳ�˷�������ظ�������1000�ˣ�ĳĳ�����ӱ�������Ϊ�����˵ȵȡ��������¼�Ϊ���ģ���̬��ϢΪ���ֵ���ʽ�����Դ���ǿ��̳�û�֮��Ļ����ԣ���Ϣ���������ˣ������Ծ͸����ײ���������ͻ��һ����ʵ���������ĸо���
�������ر�����һ�£��տ�ʼ��Ӫ���õ���̳���������ݲ�����ʵ������������Ҳ�Ͳ������˸���������̳��̬��Ϣ����˶����ڲ���������ҳ���Ϊ����̳ʵʱ��̬��


[img]http://faq.comsenz.com/attachments/2009/10/26_200910091742581Wdb4.png[/img][p=30, 2, left]
[b][size=4]�������������ȶȺ����۵������Ƽ���ʽ[/size][/b] [/p][p=30, 2, left]
[b]���Ŀ�꣺[/b] [/p]
��ǰ�汾�еİ����Ƽ����߱��㷺���õ���ҳ�ĸ����ȹ��ܶ���Ϊ�˴ﵽͬһ��Ŀ�ģ�����̳�ĸ��֡����֡����߾�����Ϣ��������û��������׵Ŀ�������ʹ���ǲ������С� ����ǰ�ķ�ʽ���ڶ���̳������û��һ��ͳһ��׼������ϵͳ����˲��ܲ���������Ϣ��ȡ��ʽ�����᲻����׼����� Discuz! �����������ȶȵĸ������̳�û��㷺�Ĳ��뵽��̳���ݵ���������������̳���ֺ;�����Ϣ����ȡ��չ�ֱ�ø���׼ȷ�����ӷ��㡣
[b]����ָ����[/b]
��[url=http://faq.comsenz.com/viewnews-851]�����ȶ�[/url]����Ӱ�������������б���ʾʱ�����ͼ�����ʾ(��ͼ)��������ȶȸ��ݻظ���������ֵ�Ȳ�������һ���㷨����õ������ȶ�ֵ�ﵽ�趨����ʾ������50��100��200 ʱ���������б�������ı�������ʾ��Ӧ�����ͼ�꣬����ʾ����������ų̶ȡ�վ��Ӧ�ø���վ�㵱ǰ��Ӫ������趨��Щֵ��һ���Ƽ��ķ����Ǳ�֤�����б��У������������ͨ����ı����� 1:7 ���ҡ�

[img]http://faq.comsenz.com/attachments/2009/10/26_200910091749011stPk.png[/img]

��[url=http://faq.comsenz.com/viewnews-851]��������[/url]������ͨ���ռ��û�����������ۣ�����������ͼ�����ʾ���𣬵��ﵽ�趨�ļ�����ֵʱ���������б�����ʾ��������Ķ�Ӧ������Ƽ�ͼ�ꡣ�û������ٺ�̨�����������۵Ĵ��� ����ӷֲ����ͼ��ֲ����ֱ�����Ϊ���������ȡ����ߡ�֧�֡������ӡ��ȣ����������������û�������Ȥ���뵽�����������������

[img]http://faq.comsenz.com/attachments/2009/10/26_20091009174901250VL.png[/img]

��[url=http://faq.comsenz.com/viewnews-854]��̳�ȵ�[/url]���Ǹ��������ȶ���ѡȡһ�����ȵ�����չʾ����ҳ������ѡȡ�������ȶȸߣ�������ǿ����֮��ʾ����ҳ�����ܺõĴٽ���̳���յĻ�Ծ��������վ������̳�ȵ��Ч������ܺã���Ϊ��վ�����٣������٣���ȡ�������ȵ�׼ȷ�Ծͻ����ۿۡ�����һ�������������������ȵ�����������ȵ㡣һ���Ƽ��ķ�ʽ�ǣ���߷���ͼƬչʾ���ұ����� 10 �� 14 ���Ƽ����⡣

[url=http://faq.comsenz.com/attachments/2009/10/26_200910091744121xhVg.png][img=644,186]http://faq.comsenz.com/attachments/2009/10/26_200910091744121xhVg.png[/img][/url]

��[url=http://faq.comsenz.com/viewnews-852]�Ƽ�����[/url]������ͨ���Զ����ֶ���ʽ����̳��������ȡһЩ������Ϊϵͳ�Ƽ������⣬��Щ����һ��Ϊ��̳�����ݾ��ʡ��û�����ȸߵĻ��⡣�Ƽ����������Ӧ���ú���̫���������ۻ����ң�̫�������ۡ����ݻ���ʱ��ҲҪ���õõ�����ֵ����̫�������ݳ�ʱ�䲻���£�����������½�������̫СƵ�����»����ֻ����ӷ��������������������Ƽ������������ʾ�������б�ҳ��Ҳ������ʾ������鿴ҳ����ͼ����һ�������Ͱ�黰����ּ���ϳ̶ȸߣ������������ʳ̶ȸߵ������ʺ���ʾ�������б�ҳ������������ӱ��������ǿ����ʺ���ʾ������鿴ҳ��

[img]http://faq.comsenz.com/attachments/2009/10/26_200910091759431vD2J.png[/img]
[url=http://faq.comsenz.com/attachments/2009/10/26_200910091759432UPgA.png][img=644,459]http://faq.comsenz.com/attachments/2009/10/26_200910091759432UPgA.png[/img][/url]
[p=30, 2, left]
[b][size=4]�����������û������չ����û�[/size][/b][/p]

[b]���Ŀ�꣺[/b]
���Ż������ķ�չ����̳�û�Ⱥ��Խ��Խ�㷺���ܶ���û�ж������������ʹ�þ��飬���ǳ�Ϊ��̳���û���������֪���Լ�������̳��Щʲô��ͨ�����������ܣ����򵼵�ָ���£�����Щ�û��ܿ��������̳�������������ٶ���̳�е�İ��������Ĳ��뵽��̳�ĸ��ֻ���С�
[b]����ָ����[/b]
����վ���ڽ��С�[url=http://faq.comsenz.com/viewnews-853]��������[/url]��������ʱ���濼�����������ͺ;���Ľ�����ֵ��һ��������ͬʱʹ�ö��ֽ�����ʽ(��̳���趨���������ߡ��͡�ѫ�¡�����)���ܼ��������ǰ����������������ꡣ�Ի��ֵ�����ҲҪ������Σ���Ҫ��������Ľ�����������ͬ�Ļ�����ֵ��վ��Ҳ�����޸������������ø��Ѻá�������������������������������û�������������Ȥ��������һЩ���飺

  ����һ������������д��ѧϰ������ ������10����Ǯ ���������������д�ɡ���ʼ�ҵĵ�һ�Ρ�������һ�ֵ��ߡ� ��������������д�ɡ����ڲ�ͬ��������һöѫ�¡�
   վ��Ӧ�ø����Լ�վ���û�Ⱥ����������������Щ����������Ŀ�������û�Ⱥ��ҪΪ����ʱ�䲻�����Է������������в���Ϥ���û�����ô�Ϳ���ѧϰ��������������ȱȽϳ�������������û�ȺΪ�Ѿ���һ���������飬����̳����ķ������������Ѿ��Ƚ���Ϥ����ô�Ϳ���ֻ�����޸ĸ������ϣ��޸�ͷ��ȱȽϸ߼�������
[color=#ff0000]��ע�� ���еĹ�������"���������ơ�" ��ʽ��ע �����磺��[/color][url=http://faq.comsenz.com/viewnews-853][color=#ff0000]��������[/color][/url][color=#ff0000]�� ����������Ƶ����ӣ����Բ鿴�ù��ܵ�ʹ��˵����[/color]
EOD;

$testdatacontent[1]['subject'] = 'Discuz! 7.1 �¹��ܡ���վ���Ƽ� ';
$testdatacontent[1]['message'] = <<<EOD

Discuz! 7.1 ����վ���Ƽ����ܣ�����Ա��������һ��������Ϊ��վ���Ƽ�����������Ϊ��վ���Ƽ�������Щ���Ӿͻ�����������ҳ�����½��Ը������ڵ���ʽչʾ������ߣ�����ж�����ⱻ����Ϊ��վ���Ƽ��������������ʾ��
վ���Ƽ��������Ϊһ��ȫ�������Ƽ�������Ա���Խ�һЩ�����Ҫ��Ϣ������֪ͨ�����������Ϊվ���Ƽ����Ա�֤������û���������������߻������ڴ˼�����������Ӯ����һ�����ܶ����÷�����λվ�����Ը�������������ú�ʹ�á�
��վ���Ƽ����ں�̨������λ��Ϊ��Discuz! 7.1 ��̨ => ��� => վ���Ƽ�
[img]http://faq.comsenz.com/attachments/2009/09/9_200909271722051W2zR.gif[/img][p=30, 2, left]һ������վ���Ƽ�[/p]
�����ڴ�����վ���Ƽ�������⣬Ĭ��Ϊ��վ���Ƽ���������������Ϊ��ϣ���ı��⣺
[img]http://faq.comsenz.com/attachments/2009/09/9_200909271722052Ji2X.gif[/img][p=30, 2, left]�������վ���Ƽ�[/p]
����ȵ㻰�������ַ�ʽ���ֶ���ӡ�������⡢�Զ���ӡ�[p=30, 2, left]1���ֶ����[/p]
��ͼ�����е������ӡ����ڵ����������������ǵ�����ֶ���ӡ���

[img]http://faq.comsenz.com/attachments/2009/09/9_200909271722053xvtI.gif[/img]



���롰�Ƽ����ӵ�ַ�������Ƽ����⡱�����Ƽ����ݡ��͡�����ͼƬ����Ȼ���ύ�������ֶ����һ��վ���Ƽ���
[img]http://faq.comsenz.com/attachments/2009/09/9_200909271722054Fae6.gif[/img]
��ͼ�����е�����ύ��������ӳɹ�������Ϊ��վ���Ƽ�����
[img]http://faq.comsenz.com/attachments/2009/09/9_200909271722055tNdh.gif[/img]
����ǰ̨������������ҳ�����½ǾͿ��Կ������Ǹղ����õĸ�վ���Ƽ���
[img]http://faq.comsenz.com/attachments/2009/09/9_200909271722056CKUJ.gif[/img]
[b]2���������[/b]
��̨��վ���Ƽ����������ӡ����ڵ����������������ǵ����������⡱��
[img]http://faq.comsenz.com/attachments/2009/09/9_200909271722057gPEE.gif[/img]
�������ӵ�ַ��������ȡ�������ݡ�����Ի�ȡ�������ӵı������������ժҪ��
[img]http://faq.comsenz.com/attachments/2009/09/9_200909271722058BY2H.gif[/img]
[img]http://faq.comsenz.com/attachments/2009/09/9_200909271722059Fwkt.gif[/img]
�ύ���ɳɹ���Ӹ�����Ϊվ���Ƽ���Ϣ��
[p=30, 2, left]3���Զ����[/p]
��̨��վ���Ƽ����������ӡ����ڵ����������������ǵ�����Զ���ӡ���
[img]http://faq.comsenz.com/attachments/2009/09/9_2009092717220510PAXX.gif[/img]
ϵͳ���Զ��Ƽ� 10 ��������Ϊվ���Ƽ���ѡ���Զ��Ƽ���ԭ��Ļ�ȡվ�����а������Ƽ����⣬��������Ƽ����ⲻ�� 10 �����м����Զ��Ƽ�������
[img]http://faq.comsenz.com/attachments/2009/09/9_200909081525058gOVO.gif[/img]
����Ա�����ڴ�ѡ����Щ�Ƽ���������Ϊվ���Ƽ���ѡ���Ƽ�����ǰ��Ķ�ѡ�򣬡��ύ�����ɡ�[p=30, 2, left]��������վ���Ƽ�[/p]
���б�����Ϊվ���Ƽ�����������Ϊվ���Ƽ������ⶼ���ڴ���ʾ������Ա�����ڴ�����һЩ����Ϊվ���Ƽ�Ҳ����ȡ����վ���Ƽ�����ݣ������Ա༭��Щ���⣬���߽���Щ����ɾ����վ���Ƽ���
[img]http://faq.comsenz.com/attachments/2009/09/9_2009092717220511ZOHp.gif[/img][p=30, 2, left]����ǰ̨��ʾ[/p]
����������վ���Ƽ���ǰ̨����ʾЧ���������һƪ��������ҳ�������½����ǿ��Կ���һ���������ڣ��������վ���Ƽ�����ʾ���棺
[img]http://faq.comsenz.com/attachments/2009/09/9_2009092717220512QfsZ.gif[/img]
�ڸø��������е�����ӱ��⡢����ժҪ���������½ǵġ��鿴�����Ӷ����Խ������������ҳ�鿴���顣
�����������Ŵ���Ѿ�����˸ù��ܵ�ʵ����;��ʹ�÷�������ô���Ͽ�����ɣ�
EOD;

$testdatacontent[2]['subject'] = '��̳�ȵ㣺�ڵ�һ�۾���ס�û� ';
$testdatacontent[2]['message'] = <<<EOD

��������̳�ڵ�һ�۾���ס�û�����Ҫ��ʱ�İ��ȵ��¼����ݸ��û����ڵ�һʱ������ܻ�Ա��ӭ��������Դչʾ���������ܹ�����޶ȵ������̳�Ļ����ԣ�������������Ŷȡ�
Discuz! 7.1��������̳�ȵ㹦�ܣ��ܹ��ܺõ���������۽���Ӧ�����󡣸ù��ܿ��Խ������������۵��������ʾ����̳��ҳ��ͷ���������û���������̳������ȵ���Ϣ����̳�ȵ�����ú� Discuz! ����������һ�����ǳ��򵥣�ֻ��Ҫ�Թ���Ա��ݵ���̨�򵥿������ɡ�

���ǽ�ͼ����һ�¿������Ч����
[img]http://faq.comsenz.com/attachments/2009/09/9_200909110951061nyw4.gif[/img]
����������˵һ�º�̨�Ŀ���������
���� Discuz! 7.1 ��̨ => ���� => �������� => ��ҳ���ã�
[img]http://faq.comsenz.com/attachments/2009/09/9_200909271530271hxkL.gif[/img]
��ͼ�������ǿ��Կ�������̳�ȵ㡱����ѡ������ѡ���ǡ������ø�����ʾ������
[img]http://faq.comsenz.com/attachments/2009/09/9_200909101157263lTaf.gif[/img]
��̳�ȵ㣺��/�������Ƿ���ʾȫ��̳����̳�ȵ����⡣
��̳�ȵ���ʾ������������̳�ȵ���Ŀ����Ĭ��ֵ 10 ����
��̳�ȵ�������ڣ��룩��������̳�ȵ��ڶ೤ʱ�����һ�Σ�Ĭ��ֵ 900 ��
��̳�ȵ�ͼƬ��С��������ҳ��̳�ȵ�ͼƬ�Ĵ�С��Ĭ��ֵ 100*70 ��
��̳�ȵ����ݽ�ȡ���ֳ��ȣ�������̳�ȵ����ݵ����ֳ��ȣ�Ĭ��ֵ 200 ���֡�
������ú��Ч����ͼ��ƪ��һ��ͼƬ��ʾ����Ҫע����ǣ��������õġ���̳�ȵ���ʾ������Ϊ 10 ��ָ�Ҳ಻����ͼƬ���ȵ����⣬���������Ĵ�ͼƬ���⡣
��̳�ȵ����ʾ�ṹΪ��
���һ�е��ô�ͼƬ�������ȶ����һ�����⼰��ͼƬ����ͼ����������ժҪ���䷢��ʱ������ߣ�
�Ҳ���������ȶ���ߵ� 10 ������ͼƬ�����⣬���������ȶȴӸߵ�������ǰ�� 2 ����ʾ������⡢���߼�������ժҪ������� 8 ��֮��ʾ������⡣
������̳�ȵ�Ŀ�����Ч��չʾΪ��ҽ�������ˣ���ô������������ǲ��Ǻ������㣬����ʲô���Ͽ찲װ���°� Discuz! 7.1 ���߽�������̳������ Discuz! 7.1 ������ɣ�
EOD;

$testdatacontent[3]['subject'] = 'Discuz! 7.1�����ԡ��������ȶ�/����';
$testdatacontent[3]['message'] = <<<EOD

Discuz!7.1 �ڶ�����Ĳ���������������ԣ��������ȶȺ��������ۡ�
�����ȶȣ��û����Զ�������лظ������۵Ȳ�������Щ�����������������ȶȣ�������ﵽһ�����ȶ�ʱ������ʾ�ȶ�ͼ�ꣻ
�������ۣ��û����Զ�ĳ�������ۣ��Ա���Լ��Ĺ۵�̬�ȣ����������õ�����ָ���ﵽ��̨���õ�ָ������ʱ������ʾ����ͼ�ꣻ
�����������Զ����������û������Ч�ʣ������������ȶȸ߻����۸���������������
�����뿴��ϸ���ܣ�[p=30, 2, left][b]һ�������ȶ�[/b][/p]
�����ȶ��� Discuz!7.1 ���������ԣ�վ�������ں�̨���������ȶ�ֵ���ȶ�����Ȩ��ֵ����ĳ������ﵽվ�����õ��ȶ�ʱ�����������б�ҳ�����Ҳ���ʾ�ȶ�ͼ�꣬�������ȶ���������������
1����̨����
��̳��̨ => ȫ�� => ��̳���� => �����ȶȣ�����ͼ��
[img]http://faq.comsenz.com/attachments/2009/09/15_200909271400321aEXX.gif[/img]
�ظ�����Ȩ�أ�ÿ�λظ�����ʱ�������ȶȵ�������Ĭ��ֵΪ 5 ��
��������Ȩ�أ�ÿ�ζ�����������۲���ʱ�������ȶȵ�������Ĭ��Ϊ 3 ��
����������ʾ�������������б�ҳ�����ȶȵļ����Ӧ�ȶ�ֵ��ÿ����������Ӧ���ȶ�ͼ�꣬�����������𡣼����ȶ�ֵ���ö��ŷָ�������Ϊ����ʾ�ȶ�ͼ�ꡣ
������Ϻ󣬵�����ύ����ť������á�
������ɺ󣬵�ǰ̨�����б�ҳˢ�£��ﵽ�ȶ�ֵ������ǰ����ʾ�����ȶ�ͼ�꣺
[url=http://faq.comsenz.com/attachments/2009/09/15_200909081603201rueN.gif][img=644,171]http://faq.comsenz.com/attachments/2009/09/15_200909081603201rueN.gif[/img][/url]
2�����������ȶ����������
�û����Ը��������ȶ����������б�����������������б�ҳ��
[img]http://faq.comsenz.com/attachments/2009/09/15_200909081603202zliR.gif[/img][p=30, 2, left][b]������������[/b][/p]
���������� Discuz!7.1 ���������ԣ���̨���Ƿ����ù��ܵĿ��أ�����վ�������ں�̨��������ͼ����ʾ����
1����̨����
��̳��̨ => ȫ�� => ��̳���� => �������ۣ�����ͼ��
[img]http://faq.comsenz.com/attachments/2009/09/15_200909271400351aw5h.gif[/img]
[img]http://faq.comsenz.com/attachments/2009/09/15_200909271400371JFAt.gif[/img]
[img]http://faq.comsenz.com/attachments/2009/09/15_200909271400401hrE4.gif[/img]
�����������ۣ�ѡ���ǡ������������۹��ܡ�
�ӷֲ������֣��������ۼӷ���ı�����֣��������õļ����ˣ����Ҳ��˹�����Ĭ��ÿ����һ�μ� 1 �����ۻ��֡�
���ֲ������֣��������ۼ�����ı�����֣��������õļ����ˣ����Ҳ��˹�����Ĭ��ÿ����һ�μ� 1 �����ۻ��֡�
Ĭ����ʾ��ֵ����������������ҳĬ����ʾ�����۽����ֵ���û�������л���
ÿ 24 Сʱ������������������û�ÿ 24 Сʱ�������۶���ƪ���⣬0 ������Ϊ�����ơ�
�Ƿ����������Լ������ӣ������Ƿ����������Լ������⣬�����Լ��������޻��ֽ�����
����ͼ����ʾ�������������б�ҳ����ͼ��ÿһ�����Ӧ������ָ��������Ϊ 3 ������ÿ�����������ָ�����ö��ŷָ���
���úã�������ύ��������á�
2��ǰ̨��������
��ǰ̨�����������ʱ�����ῴ������ͼ��
[img]http://faq.comsenz.com/attachments/2009/09/15_200909081605032M3vn.gif[/img]
�����������ָ���ﵽ��̨���õ�ָ������ʱ���������б�ҳ������ʾ��Ӧ���������ͼ�꣺
[url=http://faq.comsenz.com/attachments/2009/09/15_200909081605033D2LF.gif][img=644,201]http://faq.comsenz.com/attachments/2009/09/15_200909081605033D2LF.gif[/img][/url]
����ͼ���ֱ�۵ظ��߸��û���������ۣ���˻����������۸���������������
���ˣ������ȶȺ����������Ѿ�˵����ϣ��Ͻ�ȥ�����°ɣ�
EOD;

$testdatacontent[4]['subject'] = 'Discuz! 7.1 �����ԡ����Ƽ�����';
$testdatacontent[4]['message'] = <<<EOD
D
Discuz!7.1 �汾�������Ƽ����⹦�ܣ��Ƽ�����ʱ�����޸�������⡢����ѡ���Ե��Ƽ������е�ͼƬ�ȣ��û��������˺ܴ����ߡ�
�����뿴��ϸ���ܣ�[p=30, 2, left][b]һ����̨�����Ƽ������Ȩ��[/b][/p]
��̳��̨ => ��� => ������ => �༭�����ɿ�����ͼ��ʾ��
[img]http://faq.comsenz.com/attachments/2009/09/15_200909081607261Sfhr.gif[/img]
ѡ���Ƽ�����ķ�ʽ���������ã�
[img]http://faq.comsenz.com/attachments/2009/09/15_200909081607391e0B1.gif[/img]
[img]http://faq.comsenz.com/attachments/2009/09/15_200909081607392ZKwm.gif[/img]
���úú󣬵�����ύ������Ƽ�����ĺ�̨���á�[p=30, 2, left][b]����ǰ̨�Ƽ�����[/b][/p]
���Ƽ�Ȩ�޵��û���ǰ̨�����Ƽ�ĳ�����⣬��ͼ��
[url=http://faq.comsenz.com/attachments/2009/09/15_200909081607393hITH.gif][img=644,131]http://faq.comsenz.com/attachments/2009/09/15_200909081607393hITH.gif[/img][/url]
�Ƽ��������ã�
[img]http://faq.comsenz.com/attachments/2009/09/15_200909081607394uMLw.gif[/img]
���Ƽ������⣬�����������б�ҳ����������
[img]http://faq.comsenz.com/attachments/2009/09/15_200909081607395MQ0W.gif[/img]
����Ϊֹ���Ƽ����⹦���Ѿ�������ϣ��Ͻ�ȥ�����°ɣ�
EOD;

$testdatacontent[5]['subject'] = 'Discuz! 7.1 �����ԡ�����������';
$testdatacontent[5]['message'] = <<<EOD

Discuz!7.1 ����̳�����������Ľ���ϵͳ������Ե��Դ��� 7 ��������������վ����ӡ���ע���û����Զ���������񣬴˹��ܿ����ڷ�ֹ����ע�ἰ�����ˮ�����������ֿ�����Ϥ��̳��
�����뿴��ϸ���ܣ�[p=30, 2, left][b]һ����̨����[/b][/p]
ϵͳ�Դ��� 7 �����������ں�̨���п��أ����������û�ر�����
��̳��̨ => ��չ => ��̳��������ͼ��
[url=http://faq.comsenz.com/attachments/2009/09/15_200909081610241JZg0.gif][img=644,436]http://faq.comsenz.com/attachments/2009/09/15_200909081610241JZg0.gif[/img][/url]
�Ƿ�����̳����ѡ���ǡ�������̳�������ѡ�񡰷񡱣���ô��������ö��ǲ������õġ�
���ã���ѡ��ʾ��������ã��û�ע�����Զ�����������������ѡ����ʾ�����ã��û�ע��󣬲��ῴ��������[p=30, 2, left][b]�����༭��������[/b][/p]
�����Ե�������ġ��༭�����༭�������һЩ��Ϣ����ͼ��
[img]http://faq.comsenz.com/attachments/2009/09/15_200909081610341n8AY.gif[/img]
�༭�õ�����ύ����ɱ༭��[p=30, 2, left][b]��������ǰִ̨������[/b][/p]
�û�ע��󣬵�¼ǰ̨�����ɿ��������������ʾ����ͼ��
[img]http://faq.comsenz.com/attachments/2009/09/15_200909081610343KZDL.gif[/img]
����ÿ���һ�����񣬾ͻ�õ���Ӧ�Ľ�����
���ˣ����������Ѿ�������ϣ��Ͻ�ȥ�����°ɣ�
EOD;

$testdatacontent[6]['subject'] = '�����ע������̳���Ӷ�����';
$testdatacontent[6]['message'] = <<<EOD

�û���������̳�еĺ���Ӧ������Ч��Ϣ�Ļ�ȡ�����û��ڡ��䡱��̳��ʱ�򣬷�����һ���ܸ���Ȥ�Ļ���������Ҫһ����ʱ��ɱ�������û�����һ�����ӣ��������ݺ�����Ļظ���������˼����ʱ���û��϶����뼴ʱ�˽⵽���ӵķ�չ��������ˡ������ע�����ܣ����Ժܷ����ʵ��������Ϣ�Ļ�����ֻ��Ҫ�û������ֵ�ù�ע��������Ϊ��ע״̬��������Ա�ظ�������ʱ���û��Ϳ����յ�һ�������Ե�֪ͨ��ֻ��Ҫ�������һ�㣬�ͻῴ������ע������������Щ��־ͬ���ϡ��ߵĻظ��ˡ�����̳�������ݻ����������ǲ��ǻ��в�һ���о��أ�
�������ע��������������̳����ϸ���ϵ�ʵ�֣��������������������û��Ķ�ϰ�ߵĻ���֮�ϵ��������¡��û�ä��Ŀ�ĵ�ȥ����������ݺ������γ��Ķ�ƣ�ͣ��������ע���������ɵİ��û����ĵ�������Ϣ������չʾ���û������û�����Ч��Ϣ��ȡ���Ӿ�׼��Ч��
�������һ�»�Ա���ʹ�������ע���ܣ�[p=30, 2, left][b]һ����ע����[/b][/p]
Ҫ��һ��������Ϊ����ע״̬���������ַ�����
1������������ʱ
�ڷ��������ʱ�򣬵���������⡱��ť�Ҳ�ġ�����ѡ���Ȼ���ڡ�����ѡ���ѡ�С���ע���⡱�ĸ�ѡ��
[img]http://faq.comsenz.com/attachments/2009/09/42_200909071426591JMm0.gif[/img]
[img]http://faq.comsenz.com/attachments/2009/09/42_200909071426592XJFt.gif[/img]
2������ʱ
������������·��� ����ע��ͼ��
[img]http://faq.comsenz.com/attachments/2009/09/42_2009090714265936C8U.gif[/img]
3���ظ�ĳ����ʱ
�ڿ��ٻظ��򡰷���ظ�����ť�Թ�ѡ����ע���⡱��ѡ��
[img]http://faq.comsenz.com/attachments/2009/09/42_200909071426594Q6fh.gif[/img][p=30, 2, left][b]�����鿴����ע������[/b][/p]
1����ע��Ϣ��ʾ
��������ע���������µĻظ�ʱ��������ҳ�涥������������ʾ��������ͨ�����֪ͨ��Ĺ�ע��ʾ�鿴����ע��������»ظ�
[img]http://faq.comsenz.com/attachments/2009/09/42_200909071426595h1BX.gif[/img]
2���ڡ��������ġ��ġ��ҵĹ�ע���в鿴��ע�б�
�ڹ�ע�б��У�������ѡ��鿴���ڹ�ע�ġ����»ظ������⡱���ߡ�ȫ�����⡱
[img]http://faq.comsenz.com/attachments/2009/09/42_200909071426596KB38.gif[/img]
[img]http://faq.comsenz.com/attachments/2009/09/42_200909071426597be40.gif[/img][p=30, 2, left][b]����ȡ����ע����[/b][/p]
1�����ѹ�ע������Ŀ��ٻظ����µġ�����ظ�����ť�Թ�ѡ��ȡ����ע����ѡ�������ظ�������⽫���ٴ��ڱ���ע״̬
[img]http://faq.comsenz.com/attachments/2009/09/42_2009090714265987Qgz.gif[/img]
2���ڶ���2���Ĺ�ע�б���ѡ����Ҫȡ����ע�����Ⲣ������ύ����ť����˲�����ʹ��ѡ�����ⲻ�ٴ��ڱ���ע״̬
[img]http://faq.comsenz.com/attachments/2009/09/42_200909071426599uWKh.gif[/img]
���⣬վ��Ĺ���Ա���˿��Ժ���ͨ��Աһ��ʹ�������ע�����⣬��������ϵͳ�����жԻ�Ա�������ע�б��������п��ƣ�����������£�
ϵͳ���� => ȫ�� => �û�Ȩ�� => �����ע�б�����
[img]http://faq.comsenz.com/attachments/2009/09/42_20090907142659101UTM.gif[/img]
EOD;

$testdatacontent[7]['subject'] = 'Discuz! 7.1 �����ԡ�����̳��̬';
$testdatacontent[7]['message'] = <<<EOD
Discuz! 7.1 ��������̳��̬ʵʱ������ܣ��û��������ԭ���İ���б�����⻹�����л�������̳��̬������鿴��̳ʵʱ��̬���˹��������� SNS �Ķ�̬�鿴���ܣ����Լ��в鿴�����к��ѵĶ�̬��Ϣ�������������Ѹ�����ҳ�鿴��
[img]http://faq.comsenz.com/attachments/2009/10/9_2009100915323019jA5.gif[/img]
վ�������� Discuz! 7.1 ��̨������̳��ҳ��Ĭ����ʾ��񣬽��� Discuz! 7.1 ϵͳ���� => ���� => �������� => ��ҳ���� => ��ҳ��ʾ���
[img]http://faq.comsenz.com/attachments/2009/10/9_200910091532302Z0N9.gif[/img]
�������б�����̳����б�չʾģʽ��Ҳ�����ϰ汾�� Discuz! ��չʾЧ����
��̳ʵʱ��̬���� Discuz! 7.1 �����Ķ�̬ʵʱ�鿴���ܣ��û������ڴ˼��в鿴�����к��ѵĶ�̬��Ϣ��
���վ��ϣ��վ�������û���¼ Discuz! �󿴵�������ѵĶ�̬��Ϣ����ô������Ϳ���ѡ����ҳ��Ĭ����ʾ���Ϊ����̳ʵʱ��̬����Ч������ͼ��ʾ��
[img]http://faq.comsenz.com/attachments/2009/10/9_200910091532303yDV1.gif[/img]
��ô�����ǲ��Ǻܷ��㣿�Ͽ찲װȫ�µ� Discuz! 7.1 ��������������̳����һ�°ɣ�
EOD;

$testdatacontent[8]['subject'] = 'Discuz! 7.1 �����ԡ�������ϵͳ';
$testdatacontent[8]['message'] = <<<EOD
Discuz! 7.1 ����������ϵͳ���������ݰ�����ȫ��������ϵͳ��Ϣ������������Ϣ������������Ϣ����������Щ��Ϣ��Ҫ������Ϣ��ͻ��ڵ����ġ����ѡ������������Ϣ��ʾ������ͼ��ʾ��
[img]http://faq.comsenz.com/attachments/2009/09/9_200909291118351ebXv.gif[/img]
���ŵ������ѡ��ϾͿ��Կ����յ�����Ϣ���ݡ�
��������ѡ����ɽ�������ϵͳ���棺
[img]http://faq.comsenz.com/attachments/2009/09/9_200909291118352Km5p.gif[/img]
����ϵͳ��ҳ��ȫ����Ϣչʾ������������Ե����������Ϣ�����������
ϵͳ��Ϣ����������������ѡ������������ѡ��������ѡ��������ѵȡ�
[img]http://faq.comsenz.com/attachments/2009/09/9_200909291118353g4uE.gif[/img]
������Ϣ����Ӻ��ѵ����ѡ�
[img]http://faq.comsenz.com/attachments/2009/09/9_200909291118354wCOX.gif[/img]
������Ϣ�������ע������ѡ�
[img]http://faq.comsenz.com/attachments/2009/09/9_200909291118355XZp5.gif[/img]
Discuz! 7.1 ����ϵͳ����ϸ���˸���������������Ϣ���ѣ�һĿ��Ȼ�ķ��࣬ʹ�ú������Ϣ����������ȷ��
EOD;

$testdatacontent[9]['subject'] = '���ӱ༭�����������ɷ���';
$testdatacontent[9]['message'] = <<<EOD
���������չʾ��������̳�ĺ��Ĺ��ܡ���Ա�ڷ����ظ�����ʱ���õ��ľͶ���Ǳ༭�����ܣ��ܶ�վ�������ڻ�Ա����ʱ�Ű���Ҷ��޷������һ��רҵ�༭�������跢�����Ի��Ľ��棬����Ҫ�ܹ������û����õĶ��������ݽ��б༭����������̳������Ű����רҵ�Ͷ�������
������Ա��ʹ��ϰ�ߣ�Discuz! 7.1 �ı༭�����˺ܴ�ĸĽ���������Ѻã�ͻ����ʾ���ò���ͼ�꣬���������˺ܶ๦�ܡ�
����ɾ���ߺͷָ��߱�ǩ�����������Ӹ�ʽ���ӷḻ��ʣ�����ͼƬ�������ÿ�ߣ����ֲ���֧�� mp3 wma ra rm ram midwav �ȶ��ָ�ʽ����Ƶ����֧�� wmv rm rmvb flv swf avi asf mpg mpeg mov�ȶ��ָ�ʽ����Ƶ�����Զ������ſᡢ������ku6����������Ƶ��վ����Ƶ��ַ�������ϴ����޸ġ�ɾ���Ȳ������ϵ��༭���У������ϴ����ƸĽ���ͬʱ�ϴ��������ʱ������ϴ�ʧ�ܵģ���Ӱ���Ѿ��ɹ��ϴ��ġ�
Discuz! 7.1 ǰ̨ => ���������ɽ���ȫ�µ� Discuz! 7.1 �༭����
[img]http://faq.comsenz.com/attachments/2009/09/9_200909281123281iC4p.gif[/img]
����ͼ������Կ�����Discuz! 7.1 �ı༭�����˺ܴ�ĸĹۣ�ͻ����ʾĿǰ�����Ƚϳ��õı��顢ͼƬ�����֡���Ƶ��Flash�����롢���ã���������Ѻá�
������ܼ����������ܣ�[p=30, 2, left][b]1������ɾ���ߺͷָ��߱�ǩ[/b][/p]
����ɾ���ߺͷָ��߱�ǩ�����������Ӹ�ʽ���ӷḻ��ʡ�
����ͼ��ʾ����Ϊ����ʡ��⡱��������ӡ�ɾ���ߡ���
[img]http://faq.comsenz.com/attachments/2009/09/9_200909071737197EwOi.gif[/img]
���ú��Ч������ͼ��ʾ��
[img]http://faq.comsenz.com/attachments/2009/09/9_200909071737198xEI5.gif[/img]
ͬ����ӷָ��ߣ�
[img]http://faq.comsenz.com/attachments/2009/09/9_200909071737199TrYW.gif[/img]
��Ӻ��Ч����
[img]http://faq.comsenz.com/attachments/2009/09/9_2009090717371910v6WE.gif[/img][p=30, 2, left][b]2������ͼƬ�������ÿ��[/b][/p]
[img]http://faq.comsenz.com/attachments/2009/09/9_2009090717371911dvfs.gif[/img][p=30, 2, left][b]3�����ֲ���֧�� mp3 wma ra rm ram mid wav �ȶ��ָ�ʽ[/b][/p]
[img]http://faq.comsenz.com/attachments/2009/09/9_2009090717371912wMUt.gif[/img]
��������ִ������ƣ�
[audio]http://vfile.home.news.cn/music/public/vd06/200908/18/50/MUfs06200908181354375150fd99.mp3[/audio]
����Ч����
[img]http://faq.comsenz.com/attachments/2009/09/9_2009090717371913aJ2z.gif[/img][p=30, 2, left][b]4����Ƶ���ܸ�ǿ��[/b][/p]
��Ƶ����֧�� wmv rm rmvb flv swf avi asf mpg mpeg mov �ȶ��ָ�ʽ
[img]http://faq.comsenz.com/attachments/2009/09/9_2009090717371914Ad5B.gif[/img]
�������Ƶ�������ƣ�
[media=wmv,400,300]http://w4180.s11.mydiscuz.com/Alizee_lais la bonita.wmv[/media]
�������Ч����
[img]http://faq.comsenz.com/attachments/2009/09/9_2009090717371915G371.gif[/img]
��Ƶ�������Զ������ſᡢ������ku6 ����������Ƶ��վ����Ƶ��ַ��
[img]http://faq.comsenz.com/attachments/2009/09/9_2009090717371916vS5U.gif[/img]
�������Ƶ�������ƣ�
[media=swf,400,300]http://player.youku.com/player.php/sid/XMTA3OTE4NjIw/v.swf[/media]
�������Ч����
[img]http://faq.comsenz.com/attachments/2009/09/9_20090907173719177BxG.gif[/img][p=30, 2, left][b]5���༭�����ϴ�����[/b][/p]
�����ϴ����޸ġ�ɾ���Ȳ������ϵ��༭���У������ϴ����ƸĽ���ͬʱ�ϴ��������ʱ������ϴ�ʧ�ܵģ���Ӱ���Ѿ��ɹ��ϴ��ġ�
�����ϴ���
[img]http://faq.comsenz.com/attachments/2009/09/9_20090907173719182BAP.gif[/img]
��ͨ�ϴ���
[img]http://faq.comsenz.com/attachments/2009/09/9_2009090717371919zovp.gif[/img]
�����б�
[img]http://faq.comsenz.com/attachments/2009/09/9_2009090717371920TWNz.gif[/img]
���� Discuz! 7.1 ���±༭���������ˣ���ô�����ǲ��ǳ����ţ�����ʹ���������ӵ���Ӧ�֣���ô����ʲô���Ͽ�����ɣ�

��̳��Ӫ�ؼ� - Discuz!7.1 �¹��ܵ����ã�[url=http://faq.comsenz.com/viewnews-869]http://faq.comsenz.com/viewnews-869[/url]
                                                                [p=30, 2, left][img]http://faq.comsenz.com/attachments/2009/09/9_200909101647044oJ0j.gif[/img][/p]                [p=30, 2, left]5[/p]                                                                [p=30, 2, left][img]http://faq.comsenz.com/attachments/2009/09/9_200909101647043aKeo.gif[/img][/p]                [p=30, 2, left]4[/p]                                                                [p=30, 2, left][img]http://faq.comsenz.com/attachments/2009/09/9_200909101650201K3yq.gif[/img][/p]                [p=30, 2, left]3[/p]                                                                [p=30, 2, left][img]http://faq.comsenz.com/attachments/2009/09/9_200909071737196ENcW.gif[/img][/p]
EOD;

$testdatacontent[10]['subject'] = 'ת����Ƶ�����׸�ǿ��';
$testdatacontent[10]['message'] = <<<EOD
Discuz! 7.1 ����Ƶ���ŷ������˺ܴ�Ľ�������֧�ֲ��� wmv rm rmvb flv swf avi asf mpgmpeg mov �ȶ��ָ�ʽ���������Զ������ſᡢ������ku6����������Ƶ��վ����Ƶ��ַ��������Ҫ��ȥ��ר�ŵ�ת�����õ�ַ��ʹ��ת����Ƶ��ø����׸�ǿ������Ϊ��Ҿ�����ʾ������������Ƶ��վ����Ƶת�����ܡ�
Discuz! 7.1 ǰ̨ => ���������ɽ���ȫ�� Discuz! 7.1 �༭����
[img]http://faq.comsenz.com/attachments/2009/09/9_200909271441261E3fW.gif[/img]
�������Ǵ��ſᡢ������ku6 ����������Ƶ��վ�ֱ���һ����Ƶ��ַ��
�ſ᣺[url=http://v.youku.com/v_show/id_XOTMwODQ2NjQ=.html]http://v.youku.com/v_show/id_XOTMwODQ2NjQ=.html[/url]
������[url=http://www.tudou.com/programs/view/4NgBn7J39bg/]http://www.tudou.com/programs/view/4NgBn7J39bg/[/url]
ku6 ��[url=http://v.ku6.com/show/VrLuttrXQb1CIbkC.html]http://v.ku6.com/show/VrLuttrXQb1CIbkC.html[/url]
������������ַ�ֱ������Ƶ����ͼ��ʾ��
[img]http://faq.comsenz.com/attachments/2009/09/9_200909111355583f0Ft.gif[/img]
�����Ĵ�������ͼ��ʾ��
[img]http://faq.comsenz.com/attachments/2009/09/9_200909111355584BDQP.gif[/img]
�ύ�������Ч������ͼ��ʾ��
[img]http://faq.comsenz.com/attachments/2009/09/9_200909111355585XszF.gif[/img]
���������ʾ���Կ�������Discuz! 7.1 �����ſᡢ������ku6 ����������Ƶ��վ����Ƶ��÷ǳ����ף������ٷ�������̳���õ�ַ��ֱ��������Ƶ����ַϵͳ�ͻ��Զ�������ȡ���յĲ��ŵ�ַ������ʲô���������������һ�Ѱɣ�
EOD;

$testdatacontent[11]['subject'] = 'Discuz!7.1 �����ԡ���ManyouӦ�õĿ���';
$testdatacontent[11]['message'] = <<<EOD

Discuz!7.1 ��֮ǰ�İ汾���ں�̨����� Manyou Ӧ�õĿ��ء���Ϊ�������ʽ��վ�������ں�̨������رոù��ܡ������ù��ܺ󣬻�Ա����̳Ҳ���Կ��� Manyou Ӧ�õĶ�̬��Ϣ����Ҷ�����ʲôӦ����Ϸ��ͬʱ�����Բ��������
�����뿴��ϸ���ܣ�
[b]һ����װ Manyou ���������[/b]

��̳��̨ => ��� => ��̳�������ͼ��

[url=http://faq.comsenz.com/attachments/2009/09/15_200909271430221otXN.gif][img=644,233]http://faq.comsenz.com/attachments/2009/09/15_200909271430221otXN.gif[/img][/url]
��װ�������øò������ͼ��
[img]http://faq.comsenz.com/attachments/2009/09/15_200909271432211ofuE.gif[/img][p=30, 2, left][b]�������� Manyou Ӧ��[/b][/p]
��̳��̨ => ��� => Manyou��
[url=http://faq.comsenz.com/attachments/2009/09/15_200909101753151btyX.gif][img=644,440]http://faq.comsenz.com/attachments/2009/09/15_200909101753151btyX.gif[/img][/url]
��������á���
[url=http://faq.comsenz.com/attachments/2009/09/15_2009091017533510KUl.gif][img]http://faq.comsenz.com/attachments/2009/09/15_2009091017533510KUl.gif[/img][/url]
�����MYOP Ӧ�ù�����
[url=http://faq.comsenz.com/attachments/2009/09/15_20090910175335228pj.gif][img=644,228]http://faq.comsenz.com/attachments/2009/09/15_20090910175335228pj.gif[/img][/url]
��������÷��񡱣�
[img]http://faq.comsenz.com/attachments/2009/09/15_200909101753353h7yn.gif[/img]
Ϊվ�㿪�������Ӧ�û���Ϸ��
[url=http://faq.comsenz.com/attachments/2009/09/15_200909101753354uW1y.gif][img=644,474]http://faq.comsenz.com/attachments/2009/09/15_200909101753354uW1y.gif[/img][/url]
��ΪĬ��Ӧ�ã������û��Ƿ���Ӵ�Ӧ�ã�Ĭ��Ӧ�ö�����ʾ�������û��Ŀ�ʼ�˵����档
�ر�Ӧ�ã��û�������Ӵ��ڹر�״̬��Ӧ�ã�Ӧ��Ŀ¼��Ҳ������ʾ���ڹر�״̬��Ӧ�á�
��Ϊ�Ƽ�Ӧ�ã�������Ϊ�Ƽ���Ӧ�ý���ʾ������վӦ��Ŀ¼���Ƽ���Ŀ�¡�
Ϊʹ���������̳��ҳ�����Ƽ���Ӧ�ò�����ز����������������ҳӦ���Ƽ���Ŀ����
[url=http://faq.comsenz.com/attachments/2009/09/15_200909101753355rwqi.gif][img]http://faq.comsenz.com/attachments/2009/09/15_200909101753355rwqi.gif[/img][/url][p=30, 2, left][b]����ǰ̨�鿴[/b][/p]
1��ǰ̨�鿴 Manyou ��̬
����̳��ҳ�����Կ�����ͼ��ʾ��
[url=http://faq.comsenz.com/attachments/2009/09/15_2009091017533561U7q.gif][img=644,244]http://faq.comsenz.com/attachments/2009/09/15_2009091017533561U7q.gif[/img][/url]
�����Ӧ�ö�̬�������ɲ鿴 Manyou Ӧ�õ���ض�̬��
[url=http://faq.comsenz.com/attachments/2009/09/15_200909101753357KO05.gif][img]http://faq.comsenz.com/attachments/2009/09/15_200909101753357KO05.gif[/img][/url]
�������̳��顱����������̳��ҳ�·������Ƽ��� Manyou Ӧ�ã�
[url=http://faq.comsenz.com/attachments/2009/09/15_200909101753358q9hK.gif][img=644,317]http://faq.comsenz.com/attachments/2009/09/15_200909101753358q9hK.gif[/img][/url]
���ˣ�Manyou Ӧ�ù����Ѿ�������ϣ��Ͻ�ȥ����һ�°ɣ�
EOD;

$testdatacontent[12]['subject'] = 'Discuz! 7.1 ��װͼ�Ľ̳�';
$testdatacontent[12]['message'] = <<<EOD

���̳̽����ȫ�°�װ Discuz!7.1 �ķ�����������ռ��ϰ�װ Discuz!7.1 Ϊ����ʾ������װ Discuz! 7.1 ǰ��ȷ���Ƿ��Ѿ���װ���� UCenter 1.5���ο��̳����£�
UCenter 1.5 ��װͼ�Ľ̳̣�[url=http://faq.comsenz.com/viewnews-449]http://faq.comsenz.com/viewnews-449[/url]
UCenter 1.5 ��װ��Ƶ�̳̣�[url=http://faq.comsenz.com/viewnews-494]http://faq.comsenz.com/viewnews-494[/url]
һ�������ʺ��Լ� Discuz!7.1 �汾�����ػ������
���ص�ַ��[url=http://www.comsenz.com/downloads#down_discuz]http://www.comsenz.com/downloads#down_discuz[/url]
˵�����ٷ��ṩ�� 4 �ֲ�ͬ�ı��롣���� GBK �������İ�(�Ƽ�)��UTF-8 �������İ桢BIG5 �������İ�(�Ƽ�)��UTF-8 �������İ档�������վ����Ҫ�ǹ��ڻ�Ա���Ƽ���ʹ�� GBK ��顣
������ѹ���ϴ���̳���򵽷��������޸���ӦĿ¼Ȩ��
1���ϴ���̳���򵽷�������
������ʾ�� GBK �ַ����汾Ϊ���������ַ����汾��Ҳ���մ˽̷̳�������װ������ѹ���õ�����ͼ��ʾ�������ļ��У�
[img]http://faq.comsenz.com/attachments/2009/10/9_2009101311182012w8v.gif[/img]
upload ���Ŀ¼����������ļ���������Ҫ�ϴ����������ϵĿ��ó����ļ���
readme Ŀ¼Ϊ��Ʒ���ܡ���Ȩ����װ��������ת���Լ��汾������־˵����
utilities Ŀ¼Ϊ��̳�������ߣ�������������� Tools �����䡣
������ upload Ŀ¼�µ������ļ�ʹ�� FTP ����Զ����Ʒ�ʽ������ FTP ��������Ƶ����÷���[url=http://faq.comsenz.com/viewnews-373]http://faq.comsenz.com/viewnews-373[/url]���ϴ����ռ䣨���½�ͼ��ʹ�õ� FTP ���Ϊ FlashFXP���йش˹��ߵ�ʹ�ý̳������FTP ʹ�ý̳̣�������ͼ��ʾ��
[img]http://faq.comsenz.com/attachments/2009/10/9_200910131118202xarO.gif[/img]
2���������Ŀ¼���ļ����ԣ��Ա������ļ����Ա�������ȷ��д
ʹ�� FTP �����¼���ķ���������������������Ŀ¼���Լ���Ŀ¼����������ļ�����������Ϊ 777��Win ���������� internet �����ʻ��ɶ�д���ԡ�
./config.inc.php
./attachments
./forumdata
./forumdata/cache
./forumdata/templates
./forumdata/threadcaches
./forumdata/logs
./uc_client/data/cache
����Ŀ¼Ȩ���޸Ŀ��Բο���[url=http://faq.comsenz.com/viewnews-183]http://faq.comsenz.com/viewnews-183[/url]
[img]http://faq.comsenz.com/attachments/2009/10/9_2009101311182039EvQ.gif[/img]
[img]http://faq.comsenz.com/attachments/2009/10/9_200910131118204PnaX.gif[/img]
������װ����
�ϴ���Ϻ󣬿�ʼ��������а�װ Discuz! 7.1 ����¼ UCenter 1.5 => Ӧ�ù��� => �����Ӧ�� => URL ��װ (�Ƽ�)��
[img]http://faq.comsenz.com/attachments/2009/10/9_200910131118205dXAu.gif[/img]
�������վ�����滻��Ӧ�ó���װ��ַ���е� ��http://domainname�����֣�Ȼ��������װ������׼����װ���棺
[img]http://faq.comsenz.com/attachments/2009/10/9_200910131118206VQ2S.gif[/img]
�Ķ���ȨЭ���������ͬ�⡱��ϵͳ���Զ���黷�����ļ�Ŀ¼Ȩ�ޣ�����ͼ��ʾ��
[img]http://faq.comsenz.com/attachments/2009/10/9_200910131118207utqY.gif[/img]
���ɹ����������һ������������������������Լ����� UCenter ���棬����ͼ��ʾ��
[img]http://faq.comsenz.com/attachments/2009/10/9_200910131118208SzPs.gif[/img]
������Զ���ȡ UCenter ��Ϣ�����������ֶ����ã�ֱ�ӵ������һ�����������ݿ���Ϣ���ý��棺
[img]http://faq.comsenz.com/attachments/2009/10/9_200910131118209DXHh.gif[/img]
��д�� Discuz! ���ݿ���Ϣ������Ա��Ϣ�󣬵������һ������ϵͳ���Զ���װ���ݿ�ֱ����ϣ�����ͼ��ʾ��
[img]http://faq.comsenz.com/attachments/2009/10/9_2009101311182010BBjW.gif[/img]
[img]http://faq.comsenz.com/attachments/2009/10/9_200910131648481KeBN.gif[/img]
�����������һ����д��ϵ��ʽ����
[img]http://faq.comsenz.com/attachments/2009/10/9_200910131648482hDqK.gif[/img]
Ϊ�˲��ϸĽ���Ʒ�����������û����飬Discuz!7.1���԰�������ͳ��ϵͳ����ͳ��ϵͳ�ռ���̳���ܵ��������ݰ���������ʹ��Ƶ�ʣ������ڲ�Ԫ�صĵ���������Щ�������������Ƿ����û�����̳�Ĳ���ϰ�ߣ���������������δ���İ汾�жԲ�Ʒ���иĽ�����Ƴ��������û�������¹��ܡ�
��ͳ��ϵͳ�����ռ�վ��������Ϣ�����ռ��û����ϣ������ڰ�ȫ���գ����Ҿ���ʵ�ʲ��Բ���Ӱ����̳������Ч�ʡ�
����װʹ�ñ��汾��ʾ��ͬ����롶��ʢ���Ƽƻ�����Discuz!��Ӫ���Ż�ͨ����վ�����ݵķ���Ϊ���ṩ��Ӫָ�����飬���ǽ���ʾ����θ���վ���������������̳���ܣ���ν��к���Ĺ������ã��Լ��ṩ������һЩ��Ӫ����ȡ�
Ϊ�˷������Ǻ�����ͨ��Ӫ���ԣ��������³��õ�������ϵ��ʽ��QQ��MSN��E-mail��
��д����ϵ��ʽ�������ύ����Ҳ���Ե��������������ֱ����ɰ�װ��
[img]http://faq.comsenz.com/attachments/2009/10/9_2009101311182011sKXl.gif[/img]
��װ��Ϻ���� Discuz! 7.1 ��ҳ�鿴��վ��
[img]http://faq.comsenz.com/attachments/2009/10/9_2009101311182012swWS.gif[/img]
���ˣ�Discuz! 7.1 �Ѿ��ɹ��ذ�װ��ϣ������Ե�¼ Discuz! 7.1 վ�㲢��ʼ�����ˡ�
EOD;

$testdatacontent[13]['subject'] = 'Discuz! 7.0��Discuz! 7.1����ͼ�Ľ̳�';
$testdatacontent[13]['message'] = <<<EOD

���̷̳�������Ϊ��ҽ����������̣�����ǰ��׼���������е�ע�������Լ���������Ҫ���е�һЩ�ƺ������[p=30, 2, left][b]һ������ǰ��׼��[/b][/p]
1������ Discuz! 7.1 �ٷ��浽���ػ��߷�������
���ص�ַ��[url=http://download.comsenz.com/Discuz/7.1/]http://download.comsenz.com/Discuz/7.1/[/url]
������Ҫ˵��һ�£������ṩ�� 4 �ֲ�ͬ�ı��롣���� GBK �������İ�(�Ƽ�)��UTF-8 �������İ桢BIG5�������İ�(�Ƽ�)��UTF-8 �������İ棬�������ʹ�õ� Discuz! 7.0 �ı��룬ѡ����Ӧ�汾�� Discuz! 7.1��̳�������ء�
������ʾ�� GBK �汾Ϊ�����ص����أ���ѹ���õ�����ͼ��ʾ�������ļ���
[img]http://faq.comsenz.com/attachments/2009/09/15_200909231743591odIN.gif[/img]
upload ���Ŀ¼����������ļ���������Ҫ�ϴ����������ϵĿ��ó����ļ���
readme Ŀ¼Ϊ��Ʒ���ܡ���Ȩ����װ��������ת���Լ��汾������־˵����
utilities Ŀ¼Ϊ��̳�������ߣ����������������Ҫ�õ��� upgrade12.php ����
2�������� Discuz! 7.1 ��ԭ���ķ�񽫲��ܼ���ʹ�ã��ʴ�������֮ǰ�Ȱѷ�񻻻�Ĭ�Ϸ�񣬲�����������񲻿���
�ù���Ա��¼��̳��̨ => ���� => �������� => ȫ�֣�����̳Ĭ�Ϸ��ѡ��Ĭ�Ϸ�񡱣���ͼ��
[img]http://faq.comsenz.com/attachments/2009/09/15_200909231743592jLXS.gif[/img]
�ù���Ա��¼��̳��̨ => ���� => ����������Ĭ�ϵ��������з������Ϊ�����ã���ͼ��
[img]http://faq.comsenz.com/attachments/2009/09/15_200909231743593YYAj.gif[/img]
3���ر���̳
�ù���Ա��¼��̳��̨ => ȫ�� => վ����Ϣ => ��̳�رգ�ѡ���ǡ�����ͼ��
[img]http://faq.comsenz.com/attachments/2009/09/15_2009092317435947YC7.gif[/img]
4����������
1����̳�����丽���ı���
�Ƽ�������̳Ŀ¼�µ�����Ŀ¼���ļ����ػ��߿�������Ҫ���ݵĵط��������û�ж���̳�����ģ�������ܴ�ĸĶ�����ôֻҪ���� attachments ������Ŀ¼���Ϳ����ˡ�
���ǳ��õĶ���̳�����丽���ı��ݷ���Ϊ����ԭ��̳��Ŀ¼���½�һ��Ŀ¼ oldbbs��Ȼ��ѳ� attachments ������������ļ�ȫ���ƶ��� oldbbs Ŀ¼�С���Ȼ����Ҳ���Խ�������̳�ļ����Ƶ��� oldbbs Ŀ¼�н��б��ݡ�
2�����ݿⱸ��
��������ֱ�ӵ� MySQL �� data Ŀ¼����һ�ݵ�ǰ Discuz! 7.0 ʹ�õ����ݿ⼴�ɣ��ǵÿ���֮ǰֹͣ MySQL ���񣬷������ɱ������ݵ��𻵡�
���������û��Ƽ�ֱ������̳��̨���б��ݣ��ù���Ա��¼��̳��̨ => ���� => ���ݿ� => ���ݣ��Ƽ����ݡ���̳ȫ�����ݡ�����ͼ��
[img]http://faq.comsenz.com/attachments/2009/09/15_200909231743595dLKT.gif[/img]
���Ҫ�Ա���������������Ҫ����Ե����ͼ�еġ�����ѡ�������Ҫ����ѡ��
[img]http://faq.comsenz.com/attachments/2009/09/15_200909231743596rAgt.gif[/img]
�������ѡ��ĺ�����Բο�����̳��е�˵����[url=http://www.discuz.net/thread-744280-1-1.html]http://www.discuz.net/thread-744280-1-1.html[/url][p=30, 2, left][b]���������е�ע����������������ϵ�������������ʾ��[/b][/p]
1�����Ȱ� FTP Ŀ¼�³��� attachments �� config.inc.php �ļ����⣬�����������ļ���Ŀ¼ȫ���ƶ���һ���½��� oldbbs Ŀ¼��
[img]http://faq.comsenz.com/attachments/2009/09/15_200909231743597EA07.gif[/img]
˵�������� Discuz! 7.0 �� config.inc.php �ļ���ԭ���������汾������ļ�û�����޸ģ�������Ϊ����ȥ�������õ��鷳��
2���ϴ� Discuz! 7.1 ��ѹ�� upload Ŀ¼�£��� attachments ��install �� config.inc.php �ļ��⣩�����г����ļ�����������
[img]http://faq.comsenz.com/attachments/2009/09/15_2009092317435981Gpy.gif[/img]
ע�����������ϴ��ļ���һ��ʹ�ö����Ʒ�ʽ�ϴ�����������ֱ���ڷ����������ؽ�ѹ�����ɡ�
���ֳ����� FTP ����Ķ�������������[url=http://faq.comsenz.com/viewnews-862?action-viewnews-itemid-373]http://faq.comsenz.com/?action-viewnews-itemid-373[/url]
3������Ŀ¼Ȩ��
�޸� config.inc.php��attachments��forumdata ���Լ� forumdata/* �� forumdata�µ������ļ��У���templates���Լ�templates/*( templates �µ������ļ���) ��Ŀ¼����Ϊ 777 ;Windows ϵͳ����ЩĿ¼ IIS �����Ķ�дȨ�ޡ�
����Ŀ¼Ȩ���޸Ŀɲο���[url=http://faq.comsenz.com/viewnews-183]http://faq.comsenz.com/viewnews-183[/url]
4���ϴ����ص� Discuz! 7.1 ��װ���� ./utilities/upgrade12.php ����̳�����Ŀ¼��
[img]http://faq.comsenz.com/attachments/2009/09/15_200909231743599hYd1.gif[/img]
5��������������� [url]http://www.domain.com/upgrade12.php[/url] ��������������������� [url]http://www.domain.com[/url] Ϊ�����̳���ʵ�ַ��
[img]http://faq.comsenz.com/attachments/2009/09/15_2009092317435910Go9J.gif[/img]
��ͼ�����е����>> �������ȷ���������Ĳ���,����������� �����ӿ�ʼ�����������������Զ���ת�ģ������˹���Ԥ��
[img]http://faq.comsenz.com/attachments/2009/09/15_2009092317435911gptA.gif[/img]
��������ͳ��ϵͳ˵����
[img]http://faq.comsenz.com/attachments/2009/09/15_2009092317435912w82l.gif[/img]
Ϊ�˷������Ǻ�����ͨ��Ӫ���ԣ����������³��õ�������ϵ��ʽ��Ҳ���Ե�������������衱��
��Ҫ�������ã�
[img]http://faq.comsenz.com/attachments/2009/09/15_2009092317435913RVPg.gif[/img]
��Ҫ����������������վ����һЩ�Ƚ���Ҫ�Ĺ��ܽ����������ã������������Կ����ù��ܵĽ��ܼ�������
�����Ե�������ھͽ����̨���á������������øù��ܣ�Ҳ���Ե����������һ�����ܡ����������������ù��ܵ����ã��Ժ����á�
������ɣ�
[img]http://faq.comsenz.com/attachments/2009/09/15_2009092317435914F9iF.gif[/img][p=30, 2, left][b]�����������һЩ�ƺ����[/b][/p]
1��ɾ���������ϵ��������� upgrade12.php ��
ע�⣺����������Զ���ɾ���������������һЩԭ��û�б��Զ�ɾ�����ֶ�ɾ����
2��ʹ�ù���Ա��ݵ�¼��̳�������̨ => ���� => ���»��档
[img]http://faq.comsenz.com/attachments/2009/09/15_2009092317435915oM3A.gif[/img]
3������̳����ע�ᡢ��¼�������ȳ�����ԣ����������Ƿ�������
4�������Ҫ��ǰ��ͼƬ���ļ������Ե� oldbbs �ļ������ң���Ȼ���ȷ������Ҫ�˻� Discuz!7.1 û�����˿��԰� oldbbs ɾ������
���ˣ� Discuz! 7.0 �� Discuz! 7.1 �����ɹ���
[img]http://faq.comsenz.com/attachments/2009/09/15_200909240938541sLpw.gif[/img]
EOD;
?>