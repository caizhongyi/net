if(typeof(czyjs.UI)=="undefined")
{
    czyjs.UI = {};
}

czyjs.UI.ComBoxDateTime = Class.create();
czyjs.UI.ComBoxDateTime.prototype={
	initialize: function(param){

	    this.selectYearObj =document.getElementById(param.selectYearId);
	    this.selectMonthObj = document.getElementById(param.selectMonthId);
	    this.selectDayObj = document.getElementById(param.selectDayId);
        this.GetYear();
		
		 if(this.selectMonthObj!=null)
		 {
		 	 this.GetMonth();
			addEventHandler(this.selectYearObj,"change",BindAsEventListener(this,
               function(){
			   	this.GetMonth();
			   	if(this.selectMonthObj.options[0].text==1)
				{
					 this.GetDay();
				}
			   	
			   }
			));
		 }
		 if(this.selectDayObj!=null)
		 {
		 	 this.GetDay();
		    	addEventHandler(this.selectMonthObj,"change",BindAsEventListener(this,
              this.GetDay
			));
		 	
		 }
	
	},
//获取年月日
GetYear:function()
{   
    for(var i=(new Date().getYear()-200);i<= (new Date().getYear());i++)
    {
      var NewOption = new Option(i,i);
      this.selectYearObj.options.add(NewOption);
    }
   
},


GetMonth:function ()
{
    this.selectMonthObj.length=0;
    for(var i=1;i<=12;i++)
    {
       var NewOption = new Option(i,i);
       this.selectMonthObj.options.add(NewOption);
    }
},

GetDay:function ()
{
    this.selectDayObj.length=0;
    if(this.selectMonthObj.value==2) //判断是否为2月
    {
        if(this.selectYearObj.value%400==0 || (this.selectYearObj.value%100!=0 &&this.selectYearObj.value%4==0))
        {   
            for(var i=1;i<=28;i++)
            {
               var NewOption = new Option(i,i);
               this.selectDayObj.options.add(NewOption);
            }
        }
        else
        {   
            
            for(var i=1;i<=29;i++)
            {
               var NewOption = new Option(i,i);
               this.selectDayObj.options.add(NewOption);
            }
        }
    }
    else if((this.selectMonthObj.value<=7 && this.selectMonthObj.value!=2 && this.selectMonthObj.value %2 !=0 ) || (this.selectMonthObj.value>7 && this.selectMonthObj.value %2==0 ))//判断是否为31天
    {
           for(var i=1;i<=31;i++)
           {
               var NewOption = new Option(i,i);
               this.selectDayObj.options.add(NewOption);
           }
    }
    else
    {
           for(var i=1;i<=30;i++)
           {
               var NewOption = new Option(i,i);
               this.selectDayObj.options.add(NewOption);
           }
    }
}
}