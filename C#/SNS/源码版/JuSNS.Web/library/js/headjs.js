function getEvent()
{
if(document.all)    return window.event;        
     func=getEvent.caller;            
     while(func!=null){    
         var arg0=func.arguments[0];
         if(arg0){
             if((arg0.constructor==Event || arg0.constructor ==MouseEvent)
                 || (typeof(arg0)=="object" && arg0.preventDefault && arg0.stopPropagation)){    
                 return arg0;
             }
         }
         func=func.caller;
     }
     return null;
}
 
window.onload=function()
{
    var cuObj=null;
    var yw=48;
	var yh=48;
    var form=document.forms[0];
	var img=document.getElementById("controlImg");
	var zhezhao=document.getElementById("zhezhaoceng");
	var headk=document.getElementById("headk");
	var yulan=document.getElementById("yulan");
	yulan.style.margin=0+"px";
	yulan.style.padding=0+"px";
	yulan.style.height=yh+"px";
	yulan.style.width=yw+"px";
	yulan.style.overflow="hidden";
	var ydiv=document.createElement("div");
	ydiv.style.position="relative";
	ydiv.style.margin=0+"px";
	ydiv.style.padding=0+"px";
	ydiv.style.top=0+"px";
	ydiv.style.left=0+"px"
	ydiv.style.height=yh+"px";
	ydiv.style.width=yw+"px";
	ydiv.style.overflow="hidden";
	var yimg=document.createElement("img");
	yimg.style.position="relative";
	yimg.style.top=0+"px";
	yimg.style.left=0+"px";
	yimg.src=img.src;
	yulan.appendChild(ydiv);
	ydiv.appendChild(yimg);
	
	if(zhezhao)
	{
		document.body.removeChild(zhezhao);
	}
	if(headk)
	{
		document.body.removeChild(headk);
	}
	var w=100;
	var h=100;
	
	var event=getEvent();
	var XY=getXY(img);
	var zhezhao=document.getElementById("zhezhaoceng");
	if(!zhezhao)
	{
		zhezhao=document.createElement("div");
		zhezhao.setAttribute("id","zhezhaoceng");
		document.body.appendChild(zhezhao);
	}
	zhezhao.className="zhezhaoceng";
	zhezhao.style.cursor="crosshair";
	zhezhao.style.zIndex="2";
	zhezhao.style.left=XY.X+"px";
	zhezhao.style.top=XY.Y+"px";
	zhezhao.style.width=img.width+"px";
	zhezhao.style.height=img.height+"px";
	if(img.width<w||img.height<h)
	{
	    if(img.width>img.height)
	    {
		    w=parseInt(img.height);
		    h=w;
	    }
	    if(img.height>img.width)
	    {
		    w=parseInt(img.width);
		    h=w;
	    }
	}
	var k=document.getElementById("headk");
	if(!k)
	{
		k=document.createElement("div");
		k.setAttribute("id","headk");
		k.setAttribute("moveable","false");
		document.body.appendChild(k);
	}
	var scrollTop=document.body.scrollTop;
	if(scrollTop==0)
	{
		scrollTop=document.documentElement.scrollTop;
	}
	var x0=XY.X+(img.width-w)/2;
	var y0=XY.Y+(img.height-h)/2;
	var returnX=x0-XY.X;
	var returnY=y0-XY.Y;
	movekk(returnX,returnY,x0,y0);
	/*firefox end*/
	zhezhao.onclick=img.onclick;		
	
	k.onmousedown=function()
	{
		k.setAttribute("moveable","true");
	}
	k.onmouseup=function()
	{
	    k.setAttribute("moveable","false");
	}
	k.onmousemove=mmove;
	function mmove()
	{
		var event=getEvent();
		if(!eval(k.getAttribute("moveable")))
		{
			return;
		}
		var scrollTop=document.body.scrollTop;
		if(scrollTop==0)
		{
			scrollTop=document.documentElement.scrollTop;
		}
		var x0=(event.clientX-w/2);
		var y0=(event.clientY+scrollTop-h/2);
		var returnX=x0-XY.X;
		var returnY=y0-XY.Y;
		if(x0>(XY.X+img.width-w))
		{
			x0=(XY.X+img.width-w);
		}
		if(y0>(XY.Y+img.height-h))
		{
			y0=(XY.Y+img.height-h);
		}
		if(x0<XY.X)
		{
			x0=XY.X;
		}
		if(y0<XY.Y)
		{
			y0=XY.Y;
		}
		var returnX=x0-XY.X;
		var returnY=y0-XY.Y;
		movekk(returnX,returnY,x0,y0);
	}
    var ex=0;
    var ey=0;
	function movebar(x,y,kw,kh,returnX,returnY)
	{
	    var cw=8;
	    var a=new Array();
	    for(var i=0;i<4;i++)
	    {
	        var id="imghander"+i;
	        var obj=document.getElementById(id);
	        if(obj)
	        {
	            a[a.length]=obj;	            
	        }
	        else
	        {
	            obj=document.createElement("div");
	            obj.setAttribute("id",id);
	            obj.setAttribute("moveable","false");
	            switch(i)
	            {
	                case 0:
	                    obj.style.cursor="nw-resize";
	                    break;
	                case 1:
	                    obj.style.cursor="ne-resize";
	                    break;
	                case 2:
	                    obj.style.cursor="sw-resize";
	                    break;
	                case 3:
	                    obj.style.cursor="se-resize";
	                    break;
	            }
	            document.body.appendChild(obj);
	            a[a.length]=obj;	           
	        }	        
	        obj.style.position="absolute";
	        obj.style.zIndex="100";
	        obj.style.width=cw+"px";
	        obj.style.height=cw+"px";
	        obj.innerHTML="";
	        obj.style.color="red";
	        obj.style.backgroundColor="red";	        
	    }
	    a[0].style.left=x+"px";
	    a[0].style.top=y+"px";
	    a[1].style.left=x+kw-cw+4+"px";
	    a[1].style.top=y+"px";
	    a[2].style.left=x+"px";
	    a[2].style.top=y+kh-cw+4+"px";
	    a[3].style.left=x+kw-cw+4+"px";
	    a[3].style.top=y+kh-cw+4+"px";
	    var obj=a[0];
	    var mx=0;
	    var my=0;
	    var kkw=kw;
	    var kkh=kh;
	    var rx=returnX;
	    var ry=returnY;
	    var xx=x;
	    var yy=y;
	    obj.onmousedown=function()
	    {
	        var event=getEvent();
	        ex=event.clientX;
	        ey=event.clientY; 
	        mx=0;
	        my=0;
	        cuObj=obj;
	        obj.setAttribute("moveable","true");
	        loadEvent(0);           
	    }
	    
	    function move0()
	    {	        
	        var event=getEvent();
	              
	        if(!eval(cuObj.getAttribute("moveable")))
	        {
	            return;
	        } 
	        mx=event.clientX-ex;
	        my=event.clientY-ey;
	        kkw+=(0-mx);
	        kkh+=(0-my); 
	        rx+=mx;
	        ry+=my;
	        xx+=mx;  
	        yy+=my; 
	        w=kkw;
	        h=kkh;	        	    
	        movekk(rx,ry,xx,yy);
	        ex=event.clientX;
	        ey=event.clientY;	        
	    }	    
	    var obj=a[1];	   
	    obj.onmousedown=function()
	    {
	        var event=getEvent();
	        ex=event.clientX;
	        ey=event.clientY;	       
	        mx=0;
	        my=0;
	        cuObj=obj;
	        obj.setAttribute("moveable","true");
	        loadEvent(1);
	    }
	    function move1()
	    {
	        var event=getEvent();	       
	        if(!eval(cuObj.getAttribute("moveable")))
	        {
	            return;
	        }
	              
	        mx=event.clientX-ex;
	        my=event.clientY-ey;
	        kkw+=(mx);
	        kkh+=(0-my);
	        yy+=my; 
	        w=kkw;
	        h=kkh;	        	    
	        movekk(rx,ry,xx,yy)
	        ex=event.clientX;
	        ey=event.clientY;	        
	    }	    
	    var obj=a[2];	    
	    obj.onmousedown=function()
	    {
	        var event=getEvent();
	        ex=event.clientX;
	        ey=event.clientY;	       
	        mx=0;
	        my=0;
	        cuObj=obj;
	        obj.setAttribute("moveable","true");
	        loadEvent(2);
	    }
	    function move2()
	    {
	        var event=getEvent();	       
	        if(!eval(cuObj.getAttribute("moveable")))
	        {
	            return;
	        }
	              
	        mx=event.clientX-ex;
	        my=event.clientY-ey;
	        kkw+=(0-mx);
	        kkh+=(my);
	        xx+=(mx);
	        w=kkw;
	        h=kkh;	        	    
	        movekk(rx,ry,xx,yy)
	        ex=event.clientX;
	        ey=event.clientY;	        
	    }
	    var obj=a[3];
	    obj.onmousedown=function()
	    {
	        var event=getEvent();
	        ex=event.clientX;
	        ey=event.clientY;	       
	        mx=0;
	        my=0;
	        cuObj=obj;
	        obj.setAttribute("moveable","true");
	        loadEvent(3);
	    }
	    function move3()
	    {
	        var event=getEvent();	       
	        if(!eval(cuObj.getAttribute("moveable")))
	        {
	            return;
	        }	              
	        mx=event.clientX-ex;
	        my=event.clientY-ey;
	        kkw+=(mx);
	        kkh+=(my);
	        w=kkw;
	        h=kkh;	        	    
	        movekk(rx,ry,xx,yy)
	        ex=event.clientX;
	        ey=event.clientY;	        
	    }
	    
	    function loadEvent(n)
	    {	        
	        for(var i=0;i<4;i++)
	        {
	            var obj=document.getElementById("imghander"+i);
	            if(i!=n)
	            {	                
                    if (window.attachEvent)
                    {
                        img.detachEvent("onmousemove",eval("move"+i)); 
                        obj.detachEvent("onmousemove",eval("move"+i)); 
                        k.detachEvent("onmousemove",eval("move"+i)); 
                    }
                    if (window.addEventListener)
                    {
                        img.removeEventListener("mousemove",eval("move"+i),false);
                        obj.removeEventListener("mousemove",eval("move"+i),false);
                        k.removeEventListener("mousemove",eval("move"+i),false);
                    } 
	            }
	            else
	            {
	                if (window.attachEvent)
                    {
                        img.attachEvent("onmousemove",eval("move"+i)); 
                        obj.attachEvent("onmousemove",eval("move"+i)); 
                        k.attachEvent("onmousemove",eval("move"+i)); 
                    }
                    if (window.addEventListener)
                    {
                        img.addEventListener("mousemove",eval("move"+i),false);
                        obj.addEventListener("mousemove",eval("move"+i),false);
                        k.addEventListener("mousemove",eval("move"+i),false);
                    } 
	            }
	        }
	    }
	    if (window.attachEvent)
        {
            document.body.attachEvent("onmouseup",imgmouseup); 
            img.attachEvent("onmouseout",imgmouseup); 
        }
        if (window.addEventListener)
        {
            document.body.addEventListener("mouseup",imgmouseup,false);
            img.attachEvent("mouseout",imgmouseup); 
        } 
        function imgmouseup()
        {            
            for(var i=0;i<4;i++)
            {
                var obj=document.getElementById("imghander"+i);
                if (window.attachEvent)
                {
                    img.detachEvent("onmousemove",eval("move"+i)); 
                    obj.detachEvent("onmousemove",eval("move"+i)); 
                    k.detachEvent("onmousemove",eval("move"+i)); 
                }  
            }   
        }	    
	}
	function movekk(returnX,returnY,x0,y0)
	{
	    var ow=getSmall(img.width,img.height);
	    if(w>ow)
	    {
		    w=ow;
	    }
	    if(w<50)
	    {
		    w=50;
	    }
	    h=w;
		if(x0+2>(XY.X+img.width-w))
		{
			x0=(XY.X+img.width-w);
		}
		if(y0+2>(XY.Y+img.height-h))
		{
			y0=(XY.Y+img.height-h);
		}
		if(x0-2<XY.X)
		{
			x0=XY.X;
		}
		if(y0-2<XY.Y)
		{
			y0=XY.Y;
		}
		returnX=x0-XY.X;
		returnY=y0-XY.Y;	    
	    form.action="?x="+parseInt(returnX)+"&y="+parseInt(returnY)+"&w="+w;
		k.style.border="2px #ff0000 solid";
		k.style.left=x0+"px";
		k.style.top=y0+"px";
		k.style.width=w+"px";
		k.style.height=h+"px";
		k.style.position="absolute";
		k.style.zIndex="3";
		k.style.backgroundImage="url("+img.src+")";
		k.style.backgroundPositionX="-"+returnX+"px";
		k.style.backgroundPositionY="-"+returnY+"px";
		k.style.overflow="hidden";
		k.style.padding="0px 0px 0px 0px";
		k.style.cursor="move";
		yimg.width=img.width*yw/w;
	    yimg.height=img.height*yh/h;
	    yimg.style.top="-"+returnY*yw/w+"px";
	    yimg.style.left="-"+returnX*yh/h+"px";
	    movebar(x0,y0,w,h,returnX,returnY);
		/*firefox*/
		var baifenX=Math.round(returnX*100/(img.width-100))+"%";
		var baifenY=Math.round(returnY*100/(img.height-120))+"%";
		k.setAttribute("style","top:"+y0+"px;left:"+x0+"px;padding:0px;background-repeat:no-repeat;background-image:url("+img.src+");background-position:"+baifenX+" "+baifenY+"");
	}	
}
function check()
{ 
    var form=document.getElementById("uploadpicture");
    if(form.isselect.checked==true)
    {
        form.isselect.checked=false;
    }
    else
    {
        form.isselect.checked=true;
    }
}

function Counting(count)
{
    var event=getEvent();
    if (event.wheelDelta >= 120)
    count++;
    else if (event.wheelDelta <= -120)
    count--;
    return count;
} 

function getSmall(x,y)
{
    if(x>y)
    {
        return y;
    }
    else
    {
        return x;
    }
}
