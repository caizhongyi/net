/*
* OOCSS 2.0 Beta
* Copyright(c) 2009-2010, 李子哥哥.
* myoocss@gmail.com
* http://www.oo-css.cn
*/

//## ooCss
var ooCss =
{
	LINK_OFF: false, 			//关闭LINK标签
	OOCSS_OFF: false, 			//关闭OOCSS标签
	DEBUG_OFF:true,				//关闭调试
	defer: 0, 					//延迟加载（毫秒）

	regMultiBlank: /[\s\r\n]+/g,//多个空
	regBlank: /^[\s\r\n]*$/g, 	//空
	regAllLeftLargeBracket: /\{/g,
	regAllRightLargeBracket: /\}/g,

	browerType: 'IE',
	vars: {},
	varsInfos:{},
	
	//## debug
	debug : 
	{
		outputDivID: 'output',
		messages:[],
		line:1,
		
		//## output
		_output:function(msg)
		{
			var child = document.createElement('div')
			child.innerHTML ='<br />'+(this.line++)+'<br />'+ msg
			this.outputDiv.appendChild(child)
		},
		output : function(msg)
		{
			if(ooCss.DEBUG_OFF)
				return;
			
			if(this.hasOutputDiv == null)
			{			
				this.outputDiv = document.getElementById(this.outputDivID)
				this.hasOutputDiv = this.outputDiv != null
				if(!this.hasOutputDiv && this.outputDivID)
				{
					function _showAll()
					{
						this.outputDiv =this.outputDiv|| document.getElementById(this.outputDivID)
						if(this.outputDiv==null && document.body)
						{
							var outputDiv=this.outputDiv=document.createElement('div');
							outputDiv.id=this.outputDivID
							outputDiv.title=ooCss.local.output+'   '+ooCss.local.dblClickClose
							outputDiv.innerHTML='<a style="margin:0px;padding:0px;top:3px;right:10px;width:auto;height:auto;background:none;position:absolute" target=_blank href="http://www.oo-css.cn/help">{0}</a>'.oc_format(ooCss.local.help)
							var minimized=0
							outputDiv.ondblclick=function()
							{
								if(minimized)
									this.style.cssText=''	
								else
									this.style.cssText = 'width:10px;height:10px;min-width:10px;min-height:10px;overflow:hidden;border:1px solid blue'
								minimized^=true;
							}
							var sheet=ooCss.brower.createStyleSheet()
							ooCss.brower.addRule(sheet,"#{0}".oc_format(outputDiv.id),'z-index:10000;font-size:9pt;color:Black;border:1px solid gray;margin:5px;padding:5px;position:absolute;bottom:10px;min-width:200px;max-width:800px;max-height:300px;overflow:auto')
							ooCss.brower.addRule(sheet,"#{0}:hover".oc_format(outputDiv.id),'background-color:#EAE6D3')
							ooCss.brower.addRule(sheet,"#{0} div".oc_format(outputDiv.id),'text-align:left')
							document.body.appendChild(outputDiv)
						}
						if(this.outputDiv)
						{
							for(var i = 0; i < this.messages.length; i ++ )
								this._output(this.messages[i])
							this.messages=[]
						}
					}
					ooCss.brower.addListener(window, 'onload',_showAll , this)
					_showAll.oc_timeout(100,this)
				}
			}
			
			if(this.outputDiv == null)
			{
				this.messages.push(msg)
			}
			else 
			{
				this._output(msg)
			}

			window.status = msg
		},
		//#end
		
		error : function(msg)
		{
			throw new Error(msg)
		}
	},
	//#end

	//## load ,create
	load : function(url, defer, namespace)
	{
		return new ooCss.Sheet(
		{
			url : url,
			namespace : namespace,
			defer : defer
		})
	},
	
	
	create : function(defer, namespace, content)
	{
		return new ooCss.Sheet(
		{
			content : content,
			namespace : namespace,
			defer : defer
		})
	},
	//#end
	
	
	//## renderOOCss ,renderPageLink
	renderDom : function(dom, tagName)
	{
		if(dom)
		{
			var content = '';
			if(dom.innerHTML)
			{
				content = dom.innerHTML
				if(content.search(/[^\s\r\n]+/) < 0)
				content = ''
			}
			else if(dom.nextSibling && dom.nextSibling.nodeName == '#text' && dom.nextSibling.nextSibling && dom.nextSibling.nextSibling.nodeName.indexOf(tagName.toUpperCase()) > -1)
			{
				content = dom.nextSibling.nodeValue;
				dom.nextSibling.parentNode.removeChild(dom.nextSibling)
			}
			
			
			var url = dom.getAttribute('url')
			if(url || dom.getAttribute('disabled') != true)
			{
				new ooCss.Sheet(
				{
					url : url,
					content : content,
					namespace : dom.getAttribute('namespace'),
					defer : dom.getAttribute('defer')
				})
				window.setTimeout(function()
				{
				    dom.parentNode.removeChild(dom)
				}, 10)
			}
		}
		else
		{
			var doms = document.getElementsByTagName(tagName);
			for(var i = 0; i < doms.length; i ++ )
			{
				dom = doms[i]
				this.renderDom(dom, tagName)
			}
		}
	},
	renderOOCss : function(dom)
	{
		this.renderDom(dom, 'OOCSS');
	},
	renderPageLink : function(dom)
	{
		this.renderDom(dom, 'LINK');
	},
	
	//#end

	//## _onPageLoad ,init
	_onPageLoad: function() {
		if (this.LINK_OFF != true)
			this.renderPageLink()
		if (this.OOCSS_OFF != true)
			this.renderOOCss()
	},

	init: function() {
		this.brower = this.browerFactory.Common
		var ua = navigator.userAgent.toLowerCase();
		if (ua.indexOf("msie") > -1)
			this.browerType = "IE"
		else if (ua.indexOf('firefox') > -1)
			this.browerType = "Firefox"
		else if (ua.indexOf('opera') > -1)
			this.browerType = "Opera"
		else if (ua.indexOf('safari') > -1)
			this.browerType = "Safari"
		else if (ua.indexOf('camino') > -1)
			this.browerType = "Camino"
		else if (ua.indexOf('gecko') > -1)
			this.browerType = "Gecko"

		//var isStrict=document.compatMode=="CSS1Compat",isOpera=ua.indexOf("opera")>-1,isSafari=(/webkit|khtml/).oc_test(ua),isSafari3=isSafari&&ua.indexOf("webkit/5")!=-1,isIE=!isOpera&&ua.indexOf("msie")>-1,isIE7=!isOpera&&ua.indexOf("msie 7")>-1,isGecko=!isSafari&&ua.indexOf("gecko")>-1,isGecko3=!isSafari&&ua.indexOf("rv:1.9")>-1,isBorderBox=isIE&&!isStrict,isWindows=(ua.indexOf("windows")!=-1||ua.indexOf("win32")!=-1),isMac=(ua.indexOf("macintosh")!=-1||ua.indexOf("mac os x")!=-1),isAir=(ua.indexOf("adobeair")!=-1),isLinux=(ua.indexOf("linux")!=-1),isSecure=window.location.href.toLowerCase().indexOf("https")===0;if(isIE&&!isIE7){try{document.execCommand("BackgroundImageCache",false,true)}catch(e){}}Ext.apply(Ext,{isStrict:isStr
		this.brower = this.browerFactory[this.browerType] || this.brower;
        
		if (this.LINK_OFF != true) {
			if (document.readyState == 'complete') {
				this._onPageLoad()
			}
			else {
				this.brower.addListener(window, 'onload', this._onPageLoad, this)
			}
		}
	}
	//#end
};
//#end

