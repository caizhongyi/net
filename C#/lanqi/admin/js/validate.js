/*
 * -------------------------------------------------------------------------------
 * 文件名称：validate.js 说 明：JavaScript脚本，用于检查网页提交表单的输入数据 版 本：1.0 修改纪录:
 * ---------------------------------------------------------------------------
 * 时间 修改人 说明 2009-6-12 wangchh 创建
 * -------------------------------------------------------------------------------
 */

/*
 * 用途：校验ip地址的格式 
 * 输入：strIP：ip地址 
 * 返回：如果通过验证返回true,否则返回false;
 */
function isIP(strIP) {
	if (isNull(strIP))
		return false;
	var re = /^(\d+)\.(\d+)\.(\d+)\.(\d+)$/g // 匹配IP地址的正则表达式
	if (re.test(strIP)) {
		if (RegExp.$1 < 256 && RegExp.$2 < 256 && RegExp.$3 < 256
				&& RegExp.$4 < 256)
			return true;
	}
	return false;
}

/*
 * 用途：检查输入字符串是否为空或者全部都是空格 
 * 输入：str 
 * 返回： 如果全是空返回true,否则返回false
 */
function isNull(str) {
	if (str == "")
		return true;
	var regu = "^[ ]+$";
	var re = new RegExp(regu);
	return re.test(str);
}

/*
 * 用途：检查输入对象的值是否符合整数格式 
 * 输入：str 输入的字符串 
 * 返回：如果通过验证返回true,否则返回false
 */
function isInteger(str) {
	var regu = /^[-]{0,1}[0-9]{1,}$/;
	return regu.test(str);
}

/*
 * 用途：检查输入手机号码是否正确 
 * 输入： s：字符串 
 * 返回： 如果通过验证返回true,否则返回false
 */
function checkMobile(s) {
	var regu = /^[1][3][0-9]{9}$/;
	var re = new RegExp(regu);
	if (re.test(s)) {
		return true;
	} else {
		return false;
	}
}

/*
 * 用途：检查输入字符串是否符合正整数格式 
 * 输入： s：字符串 
 * 返回： 如果通过验证返回true,否则返回false
 */
function isNumber(s) {
	var regu = "^[0-9]+$";
	var re = new RegExp(regu);
	if (s.search(re) != -1) {
		return true;
	} else {
		return false;
	}
}

/*
 * 用途：检查输入字符串是否是带小数的数字格式,可以是负数 
 * 输入： s：字符串 
 * 返回： 如果通过验证返回true,否则返回false
 */
function isDecimal(str) {
	if (isInteger(str))
		return true;
	var re = /^[-]{0,1}(\d+)[\.]+(\d+)$/;
	if (re.test(str)) {
		if (RegExp.$1 == 0 && RegExp.$2 == 0)
			return false;
		return true;
	} else {
		return false;
	}
}

/*
 * 用途：检查输入对象的值是否符合端口号格式 
 * 输入：str 输入的字符串 
 * 返回：如果通过验证返回true,否则返回false
 */
function isPort(str) {
	return (isNumber(str) && str < 65536);
}

/*
 * 用途：检查输入对象的值是否符合E-Mail格式 
 * 输入：str 输入的字符串 
 * 返回：如果通过验证返回true,否则返回false
 */
function isEmail(str) {
	var myReg = /^[-_A-Za-z0-9]+@([_A-Za-z0-9]+\.)+[A-Za-z0-9]{2,3}$/;
	if (myReg.test(str))
		return true;
	return false;
}

/*
 * 用途：检查输入字符串是否符合金额格式 格式定义为带小数的正数，小数点后最多三位 
 * 输入： s：字符串 
 * 返回： 如果通过验证返回true,否则返回false
 */
function isMoney(s) {
	var regu = "^[0-9]+[\.][0-9]{0,3}$";
	var re = new RegExp(regu);
	if (re.test(s)) {
		return true;
	} else {
		return false;
	}
}
/*
 * 用途：检查输入字符串是否只由英文字母和数字和下划线组成 
 * 输入： s：字符串 
 * 返回： 如果通过验证返回true,否则返回false
 */
