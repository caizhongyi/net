/*!
 * Image3D
 * Copyright (c) 2010 cloudgamer
 * Blog: http://cloudgamer.cnblogs.com/
 * Date: 2010-9-18
 */

//容器对象
var Image3D = function(container, options){
	this._initialize( container, options );
	this._initMode();
	if ( this._support ) {
		this._initContainer();
	} else {//模式不支持
		this.onError("not support");
	}
};
Image3D.prototype = {
  //初始化程序
  _initialize: function(container, options) {
	var container = this._container = $$(container);
	this._clientWidth = container.clientWidth;//显示区域宽度
	this._clientHeight = container.clientHeight;//显示区域高度
	this._support = false;//是否支持指定模式
	this._layers = {};//层集合
	this._invalid = [];//无效层集合
	this._show = $$.emptyFunction;
	
	var opt = this._setOptions(options);
	
	this.fixedFar = opt.fixedFar;
	
	this._x = opt.x;
	this._y = opt.y;
	this._z = opt.z;
	this._r = opt.r;
	this._getScale = opt.getScale;
	
	this.onError = opt.onError;
	
	$$CE.fireEvent( this, "init" );
  },
  //设置默认属性
  _setOptions: function(options) {
    this.options = {//默认值
		mode:		"css3|zoom|base",//模式
		x:			0,//水平偏移值
		y:			0,//垂直偏移值
		z:			0,//深度偏移值
		r:			0,//旋转角度(css3支持)
		fixedFar:	false,//是否远点固定
		getScale:	function(z){ return 1 - z / 1000; },//获取比例方法
		onError:	function(err){}//出错时执行
    };
    return $$.extend(this.options, options || {});
  },
  //模式设置
  _initMode: function() {
	var modes = Image3D.modes;
	this._support = $$A.some( this.options.mode.toLowerCase().split("|"), function(mode){
		mode = modes[ mode ];
		if ( mode && mode.support ) {
			this._show = mode.show; return true;
		}
	}, this );
  },
  //初始化容器对象
  _initContainer: function() {
	var container = this._container, style = container.style, position = $$D.getStyle( container, "position" );
	this._style = { "position": style.position, "overflow": style.overflow };//备份样式
	if ( position != "relative" && position != "absolute" ) { style.position = "relative"; }
	style.overflow = "hidden";
	$$CE.fireEvent( this, "initContainer" );
  },
  //显示
  show: function() {
	if ( !this._support ){ this.onError("not support"); return; }
	$$A.forEach( this._layers, function(layer, z){ this._showLayer( z * 1 ); }, this );
  },
  //根据深度显示层
  _showLayer: function(z) {
	var layer = this._layers[ z ], scale = this._getScale( z + this._z );
	if ( scale <= 1 && scale > 0 ) {
		var moveScale = this.fixedFar ? scale : (1 - scale);
		this._show( layer, scale, this._x * moveScale, this._y * moveScale );
		layer.style.visibility  = "visible";
	} else {
		layer.style.visibility  = "hidden";
	}
  },
  //添加图片
  add: function(src, options) {
	if ( !this._support ){ this.onError("not support"); return; }
	var img = new Image(), opt = options || {}, oThis = this;
	//加载函数
	function load(){ this.onload = null; oThis._load( this, opt); };
	function error(){ oThis.onError("err image"); };
	//加载图片
	img.onload = load; img.onerror = error; img.src = src;
	//返回图片操作对象
	return {
		img: img,
		src: src,
		options: opt,
		show: function(){//重新显示
			oThis._remove(img); img.onload = load; img.src = this.src;
		},
		remove: function(){ oThis._remove(img); }//移除
	}
  },
  //加载图片
  _load: function(img, options) {
	//设置属性
	var opt = $$.extend({//默认值
		x:		0,//水平位移
		y:		0,//垂直位移
		z:		0,//深度
		width:	0,//宽度
		height:	0,//高度
		scaleW:	1,//宽度缩放比例
		scaleH:	1//高度缩放比例
	}, options || {} );
	//图片定位
	var clientWidth = this._clientWidth, clientHeight = this._clientHeight,
		width = opt.width || img.width * opt.scaleW,
		height = opt.height || img.height * opt.scaleH;
		z = img._z = opt.z;
	//设置样式
	img.style.cssText = "position:absolute;border:0;padding:0;margin:0;-ms-interpolation-mode:nearest-neighbor;"
		+ "z-index:" + (99999 - z) + ";width:" + width + "px;height:" + height + "px;"
		+ "left:" + (((clientWidth - width) / 2 + opt.x) / clientWidth * 100).toFixed(5) + "%;"
		+ "top:" + ((clientHeight - height - opt.y) / clientHeight * 100).toFixed(5) + "%;";
	//插入层并显示
	this._insertLayer( img, z );
	this._showLayer( z );
  },
  //插入层
  _insertLayer: function(img, z) {
	var layer = this._layers[ z ];
	if ( !layer ) {//创建层
		layer = this._invalid.pop();
		if ( !layer ) {
			layer = document.createElement("div");
			layer.style.cssText = "position:absolute;border:0;padding:0;margin:0;left:0;top:0;visibility:hidden;background:transparent;width:" + this._clientWidth + "px;height:" + this._clientHeight + "px";
		}
		//修正zIndex
		if ( $$B.ie6 || $$B.ie7 ) { layer.style.zIndex = 99999 - z; }
		layer._count = 0;//记录层包含图片数
		layer._z = z;
		this._layers[ z ] = this._container.appendChild(layer);
	}
	layer._count++;
	layer.appendChild(img);
  },
  //移除
  _remove: function(img) {
	var z = img._z, layer = this._layers[ z ];
	if ( layer && img.parentNode == layer ) {//确定正确元素
		layer.removeChild(img);
		if ( !--layer._count ) {//层里面没有图片
			delete this._layers[ z ];
			this._invalid.push(this._container.removeChild(layer));//可重复使用
		}
	}
  },
  //重置
  reset: function() {
	var opt = this.options;
	this._x = opt.x; this._y = opt.y; this._z = opt.z; this._r = opt.r;
	this.show();
  },
  //销毁程序
  dispose: function() {
	$$CE.fireEvent( this, "dispose" );
	//清除dom
	var container = this._container;
	$$D.setStyle( container, this._style );//恢复样式
	//清除层元素
	$$A.forEach( this._layers, function(layer){
		layer.innerHTML = ""; container.removeChild(layer);
	});
	//清除属性
	this._container = this._invalid = this._layers = this._style = this._support = null;
  }
};


