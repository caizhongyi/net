if(typeof(czyjs.Validate)=="undefined")
{
    czyjs.Validate = {};
}

czyjs.Validate.ValidateHelper = Class.create();
czyjs.Validate.ValidateHelper.prototype = {
	initialize: function(){
	},
RefreshCode:function(obj, imgUrl) {
        obj.src = imgUrl + "?rnd=" + Math.random() * 10000;
    },
	//是否是特殊字符
HasIllegalStr:function(str)
{
	var specialStrEN=new Array("~","!","`","@","#","$","%","^","&","*","(",")","-","_","+","=","\\","|","{","[","]","}",":","'","\"",";","<",">",",",".","?","/");
    var specialStrCN=new Array("！","·","#","￥","%","……","—","*","（","）","-","——","。","+","|","、","《","》","，","：","；","“","‘","？");
    for(var i=0;i<specialStrEN.length;i++)
	{
		if(str.indexOf(specialStrEN[i])!=-1)
		{
			return true;
		}
	}
	  for(var i=0;i<specialStrCN.length;i++)
	{
		if(str.indexOf(specialStrCN[i])!=-1)
		{
			return true;
		}
	}
	return false;
},	
	 //电话号码验证
isPhone: function(str){
	/*
		  var reg = /^(\d{3,4})-(\d{7,8})/;
	    if( str.constructor === String ){
	       var re = str.match( reg );
	       return true;
	   }
	   return false;
   */
     this.isNumber(str)?true:false;
	},
	//判断文本是否为空
	TxtIsNull: function(str){
		if (str == "") {
			return true;
		}
		else {
			return false;
		}
	},
	//判断select是否选择
	SelectIsChoose: function(obj){
		if (obj.selectedIndex != 0) {
			return true;
		}
		else {
			return false;
		}
	},
	//验证是否为手机号
	isTel: function(str){
		var tel = /^[0-9]{11}$/;
		if (tel.test(str)) {
			return true;
		}
		else {
			return false;
		}
		
	},
	//验证邮箱是否合法
	isMail: function(str){
		return (new RegExp(/^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$/).test(str));
	},
	
	//身份号验证
	isIDNumber: function(str){
		return (new RegExp(/^\d{15}|\d{17}[A-Z]$/).test(str));
	},
	//字母下画线和数字 并且字母开头 3 - 16位
	UserFormat: function(str){
		var pattern = /^[a-zA-Z][a-zA-Z0-9_]{1,14}[a-zA-Z0-9]$/i;// 用户名只能是字母下画线和数字 并且字母开头 3 - 16位
		if (pattern.test(str)) {
			return true;
		}
		else {
			return false;
		}
	},
	// 末字符不能是下划线	
	LastCharNotLine: function(str){
		var patternLastChar = /^[a-zA-Z0-9_]{1,15}_$/i;// 末字符不能是下划线
		if (patternLastChar.test(str)) {
			return true;
		}
		else {
			return false;
		}
	},
	// 开头字符必须为字母
	FistCharIsChar: function(str){
		var patternFirstChar = /^[0-9_][a-zA-Z0-9_]{1,14}$/i; // 开头字符必须为字母
		if (patternFirstChar.test(str)) {
			return true;
		}
		else {
			return false;
		}
	},
	//判断字符窜中是否包含某字符
	StrIsNotIncludeChar: function(str, notNeedChar){
		if (str.indexOf(notNeedChar, 0) != -1) {
			return true;
		}
		else {
			return false;
		}
	},
	//控制文本输入的长度0则为不限制
	limitStrLenght: function(str, minLength, maxLength){
		if (maxLength == 0 && str.length >= minLength) {
			return true;
		}
		else 
			if (str.length >= minLength && str.length <= maxLength) {
				return true;
			}
		eles
		{
			return false;
		}
		
	},
	//对比两字符窜是否相同
	CompareStr: function(str1, str2){
		if (str1 == str2) {
			return true;
		}
		else {
			return false;
		}
	},
	
	/*
	 用途：校验ip地址的格式
	 输入：strIP：ip地址
	 返回：如果通过验证返回true,否则返回false；
	 
	 */
	isIP: function(strIP){
		if (isNull(strIP)) 
			return false;
		var re = /^(\d+)\.(\d+)\.(\d+)\.(\d+)$/g //匹配IP地址的正则表达式 
		if (re.test(strIP)) {
			if (RegExp.$1 < 256 && RegExp.$2 < 256 && RegExp.$3 < 256 && RegExp.$4 < 256) 
				return true;
		}
		return false;
	},
	
	/* 
	 用途：检查输入字符串是否为空或者全部都是空格
	 输入：str
	 返回：
	 如果全是空返回true,否则返回false
	 */
	isNull: function(str){
		if (str == "") 
			return true;
		var regu = "^[ ]+$";
		var re = new RegExp(regu);
		return re.test(str);
	},
	
	
	/* 
	 用途：检查输入对象的值是否符合整数格式
	 输入：str 输入的字符串
	 返回：如果通过验证返回true,否则返回false
	 
	 */
	isInteger: function(str){
		var regu = /^[-]{0,1}[0-9]{1,}$/;
		return regu.test(str);
	},
	
	/* 
	 用途：检查输入手机号码是否正确
	 输入：
	 s：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 
	 */
	checkMobile: function(s){
		var regu = /^[1][3][0-9]{9}$/;
		var re = new RegExp(regu);
		if (re.test(s)) {
			return true;
		}
		else {
			return false;
		}
	},
	
	
	/* 
	 用途：检查输入字符串是否符合正整数格式
	 输入：
	 s：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 
	 */
	isNumber: function(s){
		var regu = "^[0-9]+$";
		var re = new RegExp(regu);
		if (s.search(re) != -1) {
			return true;
		}
		else {
			return false;
		}
	},
	
	/* 
	 用途：检查输入字符串是否是带小数的数字格式,可以是负数
	 输入：
	 s：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 
	 */
	isDecimal: function(str){
		if (isInteger(str)) 
			return true;
		var re = /^[-]{0,1}(\d+)[\.]+(\d+)$/;
		if (re.test(str)) {
			if (RegExp.$1 == 0 && RegExp.$2 == 0) 
				return false;
			return true;
		}
		else {
			return false;
		}
	},
	
	/* 
	 用途：检查输入对象的值是否符合端口号格式
	 输入：str 输入的字符串
	 返回：如果通过验证返回true,否则返回false
	 
	 */
	isPort: function(str){
		return (isNumber(str) && str < 65536);
	},
	
	/* 
	 用途：检查输入对象的值是否符合E-Mail格式
	 输入：str 输入的字符串
	 返回：如果通过验证返回true,否则返回false
	 
	 */
	isEmail: function(str){
		var myReg = /^[-_A-Za-z0-9]+@([_A-Za-z0-9]+\.)+[A-Za-z0-9]{2,3}$/;
		if (myReg.test(str)) 
			return true;
		return false;
	},
	
	/* 
	 用途：检查输入字符串是否符合金额格式
	 格式定义为带小数的正数，小数点后最多三位
	 输入：
	 s：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 
	 */
	isMoney: function(s){
		var regu = "^[0-9]+[\.][0-9]{0,3}$";
		var re = new RegExp(regu);
		if (re.test(s)) {
			return true;
		}
		else {
			return false;
		}
	},
	/* 
	 用途：检查输入字符串是否只由英文字母和数字和下划线组成
	 输入：
	 s：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 
	 */
	isNumberOr_Letter: function(s){//判断是否是数字或字母 
		var regu = "^[0-9a-zA-Z\_]+$";
		var re = new RegExp(regu);
		if (re.test(s)) {
			return true;
		}
		else {
			return false;
		}
	},
	/* 
	 用途：检查输入字符串是否只由英文字母和数字组成
	 输入：
	 s：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 
	 */
	isNumberOrLetter: function(s){//判断是否是数字或字母 
		var regu = "^[0-9a-zA-Z]+$";
		var re = new RegExp(regu);
		if (re.test(s)) {
			return true;
		}
		else {
			return false;
		}
	},
	/* 
	 用途：检查输入字符串是否只由汉字、字母、数字组成
	 输入：
	 ＆#118alue：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 
	 */
	isChinaOrNumbOrLett: function(s){//判断是否是汉字、字母、数字组成 
		var regu = "^[0-9a-zA-Z\u4e00-\u9fa5]+$";
		var re = new RegExp(regu);
		if (re.test(s)) {
			return true;
		}
		else {
			return false;
		}
	},
	
	/* 
	 用途：判断是否是日期
	 输入：date：日期；fmt：日期格式
	 返回：如果通过验证返回true,否则返回false
	 */
	isDate: function(date, fmt){
		if (fmt == null) 
			fmt = "yyyyMMdd";
		var yIndex = fmt.indexOf("yyyy");
		if (yIndex == -1) 
			return false;
		var year = date.substring(yIndex, yIndex + 4);
		var mIndex = fmt.indexOf("MM");
		if (mIndex == -1) 
			return false;
		var month = date.substring(mIndex, mIndex + 2);
		var dIndex = fmt.indexOf("dd");
		if (dIndex == -1) 
			return false;
		var day = date.substring(dIndex, dIndex + 2);
		if (!isNumber(year) || year > "2100" || year < "1900") 
			return false;
		if (!isNumber(month) || month > "12" || month < "01") 
			return false;
		if (day > getMaxDay(year, month) || day < "01") 
			return false;
		return true;
	},
	
	getMaxDay: function(year, month){
		if (month == 4 || month == 6 || month == 9 || month == 11) 
			return "30";
		if (month == 2) 
			if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0) 
				return "29";
			else 
				return "28";
		return "31";
	},
	
	/* 
	 用途：字符1是否以字符串2结束
	 输入：str1：字符串；str2：被包含的字符串
	 返回：如果通过验证返回true,否则返回false
	 
	 */
	isLastMatch: function(str1, str2){
		var index = str1.lastIndexOf(str2);
		if (str1.length == index + str2.length) 
			return true;
		return false;
	},
	
	
	/* 
	 用途：字符1是否以字符串2开始
	 输入：str1：字符串；str2：被包含的字符串
	 返回：如果通过验证返回true,否则返回false
	 
	 */
	isFirstMatch: function(str1, str2){
		var index = str1.indexOf(str2);
		if (index == 0) 
			return true;
		return false;
	},
	
	/* 
	 用途：字符1是包含字符串2
	 输入：str1：字符串；str2：被包含的字符串
	 返回：如果通过验证返回true,否则返回false
	 
	 */
	isMatch: function(str1, str2){
		var index = str1.indexOf(str2);
		if (index == -1) 
			return false;
		return true;
	},
	
	
	/* 
	 用途：检查输入的起止日期是否正确，规则为两个日期的格式正确，
	 且结束如期>=起始日期
	 输入：
	 startDate：起始日期，字符串
	 endDate：结束如期，字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 
	 */
	checkTwoDate: function(startDate, endDate){
		if (!isDate(startDate)) {
			alert("起始日期不正确!");
			return false;
		}
		else 
			if (!isDate(endDate)) {
				alert("终止日期不正确!");
				return false;
			}
			else 
				if (startDate > endDate) {
					alert("起始日期不能大于终止日期!");
					return false;
				}
		return true;
	},
	
	/* 
	 用途：检查输入的Email信箱格式是否正确
	 输入：
	 strEmail：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 
	 */
	checkEmail: function(strEmail){
		//var emailReg = /^[_a-z0-9]+@([_a-z0-9]+\.)+[a-z0-9]{2,3}$/; 
		var emailReg = /^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$/;
		if (emailReg.test(strEmail)) {
			return true;
		}
		else {
			alert("您输入的Email地址格式不正确！");
			return false;
		}
	},
	
	/*
	 用途：检查输入的电话号码格式是否正确
	 输入：
	 strPhone：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 
	 */
	checkPhone: function(strPhone){
		var phoneRegWithArea = /^[0][1-9]{2,3}-[0-9]{5,10}$/;
		var phoneRegNoArea = /^[1-9]{1}[0-9]{5,8}$/;
		var prompt = "您输入的电话号码不正确!"
		if (strPhone.length > 9) {
			if (phoneRegWithArea.test(strPhone)) {
				return true;
			}
			else {
				alert(prompt);
				return false;
			}
		}
		else {
			if (phoneRegNoArea.test(strPhone)) {
				return true;
			}
			else {
				alert(prompt);
				return false;
			}
		}
	},

	//上传图片的扩展名验证
	checkPhotoPath: function(photoPath){
		var files = /\.bmp$|\.BMP$|\.gif$|\.jpg$|\.png$|\.PNG$|\.jpeg$|\.JPEG$|\.GIF$|\.JPG$\b/;
		if (!files.test(photoPath)) {
			return false;
		}
		return true;
	}
}
