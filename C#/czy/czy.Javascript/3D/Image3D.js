/*!
 * Image3D
 * Copyright (c) 2010 cloudgamer
 * Blog: http://cloudgamer.cnblogs.com/
 * Date: 2010-9-18
 */

//��������
var Image3D = function(container, options){
	this._initialize( container, options );
	this._initMode();
	if ( this._support ) {
		this._initContainer();
	} else {//ģʽ��֧��
		this.onError("not support");
	}
};
Image3D.prototype = {
  //��ʼ������
  _initialize: function(container, options) {
	var container = this._container = $$(container);
	this._clientWidth = container.clientWidth;//��ʾ������
	this._clientHeight = container.clientHeight;//��ʾ����߶�
	this._support = false;//�Ƿ�֧��ָ��ģʽ
	this._layers = {};//�㼯��
	this._invalid = [];//��Ч�㼯��
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
  //����Ĭ������
  _setOptions: function(options) {
    this.options = {//Ĭ��ֵ
		mode:		"css3|zoom|base",//ģʽ
		x:			0,//ˮƽƫ��ֵ
		y:			0,//��ֱƫ��ֵ
		z:			0,//���ƫ��ֵ
		r:			0,//��ת�Ƕ�(css3֧��)
		fixedFar:	false,//�Ƿ�Զ��̶�
		getScale:	function(z){ return 1 - z / 1000; },//��ȡ��������
		onError:	function(err){}//����ʱִ��
    };
    return $$.extend(this.options, options || {});
  },
  //ģʽ����
  _initMode: function() {
	var modes = Image3D.modes;
	this._support = $$A.some( this.options.mode.toLowerCase().split("|"), function(mode){
		mode = modes[ mode ];
		if ( mode && mode.support ) {
			this._show = mode.show; return true;
		}
	}, this );
  },
  //��ʼ����������
  _initContainer: function() {
	var container = this._container, style = container.style, position = $$D.getStyle( container, "position" );
	this._style = { "position": style.position, "overflow": style.overflow };//������ʽ
	if ( position != "relative" && position != "absolute" ) { style.position = "relative"; }
	style.overflow = "hidden";
	$$CE.fireEvent( this, "initContainer" );
  },
  //��ʾ
  show: function() {
	if ( !this._support ){ this.onError("not support"); return; }
	$$A.forEach( this._layers, function(layer, z){ this._showLayer( z * 1 ); }, this );
  },
  //���������ʾ��
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
  //���ͼƬ
  add: function(src, options) {
	if ( !this._support ){ this.onError("not support"); return; }
	var img = new Image(), opt = options || {}, oThis = this;
	//���غ���
	function load(){ this.onload = null; oThis._load( this, opt); };
	function error(){ oThis.onError("err image"); };
	//����ͼƬ
	img.onload = load; img.onerror = error; img.src = src;
	//����ͼƬ��������
	return {
		img: img,
		src: src,
		options: opt,
		show: function(){//������ʾ
			oThis._remove(img); img.onload = load; img.src = this.src;
		},
		remove: function(){ oThis._remove(img); }//�Ƴ�
	}
  },
  //����ͼƬ
  _load: function(img, options) {
	//��������
	var opt = $$.extend({//Ĭ��ֵ
		x:		0,//ˮƽλ��
		y:		0,//��ֱλ��
		z:		0,//���
		width:	0,//���
		height:	0,//�߶�
		scaleW:	1,//������ű���
		scaleH:	1//�߶����ű���
	}, options || {} );
	//ͼƬ��λ
	var clientWidth = this._clientWidth, clientHeight = this._clientHeight,
		width = opt.width || img.width * opt.scaleW,
		height = opt.height || img.height * opt.scaleH;
		z = img._z = opt.z;
	//������ʽ
	img.style.cssText = "position:absolute;border:0;padding:0;margin:0;-ms-interpolation-mode:nearest-neighbor;"
		+ "z-index:" + (99999 - z) + ";width:" + width + "px;height:" + height + "px;"
		+ "left:" + (((clientWidth - width) / 2 + opt.x) / clientWidth * 100).toFixed(5) + "%;"
		+ "top:" + ((clientHeight - height - opt.y) / clientHeight * 100).toFixed(5) + "%;";
	//����㲢��ʾ
	this._insertLayer( img, z );
	this._showLayer( z );
  },
  //�����
  _insertLayer: function(img, z) {
	var layer = this._layers[ z ];
	if ( !layer ) {//������
		layer = this._invalid.pop();
		if ( !layer ) {
			layer = document.createElement("div");
			layer.style.cssText = "position:absolute;border:0;padding:0;margin:0;left:0;top:0;visibility:hidden;background:transparent;width:" + this._clientWidth + "px;height:" + this._clientHeight + "px";
		}
		//����zIndex
		if ( $$B.ie6 || $$B.ie7 ) { layer.style.zIndex = 99999 - z; }
		layer._count = 0;//��¼�����ͼƬ��
		layer._z = z;
		this._layers[ z ] = this._container.appendChild(layer);
	}
	layer._count++;
	layer.appendChild(img);
  },
  //�Ƴ�
  _remove: function(img) {
	var z = img._z, layer = this._layers[ z ];
	if ( layer && img.parentNode == layer ) {//ȷ����ȷԪ��
		layer.removeChild(img);
		if ( !--layer._count ) {//������û��ͼƬ
			delete this._layers[ z ];
			this._invalid.push(this._container.removeChild(layer));//���ظ�ʹ��
		}
	}
  },
  //����
  reset: function() {
	var opt = this.options;
	this._x = opt.x; this._y = opt.y; this._z = opt.z; this._r = opt.r;
	this.show();
  },
  //���ٳ���
  dispose: function() {
	$$CE.fireEvent( this, "dispose" );
	//���dom
	var container = this._container;
	$$D.setStyle( container, this._style );//�ָ���ʽ
	//�����Ԫ��
	$$A.forEach( this._layers, function(layer){
		layer.innerHTML = ""; container.removeChild(layer);
	});
	//�������
	this._container = this._invalid = this._layers = this._style = this._support = null;
  }
};


//�任ģʽ
Image3D.modes = function(){
	var unit = $$B.firefox ? "px" : "", css3Transform;//ccs3�任��ʽ
	return {
		css3: {//css3����
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
				//���ñ任
				layer.style[ css3Transform ] = "matrix("
					+ ( Cos * scale).toFixed(5) + "," + (Sin * scale).toFixed(5) + ","
					+ (-Sin * scale).toFixed(5) + "," + (Cos * scale).toFixed(5) + ", "
					+ Math.round(x) + unit + ", " + Math.round(y) + unit + ")";
			}
		},
		zoom: {//zoom����
			support: function(){ return "zoom" in document.createElement("div").style; }(),
			show: function(layer, scale, x, y){
				var style = layer.style, MAX = Number.MAX_VALUE, opScale = 1 - scale,
					left = this._clientWidth * opScale / 2 + x,
					top = this._clientHeight * opScale / 2 + y;
				//��λ����
				if ( !$$B.ie6 && !$$B.ie7 ) {  left /= scale; top /= scale; }
				//ֵ����
				left = Math.min(MAX, Math.max( -MAX, left )) | 0;
				top = Math.min(MAX, Math.max( -MAX, top )) | 0;
				//��ʽ����
				style.zoom = scale; style.left = left + "px"; style.top = top + "px";
			}
		},
		base: {//base����
			support: true,
			show: function(layer, scale, x, y){
				var opScale = 1 - scale,
					left = this._clientWidth * opScale / 2 + x,
					top = this._clientHeight * opScale / 2 + y;
				//����
				$$A.forEach( layer.getElementsByTagName("img"), function(img){
					//��ȡ��¼������
					var original = img._original = img._original || {
						width: img.offsetWidth,	height: img.offsetHeight,
						left: img.offsetLeft, 	top: img.offsetTop
					};
					//��ʽ����
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


//�϶��Ӿ��任
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
	//��ʼ����
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
	//�϶�����
	function move(e){
		this._x = Math.min(this._mtMaxX, Math.max( this._mtMinX, this._mtX - e.clientX ));
		this._y = Math.min(this._mtMaxY, Math.max( this._mtMinY, this._mtY - e.clientY ));
		this.show();
		window.getSelection ? window.getSelection().removeAllRanges() : document.selection.empty();
	};
	//ֹͣ����
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
			//��չoptions
			$$.extend( options, {
				mrMinX:	-Number.MAX_VALUE,//x��Сֵ
				mrMaxX:	Number.MAX_VALUE,//x���ֵ
				mrMinY:	-Number.MAX_VALUE,//y��Сֵ
				mrMaxY:	Number.MAX_VALUE//y���ֵ
			}, false );
			//��չ����
			$$A.forEach( methods, function( method, name ){
				$$CE.addEvent( this, name, method );
			}, this );
		}
		init.apply( this, arguments );
	}
})();

//������ȱ任
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
	//���ź���
	function zoom(e){
		this._z = Math.min(this._mzMax, Math.max( this._mzMin,
			(e.wheelDelta ? -e.wheelDelta : e.detail * 40) + this._z ));
		this.show();
		e.preventDefault();
	};
	return function(){
		var options = arguments[1];
		if ( !options || options.mouseZoom !== false ) {
			//��չoptions
			$$.extend( options, {
				mzMin:	-Number.MAX_VALUE,//z��Сֵ
				mzMax:	Number.MAX_VALUE//z���ֵ
			}, false );
			//��չ����
			$$A.forEach( methods, function( method, name ){
				$$CE.addEvent( this, name, method );
			}, this );
		}
		init.apply( this, arguments );
	}
})();