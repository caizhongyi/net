/*
String 对像的扩展
functions:isImage() -> 文件名称是否为图片类型
		  hasSpecialStr() -> 是否包含特殊字符
		  isTel(str) -> 验证是否为手机号
		  isPhone()-> 是否为电话号码
		  isMail() -> 是否为邮件
		  isIDNumber() -> 是否为身份号
		  isUserName() -> 字母下画线和数字 并且字母开头 3 - 16位
		  isLastLine() -> 末字是否有下划线
		  isFirstLetter() -> 是否开头为字母
		  isIP() -> 是否是IP地址
		  isInteger() -> 检查输入对象的值是否符合整数格式
		  isNumber() -> 检查输入字符串是否符合正整数格式
		  isDecimal() -> 检查输入字符串是否是带小数的数字格式,可以是负数
		  isPort() -> 检查输入对象的值是否符合端口号格式
		  isMoney() -> 检查输入字符串是否符合金额格式
		  isNumberOr_Letter() -> 检查输入字符串是否只由英文字母和数字和下划线组成
		  isNumberOrLetter() -> 检查输入字符串是否只由英文字母和数字组成
		  isChinaOrNumbOrLett() -> 检查输入字符串是否只由汉字、字母、数字组成
		  isLastLine() -> 末字是否有下划线

*/

