//Copyright 2005-2006 (C) Emar Software, Ltd. All Rights Reserved.
//This software is the proprietary information of Emar, Software, Ltd.

//���ļ��ṩWEBӦ�ó����г��õ�javascript����

//У�����пؼ������ֵ�Ƿ�Ϸ�

//Ҫ����Ŀؼ����뺬��alt���磺
//<input type="text" name="int" alt="����|0|i|4|10|30">
function isChinese(c) {
	return 128 < c.substring(0,1).charCodeAt(0);
}

function getStrRealLength(str) {
	if(str == null || str.length == 0) {
		return 0;
	}
	var realLength = 0;
	var c;
	for(var i = 0; i < str.length; i++) {
		if( i == str.length -1) {
			c = str.substring(i);
		} else {
			c = str.substring(i, i+1);
		}
		if(isChinese(c)) {
				realLength += 2;
		} else {
			realLength++;
		}
	}
	return realLength;
}
function checkAll(p_frm) {
	var eCount = p_frm.elements.length;
	
	var v="";
	
	var discript = "";//����ֵ����

	var nullflag = "1";//�Ƿ��Ϊ�գ�0�����ɣ�1����
	
	var v_type = "s";//����ֵ���ͣ�s���ַ�����i��������f����������d�����ڣ�e���ʼ���c�����֤��p���ʱ�  m-MoneY

	var length = "";//����ֵ����

	var mins = "";//��С����ֵ

	var maxs = "";//�������ֵ
	
//	for(var ii = 0; ii < eCount; ii++) {
//		var e = p_frm.elements[ii];
//		if (e.alt == null || e.alt == "")
//			continue;
//		if(document.getElementById(e.id+"Error")){
//			document.getElementById(e.id+"Error").innerHTML ="";
//		}
//	}
	
	for(var i = 0; i < eCount; i++) {

		discript = "";
		v_type = "s";
		length = "";
		nullflag = "1";
		maxs = "";
		mins = "";


		var e=p_frm.elements[i];
		if (e.alt == null || e.alt == "")
			continue;
		parseTag(e.alt);
		if (( v = getNextValue()) != null) {
			discript = v;
		}
		if ((v = getNextValue()) != null) {
			nullflag = v;
		}
		if ((v = getNextValue()) != null) {
			v_type = v;
		}
		if ((v = getNextValue()) != null) {
			length = v;
		}
		if ((v = getNextValue()) != null) {
			mins = eval(v);
		}
		if ((v = getNextValue()) != null) {
			maxs = eval(v);
		}
			
		if (checkValidate(p_frm,e, discript, v_type, length, nullflag, maxs, mins) == "0")
			return false;
	}
	return true;
}