function isNumberOr_Letter(s) { // 判断是否是数字或字母
	var regu = "^[0-9a-zA-Z\_]+$";
	var re = new RegExp(regu);
	if (re.test(s)) {
		return true;
	} else {
		return false;
	}
}
/*
 * 用途：检查输入字符串是否只由英文字母和数字组成 
 * 输入： s：字符串 
 * 返回： 如果通过验证返回true,否则返回false
 */
function isNumberOrLetter(s) { // 判断是否是数字或字母
	var regu = "^[0-9a-zA-Z]+$";
	var re = new RegExp(regu);
	if (re.test(s)) {
		return true;
	} else {
		return false;
	}
}
/*
 * 用途：检查输入字符串是否只由汉字、字母、数字组成 
 * 输入： value：字符串 
 * 返回： 如果通过验证返回true,否则返回false
 */
function isChinaOrNumbOrLett(s) { // 判断是否是汉字、字母、数字组成
	var regu = "^[0-9a-zA-Z\u4e00-\u9fa5]+$";
	var re = new RegExp(regu);
	if (re.test(s)) {
		return true;
	} else {
		return false;
	}
}

/*
 * 用途：检查输入字符串是否只由汉字、字母、数字、下划线组成 
 * 输入： value：字符串 
 * 返回： 如果通过验证返回true,否则返回false
 */
function isChinaOrNumbOrLettOr_(s) { // 判断是否是汉字、字母、数字、下划线组成
	var regu = "^[0-9a-zA-Z\u4e00-\u9fa5\_]+$";
	var re = new RegExp(regu);
	if (re.test(s)) {
		return true;
	} else {
		return false;
	}
}

/*
 * 用途：检查输入的日期是否符合 yyyyMMdd 
 * 输入： value：字符串 
 * 返回： 如果通过验证返回true,否则返回false
 */
function isDate(value) {
	if (value.length != 8 || !isNumber(value))
		return false;
	var year = value.substring(0, 4);
	if (year > "2100" || year < "1900")
		return false;

	var month = value.substring(4, 6);
	if (month > "12" || month < "01")
		return false;

	var day = value.substring(6, 8);
	if (day > getMaxDay(year, month) || day < "01")
		return false;

	return true;
}

/*
 * 用途：检查输入的日期是否符合 YYYY-MM-DD
 * 输入： thedate：日期值      obj：日期字段
 * 返回： 如果通过验证返回true,否则返回false
 */
function isDateBirthday(thedate, obj) {
	var reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})$/;
	var r = thedate.match(reg);
	if (r == null) {
		obj.focus();
		return false;

	}
	var d = new Date(r[1], r[3] - 1, r[4]);
	var newStr = d.getFullYear() + r[2] + (d.getMonth() + 1) + r[2]
			+ d.getDate()
	var newDate = r[1] + r[2] + (r[3] - 0) + r[2] + (r[4] - 0)
	if (newStr == newDate) {
		return true;
	}
	obj.focus();
	return false;
}

/*
 * 用途：根据输入的年月值，返回该月对应的最大天数
 * 输入： year:年  month:月
 * 返回： 该月的最大天数
 */
function getMaxDay(year, month) {
	if (month == 4 || month == 6 || month == 9 || month == 11)
		return "30";
	if (month == 2)
		if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
			return "29";
		else
			return "28";
	return "31";
}

/*
 * 用途：字符1是否以字符串2结束 
 * 输入：str1：字符串；str2：被包含的字符串 
 * 返回：如果通过验证返回true,否则返回false
 */
function isLastMatch(str1, str2) {
	var index = str1.lastIndexOf(str2);
	if (str1.length == index + str2.length)
		return true;
	return false;
}

/*
 * 用途：字符1是否以字符串2开始 
 * 输入：str1：字符串；str2：被包含的字符串 
 * 返回：如果通过验证返回true,否则返回false
 */
function isFirstMatch(str1, str2) {
	var index = str1.indexOf(str2);
	if (index == 0)
		return true;
	return false;
}

