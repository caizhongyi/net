// JavaScript Document

// 随机数发生器
rnd.today=new Date();
rnd.seed=rnd.today.getTime();
function rnd() {
	rnd.seed = (rnd.seed*9301+49297) % 233280;
	return rnd.seed/(233280.0);
}
function rand(number) {
	return Math.ceil(rnd()*number);
}

// 滚动widget
function simplescroll(c, config) {
	this.config = config ? config : {start_delay:3000, speed: 23, delay:4000, scrollItemCount:1,movecount:1};
	this.container = $i(c);
	this.pause = false;
	var _this = this;
	
	this.init = function() {
		_this.scrollTimeId = null;
		setTimeout(_this.start,_this.config.start_delay);
	}
	
	this.start = function() {
		var d = _this.container;
		var line_height = d.getElementsByTagName('li')[0].offsetHeight;
		if(d.scrollHeight-d.offsetHeight>=line_height) _this.scrollTimeId = setInterval(_this.scroll,_this.config.speed)
	};
	
	this.scroll = function() {
		if(_this.pause)return;
		var d = _this.container;d.scrollTop+=2;
		var line_height = d.getElementsByTagName('li')[0].offsetHeight;
		//alert(d.scrollTop + "%" + line_height + " : " + d.scrollTop%line_height);
		if(d.scrollTop%(line_height*_this.config.scrollItemCount)<=1){
			if(_this.config.movecount != undefined)
				for(var i=0;i<_this.config.movecount;i++){d.appendChild(d.getElementsByTagName('li')[0]);}
			else for(var i=0;i<_this.config.scrollItemCount;i++){d.appendChild(d.getElementsByTagName('li')[0]);}
			d.scrollTop=0;
			clearInterval(_this.scrollTimeId);
			setTimeout(_this.start,_this.config.delay);
		}
	}
	
	this.container.onmouseover=function(){_this.pause = true;}
	this.container.onmouseout=function(){_this.pause = false;}
}

