//Copyright 2005-2006 (C) Emar Software, Ltd. All Rights Reserved.
//This software is the proprietary information of Emar, Software, Ltd.

//此文件提供WEB应用程序中常用的javascript方法

//校验所有控件输入的值是否合法

//要检验的控件必须含有alt，如：
//<input type="text" name="int" alt="整数|0|i|4|10|30">
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
	
	var discript = "";//输入值名称

	var nullflag = "1";//是否可为空，0－不可，1－可
	
	var v_type = "s";//输入值类型，s－字符串，i－整数，f－浮点数，d－日期，e－邮件，c－身份证，p－邮编  m-MoneY

	var length = "";//输入值长度

	var mins = "";//最小输入值

	var maxs = "";//最大输入值
	
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
* 校验用户指定的对象是否合法
* @param element        用户需要校验的对象
* @param discript       对象的描述
* @param type           对象的类型:
*                       i  整数
*                       f  浮点           
*                       s  字符
*                       d  日期
*                       e  邮件
*                       c  身份证
*                       p  邮编

* @param length         对象的最大长度

* @param type           能否为空值
*                       1  可以为空
*                       0  不能为空
* @param maxs           最大值

* @param mins           最小值

* @return               全部条件合法返回true；否则返回false
*/
var elementId;
function checkValidate(p_frm, element, discript, type, length, nullflag, maxs, mins) {
	
	elementId = element.id+"Error";
    if (element == "") {
        window.alert("函数调用出错,请输入控件!");
        return(0);
    }
    
    if (discript == "") {
        window.alert("函数调用出错,请输入控件描述!");
        return(0);
    }
    
    if (nullflag == 0) {
        if (element.value.TrimAll() == "") {
    	    //window.alert("请输入" + discript + "!");
    	    document.getElementById(elementId).innerHTML="<font color=\"red\">请输入\“"+discript + "\” !×</font>";
    	    element.focus();
    	    return(0); 
    	}
    }
    

	    
    if (type != "") {
        switch (type) {	
    		case "i"://整数
    			{
    				if (element.value.length != 0 && isInteger(element.value) != true) {
			   			document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "“ 只能输入整数!×</font>";
//    					window.alert(discript + "只能输入整数!");
    					element.focus();
    					return(0);		   
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
						
    				}
    				break;  
    			}
    
    		case "f"://浮点数
    			{
    				if (element.value.length != 0 && isNaN(element.value) == true) {
    					document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "“ 只能输入数字!×</font>";
    			        //window.alert(discript + "只能输入数字!");
    					element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
						
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
    					document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "“ 只能输入数字!×</font>";
    			      //  window.alert(discript + "只能输入数字!");
    					element.focus();
    					return(0);
    				}else if(strLen.length>2 && tempLen != -1){
						//  window.alert(discript + "  数据不符合要求,请检查!");
    				      document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "“ 数据不符合要求,请检查!×</font>";
    				      element.focus();
    					  return(0);
    				}else{
    				   document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
    				}
    				break;
    			}
    		
    		
    		
    			
    		case "ccc"://只能输入汉字
    			{
    			    if (element.value.length != 0 && element.value.search(/[^\u4E00-\u9FA5]/g) != -1) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "“ 只能输入汉字!×</font>";
    			        //window.alert(discript + "只能输入汉字!");
    					element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
						
    				}
    				break;
    			}
    			
    	    case "cc"://只能输入汉字允许空白字符下划线
    			{
    			    if (element.value.length != 0 && element.value.search(/[^\u4E00-\u9FA5_\s]/g) != -1) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "“ 只能输入汉字!×</font>";
    			        //window.alert(discript + "只能输入汉字!");
    					element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
						
    				}
    				break;
    			}
    			
    		case "l"://只能输入字母允许空白字符下划线
    			{
    			    if (element.value.length != 0 && element.value.search(/[^a-zA-Z_\s]/g) != -1) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "“ 只能输入字母!×</font>";
    			        //window.alert(discript + "只能输入字母!");
    					element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
						
    				}
    				break;
    			}
    			
    		case "li"://只能输入字母和数字允许空白字符下划线
    			{
    			    if (element.value.length != 0 && isInteger(element.value.charAt(0))) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "“ 首字符不能是数字!×</font>";
    			        //window.alert(discript + "首字符不能是数字!");
    					element.focus();
    					return(0);
    			    } else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
						
    				}
    				
    			    if (element.value.length != 0 && element.value.search(/[^a-zA-Z_0-9\s]/g) != -1) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "“ 只能输入字母和数字!×</font>";
    			        //window.alert(discript + "只能输入字母和数字！");
    					element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
						
    				}
    				break;
    			}
    			
    		case "pwd":
    			{
				    if (element.value.length != 0 && element.value.search(/[^a-zA-Z0-9]/g) != -1) {
				    	document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "“ 只能输入字母和数字!×</font>";
    			        //window.alert(discript + "只能输入字母和数字！");
    					element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
						
    				}
    				break;
    			}	
    			
    		case "ali":{
    			if (element.value.TrimAll().length!= 0 && element.value.TrimAll().search(/[^a-zA-Z0-9-_]/g) != -1) {
    				document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "“ 只能输入字母和数字!×</font>";
   			        //window.alert(discript + "只能输入字母、数字、下划线和中横线");
   					element.focus();
   					return(0);
   				} else {
					document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
					
    			}
   				break;
    		}	
   			
    		case "cccli"://只能输入字母,数字允许空白字符下划线
    			{
    			    for (var i = 0;i<element.value.length;i++){
				    	if (element.value.charAt(i) == ' '){
				    		document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "“ 不能带空格符!×</font>";
	    			        //window.alert(discript + "不能带空格符");
	    					element.focus();
	    					return(0);
    					} else {
							document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
							
    					}
					}
    			    if (element.value.length != 0 && isInteger(element.value.charAt(0))) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "“ 首字符不能是数字!×</font>";
    			        //window.alert(discript + "首字符不能是数字!");
    					element.focus();
    					return(0);
    			    } else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
						
    				}
    				
    			    if (element.value.length != 0 && element.value.search(/[^a-zA-Z_0-9]/g) != -1) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "“ 只能输入字母或数字，允许下划线!×</font>";
    			        //window.alert(discript + "只能输入字母或数字，允许下划线!");
    					element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
						
    				}
    				break;
    			}
    			
    		case "ccli"://只能输入汉字,字母,数字允许空白字符下划线
    			{
    			    if (element.value.length != 0 && isInteger(element.value.charAt(0))) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "“ 首字符不能是数字!×</font>";
    			        //window.alert(discript + "首字符不能是数字!");
    					element.focus();
    					return(0);
    			    } else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
						
    				}
    				
    			    if (element.value.TrimAll().length != 0 && element.value.TrimAll().search(/[^a-zA-Z_0-9\u4E00-\u9FA5\s]/g) != -1) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "“ 只能输入汉字、字母和数字!×</font>";
    			        //window.alert(discript + "只能输入汉字、字母和数字!");
    					element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
						
    				}
    				break;
    			}
    			
			case "cli"://只能输入汉字,字母,数字
    			{
    			    if (element.value.TrimAll().length != 0 && element.value.TrimAll().search(/[^a-zA-Z0-9\u4E00-\u9FA5]/g) != -1) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "“ 只能输入汉字、字母和数字!×</font>";
    			        //window.alert(discript + "只能输入汉字、字母和数字!");
    					element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
						
    				}
    				break;
    			}
    			
    		case "cliai"://只能输入汉字,字母,数字,"-"
    			{
    			    if (element.value.TrimAll().length != 0 && element.value.TrimAll().search(/[^a-zA-Z0-9-\u4E00-\u9FA5\－]/g) != -1) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "“ 只能输入汉字、-、字母和数字!×</font>";
    			        //window.alert(discript + "只能输入汉字、字母和数字!");
    					element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
    				}
    				break;
    			}	
    		case "ip"://ip地址校验
    			{
    			    if (element.value.length != 0 && isIP(element.value) != true) {
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">请输入有效的 ”"+discript + "“×</font>";
    			        //window.alert("请输入有效的" + discript + "!");
    			        element.focus();
    					return(0);
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
						
    				}
    				break;
    			}
    			
    		case "s"://字符串
    			{
    				if(element.value.TrimAll()=='' && '0'==nullflag){
    			    	document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "“ 不能为空!×</font>";
	   					//window.alert(discript + "不能为空!");
	   					element.focus();
	   					return(0);		   
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
						//
    				}
    				break;
    			}
    			
    		case "d"://日期
    			{
    				if (element.value.length != 0 && isDate(element.value) == false) {
    					document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "“ 必须输入有效日期!×</font>";
    			        //window.alert(discript + "必须输入有效日期!");
    					element.focus();
    					return(0);		   
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
						
    				}
    			    break;
    			}
    			
    	    case "e"://邮件地址
    			{
    				if (element.value.length != 0 && isEmail(element.value) == false) {
    					document.getElementById(elementId).innerHTML="<font color=\"red\">请输入有效的 ”"+discript + "“×</font>";
    			        //window.alert("请输入有效的" + discript + "!");
    					element.focus();
    					return(0);		   
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
						
    				}
    			    break;
    			}
    			
    		case "c"://身份证
    			{
    				if (element.value.length != 0 && isIdCard(element.value) == false) {
    					document.getElementById(elementId).innerHTML="<font color=\"red\">请输入有效的 ”"+discript + "“×</font>";
    			        //window.alert("请输入有效的" + discript + "!");
    					element.focus();
    					return(0);		   
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
						
    				}
    			    break;
    			}
    			
    		case "p"://邮编
    			{
    				if (element.value.length != 0 && isPostCode(element.value) == false) {
    					document.getElementById(elementId).innerHTML="<font color=\"red\">请输入有效的 ”"+discript + "“×</font>";
    			        //window.alert("请输入有效的" + discript + "!");
    					element.focus();
    					return(0);		   
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
						
    				}
    			    break;
    			}
    		case "h":
    			{
    				if (element.value.length != 0 && validateURL(element.value) == false) {
    					document.getElementById(elementId).innerHTML="<font color=\"red\">请输入合法的 “"+discript + "”×</font>";
    			        //window.alert("请输入合法的网页地址！");
    					element.focus();
    					return(0);		   
    				} else {
						document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
						
    				}
    			    break;
    			}	

    	}
    }
    
    if (length != "" && type != "d") {
		var elementId = element.id +"Error";
    	if(type=="i"){
    		if (lengthCheck(element.value, length)) {
//	    	    window.alert(discript + "的长度必须小于" + length + "个数字！");
    			document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "” 的长度必须小于" + length + "个数字！×</font>";
	    	    element.focus();
	    	    return(0);
	    	}
    	}else if(type=="m"){
    		if (lengthCheck(element.value, length)) {
//	    	    window.alert(discript + "的长度必须小于" + length + "个数字！");
    			document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "” 的长度必须小于" + length + "位！×</font>";
	    	    element.focus();
	    	    return(0);
	    	}
    	}
    	else if (lengthCheck(element.value, length)) {
    			//alert(element.id);
   			document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "” 的长度必须小于" + length/2 + "个汉字！" + length + "个字符！×</font>";
//    	    window.alert(discript + "的长度必须小于" + length/2 + "个汉字！" + length + "个字符！");
    	    element.focus();
    	    return(0);
    	}
  		
		document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";

    }
    
    if (maxs != "" && mins != "") {
        if (element.value < mins || element.value > maxs) {
        	document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "” 的值必须小于" + maxs + ", 大于" + mins + "!×</font>";
            //window.alert(discript + "的值必须小于" + maxs + ", 大于" + mins + "!");
    	    element.focus();
    	    return(0);
        } else {
			document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
			
		}
    } else {
        if (maxs != "") {
    	    if (element.value > maxs) {
    	    	document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "” 的值必须小于" + maxs + "!×</font>";
    	        window.alert(discript + "的值必须小于" + maxs + "!");
    		    element.focus();
    		    return(0);
    	    } else {
				document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
				
			}
        }
        if (mins != "") {
    	    if (element.value < mins) {
    	    	document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript + "” 的值必须大于" + mins + "!×</font>";
    	        //window.alert(discript + "的值必须大于" + mins + "!");
    		    element.focus();
    		    return(0);
    	    } else {
				document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
				
			}
        }
    }
    
    return(1);
}

