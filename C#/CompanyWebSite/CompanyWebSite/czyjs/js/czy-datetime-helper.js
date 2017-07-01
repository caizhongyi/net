/*******************
 *DateTimeHelper   
 *时间帮助类
 ******************/
if(typeof(czyjs.Time)=="undefined")
{
    czyjs.Time = {};
}

/*
方法 描述 FF IE 
Date() 返回当日的日期和时间。 1 3 
getDate() 从 Date 对象返回一个月中的某一天 (1 ~ 31)。 1 3 
getDay() 从 Date 对象返回一周中的某一天 (0 ~ 6)。 1 3 
getMonth() 从 Date 对象返回月份 (0 ~ 11)。 1 3 
getFullYear() 从 Date 对象以四位数字返回年份。 1 4 
getYear() 请使用 getFullYear() 方法代替。 1 3 
getHours() 返回 Date 对象的小时 (0 ~ 23)。 1 3 
getMinutes() 返回 Date 对象的分钟 (0 ~ 59)。 1 3 
getSeconds() 返回 Date 对象的秒数 (0 ~ 59)。 1 3 
getMilliseconds() 返回 Date 对象的毫秒(0 ~ 999)。 1 4 
getTime() 返回 1970 年 1 月 1 日至今的毫秒数。 1 3 
getTimezoneOffset() 返回本地时间与格林威治标准时间 (GMT) 的分钟差。 1 3 
getUTCDate() 根据世界时从 Date 对象返回月中的一天 (1 ~ 31)。 1 4 
getUTCDay() 根据世界时从 Date 对象返回周中的一天 (0 ~ 6)。 1 4 
getUTCMonth() 根据世界时从 Date 对象返回月份 (0 ~ 11)。 1 4 
getUTCFullYear() 根据世界时从 Date 对象返回四位数的年份。 1 4 
getUTCHours() 根据世界时返回 Date 对象的小时 (0 ~ 23)。 1 4 
getUTCMinutes() 根据世界时返回 Date 对象的分钟 (0 ~ 59)。 1 4 
getUTCSeconds() 根据世界时返回 Date 对象的秒钟 (0 ~ 59)。 1 4 
getUTCMilliseconds() 根据世界时返回 Date 对象的毫秒(0 ~ 999)。 1 4 
parse() 返回1970年1月1日午夜到指定日期（字符串）的毫秒数。 1 3 
setDate() 设置 Date 对象中月的某一天 (1 ~ 31)。 1 3 
setMonth() 设置 Date 对象中月份 (0 ~ 11)。 1 3 
setFullYear() 设置 Date 对象中的年份（四位数字）。 1 4 
setYear() 请使用 setFullYear() 方法代替。 1 3 
setHours() 设置 Date 对象中的小时 (0 ~ 23)。 1 3 
setMinutes() 设置 Date 对象中的分钟 (0 ~ 59)。 1 3 
setSeconds() 设置 Date 对象中的秒钟 (0 ~ 59)。 1 3 
setMilliseconds() 设置 Date 对象中的毫秒 (0 ~ 999)。 1 4 
setTime() 以毫秒设置 Date 对象。 1 3 
setUTCDate() 根据世界时设置 Date 对象中月份的一天 (1 ~ 31)。 1 4 
setUTCMonth() 根据世界时设置 Date 对象中的月份 (0 ~ 11)。 1 4 
setUTCFullYear() 根据世界时设置 Date 对象中的年份（四位数字）。 1 4 
setUTCHours() 根据世界时设置 Date 对象中的小时 (0 ~ 23)。 1 4 
setUTCMinutes() 根据世界时设置 Date 对象中的分钟 (0 ~ 59)。 1 4 
setUTCSeconds() 根据世界时设置 Date 对象中的秒钟 (0 ~ 59)。 1 4 
setUTCMilliseconds() 根据世界时设置 Date 对象中的毫秒 (0 ~ 999)。 1 4 
toSource() 返回该对象的源代码。 1 - 
toString() 把 Date 对象转换为字符串。 1 4 
toTimeString() 把 Date 对象的时间部分转换为字符串。 1 4 
toDateString() 把 Date 对象的日期部分转换为字符串。 1 4 
toGMTString() 请使用 toUTCString() 方法代替。 1 3 
toUTCString() 根据世界时，把 Date 对象转换为字符串。 1 4 
toLocaleString() 根据本地时间格式，把 Date 对象转换为字符串。 1 3 
toLocaleTimeString() 根据本地时间格式，把 Date 对象的时间部分转换为字符串。 1 3 
toLocaleDateString() 根据本地时间格式，把 Date 对象的日期部分转换为字符串。 1 3 
UTC() 根据世界时返回 1997 年 1 月 1 日 到指定日期的毫秒数。 1 3 
valueOf() 返回 Date 对象的原始值。 1 4 

*/


