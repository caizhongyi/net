if(typeof(czyjs.UI)=="undefined")
{
    czyjs.UI = {};
};

if(typeof(czyjs.UI.Controls)=="undefined")
{
    czyjs.UI.Controls = {};
};

//可不设宽和高
czyjs.UI.Controls.GridView=Class.create();
czyjs.UI.Controls.GridView.prototype = {
    initialize: function (param) {
         
        this.width=param.width==null?"auto":param.width;   
        this.height=param.height==null?"auto":param.height;
        this.borderPix = param.borderPix == null ? { width: 10, height: 10} : param.borderPix;
        this.data = param.data;     //示例:{head:[{text:"head",width:100},{text:"head",width:100}],body:[["data","data"],["data1","data1"],["data2","data2"]]}
        this.upDataEvent = param.upDataEvent;
        this.deleteEvent = param.deleteEvent;
        this.id = param.id == null ? "czy-gridView" : param.id;
        this.obj = document.getElementById(param.randerTo) == null ? param.randerTo : document.getElementById(param.randerTo);
        this.isOperation = param.isOperation == null ? true : false;
        this.operText = param.operText == null ? { updateText: "修改", deleteText: "删除"} : param.operText;
        this.className = param.className == null ? "czy-grid" : param.className;
        this.footHTML = param.footHTML == null ? "" : param.footHTML;
        this.selectedIndex = 0;
        this.columnsLenght=this.data.head.length;
        this.rowsLenght=this.data.body.length;

        this._DataLoad = BindAsEventListener(this, this.DataLoad);
        this._UpData = BindAsEventListener(this, this.UpData);
        this._Delete = BindAsEventListener(this, this.Delete);

        addEventHandler(this.obj, "click", this._UpData);
        addEventHandler(this.obj, "click", this._Delete);
        this._DataLoad();
    },
    DataLoad: function () {

        var head = this.data.head;
        var body = this.data.body;
        var p= (1/(this.columnsLenght+2))*100;
        var html = "<div class='data-content'>";
        html += "<ul style=\"margin:0px;padding:0px;list-style-type:none; overflow:hidden;\" class=\"czy-grid-head\">";
        for (var i = 0; i < head.length; i++) {
            var width = head[i].width == null ? "width:"+p +"%;" : "width:" + head[i].width + "px;";
            html += "<li style=\"float:left;" + width + "\">" + head[i].text + "</li>";
            if (i == head.length - 1) {
                html += "<li style=\"float:left;" + width + "\" >" + this.operText.updateText + "</li>";
                html += "<li style=\"float:left;" + width + "\" >" + this.operText.deleteText + "</li>";
            }
        }
        html += "</ul>";
        html += "<ul  style=\"margin:0px;padding:0px;list-style-type:none;\" class=\"czy-grid-body\">";
        for (var m = 0; m < body.length; m++) {
            html += "<li>";
            html += "<ul style=\"margin:0px;padding:0px;list-style-type:none;overflow:hidden\" class=\"czy-grid-row\" index='" + m + "'>";
            for (var j = 0; j < body[m].length; j++) {
                var width = head[j].width == null ? "width:"+p +"%;" : "width:" + head[j].width + "px;";
                html += "<li style=\"float:left;" + width + "\">" + body[m][j] + "</li>";
                if (j == body[m].length - 1) {
                    html += "<li style=\"float:left;cursor:pointer;" + width + "\" oper=\"Update\">" + this.operText.updateText + "</li>";
                    html += "<li style=\"float:left;cursor:pointer;" + width + "\" oper=\"Delete\">" + this.operText.deleteText + "</li>";
                }
            }
            html += "</ul>";
            html += "<div style='clear:both;'></div>";
            html += "</li>";
        }
        html += "</ul>";
        html += "</div>";
        html += this.footHTML;
        
        var s_width=this.width=="auto"?"":"width:"+this.width+"px;";
        var s_height=this.height=="auto"?"":"width:"+this.height+"px;";
        var borderHTML = "<div id='"+this.id+"'  class='"+this.className+"' style='"+s_width+s_height+"'>";
            borderHTML += "<div class='t-border' >";
            borderHTML += "<div class='t-topLeft div-left'   style='width:"+this.borderPix.width+"px;height:"+this.borderPix.height+"px; '  ></div>";
            borderHTML += "<div class='t-topRight div-right'  style='width:"+this.borderPix.width+"px;height:"+this.borderPix.height+"px; ' ></div>";
            borderHTML += "<div class='t-topCenter' style='height:"+this.borderPix.height+"px' ></div>";
          
            borderHTML += "<div >";
            borderHTML += "<div class='t-middleLeft div-left' style='width:"+this.borderPix.width+"px;'></div>";
            borderHTML += "<div class='t-middleRight div-right' style='width:"+this.borderPix.width+"px; '></div>";
            borderHTML += "<div class='t-middleCenter'   style='width:"+(this.width-this.borderPix.width)+"px; height:"+(this.height-this.borderPix.height*2)+"px; ' >";
            borderHTML += html;
            borderHTML += "</div>";
            borderHTML += "</div>";
            

            borderHTML += "<div class='t-bottomLeft div-left'   style='width:"+this.borderPix.width+"px;height:"+this.borderPix.height+"px' ></div>";
            borderHTML += "<div class='t-bottomRight div-right'  style='width:"+this.borderPix.width+"px;height:"+this.borderPix.height+"px'  ></div>";
            borderHTML += "<div class='t-bottomCenter ' style='height:"+this.borderPix.height+"px' ></div>";
            borderHTML += "</div>";
            borderHTML += "</div>";
        this.obj.innerHTML = borderHTML;

    },
    UpData: function () {
        var o = czyjs.Event.GetEventObj();
        if (o.oper != null && o.oper == "Update") {
            if (this.upDataEvent != null) {
                this.selectedIndex = o.parentNode.index;
                var dataRow = this.data.body[this.selectedIndex];
                this.upDataEvent(this.selectedIndex, dataRow);
            }

        }

    },
    Delete: function () {
        var o = czyjs.Event.GetEventObj();
        if (o.oper != null && o.oper == "Delete") {
            if (this.deleteEvent != null) {
                this.selectedIndex = o.parentNode.index;
                var dataRow = this.data.body[this.selectedIndex];
                this.deleteEvent(this.selectedIndex, dataRow);
            }

        }
    }


}
