function isMatch(str1,str2) 
{  
var index = str1.indexOf(str2); 
if(index==-1) return false; 
return true; 
} 

function ResumeError() { 
return true; 
} 
window.onerror = ResumeError; 

function $(id) {
    return document.getElementById(id);
}
// 相对尺寸
function GetOffsetTop (el, p) {
    var _t = el.offsetTop;
    var _p = el.offsetParent;

    while (_p) {
        if (_p == p) break;
        _t += _p.offsetTop;
        _p = _p.offsetParent;
    }

    return _t;
};
function GetOffsetLeft (el, p) {
    var _l = el.offsetLeft;
    var _p = el.offsetParent;

    while (_p) {
        if (_p == p) break;
        _l += _p.offsetLeft;
        _p = _p.offsetParent;
    }

    return _l;
};
function showMenu (baseID, divID) {
    baseID = $(baseID);
    divID  = $(divID);

    //var l = GetOffsetLeft(baseID);
    //var t = GetOffsetTop(baseID);

    //divID.style.left = l + 'px';
//    divID.style.top = t + baseID.offsetHeight + 'px';
    if (showMenu.timer) clearTimeout(showMenu.timer);
	hideCur();
    divID.style.display = 'block';
	showMenu.cur = divID;

    if (! divID.isCreate) {
        divID.isCreate = true;
        //divID.timer = 0;
        divID.onmouseover = function () {
            if (showMenu.timer) clearTimeout(showMenu.timer);
			hideCur();
            divID.style.display = 'block';
        };

        function hide () {
            showMenu.timer = setTimeout(function () {divID.style.display = 'none';}, 1000);
        }

        divID.onmouseout = hide;
        baseID.onmouseout = hide;
    }
	function hideCur () {
		showMenu.cur && (showMenu.cur.style.display = 'none');
	}
}

function doClick_down(o){
	 o.className="current";
	 var j;
	 var id;
	 var e;
	 for(var i=1;i<=7;i++){
	   id ="down"+i;
	   j = document.getElementById(id);
	   e = document.getElementById("d_con"+i);
	   if(id != o.id){
	   	 j.className="";
	   	 e.style.display = "none";
	   }else{
		e.style.display = "block";
	   }
	 }
	 }
	 
function doClick_jy(o){
	 o.className="current";
	 var j;
	 var id;
	 var e;
	 for(var i=1;i<=8;i++){
	   id ="jy"+i;
	   j = document.getElementById(id);
	   e = document.getElementById("jy_con"+i);
	   if(id != o.id){
	   	 j.className="";
	   	 e.style.display = "none";
	   }else{
		e.style.display = "block";
	   }
	 }
	 }


function doZoom(size){
	document.getElementById('textbody').style.fontSize=size+'px'
}


/// 修改及新增
function doClick_menu (o) {
	o.className = 'menu_gg';
	var j, id, e;
	for (var i = 1; i <= 10; i++) {
	    id = 'menu' + i;
	    j = document.getElementById(id);
	    e = document.getElementById('menu_con' + i);
	    if (id != o.id) {
	   	   j.className = '';
	   	   e.style.display = 'none';
	    }
        else {
		    e.style.display = 'block';
	    }
	 }
     var url = '';
     switch (o.innerHTML) {
        case '全站全文搜索':
            url = '/plus/search.php?searchtype=titlekeyword&keyword=';
            break;
        case '下载':
            url = 'http://down.chinaz.com/query.asp?q=';
            break;
        case '论坛':
            url = 'http://bbs.chinaz.com/Search.html?mode=1&searchText=';
            break;
        case '博客':
            url = 'http://my.chinaz.com/list.asp?selecttype=topic&keyword=';
            break;
        case '交易':
            url = 'http://1m.chinaz.com/Search.asp?keyword=';
            break;
        case 'GOOGLE':
            url = 'http://google.chinaz.com/custom?hl=zh-CN&inlang=zh-CN&ie=GB2312&newwindow=1&cof=AWFID%3A0b9847e42caf283e%3BL%3Ahttp%3A%2F%2Fwww.chinaz.com%2FIMAGES%2Flogo.gif%3BLH%3A60%3BLW%3A165%3BBGC%3Awhite%3BT%3A%23000000%3BLC%3A%230000cc%3BVLC%3A%23551A8B%3BALC%3A%23ff3300%3BGALT%3A%23008000%3BGFNT%3A%237777CC%3BGIMP%3A%23FF0000%3BDIV%3A%23000099%3BLBGC%3Awhite%3BAH%3Aleft%3B&domains=chinaz.com&sitesearch=chinaz.com&q=';
            break;
        case 'ALEXA':
            url = 'http://alexa.chinaz.com/index.asp?domain=';
            break;
        case 'IP归属':
            url = 'http://tool.chinaz.com/Ip/Index.asp?ip=';
            break;
        case 'Whois':
            url = 'http://tool.chinaz.com/whois/';
            break;
		case '素材':
            url = 'http://sc.chinaz.com/Search.aspx?page=1&SearchWord=';
            break;
     }
     doClick_submit.url = url;
}
doClick_submit.url = '/plus/search.php?searchtype=titlekeyword&keyword=';
function doClick_submit () {
    var keyword = document.getElementsByName('keyword')[0].value;
    window.open(doClick_submit.url + keyword);
}
window.onload = function (){
	var keyword = document.getElementsByName('keyword')[0];
	keyword.onkeydown = function (e) {
		e = e || window.event;
		if (e.keyCode == 13) {
			doClick_submit();
		}
	};
}