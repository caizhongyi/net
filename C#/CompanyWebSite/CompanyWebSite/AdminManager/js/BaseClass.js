
var Class = {
create: function() {
   return function() { this.initialize.apply(this, arguments); }
}
}
//****************
//
//***************
//Ϊ���������
var Extend = function(destination, source) {
for (var property in source) {
   destination[property] = source[property];
}
}
//Ϊ���������¼�(��������)
var Bind = function(object, fun) {
return function() {
   return 
}
}
//Ϊ���������¼�(��������)
var BindAsEventListener = function(object, fun) {
return function(event) {
   return fun.call(object, (event || window.event));
}
}
//*********************************************

function addEventHandler(oTarget, sEventType, fnHandler){
	if (oTarget.addEventListener) {
		oTarget.addEventListener(sEventType, fnHandler, false);
	}
	else 
		if (oTarget.attachEvent) {
			oTarget.attachEvent("on" + sEventType, fnHandler);
		}
		else {
			oTarget["on" + sEventType] = fnHandler;
		}
}


function removeEventHandler(oTarget, sEventType, fnHandler) {
    if (oTarget.removeEventListener) {
        oTarget.removeEventListener(sEventType, fnHandler, false);
    } else if (oTarget.detachEvent) {
        oTarget.detachEvent("on" + sEventType, fnHandler);
    } else { 
        oTarget["on" + sEventType] = null;
    }
}

Function.prototype.bind = function(object) { 
  var method = this; 
  return function() { 
    method.apply(object, arguments); 
  } 
} 