/**
* У���û�ָ���Ķ����Ƿ�Ϸ�
* @param element        �û���ҪУ��Ķ���
* @param discript       ���������
* @param type           ���������:
*                       i  ����
*                       f  ����           
*                       s  �ַ�
*                       d  ����
*                       e  �ʼ�
*                       c  ���֤
*                       p  �ʱ�

* @param length         �������󳤶�

* @param type           �ܷ�Ϊ��ֵ
*                       1  ����Ϊ��
*                       0  ����Ϊ��
* @param maxs           ���ֵ

* @param mins           ��Сֵ

* @return               ȫ�������Ϸ�����true�����򷵻�false
*/
var elementId;
function checkValidate(p_frm, element, discript, type, length, nullflag, maxs, mins) {
	
	elementId = element.id+"Error";
    if (element == "") {
        window.alert("�������ó���,������ؼ�!");
        return(0);
    }
    
    if (discript == "") {
        window.alert("�������ó���,������ؼ�����!");
        return(0);
    }
    
    if (nullflag == 0) {
        if (element.value.TrimAll() == "") {
    	    //window.alert("������" + discript + "!");
    	    document.getElementById(elementId).innerHTML="<font color=\"red\">������\��"+discript + "\�� !��</font>";
    	    element.focus();
    	    return(0); 
    	}
    }
    

	    
    if (type != "") {
        switch (type) {	
    		case "i"://����
    			{
    				if (element.value.length != 0 && isInteger(element.value) != true) {
			   			document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ֻ����������!��</font>";
//    					window.alert(discript + "ֻ����������!");
    					element.focus();
    					return(0);		   
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
						
    				}
    				break;  
    			}
    
    		case "f"://������
    			{
    				if (element.value.length != 0 && isNaN(element.value) == true) {
    					document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ֻ����������!��</font>";
    			        //window.alert(discript + "ֻ����������!");
    					element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
						
    				}
    				break;
    			}
    		
    		
    		case "m"://Money
    			{
    			      tempLen = element.value.indexOf(".");
    			     // window.alert(discript + "   "+tempLen);
    				  strLen = element.value.substring(tempLen+1,element.value.length);
    				  // window.alert(discript + "   "+strLen);
    				if (element.value.length != 0 && isNaN(element.value) == true) {
    					document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ֻ����������!��</font>";
    			      //  window.alert(discript + "ֻ����������!");
    					element.focus();
    					return(0);
    				}else if(strLen.length>2 && tempLen != -1){
						//  window.alert(discript + "  ���ݲ�����Ҫ��,����!");
    				      document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ���ݲ�����Ҫ��,����!��</font>";
    				      element.focus();
    					  return(0);
    				}else{
    				   document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
    				}
    				break;
    			}
    		
    		
    		
    			
    		case "ccc"://ֻ�����뺺��
    			{
    			    if (element.value.length != 0 && element.value.search(/[^\u4E00-\u9FA5]/g) != -1) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ֻ�����뺺��!��</font>";
    			        //window.alert(discript + "ֻ�����뺺��!");
    					element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
						
    				}
    				break;
    			}
    			
    	    case "cc"://ֻ�����뺺������հ��ַ��»���
    			{
    			    if (element.value.length != 0 && element.value.search(/[^\u4E00-\u9FA5_\s]/g) != -1) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ֻ�����뺺��!��</font>";
    			        //window.alert(discript + "ֻ�����뺺��!");
    					element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
						
    				}
    				break;
    			}
    			
    		case "l"://ֻ��������ĸ����հ��ַ��»���
    			{
    			    if (element.value.length != 0 && element.value.search(/[^a-zA-Z_\s]/g) != -1) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ֻ��������ĸ!��</font>";
    			        //window.alert(discript + "ֻ��������ĸ!");
    					element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
						
    				}
    				break;
    			}
    			
    		case "li"://ֻ��������ĸ����������հ��ַ��»���
    			{
    			    if (element.value.length != 0 && isInteger(element.value.charAt(0))) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ���ַ�����������!��</font>";
    			        //window.alert(discript + "���ַ�����������!");
    					element.focus();
    					return(0);
    			    } else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
						
    				}
    				
    			    if (element.value.length != 0 && element.value.search(/[^a-zA-Z_0-9\s]/g) != -1) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ֻ��������ĸ������!��</font>";
    			        //window.alert(discript + "ֻ��������ĸ�����֣�");
    					element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
						
    				}
    				break;
    			}
    			
    		case "pwd":
    			{
				    if (element.value.length != 0 && element.value.search(/[^a-zA-Z0-9]/g) != -1) {
				    	document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ֻ��������ĸ������!��</font>";
    			        //window.alert(discript + "ֻ��������ĸ�����֣�");
    					element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
						
    				}
    				break;
    			}	
    			
    		case "ali":{
    			if (element.value.TrimAll().length!= 0 && element.value.TrimAll().search(/[^a-zA-Z0-9-_]/g) != -1) {
    				document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ֻ��������ĸ������!��</font>";
   			        //window.alert(discript + "ֻ��������ĸ�����֡��»��ߺ��к���");
   					element.focus();
   					return(0);
   				} else {
					document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
					
    			}
   				break;
    		}	
   			
    		case "cccli"://ֻ��������ĸ,��������հ��ַ��»���
    			{
    			    for (var i = 0;i<element.value.length;i++){
				    	if (element.value.charAt(i) == ' '){
				    		document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ���ܴ��ո��!��</font>";
	    			        //window.alert(discript + "���ܴ��ո��");
	    					element.focus();
	    					return(0);
    					} else {
							document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
							
    					}
					}
    			    if (element.value.length != 0 && isInteger(element.value.charAt(0))) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ���ַ�����������!��</font>";
    			        //window.alert(discript + "���ַ�����������!");
    					element.focus();
    					return(0);
    			    } else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
						
    				}
    				
    			    if (element.value.length != 0 && element.value.search(/[^a-zA-Z_0-9]/g) != -1) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ֻ��������ĸ�����֣������»���!��</font>";
    			        //window.alert(discript + "ֻ��������ĸ�����֣������»���!");
    					element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
						
    				}
    				break;
    			}
    			
    		case "ccli"://ֻ�����뺺��,��ĸ,��������հ��ַ��»���
    			{
    			    if (element.value.length != 0 && isInteger(element.value.charAt(0))) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ���ַ�����������!��</font>";
    			        //window.alert(discript + "���ַ�����������!");
    					element.focus();
    					return(0);
    			    } else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
						
    				}
    				
    			    if (element.value.TrimAll().length != 0 && element.value.TrimAll().search(/[^a-zA-Z_0-9\u4E00-\u9FA5\s]/g) != -1) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ֻ�����뺺�֡���ĸ������!��</font>";
    			        //window.alert(discript + "ֻ�����뺺�֡���ĸ������!");
    					element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
						
    				}
    				break;
    			}
    			
			case "cli"://ֻ�����뺺��,��ĸ,����
    			{
    			    if (element.value.TrimAll().length != 0 && element.value.TrimAll().search(/[^a-zA-Z0-9\u4E00-\u9FA5]/g) != -1) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ֻ�����뺺�֡���ĸ������!��</font>";
    			        //window.alert(discript + "ֻ�����뺺�֡���ĸ������!");
    					element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
						
    				}
    				break;
    			}
    			
    		case "cliai"://ֻ�����뺺��,��ĸ,����,"-"
    			{
    			    if (element.value.TrimAll().length != 0 && element.value.TrimAll().search(/[^a-zA-Z0-9-\u4E00-\u9FA5\��]/g) != -1) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ֻ�����뺺�֡�-����ĸ������!��</font>";
    			        //window.alert(discript + "ֻ�����뺺�֡���ĸ������!");
    					element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
    				}
    				break;
    			}	
    		case "ip"://ip��ַУ��
    			{
    			    if (element.value.length != 0 && isIP(element.value) != true) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">��������Ч�� ��"+discript + "����</font>";
    			        //window.alert("��������Ч��" + discript + "!");
    			        element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
						
    				}
    				break;
    			}
    			
    		case "s"://�ַ���
    			{
    				if(element.value.TrimAll()=='' && '0'==nullflag){
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ����Ϊ��!��</font>";
	   					//window.alert(discript + "����Ϊ��!");
	   					element.focus();
	   					return(0);		   
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
						//
    				}
    				break;
    			}
    			
    		case "d"://����
    			{
    				if (element.value.length != 0 && isDate(element.value) == false) {
    					document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ����������Ч����!��</font>";
    			        //window.alert(discript + "����������Ч����!");
    					element.focus();
    					return(0);		   
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
						
    				}
    			    break;
    			}
    			
    	    case "e"://�ʼ���ַ
    			{
    				if (element.value.length != 0 && isEmail(element.value) == false) {
    					document.getElementById(elementId).innerHTML="<font color=\"red\">��������Ч�� ��"+discript + "����</font>";
    			        //window.alert("��������Ч��" + discript + "!");
    					element.focus();
    					return(0);		   
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
						
    				}
    			    break;
    			}
    			
    		case "c"://���֤
    			{
    				if (element.value.length != 0 && isIdCard(element.value) == false) {
    					document.getElementById(elementId).innerHTML="<font color=\"red\">��������Ч�� ��"+discript + "����</font>";
    			        //window.alert("��������Ч��" + discript + "!");
    					element.focus();
    					return(0);		   
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
						
    				}
    			    break;
    			}
    			
    		case "p"://�ʱ�
    			{
    				if (element.value.length != 0 && isPostCode(element.value) == false) {
    					document.getElementById(elementId).innerHTML="<font color=\"red\">��������Ч�� ��"+discript + "����</font>";
    			        //window.alert("��������Ч��" + discript + "!");
    					element.focus();
    					return(0);		   
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
						
    				}
    			    break;
    			}
    		case "h":
    			{
    				if (element.value.length != 0 && validateURL(element.value) == false) {
    					document.getElementById(elementId).innerHTML="<font color=\"red\">������Ϸ��� ��"+discript + "����</font>";
    			        //window.alert("������Ϸ�����ҳ��ַ��");
    					element.focus();
    					return(0);		   
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
						
    				}
    			    break;
    			}	

    	}
    }
    
    if (length != "" && type != "d") {
		var elementId = element.id +"Error";
    	if(type=="i"){
    		if (lengthCheck(element.value, length)) {
//	    	    window.alert(discript + "�ĳ��ȱ���С��" + length + "�����֣�");
    			document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� �ĳ��ȱ���С��" + length + "�����֣���</font>";
	    	    element.focus();
	    	    return(0);
	    	}
    	}else if(type=="m"){
    		if (lengthCheck(element.value, length)) {
//	    	    window.alert(discript + "�ĳ��ȱ���С��" + length + "�����֣�");
    			document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� �ĳ��ȱ���С��" + length + "λ����</font>";
	    	    element.focus();
	    	    return(0);
	    	}
    	}
    	else if (lengthCheck(element.value, length)) {
    			//alert(element.id);
   			document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� �ĳ��ȱ���С��" + length/2 + "�����֣�" + length + "���ַ�����</font>";
//    	    window.alert(discript + "�ĳ��ȱ���С��" + length/2 + "�����֣�" + length + "���ַ���");
    	    element.focus();
    	    return(0);
    	}
  		
		document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";

    }
    
    if (maxs != "" && mins != "") {
        if (element.value < mins || element.value > maxs) {
        	document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ��ֵ����С��" + maxs + ", ����" + mins + "!��</font>";
            //window.alert(discript + "��ֵ����С��" + maxs + ", ����" + mins + "!");
    	    element.focus();
    	    return(0);
        } else {
			document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
			
		}
    } else {
        if (maxs != "") {
    	    if (element.value > maxs) {
    	    	document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ��ֵ����С��" + maxs + "!��</font>";
    	        window.alert(discript + "��ֵ����С��" + maxs + "!");
    		    element.focus();
    		    return(0);
    	    } else {
				document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
				
			}
        }
        if (mins != "") {
    	    if (element.value < mins) {
    	    	document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript + "�� ��ֵ�������" + mins + "!��</font>";
    	        //window.alert(discript + "��ֵ�������" + mins + "!");
    		    element.focus();
    		    return(0);
    	    } else {
				document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
				
			}
        }
    }
    
    return(1);
}