/*******************
*字符转化时间格式 
*datestr:时间字符窜
*返回datetime是:Wed Mar 04 2009 11:05:05 GMT+0800格式  得到结果：2009-06-12 17:18:05
******************/
czyjs.Time.ToDate = function (datestr) {
 	var dateArray = new Array();
 	var yearArray = new Array();
 	var timeArray = new Array();
 	
 	dateArray = datestr.split(" ");
 	
 	yearArray = dateArray[0].split('-');
 	timeArray = dateArray[1].split(':');
 	
 	
 	var datetime = new Date(yearArray[0], yearArray[1], yearArray[2], timeArray[0], timeArray[1], timeArray[2]);
 	return datetime;
 }
/*******************
 *datetime:时间格式   
 *format:转化的格式[yyyy-MM-dd]
 ******************/
czyjs.Time.ToDateFormat = function (datetime, format) {

	var year = datetime.getFullYear();
	var month = datetime.getMonth() + 1;//js从0开始取 
	var date = datetime.getDate();
	var hour = datetime.getHours();
	var minutes = datetime.getMinutes();
	var second = datetime.getSeconds();
	
	if (month < 10) {
		month = "0" + month;
	}
	if (date < 10) {
		date = "0" + date;
	}
	if (hour < 10) {
		hour = "0" + hour;
	}
	if (minutes < 10) {
		minutes = "0" + minutes;
	}
	if (second < 10) {
		second = "0" + second;
	}
	
	switch (format) {
		case "yyyy-MM-dd":
			return year + "-" + month + "-" + date;
		case "yyyy-MM-dd hh:mm:ss":
			return year + "-" + month + "-" + date + " " + hour + ":" + minutes + ":" + second;
		case "yyyy/MM/dd":
			return year + "/" + month + "/" + date;
		case "yyyy/MM/dd hh:mm:ss":
			return year + "/" + month + "/" + date + " " + hour + ":" + minutes + ":" + second;
		case "yyyy年MM月dd日":
			return year + "年" + month + "月" + date + "日";
		case "yyyy年MM月dd日 hh时mm分ss秒":
			return year + "年" + month + "月" + date + "日 " + hour + "时" + minutes + "分" + second + "秒";
		case "yy-MM-dd":
			return year.toString().substring(2) + "-" + month + "-" + date;
		case "yy-MM-dd hh:mm:ss":
			return year.toString().substring(2) + "-" + month + "-" + date + " " + hour + ":" + minutes + ":" + second;
		case "yy/MM/dd":
			return year.toString().substring(2) + "/" + month + "/" + date;
		case "yy/MM/dd hh:mm:ss":
			return year.toString().substring(2) + "/" + month + "/" + date + " " + hour + ":" + minutes + ":" + second;
		case "yy年MM月dd日":
			return year.toString().substring(2) + "年" + month + "月" + date + "日";
		case "yy年MM月dd日 hh时mm分ss秒":
			return year.toString().substring(2) + "年" + month + "月" + date + "日 " + hour + "时" + minutes + "分" + second + "秒";
		default:
			return year + "-" + month + "-" + date + " " + hour + ":" + minutes + ":" + second;
	}
	
}


/*******************
 * 时间格式转换
 * 为Date类增加format方法
 * format:yyyy-MM-dd
 ******************/
