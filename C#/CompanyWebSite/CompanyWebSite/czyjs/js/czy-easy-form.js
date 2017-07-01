
if(typeof(czyjs.UI)=="undefined")
{
    czyjs.UI = {};
}


//************************
//弹出JS渐变窗口
//************************
//formContent:  内容
//width       宽
//height      高
//contenterId 加载元素ID
//speed       渐变速度
//isPicture   是否为内容显示为图片
//***************************************
czyjs.UI.EasyWindowForm=Class.create();
czyjs.UI.EasyWindowForm.prototype = {
    initialize: function (param) {

 	
        //窗口顶部图片title列表
        this.fontList = {
            closeTitle: "关闭",
            zoomTitle: "放大",
            narrowTitle: "缩小",
            returnDocTitle: "原始",
            openNewWindowTitle: '新窗口打开'

        }
        
        //初始化变量
        this.border =param.border==null? { width: 10, height: 10 }:param.border;
        this.operPic =param.operPic==null? { width: 32, height: 32 }:param.operPic;
        this.apahValue = 0;   //初始透明度
        this.diffHeight = 50;  //内框与外框的差值
        this.formContentStr = param.formContent; //内容
        this.speed = document.all ? param.speed : param.speed;  //变化速度
        this.resizeValue = param.resizeValue == null ? 40 : param.resizeValue; //放缩大小
        this.MaxWidth = param.MaxWidth == null ? 700 : param.MaxWidth;   //最大宽度
        this.MinWidth = param.MinWidth == null ? 700 : param.MinWidth;  //最小宽度
        this.isPicture = param.isPicture == null ? false : param.isPicture;  //是否为图片
        this.formTopHTML = param.formTopHTML == null ? "" : param.formTopHTML;  //顶部HTML
        this.scroll = param.scroll == null ? false : param.scroll;  //是否带滚动条
       // this.borderPixs = param.borderPixs == null ? {
       //     width: { leftW: 16, rightW: 18, centerW: 400 },
       //     height: { topH: 11, bottomH: 18, middleH: 400 }
       // } : param.borderPixs; //边框的大小宽度
        this.className = param.className == null ? "czy-easyform" : param.className; //窗口样式
        this.id = param.id == null ? "windowform" : param.id; //窗口ID
        this.type = param.type == null ? "zoom" : param.type; //动画类型
        //高和宽
//        this.contentWidth = this.borderPixs.width.centerW;
//        this.contentHeight = this.borderPixs.height.middleH;
        this.width = param.width==null?400:param.width;
        this.height = param.height;
        if (this.type == "zoom") {
            this.left = document.documentElement.scrollLeft + (document.body.clientWidth / 2);
            this.top = document.documentElement.scrollTop + (document.body.clientHeight / 2);
        }
        else {
            this.left = document.documentElement.scrollLeft + (document.body.clientWidth / 2) - this.width / 2;
            this.top = document.documentElement.scrollTop + (document.body.clientHeight / 2) - this.height / 2;
        }
        this.CreateElement();  //创始元素
    },

    CreateElement: function () {
        //加载父级元素
        this.formParentObj = document.createElement("div");
        this.obj = document.createElement("div");
        this.formBgObj = document.createElement("div");
        document.body.insertBefore(this.formParentObj, document.body.firstChild);
        this.formParentObj.style.position = "relative";
        this.formParentObj.style.zIndex = "100";
        this.formParentObj.style.textAlign = "left";


        this.SetBackGround();


        this.obj.style.left = this.left + "px";
        this.obj.style.top = this.top + "px";
        this.obj.style.display = 'none';

        this.obj.style.width = this.width + "px";
        this.obj.style.height = this.height + "px";

        this.obj.style.position = "absolute";
        this.obj.style.overflow = "hidden";
        this.obj.zIndex = "100";

        this.formParentObj.appendChild(this.formBgObj);
        this.formParentObj.appendChild(this.obj);

        this.contentClassName = this.scroll ? "czy-easyform-scrollContent" : "czy-easyform-content";
        var contentHTML = "";
        contentHTML += "<div class='czy-easyfrom-top' style='overflow:hidden;  '>";
        contentHTML += "<div style='float:left'>" + this.formTopHTML + "</div>";
        contentHTML += "<div id='" + this.id + "_oper' style='float:right;'>";
        contentHTML += "<ul class='czy-easyform-oper' style='list-style-type:none;margin:0px;padding:0px;overflow:hidden;'>";
        if (this.isPicture) {
            contentHTML += "<li id='" + this.id + "_zoom' style='float:left;overflow:hidden;'><a href='javascript:void(0)' style='display:block;width:" + this.operPic.width + "px;height:" + this.operPic.height + "px;  background-position:center; ' class='czy-easyform-zoom'  title='" + this.fontList.zoomTitle + "'></a></li>";
            contentHTML += "<li id='" + this.id + "_narrow' style='float:left;overflow:hidden;'><a href='javascript:void(0)' style='display:block;width:" + this.operPic.width + "px;height:" + this.operPic.height + "px; background-position:center; ' class='czy-easyform-narrow'  title='" + this.fontList.narrowTitle + "'></a></li>";
            contentHTML += "<li id='" + this.id + "_returnDoc' style='float:left;overflow:hidden;'><a href='javascript:void(0)'  style='display:block;width:" + this.operPic.width + "px;height:" + this.operPic.height + "px; background-position:center; ' class='czy-easyform-returnDoc'  title='" + this.fontList.returnDocTitle + "'></a></li>";
            contentHTML += "<li id='" + this.id + "_openNewWindows' style='float:left;overflow:hidden;'><a href='javascript:void(0)'  style='display:block;width:" + this.operPic.width + "px;height:" + this.operPic.height + "px; background-position:center; ' class='czy-easyform-openNewWindow'  title='" + this.fontList.openNewWindowTitle + "'></a></li>";
        }
        contentHTML += "<li id='" + this.id + "_close' style='float:left;overflow:hidden;'><a href='javascript:void(0)'  style='display:block;width:32px;height:32px; background-position:center; ' class='czy-easyform-close' title='" + this.fontList.closeTitle + "'></a></li>";
        contentHTML += "</ul>";
        contentHTML += "</div>";
        contentHTML += "</div>";
        contentHTML += "<div id='" + this.id + "_data' class='container' >";
        contentHTML += this.formContentStr;
        contentHTML += "</div>";


        var html = "<div  class='"+this.className+"'>";
       
            html += "<table class='t-border' border=\"0\" cellpadding=\"0\" cellspacing=\"0\"  style='width:100%; height:100%;'>";
            html += "<tr>";
            html += "<td class='t-topLeft'   width='"+this.border.width+"' height='"+this.border.height+"'  ></td>";
            html += "<td  class='t-topCenter'  >";
            html += "</td>";
            html += "<td class='t-topRight'   width='"+this.border.width+"' height='"+this.border.height+"'></td>";
            html += "</tr>";
            
            html += "<tr>";
            html += "<td class='t-middleLeft' ></td>";
            html += "<td  class='t-middleCenter' valign=\"top\"  >";
            html += contentHTML;
            html += "</td>";
            html += "<td class='t-middleRight' ></td>";
            html += "</tr>";
            
            html += "<tr>";
            html += "<td class='t-bottomLeft'    width='"+this.border.width+"' height='"+this.border.height+"'></td>";
            html += "<td class='t-bottomCenter'  >";
            html += "</td>";
            html += "<td class='t-bottomRight'   width='"+this.border.width+"' height='"+this.border.height+"' ></td>";
            html += "</tr>";
            
            
//            html += "<div class='easyform-borderTop' style='overflow:hidden;'>";
//            html += "<div class='easyform-borderTop-left' style='overflow:hidden;float:left;margin-right:-3px;width:" + this.borderPixs.width.leftW + "px;height:" + this.borderPixs.height.topH + "'></div>";
//            html += "<div class='easyform-borderTop-right' style='overflow:hidden;float:right;margin-left:-3px;width:" + this.borderPixs.width.rightW + "px;height:" + this.borderPixs.height.topH + "px'></div>";
//            html += "<div class='easyform-borderTop-center' style='overflow:hidden;height:" + this.borderPixs.height.topH + "px;width:100%;'></div>";
//            html += "</div>";
//            html += "<div  id='" + this.id + "_middle' class='easyform-borderMiddle' style='overflow:hidden;height:" + this.borderPixs.height.middleH + "px;'>";
//            html += "<div  class='easyform-borderMiddle-left' style='overflow:hidden;float:left;margin-right:-3px;width:" + this.borderPixs.width.leftW + "px;height:100%;'></div>";
//            html += "<div  class='easyform-borderMiddle-right' style='overflow:hidden;float:right;margin-left:-3px;width:" + this.borderPixs.width.rightW + "px;height:100%;'></div>";
//            html += "<div  id='" + this.id + "_data' class='easyform-borderMiddle-center' style='overflow:hidden;height:100%;'>";
//            html += contentHTML;
//            html += "</div>";
//            html += "</div>";
//            html += "<div class='easyform-borderBottom' style='overflow:hidden;height:" + this.borderPixs.height.bottomH + "px'>";
//            html += "<div class='easyform-borderBottom-left' style='overflow:hidden;float:left;margin-right:-3px;width:" + this.borderPixs.width.leftW + "px;height:" + this.borderPixs.height.bottomH + "px'></div>";
//            html += "<div class='easyform-borderBottom-right' style='overflow:hidden;float:right;margin-left:-3px;width:" + this.borderPixs.width.rightW + "px;height:" + this.borderPixs.height.bottomH + "px'></div>";
//            html += "<div class='easyform-borderBottom-center' style='overflow:hidden;height:" + this.borderPixs.height.bottomH + "px;width:100%;'></div>";
//            html += "</div>";
            html += "</table>";
           
            html += "</div>";
        this.obj.innerHTML = html;
        this.obj.id=this.id;
       
        //加载关闭等
        this.AddListen();
        if (this.obj.id != null) {
            //new YAHOO.util.DD(this.formObj.id,'YahooDarg'); //yhoo拖动效果
            //new YAHOO.util.DDProxy(this.formObj.id,'YahooDarg');
            //yhoo拖动效果
            var yuiID = "#" + this.id;
            YUI().use('dd-constrain', function (Y) {
                var dd1 = new Y.DD.Drag({ node: yuiID }).plug(Y.Plugin.DDConstrained, { constrain2node: document.getElementById( this.id+"czy-easyform-bg")})
            });

        }

    },
    SetBackGround: function () {
        if (document.body.clientHeight < window.screen.availHeight) {
            this.formBgObj.style.posHeight = window.screen.availHeight;
            this.formBgObj.style.height = window.screen.availHeight + "px";

        }
        else {
            this.formBgObj.style.posHeight = document.body.clientHeight;
            this.formBgObj.style.height = document.body.clientHeight + "px";
        }
        if (document.body.clientWidth < window.screen.availWidth) {
            this.formBgObj.style.posWidth = window.screen.availWidth;
            this.formBgObj.style.width = window.screen.availWidth + "px";
        }
        else {
            this.formBgObj.style.posWidth = document.body.clientWidth;
            this.formBgObj.style.width = document.body.clientWidth + "px";
        }
        this.formBgObj.style.overflow = "hidden";
        this.formBgObj.className = "czy-easyform-bg";
        this.formBgObj.id = this.id+"czy-easyform-bg";
        this.formBgObj.style.position = "absolute";
        //this.formBgObj.style.filter = "alpha(opacity=75)";
        //this.formBgObj.style.opacity = "0.75";
        this.formBgObj.style.display = "none";
        this.formBgObj.zIndex = "99";
    },
    /*
    * 添加头部无素
    */
    AddListen: function () {


        ///添加事件
        this._close = BindAsEventListener(this, this.Close);
        this._returnDoc = BindAsEventListener(this, this.ReturnDocEasyWindwoForm);
        this._openNewWindows = BindAsEventListener(this, this.OpenNewWindowsEasyWindwoForm);
        this._zoom = BindAsEventListener(this, this.ZoomEasyWindwoForm);
        this._narrow = BindAsEventListener(this, this.NarrowEasyWindwoForm);
        this._Show = BindAsEventListener(this, this.Show);

        if (this.isPicture) {
            var a1 = document.getElementById(this.id + "_zoom");
            var a2 = document.getElementById(this.id + "_narrow");
            var a3 = document.getElementById(this.id + "_returnDoc");
            var a4 = document.getElementById(this.id + "_openNewWindows");
            addEventHandler(a1, "click", this._zoom);
            addEventHandler(a2, "click", this._narrow);
            addEventHandler(a3, "click", this._returnDoc);
            addEventHandler(a4, "click", this._openNewWindows);
        }
        var a5 = document.getElementById(this.id + "_close");
        addEventHandler(a5, "click", this._close);

    },

    /*
    * 打开
    */
    Show: function () {
        document.body.scroll = "no";
        document.body.style.overflow = "hidden";

        this.formBgObj.style.display = "block";
        this.obj.style.display = "block";
        // document.documentElement.style.margin = "0px";
        //this.DisableScroll();
        var d= document.getElementById(this.id + "_data");
//        var diffHeight = this.height - this.contentHeight;
//        var strContent = document.getElementById(this.id + "_strContent");
//        var strContentDiffWidth = this.width - this.borderPixs.width.centerW;
//        var strContentDiffHeight = this.height - this.borderPixs.height.middleH - this.operPic.height;
//        var middle = document.getElementById(this.id + "_middle");

//        var obj = this.obj;
        if (this.type == 'zoom') {
//            var mHeight = -diffHeight;
//            var scHeight = -strContentDiffHeight;
            d.style.width = "0px";
            // strContent.style.width = "0px";
            d.style.height = "0px";
            var sb3 = new czyjs.Effect.StoryBoard(
       {
           id: this.obj,
           startSize: { w: 0, h: 0 },
           endSize: { w: this.width, h: this.height },
           // position: { x: this.left, y: this.top },
           changingEvent: function (e, x, y) {

//               mHeight += y;
//               middle.style.height = mHeight > 0 ? mHeight + "px" : 0;
//               scHeight += y;
//               strContent.style.height = scHeight > 0 ? scHeight + "px" : 0;
           },
           changedEvent: function (e, x, y) { },
           delayTime: 10,
           type: "zoom",
           speed: 50
       });
            sb3.Start();
        }
        else {
            var sb = new czyjs.Effect.StoryBoard(
                {
                    id: this.obj,
                    startOpacity: 0,
                    endOpacity: 100,
                    delayTime: 10,
                    type: "opacity",
                    speed: 20
                });
            sb.Start();
        }

    },
    /*
    * 关闭
    */
    Close: function () {
        var bg = this.formBgObj;
        var obj = this.obj;
        if (this.type == "zoom") {

            var sb3 = new czyjs.Effect.StoryBoard(
               {
                   id: this.obj,
                   startSize: { w: this.width, h: this.height },
                   endSize: { w: 0, h: 0 },
                   //  position: { x: this.left, y: this.top },
                   changedEvent: function (e, x, y) {
                       bg.style.display = "none";
                       obj.style.display = 'none';
                       document.body.scroll = "yes";
                       document.body.style.overflow = "auto";
                   },
                   delayTime: 10,
                   type: "zoom",
                   speed: 50
               });

            sb3.Start();
        }
        else {
            var sb = new czyjs.Effect.StoryBoard(
                {
                    id: this.obj,
                    startOpacity: 100,
                    endOpacity: 0,
                    delayTime: 10,
                    type: "opacity",
                    speed: 20,
                    changedEvent: function () {

                        bg.style.display = "none";
                        obj.style.display = 'none';
                        document.body.scroll = "yes";
                        document.body.style.overflow = "auto";
                    }
                });
            sb.Start();
        }


    },

    DisableScroll: function () {
        //�ж�ҳ���Ƿ���XHTML��׼
        // var isXhtml=true;
        // if(document.documentElement == null || document.documentElement.clientHeight <= 0)
        // {
        // 	if(document.body.clientHeight>0 )
        //	{isXhtml = false;}
        // }
        // var htmlbody = isXhtml ?document.documentElement:document.body;
        // document.body.style.overflow = "hidden";

    },

    ableScroll: function () {
        //�ж�ҳ���Ƿ���XHTML��׼
        //  var isXhtml=true;
        // if(document.documentElement == null || document.documentElement.clientHeight <= 0)
        // {
        //	if(document.body.clientHeight>0 )
        //	{isXhtml = false;}
        // }
        // var htmlbody = isXhtml ?document.documentElement:document.body;
        //document.body.style.overflow = "auto";

    },

    /*
    * 原始大小
    */
    ReturnDocEasyWindwoForm: function () {
        var middle = document.getElementById(this.id + "_middle");
        var curWidth = parseInt(this.obj.style.width.replace("px", ""));
        var curHeight = parseInt(this.obj.style.height.replace("px", ""));
        var strContent = document.getElementById(this.id + "_strContent");
        if (this.type == 'zoom') {
            var sb3 = new czyjs.Effect.StoryBoard(
       {
           id: this.obj,
           startSize: { w: curWidth, h: curHeight },
           endSize: { w: this.width, h: this.height },
           // position: { x: this.left, y: this.top },
           changingEvent: function (obj, incrementW, incrementH) {

               middle.style.height = (parseInt(middle.style.height.replace("px", "")) + incrementH) + "px";

               strContent.style.height = (parseInt(strContent.style.height.replace("px", "")) + incrementH) + "px";
           },
           delayTime: 10,
           type: "zoom",
           speed: 50
       });
            sb3.Start();
        }
        else {
            var sb2 = new czyjs.Effect.StoryBoard(
       {
           id: this.obj,
           startSize: { w: curWidth, h: curHeight },
           endSize: { w: this.width, h: this.height },
           // position: { x: this.left, y: this.top },
           changingEvent: function (obj, incrementW, incrementH) {
           },
           delayTime: 10,
           type: "zoom",
           speed: 50
       });
            sb2.Start();
        }

    },
    /*
    * 新打开窗口
    */
    OpenNewWindowsEasyWindwoForm: function () {
        var contentListObj = this.formContent.childNodes;
        if (contentListObj != null) {
            for (var i = 0; i < contentListObj.length; i++) {
                if (contentListObj[i].tagName == "img") {
                    windows.open(contentListObj[i].src, "_black");
                }
            }
        }
    },
    /*
    * 放大
    */
    ZoomEasyWindwoForm: function () {

        // var subContent = document.getElementById(this.id + "_data");
        var middle = document.getElementById(this.id + "_middle");
        var curWidth = parseInt(this.obj.style.width.replace("px", ""));
        var curHeight = parseInt(this.obj.style.height.replace("px", ""));
        var strContent = document.getElementById(this.id + "_strContent");

        if (this.type == 'zoom') {
            var sb3 = new czyjs.Effect.StoryBoard(
       {
           id: this.obj,
           startSize: { w: curWidth, h: curHeight },
           endSize: { w: curWidth + this.resizeValue, h: curHeight + this.resizeValue },
           // position: { x: this.left, y: this.top },
           changingEvent: function (obj, incrementW, incrementH) {

               middle.style.height = (parseInt(middle.style.height.replace("px", "")) + incrementH) + "px";
              
               strContent.style.height = (parseInt(strContent.style.height.replace("px", "")) + incrementH) + "px";
           },
           delayTime: 10,
           type: "zoom",
           speed: 50
       });
            sb3.Start();
        }
        else {
            var sb2 = new czyjs.Effect.StoryBoard(
       {
           id: this.obj,
           startSize: { w: curWidth, h: curHeight },
           endSize: { w: curWidth + this.resizeValue, h: curHeight + this.resizeValue },
           // position: { x: this.left, y: this.top },
           changingEvent: function (obj, incrementW, incrementH) {
           },
           delayTime: 10,
           type: "zoom",
           speed: 50
       });
            sb2.Start();

        }

    },
    /*
    * 缩小
    */
    NarrowEasyWindwoForm: function () {
        var subContent = document.getElementById(this.id + "_data");
        var middle = document.getElementById(this.id + "_middle");
        var curWidth = parseInt(this.obj.style.width.replace("px", ""));
        var curHeight = parseInt(this.obj.style.height.replace("px", ""));
        var strContent = document.getElementById(this.id + "_strContent");
        if (this.type == 'zoom') {
            var sb3 = new czyjs.Effect.StoryBoard(
       {
           id: this.obj,
           startSize: { w: curWidth, h: curHeight },
           endSize: { w: curWidth - this.resizeValue, h: curHeight - this.resizeValue },
           // position: { x: this.left, y: this.top },
           changingEvent: function (obj, incrementW, incrementH) {
               middle.style.height = (parseInt(middle.style.height.replace("px", "")) + incrementH) + "px";
               // strContent.style.width = (parseInt(strContent.style.width.replace("px", "")) + incrementW) + "px";
               strContent.style.height = (parseInt(strContent.style.height.replace("px", "")) + incrementH) + "px";
           },
           delayTime: 10,
           type: "zoom",
           speed: 50
       });
            sb3.Start();
        }
        else {
            var sb2 = new czyjs.Effect.StoryBoard(
       {
           id: this.obj,
           startSize: { w: curWidth, h: curHeight },
           endSize: { w: curWidth - this.resizeValue, h: curHeight - this.resizeValue },
           // position: { x: this.left, y: this.top },
           changingEvent: function (obj, incrementW, incrementH) {
           },
           delayTime: 10,
           type: "zoom",
           speed: 50
       });
            sb2.Start();
        }

    }

}





