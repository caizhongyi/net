var RootDir='';var ExName='.aspx';
function checkall(form){
	for (var i=0;i<form.elements.length;i++){
		var e = form.elements[i];
		if (e.name != 'all')
		e.checked = form.all.checked;
	}
}
var userAgent = navigator.userAgent.toLowerCase();
var is_opera = userAgent.indexOf('opera') != -1 && opera.version();
var is_moz = (navigator.product == 'Gecko') && userAgent.substr(userAgent.indexOf('firefox') + 8, 3);
var is_ie = (userAgent.indexOf('msie') != -1 && !is_opera) && userAgent.substr(userAgent.indexOf('msie') + 5, 3);
var is_safari = (userAgent.indexOf('webkit') != -1 || userAgent.indexOf('safari') != -1);
/*加载*/
jQuery(document).ready(function(){
      var roll=jQuery("ul.annbodylis li");
      var Num=roll.length;
      var i=0; 
      setInterval(function()
      {
           roll.eq(i).fadeOut(1000);
           roll.eq(i+1).fadeIn(500);
           i++;
           if(i>=Num){i=0;roll.eq(i).fadeIn(500);}
      },5000); 
      
	jQuery("ul.stopnav li span").mouseover(function() { 
		jQuery(this).parent().find("ul.subnav").slideDown('fast').show(); 
		jQuery(this).parent().hover(function() {
		}, function(){	
			jQuery(this).parent().find("ul.subnav").slideUp('slow'); 
		});
		}).hover(function() { 
			jQuery(this).addClass("subhover"); //On hover over, add class "subhover"
		}, function(){	//On Hover Out
			jQuery(this).removeClass("subhover"); //On hover out, remove class "subhover"
	});

	jQuery("ul.topusermenu li span").mouseover(function() 
	{ 
		jQuery(this).parent().find("ul.friendlisttop").slideDown('fast').show(); 
		jQuery(this).parent().hover(function() {
		}, function(){	
			jQuery(this).parent().find("ul.friendlisttop").slideUp('fast'); 
		});
		}).hover(function() { 
			$(this).addClass("subhover"); //On hover over, add class "subhover"
		}, function(){	//On Hover Out
			$(this).removeClass("subhover"); //On hover out, remove class "subhover"
	});
	
  var rhtml = ($("#toprights").html());
  if(rhtml)
  {
        showRightResult(rhtml);
  }
  var ehtml = ($("#toperrors").html());
  if(ehtml)
  {
        showErrorResult(ehtml);
  }
}); 

//是否是电子邮件
function isEmail(strEmail)
 {
    if (strEmail.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1)
    {
        return false;
    }
    else
    {
        return true;
    }
}

function isNumber(obj)
{
    if(isNaN(obj.value)) obj.value="";
}

///检查是否是正确的用户名
function isusername(uname)
{
   var re = /^[0-9a-z\.\@\-_ ]+$/gi;
   return !re.test(uname);
}

function passKeydown(e) 
{
        e = e || event;
        if (e.keyCode == 13) 
        {
            chkbannerForm();
        }
}


function showmsg(data)
{
    var Ws = WinTip("操作结果", data);
    setTimeout(function(){Ws.Close();}, 20000);
}

function showOK(did,flag)
{
    var html="操作失败";
    if(flag)
    {
        html="操作成功";
        html = "<div style=\"float:left;width:100%;border-bottom:1px dotted #83B623;padding-bottom:0px;margin-bottom:5px;line-height:20px;color:#467903;\"><span style=\"float:left;font-weight:normal;font-size:12px;padding-left:1px;\">"+html+"</span><span style=\"float:right\"><a title=\"关闭\" class=\"showok\" onclick=\"closediv('"+did+"')\" /></a></span></div>";
    }
    else
    {
        html = "<div style=\"float:left;width:100%;border-bottom:1px dotted #EC831D;padding-bottom:0px;margin-bottom:5px;line-height:20px;color:#D44904;\"><span style=\"float:left;font-weight:normal;font-size:12px;padding-left:1px;\">"+html+"</span><span style=\"float:right\"><a title=\"关闭\" class=\"showok1\" onclick=\"closediv('"+did+"')\" /></a></span></div>";
    }
    return html;
}

function closediv(div)
{
    jQuery(div).hide();
}

//显示正确信息
function showRightResult(msg)
{
    try
    {
        var htmls=showOK("#top_rightmsg",true);
        htmls += msg.replace("succs","");
        jQuery("#top_rightmsg").html(htmls);
        jQuery("#top_rightmsg").show();
        showposition("top_rightmsg");
        setTimeout(function() 
        {
            jQuery("#top_rightmsg").hide();
        }, 3000);
    }    
    catch(e){}
}

