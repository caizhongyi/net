/**************************************************************
           ALIMAMA.COM! JavaScriptComponent Base
Written by:limu(lenel@yahoo.cn)
Important:to use this script don't remove these comments
Version 1.0
Created Date: 2007-5-1
Copyright:1997-2007 Alimama.Com All rights reserved.
**************************************************************/

var $w = window;
var $d = document;
var $l = location;
function $i(s){return $d.getElementById(s);}
function $t(s){return $d.getElementsByTagName(s);}
function $n(s){return $d.getElementsByName(s);}

var _jsc = {};
if(!_jschome)var _jschome='http://www.alimama.com/jsc/';

_jsc.loaded = false;

_jsc.client = (function(){	
	var t = {};
	var b = navigator.userAgent.toLowerCase();
	t.isOpera = (b.indexOf('opera') > -1);
	t.isIE = (!t.isOpera && b.indexOf('msie') > -1);
	t.isFF = (!t.isOpera &&!t.isIE&&b.indexOf('firefox') > -1);
	return t;
})();

_jsc.util = (function(){
	var t = {};
	t.addEvent = function(o,c,h){
		if(_jsc.client.isIE){
			o.attachEvent('on'+c,h);
		}else{
			o.addEventListener(c,h,false);
		}
		return true;
	};
	t.delEvent = function(o,c,h){
		if(_jsc.client.isIE){
			o.detachEvent('on'+c,h);
		}else{
			o.removeEventListener(c,h,false);
		}
		return true;
	};
	t.ga = function(o,s){
		return o.getAttribute(s);
	};
	t.sa = function(o,k,v){
		return o.setAttribute(k,v);
	};
	t.s2d = function(s){
		var sa = s.split('-');
		var d =  new Date(sa[0],(sa[1]-1),sa[2]);
		return d;
	};
	t.d2s = function(d,b){
		var ye = d.getFullYear();
		var me = (parseInt(d.getMonth())+1).toString()
		var de = d.getDate();
		if(me.length==1&&b)me='0'+me;
		if(de.length==1&&b)de='0'+de;
		return ye+me+de;
	};
	return t;
})();

_jsc.mgr = (function(){
	var t = {};
	t.s = [];
	t.addC = function(o){
		this.s.push(o);
	};
	t.getC = function(bid){
		for(var i=0;i<this.s.length;i++){
			if(this.s[i].bid==bid){
				return this.s[i];
			}
		}
		return null;
	};
	return t;
})();

_jsc.ajax = (function(){
	t={};
	t.getAjax = function(){
		try{
			return new XMLHttpRequest();
		}catch(e){
			try{
				return new ActiveXObject('Msxml2.XMLHTTP');
			}catch(e){
				return new ActiveXObject('Microsoft.XMLHTTP')
			}
		}
		return null;
	};
	return t;
})();

_jsc.dom = (function(){
	var t = {};
	t.gNxtSib = function(o){
		var co = o;
		do{
			if(co.nextSibling==null || co.nextSibling.nodeType==1){
				return co.nextSibling;
			}else{
				co = co.nextSibling;
			}			
		}while(true)
	};
	
	return t;
})();

_jsc.evt = (function(){
	var t = {};
	t.gTar = function(oe){
		if(_jsc.client.isIE){
			return oe.srcElement;
		}else{
			return oe.target;
		}
	};
	t.gJsc = function(o){
		var ot = o
		do{
			if(ot.getAttribute("c_type")){
				return ot;
			}
			if(ot.parentNode){
				ot = ot.parentNode;
			}else{
				return null;
			}
		}while(true);
	};
	t.evtHandler = function(){
		var eo = window.event?window.event:arguments[0];
		var tar = _jsc.evt.gTar(eo);
		var jsc = _jsc.evt.gJsc(tar).jsc;
		et = eo.type;
		eval("var h = jsc.jsc"+et);
		h(tar,jsc);
	};
	t.fire = function(jsc,etype,evt){
		eval("var h = jsc.c_on"+etype);
		eval("var t = typeof "+h);
		if(t == "function"){
			eval(h+"(evt)");
		}
	};
	return t;
})();