//## ooCss.browerFactory
ooCss.browerUtils=
{
	Opera_Firefox_createStyleSheet:function () 
	{
		var dom=document.createElement('style');
		document.getElementsByTagName('head')[0].appendChild(dom)
		var sheet=document.styleSheets[document.styleSheets.length-1]
		sheet.rules=sheet.cssRules
		return sheet
	},
	Opera_Firefox_addRule:function(sheet,selector,style)
	{
		return sheet.insertRule(selector+"{"+style+"}",sheet.cssRules.length)
	},
	lastIndexOf:function(str,reg)
	{
		var ms=str.match(reg)
		if(ms==null)
			return -1
			
		if(ooCss.browerType=="IE")
		{
			return ms.index
		}
		else 
		{
			var parts=str.split(reg)
			return str.length-parts[parts.length-1].length-ms[ms.length-1].length
		}
	}
}

ooCss.browerFactory=
{
	IE:
	{
		addListener:function(dom,eName,handler,scope)
		{
			if(scope)
				handler=handler.oc_bind(scope)
			dom.attachEvent(eName,handler)
		},
		createStyleSheet:function(){return document.createStyleSheet();},
		addRule:function(sheet,selector,style)
		{
         
		     //ie6
				var regs=[ 
						  /\[.*\]/,  
						  /.*\>.*/,
						  /.*\~.*/,
						  /.*\+.*/
				];
				for(var i=0;i<regs.length;i++)
				{
					if(regs[i].test(selector))
					{
						selector="";
						style="";
						return;
					}
				}
		     //endie6
			return sheet.addRule(selector,style);
			
		}
	},
	
	Common:
	{
		addListener:function(dom,eName,handler,scope)
		{
			eName=eName.replace(/^on/i,'')
			if(scope)
				handler=handler.oc_bind(scope)
			dom.addEventListener(eName,handler,false)
		},
		createStyleSheet:ooCss.browerUtils.Opera_Firefox_createStyleSheet,
		addRule:ooCss.browerUtils.Opera_Firefox_addRule
	}
}

//#end

