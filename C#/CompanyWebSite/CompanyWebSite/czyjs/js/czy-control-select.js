if(typeof(czyjs.UI)=="undefined")
{
    czyjs.UI.Controls = {};
};

if(typeof(czyjs.UI.Controls)=="undefined")
{
    czyjs.UI.Controls = {};
};

/*
 * 选择下拉图片地址
 */

czyjs.UI.Controls.SelectImg=hostName+"images/controls/czy-select.gif";
czyjs.UI.Controls.SelectImgOn=hostName+"images/controls/czy-select-on.gif";

czyjs.UI.Controls.Select=Class.create();
czyjs.UI.Controls.Select.prototype = {
    /*
    * select 控件
    * data:json数据集  示例:[{text:"text",value:"value"}]
    * widht:宽
    * height:高
    * id:select控件ID
    * contenterId:加载容器id
    * enable:是否可输入
    * listHeight:下拉框的高度,默认300
    */
    initialize: function (param) {

        this.sep = 30;
        this.textDefaultValue = "请选择...";
        this.cssConfig = {
            text: "czy-select-text",
            selectImg: "czy-select-img",
            selectList: "czy-select-list",
            mouseOn: "czy-select-mouseOn",
            mouseOut: "czy-select-mouseOut"
        }

        this.config = {
            data: param.data,
            txtField: param.txtField,
            valueField: param.valueField,
            width: param.width,
            height: param.height,
            id: param.id,
            contenterId: param.randerTo,
            enable: param.disable,
            listHeight: param.listHeight
        }


        if (this.config.enable == null) {
            this.config.enable = true;
        }
        if (this.config.listHeight == null) {
            this.maxHeight = 300;
        }
        else {
            this.maxHeigh = this.config.listHeight;
        }


        this.valueList = new Array();
        this.width = this.config.width;
        this.height = this.config.height;
        this.selectIndex = 0; //当前选择ID
        //this._selectIndex=BindAsEventListener(this,this.selectIndex);
        this.cout = 0; //总个数
        this.selectValue = "";
        this.selectText = "";

        this.data = this.config.data;
        this.cout = this.data.length;

        this.contentObj = document.getElementById(this.config.contenterId) == null ? this.config.contenterId : document.getElementById(this.config.contenterId);

        this.selectObj = document.createElement("div"); //总容器对像
        this.textObj = document.createElement("input"); //左边的text
        this.selectImgObj = document.createElement("img"); //右边的I图片
        this.selectListObj = document.createElement("div"); //下拉list div
        this.selectUl = document.createElement("ul");

        this.selectObj.id = this.config.id;
        this.selectObj.style.width = this.config.width + "px";
        this.selectObj.style.height = this.config.height + "px";
        this.contentObj.groupId = "selectGroup";
        this.selectObj.groupId = "selectGroup";
        this.textObj.groupId = "selectGroup";
        this.selectImgObj.groupId = "selectGroup";
        this.selectUl.groupId = "selectGroup";
        this.selectListObj.groupId = "selectGroup";

        this.textObj.className = this.cssConfig.text;
        this.selectObj.className = this.cssConfig.selectImg;
        this.selectListObj.className = this.cssConfig.selectList;

        this.selectUl.style.listStyleType = "none";
        this.selectUl.style.margin = "0px";
        this.selectUl.style.overflow = "hidden";




        this.selectListObj.style.position = "absolute";
        this.selectListObj.style.top = (this.config.height + 1) + "px";
        this.selectListObj.style.left = "0px";
        //this.selectListObj.style.overflow="hidden";
        this.selectListObj.style.display = "none";
        this.selectListObj.style.height = "0px";
        //this.selectListObj.style.backgroundColor="Red";
        this.selectListObj.style.width = this.config.width + "px";


        this.selectObj.style.position = "relative";
        this.selectObj.id = this.config.id;



        this.selectImgObj.style.height = this.config.height + "px";
        this.selectImgObj.style.width = "10%";
        this.selectImgObj.src = czyjs.UI.Controls.SelectImg;
        if (document.all) {
            this.selectImgObj.style.marginBottom = "-4px";
        }
        else {
            this.selectImgObj.style.marginBottom = "-5px";
        }
        this.selectImgObj.style.cursor = "pointer";
        this.selectImgObj.className = this.cssConfig.selectImg;

        this.textObj.style.width = "90%";
        this.textObj.style.height = this.config.height + "px";
        this.textObj.value = this.textDefaultValue;
        //this.textObj.style.lineHeight=this.config.height;
        this.textObj.type = "text";
        this.textObj.id = this.config.id + "Text";
        if (!this.config.enable) {
            this.textObj.disabled = "disabled";
        }


        this.selectObj.appendChild(this.textObj);
        this.selectObj.appendChild(this.selectImgObj);
        this.selectObj.appendChild(this.selectListObj);


        this.selectListObj.appendChild(this.selectUl);

        this.contentObj.appendChild(this.selectObj);


        //增加数据
        for (var i = 0; i < this.data.length; i++) {
            this.valueList.push(this.data[i][0]);
            this.selectUl.innerHTML +=
			"<li index=\"" + i + "\" groupId=\"selectGroup\"   style=\"padding-top:5px; padding-bottom:5px;\">" + this.data[i][1] + "</li>";

        }
        this.selectListObj.style.height = this.maxHeight + "px";
        this.selectListObj.style.top = this.config.height + "px";

        //document.getElementById("drag").innerHTML=height;
        //事件
        this._ShowList = BindAsEventListener(this, this.ShowList);
        this._HiddenList = BindAsEventListener(this, this.HiddenList);
        this._Selected = BindAsEventListener(this, this.Selected);
        this._LostPoer = BindAsEventListener(this, this.LostPoer);
        this._Mouseover = BindAsEventListener(this, this.Mouseover);
        this._Mouseout = BindAsEventListener(this, this.Mouseout);
        this._SelectMouseOverEvent = BindAsEventListener(this, this.SelectMouseOverEvent);
        this._SelectMouseOutEvent = BindAsEventListener(this, this.SelectMouseOutEvent);
        this._TextOnfocusEvent = BindAsEventListener(this, this.TextOnfocusEvent);
        this._TextOnblurEvent = BindAsEventListener(this, this.TextOnblurEvent);
        this._UlMouseout = BindAsEventListener(this, this.UlMouseout);

        addEventHandler(this.selectImgObj, "click", this._LostPoer);
        addEventHandler(this.selectImgObj, "mouseover", this._SelectMouseOverEvent);
        addEventHandler(this.selectImgObj, "mouseout", this._SelectMouseOutEvent);
        addEventHandler(this.selectListObj, "click", this._Selected);
        addEventHandler(this.selectListObj, "mouseover", this._Mouseover);
        addEventHandler(this.selectListObj, "mouseout", this._Mouseout);
        addEventHandler(this.selectObj, "mousemove", this._UlMouseout);
        addEventHandler(this.textObj, "focus", this._TextOnfocusEvent);
        addEventHandler(this.textObj, "blur", this._TextOnblurEvent);
    },
    /*
    * 显示和隐藏下拉框操作
    */
    LostPoer: function () {

        var clientHeigth = document.body.clientHeight;

        var e = czyjs.Event.GetEvent();


        if (e.clientY + this.maxHeight > clientHeigth) {
            this.sep = -this.sep;
            this.selectListObj.style.top = "0px";
        }

        if (this.selectListObj.style.display == "none") {
            this.ShowList();
        }
        else {
            this.HiddenList();
        }
    },
    /*
    * 显示下拉框
    */
    ShowList: function () {

        // var height = parseInt((this.selectListObj.style.height.replace("px", "")));
        this.selectListObj.style.display = "block";
        var sb = new czyjs.Effect.StoryBoard(
       {
           id: this.selectListObj,
           startSize: { w: this.config.width, h: 0 },
           endSize: { w: this.config.width, h: this.maxHeight },
           delayTime: 10,
           type: "size",
           speed: 50
       });
        sb.Start();


    },
    /*
    * 隐藏下拉框
    */
    HiddenList: function () {
        //var height = parseInt(this.selectListObj.style.height.replace("px", ""));

        var sb = new czyjs.Effect.StoryBoard(
       {
           id: this.selectListObj,
           startSize: { w: this.config.width, h: this.maxHeight },
           endSize: { w: this.config.width, h: 0 },
           changedEvent: function (e, x, y) { e.style.display = "none"; },
           delayTime: 10,
           type: "size",
           speed: 50
       });
        sb.Start();


    },
    /*
    * 选择事件
    */
    Selected: function () {
        var e = czyjs.Event.GetEventObj();
        if (e.tagName == "LI") {
            this.selectIndex = e.index; //当前选择ID
            this.selectValue = this.data[this.selectIndex][0]
            this.selectText = this.data[this.selectIndex][1];
            this.textObj.value = this.selectText;
            this.HiddenList();
        }

    },
    /*
    * 鼠标放在下拉宽的效果
    */
    Mouseover: function () {

        var e = czyjs.Event.GetEventObj();
        if (e.tagName == "LI") {
            e.className = this.cssConfig.mouseOn;
            //this.ShowList();
        }
    },
    /*
    *  鼠标移出范围隐藏list
    */
    Mouseout: function () {
        var e = czyjs.Event.GetEventObj();
        if (e.tagName == "LI") {
            e.className = this.cssConfig.mouseOut;

        }


    }
	,

    /*
    * 下拉选择效果
    */
    SelectMouseOverEvent: function () {
        this.selectImgObj.src = czyjs.UI.Controls.SelectImgOn;
    },
    SelectMouseOutEvent: function () {
        this.selectImgObj.src = czyjs.UI.Controls.SelectImg;
    },
    /*
    * 文件效果
    */
    TextOnfocusEvent: function () {
        if (this.selectText == "" && this.config.enable == true) {
            this.textObj.value = "";
        }
    },
    TextOnblurEvent: function () {
        if (this.selectText == "" && this.config.enable == true) {
            this.textObj.value = this.textDefaultValue;
        }
    },
    /*
    * 鼠标移出范围隐藏list
    */
    UlMouseout: function () {
        var e = czyjs.Event.GetEventObj();

        if (e.groupId != "selectGroup") {
            this.HiddenList();
        }


    }

}