_jsc.his = (function(){
	var t={};
	t.srvPath = _jschome+'htm/hisSrv.html';
	t.srvFrame = $i("hisSrvFrame");
	t.ahMap = {};
	t.setAH = function(act,handler){
		var m = this.ahMap;
		eval('m.'+act+'="'+handler+'";');
	};
	t.doGet = function(act,para){
	    var appSch = $n("jscAppendSearch");
	    var as = "";
	    for(var i=0;i<appSch.length;i++){
	        var appAct = appSch[i].getAttribute("act");
	        if(appAct == act){
	            as += appSch[i].value;
	        }
	    }
		if(!this.srvFrame)this.srvFrame = $i("hisSrvFrame");
		this.srvFrame.src = this.srvPath+'?act='+act+as+para;
	};
	t.actionSwitch = function(act,para){
		var m = this.ahMap;
		eval('var h = this.ahMap.'+act+';');
		eval(h+'(para);');
	};
	t.setRefCookie = function(pagename){
	    try{
	        var refCookie = []; 
			refCookie.push(pagename);
            refCookie.push($i("hisSrvFrame").contentWindow.location.search);
            if(typeof addRefCookie == "function"){
                addRefCookie(refCookie);
            }
            var refreshCookie = refCookie.join("@jsc@");
            _jsc.cookies.setCookie("jscref",refreshCookie,6/60/60/24);
        }catch(e){}
	};
	t.getRefCookie = function(){
	    var refCookie = []; 
	    try{
	       var refreshCookie = _jsc.cookies.getCookie("jscref");
	       refCookie = refreshCookie.split("@jsc@");
	    }catch(e){}
	    return refCookie;
	};
	return t;
})();

_jsc.state = (function(){
	var t = {};
	t.eleList = [];
	t.chkList = [];
	t.loaded = false;
	t.addEle = function(o){
		if(o instanceof JscStateElement){
			this.eleList.push(o);
			if(o.check==true){
				this.chkList.push(o);
			}
		}
	};	
	t.setState = function(para){
		var u = para;
		for(var i = 0;i < this.eleList.length;i++){
			var ele = this.eleList[i];
			var ukey = ele.urlKey;
			var uvalue = this.getUrlValue(para,ukey);
			ele.setState(uvalue);
		}
	};
	t.getUrlValue = function(para,ukey){
		var andpara = '&'+para;
		var lowerpara = andpara.toLowerCase();
		if(lowerpara.indexOf('&'+ukey+'=')!=-1){
			var s = andpara.substr(lowerpara.indexOf('&'+ukey+'=')+1);
			var sa = s.split('&');
			var sav = sa[0].split('=');
			if(sav.length==2){
				return sav[1];
			}
		}
		return '';
	};
	t.getState = function(ukey){
		var u='';
		for(var i = 0;i < this.eleList.length;i++){
			var ele = this.eleList[i];
			if(ele.urlKey==ukey||ele.check==true){
				ele.getState();
			}
		}
		for(var i = 0;i < this.eleList.length;i++){
			var ele = this.eleList[i];
			u+='&'+ele.urlKey+'='+ele.currentState;
		}
		_jsc.his.doGet('sch',u);
	};
	t.chgPage = function(pgno){
		var u = '&p='+pgno;
		for(var i = 0;i < this.eleList.length;i++){
			var ele = this.eleList[i];
			ele.getState();
			u+='&'+ele.urlKey+'='+ele.currentState;
		}
		_jsc.his.doGet('sch',u);
		return false;
	};
	t.toDefaultState = function(){
		
	};
	return t;
})();

_jsc.pos = (function(){
	var t = {};
	t.getX = function (obj){
        var curleft = 0;
        if (obj.offsetParent)
        {
          while (obj.offsetParent)
        {
             curleft += obj.offsetLeft;
             obj = obj.offsetParent;
          }
        }
        else if (obj.x) {
          curleft += obj.x;
        }
        return curleft;
    };
    t.getY = function (obj){
    var curtop = 0;
        if (obj.offsetParent)
        {
          while (obj.offsetParent)
          {
             curtop += obj.offsetTop;
             obj = obj.offsetParent;
          }
        }
        else if (obj.y)
          curtop += obj.y;
        return curtop;
    };
	return t;
})();