//判断是否整数
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

//判断是否有效邮件
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

//判断是否为身份证
function isIdCard(idCard) {
    var v_idCard = killSpace(idCard);
    if (v_idCard == "") return false;
    
    if (v_idCard.length != 15 && v_idCard.length != 18) {
    	document.getElementById(elementId).innerHTML="<font color=\"red\">“身份证” 必须为15位或18位!×</font>";
        //alert("身份证必须为15位或18位！");
        return false;
    } else {
		document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
		return true;
	}
    
    if (v_idCard.search(/[^0-9a-zA-Z]/g) != -1) {
    	document.getElementById(elementId).innerHTML="<font color=\"red\">“身份证” 必须全部为字母或数字，不能包含其它字符!×</font>";
        //alert("身份证必须全部为字母或数字，不能包含其它字符!");
        return false;
    } else {
    	document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
        return true;
    }
}

//判断是否为邮编

function isPostCode(postCode) {
    var v_postCode = killSpace(postCode);
    if (v_postCode == "") return false;
        
    if (v_postCode.length != 6) {
		document.getElementById(elementId).innerHTML="<font color=\"red\">“邮政编码” 必须为6位数字!×</font>";
        //alert("邮政编码必须为6位数字！");
        return false;
    } else {
    	document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
        return true;
    }
    
    if (!isInteger(v_postCode)) {
    	document.getElementById(elementId).innerHTML="<font color=\"red\">“邮政编码” 必须全部为数字!×</font>";
        //alert("邮政编码必须全部为数字！");
        return false;
    }else {
    	document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
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
* 调用此函数校验日期格式

* @param s         输入日期string s,例如2001-01-01
* @return          若输入的日期没有包含非法字符，以及该日期合法，则返回true；否则返回false
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

//校验日期是否合法
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
    		if(mod(iyear, 4) == 0 && (mod(iyear, 100) != 0 || mod(iyear, 400) == 0)) {//判断是否润年
    			if (iday > 29) return false;
    		}else {
    			if (iday > 28) return false;
    		}
    }
	return true;
}