// 横着滚动
function simpleSideScroll(c,ul,config,direction){
    this.config = config ? config : {start_delay:3000, speed: 23, delay:4000, scrollItemCount:1};
	this.c = $i(c);
	this.ul = $i(ul);
	this.direction = direction ? direction : "left";
	this.pause = false;
	this.buttonlist= new Object();
	this.delayTimeId=null;
	
	var _this = this;



	this.c.onmouseover=function(){_this.pause = true;}
	this.c.onmouseout=function(){_this.pause = false;}
	
	this.init = function() {
		_this.scrollTimeId = null;
		setTimeout(_this.start,_this.config.start_delay);
	}
	
	this.start = function() {
		var d = _this.c;
		var width = d.getElementsByTagName('li')[0].offsetWidth;
		if(d.scrollWidth-d.offsetLeft>=width) _this.scrollTimeId = setInterval(_this.scroll,_this.config.speed)
	};
	
	this.scroll = function() {
		if(_this.pause)return;
		var ul= _this.ul;
		var d = _this.c;
		var width = d.getElementsByTagName('li')[0].offsetWidth;
		if(_this.direction == 'left'){
	        d.scrollLeft += 2;
	        if(d.scrollLeft%(width*_this.config.scrollItemCount)<=1){
		        if(_this.config.movecount != undefined)
			        for(var i=0;i<_this.config.movecount;i++){ul.appendChild(ul.getElementsByTagName('li')[0]);}
		        else for(var i=0;i<_this.config.scrollItemCount;i++){ul.appendChild(ul.getElementsByTagName('li')[0]);}
		        d.scrollLeft=0;
		        clearInterval(_this.scrollTimeId);
		        
		        _this.delayTimeId=setTimeout(_this.start,_this.config.delay);
	        }
		}
		else {
		    if(d.scrollLeft==0)
		    {
		        var lis=ul.getElementsByTagName('li');
		        for(var i=0;i<_this.config.scrollItemCount;i++){
		            ul.insertBefore(lis[lis.length-1],lis[0]);
		        }
		        d.scrollLeft = width;
		    }
		    d.scrollLeft -= 2;
		    if(d.scrollLeft%(width*_this.config.scrollItemCount)<=1){
		        d.scrollLeft=0;
		        clearInterval(_this.scrollTimeId);
		        _this.delayTimeId=setTimeout(_this.start,_this.config.delay);
		    }
		}
	}
	
	this.setButton=function(id,direction){
	    if($i(id)){
	        var c=$i(id);
	        var buttonlist =_this.buttonlist;
	        if(buttonlist[id] == undefined){
	            buttonlist[id] =new Object();
	            _this.buttonlist[id][0]=c;
	            _this.buttonlist[id][1]=direction;
	            
	            c.onclick=function(){
	                 clearInterval(_this.scrollTimeId);
	                 
	                var dir=_this.buttonlist[this.id][1];
	                var d = _this.c;
	                var ul= _this.ul;
	                d.scrollLeft=0;
	                if(dir =="left")
	                {
	                    for(var i=0;i<_this.config.scrollItemCount;i++){ul.appendChild(ul.getElementsByTagName('li')[0]);}
	                }
	                else{
	                    var lis=ul.getElementsByTagName('li');
		                for(var i=0;i<_this.config.scrollItemCount;i++){
		                    ul.insertBefore(lis[lis.length-1],lis[0]);
		                }
	                }
	                    
	                _this.direction= dir;
	                clearTimeout(_this.delayTimeId);
	                _this.delayTimeId=setTimeout(_this.start,_this.config.delay);
	                return false;
	            }
	        }
	    }
	}
}
// tab切换
function tabswitch(c, config){
	this.config = config ? config : {start_delay:3000, delay:1500};
	this.container = $i(c);
	this.pause = false;
	this.nexttb = 1;
	this.tabs = this.container.getElementsByTagName('dt');
	var _this = this;
	if(this.tabs.length<1)this.tabs = this.container.getElementsByTagName('li');
	for(var i = 0; i < this.tabs.length; i++){
		var _ec = this.tabs[i].getElementsByTagName('span');
		if(_ec.length<1)_ec = this.tabs[i].getElementsByTagName('a');
		if(_ec.length<1){
			_ec = this.tabs[i]
		}else{
			_ec = _ec[0];
		}
		_ec.onmouseover = function(e) {
			_this.pause = true;
			var ev = !e ? window.event : e;
			_this.start(ev, false, null);
		};
		
		_ec.onmouseout = function() {
			_this.pause = false;
		};
		
		try{
			$i(this.tabs[i].id + '_body_1').onmouseover = function(){
				_this.pause = true;
			};
			
			$i(this.tabs[i].id + '_body_1').onmouseout = function(){
				_this.pause = false;
			};
		}catch(e){}
	}

	if ($i(c + '_sts')) {
		var _sts = $i(c + '_sts');
		var _step = _sts.getElementsByTagName('li');
		if(_step.length<1)_step = _sts.getElementsByTagName('div');
		_step[0].onclick = function() {
			if (_this.tabs[_this.tabs.length-1].className.indexOf('current') > -1) {
				_this.nexttb = _this.tabs.length + 1;
			};
			_this.nexttb = _this.nexttb - 2 < 1 ? _this.tabs.length : _this.nexttb - 2;
			//alert(_this.nexttb);
			_this.start(null, null, _this.nexttb);
		};
		
		_step[1].onclick = function() {
			_this.nexttb = _this.nexttb < 1 ? 1 : _this.nexttb;
			_this.start(null, null, _this.nexttb);
		};
	};
	
	this.start = function(e, r, n){
		if(_this.pause && !e)return;
		if(r){
			curr_tab = $i(_this.container.id + '_' + rand(4));
		}else{
			if(n){
				//alert(_this.container.id + '_' + _this.nexttb);
				curr_tab = $i(_this.container.id + '_' + _this.nexttb);
			}else{
				curr_tab = _jsc.evt.gTar(e);
				if(curr_tab.id=="")curr_tab = curr_tab.parentNode;
			}
		}
		
		var tb = curr_tab.id.split("_");
		for(var i = 0; i < _this.tabs.length; i++){
			if(_this.tabs[i]==curr_tab){
				_this.tabs[i].className="hot Selected current";
				try{
					//alert(_this.tabs[i].id);
					$i(_this.tabs[i].id + '_body_1').style.display = "block";
				}catch(e){}
			}else{
				_this.tabs[i].className="";
				try{
					$i(_this.tabs[i].id + '_body_1').style.display = "none";
				}catch(e){}
			}
		}
		_this.nexttb = parseInt(tb[tb.length-1]) >= _this.tabs.length ? 1 : parseInt(tb[tb.length-1]) + 1;
	};
}

function multibanners(){
	this.i = 0;
	this.ul = $i('multi_banners');
	this.nav = $i('mb-nav');
	this.pause = false;
	this.lis = this.ul.getElementsByTagName('li');
	this.navs = this.nav.getElementsByTagName('li');
	var _this = this;
	
	this.sw = function(o){
		for(var i = 0; i < _this.lis.length; i++){
			_this.lis[i].className = '';
			_this.navs[i].className = '';
		}
		o.className = 'current';
		var _cp = parseInt(o.innerHTML);
		_this.lis[_cp-1].className='current';
		//_this.i = ++_cp % 2;
		//var navs = _this.navs;
		//setTimeout(function() {
		//	if(_this.pause)return;
		//	_this.sw(navs[++_this.i % 2]);
		//}, 7800);
		//if(_this.i>=8)_this.i=0;
	};
	
	this.init = function(instance_name){
		var lis = _this.lis;
		var navs = _this.navs;
		for(var i = 0; i < lis.length; i++){
			var _img;
			_img = lis[i].getElementsByTagName('img')[0];
			if(!_img)_img = lis[i].getElementsByTagName('object')[0];
			_img.onmouseover = function(){_this.pause=true};
			_img.onmouseout = function(){_this.pause=false};
			navs[i].onmouseover = function(e){
				_this.pause=true;
				var ev = !e ? window.event : e;
				_this.sw(_jsc.evt.gTar(ev));
			};
			navs[i].onmouseout = function(){
				_this.pause=false;
			};
		}
		_this.sw(navs[0]);
		setInterval(function() {
			if(_this.pause)return;
			_this.sw(navs[++_this.i % 3]);
		}, 4800);
	};
}