_jsc.cookies = (function(){
	var t = {};
	t.setCookie = function(name,value,days)
	{
		if(days){
	  	var exp  = new Date(); 
	  	exp.setTime(exp.getTime() + days*24*60*60*1000);
	  	document.cookie = name + "="+ escape(value) +";expires="+ exp.toGMTString()+";path=/;";
		}else{
			document.cookie = name + "="+ escape(value)+";path=/;";
		}
	};
	
	t.getCookie = function(name)
	{
	  var arr = document.cookie.match(new RegExp("(^| )"+name+"=([^;]*)(;|$)"));
	  if(arr != null) return unescape(arr[2]); return null;
	};
	
	t.setCookie2 = function(name,value,days)
	{
		if(days){
	  	var exp  = new Date(); 
	  	exp.setTime(exp.getTime() + days*24*60*60*1000);
	  	document.cookie = name + "="+ encodeURIComponent(value) +";expires="+ exp.toGMTString()+";path=/;";
		}else{
			document.cookie = name + "="+ encodeURIComponent(value)+";path=/;";
		}
	};
	
	t.getCookie2 = function(name)
	{
	  var arr = document.cookie.match(new RegExp("(^| )"+name+"=([^;]*)(;|$)"));
	  if(arr != null) return decodeURIComponent(arr[2]); return null;
	};
	
	t.delCookie = function(name)
	{
	  var exp = new Date();
	  exp.setTime(exp.getTime() - 1);
	  var cval=getCookie(name);
	  if(cval!=null) document.cookie=name +"="+cval+";expires="+exp.toGMTString();
	};
	
	t.delCookie2 = function(name)
	{
	  var exp = new Date();
	  exp.setTime(exp.getTime() - 1);
	  var cval=getCookie2(name);
	  if(cval!=null) document.cookie=name +"="+cval+";expires="+exp.toGMTString();
	};
	
	return t;
})();

_jsc.jscload = (function(){
	var t = function(){
	    if(!_jsc.quickload){
    		var cs = $t('label');
    		for(var i=0;i<cs.length;i++){		
    			var ct = _jsc.util.ga(cs[i],'c_type');
    			if(ct){
    				var pe = _jsc.dom.gNxtSib(cs[i]);
    				if(pe){
    					eval("new "+ct+"(pe)");
    				}
    			}
    		}
		}
		if(typeof jsconload == 'function'){
			jsconload();
		}
		_jsc.loaded = true;
	};
	return t;
})();

_jsc.jscunload = (function(){
	var t = function(){
		if(true){
		    try{
				if(typeof jsconload == 'function'){
					jsconunload();
				}		                       		
            }catch(e){}
		}
	};
	return t;
})();

_jsc.init = (function(){
	_jsc.util.addEvent(window,'load',_jsc.jscload);
	_jsc.util.addEvent(window,'unload',_jsc.jscunload);
	return true;
})();

function Jsc(){
	this.chkPropName = function(s){
		if(s == 'c_type'){
			return false;
		}
		return true;
	};		
	
	this.getAttr = function(k){
		return eval("this."+k);
	};
	
	this.setAttr = function(k,v,n){
		if(!n){
			if(typeof v == "string"){
				eval("this."+k+" = \""+v+"\"");
			}else{
				eval("this."+k+" = "+v);
			}
		}
	};
	
	this.initBase = function(){
		_jsc.mgr.addC(this);
		for(var i = 0;i<this.doc.attributes.length;i++){
			var nn = this.doc.attributes[i].nodeName;
			if(nn.indexOf("c_") == 0){
				if(this.chkPropName(nn)){
					var nv = this.doc.attributes[i].nodeValue;
					eval("this."+nn+" = '"+nv+"'");
				}
			}
		}
	};
}

function JscStateElement(ele,dft,ukey){
	this.getState = null;//y
	this.setState = null;//y
	this.defaultState = dft;
	this.currentState = dft;
	this.ele = ele;
	this.eid = ele.id;
	this.must = true;	
	this.ischanged = false;
	this.check = false;//o
	this.urlKey = ukey;
}

function JscSet(){
	this.arr = [];
	this.idx = {};
	this.cp = -1;
	this.al = 0;
	this.add = function(oid,obj){
		this.arr.push([oid,obj]);
		this.cp++;
		this.idx[oid] = this.cp;
		this.al++;
	};
	this.del = function(oid){
		var add = this.idx[oid];
		delete this.arr[add];
		this.al--;
		delete this.idx[oid];
	};
		
	this.getFirst = function(){
		if(this.al<1)return null;
		for(var i=0;i<this.arr.length;i++){
			if(this.arr[i]){
				return this.arr[i][1];
			}
		}
		return null;
	};
}