Date.prototype.format = function(format){
	var o = {
		"M+": this.getMonth() + 1, //month 
		"d+": this.getDate(), //day 
		"h+": this.getHours(), //hour 
		"m+": this.getMinutes(), //minute 
		"s+": this.getSeconds(), //second 
		"q+": Math.floor((this.getMonth() + 3) / 3), //quarter 
		"S": this.getMilliseconds() //millisecond 
	}
	if (/(y+)/.test(format)) 
		format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
	for (var k in o) 
		if (new RegExp("(" + k + ")").test(format)) 
			format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
	return format;
}


  
/**   
*功能:格式化时间   
*示例:DateUtil.Format("yyyy/MM/dd","Thu Nov 9 20:30:37 UTC+0800 2006 ");   
*返回:2006/11/09   
*/
czyjs.Time.Format = function (fmtCode, date) {    
    var result,d,arr_d;    
        
    var patrn_now_1=/^y{4}-M{2}-d{2}\sh{2}:m{2}:s{2}$/;    
    var patrn_now_11=/^y{4}-M{1,2}-d{1,2}\sh{1,2}:m{1,2}:s{1,2}$/;    
        
    var patrn_now_2=/^y{4}\/M{2}\/d{2}\sh{2}:m{2}:s{2}$/;    
    var patrn_now_22=/^y{4}\/M{1,2}\/d{1,2}\sh{1,2}:m{1,2}:s{1,2}$/;    
        
    var patrn_now_3=/^y{4}年M{2}月d{2}日\sh{2}时m{2}分s{2}秒$/;    
    var patrn_now_33=/^y{4}年M{1,2}月d{1,2}日\sh{1,2}时m{1,2}分s{1,2}秒$/;    
        
    var patrn_date_1=/^y{4}-M{2}-d{2}$/;    
    var patrn_date_11=/^y{4}-M{1,2}-d{1,2}$/;    
        
    var patrn_date_2=/^y{4}\/M{2}\/d{2}$/;    
    var patrn_date_22=/^y{4}\/M{1,2}\/d{1,2}$/;    
        
    var patrn_date_3=/^y{4}年M{2}月d{2}日$/;    
    var patrn_date_33=/^y{4}年M{1,2}月d{1,2}日$/;    
        
    var patrn_time_1=/^h{2}:m{2}:s{2}$/;    
    var patrn_time_11=/^h{1,2}:m{1,2}:s{1,2}$/;    
    var patrn_time_2=/^h{2}时m{2}分s{2}秒$/;    
    var patrn_time_22=/^h{1,2}时m{1,2}分s{1,2}秒$/;    
        
    if(!fmtCode){fmtCode="yyyy/MM/dd hh:mm:ss";}    
    if(date){    
        d=new Date(date);    
        if(isNaN(d)){    
            msgBox("时间参数非法\n正确的时间示例:\nThu Nov 9 20:30:37 UTC+0800 2006\n或\n2006/       10/17");    
            return;}    
    }else{    
        d=new Date();    
    }    
   
    if(patrn_now_1.test(fmtCode))    
    {    
        arr_d=splitDate(d,true);    
        result=arr_d.yyyy+"-"+arr_d.MM+"-"+arr_d.dd+" "+arr_d.hh+":"+arr_d.mm+":"+arr_d.ss;    
    }    
    else if(patrn_now_11.test(fmtCode))    
    {    
        arr_d=splitDate(d);    
        result=arr_d.yyyy+"-"+arr_d.MM+"-"+arr_d.dd+" "+arr_d.hh+":"+arr_d.mm+":"+arr_d.ss;    
    }    
    else if(patrn_now_2.test(fmtCode))    
    {    
        arr_d=splitDate(d,true);    
        result=arr_d.yyyy+"/"+arr_d.MM+"/"+arr_d.dd+" "+arr_d.hh+":"+arr_d.mm+":"+arr_d.ss;    
    }    
    else if(patrn_now_22.test(fmtCode))    
    {    
        arr_d=splitDate(d);    
        result=arr_d.yyyy+"/"+arr_d.MM+"/"+arr_d.dd+" "+arr_d.hh+":"+arr_d.mm+":"+arr_d.ss;    
    }    
    else if(patrn_now_3.test(fmtCode))    
    {    
        arr_d=splitDate(d,true);    
        result=arr_d.yyyy+"年"+arr_d.MM+"月"+arr_d.dd+"日"+" "+arr_d.hh+"时"+arr_d.mm+"分"+arr_d.ss+"秒";    
    }    
    else if(patrn_now_33.test(fmtCode))    
    {    
        arr_d=splitDate(d);    
        result=arr_d.yyyy+"年"+arr_d.MM+"月"+arr_d.dd+"日"+" "+arr_d.hh+"时"+arr_d.mm+"分"+arr_d.ss+"秒";    
    }    
        
    else if(patrn_date_1.test(fmtCode))    
    {    
        arr_d=splitDate(d,true);    
        result=arr_d.yyyy+"-"+arr_d.MM+"-"+arr_d.dd;    
    }    
    else if(patrn_date_11.test(fmtCode))    
    {    
        arr_d=splitDate(d);    
        result=arr_d.yyyy+"-"+arr_d.MM+"-"+arr_d.dd;    
    }    
    else if(patrn_date_2.test(fmtCode))    
    {    
        arr_d=splitDate(d,true);    
        result=arr_d.yyyy+"/"+arr_d.MM+"/"+arr_d.dd;    
    }    
    else if(patrn_date_22.test(fmtCode))    
    {    
        arr_d=splitDate(d);    
        result=arr_d.yyyy+"/"+arr_d.MM+"/"+arr_d.dd;    
    }    
    else if(patrn_date_3.test(fmtCode))    
    {    
        arr_d=splitDate(d,true);    
        result=arr_d.yyyy+"年"+arr_d.MM+"月"+arr_d.dd+"日";    
    }    
    else if(patrn_date_33.test(fmtCode))    
    {    
        arr_d=splitDate(d);    
        result=arr_d.yyyy+"年"+arr_d.MM+"月"+arr_d.dd+"日";    
    }    
    else if(patrn_time_1.test(fmtCode)){    
        arr_d=splitDate(d,true);    
        result=arr_d.hh+":"+arr_d.mm+":"+arr_d.ss;    
    }    
    else if(patrn_time_11.test(fmtCode)){    
        arr_d=splitDate(d);    
        result=arr_d.hh+":"+arr_d.mm+":"+arr_d.ss;    
    }    
    else if(patrn_time_2.test(fmtCode)){    
        arr_d=splitDate(d,true);    
        result=arr_d.hh+"时"+arr_d.mm+"分"+arr_d.ss+"秒";    
    }    
    else if(patrn_time_22.test(fmtCode)){    
        arr_d=splitDate(d);    
        result=arr_d.hh+"时"+arr_d.mm+"分"+arr_d.ss+"秒";    
    }    
    else{    
        msgBox("没有匹配的时间格式!");    
        return;    
    }    
        
   return result;    
};    

function splitDate(d,isZero){    
    var yyyy,MM,dd,hh,mm,ss;    
    if(isZero){    
         yyyy=d.getYear();    
         MM=(d.getMonth()+1)<10?"0"+(d.getMonth()+1):d.getMonth()+1;    
         dd=d.getDate()<10?"0"+d.getDate():d.getDate();    
         hh=d.getHours()<10?"0"+d.getHours():d.getHours();    
         mm=d.getMinutes()<10?"0"+d.getMinutes():d.getMinutes();    
         ss=d.getSeconds()<10?"0"+d.getSeconds():d.getSeconds();    
    }else{    
         yyyy=d.getYear();    
         MM=d.getMonth()+1;    
         dd=d.getDate();    
         hh=d.getHours();    
         mm=d.getMinutes();    
         ss=d.getSeconds();      
    }    
    return {"yyyy":yyyy,"MM":MM,"dd":dd,"hh":hh,"mm":mm,"ss":ss};      
}    



    


