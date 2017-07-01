  function CenterWindow(flag) {
    this.ID = "CenterWindow";
    this.Width = 400; //窗口宽度
    this.Title = "";
    this.THeight = 30;
    this.TColor = "#000"; //标题颜色
    this.TBgColor = "#CFDEF8";
    this.Content = "";
    this.CColor = "#000000"; //内容颜色
    this.CBgColor = "#ffffff"; ///内容背景颜色
    this.Bottom = "";
    this.BColor = "#000000";
    this.BBgColor = "#E3E3E6";
    this.BTopBorderColor = "#A6A6A6";
    this.BHeight = 30;
    this.alphaWidth = 8; //透明边框宽度
    this.HasBottom = true; //是否有底部
    this.TClassName = "centerwindowtitle"; //标题栏样式名称
    this.Zhezhao = true;
    this.Confirm = function() {

    }
    this.Cancle = function() {

    }
    this.ConfirmButton = "确定";
    this.CancleButton = "取消";
    var ID = this.ID;
    this.Close = function() {
        var div = document.getElementById(ID);
        if (div) {
            div.parentNode.removeChild(div);
        }
        var aDiv = document.getElementById("a" + ID);
        if (aDiv) {
            aDiv.parentNode.removeChild(aDiv);
        }
        var div = document.getElementById(ID + "_zhezhao");
        if (div) { div.parentNode.removeChild(div); }
    }
    this.IconClose = function() {

    }
    var the = this;
    this.Create = function() {
        this.Close();
        if (the.Create.arguments.length > 0) {
            the.Zhezhao = false;
        }
        else {
            the.Zhezhao = true;
        }
        var HTML = '';
        var bottomHTML = '';
        if (this.ConfirmButton != "") 
        {
            bottomHTML += '<input type="button"  class=\"btn_blue4\" style=\"height:22px;margin-top:3px;\" value="' + this.ConfirmButton + '" id="centerwindowconfirm"/>';
        }
        if (this.CancleButton != "") 
        {
            bottomHTML += '&nbsp;&nbsp;<input type="button"  class=\"btn_gray4\" style=\"height:22px;margin-top:3px;\" value="' + this.CancleButton + '" id="centerwindowcancle"/>';
        }
        HTML += ' 	<div style="position:relative;text-align:left;padding:0px;height:100%" class="centerwindow">';
        HTML += ' 		<div>';
        HTML += ' 			<div style="width:100%;position:absolute;top:0px;" class="toptitle">';
        
        if (this.TClassName == "" || this.TClassName == null || this.TClassName == undefined) {
            HTML += ' 				<div id="' + this.ID + '_control" style="height:' + this.THeight + 'px;background-color:' + this.TBgColor + ';font-weight:600;line-height:' + this.THeight + 'px;color:' + this.TColor + ';padding-left:10px;padding-right:10px;" class="' + this.TClassName + '"><span style="float:left;">' + this.Title + '</span><span  style="float:right;padding-top:10px;"><a class="showok" id="centerwindowconfirm" href="javascript:void(0)" title="关闭"></a></span></div>';
        }
        else 
        {
            var ert='';
            if(flag!=0)
            {
                ert='';
            }
            HTML += ' 				<div id="' + this.ID + '_control" style="height:' + this.THeight + 'px;font-weight:600;line-height:' + this.THeight + 'px;padding-left:10px;padding-right:10px;" class="' + this.TClassName + '"><span style="float:left;">' + this.Title + '</span><span style="float:right;padding-top:10px;"><a id="centerwindowclose" href="javascript:void(0)"  title="关闭" class="showok"></a></span></div>';
        }

        HTML += ' 			</div>';
        HTML += ' 			<div style="height:100%;color:' + this.CColor + ';background-color:' + this.CBgColor + ';padding-top:' + this.THeight + 'px;padding-bottom:' + this.BHeight + 'px">';
        HTML += ' 				<div style="padding:10px 5px;"><div>';
        HTML += ' 				' + this.Content + '';
        HTML += ' 				</div></div>';
        HTML += '			</div>';
        if(flag==0)
        {
            if (this.HasBottom) 
            {
                HTML += '			<div style="width:100%;position:absolute;bottom:0px;">';
                HTML += '				<div style="height:' + this.BHeight + 'px;background-color:' + this.BBgColor + ';line-height:' + this.BHeight + 'px;text-align:center;padding-right:10px;border-width:1px 0px 0px 0px;border-style:solid;border-color:' + this.BTopBorderColor + '">' + bottomHTML + '</div>';
                HTML += '			</div>';
            }
        }
        HTML += '		</div>';
        HTML += '	</div>';
        var div = document.getElementById(this.ID);
        if (!div) {
            div = document.createElement("div");
            div.setAttribute("id", this.ID);
            div.setAttribute("systype", "centerwindow");
            document.body.appendChild(div);
        }
        div.innerHTML = HTML;
        div.style.position = "absolute";
        div.style.zIndex = "100";
        div.style.width = this.Width + "px";
        var scrolltop = document.documentElement.scrollTop;
        if (scrolltop == 0) {
            scrolltop = document.body.scrollTop;
        }
        var ofHeight = document.documentElement.offsetHeight;
        if (ofHeight > window.screen.availHeight) ofHeight = window.screen.availHeight;
        var scrollTop = scrolltop;
        if (scrollTop == 0) {
            try {
                scrollTop = parent.document.documentElement.scrollTop;
            } catch (e) { }
        }
        var top = (ofHeight) / 2 - div.offsetHeight / 2 + scrollTop - 50;
        var left = document.documentElement.offsetWidth / 2 - div.offsetWidth / 2;

        div.style.top = top + "px";
        div.style.left = left + "px";

        var aDiv = document.getElementById("a" + this.ID);
        if (!aDiv) {
            aDiv = document.createElement("div");
            aDiv.setAttribute("id", "a" + this.ID);
            document.body.appendChild(aDiv);
        }
        aDiv.style.position = "absolute";
        aDiv.style.filter = "alpha(opacity=20);";
        if (is_moz||is_opera||is_safari) 
        {
            aDiv.setAttribute("style", "position:absolute;width:" + w + "px;height:" + h + "px;top:" + y + "px;left:" + x + "px;z-index:" + z + ";background-color:#F7F7F7;filter:alpha(opacity=50);moz-opacity:50;opacity:50;;");
        }
        aDiv.style.backgroundColor = "#205880";
        aDiv.style.zIndex = "50";
        var w = this.Width + this.alphaWidth * 2;
        var h = div.offsetHeight + this.alphaWidth * 2;
        var x = (left - this.alphaWidth);
        var y = (top - this.alphaWidth);
        aDiv.style.width = w + "px";
        aDiv.style.height = h + "px";
        aDiv.style.top = y + "px";
        aDiv.style.left = x + "px"


        var Confirm = this.Confirm;
        var Cancle = this.Cancle;
        var Clo = this.Close;
        if (this.ConfirmButton != "" && document.getElementById("centerwindowconfirm")) {
            document.getElementById("centerwindowconfirm").onclick = Confirm;
        }
        if (this.CancleButton != "" && document.getElementById("centerwindowcancle")) {
            document.getElementById("centerwindowcancle").onclick = Cancle;
        }
        var iconClose = this.IconClose;
        if (flag == 0) {
            document.getElementById("centerwindowclose").onclick = function() { Clo(); Cancle(); iconClose(); };
        }
        if (the.Zhezhao) 
        {
            //遮照层
            var divID = this.ID + "_zhezhao";
            var ExsitDivObj = document.getElementById(divID);
            if (ExsitDivObj != null) ExsitDivObj.parentNode.removeChild(ExsitDivObj);
            var objDiv = document.createElement("div");
            objDiv.id = divID;
            objDiv.style.position = "absolute";
            document.body.appendChild(objDiv);
            var w = (document.documentElement.clientWidth == 0 ? document.body.clientWidth : document.documentElement.clientWidth);
            var h = (document.documentElement.scrollHeight == 0 ? document.body.scrollHeight : document.documentElement.scrollHeight);
            var x = 0;
            var y = 0;
            var z = 99;
            var backcolor = "#000000";
            objDiv.style.filter = "alpha(opacity=40);";
            if (is_moz||is_opera||is_safari) 
            {
                objDiv.setAttribute("style", "position:absolute;width:" + w + "px;height:" + h + "px;top:" + y + "px;left:" + x + "px;z-index:" + z + ";background-color:#243768;-moz-opacity:0.55;opacity: 0.55;");
            }
            objDiv.style.backgroundColor = backcolor;
            objDiv.style.top = y + 'px';
            objDiv.style.left = x + 'px';
            objDiv.style.width = w + "px";
            objDiv.style.height = h + "px";

            objDiv.style.zIndex = z;
        }
    }
}


