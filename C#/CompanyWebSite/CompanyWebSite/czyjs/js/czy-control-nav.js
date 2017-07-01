if(typeof(czyjs.UI)=="undefined")
{
    czyjs.UI.Controls = {};
};

if(typeof(czyjs.UI.Controls)=="undefined")
{
    czyjs.UI.Controls = {};
};


czyjs.UI.Controls.Nav=Class.create();
czyjs.UI.Controls.Nav.prototype = {
    initialize: function (param) {

        this.width = param.width;
        this.height = param.height;
        this.data = param.data;
    
        this.itemWidth = this.width / this.data.lenght;
        this.resizeValue = 20; //导航子项放大的值

    },
    CreateUI: function () {
        var ui = "";
        ui += "<ul id='czy-nav' style='margin:0px;list-style-type:none;position:relative'  >";
        for (var i = 0; i < data.length; i++) {
            ui += "<li id='czy-nav_" + i + "' style='width:" + this.itemWidth + "px;height:" + this.height + "px; float:left;left:" + this.itemWidth * i + "px;top:0px'>";
            ui += data[i].item;
            var subItems = data.subItems;

            if (subItems != null) {
                ui += "<ul id='czy-nav-sub' style='height:0px;margin:0px;list-style-type:none;'>";
                for (var j = 0; j < subItems.lenght; j++) {
                    ui += "<li id='czy-nav-sub_'" + j + ">" + j.subItems[i] + "</li>";
                }
                ui += "</ul>";
            }
            ui += "</li>";
        }
        ui += "</ul>";

        var nav = document.getElementById("czy-nav");
        if (nav != null) {
            this._OnMouseOver = BindAsEventListener(this, this.OnMouseOver);
            this._OnMouseOut = BindAsEventListener(this, this.OnMouseOut);
            addEventHandler(nav, "mouseover", this._OnMouseOver);
            addEventHandler(nav, "mouseout", this._OnMouseOut);
        }
    },


    OnMouseOver: function () {
        var o = czyjs.Event.GetEventObj();
        this.left = parseFloat(o.style.left.replace("px", ""));
        this.top = parseFloat(o.style.top.replace("px", ""));
        if (o.tagName == "LI" && o.parentNode.id == "czy-nav") {
            var sb = new czyjs.Effect.StoryBoard(
        {
            id: o.id,
            startSize: { w: this.itemWidth, h: this.height },
            endSize: { w: this.itemWidth + this.resizeValue, h: this.itemWidth + this.resizeValue },
            position: { x: this.left, y: this.top },
            delayTime: 10,
            type: "zoom",
            speed: 50
        });
            var sb1 = new czyjs.Effect.StoryBoard(
        {
            id: o.id,
            startSize: { w: this.itemWidth, h: this.height },
            endSize: { w: 0, h: 0 },
            position: { x: 100, y: 100 },
            delayTime: 10,
            type: "zoom",
            speed: 50
        });

        }
    },
    OnMouseOut: function () {
        var o = czyjs.Event.GetEventObj();
        this.width =
        this.left = parseFloat(o.style.left.replace("px", ""));
        this.top = parseFloat(o.style.top.replace("px", ""));
        if (o.tagName == "LI" && o.parentNode.id == "czy-nav") {
            var sb = new czyjs.Effect.StoryBoard(
        {
            id: o.id,
            startSize: { w: this.itemWidth + this.resizeValue, h: this.height + this.resizeValue },
            endSize: { w: this.itemWidth, h: this.height },
            position: { x: this.left, y: this.top },
            delayTime: 10,
            type: "zoom",
            speed: 50
        });
        }
    }
}