//�ж��Ƿ�����
function isInteger(integer) {
    if(integer.search(/[^0-9]/g) != -1) return false;
    var count;
    var numchar;
    var numvalue;	
    for (count = 0; count < integer.length; count++) {
    	numchar = integer.charAt(count);
    	numvalue = numchar - '0';
    	if (!(numvalue >= 0 && numvalue <= 9))
    		return false;
    }
    return true;
}

//�ж��Ƿ���Ч�ʼ�
function isEmail(mail) {
    if (killSpace(mail) == "") return false;
    
	var v_email = mail.substring(mail.indexOf("@") + 1)
	if ( (mail.indexOf("@") == -1) ||
              (mail.indexOf("@") == 0)  ||
               (mail.indexOf("@") != mail.lastIndexOf("@")) ||
                 (v_email.indexOf(".") == -1)  ||
	                   (v_email.indexOf(".") == 0) ||
                     ((mail.indexOf(".") + 1) == mail.length) ) {
		return false;
	}	
	return true;    
}

//�ж��Ƿ�Ϊ���֤
function isIdCard(idCard) {
    var v_idCard = killSpace(idCard);
    if (v_idCard == "") return false;
    
    if (v_idCard.length != 15 && v_idCard.length != 18) {
    	document.getElementById(elementId).innerHTML="<font color=\"red\">�����֤�� ����Ϊ15λ��18λ!��</font>";
        //alert("���֤����Ϊ15λ��18λ��");
        return false;
    } else {
		document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
		return true;
	}
    
    if (v_idCard.search(/[^0-9a-zA-Z]/g) != -1) {
    	document.getElementById(elementId).innerHTML="<font color=\"red\">�����֤�� ����ȫ��Ϊ��ĸ�����֣����ܰ��������ַ�!��</font>";
        //alert("���֤����ȫ��Ϊ��ĸ�����֣����ܰ��������ַ�!");
        return false;
    } else {
    	document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
        return true;
    }
}