//设置某个元素可以移动
function setObjMove(objid, controlobjid) {
    var obj = document.getElementById(objid);
    var cobj = document.getElementById(controlobjid);
    cobj.style.cursor = "move";
    if (!obj || !cobj) {
        return;
    }
    var scrip = '';
    var iLayerMaxNum = 10000;
    var $a;
    var $b = "";
    var $c = "";
    document.onmouseup = me;
    document.onmousemove = ms;
    var $d;
    var $e;
    cobj.onmousedown = function() {
        M(obj, event);
    }
    function M(Object, event) {
        scrip = Object.id;
        var cuobj = document.getElementById(scrip);
        if (document.all) {
            cuobj.setCapture();
            $d = event.clientX - cuobj.style.pixelLeft;
            $e = event.clientY - cuobj.style.pixelTop;
        } else if (window.captureEvents) {
            window.captureEvents(Event.MOUSEMOVE | Event.MOUSEUP);
            $d = event.layerX;
            $e = event.layerY;
        };

    };
    function ms($f) {
        if (scrip != '') {
            // top.document.title = scrip;
            var cuobj = document.getElementById(scrip);
            if (document.all) {
                cuobj.style.left = event.clientX - $d;
                cuobj.style.top = event.clientY - $e;
            } else if (window.captureEvents) {
                cuobj.style.left = ($f.clientX - $d) + "px";
                cuobj.style.top = ($f.clientY - $e) + "px";
            }
            // top.document.title += cuobj.style.pixelLeft + "," + cuobj.style.pixelTop;
        }
    };
    function me($f) {
        if (scrip != '') {
            if (document.all) {
                document.getElementById(scrip).releaseCapture(); scrip = '';
            } else if (window.captureEvents) {
                window.captureEvents(Event.MOUSEMOVE | Event.MOUSEUP); scrip = '';
            }
        }
    };
}

function WinTip(title, msg) {
    var wi = new CenterWindow(0);
    wi.Title = title;
    wi.Content = msg;
    wi.CancleButton = "";
    wi.ConfirmButton = "确定";
    wi.Confirm = function() {
        wi.Close();
    }
    wi.Create(0);
    return wi;
}
function WinTip1(title, msg) {
    var wi = new CenterWindow(1);
    wi.Title = title;
    wi.Content = msg;
    wi.CancleButton = "";
    wi.ConfirmButton = " 关 闭 ";
    wi.Confirm = function() {
        wi.Close();
    }
    wi.Create();
    return wi;
}

function getEvent() {     //同时兼容ie和ff的写法
    if (document.all) return window.event;
    func = getEvent.caller;
    while (func != null) {
        var arg0 = func.arguments[0];
        if (arg0) {
            if ((arg0.constructor == Event || arg0.constructor == MouseEvent)
                         || (typeof (arg0) == "object" && arg0.preventDefault && arg0.stopPropagation)) {
                return arg0;
            }
        }
        func = func.caller;
    }
    return null;
}