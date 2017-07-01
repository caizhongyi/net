if(typeof(czyjs.UI)=="undefined")
{
    czyjs.UI.Controls = {};
};

if(typeof(czyjs.UI.Controls)=="undefined")
{
    czyjs.UI.Controls = {};
};


czyjs.UI.Controls.TabView=Class.create();
czyjs.UI.Controls.TabView.prototype = {

    initialize: function (param) {
        
         //传入参数
        this.titlePix=param.titlePix==null?{width:50,height:25}:param.titlePix;
        this.borderPix=param.borderPix==null?{width:10,height:10}:param.borderPix;
        this.obj = document.getElementById(param.randerTo) == null ? param.randerTo : document.getElementById(param.randerTo);
        this.data = param.data;              //[title:"",content:""]
        this.id = param.id == null ? "czy-tab-view" : param.id;
        this.width = param.width == null ? 300 : param.width;
        this.height = param.height == null ? 300 : param.height;
        this.className=param.className==null?"czy-tab-view":param.className;
        this.titlePostion=param.titlePostion==null? "top":param.titlePostion;//标题位置:默认为top [or bottom] 
        this.direction = param.direction == null ? "H" : param.direction; //默认参数为H(横向) ,V(则为纵向);
        this.animat = param.animat == null ? "true" : param.animat; //是否有动画. 默认为true;
       
        //初始化
        
        this.obj.id = this.id;
        this.tempIndex=0;
        this.selectedIndex = 0;
        
        this.Init();
    },
    Init: function () {
        this.CreateUI();
        this.AddListener();
    },
    CreateUI:function()
    {     
          this.contentHeight=this.height-this.titlePix.height-this.borderPix.height*2;
          this.contentWidth=this.width-this.borderPix.width;
          this.listWidth=this.direction=="H"?this.contentWidth * this.data.length:this.contentWidth;
             
             
          var direction=this.direction=="H"?"float:left;":"";
          var display=this.animit?"display:block;":"display:none;";
          var titleHTML="<ul id='"+this.id+"_titles' class='titles list-init' style='overflow:hidden'>";
          var containterHTML="";
          for(var i=0;i<this.data.length;i++)
          {
              var cls =i==this.selectedIndex?"current":"normal";
              titleHTML+="<li><a href='#' index='"+i+"' class='"+cls+"' style='height:"+this.titlePix.height+"px;width:"+this.titlePix.width+"px; line-height:"+this.titlePix.height+"px;display:block'>"+this.data[0].title+"</a></li>";
          }
          titleHTML+="</ul>"; 
       
          var contentHTML="<div style='position:relative;'><ul id='"+this.id+"_contents' class='contents list-init' style='position:absolute;margin-left:15px; left:0px; top:0px; overflow:hidden;width:"+this.listWidth+"px'>";
          for(var i=0;i<this.data.length;i++)
          {
              display=i==this.selectedIndex?"display:block;":display;
              contentHTML+="<li style='"+direction+" width:"+this.contentWidth+"px;height:"+this.contentHeight+"px;"+display+";'>"+this.data[0].content+"</li>";
          }
          contentHTML+="</ul></div>"; 
          contentHTML+="<div style='clear:both'></div>"; 
          switch(this.titlePostion)
          {
              case "top": containterHTML=titleHTML+contentHTML;break;
              case "bottom": containterHTML=contentHTML+titleHTML;break;
              default: containterHTML=titleHTML+contentHTML;break ;
          }

          
            var html = "<div id='"+this.id+"'  class='"+this.className+"' style='width:"+this.width+"px;'>";
            html += "<div class='t-border' >";
            html += "<div class='t-topLeft div-left'   style='width:"+this.borderPix.width+"px;height:"+this.borderPix.height+"px; '  ></div>";
            html += "<div class='t-topRight div-right'  style='width:"+this.borderPix.width+"px;height:"+this.borderPix.height+"px; ' ></div>";
            html += "<div class='t-topCenter' style='height:"+this.borderPix.height+"px' ></div>";
          
            html += "<div >";
            html += "<div class='t-middleLeft div-left' style='width:"+this.borderPix.width+"px;'></div>";
            html += "<div class='t-middleRight div-right' style='width:"+this.borderPix.width+"px; '></div>";
            html += "<div class='t-middleCenter'   style='width:"+(this.width-this.borderPix.width)+"px; height:"+(this.height-this.borderPix.height*2)+"px; ' >";
            html += containterHTML;
            html += "</div>";
            html += "</div>";
            

            html += "<div class='t-bottomLeft div-left'   style='width:"+this.borderPix.width+"px;height:"+this.borderPix.height+"px' ></div>";
            html += "<div class='t-bottomRight div-right'  style='width:"+this.borderPix.width+"px;height:"+this.borderPix.height+"px'  ></div>";
            html += "<div class='t-bottomCenter ' style='height:"+this.borderPix.height+"px' ></div>";
            html += "</div>";
            html += "</div>";
            
            this.obj.innerHTML=html;
     
           
    },
    
   
    AddListener: function () {
        var titles=document.getElementById (this.id+"_titles");
        this._TabClick = BindAsEventListener(this, this.TabClick);
        addEventHandler(titles, "click", this._TabClick);
         
    },

    TabClick: function () {

        var o = czyjs.Event.GetEventObj();
        if(o.tagName=="A" )
        {
             this.tempIndex=this.selectedIndex;
             this.selectedIndex=o.index;
             var titles = document.getElementById (this.id+'_titles');
             var contents = document.getElementById (this.id+'_contents');
         
                titles.childNodes[this.selectedIndex].childNodes[0].className='current';
                titles.childNodes[this.tempIndex].childNodes[0].className='normal';
                if (this.animat) {
                 
                    this.Move();
                }
                else
                {
                    contents.childNodes[this.selectedIndex].style.display='block';
                    contents.childNodes[this.tempIndex].style.display='none';
                }
        }
        
       
    },

    Move: function () {
        var sb = null;
        var contents=document.getElementById ( this.id+"_contents");
        var left = parseFloat(contents.style.left.replace("px", ""));
        var top = parseFloat(contents.style.top.replace("px", ""));
          
        if (this.direction == "H") {

            sb = new czyjs.Effect.StoryBoard(
           {
               id: contents,
               startPoint: { x: left, y: top },
               endPoint: { x: this.contentWidth * -this.selectedIndex, y: top },
               delayTime: 1,
               speed: 20,
               type: "position"
           });
        }
        else {

            sb = new czyjs.Effect.StoryBoard(
           {
               id: contents,
               startPoint: { x: left, y: top },
               endPoint: { x: left, y: this.contentHeight * -this.selectedIndex },
               delayTime: 1,
               speed: 20,
               type: "position"
           });
        }
        sb.Start();
    }

}