//取得输入字符串所代表的整数值

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

var e_tag; //当前正在分拆得alt值


//开始解析分拆alt
function parseTag(p_tag) {
	e_tag = p_tag;
}

//得到下一个分拆处理的串
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

//删除空格处理，供checkValidate调用
function killSpace(x) {
    while ((x.length > 0) && (x.charAt(0) == ' '))
    	x = x.substring(1, x.length)
    while ((x.length > 0) && (x.charAt(x.length - 1) == ' '))
    	x = x.substring(0, x.length - 1)
    return x;

}

//计算中文长度
function count_char(str) {
	var len = 0;
	for(i = 0; i < str.length; i++) {
		var ech = escape(str.charAt(i));
		if ( ech.length > 4 ){
//			len++;

//// 修改下面的数字，len + 1 表示2个字符代表一个中文字，len + 5 表示6个字符代表一个中文字
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

//校验输入的内容是否超过指定的长度
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

//判断是否IP构建正则表达式

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
			alert(parseInt(obj.value)+"地址错误!")
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

function notspecial(element,discript) {//只能输入汉字,字母,数字允许空白字符下划,主要用于列表里的查询
   if (null != element && element.value != "") {
    	if (element.value.length != 0 && element.value.search(/[^a-zA-Z_0-9\u4E00-\u9FA5\s]/g) != -1) {
			document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript+"” 只能输入汉字、字母和数字!×</font>";
    		//window.alert(discript + "只能输入汉字、字母和数字!");
    		element.focus();
    		return false;
//    	} else {
//	    	document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
//	        return true;
        }
   }
   return true;
}
function notspecialNotSpace(element,discript) {//只能输入汉字,字母,数字允许空白字符下划,主要用于列表里的查询
   if (null != element && element.value != "") {
    	if (element.value.length != 0 && element.value.search(/[^a-zA-Z_0-9\u4E00-\u9FA5]/g) != -1) {
			document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript+"“ 只能输入汉字、字母和数字!×</font>";
    		//window.alert(discript + "只能输入汉字、字母和数字!");
    		element.focus();
    		return false;
//    	} else {
//	    	document.getElementById(elementId).innerHTML="<font color=\"font_bule14\"> √</font>";
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
			document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript+"“ 不能包含“'”×</font>";
			//window.alert(discript + "不能包含'");
    		element.focus();
    		return false;
		} else if (elementValue.indexOf("　") != -1) {
			//window.alert(discript + "不能包含全角空格");
			document.getElementById(elementId).innerHTML="<font color=\"red\">“"+discript+"“ 不能包含全角空格×</font>";
    		element.focus();
    		return false;
		} else {
			element.value=elementValue;
		}
		
	} else {
		document.getElementById(elementId).innerHTML="<font color=\"red\">请输入 “"+discript+"“</font>";
//		window.alert("请输入" + discript);
		element.focus();
		return false;
	}
} else {
	document.getElementById(elementId).innerHTML="<font color=\"red\">请输入 “"+discript+"“</font>";
	//window.alert("请输入" + discript);
	element.focus();
	return false;
}
return true;
}
//****生成ReplaceAll()方法
function  stringReplaceAll(AFindText,ARepText){
  raRegExp = new RegExp(AFindText,"g");
  return this.replace(raRegExp,ARepText)
}
//************将内容中所有的空格与回车替换************
//如：
//var s= "s  dd ss " +
//		  d "	;
//换成s&nbsp;dd&nbsp;<br/>&nbsp;
function replaceSpaceAndBr(e) {
	String.prototype.ReplaceAll = stringReplaceAll;
	var eValue = e.value;
	String.prototype.ReplaceAll = stringReplaceAll;
	eValue = eValue.ReplaceAll("\r\n","<br/>");
	eValue = eValue.ReplaceAll(" ","&nbsp;");
	return eValue;
}
//************验证替换空格与回车后的文本是否超过指定的长度************
function checkLength(e,length) {
	var eValue = replaceSpaceAndBr(e);
	if (lengthCheck(eValue, length)) {
		e.focus();
		//alert("详细介绍的长度必须小于"+length/2+"个汉字！"+length+" 个字符！！");
		return false;
	} else {
		return true;
	}
}

//************将内容中所有的空格删除************
//如：
//var s= "s  dd ss " +;
//换成"sddss";
function replaceSpace(e) {
	String.prototype.ReplaceAll = stringReplaceAll;
	var eValue = e.value;
	String.prototype.ReplaceAll = stringReplaceAll;
	eValue = eValue.ReplaceAll(" ","");
	eValue = eValue.ReplaceAll("　","");
	return eValue;
}

//去前后空格
String.prototype.Trim = function()
{
    return this.replace(/(^\s*)|(\s*$)/g, "");
}
String.prototype.TrimAll = function()
{
   return this.replace(/(\s*|\r\n*)/ig,"");
}
  
