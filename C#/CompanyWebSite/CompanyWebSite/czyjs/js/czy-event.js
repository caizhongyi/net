if(typeof(czyjs.Event)=="undefined")
{
    czyjs.Event = {};
}
//滚轮事件
//addEventHandler(o, isIE ? "mousewheel" : "DOMMouseScroll", BindAsEventListener(this, this.WheelCtrl));  
//var i = this.WheelSpeed * e.detail;   
//this.SetPos(this.Bar.offsetLeft + i, this.Bar.offsetTop + i);  
//方向键事件
//addEventHandler(o, "keydown", BindAsEventListener(this, this.KeyCtrl)); 
/*
switch (e.keyCode) {   
    case 37 ://左   
        iLeft -= iWidth; break;   
    case 38 ://上   
        iTop -= iHeight; break;   
   case 39 ://右   
        iLeft += iWidth; break;   
    case 40 ://下   
        iTop += iHeight; break;   
    default: break;
		//不是方向按键返回   
}  
*/


//document加入事件`获取事件对像
czyjs.Event.GetEventObj=function()
{
	
    var obj;
	if(!document.all)
	{
		obj= arguments.callee.caller.arguments[0].target;
        return obj;	
	}
	else
	{
		obj=event.srcElement;
		return obj;
	}
}
czyjs.Event.GetEvent=function()
{
	return document.all?event: arguments.callee.caller.arguments[0];
}


////事件触发对象 
czyjs.Event.GetSrcElement=function()
{
	obj=event.srcElement;     //事件触发对象 
	obj.setCapture();         //设置属于当前对象的鼠标捕捉 
	return obj;
}
//释放当前对象的鼠标捕捉 
czyjs.Event.ReleaseCapture=function(obj)
{
	obj.releaseCapture(); //释放当前对象的鼠标捕捉 
}
//event.wheelData  120为up，-120为down