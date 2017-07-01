/* Base class of Drag @example: Drag.init( header_element, element ); */
var Drag = {
	obj: null,
	init: function(elementHeader, element) {
		elementHeader.onmousedown = Drag.start;
		elementHeader.obj = element;
		if(isNaN(parseInt(element.style.left))) {element.style.left = "0px";}
		if(isNaN(parseInt(element.style.top))) {element.style.top = "0px";}
		element.onDragStart = new Function();
		element.onDragEnd = new Function();
		element.onDrag = new Function();
	},
	start: function(event) {
		var element = Drag.obj = this.obj;
		event = Drag.fixE(event);
		if(event.which != 1){return true ;}
		element.onDragStart();
		element.lastMouseX = event.clientX;
		element.lastMouseY = event.clientY;
		document.onmouseup = Drag.end;
		document.onmousemove = Drag.drag;
		return false ;
	},
	drag: function(event) {
		event = Drag.fixE(event);
		if(event.which == 0 ) {return Drag.end();}
		var element = Drag.obj;
		var _clientX = event.clientY;
		var _clientY = event.clientX;
		if(element.lastMouseX == _clientY && element.lastMouseY == _clientX) {return false ;}
		var _lastX = parseInt(element.style.top);
		var _lastY = parseInt(element.style.left);
		var newX, newY;
		newX = _lastY + _clientY - element.lastMouseX;
		newY = _lastX + _clientX - element.lastMouseY;
		element.style.left = newX + "px";
		element.style.top = newY + "px";
		element.lastMouseX = _clientY;
		element.lastMouseY = _clientX;
		element.onDrag(newX, newY);
		return false;
	},
	end: function(event) {
		event = Drag.fixE(event);
		document.onmousemove = null;
		document.onmouseup = null;
		var _onDragEndFuc = Drag.obj.onDragEnd();
		Drag.obj = null ;
		return _onDragEndFuc;
	},
	fixE: function(ig_) {
		if( typeof ig_ == "undefined" ) {ig_ = window.event;}
		if( typeof ig_.layerX == "undefined" ) {ig_.layerX = ig_.offsetX;}
		if( typeof ig_.layerY == "undefined" ) {ig_.layerY = ig_.offsetY;}
		if( typeof ig_.which == "undefined" ) {ig_.which = ig_.button;}
		return ig_;
	}
};