/*
 * 用途：字符1是包含字符串2 
 * 输入：str1：字符串；str2：被包含的字符串 
 * 返回：如果通过验证返回true,否则返回false
 */
function isMatch(str1, str2) {
	var index = str1.indexOf(str2);
	if (index == -1)
		return false;
	return true;
}

/*
 * 用途：检查输入的起止日期是否正确，规则为两个日期的格式正确， 且结束如期>=起始日期 
 * 输入： startDate：起始日期，字符串  endDate：结束如期，字符串 
 * 返回： 如果通过验证返回true,否则返回false
 */
function checkTwoDate(startDate, endDate) {
	if (!isDate(startDate)) {
		alert("起始日期不正确!");
		return false;
	} else if (!isDate(endDate)) {
		alert("终止日期不正确!");
		return false;
	} else if (startDate > endDate) {
		alert("起始日期不能大于终止日期!");
		return false;
	}
	return true;
}

/*
 * 用途：检查输入的电话号码格式是否正确 
 * 输入： strPhone：字符串 
 * 返回： 如果通过验证返回true,否则返回false
 */
function checkPhone(strPhone) {
	var phoneRegWithArea = /^[0][1-9]{2,3}-[0-9]{5,10}$/;
	var phoneRegNoArea = /^[1-9]{1}[0-9]{5,8}$/;
	//var prompt = "您输入的电话号码不正确!"
	if (strPhone.length > 9) {
		if (phoneRegWithArea.test(strPhone)) {
			return true;
		} else {
			//alert(prompt);
			return false;
		}
	} else {
		if (phoneRegNoArea.test(strPhone)) {
			return true;
		} else {
			//alert(prompt);
			return false;
		}
	}
}

/*
 * 用途： 检查复选框被选中的数目 
 * 输入： checkboxID：字符串 
 * 返回： 返回该复选框中被选中的数目
 */
function checkSelect(checkboxID) {
	var check = 0;
	var i = 0;
	if (document.all(checkboxID).length > 0) {
		for (i = 0; i < document.all(checkboxID).length; i++) {
			if (document.all(checkboxID).item(i).checked) {
				check += 1;
			}
		}
	} else {
		if (document.all(checkboxID).checked)
			check = 1;
	}
	return check;
}

/*
 * 用途： 计算指定输入框内的字节数
 * 输入： 输入框的name 
 * 返回： 返回总字节数
 */
function getTotalBytes(varField) {
	if (varField == null)
		return -1;

	var totalCount = 0;
	for (i = 0; i < varField.value.length; i++) {
		if (varField.value.charCodeAt(i) > 127)
			totalCount += 2;
		else
			totalCount++;
	}
	return totalCount;
}

/*
 * 用途： 获取第一个被选中复选框的值
 * 输入： 复选框ID
 * 返回： 第一个被选中复选框的值
 */
function getFirstSelectedValue(checkboxID) {
	var value = null;
	var i = 0;
	if (document.all(checkboxID).length > 0) {
		for (i = 0; i < document.all(checkboxID).length; i++) {
			if (document.all(checkboxID).item(i).checked) {
				value = document.all(checkboxID).item(i).value;
				break;
			}
		}
	} else {
		if (document.all(checkboxID).checked)
			value = document.all(checkboxID).value;
	}
	return value;
}

/*
 * 用途： 获取第一个被选中复选框的序号
 * 输入： 复选框ID
 * 返回： 第一个被选中复选框的序号
 */
function getFirstSelectedIndex(checkboxID) {
	var value = -2;
	var i = 0;
	if (document.all(checkboxID).length > 0) {
		for (i = 0; i < document.all(checkboxID).length; i++) {
			if (document.all(checkboxID).item(i).checked) {
				value = i;
				break;
			}
		}
	} else {
		if (document.all(checkboxID).checked)
			value = -1;
	}
	return value;
}

/*
 * 用途： 将指定复选框的选中状态改为指定状态
 * 输入： checkboxID：复选框ID   status：指定状态
 * 返回： 
 */