//## ooCss.utils
ooCss.regEval=/eval[\s\r\n]*\(/,		//表达式开始
ooCss.utils=
{
	modifier :
	{
		'private':1,
		'protected':2,
		'internal':4,
		'public':8,
		
		'sealed':16,
		'abstract':32
		//virtual:64
	},
	
	object:function()
	{
		function oobase(){}
		oobase.prototype=
		{
			isPrivate:false,
			isProtected:false,
			isInternal:false,
			isPublic:false,
			isSealed:false,
			isAbstract:false,
			modifier:null
		}
		return 	oobase
	}(),
	
	initByModifier:function () 
	{
		var modifiers=""
		for(var m in this.modifier)
		{
			modifiers+=m+"|"
			var pName="is"+m.charAt(0).toUpperCase()+m.substr(1)
//			ooCss.Member.prototype[pName]=false
//			ooCss.Type.prototype[pName]=false
		}
//		ooCss.Member.prototype.modifier=null;
//		ooCss.Type.prototype.modifier=null;
		ooCss.regAllModifier=new RegExp("\\b("+modifiers.substr(0,modifiers.length-1)+")\\b","g")
	},
	
	//## parseModfier
	parseModfier:function(Imodifier,defaultAccess)
	{
		var hasAccess=false
		Imodifier.name=Imodifier.name.replace(ooCss.regAllModifier,function ($1) 
		{
			Imodifier["is"+$1.charAt(0).toUpperCase()+$1.substr(1).toLowerCase()]=true
			var m=ooCss.utils.modifier[$1.toLowerCase()]
			if(m<=ooCss.utils.modifier['public'])
				hasAccess=true
			
			Imodifier.modifier = Imodifier.modifier | m
			return ''
		})
		
		if(!hasAccess)
		{
			Imodifier["is"+defaultAccess.charAt(0).toUpperCase()+defaultAccess.substr(1)]=true
			Imodifier.modifier = Imodifier.modifier|ooCss.utils.modifier[defaultAccess]
		}
		return Imodifier
		
		/*
		var name=Imodifier.name.replace(/\b(private|protected|public|abstract)\b/g,'')
		if(name!=Imodifier.name)
		{
		    if(Imodifier.name.search(/\bprivate\b/i) > -1)
		    {
			    Imodifier.isPrivate = true
			    Imodifier.modifier = Imodifier.modifier | ooCss.utils.modifier.private
		    }
		    else if(Imodifier.name.search(/\bprotected\b/i) > -1)
		    {
			    Imodifier.isProtected = true;
			    Imodifier.modifier = Imodifier.modifier | ooCss.utils.modifier.protected
		    }
		    else
		    {
			    Imodifier.isPublic = true
			    Imodifier.modifier = Imodifier.modifier | ooCss.utils.modifier.public
		    }
    		
		    if(Imodifier.name.search(/\babstract\b/i) > -1)
		    {
			    Imodifier.isAbstract = true
			    Imodifier.modifier = Imodifier.modifier | ooCss.utils.modifier.abstract
		    }
		    Imodifier.name=name
		}
		Imodifier.name=name
		*/
		

		/*
	    var name=Imodifier.name
	    for(var m in this.modifier)
	    {
	        var boolPName='is'+m.replace(/\w/,m.charAt(0).toUpperCase())
	        var reg=new RegExp('\\b'+m+'\\b')
	        if(name.search(reg)>-1)
	        {
	            Imodifier.modifier=Imodifier.modifier|this.modifier[m]
	            Imodifier[boolPName]=true
	            name=name.replace(reg,' ')
	        }
	        else
	        {
	            Imodifier[boolPName]=false
	        }
        }
        Imodifier.name=name
        return Imodifier*/
	},
	//#end
	
	expression:
	{
		//## separate
		separate:function (text) 
		{
		    var expressions=[]
            while(true)
			{
			    var msE=text.match(ooCss.regEval)
			    if(!msE)
			        break
			        
		        var e=msE[0];
		        var start=text.indexOf(e)
		        var rightStr=text.substr(start+e.length)
		        
		        var rightCount=0
		        var contentLength=0;
		        var lastIndex=-1
		        while(true)
		        {
		            lastIndex=rightStr.search(/[\'\"\(\)]/)
		            if(lastIndex<0)
		            {
		                contentLength=0
		                break;
		            }
		            contentLength+=lastIndex+1;
		            var c=rightStr.charAt(lastIndex)
		            rightStr=rightStr.substring(lastIndex+1)
		            if(c=='"'||c=="'")
	                {
	                    lastIndex=rightStr.indexOf(c)
	                    if(lastIndex<0)
	                    {
	                        contentLength=0
	                        break
	                    }
	                    contentLength+=lastIndex+1
	                    rightStr=rightStr.substring(lastIndex+1)
	                    continue
	                }
	                else if(c=='(')
	                {
	                    rightCount--
	                }
	                else if(c==')')
	                {
	                    rightCount++
	                }
	                
	                if(rightCount>0)
	                {
	                    break
	                }
		        }
		        
		        var expressionContent=text.substr(start+e.length,contentLength-1)
		        var newEval=""
		        if(expressionContent!="")
	            {
	                newEval='@('+expressions.length+')'
	                expressions.push(expressionContent)
	            }
		        else
		        {
		            ooCss.debug.output(ooCss.local.evalDeclareError.oc_format(e))
		        }
		        text=text.substr(0,start)+newEval+text.substring(start+e.length+contentLength)
		    }
		 
		    return {text:text,expressions:expressions}
		},
		//#end
		
		//## execute
		execute:function (expression) 
		{
			try
			{
				var selfVars = this instanceof ooCss.Sheet ? this.vars : (this instanceof ooCss.Type ? this.sheet.vars : {})
			    with(ooCss)
                with(ooCss.vars)
				with(selfVars)
				with(this)
				{
					//var value = eval('(function(me){ return ' + expression + '}).call(this,this)')
					//var value=eval(expression)
					var me=this
					var value =null;
					try
					{
						 eval("value=" + expression)
					}
					catch(e)
					{
						if(e.name=="SyntaxError")
						{
							value = eval('(function(){ ' + expression + '}).call(this)')
						}
						else throw e
					}
					
					return value;
				}
			}
			catch(e)
			{
				ooCss.debug.output(ooCss.local.evalError.oc_format(e.message,expression))
			}
		}
		//#end
	},
	
	//## override
	override : function(target)
	{
		var isSafety=this instanceof String || typeof(this)=='string'
		for(var i = 1; i < arguments.length; i ++ )
		{
			var item = arguments[i]
			if(item)
			{
				for(var key in item)
				{
					if(isSafety)
					{
						target[this+key]=item[key]
						if(target[key]==undefined)
							target[key]=item[key]
					}
					else if(this!=false || target[key] == undefined)
					{
						target[key] = item[key];
					}
				}
			}
		}
		return target;
	}
	//#end
}
ooCss.utils.safeOverride=function(prefix,target)
{
	return ooCss.utils.override.apply(prefix,Array.prototype.slice.call(arguments,1))
}

ooCss.utils.extend = function(target)
{
	return ooCss.utils.override.apply(false,arguments)
}
//#end

//## Function
ooCss.utils.safeOverride("oc_", Function.prototype, 
{
	bind : function(This)
	{
		var old = this;
		var args = arguments.length > 1 ? Array.prototype.slice.call(arguments, 1) : null;
		return function()
		{
			return old.apply(This || window, args || arguments)
		}
	},
	args : function()
	{
		var old = this;
		var sendArgs = Array.prototype.slice.call(arguments, 0)
		return function()
		{
			var args = sendArgs
			if(arguments.length > 0)
			{
				args = args.concat(arguments);
			}
			return old.apply(this, args)
		}
	},
	timeout : function(millisecond, This)
	{
		var old = this;
		var args = arguments.length > 1 ? Array.prototype.slice.call(arguments, 2) : null;
		window.setTimeout(function()
		{
			old.apply(This || this, args || arguments);
		}, millisecond)
	},
	defer : function(millisecond, This)
	{
		var old = this;
		var args = arguments.length > 2 ? Array.prototype.slice.call(arguments, 2) : null;
		return function()
		{
			This = This || this
			args = args || arguments
			window.setTimeout(function()
			{
				old.apply(This, args);
			}, millisecond)
		}
	},
	inherit:function(base,override)
	{
		this.base=base
		this.prototype=ooCss.utils.override(new base(),this.prototype,override)
		return this
	},
	New:function () 
	{
		var This=new this();
		if(this.base)
		{
			var base={}
			this.base.apply(base,arguments)
			ooCss.utils.extend(This,base)
		}
		return This;
	}
})

//#end

//## String
ooCss.utils.safeOverride("oc_", String.prototype, 
{
	trim : function()
	{
		return this.replace(/(^[\s\r\n]+)|([\s\r\n]+$)/g, "");
	},
	contains : function(subStr)
	{
		return this.indexOf(subStr) >- 1
	},	
	format : function()
	{
		var args=arguments
		return this.replace(/\{[\s\r\n]*(\d+)[\s\r\n]*\}/g,function($0,$1)
		{
			return args[parseInt($1)];
		})
	}
})

//#end

//## Array
ooCss.utils.safeOverride("oc_", Array.prototype, 
{
	map : function(f, where, _this)
	{
		var a = [];
		var whereIsFun = typeof(where) == 'function';
		var fIsFun = typeof(f) == 'function';
		for(var i = 0, len = this.length; i < len; i ++ )
		{
			var item = this[i]
			if(where != undefined)
			{
				if(whereIfFun)
				if( ! where.call(_this, item, i, this))
				continue
				if( ! eval(where))
				continue;
			}
			
			
			a.push(fIsFun ? f.call(_this, item, i, this) : item);
		}
		return a;
	},
	copy:function()
	{
		return this.concat([])
	},
	first : function()
	{
		return this[0];
	},
	last : function()
	{
		return this[this.length - 1];
	}
})
//#end

//## RegExp
ooCss.utils.safeOverride("oc_", RegExp.prototype, 
{
	test : function(str)
	{
		if(str)
		return str.search(this) > -1;
		else
		return false;
	}
})
//#end


//## ooCss.Member
ooCss.regMemberLastPart=/[^\s\r\n]+$/,//成员名最后部分
ooCss.Member=function (sheet,name,value) 
{
	this.value=value
	this.name=name
    ooCss.utils.parseModfier(this,'public')
	this.name = this.name.toLowerCase().oc_trim().match(ooCss.regMemberLastPart)[0];
	if(this.name == 'background' || this.name == 'background-image')
	{
		
		if(sheet.url)
		{
			this.value = this.value.replace(/\burl[\s\r\n]*\([\s\r\n]*[\'\"]?([\w\W]{1,10})/, function($0, $1)
			{
				if($1.charAt(0) == '/' || $1.substr(0,2)=='@(')
				{
					return $0
				}
				else
				{
					var index = $1.indexOf('://')
					if(index > 1 && index < 10)
					{
						return $0
					}
				}
				return $0.substr(0,$0.length-$1.length)+ sheet.urlPath+$1
			})
		}
	}
}
//## member.prototype
ooCss.regExpression=/@\(\d+\)/		//表达式
ooCss.Member.inherit(ooCss.utils.object, 
{
	type:null,
	name:null,
	value:null,
	getValue:function (type) 
	{
		if(type.sheet.hasExpression )
		{
			var value=this.value;
			while(true)
			{
				var ms=value.match(ooCss.regExpression)
				if(ms)
				{
					var eIndex=parseInt(ms[0].replace(/[^\d]/g,''));
					var expression=type.sheet.expressions[eIndex];
					value=value.replace(ms[0],ooCss.utils.expression.execute.call(type,expression))
				}
				else 
				{
					break
				}
			}
			return value
		}
		return this.value
	}
})
//#end


//## ooCss.Member.create
ooCss.regMemberName=/[\s\r\n\w\d\-]+:/g
ooCss.regMemberWipeEnd=/[;\s\r\n]+$/
ooCss.Member.create=function (sheet,text) 
{
	var items=text.split(';')
	var i=-1;
	var right=""
	var left=""
	var members=[]
	while(true)
	{
		i++
		var item=items[i]
		var _left=item?item.match(ooCss.regMemberName):null
		//有匹配的名称或到最后一项
		if(_left || item==null)
		{
			if( left && right!="")
			{
				right=right.replace(ooCss.regMemberWipeEnd,'')
				members.push(new ooCss.Member(sheet,left.substring(0,left.length-1),right))
			}
			
			if(_left)
			{
				left = _left[0]
				right = item.substring(left.length)
			}
			else
			{
				break
			}
		}
		else
		{
			right+=';'+item
		}
	}
	return members
}
//#end
		
//## ooCss.Member.getCssText
ooCss.Member.getCssText=function (type,members) 
{
	if(members instanceof ooCss.Member)
	{
		return members.name+":" +members.getValue(type,members)+";"
	}
	
	var cssText=""
	if(members instanceof Array)
	{
		for(var i = 0; i < members.length; i ++ )
		{
			var member = members[i]
			cssText += member.name + ":" + member.getValue(type,member) + ";"
		}
	}
	return cssText;
}
//#end

//#end

//## ooCss.Type
//## ooCss.Type
ooCss.Type=function (option) 
{
	ooCss.utils.override(this,option)
	ooCss.utils.parseModfier(this,'public')
	if (option.path)
	{
	    this.name=option.path+' '+this.name
	}
	this.parseName(this)
	this.init()
}

//#end
ooCss.Type.inherit(ooCss.utils.object,
{
	hasError : false,
	text : null,
	name : null,

	init : function()
	{
		if(this.sheet)
		this.sheet.rules.push(this)
		this.uniqueID = this.sheet.uniqueID + "." + this.sheet.rules.length
		ooCss.Type.reg(this)
	},
	
	
	createMap : function()
	{
		if(this.map == null)
		{
			this.map = {}
			for(var i = 0; i < this.members.length; i ++ )
			{
				var member = this.members[i]
				this.map[member.name] = member
			}
		}
	},
	
	
	//## get set
	get : function(memberName)
	{
		var member = this.findMember(memberName, true)
		if(member)
		{
			if(ooCss.utils.modifier['private'] & member.modifier)
			ooCss.error(ooCss.local.accessPrivate.oc_format(this.name + ' => ' + memberName))
			return member.getValue()
		}
	},
	getNumber:function(memberName)
	{
		var value=this.get(memberName)
		if(value instanceof String || typeof(value) == 'string')
		{
			value=value.replace(/[^\-.\d]/g,'')
		}
		return eval(value);
	},
	
	set : function(memberName, value)
	{
		var member = this.findMember(memberName, false)
		if(member == null || member.type != this)
		{
			this.members.push(new ooCss.Member(this, memberName.toLowerCase().oc_trim(), value))
		}
		else
		{
			if(ooCss.utils.modifier['private'] & member.modifier)
			ooCss.error(ooCss.local.accessPrivate.oc_format(this.name + ' => ' + memberName))
			member.value = value
		}
	},
	//#end
	
	//## render
	render : function()
	{
		this.rendered=true
		if(this.isAbstract || (this.bases == null && this.members.length == 0))
			return false;
		
		
		var text = "";
		if(this.bases)
		{
			var ms = []
			ooCss.Type.inherit.getBaseMembers(this,ms)
			text += ooCss.Member.getCssText(this, ms)
		}
		
		
		text += ooCss.Member.getCssText(this, this.members)
		if(this.cssText == text)
		{
			return false
		}
		this.cssText = text
		var cssSheet = this.sheet.cssSheet
		try
		{
			if(this.cssRule)
			{
				this.cssRule.style.cssText = this.cssText
			}
			else if(this.cssText)
			{
				ooCss.brower.addRule(cssSheet, this.name, this.cssText);
				this.cssRule = cssSheet.rules[cssSheet.rules.length - 1]
			}
		}
		catch(e)
		{
			ooCss.debug.output(ooCss.local.addRuleError.oc_format(this.name, this.cssText, e.message))
		}
		return true
	},	
	//#end
	
	//## findMember
	findMember : function(memberName, includeBase, modifier)
	{
		memberName = memberName.toLowerCase().oc_trim()
		this.createMap()
		var member = this.map[memberName]
		if(member)
		{
			if(modifier == null || modifier & member.modifier)
			return member
		}
		else if(includeBase && this.bases)
		{
			for(var i = this.bases.length - 1; i >- 1; i -- )
			{
				var bases = this.baseMap[this.bases[i]]
				for(var b = bases.length - 1; b >- 1; b -- )
				{
					member = bases[b].findMember(memberName, includeBase, ooCss.utils.modifier['protected'] | ooCss.utils.modifier['public'])
					if(member)
					return member
				}
			}
		}
	},
	//#end
	
	//## parseName
	parseName : function()
	{
		var i=0;		
		if(this.name.oc_contains('->'))
		{
			this.name=this.name.replace(/[^\'\"]+/g,function($1){return (++i %2)==0? $1:$1.replace(/[\s\r\n]+/g,' ')}).oc_trim()
			var parts = this.name.split(/[\s\r\n]*\->[\s\r\n]*/g)
			this.name = parts.shift().oc_trim()
			for(var i = 0; i < parts.length; i ++ )
			{
				var base=parts[i].oc_trim()
				if(base=="")
					continue
				if( ! this.bases)
				{
					this.bases = []
					this.baseMap = {}
				}
				this.bases.push(base)
				this.bases[base]=this.baseMap[base] = ooCss.Type.find(base,this.sheet.namespace,false)
				if(this.bases.length==1)
					this.base=this.baseMap[base]
			}
		}
		else
			this.name=this.name.replace(/[\s\r\n]+/g,' ').oc_trim()
	}
	//#end
})

//## ooCss.Type.checkEnd
ooCss.Type.checkEnd=function (text) 
{
	var msLeft=text.match(ooCss.regAllLeftLargeBracket)
	var msRight=text.match(ooCss.regAllRightLargeBracket)
	if (msRight==null || msLeft==null || msLeft.length>msRight.length) 
		return false
	else
		return true
}
//#end

//## ooCss.Type.create

ooCss.Type.create=function (sheet,path,names,cssText,hasErr) 
{
	var rules=[]
//	var hasMiddleBracket=names.indexOf('[')>-1
//	if(!hasMiddleBracket)
//	{
//		names = names.replace(ooCss.regMultiBlank, ' ').oc_trim()
//	}

    names = names.replace(/\([^\)]+\)/g, function($1)
    {
        return $1.replace(',', '```')
    })	
	
	names=names.split(',')
		
//	if(hasMiddleBracket)
//	{
//		for(var i=0;i<names.length;i++)
//		{
//			names[i]=names[i].replace(/^(1|[^1])*\[/,function ($1) 
//			{
//				return $1.replace(ooCss.regMultiBlank, ' ').oc_trim()
//			})
//		}
//	}
//	
	function _create(_path) 
	{
	    var members=ooCss.Member.create(sheet,cssText)
	    for(var i=0;i<names.length;i++)
	    {
		    var name=names[i].oc_trim();
		    if(name=='')
				continue
				
		    var pNames=null
		    
		    name = name.replace(/\)([^\(\)]+)\(/g, function($0, $1)
			{
				if($1.oc_trim()=='')
					return ') ('
				else
					return ')('+$1+')('
			})
			
		    name=name.replace(/\(([^\)]+)\)/g,function ($0,$1)
		    {
		        var subNames=$1.split('```');
		        if(pNames==null)
		        {
		            pNames=subNames
		        }
		        else
		        {
		            var _pNames=pNames
		            pNames=[]
		            for (var p=0;p<_pNames.length;p++)
		            {
    		            var pName=_pNames[p].oc_trim()
    		            if(pName=='')
    		                continue;
    		            for(var s=0;s<subNames.length;s++)
    		            {
    		                var sName=subNames[s].oc_trim() 
    		                if (sName== '')
    		                    continue;
    		                pNames.push(pName+' '+sName)
    		            }
		            }
		        }
		        return ''
		    })
	        
	        if(pNames==null)
	        {
		        rules.push(new ooCss.Type(
		        {
			        sheet:sheet,
			        text:cssText,
			        path:_path,
			        name:name,
			        members:(names.length==1?members:members.oc_copy())
		        }))
		    }
		    else
		    {
		        for(var p=0;p<pNames.length;p++)
		        {
		             rules.push(new ooCss.Type(
		            {
			            sheet:sheet,
			            text:cssText,
			            path:_path+' '+pNames[p],
			            name:name,
			            members:(names.length==1?members:members.oc_copy())
		            }))
		        }
		    }
	    }
	}
	
    if(path.charAt(0) != '(')
    {
        _create(path)
    }
    else
    {
	    var paths =  path.substr(1, path.length - 2).split(',') 
	    for(var i=0;i<paths.length;i++)
	    {
	        _create(paths[i])
	    }
	}
	return rules
}
//#end


//## ooCss.Type.parse
ooCss.regWipeRuleNameLeft=/[^ǐ]*[;\}\{]/
ooCss.regSingleRule=/[^;\}\{]+\{[^\{\}]*\}/g
ooCss.regHasInnerRule=/[^;\}\{]+\{[^ǐ]*\}/
ooCss.Type.parse=function(sheet,path,cssText)
{
	var reg=ooCss.regHasInnerRule
	var wipeLeft=ooCss.regWipeRuleNameLeft
	function create(ruleStr,hasError,isRe)
	{
		
		var index=ruleStr.indexOf('{')
		var names=ruleStr.substring(0,index).replace(wipeLeft,'')

		var content=ruleStr.substring(index+1,ruleStr.length-1)
		var msInner=content.match(reg)

		if(msInner)
		{
			while(true)
			{
				var newStr= content.replace(ooCss.regSingleRule,'')
				if(newStr.length==content.length)
					break
				else content=newStr
			}
		}
		
		var types=null;
		if(hasError && isRe!=true)
		{
			ooCss.debug.output(ooCss.local.ruleVerifyError.oc_format(names))
			if( ! isRe)
			{
				create(names+'{'+ content+'}}',true,true)
			}
		}
		else
		{
			types=ooCss.Type.create(sheet, path , names, content)
		}
		
		if(msInner)
		{
		    if(hasError|| types.length==1)
		    {
			    ooCss.Type.parse(sheet,path+' '+ (hasError?'':types[0].name), msInner[0])
			}
			else
			{
			    var _path=""
			    for(var i=0;i<types.length;i++)
			    {
			        _path+=','+path +' '+types[i].name
			    }
			    _path='('+_path.substr(1)+')'
			    ooCss.Type.parse(sheet,_path, msInner[0])
			}
		}
	}

	var parts=cssText.split('}')
	var len=parts.length
	//最后一项为空时是多余的
	if(len > 0 && parts[len - 1].replace(ooCss.regBlank, '') == "")
	{
		parts.pop()
		len=parts.length
	}
		
	var ruleStr=""
	for(var i=0;i<len;i++)
	{
		var part=parts[i]+"}";
	
		//当前项验证通过，则忽略上一项的错误
		if(ruleStr=="" && ooCss.Type.checkEnd(part))
		{
			if(ruleStr)
				create(ruleStr,true)
				
			create(part,false)
			ruleStr=""
		}
		else
		{
			ruleStr += part
			var isRight=false
			if(ruleStr != part)
				isRight = ooCss.Type.checkEnd(ruleStr);
			
			//累计验证通过、或者所有规则结束，则结束当前项
			if(isRight || i == len - 1)
			{					
				create(ruleStr,!isRight)	
				ruleStr = ""
			}
		}
	}
}
//#end

//## ooCss.Type.reg
ooCss.Type.map={}
ooCss.Type.reg=function (type) 
{
    type.sheet.NS.regType(type)
	var inName=ooCss.Type.map[type.name]
	if (inName==null) 
		ooCss.Type.map[type.name]=inName={}
	var inKey=inName[type.sheet.namespace]
	if(inKey == null)
	{
		inName[type.sheet.namespace] = type
	}
	else
	{
		if(inKey instanceof Array)
			inKey.push(type)
		else
			inName[type.sheet.namespace] =[inKey,type]
	}
	
	if( ! type.isSealed)
	{
		ooCss.Type.inherit.spread(type)
	}
	
	if(type.bases)
	{
		for(var i = 0; i < type.bases.length; i ++ )
		{
			ooCss.Type.inherit.reg(type.bases[i], type)
		}
	}
}
//#end
		
ooCss.Type.getVersion=function(name)
{
	return (ooCss.Type.map[name]||{}).version
}
		
//## ooCss.Type.find
ooCss.Type.find=function (ruleName,verifyKey,includeSealed) 
{
	var map=ooCss.Type.map[ruleName]
	var rules=[]
	if(map)
	{
		for(var namespace in map)
		{
			var rs=map[namespace]
			
			if(rs instanceof Array)
			{
				for(var r = 0; r < rs.length; r ++ )
				{
					var type=rs[r]
					if(includeSealed==false && type.isSealed) 
						continue;
					if((  type.isInternal || type.isPublic==false ) && verifyKey!=null && verifyKey!=rs.namespace)
						continue
					rules.push(type)
				}
			}
			else
			{
				if(includeSealed==false && rs.isSealed) 
					continue;
				if((  rs.isInternal || rs.isPublic==false ) && verifyKey!=null && verifyKey!=rs.namespace)
					continue
				rules.push(rs)
			}
		}
	}
	return rules
}
//#end
		
//## ooCss.Type.inherit
ooCss.Type.inherit = 
{
	map:{},
	reg:function(baseName, sub)
	{				
		var subs = this.map[baseName]
		if(subs == null)
			subs = this.map[baseName] = []
		subs.push(sub)
	},
	
	//## getBaseMembers
	getBaseMembers:function (type,members) 
	{
		members = members || []
		
		if(type.bases)
		{
			for(var b in type.baseMap)
			{
				var bases=type.baseMap[b]
				for(var r=0;r<bases.length;r++)
				{
					var base=bases[r]
					this.getBaseMembers(base,members)
					for(var m=0;m<base.members.length;m++)	
					{
						var member=base.members[m]
						if(!member.isPrivate)
							members.push(member)
					}
				}
			}
		}
		return members
	},
	//#end
	
	//## spread
	spread :function (type) 
	{
		var spreaded=","
		var map=this.map
		function _spread(_type,addToBaseMap) 
		{
			var subs=map[_type.name]
			spreaded+=_type.uniqueID+","
			if(subs)
			{
				for(var i = 0; i < subs.length; i ++ )
				{
					var sub = subs[i];
					if(sub.sheet.namespace!=_type.sheet.namespace && _type.isInternal)
						continue
					
					if(spreaded.indexOf(',' + sub.uniqueID + ',') >- 1)
					{
						ooCss.debug.output(ooCss.local.loopInherit.oc_format(sub.name + ',' + type.name))
						continue;
					}
					
					if(addToBaseMap)
						sub.baseMap[_type.name].push(_type)
						
					if(sub.rendered)
						sub.render()
					
					_spread(sub,false)
					
				}
			}	
		}
		_spread(type,true)
	}
	//#end
},
//#end
//#end

//## ooCss.Namespace
ooCss.Namespace=function (name) 
{
    ooCss.Namespace.map[name]=this
    this.typeMap=this
    this.firstTypes={}
    this.lastTypes={}
    this.regType=function(type)
    {
        var tName=type.name
        var tMap= this.typeMap[tName]
        if (tMap)
        {
            tMap.push(type)
        }
        else
        {
            var tMap= this.typeMap[tName]=[type]
        }
        
        if(this.firstTypes[tName]==null)
            this.firstTypes[tName]=type
            
        this.lastTypes[tName]=type
    }
}
ooCss.Namespace.reg=function(name)
{
    return ooCss.Namespace.map[name]||new ooCss.Namespace(name)
}
ooCss.Namespace.map=ooCss.Namespace
//#end

//## ooCss.Sheet
ooCss.Sheet = function(config)
{
	var me = this
	this.content=""
	this.rules=[]
	this.cssSheet=null;
	this.uniqueID=ooCss.Sheet.all.length
	this.expressions=[]
	this.vars={}
	ooCss.Sheet.all.push(this)
	this.hasExpression=false
	
	//## this.renderSheet
	this.render=function () 
	{
		this.cssSheet=ooCss.brower.createStyleSheet();
		for(var i = 0; i < this.rules.length; i ++ )
		{
			var r = this.rules[i];
			r.render();
		}
	}
	//#end
	
	//## this.init
	this.init = function()
	{
		var _init=function () 
		{
			var d=new Date()
			me.cssText=ooCss.Sheet.wipeComment(me.content)	
			me.extractVar(me.cssText)
			var result= ooCss.utils.expression.separate(me.cssText)

			me.expressions=result.expressions
			me.hasExpression=me.expressions.length>0

            me.extractNamespace()		
			ooCss.Type.parse(me,'',result.text)
			
			me.render()
			
			ooCss.debug.output(ooCss.local.useTime.oc_format(me.namespace,new Date()-d))
			
			//alert((new Date()-d)+' '+me.rules.length)
		}
		
		if(me.url)
		{
			if(me.url.charAt(me.url.length - 1)=='/')
			{
				me.urlPath = me.url
			}
			else
			{
				if(/file\:\/\/\//i.oc_test(me.url))
				{
					me.urlPath = me.url.replace(/[\/\\][^\/\\]*$/, function($1)
					{
						return $1.charAt(0)
					})
				}
				else
				{
					var index = me.url.indexOf('://')
					if(index > 1 && index < 10)
					{
						if(me.url.match(/\//g).length>2)
							me.urlPath=me.url.replace(/[^\/]*$/,'')
						else
							me.urlPath=me.url+'/'
					}
					else
					{
						if(me.url.oc_contains('/'))
						{
							me.urlPath = me.url.replace(/[^\/]*$/, '')
						}
						else
						{
							me.urlPath=''
						}
					}
				}
			}
			try
			{
				ooCss.simpleAjax(me.url, function(result)
				{
					me.content = result + '\n' + (me.content || '')
					_init()
				}, function(xml, status, statusText)
				{
					if(me.content)
					_init()
					ooCss.debug.output(ooCss.local.fileLoadFailed.oc_format(me.url, status + ' : ' + statusText))
				}, "get")
			}
			catch(e)
			{
			
			}
		}
		else if(me.content)
		{
			me.urlPath=''
			_init()
		}
	}
	//#end
	
	ooCss.utils.override(this,config)
	
	this.defer=this.defer==null?ooCss.defer:this.defer
	if(this.defer>0)
		this.init.oc_timeout(this.defer, this)
	else
		this.init()
}

ooCss.Sheet.all=[]

//## extractNamespace
ooCss.Sheet.prototype.extractNamespace = function()
{
	var namespace = null
	this.cssText = this.cssText.replace(/@namespace[\s\r\n]+(\w+)[\s\r\n]*;/, function($1, $2)
	{
		namespace = $2
		return ''
	}.bind(this))
	if(this.namespace == null)
	{
		this.namespace = namespace
		if(this.namespace == null)
		{
			if(this.url)
			this.namespace = this.url.replace(/([\w\W]*\/)|([\?#][[\w\W]*)/g, '').replace(/\.[^\.]*/, '').toLowerCase()
			else
			this.namespace = ooCss.Sheet.all.length
		}
	}
	this.NS = ooCss.Namespace.reg(this.namespace)
	return this.cssText
}
//#end
	
//## extractVar
ooCss.Sheet.prototype.extractVar = function()
{
	var regVar = /[\w\s\r\n]*\bvar[\s\r\n]+\w+[\s\r\n]*=/;
	while(true)
	{
		var msVar = this.cssText.match(regVar)
		if( ! msVar)
		break
		var e = msVar[0];
		var start = this.cssText.indexOf(e)
		var rightStr = this.cssText.substr(start + e.length)
		var rightCount = 0
		var contentLength = 0;
		var lastIndex =- 1
		while(true)
		{
			lastIndex = rightStr.search(/[\'\"\{\};]/)
			if(lastIndex < 0)
			{
				contentLength = 0
				break;
			}
			contentLength += lastIndex + 1;
			var c = rightStr.charAt(lastIndex)
			rightStr = rightStr.substring(lastIndex + 1)
			if(c == '"' || c == "'")
			{
				lastIndex = rightStr.indexOf(c)
				if(lastIndex < 0)
				{
					contentLength = 0
					break
				}
				contentLength += lastIndex + 1
				rightStr = rightStr.substring(lastIndex + 1)
				continue
			}
			else if(c == '{')
			{
				rightCount -- 
			}
			else if(c == '}')
			{
				rightCount ++ 
			}
			
			
			if(rightCount == 0 && c == ';')
			{
				break
			}
		}
		
		
		var varContent = this.cssText.substr(start + e.length, contentLength - 1)
		if(varContent != "")
		{
			var name =e.oc_trim().replace(/[\s\r\n]*\=[\w\W]*/,'').replace(/[\w\W]*[\s\r\n]+/g, '')
			this.vars[name]= ooCss.utils.expression.execute(varContent)
			if(/\bpublic\b/.test(e) )
			{
				var varInfo=ooCss.varsInfos[name]
				if(varInfo)
				{
					ooCss.debug.output(ooCss.local.varExist.oc_format(name,varInfo.sheet.namespace,varInfo.sheet.url))
				}
				ooCss.vars[name] = this.vars[name]
				ooCss.varsInfos[name]={sheet:this};
			}
		}
		else
			ooCss.debug.output(ooCss.local.varDeclareError.oc_format(e))
		this.cssText = this.cssText.substr(0, start) + this.cssText.substring(start + e.length + contentLength)
	}
}

//#end

//## ooCss.Sheet.wipeComment
ooCss.Sheet.wipeComment=function(content)
{
	while(true)
	{
		var start=content.indexOf("/*");
		if(start>-1)
		{
			var end=content.indexOf("*/");
			if(end >start)
			{
				content = content.substring(0,start)+" "+content.substring(end+2);
			}
		}
		else
		{
			break
		}
	}
	return content
}
//#end
//#end

//## ooCss.simpleAjax
ooCss.simpleAjax=function (url, onSuccess,onFailed, method, isXml, formData)
{
	var xml=null;
	var ajax = 
	{
		stateChange : function()
		{
			if(xml.readyState == 4)
			{
			    if(xml.status == 200 || xml.status==0 )
			    {
			        if(onFailed)
			        {
				        if(isXml)
				        {
					        onSuccess( xml.responseXML,xml);
				        }
				        else
				        {
					        onSuccess(xml.responseText,xml);
				        }
				    }
			    }
			    else 
			    {
			        if(onFailed)
			            onFailed(xml,xml.status,xml.statusText)
			    }
			}
		},
		
		request : function(isRe)
		{
			if(isRe!=true && window.XMLHttpRequest)
			{
				xml = new XMLHttpRequest()
			}
			else if(window.ActiveXObject)
			{
				try
				{
					xml = new ActiveXObject("Msxml2.XMLHTTP");
				}
				catch(e)
				{
					try
					{
						xml = new ActiveXObject("Microsoft.XMLHTTP");
					}
					catch(e)
					{}
				}
			}
			
			
			if(xml.overrideMimeType)
			{
				xml.overrideMimeType('text/xml');
			}
			if( ! xml)
			{
				throw new Error("不能建立Ajax连接")
				return false;
			}			
			
			xml.onreadystatechange =this.stateChange;
			try
			{
			    xml.open(method, url, true);
			}
			catch(e)
			{
			    if(isRe)
			        throw e
			    return ajax.request(true)
			}
			
			var type = "text/html";
			if(formData)
			{
				xml.setRequestHeader("Content-Length", formData.length);
				type = "application/x-www-form-urlencoded";
			}
			else if(isXml)
			{
				type = "text/xml"
			}
			xml.setRequestHeader("Content-Type", type);
			xml.send(formData);
		}
	}
		
	ajax.request();
}
//#end

ooCss.init();
ooCss.utils.initByModifier();


/*=========================== Local zh-cn ================================*/
ooCss.local=
{
	output:'输出',
	help:'在线帮助:www.oo-css.cn',
	varExist:'警告，变量“{0}”在命名空间“{1}” {2} 中已经定义！<br />如果不是有意覆盖，请更换变量名。<br />如果不是定义全局变量，去掉前面的“public”关键字。',
	dblClickClose:'双击 关闭/打开',
	accessPrivate:'成员"{0}"受保护',
	loopInherit:'{0} 循环继承',
	useTime:'{0} -> {1}',
	addRuleError:'添加样式出错：<br />{0}<br />{1}<br />{2}<br />',
	ruleVerifyError:'样式 {0} 验证错误，可能缺少必要的结尾',
	fileLoadFailed:'样式文件 {0} 加载失败<br />{1}<br />',
	evalError:'表达式计算错误：<br />{0}</br>{1}',
	varDeclareError:'变量 {0} 定义错误,可能缺少结束符“;”',
	evalDeclareError:'表达是输入有悟 {0} ,可能缺少结束符“)”'
}