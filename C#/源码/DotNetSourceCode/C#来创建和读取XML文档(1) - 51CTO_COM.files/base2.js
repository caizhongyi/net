/**
modify by cao2xi 2008-11-24
�޸������ύ��ʽ
commentSubmit �����޸ģ�
1.form uid ���ݡ�
2.checkform ��Ϣ�ύ����֤��ֻ��֤��֤�룬����֤�û����롣
3.ajax_sendfb ��Ϣ�ύ���ύ�û�����uid�������ύ�û����롣
*/
function InitAjax()
{
  var ajax=false; 
  try { 
    ajax = new ActiveXObject("Msxml2.XMLHTTP.3.0"); 
  } catch (e) { 
    try { 
      ajax = new ActiveXObject("Microsoft.XMLHTTP.3.0"); 
    } catch (E) { 
      ajax = false; 
    } 
  }
  if (!ajax && typeof XMLHttpRequest!='undefined') { 
    ajax = new XMLHttpRequest(); 
  } 
  return ajax;
} 
String.prototype.trim = function(){return this.replace(/(^[ |��]*)|([ |��]*$)/g, "");}
function $(s){return document.getElementById(s);}
function $$(s){return document.frames?document.frames[s]:$(s).contentWindow;}
function $c(s){return document.createElement(s);}
function initSendTime(){
	SENDTIME = new Date();
}
var err=0;
function commentSubmit(theform){
	
	var smsg =theform.msg.value;
	var suid = theform.uid.value;
	var susername = theform.username.value;
	var snouser = theform.nouser.checked;
	var sauthnum = theform.authnum.value;
	var sartID = theform.artID.value;

	
	
	var sDialog = new dialog();
	sDialog.init();
	if(smsg == ''){
		sDialog.event('��������������!','');
		sDialog.button('dialogOk','void 0');
		$('dialogOk').focus();
		return false;
	}
	if( susername == '' && snouser == false){
		sDialog.event('������½��ѡ����������!','');
		sDialog.button('dialogOk','void 0');
		$('dialogOk').focus();
		return false;
	}	
	if(sauthnum == ''){
		sDialog.event('��������֤��!','');
		sDialog.button('dialogOk','void 0');
		$('dialogOk').focus();
		return false;
	}
	if(sartID == ''){
		sDialog.event('�Ƿ��ύ,�����#001','');
		sDialog.button('dialogOk','void 0');
		$('dialogOk').focus();
		return false;
	}

	
var url = "/php/checkform.php?authnum="+sauthnum;

var ajax = InitAjax();
ajax.open("GET", url, false);
ajax.send();
err=ajax.responseText;
if(err == 0){
	var ajax = InitAjax();
	ajax.open("GET", url, false);
	ajax.send();
	err=ajax.responseText;
}

if(err == 2){
	sDialog.event('�Ƿ��ύ,�����#002','');
	sDialog.button('dialogOk','void 0');
	$('dialogOk').focus();
	return false;	
}
if(err == 1){
	sDialog.event('��֤���������!','');
	sDialog.button('dialogOk','void 0');
	$('dialogOk').focus();
	return false;
}

var url = "/php/ajax_sendfb.php?artID="+sartID+"&nouser="+snouser+"&authnum="+sauthnum+"&username="+susername+"&uid="+suid+"&mesg="+smsg;
var ajax = InitAjax();
ajax.open("GET", url, false);
ajax.send();
ajax.responseText;

	getcommentend(thistid);
	refimg();
	return false;
}
function getcommentend(tid){
	var url = "/php/artcomment2.php?artid="+tid;
	var ajax = InitAjax();
	ajax.open("GET", url, true);
	ajax.onreadystatechange = function() {
		if (ajax.readyState == 4 && ajax.status == 200) {
			document.getElementById('artcomments').innerHTML = ajax.responseText;
		}
	}
	ajax.send(null);
}