function selectAll(checkboxID, status) {
	if (document.all(checkboxID) == null)
		return;

	if (document.all(checkboxID).length > 0) {
		for (i = 0; i < document.all(checkboxID).length; i++) {
			document.all(checkboxID).item(i).checked = status;
		}
	} else {
		document.all(checkboxID).checked = status;
	}
}

/*
 * 用途： 将指定复选框的选中状态分别改为相反状态
 * 输入： checkboxID：复选框ID   
 * 返回： 
 */
function selectInverse(checkboxID) {
	if (document.all(checkboxID) == null)
		return;

	if (document.all(checkboxID).length > 0) {
		for (i = 0; i < document.all(checkboxID).length; i++) {
			document.all(checkboxID).item(i).checked = !document
					.all(checkboxID).item(i).checked;
		}
	} else {
		document.all(checkboxID).checked = !document.all(checkboxID).checked;
	}
}

/*
 * 用途：检查输入的起止日期是否正确，规则为两个日期的格式正确或都为空 且结束日期>=起始日期 
 * 输入： startDate：起始日期，字符串 endDate： 结束日期，字符串 
 * 返回： 如果通过验证返回true,否则返回false
 */
function checkPeriod(startDate, endDate) {
	if (!checkDate(startDate)) {
		//alert("起始日期不正确!");
		return false;
	} else if (!checkDate(endDate)) {
		//alert("终止日期不正确!");
		return false;
	} else if (startDate > endDate) {
		//alert("起始日期不能大于终止日期!");
		return false;
	}
	return true;
}

/*
 * 用途：检查证券代码是否正确 
 * 输入： secCode:证券代码 
 * 返回： 如果通过验证返回true,否则返回false
 */
function checkSecCode(secCode) {
	if (secCode.length != 6) {
		//alert("证券代码长度应该为6位");
		return false;
	}

	if (!isNumber(secCode)) {
		//alert("证券代码只能包含数字");
		return false;
	}
	return true;
}

/*******************************************************************************
 * function : cTrim(sInputString,iType) description : 字符串去空格的函数 parameters :
 * iType： 1=去掉字符串左边的空格 2=去掉字符串左边的空格 0=去掉字符串左边和右边的空格 return value: 去掉空格的字符串
 ******************************************************************************/
function cTrim(sInputString, iType) {
	var sTmpStr = ' ';
	var i = -1;

	if (iType == 0 || iType == 1) {
		while (sTmpStr == ' ') {
			++i;
			sTmpStr = sInputString.substr(i, 1);
		}
		sInputString = sInputString.substring(i);
	}
	if (iType == 0 || iType == 2) {
		sTmpStr = ' ';
		i = sInputString.length;
		while (sTmpStr == ' ') {
			--i;
			sTmpStr = sInputString.substr(i, 1);
		}
		sInputString = sInputString.substring(0, i + 1);
	}
	return sInputString;
}

/*
 * 用途：检查输入字符串是否以字母开头，只允许字母、数字、下划线，5-20个字节
 * 输入： value：字符串 
 * 返回： 如果通过验证返回true,否则返回false
 */
function isLogonName(s) { 
	// var regu = "^[a-zA-Z]{1}[_0-9a-zA-Z]{4,19}$";
	var regu = "^[a-zA-Z]{1}[0-9a-zA-Z_]{4,19}$";
	var re = new RegExp(regu);
	if (re.test(s)) {
		return true;
	} else {
		return false;
	}
}

/*
 * 用途：检查输入字符串是否只由汉字、字母、数字、下划线组成，1-20个字节 
 * 输入： value：字符串 
 * 返回： 如果通过验证返回true,否则返回false
 */
function isNickName(s) {
	var regu = "^[0-9a-zA-Z\u4e00-\u9fa5\_]{1,20}$";
	var re = new RegExp(regu);
	if (re.test(s)) {
		return true;
	} else {
		return false;
	}
}

/*
 * 用途：判断输入的是否为邮编。需要满足6位整数的才可能是邮编
 * 输入：field：邮编文本域
 * 返回：如果通过验证返回true,否则返回错误描述;
 */