//变换模式
Image3D.modes = function(){
	var unit = $$B.firefox ? "px" : "", css3Transform;//ccs3变换样式
	return {
		css3: {//css3设置
			support: function(){
				var style = document.createElement("div").style;
				return $$A.some(
					[ "transform", "MozTransform", "webkitTransform", "OTransform" ],
					function(css){ if ( css in style ) {
						css3Transform = css; return true;
					}});
			}(),
			show: function(layer, scale, x, y) {
				var Cos = Math.cos(this._r), Sin = Math.sin(this._r);
				layer.style.zIndex = 99999 - layer._z;
				//设置变换
				layer.style[ css3Transform ] = "matrix("
					+ ( Cos * scale).toFixed(5) + "," + (Sin * scale).toFixed(5) + ","
					+ (-Sin * scale).toFixed(5) + "," + (Cos * scale).toFixed(5) + ", "
					+ Math.round(x) + unit + ", " + Math.round(y) + unit + ")";
			}
		},
		zoom: {//zoom设置
			support: function(){ return "zoom" in document.createElement("div").style; }(),
			show: function(layer, scale, x, y){
				var style = layer.style, MAX = Number.MAX_VALUE, opScale = 1 - scale,
					left = this._clientWidth * opScale / 2 + x,
					top = this._clientHeight * opScale / 2 + y;
				//定位修正
				if ( !$$B.ie6 && !$$B.ie7 ) {  left /= scale; top /= scale; }
				//值修正
				left = Math.min(MAX, Math.max( -MAX, left )) | 0;
				top = Math.min(MAX, Math.max( -MAX, top )) | 0;
				//样式设置
				style.zoom = scale; style.left = left + "px"; style.top = top + "px";
			}
		},
		base: {//base设置
			support: true,
			show: function(layer, scale, x, y){
				var opScale = 1 - scale,
					left = this._clientWidth * opScale / 2 + x,
					top = this._clientHeight * opScale / 2 + y;
				//设置
				$$A.forEach( layer.getElementsByTagName("img"), function(img){
					//获取记录的数据
					var original = img._original = img._original || {
						width: img.offsetWidth,	height: img.offsetHeight,
						left: img.offsetLeft, 	top: img.offsetTop
					};
					//样式设置
					$$D.setStyle( img, {
						width: (original.width * scale | 0) + "px",
						height: (original.height * scale | 0) + "px",
						left: (original.left * scale + left | 0) + "px",
						top: (original.top * scale + top | 0) + "px"
					});
				});
			}
		}
	};
}();


