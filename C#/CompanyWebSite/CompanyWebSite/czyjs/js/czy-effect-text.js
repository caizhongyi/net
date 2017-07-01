//if(typeof(czyjs.UI)=="undefined")
//{
//    czyjs.UI = {};
//}
if(typeof(czyjs.Effect)=="undefined")
{
    czyjs.Effect = {};
}
/*
*  图片路径

czyjs.UI.Effect.imgs={
    txtBg              :   hostName+"../images/txt-bg.gif",         //背景图片
    txtBorder          :   hostName+"../images/txt-border.gif",  
    txtBorderOn        :   hostName+"../images/txt-border-on.gif"    
}
*/

czyjs.Effect.Text=Class.create();
czyjs.Effect.Text.prototype={
    /*
    *文本框特效
    */
	initialize:function(param){
	    
	    this.config={
	        id          :       param.id//文本id
	    }
	    
	    this.css={
	        txtBoxForm           :  "czy-effect-txtBoxForm",
	        txtBox               :  "czy-effect-txtBox",
	        txtBorderTopLeft     :  "czy-effect-txtBorderTopLeft",
	        txtBorderTopRight    :  "czy-effect-txtBorderTopRight",
	        txtBorderTopCenter   :  "czy-effect-txtBorderTopCenter",
	        txtBorderLeft        :  "czy-effect-txtBorderLeft",
	        txtBorderRight       :  "czy-effect-txtBorderRight",
	        txtBorderBottomLeft  :  "czy-effect-txtBorderBottomLeft",
	        txtBorderBottomRight :  "czy-effect-txtBorderBottomRight",
	        txtBorderBottomCenter:  "czy-effect-txtBorderBottomCenter"
	    }
	    
	    
	    this.styles=
	    {
	        txtBox    :   "czy-textbox",
	        txtBoxOn  :   "czy-textbox-on"
	    }
	    
	    if(this.config.borderWeight==null)
	    {
	       this.config.borderWeight=3;
	    }
	    
        this. Init();
	    this.AddListener();
	},
    Init:function ()
    {
      this.txtObj= document.getElementById (this.config.id);
      this.txtObj.className=this.styles.txtBox;
    },
	
	
	CreateElement:function ()
	{
	   this.txtObj=document.getElementById (this.config.id);
       this.txtObjParent=document.createElement("div");
       
       this.txtTopLeft=document.createElement("div");
       this.txtTopRight=document.createElement("div");
       this.txtTopCenter=document.createElement("div");
      
       
       this.txtLeft=document.createElement("div");
       this.txtRight=document.createElement("div");
       this.txtCenter=document.createElement("div");
       
       this.txtBottomLeft=document.createElement("div");
       this.txtBottomRight=document.createElement("div");
       this.txtBottomCenter=document.createElement("div");
       
       this.txtCenter.appendChild (this.txtObj);
       this.txtObjParent.appendChild (this.txtTopLeft);
       this.txtObjParent.appendChild (this.txtTopRight);
       this.txtObjParent.appendChild (this.txtTopCenter);
       this.txtObjParent.appendChild (this.txtLeft);
       this.txtObjParent.appendChild (this.txtRight);
       this.txtObjParent.appendChild (this.txtCenter);
       this.txtObjParent.appendChild (this.txtBottomLeft);
       this.txtObjParent.appendChild (this.txtBottomRight);
       this.txtObjParent.appendChild (this.txtBottomCenter);
       
      // this .txtObj.style.borderWidth="0px";
      this.txtObj.className=this.css.txtBox;
      
      
//       this.txtObjParent.childNodes[0].style.height=this.config.borderWeight+"px";
//       this.txtObjParent.childNodes[1].style.height=this.config.borderWeight+"px";
//       this.txtObjParent.childNodes[2].style.height=this.config.borderWeight+"px";
//       this.txtObjParent.childNodes[3].style.width=this.config.borderWeight+"px";
//       this.txtObjParent.childNodes[4].style.width=this.config.borderWeight+"px";
//       this.txtObjParent.childNodes[6].style.height=this.config.borderWeight+"px";
//       this.txtObjParent.childNodes[7].style.height=this.config.borderWeight+"px";
//       this.txtObjParent.childNodes[8].style.height=this.config.borderWeight+"px";
       
       this.txtObjParent.className=this.css.txtBoxForm;
       this.txtObjParent.childNodes[0].className=this.css.txtBorderTopLeft;
       this.txtObjParent.childNodes[1].className=this.css.txtBorderTopRight;
       this.txtObjParent.childNodes[2].className=this.css.txtBorderTopCenter;
       this.txtObjParent.childNodes[3].className=this.css.txtBorderLeft;
       this.txtObjParent.childNodes[4].className=this.css.txtBorderright;
       this.txtObjParent.childNodes[6].className=this.css.txtBorderBottomLeft;
       this.txtObjParent.childNodes[7].className=this.css.txtBorderBottomRight;
       this.txtObjParent.childNodes[8].className=this.css.txtBorderBottomCenter;
       
       
    
	},
	
	/*
	* 增加事件
	*/
	AddListener:function ()
	{
	   this._TxtOnFocus=BindAsEventListener(this,this.TxtOnFocus);
	   this._TxtOnBlur=BindAsEventListener(this,this.TxtOnBlur);
	   addEventHandler(this.txtObj, "focus",this._TxtOnFocus );
	   addEventHandler(this.txtObj, "blur",this._TxtOnBlur );
	},
    /*
	* 鼠标焦点
	*/
    TxtOnFocus:function ()
    {
       var o=czyjs.Event.GetEventObj();
       o.className=this.styles.txtBoxOn;
//       var cssOn="On";
//       o.className=this.css.txtBox+cssOn;
//       o.parentNode.childNodes[0].className=this.css.txtBorderTopLeft+cssOn;
//       o.parentNode.childNodes[1].className=this.css.txtBorderTopRight+cssOn;
//       o.parentNode.childNodes[2].className=this.css.txtBorderTopCenter+cssOn;
//       o.parentNode.childNodes[3].className=this.css.txtBorderLeft+cssOn;
//       o.parentNode.childNodes[4].className=this.css.txtBorderright+cssOn;
//       o.parentNode.childNodes[6].className=this.css.txtBorderBottomLeft+cssOn;
//       o.parentNode.childNodes[7].className=this.css.txtBorderBottomRight+cssOn;
//       o.parentNode.childNodes[8].className=this.css.txtBorderBottomCenter+cssOn;
    },
    /*
	* 鼠标离开焦点
	*/
    TxtOnBlur:function ()
    {
      
       var o=czyjs.Event.GetEventObj();
       o.className=this.styles.txtBox;
       this.value=o.value;
//       o.className=this.css.txtBox;
//       o.parentNode.childNodes[0].className=this.css.txtBorderTopLeft;
//       o.parentNode.childNodes[1].className=this.css.txtBorderTopRight;
//       o.parentNode.childNodes[2].className=this.css.txtBorderTopCenter;
//       o.parentNode.childNodes[3].className=this.css.txtBorderLeft;
//       o.parentNode.childNodes[4].className=this.css.txtBorderright;
//       o.parentNode.childNodes[6].className=this.css.txtBorderBottomLeft;
//       o.parentNode.childNodes[7].className=this.css.txtBorderBottomRight;
//       o.parentNode.childNodes[8].className=this.css.txtBorderBottomCenter;
    }
}