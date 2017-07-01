
if(typeof(czyjs.UI)=="undefined")
{
    czyjs.UI = {};
}
 czyjs.UI.CheckBoxButton=Class.create();
 czyjs.UI.CheckBoxButton.prototype = {
	initialize: function(param){
		
		this.config={
			checkCss:"CheckBoxByDiv_check",
			unCheckCss:"CheckBoxByDiv_uncheck"
		};
		
	    var json=eval(param);
		this.check=json.check;
		this.value=json.value;
		this.id=json.id;
		this.name=json.name;
		this.label=json.label;
		this.contenter=json.constructorId;
		
		
		this.checkBoxDiv= document.createElement("div");
		this.checkBoxDiv.check=this.check;
		this.checkBoxDiv.id=this.id;
		this.checkBoxDiv.value=this.value;
	        this.checkBoxDiv.innerHTML=this.label;
		this.checkBoxDiv.label=this.label;
		
		if(this.check)
		{this.checkBoxDiv.className="CheckBoxByDiv_check"}
		else
		{this.checkBoxDiv.className="CheckBoxByDiv_uncheck"}
		
		var contentObj=document.getElementById(this.contenter);
		contentObj.appendChild(this.checkBoxDiv);
		
		addEventHandler(this.checkBoxDiv, "click",BindAsEventListener(this,this.Checked));
	},
	
    Checked:function()
	{
		if( this.checkBoxDiv.check)
		{
			this.checkBoxDiv.check=false;
			this.checkBoxDiv.className=this.config.unCheckCss;
		}
		else
		{
			this.checkBoxDiv.check=true;
			this.checkBoxDiv.className=this.config.checkCss;
		}
		 
		 
	}
}