// accordion by nowa
function hslide(c){
	this.c = $i(c);
	this.b = $i('slide_body');
	this.h3s = this.c.getElementsByTagName('ul')[0].getElementsByTagName('h3');
	this.sliding = false;
	var _this = this;
	
	for(var i=0;i<this.h3s.length;i++){
		this.h3s[i].onclick = function(e){
			var ev = !e ? window.event : e;
			_this.slide(_jsc.evt.gTar(ev));
		};
	}
	
	this.slide = function(o){
		if(_this.sliding)return;
	
		var bleft = 0;
		var tleft = 0;
		var passed = false;
		var _tab;
		
		for(var i=0;i<_this.h3s.length;i++){
			if(_this.h3s[i]!=o && _this.h3s[i].parentNode.className=='current'){
				_tab = _this.h3s[i];
			}
		}
		
		try{
			var _tabb = $i(_tab.id + '_body');
			var anim = function(){
				_this.sliding = true;
            	n += 69;
            	if(n >= 367){
					_tabb.style.width = "0px";
            		_tabb.style.display = 'none';
            		_tab.parentNode.className = "";
            		window.clearInterval(tt);
					
					o.parentNode.className = 'current';
					var _tabb2 = $i(o.id + '_body');
					_tabb2.style.width = "0px";
					_tabb2.style.display = 'block';
					var anim2 = function(){
						z += 69;
						if(z >= 367){
							_tabb2.style.width = "376px";
							_this.sliding = false;
							window.clearInterval(tt2);
						}else{
							_tabb2.style.width = z + "px";
						}
					},z=0;
					var tt2 = setInterval(anim2, 30);
            	}else{
            		_tabb.style.width = (377 - n) + "px";
            	}
            },n=0;
            var tt = setInterval(anim, 30);
		}catch(e){}
	}
}

