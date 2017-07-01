/*
Date 对像的扩展
*/

(function(date)
    {
		date.fn=date.prototype;
		date.fn.getMaxDay=function(year, month) {
			if (month == 4 || month == 6 || month == 9 || month == 11) return "30";
			if (month == 2) if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0) return "29";
			else return "28";
			return "31";
	    };
	}
(Date))