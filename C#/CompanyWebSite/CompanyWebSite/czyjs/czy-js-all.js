
/*** 命名空间 ***/
/*
* if (typeof(window.Com) == "undefined")//Ҳ����ʹ�ã�
* if (typeof(Com) == "undefined")
{
window.Com = {};//Ҳ����ʹ�ã�window.Com = new Object();
}

*/

if(typeof(czyjs) == "undefined")
{
    czyjs = new Object();
}

//if(typeof(BrcLib.JavaScript)=="undefined")
//{
//    BrcLib.JavaScript = {};
//}

//if(typeof(BrcLib.JavaScript.File)=="undefined")
//{
//    BrcLib.JavaScript.File = {};
//}
//��BrcLib.JavaScript.Message
//if(typeof(BrcLib.JavaScript.Message)=="undefined")
//{
//    BrcLib.JavaScript.File = {};
//}
var hostName="";
var list=document.getElementsByTagName("script");
for(var i=0;i<list.length;i++)
{

	if(list[i].src.substr(list[i].src.lastIndexOf('/')+1)=="czy-js-all.js")
	{
		hostName=list[i].src.substr(0,list[i].src.lastIndexOf('/')+1);
	}
}
var urlList={
	baseClass       :hostName+"js/czy-base-class.js",
	ajaxPager       :hostName+"js/czy-ajax-pager.js",
	ajaxScript      :hostName+"js/czy-ajaxscript.js",
	changeStyle     :hostName+"js/czy-effect-style.js",
	checkboxDiv     :hostName+"js/czy-checkbox-div.js",
	easyForm        :hostName+"js/czy-easy-form.js",
	Datetime        :hostName+"js/czy-datetime.js",
	datetimeHelper  :hostName+"js/czy-datetime-helper.js",
	validate        :hostName+"js/czy-validate.js",
	convent         :hostName+"js/czy-convert.js",
	element         :hostName+"js/czy-element.js",
	event           :hostName+"js/czy-event.js",
	format          :hostName+"js/czy-format.js",
	util            :hostName+"js/czy-util.js",
	showPic         :hostName+"js/czy-pic-show.js",
	picscroll       :hostName+"js/czy-pictures-pos.js",
	formValidate    :hostName+"js/czy-form-validate.js",
	loading         :hostName+"js/czy-loading.js",
	select          :hostName+"js/czy-control-select.js",
	datagrid        :hostName+"js/czy-control-gridview.js",
    loginForm       :hostName+"js/czy-login-form.js",
    effect          :hostName+"js/czy-effect-text.js",
    tabView         :hostName + "js/czy-control-tab.js",
    clock           :hostName + "js/czy-datatime-clock.js",
    storyboard      :hostName + "js/czy-effect-storyboard.js",
    nav             :hostName + "js/czy-control-nav.js",
    
    /*YUI*/
	yui_min         :hostName + "js/yui/build/yui/yui-min.js",
    //Yahoo UpLoad
	yahoo_dom_event :hostName + "js/yui/yahoo-dom-event.js",
	element_min     :hostName + "js/yui/element-min.js",
	uploader_min    :hostName + "js/yui/uploader-min.js",
	czy_yahoo_upload: hostName + "js/czy-yahoo-upload.js",
	//ExtJs
    
    ext_base: hostName + "js/ext/adapter/ext/ext-base.js",
    ext_all: hostName + "js/ext/ext-all.js",
    png             :hostName + "js/DD_belatedPNG_0.0.8a.js"
 
}

/*加载所以Js文件集合 */