//显示错误信息
function showErrorResult(msg)
{
    var htmls = showOK("#top_errormsg",false);
    htmls+=msg;
    jQuery("#top_errormsg").html(htmls);
    jQuery("#top_errormsg").show();
    showposition("top_errormsg");
    setTimeout(function() 
    {
        jQuery("#top_errormsg").hide();
    }, 5000);
}

function bindmobiles(content,flag)
{
     var rstr="绑定手机";
     if(flag==0)
     {
         rstr="取消手机绑定";
     }
     var Ws = WinTip(rstr, content);
     setTimeout(function() 
     {
        Ws.Close();
      }, 60000);
}

function loading()
{
    try
    {
        jQuery("#loadingid").css("display", "");
        jQuery("#loadingid").html("<span><img src=\""+RootDir+"/template/images/loading.gif\" align=\"absmiddle\" /> 操作中，请等待...</span>");
        showposition("loadingid");
    }
    catch(e){}
}

function loadingclear()
{
    try
    {
        jQuery("#loadingid").css("display", "none");
        jQuery("#loadingid").empty();
    }
    catch(e){}
}
function getValue(Name)
{
    var rd= document.getElementsByName(Name);
    var len=rd.length
    for (var i=0; i<len; i++)
    {
        if (rd[i].checked)
        {
            return rd[i].value;
            break;
        }
    }
}

function showposition(div)
{
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
    var top = (ofHeight) / 2 - document.getElementById(div).offsetHeight / 2 + scrollTop - 100;
//    var left = document.documentElement.offsetWidth / 2 - document.getElementById(div).offsetWidth;
    var left = document.documentElement.offsetWidth / 2 - document.getElementById(div).offsetWidth / 2;
    document.getElementById(div).style.top = top + "px";
    document.getElementById(div).style.left = left + "px";
}

//提示可以输入多少字
//obj是 input,fontnum字数，showid显示提示的id 
function webdes_change(obj, fontnum, showid) 
{
    var l = obj.value.length;
    if (l > fontnum) {
        obj.value = obj.value.substr(0, fontnum);
    }
    var ll = fontnum - l;
    var tip = document.getElementById(showid);
    tip.innerHTML = "您还可以输入" + ll + "个字";
} 

//request pos
position = function(x,y)
{
    this.x = x;
    this.y = y;
}
getPosition = function(oElement)
{
    var objParent = oElement
    var oPosition = new position(0,0);
    try 
    {
        while (objParent.tagName != "BODY")
        {
            oPosition.x += objParent.offsetLeft;
            oPosition.y += objParent.offsetTop;
            objParent = objParent.offsetParent;
        }
    }
    catch(e){}
    return oPosition;
} 

function setCookie(name,value)
{
  var Days = 1; //此 cookie 将被保存 1 天
  var exp  = new Date();    //new Date("December 31, 9998");
  exp.setTime(exp.getTime() + Days*24*60*60*1000);
  document.cookie = name + "="+ escape(value) +";expires="+ exp.toGMTString();
}

function getCookie(name)
{
  var arr = document.cookie.match(new RegExp("(^| )"+name+"=([^;]*)(;|$)"));
  if(arr != null) return unescape(arr[2]); return null;
}

function delCookie(name)
{
  var exp = new Date();
  exp.setTime(exp.getTime() - 1);
  var cval=getCookie(name);
  if(cval!=null) document.cookie=name +"="+cval+";expires="+exp.toGMTString();
}


//取得对象obj的坐标，返回对象.X .Y
//arjun
function getXY(obj)
{
	var o=obj;
	var x=o.offsetLeft;
	var y=o.offsetTop;
	while(o=o.offsetParent)
	{
		x+=o.offsetLeft;
		y+=o.offsetTop;
	}
	return {X:x,Y:y}
}





getPosition = function(oElement)
{
    var objParent = oElement
    var oPosition = new position(0,0);
    while (objParent.tagName != "BODY")
    {
        oPosition.x += objParent.offsetLeft;
        oPosition.y += objParent.offsetTop;
        objParent = objParent.offsetParent;
    }
    return oPosition;
} 


function clearalloption(obj)
{
    var testnum=obj.length;
    for(var j=testnum-1;j>=0;j--)
    {
        obj.options[j]=null;
    }
}
function Addoption(obj,value)
{
    clearalloption(obj);
    if(value!="")
    {
        arr_value = value.split('||');
        s_name = arr_value[0];
        s_id = arr_value[1];
        arr_name = s_name.split(',');
        arr_id = s_id.split(',');
        for(var i=0;i<arr_name.length;i++)
        {
            var text = arr_name[i];
            var varItem = new Option(text,arr_id[i]);
            obj.options.add(varItem);
        }
    }
    else
    {
        obj.options[0] = new Option("","0");
        obj.options[0].select=true;
    }
}


