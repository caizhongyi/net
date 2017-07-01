// JScript 文件

function _10powertalk_Float()
{
	var self = this;
	this.obj = null;
	this.currentX = 0;
	this.currentY = 0;                              
	this.whichIt = null;                                         
	this.lastScrollX = 0; 
	this.lastScrollY = 0;   
	this.tempx = 0;
	this.tempY = 0;
	this.offsetx = 0;
	this.offsety = 0;
	this.IE = (document.getElementById && document.all);
	this.NS = (document.getElementById && !document.all);  
	this.layer_id = null;
	this.layer_div = '';
	this.dragapproved = false;
	this.mousetrail = false;

	this._oBody = '';
	this._oHelperIframe = '';
	this._iframe = true;
	
	this.floatHtml = '';
	this.container_div = '';
	this.container_id = null;

	this.name = '';
	this.frame_style = true;
	this.padding = 2 ;
	this.top = 120;
	this.width = 0;
	this.align = '';
	this.valign = 'middle';
	this.display = false;
	this.drag_left =0;
	this.drag_top =0;
	this.drag_status = true;
	this.move_status  = true;

	this.fly_timer = null;
	this.fly_x = 0;
	this.fly_y = 0;
	this.evt_id = '';

	this.init = function (name){
	    if(this.name.length == 0 && typeof(name) != 'undefined')
		    this.name = name;
		if(!this.container_id)
		    this.container_id = $10powertalk_get(this.container_div);

		this.container_id.innerHTML = this.floatHtml;

		this.layer_id = $10powertalk_get(this.layer_div);
		
		this.layer_id.style.zIndex = 99999;

		this.change_align( "left");
		var tmp = function (){
				eval(name + ".add_frame()")
			}
	
//		this.attachToEvent (window, 'onload', tmp);		
		
		if(this.drag_status)this.attachToEvent(this.layer_id, 'onmousedown', this.DragStart );
		
   		if( typeof(name) != 'undefined' ){
		    if(this.move_status && (this.NS || this.IE)) action = window.setInterval(this.name + ".heartBeat()",200);
		
		    var tmp = function (){
				eval(name + ".change_align()")
			}
		    this.attachToEvent(window, 'onresize', tmp);
		}

		this.layer_id.onShow = new Function();
		this.layer_id.onHide = new Function();
		this.layer_id.onDragStart	= new Function();
		this.layer_id.onDragEnd	= new Function();
		this.layer_id.onDrag	= new Function();	


		try{
			this._move_frame = $10powertalk_get('float_move_frame');
		}catch(e){

		}
	}
	this.iframe_status = function(s){
		this._iframe = s;
	}
	this.add_frame = function(){
		//if(document.readyState!="complete") this.frame_style=false;
		if(this._iframe && this.frame_style && this.IE && !this._oHelperIframe){ 
			var _layer_id_display = this.layer_id.style.display;
			this._oBody = document.getElementsByTagName("BODY").item(0);

			this._oHelperIframe = document.createElement("IFRAME");
			//this._oHelperIframe.src='about:blank';
			this._oHelperIframe.frameborder = 0;
			this._oHelperIframe.noresize = 0;
			this._oHelperIframe.border = 0;
			this._oHelperIframe.marginwidth = 0;
			this._oHelperIframe.marginheight = 0;
			//this._oHelperIframe.allowTransparency = "true";

			this._oHelperIframe.scrolling="no";
			this._oHelperIframe.style.filter='Alpha(opacity=0)';
			//this._oHelperIframe.style.backgroundColor = 'transparent';
			this._oHelperIframe.style.border = 0;
			this._oHelperIframe.width = 0;
			this._oHelperIframe.height = 0;
			this._oHelperIframe.style.position = "absolute";
			this._oBody.appendChild(this._oHelperIframe);
			this.layer_id.style.display = '';
			
			this._oBody.appendChild(this.layer_id);
			this._oHelperIframe.style.top = this.layer_id.style.top;
			this._oHelperIframe.style.left = this.layer_id.style.left;
			this._oHelperIframe.style.width = parseInt(this.layer_id.offsetWidth) > 0 ? this.layer_id.offsetWidth : this.layer_id.style.width;//.offsetWidth
			this._oHelperIframe.style.height = this.layer_id.offsetHeight;// 

			this._oHelperIframe.style.zIndex = this.layer_id.style.zIndex-1;
			this._oHelperIframe.style.display = _layer_id_display;

			this.layer_id.style.display = _layer_id_display;
		}

	}
	this.close = function () {
		this.hide();

	}
	this.hide = function () {
		//if(this.layer_id.style.display == 'none')return '';
		if(typeof(self._oHelperIframe) == 'object' && self.frame_style && this.IE){
			this._oHelperIframe.style.display = 'none';
			//this._oHelperIframe.style.VISIBILITY = 'hidden';
		}
		this.container_id.style.display = 'none';
		this.layer_id.style.display = 'none';
		
		this.layer_id.onHide();
		this.display = false;
	}
	this.show = function () {
		//if(this.layer_id.style.display != 'none')return '';
		if(typeof(self._oHelperIframe) == 'object' && self.frame_style && self.IE){
			//this._oHelperIframe.style.VISIBILITY = 'visible';
			self._oHelperIframe.style.display = '';
		}
		//this.layer_id.style.VISIBILITY = 'visible';
		this.container_id.style.display = '';
		this.layer_id.style.display = '';
		this.layer_id.onShow();
		this.change_align( );

		this.display =  true;
	}

	this.flyStart = function (e){
		if(typeof(e) == 'object'){
			evt = self.fixE(self.NS ? e : event);
			var dx = parseInt(self.layer_id.style.left, 10);
			var dy = parseInt(self.layer_id.style.top, 10);
			var mx = parseInt(evt.mouseX, 10);
			var my = parseInt(evt.mouseY, 10);
			if((my - dy) > self.bodyHeight())self.layer_id.style.top = self.scrollTop();
			if((mx - dx) > self.bodyWidth())self.layer_id.style.left = self.scrollLeft();

			self.fly_x = mx+50;
			self.fly_y = my+50;
		
			self.evt_id = evt.srcElement.id;
		}
		else {
			this.attachToEvent(document, 'onmousemove', this.flyStart );
			window.setInterval(this.name + ".fly()",200);
		}
		
	}
	this.fly = function ( x, y) {
		var percent = 1;
		
		if(self.layer_id.id == self.evt_id && self.evt_id)return;
		var dx = parseInt(self.layer_id.style.left, 10);
		if(self.fly_x != dx) {
			percent = .03 * Math.abs(self.fly_x - dx );
			
			if(percent > 0) percent = Math.ceil(percent);
			else percent = Math.floor(percent);
			
			dx = (self.fly_x - dx > 0) ? dx + percent : dx - percent;
		}
		var dy = parseInt(self.layer_id.style.top, 10);
		if(self.fly_y != dy) {
			percent = .03 * Math.abs(self.fly_y - dy) ;
			if(percent > 0) percent = Math.ceil(percent);
			else percent = Math.floor(percent);
			
			dy = (self.fly_y - dy > 0) ? dy + percent : dy - percent;
		}                       
		self.fix_position(dx, dy);
		
		//window.status = "(dx)=" + dx + "(dy)=" + dy;
		//window.status += "(self.fly_x)=" + self.fly_x + "(self.fly_y)=" + self.fly_y;
	}


	this.heartBeat = function () {
		var diffX = this.scrollLeft(); 
		var diffY = this.scrollTop(); 

		if(diffX != this.lastScrollX) {
			//var percent = .1 * (diffX - this.lastScrollX);
			var percent = diffX - this.lastScrollX;
			if(percent > 0) percent = Math.ceil(percent);
			else percent = Math.floor(percent);
			this.layer_id.style.left = (parseInt(this.layer_id.style.left, 10) + percent + 'px');
			this.lastScrollX = this.lastScrollX + percent;
		}
		if(diffY != this.lastScrollY) {
			//var percent = .1 * (diffY - this.lastScrollY);
			var percent = diffY- this.lastScrollY;
			if(percent > 0) percent = Math.ceil(percent);
			else percent = Math.floor(percent);
			
			this.layer_id.style.top = (parseInt(this.layer_id.style.top, 10) + percent + 'px');
			this.lastScrollY = this.lastScrollY + percent;
			
		}                       
		var x = parseInt(self.layer_id.style.left, 10);
		var y = parseInt(self.layer_id.style.top, 10);
		
		self.fix_position(x, y);
	}
	this.fix_position = function (x, y){
		var w = self.get_width();
		var h = self.get_height();		
		
		var b_w = self.scrollLeft() + self.bodyWidth() - w;
		var b_h = self.scrollTop() + self.bodyHeight() - h;
		
		
		if(x >= b_w){
			x = b_w;
		}
		if(x <= 1){
			x = 1;
		}

		if(y >= b_h){
			y = b_h;
		}


		if(y <= 1){
			y = 1;
		}
		
		if( !$10powertalk_get(self.layer_div) || (self.layer_id.innerHTML.indexOf('.gif')==-1 && 
		           self.container_id.innerHTML.indexOf('.jpg')==-1 &&
		           self.container_id.innerHTML.indexOf('.swf')==-1 ) )
		{
		     self.init();
		     if( self.display )
		         self.show();
		}

		self.layer_id.style.left = x + "px";
		self.layer_id.style.top = y + "px";

		if(typeof(self._oHelperIframe) == 'object' && self.frame_style && self.IE){
			self._oHelperIframe.style.left = self.layer_id.style.left;
			self._oHelperIframe.style.top = self.layer_id.style.top;
		}
		
		//if(self.name=='inviteFloat')window.status = "width=" + w + ", height=" + h + ", b_w=" + b_w + ", b_h=" + b_h + ", x=" + x + ", y=" + y + "";
	}
	this.fix_size = function (){
		var w = self.get_width();
		var h = self.get_height();
		if(typeof(self._oHelperIframe) == 'object' && self.frame_style && self.IE){
			self._oHelperIframe.style.width =  w + 'px';
			self._oHelperIframe.style.height = h + 'px';
		}
	}


	this.DragStart = function (e){
		var evt = self.NS ? e : event;
		self.offsetx = evt.clientX;
		self.offsety = evt.clientY;
		self.drag_left = parseInt(self.layer_id.style.left, 10);
		self.drag_top = parseInt(self.layer_id.style.top, 10);
		self.width = self.get_width();
		self.height = self.get_height();				
		
		
		self.dragapproved = true;

		document.onmousemove = self.Drag;
		document.onmouseup = self.DragEnd;

		self.layer_id.onDragStart();

		return false;
	}

	this.Drag = function (e){

		if(self.dragapproved){
			
			var evt = self.NS ? e : event;
			var x = self.drag_left + evt.clientX - self.offsetx;
			var y = self.drag_top + evt.clientY - self.offsety;

			self.fix_position(x, y);
			self.layer_id.onDrag();
			self.layer_id.style.cursor="default";
			//window.status = 'x=' + x + ', y=' + y;
			return false;
		}
		
	}

	this.DragEnd = function (e){
		self.dragapproved = false;
		self.layer_id.onDragEnd();
		self.layer_id.style.cursor="default";

	}




	this.fixE = function (e){
		if (typeof e == 'undefined') e = window.event;
		if (typeof e.layerX == 'undefined') e.layerX = e.offsetX;
		if (typeof e.layerY == 'undefined') e.layerY = e.offsetY;
		if (typeof e.srcElement == 'undefined') e.srcElement = e.target;
		if(self.NS){
			e.mouseX  =  e.pageX;
			e.mouseY  =  e.pageY;
		}else if(!self.NS && document.getElementById){
			e.mouseX  =  event.x + self.scrollLeft();
			e.mouseY  =  event.y + self.scrollTop();
		}else{
			e.mouseX  =  event.x;
			e.mouseY  =  event.y;
		}

		return e;
	}

	this.attachToEvent = function (obj, name, func) {
		name = name.toLowerCase();
		// Add the hookup for the event.
		if(typeof(obj.addEventListener) != "undefined") {
			if(name.length > 2 && name.indexOf("on") == 0) name = name.substring(2, name.length);
				obj.addEventListener(name, func, false);
			} else if(typeof(obj.attachEvent) != "undefined"){
				obj.attachEvent(name, func);
			} else {
				if(eval("obj." + name) != null){
				// Save whatever defined in the event
				var oldOnEvents = eval("obj." + name);
				eval("obj." + name) = function(e) {
					try{
						func(e);
						eval(oldOnEvents);
					} catch(e){}
				};
			} else {
				eval("obj." + name) = func;
			}
		}
	}
	this.get_width = function (){
		var w = parseInt(this.layer_id.style.width, 10);
		if(!w && this.IE){
			 w = parseInt(this.layer_id.offsetWidth, 10);
		}
		if(!w){
			 w = 150;
		}
		return w;
	}
	this.get_height = function (){
		var h = parseInt(this.layer_id.style.height, 10);
		if(!h && this.IE){
			 h = parseInt(this.layer_id.offsetHeight, 10);
		} 
		if(!h){
			 h = 280;
		}
		return h;
	}
	this.scrollTop = function (){
		var s; 
		if (typeof(window.pageYOffset) != 'undefined') { 
			s = window.pageYOffset; 
		} 
		else if (typeof(document.compatMode) != 'undefined' &&document.documentElement.scrollTop > 0) { 
			s = document.documentElement.scrollTop; 
		}  
		else if (typeof(document.body) != 'undefined') { 
			s = document.body.scrollTop; 
		} 
		
		return parseInt(s, 10);
	}
	this.scrollLeft = function (){
		var s; 
		if (typeof(window.pageXOffset) != 'undefined') { 
			s = window.pageXOffset; 
		} 
		else if (typeof(document.compatMode) != 'undefined' &&document.documentElement.scrollLeft > 0) { 
			s = document.documentElement.scrollTop; 
		}
		else if (typeof(document.body) != 'undefined') { 
			s = document.body.scrollLeft; 
		} 
		return parseInt(s, 10);
	}
	this.bodyWidth = function (){ 
		var w = 0;
		if (!document.all) {
			w = document.body.clientWidth > document.documentElement.clientWidth ? document.documentElement.clientWidth : document.body.clientWidth;
		}
		else{
			w = document.documentElement.clientWidth == 0 ? document.body.clientWidth : document.documentElement.clientWidth;
		}
		return parseInt(w, 10);
		//return parseInt((document.all) ? document.body.clientWidth : window.innerWidth, 10);
	}
	this.bodyHeight = function (){
		var h = 0;
		if (!document.all) {
			h = document.body.clientHeight > document.documentElement.clientHeight ? document.documentElement.clientHeight : document.body.clientHeight;
		}
		else{
			h = document.documentElement.clientHeight == 0 ? document.body.clientHeight : document.documentElement.clientHeight;
		}
		return parseInt(h, 10);
		//return parseInt((document.all) ? document.body.clientHeight : window.innerHeight, 10);
	}
	this.change_align = function (a){
		var bodyWidth = self.scrollLeft() + self.bodyWidth();
		var bodyHeight = self.scrollTop() + self.bodyHeight();
		 
		var offset_top = 120;
		var offset_left = 2;
		var w = this.get_width();
		var h = this.get_height();

		if(this.align.length < 3)this.align = a;
		
		switch(this.align){
			case 'left':
				offset_left = this.padding;
				break;
			case 'center':
				offset_left = (bodyWidth - w) / 2;
				break;
			case 'right':
				offset_left = (bodyWidth - w - this.padding );
				break;
			default:
				if(parseInt(this.left) >= 0)offset_left = parseInt(this.left);
				break;
		}
		

		switch(this.valign){
			case 'top':
				offset_top = this.top;
				break;
			case 'middle':
				offset_top = (bodyHeight - h) / 2;
				break;
			case 'bottom':
				offset_top = (bodyHeight - h - this.top );
				break;
			default:
				if(parseInt(this.top) >= 0)offset_top = parseInt(this.top);
				break;
		}
		offset_top = offset_top < 0 ? 150 : offset_top;
		
		self.fix_position(offset_left+self.scrollLeft(), offset_top+ self.scrollTop());
	}
}
