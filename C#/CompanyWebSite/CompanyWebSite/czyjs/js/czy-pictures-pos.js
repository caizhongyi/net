// JavaScript Document
/*design by koen @ 10nian5yue8ri*/
/*http://hi.baidu.com/koen_li*/
/*
 * czyjs.UI
 */
if(typeof(czyjs.UI)=="undefined")
{
    czyjs.UI = {};
}

function $(id) {return document.getElementById(id);}
function $$(tag,id){return (id==undefined?document:this.$(id)).getElementsByTagName(tag);}

czyjs.UI.PicturesPos=Class.create();
czyjs.UI.PicturesPos.prototype = {

    initialize: function (param) {
    
        this.width=param.width;
        this.height=param.height==null?300:param.height;
        this.containObj=document.getElementById( param.randerTo)==null? param.randerTo:document.getElementById( param.randerTo);
        this.id=param.id;
        this.className=param.className;
        this.pics=param.pics;  //["images/pic0.jpg","images/pic1.jpg","images/pic2.jpg"]
        this.containObj.innerHTML=this.ResponseUI();
        
        this.currentNum=0;
        this.atuokey="";
        this.interval=3000;
        //var T=3000;//每帧图片停留的时间，1000=1秒
        //var this.auto='';
        //window.onload=function(){
      
                     
              
        //}
         this._leave=BindAsEventListener(this,this.leave);
         this._setCurrent=BindAsEventListener(this,this.setCurrent);
         this._autoFocusChange = BindAsEventListener(this,this.autoFocusChange);

     
        
        
        this.init();
       this.focusChange();
        this.auto=setInterval(this._autoFocusChange.bind(this), this.interval);
   
     },
        ResponseUI:function()
        {
            var html= "<div class=\"flascroll\" id=\""+this.id+"\">";
		    html+= "<div id=\""+this.id+"_loading\" class='loading'>请稍候...</div>";
		    html+= "<div id=\""+this.id+"_ts_bg\" class='ts_bg' style=\"display:none;\">文字标题的背景</div>";
		    html+= "<div id=\""+this.id+"_btn_bg\" class='btn_bg'>&nbsp;</div>";
            html+="<div id=\""+this.id+"_pics\" class='pics'>";
     		html+=  " <ul>";
     		for(var i=0;i<this.pics.length;i++)
     		{
          	html+=	   " <li><a href=\"#\"><img src=\""+this.pics[i]+"\" alt=\"\" /></a></li>";
        	}
         html+= "  </ul>";
            html+="</div>";
            html+="<div id=\""+this.id+"_ts\" class='ts' style=\"display:none;\">";
     		html+= "<ul>";
     		for(var i=0;i<this.pics.length;i++)
     		{
     		    html+= "<li></li>";
     		}
     		html+="</ul>";
            html+= "</div>";
            html+= "<div id=\""+this.id+"_btn\" class='btn'><ul>";
            for(var i=0;i<this.pics.length;i++)
     		{
     		    html+= "<li>"+(i+1)+"</li>";
     		}
     		html+="</ul>";
            html+="</div>";
            html+="</div>";
            return html;
        }, 
         poptit:function(tsID,n){//文字上下运动函数
         var ts = $$('li',this.id+"_"+tsID);
         var setway=function(obj,y){obj.style.bottom=y+'px';}
         var getway=function(obj){return parseInt(obj.style.bottom);}
         var up=function(){
          if (ts[n].movement) clearTimeout(ts[n].movement);//为了兼容变化中的点击
          if (y1 == 0) return true;
          y1+=Math.ceil((0 - y1) / 5);
          setway(ts[n],y1);
          if(y1<0) ts[n].movement = setTimeout(up, 1);
         }
         var down=function(){
          if (ts[N].movement) clearTimeout(ts[N].movement);
          if (y2 == -32) return true;
          y2+=Math.floor((-32 - y2) / 5);
          setway(ts[N],y2);
          if(y2>-32) ts[N].movement = setTimeout(down, 1);
         }
         for(var i=0;i<ts.length;i++){
          if (!ts[i].style.bottom) ts[i].style.bottom = "-32px";
          if(ts[i].name=='up') var N=i;
         }
         if(!N&&n==0) {//开始载入...
          ts[n].name='up';
          var y1=getway(ts[n]);
          up();
          return true;
         } 
         if(N==n) return true;//防止快速移出移入的抖动
         ts[N].name=''//标记淡入的name为空
         ts[n].name='up';
         var y1=getway(ts[n]);
         var y2=getway(ts[N]);
         down();
         up();
        },
        opa:function(pics,n){//图片淡入淡出函数
         var pics = $$('img',this.id+"_"+pics);
               
         var setfade=function(obj,o){
        
          if (document.all) obj.style.filter = "alpha(opacity=" + o + ")";
          else obj.style.opacity = (o / 100);
         };
         var getfade=function(obj){
           
          return (document.all)?((obj.filters.alpha.opacity)?obj.filters.alpha.opacity:false):((obj.style.opacity)?obj.style.opacity*100:false);
         }
         var show=function(){
          if(pics[n].move) clearTimeout(pics[n].move);
          if (o1 == 100) return true;
          o1+=5;
          setfade(pics[n],o1);
          if(o1<100) pics[n].move=setTimeout(show,1);
         };
         var hide=function(){
          if(pics[N].move) clearTimeout(pics[N].move);
          if (o2 == 0) return true;
          o2-=5;
          setfade(pics[N],o2);
          if(o2>0) pics[N].move=setTimeout(hide,1);
         };
         
         for(var i=0;i<pics.length;i++){
          if(!getfade(pics[i])) setfade(pics[i],0);
          if(pics[i].name=='out') var N=i;
         }
         if(!N&&n==0) {//开始载入...
          pics[n].name='out';
          var o1=getfade(pics[n]);
          show();
          return true;
         }
         if(N==n) return true;
         pics[N].name=''
         pics[n].name='out';
         var o1=getfade(pics[n]);
         var o2=getfade(pics[N]);
         hide();
         show();
        },
        classNormal:function() {//数字标签样式清除
            var focusBtnList = $$('li',this.id+'_btn');
            for (var i = 0; i < focusBtnList.length; i++) {
                focusBtnList[i].className = '';
            }
        },
        autoFocusChange:function() {//自动运行
            if (this.atuokey) return;
            var focusBtnList = $$('li',this.id+'_btn');
            for (var i = 0; i < focusBtnList.length; i++) {
                if (focusBtnList[i].className == 'current') {
                     this.currentNum = i;
                }
            }
         if(this.currentNum<focusBtnList.length-1){
          this.poptit('ts',this.currentNum+1);
          this.opa('pics',this.currentNum+1);
      this.classNormal();
      focusBtnList[this.currentNum+1].className = 'current';
         }else if(this.currentNum==focusBtnList.length-1){
          this.poptit('ts',0);
          this.opa('pics',0);
      this.classNormal();
      focusBtnList[0].className = 'current';
         }
        },
        focusChange:function() {//交互切换
         var focusBtnList = $$('li',this.id+'_btn');
        for (var i = 0; i < focusBtnList.length; i++) {
          focusBtnList[i].I=i;
//          focusBtnList[i].onclick = function(){
//           poptit('ts',this.I);      
//           opa('pics',this.I);
//                 classNormal();
//                 focusBtnList[this.I].className = 'current';
//          }
          focusBtnList[i].onmouseover = function(){
           this.style.backgroundColor='';
           this.style.cursor='pointer';
          }
          focusBtnList[i].onmouseout = function(){
           this.style.backgroundColor='';
          }
         }
        },
        setCurrent:function()
        {
            var e = czyjs.Event.GetEventObj();
         if( e.tagName=="LI"){ 
           this.poptit('ts',e.I);
           this.opa('pics',e.I);    
           this.classNormal();
           e.className = 'current';}

        },
        
        leave:function()
        {
           this.atuokey = false; 
           this.auto=setInterval(this.autoFocusChange.bind(this), this.interval);
        },
        
        init:function(){//初始化
     
        
         $(this.id+'_btn_bg').innerHTML=$(this.id+'_btn').innerHTML;
         $(this.id).removeChild($$('div',this.id)[0]);
         this.poptit('ts',0);
         this.opa('pics',0);
       this.classNormal();
       $$('li',this.id+'_btn')[0].className = 'current';
       
      
       this._poptit = BindAsEventListener(this,function(e){this.poptit('ts',e.I);});
       this._opa = BindAsEventListener(this,function(e){this.opa('ts',e.I);});
       this._classNormal = BindAsEventListener(this,function(e){this.classNormal('ts',e.I);});
       
       addEventHandler($(this.id+'_btn'), "click", this._setCurrent);
       addEventHandler($(this.id), "mouseover", function(){ this.atuokey = true; clearInterval(this.auto);});
          //addEventHandler($(this.id), "mouseout",this._leave );
   
        }
   
 }