//�ж��Ƿ�Ϊ�ʱ�

function isPostCode(postCode) {
    var v_postCode = killSpace(postCode);
    if (v_postCode == "") return false;
        
    if (v_postCode.length != 6) {
		document.getElementById(elementId).innerHTML="<font color=\"red\">���������롱 ����Ϊ6λ����!��</font>";
        //alert("�����������Ϊ6λ���֣�");
        return false;
    } else {
    	document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
        return true;
    }
    
    if (!isInteger(v_postCode)) {
    	document.getElementById(elementId).innerHTML="<font color=\"red\">���������롱 ����ȫ��Ϊ����!��</font>";
        //alert("�����������ȫ��Ϊ���֣�");
        return false;
    }else {
    	document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
        return true;
    }
}

function validateURL(url){  
	var flag;
	var emailRegS=/^http([s])?[:][/][/][A-Za-z0-9 -]+\.[A-Za-z0-9]+[\/=\?%\-&_~@[\]:+!]*([^<>\'\'\"\"])*$/;
	if (!emailRegS.exec(url)) return false ;
	return true  ;
}

/**
* ���ô˺���У�����ڸ�ʽ

* @param s         ��������string s,����2001-01-01
* @return          �����������û�а����Ƿ��ַ����Լ������ںϷ����򷵻�true�����򷵻�false
*/
function isDate(s) {
    if (s.length != 10 || s.charAt(4) != '-' || s.charAt(7) != '-')
    	return false;
    if (!isInteger(s.substring(0, 4)) || !isInteger(s.substring(5, 7)) || !isInteger(s.substring(8, 10)) || eval(s.substring(0, 4)) < 1 || eval(s.substring(5, 7)) < 1 || eval(s.substring(8, 10)) < 1)
    	return false;
    if (checkDate(s.substring(0, 4), s.substring(5, 7), s.substring(8, 10)) == false)
    	return false;		
    return true;
}

//У�������Ƿ�Ϸ�
function checkDate(year, month, day) {
    var iyear;
    var imonth;
    var iday;
    if(year.length != 4 || month.length != 2 || day.length != 2)  
    	return false;
    if (!isInteger(year) || !isInteger(month) || !isInteger(day))
    	return false;
    iyear = getValueOfInt(year);
    imonth = getValueOfInt(month);
    iday = getValueOfInt(day);
    if (imonth < 1 || imonth > 12) return false;
    switch(imonth) {
    	case 1:
    	case 3:
    	case 5:
    	case 7:
    	case 8:
    	case 10:
    	case 12:
    		if (iday > 31) return false;
    			break;
    	case 4:
    	case 6:
    	case 9:
    	case 11:
    		if (iday > 30) return false;
    		break;
    	default:
    		if(mod(iyear, 4) == 0 && (mod(iyear, 100) != 0 || mod(iyear, 400) == 0)) {//�ж��Ƿ�����
    			if (iday > 29) return false;
    		}else {
    			if (iday > 28) return false;
    		}
    }
	return true;
}

//ȡ�������ַ��������������ֵ

function getValueOfInt(string) {
    var count;
    var numchar;
    var numvalue;
    var value;
    value = 0;
    for ( count = 0; count < string.length; count++) {
    	numchar = string.charAt(count);
    	numvalue = numchar - '0';
    	value = value * 10 + numvalue;
    }
    return value;
}

var e_tag; //��ǰ���ڷֲ��altֵ


//��ʼ�����ֲ�alt
function parseTag(p_tag) {
	e_tag = p_tag;
}

//�õ���һ���ֲ���Ĵ�
function getNextValue() {
	if(e_tag == null || e_tag == "")
		return null;
	var p = e_tag.indexOf("|");
	if(p == -1)
		p = e_tag.length;
	var r = e_tag.substring(0,p);
	e_tag = e_tag.substring(p + 1, e_tag.length);
	return r;
}

//ɾ���ո�����checkValidate����
function killSpace(x) {
    while ((x.length > 0) && (x.charAt(0) == ' '))
    	x = x.substring(1, x.length)
    while ((x.length > 0) && (x.charAt(x.length - 1) == ' '))
    	x = x.substring(0, x.length - 1)
    return x;

}

//�������ĳ���
function count_char(str) {
	var len = 0;
	for(i = 0; i < str.length; i++) {
		var ech = escape(str.charAt(i));
		if ( ech.length > 4 ){
//			len++;

//// �޸���������֣�len + 1 ��ʾ2���ַ�����һ�������֣�len + 5 ��ʾ6���ַ�����һ��������
			len = len + 1;
/*
			if (ech>"%u07ff")
				len++;
*/			
		}
		len ++;
	}
	return len;
}

//У������������Ƿ񳬹�ָ���ĳ���
function lengthCheck(text, size) {
	var len = count_char(text);
	if ( len > size ) {
        return true;
	}
	return false;
}


function mod(var1,var2) {
    return (var1%var2);
}

//�ж��Ƿ�IP����������ʽ

//^[0-9]([0-9]{0,2}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3})$
function isIP(ip) {
    if (ip.search(/^[0-9]([0-9]{0,2}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3})$/g) == -1) {
        return false;
    }
    var temp;
    var ip1 = ip.substring(0,ip.indexOf("."));
    temp = ip.substring(ip.indexOf(".")+1);
    var ip2 = temp.substring(0,temp.indexOf("."));
    temp = temp.substring(temp.indexOf(".")+1);
    var ip3 = temp.substring(0,temp.indexOf("."));
    temp = temp.substring(temp.indexOf(".")+1);
    var ip4 = temp;

    if (parseInt(ip1)>=256 ||
        parseInt(ip2)>=256 ||
        parseInt(ip3)>=256 ||
        parseInt(ip4)>=256) {
        return false;
    }
    return true;
}

function mask(form,obj){
    var objName = obj.name.substr(0,obj.name.indexOf("_"));
	obj.value=obj.value.replace(/[^\d]/g,'')
	key1=event.keyCode
	if (key1==37 || key1==39)
	{
		obj.blur();
		nextip=parseInt(obj.name.substr(obj.name.indexOf("_")+1,1));
		nextip=key1==37?nextip-1:nextip+1;
		nextip=nextip>=5?1:nextip;
		nextip=nextip<=0?4:nextip;
		eval(form.name+"."+objName+"_"+nextip+".focus()");
	}
	if(obj.value.length>=3){
		if(parseInt(obj.value)>=256 || parseInt(obj.value)<=0)
		{
			alert(parseInt(obj.value)+"��ַ����!")
			obj.value="";
			obj.focus();
			return false;
		}
		else
		{ obj.blur();
			nextip=parseInt(obj.name.substr(obj.name.indexOf("_")+1,1))+1;
			nextip=nextip>=5?1:nextip;
			nextip=nextip<=0?4:nextip;
			eval(form.name+"."+objName+"_"+nextip+".focus()");
		}
	}
}

function mask_c(form,obj)
{
	clipboardData.setData('text',clipboardData.getData('text').replace(/[^\d]/g,''));
}

function notspecial(element,discript) {//ֻ�����뺺��,��ĸ,��������հ��ַ��»�,��Ҫ�����б���Ĳ�ѯ
   if (null != element && element.value != "") {
    	if (element.value.length != 0 && element.value.search(/[^a-zA-Z_0-9\u4E00-\u9FA5\s]/g) != -1) {
			document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript+"�� ֻ�����뺺�֡���ĸ������!��</font>";
    		//window.alert(discript + "ֻ�����뺺�֡���ĸ������!");
    		element.focus();
    		return false;
//    	} else {
//	    	document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
//	        return true;
        }
   }
   return true;
}
function notspecialNotSpace(element,discript) {//ֻ�����뺺��,��ĸ,��������հ��ַ��»�,��Ҫ�����б���Ĳ�ѯ
   if (null != element && element.value != "") {
    	if (element.value.length != 0 && element.value.search(/[^a-zA-Z_0-9\u4E00-\u9FA5]/g) != -1) {
			document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript+"�� ֻ�����뺺�֡���ĸ������!��</font>";
    		//window.alert(discript + "ֻ�����뺺�֡���ĸ������!");
    		element.focus();
    		return false;
//    	} else {
//	    	document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> ��</font>";
//	        return true;
        }
   }
   return true;
}
function notSpace(element,discript) {
   if (null != element && element.value != "") {
	var elementValue = killSpace(element.value);
	if (elementValue != "" ) {
		elementValue.replace("'","");
		if (elementValue.indexOf("'") != -1) {
			document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript+"�� ���ܰ�����'����</font>";
			//window.alert(discript + "���ܰ���'");
    		element.focus();
    		return false;
		} else if (elementValue.indexOf("��") != -1) {
			//window.alert(discript + "���ܰ���ȫ�ǿո�");
			document.getElementById(elementId).innerHTML="<font color=\"red\">��"+discript+"�� ���ܰ���ȫ�ǿո��</font>";
    		element.focus();
    		return false;
		} else {
			element.value=elementValue;
		}
		
	} else {
		document.getElementById(elementId).innerHTML="<font color=\"red\">������ ��"+discript+"��</font>";
//		window.alert("������" + discript);
		element.focus();
		return false;
	}
} else {
	document.getElementById(elementId).innerHTML="<font color=\"red\">������ ��"+discript+"��</font>";
	//window.alert("������" + discript);
	element.focus();
	return false;
}
return true;
}
//****����ReplaceAll()����
function  stringReplaceAll(AFindText,ARepText){
  raRegExp = new RegExp(AFindText,"g");
  return this.replace(raRegExp,ARepText)
}
//************�����������еĿո���س��滻************
//�磺
//var s= "s  dd ss " +
//		  d "	;
//����s&nbsp;dd&nbsp;<br/>&nbsp;
function replaceSpaceAndBr(e) {
	String.prototype.ReplaceAll = stringReplaceAll;
	var eValue = e.value;
	String.prototype.ReplaceAll = stringReplaceAll;
	eValue = eValue.ReplaceAll("\r\n","<br/>");
	eValue = eValue.ReplaceAll(" ","&nbsp;");
	return eValue;
}
//************��֤�滻�ո���س�����ı��Ƿ񳬹�ָ���ĳ���************
function checkLength(e,length) {
	var eValue = replaceSpaceAndBr(e);
	if (lengthCheck(eValue, length)) {
		e.focus();
		//alert("��ϸ���ܵĳ��ȱ���С��"+length/2+"�����֣�"+length+" ���ַ�����");
		return false;
	} else {
		return true;
	}
}

//************�����������еĿո�ɾ��************
//�磺
//var s= "s  dd ss " +;
//����"sddss";
function replaceSpace(e) {
	String.prototype.ReplaceAll = stringReplaceAll;
	var eValue = e.value;
	String.prototype.ReplaceAll = stringReplaceAll;
	eValue = eValue.ReplaceAll(" ","");
	eValue = eValue.ReplaceAll("��","");
	return eValue;
}

//ȥǰ��ո�
String.prototype.Trim = function()
{
    return this.replace(/(^\s*)|(\s*$)/g, "");
}
String.prototype.TrimAll = function()
{
   return this.replace(/(\s*|\r\n*)/ig,"");
}
  