//拖动视觉变换
Image3D.prototype._initialize = (function(){
	var init = Image3D.prototype._initialize,
		MAX = Number.MAX_VALUE,
		methods = {
			"init": function(){
				var opt = this.options;
				this._mtMinX = opt.mrMinX;
				this._mtMaxX = opt.mrMaxX;
				this._mtMinY = opt.mrMinY;
				this._mtMaxY = opt.mrMaxY;
				this._mtSTART = $$F.bind( start, this );
				this._mtMOVE = $$F.bind( move, this );
				this._mtSTOP = $$F.bind( stop, this );
			},
			"initContainer": function(){
				$$E.addEvent( this._container, "mousedown", this._mtSTART );
			},
			"dispose": function(){
				$$E.removeEvent( this._container, "mousedown", this._mtSTART );
				this._mtSTOP();
				this._mtSTART = this._mtMOVE = this._mtSTOP = null;
			}
		};
	//开始函数
	function start(e){
		this._mtX = this._x + e.clientX;
		this._mtY = this._y + e.clientY;
		$$E.addEvent( document, "mousemove", this._mtMOVE );
		$$E.addEvent( document, "mouseup", this._mtSTOP );
		if ( $$B.ie ) {
			var container = this._container;
			$$E.addEvent( container, "losecapture", this._mtSTOP );
			container.setCapture();
		} else {
			$$E.addEvent( window, "blur", this._mtSTOP );
			e.preventDefault();
		}
	};
	//拖动函数
	function move(e){
		this._x = Math.min(this._mtMaxX, Math.max( this._mtMinX, this._mtX - e.clientX ));
		this._y = Math.min(this._mtMaxY, Math.max( this._mtMinY, this._mtY - e.clientY ));
		this.show();
		window.getSelection ? window.getSelection().removeAllRanges() : document.selection.empty();
	};
	//停止函数
	function stop(){
		$$E.removeEvent( document, "mousemove", this._mtMOVE );
		$$E.removeEvent( document, "mouseup", this._mtSTOP );
		if ( $$B.ie ) {
			var container = this._container;
			$$E.removeEvent( container, "losecapture", this._mtSTOP );
			container.releaseCapture();
		} else {
			$$E.removeEvent( window, "blur", this._mtSTOP );
		};
	};
	return function(){
		var options = arguments[1];
		if ( !options || options.mouseTranslate !== false ) {
			//扩展options
			$$.extend( options, {
				mrMinX:	-Number.MAX_VALUE,//x最小值
				mrMaxX:	Number.MAX_VALUE,//x最大值
				mrMinY:	-Number.MAX_VALUE,//y最小值
				mrMaxY:	Number.MAX_VALUE//y最大值
			}, false );
			//扩展钩子
			$$A.forEach( methods, function( method, name ){
				$$CE.addEvent( this, name, method );
			}, this );
		}
		init.apply( this, arguments );
	}
})();

//滚轮深度变换
Image3D.prototype._initialize = (function(){
	var init = Image3D.prototype._initialize,
		mousewheel = $$B.firefox ? "DOMMouseScroll" : "mousewheel",
		methods = {
			"init": function(){
				this._mzMin = this.options.mzMin;
				this._mzMax = this.options.mzMax;
				this._mzZoom = $$F.bind( zoom, this );
			},
			"initContainer": function(){
				$$E.addEvent( this._container, mousewheel, this._mzZoom );
			},
			"dispose": function(){
				$$E.removeEvent( this._container, mousewheel, this._mzZoom );
				this._mzZoom = null;
			}
		};
	//缩放函数
	function zoom(e){
		this._z = Math.min(this._mzMax, Math.max( this._mzMin,
			(e.wheelDelta ? -e.wheelDelta : e.detail * 40) + this._z ));
		this.show();
		e.preventDefault();
	};
	return function(){
		var options = arguments[1];
		if ( !options || options.mouseZoom !== false ) {
			//扩展options
			$$.extend( options, {
				mzMin:	-Number.MAX_VALUE,//z最小值
				mzMax:	Number.MAX_VALUE//z最大值
			}, false );
			//扩展钩子
			$$A.forEach( methods, function( method, name ){
				$$CE.addEvent( this, name, method );
			}, this );
		}
		init.apply( this, arguments );
	}
})();