function getEvent(){     //同时兼容ie和ff的写法
         if(document.all)    return window.event;        
         func=getEvent.caller;            
         while(func!=null){    
             var arg0=func.arguments[0];
             if(arg0){
                 if((arg0.constructor==Event || arg0.constructor ==MouseEvent)
                     || (typeof(arg0)=="object" && arg0.preventDefault && arg0.stopPropagation)){    
                     return arg0;
                 }
             }
             func=func.caller;
         }
         return null;
 }



function showSetMenu(mid)
{
    var m =document.getElementById(mid);
   if(m!=null)
   { 
        m.style.display="";
   } 
}

var ie4=document.all&&navigator.userAgent.indexOf("Opera")==-1
var ns6=document.getElementById&&!document.all
var ns4=document.layers
function delayhidemenu(mid)
{
		delayhide=setTimeout("hidemenu('"+mid+"')",100)
}

function hidemenu(mid)
{
	if (document.getElementById(mid))
	{
		document.getElementById(mid).style.display="none";
    }
}
function clearhidemenu()
{
    if (window.delayhide)
    {
          clearTimeout(delayhide)
    }
}


function setCopy(_sTxt)
{
	if(is_ie) 
	{
		clipboardData.setData('Text',_sTxt);
		alert ("网址“"+_sTxt+"”\n已经复制到您的剪贴板中\n您可以使用Ctrl+V快捷键粘贴到需要的地方");
	} 
	else
	{
		prompt("请复制网站地址:",_sTxt); 
	}
}


function Interface(divid,skinstyle)
{
    var formcontent = "<div style=\"line-height:25px;\">";
    for(var j=1;j<90;j++)
    {
        formcontent+="<a href=\"javascript:;\" onclick=\"intos('"+divid+"',"+j+");\"><img src=\""+RootDir+"/template/"+skinstyle+"/images/face/"+j+".gif\" /></a> ";
    }
    formcontent+="</div>";
    var W = WinTip("<div>选择表情</div>", formcontent);
    W.HasBottom=false;
    W.Create();        
}

 function checkFocus(target)
 {
	var obj = document.getElementById(target);
	if(!obj.hasfocus) {
		obj.focus();
	}
}

function isUndefined(variable) {
	return typeof variable == 'undefined' ? true : false;
}

function intos(divid,imgid)
{
    var text = '[em:'+imgid+':]';
    var obj = document.getElementById(divid);
    selection = document.selection;
    checkFocus(divid);
    if(!isUndefined(obj.selectionStart)) 
    {
        var opn = obj.selectionStart + 0;
        obj.value = obj.value.substr(0, obj.selectionStart) + text + obj.value.substr(obj.selectionEnd);
     } 
    else if(selection && selection.createRange)
    {
        var sel = selection.createRange();
        sel.text = text;
        sel.moveStart('character', -text.length);		      
    }
    else
    {
        obj.value += text;
    }	
    var W = WinTip("<div>选择表情</div>", "ok");
    W.HasBottom=false;
    W.Create();        
    W.Close();
}

function lineheight()
{
    if(document.getElementById("snsmenu")!=null&&document.getElementById("snsmenu")!=null)
    {
        var bodyheight=document.getElementById('snsbody').clientHeight;
        if(bodyheight>800)
        {
            document.getElementById("snsmenu").style.height=(bodyheight-16)+"px";
        }
    } 
}

function getEvent(){     //同时兼容ie和ff的写法
         if(document.all)    return window.event;        
         func=getEvent.caller;            
         while(func!=null){    
             var arg0=func.arguments[0];
             if(arg0){
                 if((arg0.constructor==Event || arg0.constructor ==MouseEvent)
                     || (typeof(arg0)=="object" && arg0.preventDefault && arg0.stopPropagation)){    
                     return arg0;
                 }
             }
             func=func.caller;
         }
         return null;
 }
function tab(Xname,Cname,Lenght,j){for(i=1;i<Lenght;i++){eval("ji('"+Xname+i+"').className=''");}eval("ji('"+Xname+j+"').className='current'");for(i=1;i<Lenght;i++){eval("ji('"+Cname+i+"').style.display='none'");eval("ji('"+Cname+j+"').style.display='block'");}}
function setCopy(_sTxt){
	if(navigator.userAgent.toLowerCase().indexOf('ie') > -1) {
		clipboardData.setData('Text',_sTxt);
		alert ("“"+_sTxt+"”\n已经复制到您的剪贴板中\n您可以使用Ctrl+V快捷键粘贴到需要的地方");
	} else {
		prompt("请复制网站地址:",_sTxt); 
	}
}

//加入收藏
function addBookmark(site, url){
	if(navigator.userAgent.toLowerCase().indexOf('ie') > -1) {
		window.external.addFavorite(url,site)
	} else if (navigator.userAgent.toLowerCase().indexOf('opera') > -1) {
		return;
	} else {
		return;
	}
}