(function(string) {
    string.fn = string.prototype;
    //文件名称是否为图片类型
    string.fn.isImage = function() {
        var files = /\.bmp$|\.BMP$|\.gif$|\.jpg$|\.png$|\.PNG$|\.jpeg$|\.JPEG$|\.GIF$|\.JPG$\b/;
        if (!files.test(this)) {
            return false;
        }
        return true;
    };
    string.fn.hasSpecialStr = function(str) {
        var specialStrEN = new Array("~", "!", "`", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "+", "=", "\\", "|", "{", "[", "]", "}", ":", "'", "\"", ";", "<", ">", ",", ".", "?", "/");
        var specialStrCN = new Array("！", "·", "#", "￥", "%", "……", "—", "*", "（", "）", "-", "——", "。", "+", "|", "、", "《", "》", "，", "：", "；", "“", "‘", "？");
        for (var i = 0; i < specialStrEN.length; i++) {
            if (str.indexOf(specialStrEN[i]) != -1) {
                return true;
            }
        }
        for (var i = 0; i < specialStrCN.length; i++) {
            if (str.indexOf(specialStrCN[i]) != -1) {
                return true;
            }
        }
        return false;
    };
    //电话号码验证
    string.fn.isPhone = function() {
        /*
		  var reg = /^(\d{3,4})-(\d{7,8})/;
	    if( str.constructor === String ){
	       var re = str.match( reg );
	       return true;
	   }
	   return false;
   */
        this.isNumber(this) ? true: false;
    };
    //验证是否为手机号
    string.fn.isTel = function() {
        var tel = /^[0-9]{11}$/;
        if (tel.test(this)) {
            return true;
        } else {
            return false;
        }

    };
    //验证邮箱是否合法
    string.fn.isMail = function() {
		///^[-_A-Za-z0-9]+@([_A-Za-z0-9]+\.)+[A-Za-z0-9]{2,3}$/
        return (new RegExp(/^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$/).test(this));
    };
    //身份号验证
    string.fn.isIDNumber = function() {
        return (new RegExp(/^\d{15}|\d{17}[A-Z]$/).test(this));
    };
    //字母下画线和数字 并且字母开头 3 - 16位
    string.fn.isUserName = function() {
        var pattern = /^[a-zA-Z][a-zA-Z0-9_]{1,14}[a-zA-Z0-9]$/i; // 用户名只能是字母下画线和数字 并且字母开头 3 - 16位
        if (pattern.test(this)) {
            return true;
        } else {
            return false;
        }
    };
    // 末字是否有下划线	
    string.fn.isLastLine = function() {
        var patternLastChar = /^[a-zA-Z0-9_]{1,15}_$/i; // 末字符不能是下划线
        if (patternLastChar.test(this)) {
            return true;
        } else {
            return false;
        }
    };
    // 开头字符必须为字母
    string.fn.isFirstLetter = function() {
        var patternFirstChar = /^[0-9_][a-zA-Z0-9_]{1,14}$/i; // 开头字符必须为字母
        if (patternFirstChar.test(this)) {
            return true;
        } else {
            return false;
        }
    };
    /*
	 用途：校验ip地址的格式
	 输入：strIP：ip地址
	 返回：如果通过验证返回true,否则返回false；
	 */
    string.fn.isIP=function() {
        if (isNull(this)) return false;
        var re = /^(\d+)\.(\d+)\.(\d+)\.(\d+)$/g //匹配IP地址的正则表达式 
        if (re.test(this)) {
            if (RegExp.$1 < 256 && RegExp.$2 < 256 && RegExp.$3 < 256 && RegExp.$4 < 256) return true;
        }
        return false;
    };

    /* 
	 用途：检查输入对象的值是否符合整数格式
	 输入：str 输入的字符串
	 返回：如果通过验证返回true,否则返回false
	 */
    string.fn.isInteger=function() {
        var regu = /^[-]{0,1}[0-9]{1,}$/;
        return regu.test(this);
    };
    /* 
	 用途：检查输入字符串是否符合正整数格式
	 输入：
	 s：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 */
    string.fn.isNumber=function() {
        var regu = "^[0-9]+$";
        var re = new RegExp(regu);
        if (this.search(re) != -1) {
            return true;
        } else {
            return false;
        }
    };

    /* 
	 用途：检查输入字符串是否是带小数的数字格式,可以是负数
	 输入：
	 s：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 */
    string.fn.isDecimal = function() {
        if (isInteger(this)) return true;
        var re = /^[-]{0,1}(\d+)[\.]+(\d+)$/;
        if (re.test(this)) {
            if (RegExp.$1 == 0 && RegExp.$2 == 0) return false;
            return true;
        } else {
            return false;
        }
    };
    /* 
	 用途：检查输入对象的值是否符合端口号格式
	 输入：str 输入的字符串
	 返回：如果通过验证返回true,否则返回false
	 */
    string.fn.isPort = function() {
        return (isNumber(this) && str < 65536);
    },

    /* 
	 用途：检查输入字符串是否符合金额格式
	 格式定义为带小数的正数，小数点后最多三位
	 输入：
	 s：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 */
    string.fn.isMoney=function() {
        var regu = "^[0-9]+[\.][0-9]{0,3}$";
        var re = new RegExp(regu);
        if (re.test(this)) {
            return true;
        } else {
            return false;
        }
    };
    /* 
	 用途：检查输入字符串是否只由英文字母和数字和下划线组成
	 输入：
	 s：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 */
    string.fn.isNumberOr_Letter=function() { 
        var regu = "^[0-9a-zA-Z\_]+$";
        var re = new RegExp(regu);
        if (re.test(this)) {
            return true;
        } else {
            return false;
        }
    };
    /* 
	 用途：检查输入字符串是否只由英文字母和数字组成
	 输入：
	 s：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 */
    string.fn.isNumberOrLetter=function() { //判断是否是数字或字母 
        var regu = "^[0-9a-zA-Z]+$";
        var re = new RegExp(regu);
        if (re.test(this)) {
            return true;
        } else {
            return false;
        }
    };
    /* 
	 用途：检查输入字符串是否只由汉字、字母、数字组成
	 输入：
	 ＆#118alue：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 */
     string.fn.isChinaOrNumbOrLett = function() { //判断是否是汉字、字母、数字组成 
        var regu = "^[0-9a-zA-Z\u4e00-\u9fa5]+$";
        var re = new RegExp(regu);
        if (re.test(this)) {
            return true;
        } else {
            return false;
        }
    };
    /* 
	 用途：判断是否是日期
	 输入：date：日期；fmt：日期格式
	 返回：如果通过验证返回true,否则返回false
	 */
    string.fn.isDate=function(fmt) {
        if (fmt == null) fmt = "yyyyMMdd";
        var yIndex = fmt.indexOf("yyyy");
        if (yIndex == -1) return false;
        var year = this.substring(yIndex, yIndex + 4);
        var mIndex = fmt.indexOf("MM");
        if (mIndex == -1) return false;
        var month = this.substring(mIndex, mIndex + 2);
        var dIndex = fmt.indexOf("dd");
        if (dIndex == -1) return false;
        var day = this.substring(dIndex, dIndex + 2);
        if (!isNumber(year) || year > "2100" || year < "1900") return false;
        if (!isNumber(month) || month > "12" || month < "01") return false;
        if (day > getMaxDay(year, month) || day < "01") return false;
        return true;
    };

    string.fn.getMaxDay= function(year, month) {
        if (month == 4 || month == 6 || month == 9 || month == 11) return "30";
        if (month == 2) if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0) return "29";
        else return "28";
        return "31";
    };
    /* 
	 用途：检查输入的起止日期是否正确，规则为两个日期的格式正确，
	 且结束如期>=起始日期
	 输入：
	 startDate：起始日期，字符串
	 endDate：结束如期，字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 */
    string.fn.isBetweenDate= function(startDate, endDate) {
        if (!isDate(startDate)) {
           // alert("起始日期不正确!");
            return false;
        } else if (!isDate(endDate)) {
          //  alert("终止日期不正确!");
            return false;
        } else if (startDate > endDate) {
            //alert("起始日期不能大于终止日期!");
            return false;
        }
		else if(this > startDate && this < endDate)
		{
			return true;
		}
        return false;
    };

} (String))