/*YUI*/
document.write(unescape("%3Cscript language='javascript' src='"+urlList.baseClass+"' %3E%3C/script%3E"));
document.write(unescape("%3Cscript type='text/javascript'  src='" + urlList.czy_yahoo_upload + "' %3E%3C/script%3E"));
document.write(unescape("%3Cscript language='javascript' src='" + urlList.ext_base + "'%3E%3C/script%3E"));
document.write(unescape("%3Cscript language='javascript' src='" + urlList.ext_all + "'%3E%3C/script%3E"));
document.write(unescape("%3Cscript type='text/javascript'  src='"+urlList.yui_min+"' %3E%3C/script%3E"));
/*YahooUpLoad*/
document.write(unescape("%3Cscript type='text/javascript'  src='"+urlList.yahoo_dom_event+"' %3E%3C/script%3E"));
document.write(unescape("%3Cscript type='text/javascript'  src='"+urlList.element_min+"' %3E%3C/script%3E"));
document.write(unescape("%3Cscript type='text/javascript'  src='"+urlList.uploader_min+"' %3E%3C/script%3E"));



//js文件引用
document.write(unescape("%3Cscript language='javascript' src='"+urlList.util+"'%3E%3C/script%3E"));
document.write(unescape("%3Cscript language='javascript' src='"+urlList.validate+"'%3E%3C/script%3E"));
document.write(unescape("%3Cscript language='javascript' src='"+urlList.ajaxScript+"'%3E%3C/script%3E"));

document.write(unescape("%3Cscript language='javascript' src='"+urlList.ajaxPager+"'%3E%3C/script%3E"));



document.write(unescape("%3Cscript language='javascript' src='"+urlList.changeStyle+"'%3E%3C/script%3E"));
document.write(unescape("%3Cscript language='javascript' src='"+urlList.checkboxDiv+"'%3E%3C/script%3E"));
document.write(unescape("%3Cscript language='javascript' src='"+urlList.easyForm+"'%3E%3C/script%3E"));
document.write(unescape("%3Cscript language='javascript' src='" + urlList.loginForm + "'%3E%3C/script%3E"));
document.write(unescape("%3Cscript language='javascript' src='" + urlList.nav + "'%3E%3C/script%3E"));

document.write(unescape("%3Cscript language='javascript' src='"+urlList.Datetime+"'%3E%3C/script%3E"));
document.write(unescape("%3Cscript language='javascript' src='" + urlList.datetimeHelper + "'%3E%3C/script%3E"));
document.write(unescape("%3Cscript language='javascript' src='" + urlList.clock + "'%3E%3C/script%3E"));

document.write(unescape("%3Cscript language='javascript' src='"+urlList.validate+"'%3E%3C/script%3E"));

document.write(unescape("%3Cscript language='javascript' src='"+urlList.convent+"'%3E%3C/script%3E"));
document.write(unescape("%3Cscript language='javascript' src='"+urlList.element+"'%3E%3C/script%3E"));
document.write(unescape("%3Cscript language='javascript' src='"+urlList.event+"'%3E%3C/script%3E"));
document.write(unescape("%3Cscript language='javascript' src='"+urlList.format+"'%3E%3C/script%3E"));
document.write(unescape("%3Cscript language='javascript' src='"+urlList.showPic+"'%3E%3C/script%3E"));
document.write(unescape("%3Cscript language='javascript' src='"+urlList.picscroll+"'%3E%3C/script%3E"));


document.write(unescape("%3Cscript language='javascript' src='"+urlList.formValidate+"'%3E%3C/script%3E"));
document.write(unescape("%3Cscript language='javascript' src='"+urlList.loading+"'%3E%3C/script%3E"));
document.write(unescape("%3Cscript language='javascript' src='"+urlList.select+"'%3E%3C/script%3E"));
document.write(unescape("%3Cscript language='javascript' src='"+urlList.datagrid+"'%3E%3C/script%3E"));
document.write(unescape("%3Cscript language='javascript' src='"+urlList.tabView+"'%3E%3C/script%3E"));

document.write(unescape("%3Cscript language='javascript' src='"+urlList.effect+"'%3E%3C/script%3E"));
document.write(unescape("%3Cscript language='javascript' src='"+urlList.storyboard+"'%3E%3C/script%3E"));

document.write(unescape("%3Cscript language='javascript' src='"+urlList.png+"'%3E%3C/script%3E"));



