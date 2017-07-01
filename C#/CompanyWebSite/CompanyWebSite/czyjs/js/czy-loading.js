
if(typeof(czyjs.UI)=="undefined")
{
    czyjs.UI = {};
}
/*
*contenterId:容器(注:需宽和高)
*/
czyjs.UI.Loading=Class.create();
czyjs.UI.Loading.prototype = {
	initialize: function(param){
		
		this.config={
			contenterId:param.randerTo,
            id:param.id,
            className:param.className==null?"loading":param.className
		}
		
		this.cssConfig={
			loadingBg:"loadingBg",
			loadingPic:"loadingPic"
		}
		
		this.contenter=document.getElementById(this.config.contenterId)==null ? this.config.contenterId:document.getElementById(this.config.contenterId);
		this.loadingBg=document.createElement("div");
		this.loadingPic=document.createElement("div");
		this.loadingBg.className=this.cssConfig.loadingBg;
		this.loadingPic.className=this.cssConfig.loadingPic;
		this.contenter.className=this.config.className;
		
		this.loadingBg.style.width="100%";
		this.loadingBg.style.height="100%";
		
		this.loadingBg.appendChild(this.loadingPic);
		this.contenter.appendChild(this.loadingBg);
	    
		
		//var contenterCssWidth=this.contenter.style.width?this.contenter.style.width:czyjs.Element.GetCssStyle(this.contenter,"width");
		var contenterCssHeight=this.contenter.style.height?this.contenter.style.height:czyjs.Element.GetCssStyle(this.contenter,"height");
		//var loadingCssWidth=czyjs.Element.GetCssStyle(this.loadingPic,"width");
		var loadingCssHeight=czyjs.Element.GetCssStyle(this.loadingPic,"height");
		  
	
		//var contenterWidth=parseInt(contenterCssWidth.replace("px",""));
		var contenterHeight=parseInt(contenterCssHeight.replace("px",""));
		//var loadingPicWidth=parseInt(loadingCssWidth.replace("px",""));
		var loadingPicHeight=parseInt(loadingCssHeight.replace("px",""));
		
	   
		this.loadingBg.style.textAlign="center";
		this.loadingBg.style.display="none";
	    //this.loadingPic.style.marginLeft=(contenterWidth/2-loadingPicWidth/2)+"px";
		this.loadingPic.style.marginTop=(contenterHeight/2-loadingPicHeight/2)+"px";
		this.loadingPic.style.marginLeft="auto";
		this.loadingPic.style.marginRight="auto";
		this._Show=BindAsEventListener(this,this.Show);
		this._Hidden=BindAsEventListener(this,this.Hidden);
	},
	
	Show:function()
	{
		this.loadingBg.style.display="block";
	},
	
	Close:function()
	{
		this.loadingBg.style.display="none";
	}
}