function isYzbm(field) {
	var re1 = /^\d+$/g;
	var ii1 = field.value.match(re1);

	if (ii1 == null || ii1 == "") {
		field.focus();
		return "邮编只能是数字";
	} else if (field.value.length != 6) {
		field.focus();
		return "邮编位数不对";
	}
	return true;
}

/*
 * 用途：检验身份证是否合法
 * 输入：field：身份证文本域
 * 返回：如果通过验证返回true,否则返回错误描述;
 */
function checkSfz(field) {
	var zjhm = field.value.toUpperCase();
	if (zjhm != null && zjhm != "") {
		if (!(zjhm.length == 15 || zjhm.length == 18)) {
			field.focus();
			return "身份证长度不对,请检查！";
		}
		zjhm1 = zjhm;
		if (zjhm.length == 18) {
			zjhm1 = zjhm.substr(0, 17);
			zjhm2 = zjhm.substr(17, 1);
		}
		re = new RegExp("[^0-9]");
		if (s = zjhm1.match(re)) {
			field.focus();
			return "身份证输入的值中含有非法字符'" + s + "'请检查！";
		}
		// 取出生日期
		if (zjhm.length == 15) {
			birthday = "19" + zjhm.substr(6, 6);
		} else {
			re = new RegExp("[^0-9A-Z]");
			if (s = zjhm2.match(re)) { // 18位身份证对末位要求数字或字符
				field.focus();
				return "身份证输入的值中含有非法字符'" + s + "'请检查！";
			}
			birthday = zjhm.substr(6, 8);
		}

		birthday = birthday.substr(0, 4) + "-" + birthday.substr(4, 2) + "-"
				+ birthday.substr(6, 2)

		if (isDateBirthday("身份证号码出生日期", birthday, field) == 0) { // 检查日期的合法性
			return false;
		}
		if (zjhm.length == 18) {
			return (checkSfzOf18(zjhm, field)); // 对18位长的身份证进行末位校验
		}
	}
	return true;
}



/*
 * 用途：对18位长的身份证进行末位校验
 * 输入：身份证号码
 * 返回：如果通过验证返回true,否则返回false;
 */
function checkSfzOf18(hm, obj) {

	var w = new Array();
	var ll_sum;
	var ll_i;
	var ls_check;

	w[0] = 7;
	w[1] = 9;
	w[2] = 10;
	w[3] = 5;
	w[4] = 8;
	w[5] = 4;
	w[6] = 2;
	w[7] = 1;
	w[8] = 6;
	w[9] = 3;
	w[10] = 7;
	w[11] = 9;
	w[12] = 10;
	w[13] = 5;
	w[14] = 8;
	w[15] = 4;
	w[16] = 2;
	ll_sum = 0;

	for (ll_i = 0; ll_i <= 16; ll_i++) { // alert("ll_i:"+ll_i+"
											// "+hm.substr(ll_i,1)+"w[ll_i]:"+w[ll_i]+"
											// ll_sum:"+ll_sum);
		ll_sum = ll_sum + (hm.substr(ll_i, 1) - 0) * w[ll_i];

	}
	ll_sum = ll_sum % 11;

	switch (ll_sum) {
		case 0 :
			ls_check = "1";
			break;
		case 1 :
			ls_check = "0";
			break;
		case 2 :
			ls_check = "X";
			break;
		case 3 :
			ls_check = "9";
			break;
		case 4 :
			ls_check = "8";
			break;
		case 5 :
			ls_check = "7";
			break;
		case 6 :
			ls_check = "6";
			break;
		case 7 :
			ls_check = "5";
			break;
		case 8 :
			ls_check = "4";
			break;
		case 9 :
			ls_check = "3";
			break;
		case 10 :
			ls_check = "2";
			break;
	}

	if (hm.substr(17, 1) != ls_check) {
		// alert("身份证校验错误！------ 末位应该:"+ls_check+" 实际:"+hm.substr(17,1));
		// obj.parentNode.previousSibling.focus();
		// // obj.focus();
		// return 0;

		//alert('身份证校验错误,请重输');
		// obj.focus();
		obj.focus();
		return false;

	}
	return 1;
}
