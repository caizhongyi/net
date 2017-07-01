
var DataTimeClock = Class.create();
DataTimeClock.prototype = {
	initialize: function(param){
        var json=eval(param);
        this.contenterObj=json.ContenterObj;
        
        this.DateTimeFun= BindAsEventListener(this,this.ResponseDateTime);
        this.GetDateFun= BindAsEventListener(this,this.GetDay);
        this.DateTimeFun();
    },
    
    ResponseDateTime:function()
    {
        var myDate = new Date();   
        this.year=  myDate.getYear();       //获取当前年份(2位)   
        this.fullYear = myDate.getFullYear();   //获取完整的年份(4位,1970-????)   
        this.month = myDate.getMonth();       //获取当前月份(0-11,0代表1月)   
        this.date = myDate.getDate();       //获取当前日(1-31)   
        this.day = myDate.getDay();         //获取当前星期X(0-6,0代表星期天)   
        this.time = myDate.getTime();       //获取当前时间(从1970.1.1开始的毫秒数)   
        this.hour = myDate.getHours();       //获取当前小时数(0-23)   
        this.min = myDate.getMinutes();     //获取当前分钟数(0-59)   
        this.sec = myDate.getSeconds();     //获取当前秒数(0-59)   
        this.millisec = myDate.getMilliseconds();   //获取当前毫秒数(0-999)   
        this.localDate = myDate.toLocaleDateString();     //获取当前日期   
        this.localTime=myDate.toLocaleTimeString();     //获取当前时间   
        this.mydate = myDate.toLocaleString( );       //获取日期与时间   
        
        this.contenterObj.innerHTML ="<span> 时间 : </span><span>"+this.mydate+"</span>  "+this.GetDateFun();
    
        setTimeout(this.DateTimeFun,1000);
    },
    
    GetDay:function ()
    {
        switch (this.day)
        {
          case 0:return "星期天";
          case 1:return "星期一";
          case 2:return "星期二";
          case 3:return "星期三";
          case 4:return "星期四";
          case 5:return "星期五";
          case 6:return "星期六";
        }
    }
    
    
}