function init_imh () {
	var _ul = document.getElementsByTagName('ul');
	for (var i=0;i<_ul.length;i++) {
		if (_ul[i].getAttribute('jpe') == 'imh:hover') {
			var _lis = _ul[i].getElementsByTagName('li');
			for (var j=0;j<_lis.length;j++) {
				_lis[j].onmouseover = function(e) {
					var ev = !e ? window.event : e;
					var _target = _jsc.evt.gTar(ev);
					while (_target.tagName.toLowerCase() != 'li') {
						_target = _target.parentNode;
					}
					_target.className = 'hover';
				};
				
				_lis[j].onmouseout = function(e) {
					var ev = !e ? window.event : e;
					var _target = _jsc.evt.gTar(ev);
					while (_target.tagName.toLowerCase() != 'li') {
						_target = _target.parentNode;
					}
					_target.className = '';
				};
			}
		}
	}
	
	var _lis = document.getElementsByTagName('li');
	for (var i=0;i<_lis.length;i++) {
		if (_lis[i].getAttribute('jpe') == 'linkto:a') {
			_lis[i].onclick = function(e) {
				var ev = !e ? window.event : e;
				var _target = _jsc.evt.gTar(ev);
				while (_target.tagName.toLowerCase() != 'li') {
					_target = _target.parentNode;
				}
				location.href = _target.getElementsByTagName('a')[0].getAttribute('href');
				return false;
			};
		}
	}
}
var mm_select=function(c){
	this.c=document.getElementById(c); //整体下拉框
	this.selecttext={};  //选中后显示的内容
	this.select={};  //下拉框内容
	this.hidetimer=null;  //移出时的隐藏
	var _this=this;
	this.addonclick=null;
	this.iframe=null;

	this.returnvalue=function(){
        return this.c.getAttribute("val");
    }

	this.c.onmouseover=function(){
        var th=_this;
        if(th.hidetimer)
            window.clearTimeout(th.hidetimer);
        th.c.className="mm_select1";
    }

	this.c.onmouseout=function(){
        _this.select_none();
    }


	this.c.onmousedown=function(){
	    _this.c.className="mm_select2";
	}

	this.c.onmouseup=function(){
       var th=_this;
        if(th.hidetimer)
            window.clearTimeout(th.hidetimer);
        th.c.className="mm_select1";    
        
        th.iframe.style.top=_jsc.pos.getY(th.c)+24+"px";
        th.iframe.style.left=_jsc.pos.getX(th.c)+"px"; 
        th.iframe.style.display="block";
        
        th.select.style.top=th.iframe.style.top;
        th.select.style.left=th.iframe.style.left;
        th.select.style.display="block";
        
        if(th.iframe.style.height=="0px")
        {
            th.iframe.style.height=th.select.offsetHeight+"px";
            th.iframe.style.width=th.select.offsetWidth+"px";
        }
    }
 
	this.select_none=function(){
        _this.hidetimer = window.setTimeout(function(){
            _this.c.className="mm_select";
            _this.select.style.display="none";
             _this.iframe.style.display="none";
         },300);
    }

	this.init=function(){
        var c=this.c;var divs=new Array();var childs=c.childNodes;
        for(var i=0;i<childs.length;i++)
        { 
            if(childs[i].tagName && childs[i].tagName.toLowerCase() =="div") divs[divs.length]=childs[i];
        }
        this.selecttext=divs[0];
        this.select=divs[1];
        this.iframe=document.createElement("iframe");
        this.iframe.style.cssText="position:absolute;display:none;filter:alpha(opacity=0);opacity:0;border-width:0;height:0px;";
        this.iframe.src="#";
        c.insertBefore(this.iframe,this.select);
        
        this.select.onclick=function(event){
            var tar =event?event.target:window.event.srcElement;
            if(tar.tagName.toLowerCase() =="a")
            {_this.c.setAttribute("val",tar.getAttribute("val"));
            _this.selecttext.innerHTML=tar.innerHTML; }
            this.style.display="none";
            _this.iframe.style.display="none";
            return false;
        }
        if(this.addonclick)
        {
            _jsc.util.addEvent(this.select,"click",this.addonclick);
        }
        this.select.onmouseover=function(){
            var th=_this;
            if(th.hidetimer)
                window.clearTimeout(th.hidetimer);
        }
        this.select.onmousedown=function(event){
            if(_jsc.client.isIE)
            {
                event=window.event;
                event.cancelBubble=true;
            }
            else
                event.stopPropagation();
        }
    }
}

var banners,  super_rec,  star,bargain_scroll,buy_ok_div,another_div;
var select_pr,select_pc,select_cpk;
_jsc.util.addEvent(window, 'load', function() {
		if ($i('banners')) {	
			banners = new tabswitch('banners', {});
			setInterval("banners.start(null, null, 1);", 6000);
		}
		
		if ($i('super_rec')) {	
			super_rec = new tabswitch('super_rec', {});
			setInterval("super_rec.start(null, null, 1);", 5000);
		}
		
		if ($i('star')) {	
			star = new tabswitch('star', {});
			setInterval("star.start(null, null, 1);", 6000);
		}
		
		
		if($i('bargain_scroll')){
		    bargain_scroll=new simpleSideScroll('bargain_scroll','bargain_scroll_ul',{start_delay:3000, speed: 23, delay:3000, scrollItemCount:1},'left')
		    bargain_scroll.setButton('bargain_scroll_left','left');
		    bargain_scroll.setButton('bargain_scroll_right','right');
		    bargain_scroll.init();
		}
		
		if($i('buy_ok_div')){
		    buy_ok_div=new simpleSideScroll('buy_ok_div','buy_ok_ul',{start_delay:3000, speed: 23, delay:3000, scrollItemCount:1},'left')
		    buy_ok_div.setButton('buy_ok_scroll_left','left');
		    buy_ok_div.setButton('buy_ok_scroll_right','right');
		    buy_ok_div.init();
		}
		
		if($i('another_div')){
		    another_div=new simpleSideScroll('another_div','another_ul',{start_delay:3000, speed: 30, delay:3000, scrollItemCount:1},'left')
		    another_div.setButton('another_scroll_left','left');
		    another_div.setButton('another_scroll_right','right');
		    another_div.init();
		}
		
		
		if($i('category')){
		    var category_lis = $i('category').getElementsByTagName("li");
		    for(var i=0;i<category_lis.length;i++){
		        category_lis[i].onmouseover=function(){
		            this.className +=" onmouse";
		        }
		        category_lis[i].onmouseout=function(){
		            this.className = this.className.replace(" onmouse","").replace("onmouse","")
		        }
		    }
		}
		
		init_imh();
		
		if($i("select_arp")){
		    init_arp();
            select_pr=new mm_select("select_arp");select_pr.addonclick=change_city; select_pr.init();
        
            select_pc=new mm_select("select_arc"); select_pc.init();
            select_cpk=new mm_select("batch_schCPK");select_cpk.init();
        }
});