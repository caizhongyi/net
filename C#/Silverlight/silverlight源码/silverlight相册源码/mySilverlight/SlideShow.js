// Slide.Show, version 1.0
// Copyright © Vertigo Software, Inc.
// This source is subject to the Microsoft Public License (Ms-PL).
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/publiclicense.mspx.
// All other rights reserved.

/// <reference path="Silverlight.js" />

/*******************************************
 * namespace: SlideShow
 *******************************************/
if (!window.SlideShow)
	window.SlideShow = {};

/*******************************************
 * function: SlideShow.createDelegate
 *******************************************/
SlideShow.createDelegate = function(instance, method)
{
	/// <summary>Creates a delegate function that executes the specified method in the correct context.</summary>
	/// <param name="instance">The instance whose method should be executed.</param>
	/// <param name="method">The method to execute.</param>
	/// <returns>The delegate function.</returns>
	
	return function()
	{
		return method.apply(instance, arguments);
	};
};

/*******************************************
 * function: SlideShow.merge
 *******************************************/
SlideShow.merge = function(destination, source, preventNew)
{
	/// <summary>Merges properties from a source object into a destination object.</summary>
	/// <param name="destination">The destination object.</param>
	/// <param name="source">The source object.</param>
	/// <param name="preventNew">Indicates whether or not new properties are prevented from being added to the destination object.</param>
	
	var root = destination;
	
	for (var i in source)
	{
		var s = source[i], d;
		var properties = i.split(".");
		var count = properties.length;
		
		for (var j = 0; j < count; j++)
		{
			i = properties[j];
			d = destination[i];
			
			if (d)
			{
				if (typeof(d) == "object")
					destination = d;
			}
			else if (j > 0 && j < count - 1)
			{
				var obj = {};
				obj[properties.slice(j, count).join(".")] = s;
				s = obj;
				break;
			}
		}
		
		if (d && typeof(d) == "object" && typeof(s) == "object")
			this.merge(d, s, false);
		else if (preventNew && count <= 1 && typeof(d) == "undefined")
			throw new Error("Undefined property: " + i);
		else
			destination[i] = s;
		
		destination = root;
	}
};

/*******************************************
 * function: SlideShow.extend
 *******************************************/
SlideShow.extend = function(baseClass, derivedClass, derivedMembers)
{
	/// <summary>Provides support for class inheritance.</summary>
	/// <param name="baseClass">The base class.</param>
	/// <param name="derivedClass">The derived class.</param>
	/// <param name="derivedMembers">The members to add to the derived class and override in the base class.</param>
	
	var F = function() {};
	F.prototype = baseClass.prototype;
	derivedClass.prototype = new F();
	derivedClass.prototype.constructor = derivedClass;
	derivedClass.base = baseClass.prototype;
	
	if (baseClass.prototype.constructor == Object.prototype.constructor)
		baseClass.prototype.constructor = baseClass;
	
	// Note: IE will not enumerate derived members that exist in the Object
	// prototype (e.g. toString, valueOf). If overriding these members
	// is necessary, search for "_IEEnumFix" for one possible solution.
	if (derivedMembers)
		for (var i in derivedMembers)
			derivedClass.prototype[i] = derivedMembers[i];
};

/*******************************************
 * function: SlideShow.parseBoolean
 *******************************************/
SlideShow.parseBoolean = function(value)
{
	/// <summary>Parses a boolean from the specified value.</summary>
	/// <param name="value">The value to parse.</param>
	/// <returns>True if the specified value is parsed as true.</returns>
	
	return (typeof(value) == "string") ? (value.toLowerCase() == "true") : Boolean(value);
};

/*******************************************
 * function: SlideShow.formatString
 *******************************************/
SlideShow.formatString = function(value)
{
	/// <summary>Formats a string.</summary>
	/// <param name="value">The string to format.</param>
	/// <returns>The formatted string.</returns>
	
	for(i = 1, j = arguments.length; i < j; i++)
		value = value.replace("{" + (i - 1) + "}", arguments[i]);
	
	return value;
};

/*******************************************
 * function: SlideShow.getUniqueId
 *******************************************/
SlideShow.getUniqueId = function(prefix)
{
	/// <summary>Gets a random unique identifier.</summary>
	/// <param name="prefix">The prefix to prepend to the generated identifier.</param>
	/// <returns>The unique identifier.</returns>
	
	return prefix + Math.random().toString().substring(2);
};

/*******************************************
 * function: SlideShow.addTextToBlock
 *******************************************/
SlideShow.addTextToBlock = function(textBlock, text)
{
	/// <summary>Sets the text in the specified block, truncating it to fit if necessary.</summary>
	/// <param name="textBlock">The element to modify.</param>
	/// <param name="text">The text to set.</param>
	
	if (text)
	{
		textBlock.text = text;
		
		var width  = textBlock.width;
		var height = textBlock.height;
		
		try
		{
			if (textBlock.actualWidth <= width && textBlock.actualHeight <= height)
				return;
			
			var min = 0;
			var max = text.length;
			var nonWordChars = /\W*$/;
			var ellipsis = "\u2026";
			
			// binary search
			while (true)
			{
				var mid = Math.floor((max + min) / 2);
				textBlock.text = text.substring(0, mid).replace(nonWordChars, ellipsis);
				
				if (mid == min)
					break;
				
				if (textBlock.actualWidth > width || textBlock.actualHeight > height)
					max = mid;
				else
					min = mid;
			}
			
			// clear if text still doesn't fit
			if (textBlock.actualWidth > width || textBlock.actualHeight > height)
				textBlock.text = null;
		}
		catch (error)
		{
			// ignore if textBlock.actualWidth can't be evaluated
		}
	}
	else
	{
		textBlock.text = null;
	}
};

/*******************************************
 * class: SlideShow.Object
 *******************************************/
SlideShow.Object = function()
{
	/// <summary>Provides a common base class with support for options and events.</summary>
	
	this.options = {};
	this.eventHandlers = {};
};

SlideShow.Object.prototype = 
{
	setOptions: function(options)
	{
		/// <summary>Merges the specified options with existing options.</summary>
		/// <param name="options">The options to merge.</param>
		
		SlideShow.merge(this.options, options, true);
	},
	
	addEventListener: function(name, handler)
	{
		/// <summary>Adds an event handler to the list of handlers to be called when the specified event is fired.</summary>
		/// <param name="name">The event name.</param>
		/// <param name="handler">The event handler to add.</param>
		/// <returns>The identifying token (i.e. index) for the added event handler.</return>
		
		var handlers = this.eventHandlers[name];
		
		if (!handlers)
			this.eventHandlers[name] = handlers = [];
		
		// regarding token, see http://msdn2.microsoft.com/en-us/library/bb232863.aspx
		var token = handlers.length;
		handlers[token] = handler;
		return token;
	},
	
	removeEventListener: function(name, handlerOrToken)
	{
		/// <summary>Removes the first matching event handler from the list of handlers to be called when the specified event is fired.</summary>
		/// <param name="name">The event name.</param>
		/// <param name="handlerOrToken">The event handler or indentifying token to remove.</param>
		
		if (typeof(handlerOrToken) == "function")
		{
			var handlers = this.eventHandlers[name];
			
			if (handlers)
			{
				for (var i = 0, j = handlers.length; i < j; i++)
					if (handlers[i] == handlerOrToken)
						break;
				
				handlers.splice(i, 1);
			}
		}
		else
		{
			handlers.splice(handlerOrToken, 1);
		}
	},
	
	fireEvent: function(name, e)
	{
		/// <summary>Fires the specified event and calls each listening handler.</summary>
		/// <param name="name">The name of the event to fire.</param>
		/// <param name="e">The event arguments to pass to each handler.</param>
		
		var handlers = this.eventHandlers[name];
		
		if (handlers)
			for (var i = 0, j = handlers.length; i < j; i++)
				handlers[i](this, e);			
	},
	
	dispose: function()
	{
		/// <summary>Releases the object from memory.</summary>
		
		this.options = null;
		this.eventHandlers = null;
	}
};

/*******************************************
 * class: SlideShow.JsonParser
 *******************************************/
SlideShow.JsonParser = function(options)
{
	/// <summary>Provides support for JSON parsing.</summary>
	/// <param name="options">The options for the parser.</param>

	SlideShow.JsonParser.base.constructor.call(this);
	
	SlideShow.merge(this.options,
	{
		arrays: null
	});
	
	this.setOptions(options);
	
	this.initializeForcedArrays();
};

SlideShow.extend(SlideShow.Object, SlideShow.JsonParser,
{
	initializeForcedArrays: function()
	{
		/// <summary>Converts the "arrays" option into a hash for lookups.</summary>
		
		this.forcedArrays = {};
		
		if (this.options.arrays)
		{
			var items = this.options.arrays.split(",");
			
			for (var i = 0, j = items.length; i < j; i++)
				this.forcedArrays[items[i]] = true;
		}
	},
	
	fromFeed: function(url, callback)
	{
		/// <summary>Adds an external script tag that references the specified JSON feed which calls the specified callback function.</summary>
		/// <param name="url">The location of the feed.</param>
		/// <param name="callback">The name of the callback function.</param>
		
		window[callback] = SlideShow.createDelegate(this, this.onFeedCallback);
		var scriptId = SlideShow.getUniqueId("SlideShow_Script_");
		SlideShow.ScriptManager.addExternalScript(scriptId, "text/javascript", url);
	},
	
	onFeedCallback: function(obj)
	{
		/// <summary>Handles the callback from a JSON feed and fires the "callback" event.</summary>
		/// <param name="obj">The returned JSON object.</param>
		
		this.fireEvent("callback", obj);
	},	
	
	fromXml: function(url, async)
	{
		/// <summary>Parses a JSON object from the specified XML file and fires the "parseComplete" event.</summary>
		/// <param name="url">The location of the file to parse.</param>
		/// <param name="async">Specifies whether or not to parse the XML asynchronously.</param>
		
		var request;
		
		if (window.XMLHttpRequest)
			request = new window.XMLHttpRequest();
		else if (window.ActiveXObject)
			request = new window.ActiveXObject("Microsoft.XMLHTTP");
		else
			throw new Error("XML parsing failed: Unsupported browser");
		
		var handleReadyStateChange = function()
		{
			if (request.readyState == 4) 
			{
				if (request.status == 200)
				{
					var document = request.responseXML;
					var obj = this.parseXmlDocument(document);
					this.fireEvent("parseComplete", obj);
				}
				else
				{
					throw new Error("XML parsing failed: " + request.statusText);
				}
			}
		};
		
		if (async)
		{
			request.onreadystatechange = SlideShow.createDelegate(this, handleReadyStateChange);
			request.open("GET", url, true);
			request.send(null);
		}
		else
		{
			request.open("GET", url, false);
			request.send(null);
			handleReadyStateChange.apply(this);
		}
	},
	
	parseXmlDocument: function(document)
	{
		/// <summary>Parses a JSON object from the specified XML document.</summary>
		/// <param name="document">The document to parse.</param>
		/// <returns>The parsed object.</returns>
		
		var element = document.documentElement;
		
		if (!element)
			return;
		
		var elementName = element.nodeName;
		var elementType = element.nodeType;
		var elementValue = this.parseXmlNode(element);
		
		if (this.forcedArrays[elementName])
			elementValue = [ elementValue ];
		
		// document fragment
		if (elementType == 11)
			return elementValue;
		
		var obj = {};
		obj[elementName] = elementValue;
		return obj;
	},
	
	parseXmlNode: function(node)
	{
		/// <summary>Recursively parses a JSON object from the specified XML node.</summary>
		/// <param name="element">The node to parse.</param>
		/// <returns>The parsed object.</returns>
		
		switch (node.nodeType)
		{
			// comment
			case 8:
				return;
			
			// text and cdata
			case 3:
			case 4:
			
				var nodeValue = node.nodeValue;
				
				if (!nodeValue.match(/\S/))
					return;
				
				return this.formatValue(nodeValue);
			
			default:
				
				var obj;
				var counter = {};
				var attributes = node.attributes;
				var childNodes = node.childNodes;
				
				if (attributes && attributes.length)
				{
					obj = {};
					
					for (var i = 0, j = attributes.length; i < j; i++)
					{
						var attribute = attributes[i];
						var attributeName = attribute.nodeName.toLowerCase(); // lowered in order to be consistent with Safari
						
						if (typeof(attributeName) != "string")
							continue;
						
						var attributeValue = attribute.nodeValue;
						
						if (!attributeValue)
							continue;
						
						if (typeof(counter[attributeName]) == "undefined")
							counter[attributeName] = 0;
						
						this.addProperty(obj, attributeName, this.formatValue(attributeValue), ++counter[attributeName]);
					}
				}
				
				if (childNodes && childNodes.length)
				{
					var textOnly = true;
					
					if (obj)
						textOnly = false;
					
					for (var k = 0, l = childNodes.length; k < l && textOnly; k++)
					{
						var childNodeType = childNodes[k].nodeType;
						
						// text or cdata
						if (childNodeType == 3 || childNodeType == 4)
							continue;
						
						textOnly = false;
					}
					
					if (textOnly)
					{
						if (!obj)
							obj = "";
					
						for (var m = 0, n = childNodes.length; m < n; m++)
							obj += this.formatValue(childNodes[m].nodeValue);
					}
					else
					{
						if (!obj)
							obj = {};
						
						for (var o = 0, p = childNodes.length; o < p; o++)
						{
							var childNode = childNodes[o];
							var childName = childNode.nodeName;
							
							if (typeof(childName) != "string")
								continue;
							
							var childValue = this.parseXmlNode(childNode);
							
							if (!childValue)
								continue;
							
							if (typeof(counter[childName]) == "undefined")
								counter[childName] = 0;
							
							this.addProperty(obj, childName, this.formatValue(childValue), ++counter[childName]);
						}
					}
				}
				
				return obj;
		}
	},
	
	formatValue: function(value)
	{
		/// <summary>Formats the specified value to its most suitable type.</summary>
		/// <param name="value">The value to format.</param>
		/// <returns>The formatted value or the original value if no more suitable type exists.</returns>
		
		if (typeof(value) == "string")
		{
			var loweredValue = value.toLowerCase();
			
			if (loweredValue == "true")
				return true;
			else if (loweredValue == "false")
				return false;
			
			if (!isNaN(value))
				return new Number(value).valueOf(); // fixes number issue with option values
		}
		
		return value;
	},
	
	addProperty: function(obj, name, value, count)
	{
		/// <summary>Adds a property to the specified object.</summary>
		/// <param name="obj">The target object.</param>
		/// <param name="name">The name of the property.</param>
		/// <param name="value">The value of the property.</param>
		/// <param name="count">A count that indicates whether or not the property should be an array.</param>
		
		if (this.forcedArrays[name])
		{
			if (count == 1)
				obj[name] = [];
			
			obj[name][obj[name].length] = value;
		}
		else
		{
			switch (count)
			{
				case 1:
					obj[name] = value;
					break;
				
				case 2:
					obj[name] = [ obj[name], value ];
					break;
				
				default:
					obj[name][obj[name].length] = value;
					break;
			}
		}
	}	
});

/*******************************************
 * class: SlideShow.XmlConfigProvider
 *******************************************/
SlideShow.XmlConfigProvider = function(options)
{
	/// <summary>Provides configuration data to the Slide.Show control from an XML file.</summary>

	SlideShow.XmlConfigProvider.base.constructor.call(this);
	
	SlideShow.merge(this.options,
	{
		url: "Configuration.xml"
	});
	
	this.setOptions(options);
};

SlideShow.extend(SlideShow.Object, SlideShow.XmlConfigProvider,
{
	getConfig: function(configHandler)
	{
		/// <summary>Retrieves the configuration data synchronously and calls the specified event handler (with the data).</summary>
		/// <param name="configHandler">The event handler to be called after the configuration data is retrieved.</param>
		
		var parser = new SlideShow.JsonParser({ arrays: "module,option,script,transition" });
		parser.addEventListener("parseComplete", configHandler);
		parser.fromXml(this.options.url, false);
	}
});

/*******************************************
 * class: SlideShow.ScriptManager
 *******************************************/
SlideShow.ScriptManager = function()
{
	/// <summary>Provides support for loading scripts dynamically.</summary>
	
	SlideShow.ScriptManager.base.constructor.call(this);
	this.scripts = {};
	this.timeoutId = null;
};

SlideShow.extend(SlideShow.Object, SlideShow.ScriptManager,
{
	register: function(key, url)
	{
		/// <summary>Registers a script to be loaded.</summary>
		/// <param name="key">The unique key that identifies the script.</param>
		/// <param name="url">The location of the script.</param>
		
		if (this.scripts[key])
			throw new Error("Duplicate script: " + key);
		
		this.scripts[key] = { url: url, loaded: false };
	},
	
	load: function()
	{
		/// <summary>Loads the registered scripts.</summary>
		
		for (var key in this.scripts)
		{
			var id = "SlideShow_Script_" + key;
			SlideShow.ScriptManager.addExternalScript(id, "text/javascript", this.scripts[key].url);
		}
		
		// timeout after 15 seconds
		this.timeoutId = window.setTimeout(SlideShow.createDelegate(this, this.onLoadTimeout), 15000);
		this.checkLoadStatus();
	},
	
	checkLoadStatus: function()
	{
		/// <summary>Fires the "loadComplete" event when all registered scripts are loaded.</summary>
		
		for (var key in this.scripts)
		{
			var script = this.scripts[key];
			
			if (!script.loaded)
			{
				if (typeof(eval("SlideShow." + key)) == "undefined")
				{
					window.setTimeout(SlideShow.createDelegate(this, this.checkLoadStatus), 100);
					return;
				}
				else
				{
					script.loaded = true;
				}
			}
		}
		
		if (this.timeoutId)
		{
			window.clearTimeout(this.timeoutId);
			this.timeoutId = null;
		}
		
		this.fireEvent("loadComplete");
	},
	
	onLoadTimeout: function()
	{
		/// <summary>Handles the event fired when the registered scripts fail to load within the allowed time.</summary>
		
		this.timeoutId = null;
		throw new Error("Scripts failed to load in time");
	}
});

SlideShow.ScriptManager.addExternalScript = function(id, type, url)
{
	/// <summary>Adds an external script tag to the document head.</summary>
	/// <param name="id">The ID that uniquely identifies the script element.</param>
	/// <param name="type">The script type.</param>
	/// <param name="url">The script location.</param>
	
	if (!document.getElementById(id))
	{
		var element = document.createElement("script");
		element.id = id;
		element.type = "text/javascript";
		element.src = url;
		document.getElementsByTagName("head")[0].appendChild(element);
	}
};

SlideShow.ScriptManager.addInlineScript = function(id, type, text)
{
	/// <summary>Adds an inline script tag to the document head.</summary>
	/// <param name="id">The ID that uniquely identifies the script element.</param>
	/// <param name="type">The script type.</param>
	/// <param name="text">The script text.</param>
	
	if (!document.getElementById(id))
	{
		var element = document.createElement("script");
		element.id = id;
		element.type = type;
		element.text = text;
		
		try
		{
			// Safari workaround
			element.innerText = text;
		}
		catch (error)
		{
		}
		
		document.getElementsByTagName("head")[0].appendChild(element);
	}
};

/*******************************************
 * class: SlideShow.UserControl
 *******************************************/
SlideShow.UserControl = function(control, parent, xaml, options)
{
	/// <summary>Provides a base class for user controls.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="xaml">The XAML for the control.</param>
	/// <param name="options">The options for the control.</param>
	
	SlideShow.UserControl.base.constructor.call(this);
	
	SlideShow.merge(this.options,
	{
		top: "Auto",
		left: "Auto",
		bottom: "Auto",
		right: "Auto",
		width: "Auto",
		height: "Auto",
		background: "Transparent",
		opacity: 1,
		visibility: "Visible",
		zIndex: 0,
		cursor: "Default"
	});
	
	this.setOptions(options);
	
	this.control = control;
	this.children = [];
	
	if (parent)
	{
		this.parent = parent;
		this.parent.children.push(this);
	}
	
	if (xaml)
		this.root = control.host.content.createFromXaml(xaml, true);
	
	if (this.parent && this.parent.root && this.root)
		this.parent.root.children.add(this.root);
};

SlideShow.extend(SlideShow.Object, SlideShow.UserControl,
{
	render: function()
	{
		/// <summary>Renders the control using the current options.</summary>
		
		this.resize(this.options.width, this.options.height);
		this.reposition();
		
		this.root.background = this.options.background;
		this.root.opacity = this.options.opacity;
		this.root.visibility = this.options.visibility;
		this.root["Canvas.ZIndex"] = this.options.zIndex;
		this.root.cursor = this.options.cursor;
		
		for (var i = 0, j = this.children.length; i < j; i++)
			this.children[i].render();
	},
	
	resize: function(width, height)
	{
		/// <summary>Resizes the control.</summary>
		/// <param name="width">The width.</param>
		/// <param name="height">The height.</param>
		
		var auto = "Auto";
		var currentWidth = this.root.width;
		var currentHeight = this.root.height;
		
		this.root.width = (width != auto) ? Math.max(width, 0) : 0;
		this.root.height = (height != auto) ? Math.max(height, 0) : 0;
		
		if (currentWidth != this.root.width || currentHeight != this.root.height)
			this.onSizeChanged();
	},
	
	reposition: function()
	{
		/// <summary>Positions the control relative to the parent control.</summary>
		
		var auto = "Auto";
		var currentWidth = this.root.width;
		var currentHeight = this.root.height;
		
		this.root["Canvas.Top"] = (this.options.top != auto) ? this.getPosition("top", this.options.top) : 0;
		this.root["Canvas.Left"] = (this.options.left != auto) ? this.getPosition("left", this.options.left) : 0;
		
		if (this.options.bottom != auto)
		{
			if (this.options.height != auto && this.options.top == auto)
				this.root["Canvas.Top"] = this.parent.root.height - this.root.height - this.getPosition("bottom", this.options.bottom);
			else if (this.options.height == auto && this.options.top != auto)
				this.root.height = Math.max(this.parent.root.height - this.root["Canvas.Top"] - this.getPosition("bottom", this.options.bottom), 0);
		}
		
		if (this.options.right != auto)
		{
			if (this.options.width != auto && this.options.left == auto)
				this.root["Canvas.Left"] = this.parent.root.width - this.root.width - this.getPosition("right", this.options.right);
			else if (this.options.width == auto && this.options.left != auto)
				this.root.width = Math.max(this.parent.root.width - this.root["Canvas.Left"] - this.getPosition("right", this.options.right), 0);
		}
		
		if (currentWidth != this.root.width || currentHeight != this.root.height)
			this.onSizeChanged();
	},
	
	getPosition: function(name, value)
	{
		/// <summary>Gets the normalized position.</summary>
		/// <param name="name">The position direction (e.g. top, left, bottom, right).</param>
		/// <param name="value">The position value (e.g. -106, 50%).</param>
		/// <returns>The normalized position (in pixels).</returns>
		
		if (!isNaN(value))
			return value;
		
		// trim trailing "%"
		var percent = value.slice(0, value.length - 1) / 100;
		
		switch (name)
		{
			case "top": return this.parent.root.height * percent - this.root.height / 2;
			case "left": return this.parent.root.width * percent - this.root.width / 2;
			case "bottom": return (1 - this.parent.root.height * percent) - this.root.height / 2;
			case "right": return (1 - this.parent.root.width * percent) - this.root.width / 2;
			default: throw new Error("Invalid name: " + name);
		}
	},
	
	dispose: function()
	{
		/// <summary>Releases the control from memory.</summary>
		
		SlideShow.UserControl.base.dispose.call(this);
		
		if (this.parent)
		{
			for (var i = 0, j = this.parent.children.length; i < j; i++)
				if (this.parent.children[i] == this)
					break;
			
			this.parent.children.splice(i, 1);
			this.parent.root.children.remove(this.root);
		}
		
		this.control = null;
		this.parent = null;
		this.children = null;
		this.root = null;
	},
	
	onSizeChanged: function()
	{
		/// <summary>Handles the event fired when the control is resized.</summary>
		
		for (var i = 0, j = this.children.length; i < j; i++)
			this.children[i].reposition();
	}
});

/*******************************************
 * class: SlideShow.Control
 *******************************************/
SlideShow.Control = function(options)
{
	/// <summary>Initializes and renders a Slide.Show control.</summary>
	/// <param name="options">The options for the control.</param>
	
	SlideShow.Control.base.constructor.call(this);
	
	SlideShow.merge(this.options,
	{
		id: null,
		width: 640,
		height: 480,
		background: "Black",
		windowless: false,
		frameRate: 48,
		enableFrameRateCounter: false,
		enableRedrawRegions: false,
		enableTrace: true,
		installInPlace: true,
		installUnsupportedBrowsers: false,
		cssClass: "SlideShow",
		scripts: null,
		modules: null,
		transitions: null,
		dataProvider: null
	});
	
	if (options instanceof SlideShow.XmlConfigProvider)
	{
		var configProvider = options;
		configProvider.getConfig(SlideShow.createDelegate(this, this.onConfigLoad));
	}
	else
	{
		this.onConfigLoad(this, { configuration: options });
	}
};

SlideShow.extend(SlideShow.UserControl, SlideShow.Control,
{
	render: function()
	{
		/// <summary>Renders the control using the current options.</summary>
		
		SlideShow.Control.base.render.call(this);
		
		this.host.settings.enableFramerateCounter = this.options.enableFrameRateCounter;
		this.host.settings.enableRedrawRegions = this.options.enableRedrawRegions;
		
		if (this.options.enableTrace)
		{
			this.traceLog = this.host.content.createFromXaml('<TextBlock Canvas.Top="10" Canvas.Left="10" Canvas.ZIndex="999" Foreground="#66FFFFFF" FontSize="10" />');
			this.root.children.add(this.traceLog);
		}		
	},
	
	createObject: function()
	{
		/// <summary>Creates and adds the necessary DOM elements for the control.</summary>
		
		this.id = this.options.id || SlideShow.getUniqueId("SlideShow_");
		var sourceElementId = "SlideShow_Source";
		var parentElementId = this.id;
		var objectElementId = parentElementId + "_Object";
		
		// Add the source element
		// Note: Inline XAML used to be buggy in Firefox, but has been fixed in the latest update of Silverlight 1.0
		var xaml = '<Canvas xmlns="http://schemas.microsoft.com/client/2007" xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="Control" Visibility="Collapsed"></Canvas>';
		SlideShow.ScriptManager.addInlineScript(sourceElementId, "text/xaml", xaml);
		
		// Add the parent element
		// Note: We're using document.write instead of document.appendChild because the latter will not always work correctly (e.g. in tables).
		document.write('<div id="' + parentElementId + '" class="' + this.options.cssClass + '"></div>');
		
		// Add the object element
		// Note: See http://msdn2.microsoft.com/en-us/library/bb412401.aspx for createObjectEx documentation.
		Silverlight.createObjectEx(
		{
			id: objectElementId,
			source: "#" + sourceElementId,
			parentElement: document.getElementById(parentElementId),
			properties:
			{
				width: String(this.options.width),
				height: String(this.options.height),
				background: this.options.background,
				isWindowless: String(this.options.windowless),
				framerate: String(this.options.frameRate),
				inplaceInstallPrompt: SlideShow.parseBoolean(this.options.installInPlace),
				ignoreBrowserVer: SlideShow.parseBoolean(this.options.installUnsupportedBrowsers),
				version: "1.0"
			},
			events:
			{
				onLoad: SlideShow.createDelegate(this, this.onObjectLoad)
			}
		});
	},
	
	getTypeFromConfig: function(config)
	{
		/// <summary>Gets the type from the specified configuration.</summary>
		/// <param name="config">The configuration.</param>
		/// <returns>The evaluated type.</returns>
		
		var type = eval("SlideShow." + config.type);
		
		if (!type)
			throw new Error("Invalid type: " + config.type);
		
		return type;
	},
	
	getOptionsFromConfig: function(config)
	{
		/// <summary>Gets options (in hash format for lookups) from the specified configuration.</summary>
		/// <param name="config">The configuration.</param>
		/// <returns>The options hash.</returns>
		
		var options = {};
		
		if (config.option)
		{
			for (var i = 0, j = config.option.length; i < j; i++)
			{
				var name = config.option[i]["name"];
				var value = config.option[i]["value"];
				options[name] = value;
			}
		}
		
		return options;
	},
	
	createObjectInstanceFromConfig: function(config)
	{
		/// <summary>Creates an instance of the configured object.</summary>
		/// <param name="config">The configuration.</param>
		/// <returns>The instance.</returns>
		
		var type = this.getTypeFromConfig(config);
		var options = this.getOptionsFromConfig(config);
		return new type(this, options);
	},
	
	createModuleInstanceFromConfig: function(config)
	{
		/// <summary>Creates an instance of the configured module.</summary>
		/// <param name="config">The configuration.</param>
		/// <returns>The instance.</returns>
		
		var type = this.getTypeFromConfig(config);
		var options = this.getOptionsFromConfig(config);
		return new type(this, this, options);
	},
	
	loadScripts: function()
	{
		/// <summary>Loads the scripts configured for the control.</summary>
		
		if (this.options.scripts && this.options.scripts.script)
		{
			var manager = new SlideShow.ScriptManager();
			manager.addEventListener("loadComplete", SlideShow.createDelegate(this, this.onScriptsLoad));
			
			for (var i = 0, j = this.options.scripts.script.length; i < j; i++)
			{
				var script = this.options.scripts.script[i];
				manager.register(script.key, script.url);
			}
			
			manager.load();
		}
		else
		{
			this.onScriptsLoad(this);
		}
	},
	
	loadModules: function()
	{
		/// <summary>Loads the modules configured for the control.</summary>
		
		if (this.options.modules && this.options.modules.module)
		{
			var modules = {};
			
			for (var i = 0, j = this.options.modules.module.length; i < j; i++)
			{
				var config = this.options.modules.module[i];
				var module = modules[config.type] = this.createModuleInstanceFromConfig(config);
				module.render();
			}
			
			this.onModulesLoad(this, modules);
		}
	},
	
	loadData: function()
	{
		/// <summary>Loads the data configured for the control.</summary>
		
		if (this.options.dataProvider)
		{
			var provider = this.createObjectInstanceFromConfig(this.options.dataProvider);
			provider.getData(SlideShow.createDelegate(this, this.onDataLoad));
		}
	},
	
	isAlbumIndexValid: function(albumIndex)
	{
		/// <summary>Determines whether or not the specified album index is valid.</summary>
		/// <param name="albumIndex">The album index to check.</param>
		/// <returns>True if the album index is valid.</returns>
		
		return this.data && this.data.album && this.data.album[albumIndex];
	},
	
	isSlideIndexValid: function(albumIndex, slideIndex)
	{
		/// <summary>Determines whether or not the specified album and slide indexes are valid.</summary>
		/// <param name="albumIndex">The album index to check.</param>
		/// <param name="slideIndex">The slide index to check.</param>
		/// <returns>True if the indexes are valid.</returns>
		
		if (this.isAlbumIndexValid(albumIndex))
			return this.data.album[albumIndex].slide && this.data.album[albumIndex].slide[slideIndex];
		
		return false;
	},
	
	getSlideTransitionData: function(albumIndex, slideIndex)
	{
		/// <summary>Gets the transition data for a slide.</summary>
		/// <param name="albumIndex">The album index.</param>
		/// <param name="slideIndex">The slide index.</param>
		/// <returns>The transition data.</returns>
		
		var transitionName;
		
		if (!this.transitions)
			this.transitions = { notransition: { type: "NoTransition" } };
		
		if (this.isSlideIndexValid(albumIndex, slideIndex))
			transitionName = this.data.album[albumIndex].slide[slideIndex].transition;
		
		if (!transitionName && this.isAlbumIndexValid(albumIndex))
			transitionName = this.data.album[albumIndex].transition;
		
		if (!transitionName && this.data)
			transitionName = this.data.transition;
		
		if (!transitionName)
			transitionName = "NoTransition";
		
		var key = transitionName.toLowerCase();
		var transition = this.transitions[key];
		
		if (!transition)
		{
			for (var i = 0, j = this.options.transitions.transition.length; i < j; i++)
			{
				if (this.options.transitions.transition[i].name.toLowerCase() == key)
				{
					transition = this.options.transitions.transition[i];
					break;
				}
			}
			
			if (transition)
				this.transitions[key] = transition;
			else
				throw new Error("Invalid transition: " + transitionName);
		}
		
		return transition;
	},
	
	resize: function(width, height)
	{
		/// <summary>Resizes the control.</summary>
		/// <param name="width">The width value.</param>
		/// <param name="height">The height value.</param>
		
		this.host.setAttribute("width", width);
		this.host.setAttribute("height", height);
	},
	
	showEmbeddedMode: function()
	{
		/// <summary>Shows the control in embedded mode.</summary>
		
		this.host.content.fullScreen = false;
	},
	
	showFullScreenMode: function()
	{
		/// <summary>Shows the control in full-screen mode.</summary>
		
		this.host.content.fullScreen = true;
	},
	
	toggleFullScreenMode: function()
	{
		/// <summary>Toggles the control between embedded and full-screen mode.</summary>
		
		this.host.content.fullScreen = !this.host.content.fullScreen;
	},
	
	isFullScreenMode: function()
	{
		/// <summary>Specifies whether or not the control is in full-screen mode.</summary>
		/// <returns>True if the control is in full-screen mode.</returns>
		
		return this.host.content.fullScreen;
	},	
	
	trace: function(message)
	{
		/// <summary>Prepends a message to the on-screen trace log.</summary>
		/// <param name="message">The message to display.</param>
		
		if (this.traceLog)
		{
			if (this.traceLog.actualHeight > this.host.content.actualHeight - 10)
				this.traceLog.text = "";
			
			this.traceLog.text = message + "\n" + this.traceLog.text;
		}
	},
	
	onObjectLoad: function(host, context, root)
	{
		/// <summary>Handles the event fired when the Silverlight object is loaded.</summary>
		/// <param name="host">The host object.</param>
		/// <param name="context">The user context.</param>
		/// <param name="root">The root element.</param>
		
		this.root = root;
		this.host = host;
		
		this.render();
		this.onResize(this); // fixes a resize issue when width and height is set directly by createObjectEx
		
		this.host.content.onResize = SlideShow.createDelegate(this, this.onResize);
		this.host.content.onFullScreenChange = SlideShow.createDelegate(this, this.onFullScreenChange);
		//this.host.focus(); // enables initial focus in IE, but behaves oddly in Firefox
		
		this.loadScripts();
		this.fireEvent("objectLoad");
	},
	
	onConfigLoad: function(sender, e)
	{
		/// <summary>Handles the event fired when the configuration is loaded.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.fireEvent("configLoad");
		this.setOptions(e.configuration);
		this.createObject();
	},
	
	onScriptsLoad: function(sender, e)
	{
		/// <summary>Handles the event fired when the configured scripts are loaded.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.fireEvent("scriptsLoad");
		this.loadModules();
		this.loadData();
	},
	
	onModulesLoad: function(sender, e)
	{
		/// <summary>Handles the event fired when the configured modules are loaded.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.modules = e;
		this.fireEvent("modulesLoad");
	},
	
	onDataLoad: function(sender, e)
	{
		/// <summary>Handles the event fired when the configured data is loaded.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.data = e.data;
		
		if (this.data)
		{
			if (this.data.startalbumindex && !this.isAlbumIndexValid(this.data.startalbumindex))
				throw new Error("Invalid configuration: startAlbumIndex");
			
			if (this.data.startslideindex && !this.isSlideIndexValid((this.data.startalbumindex)? this.data.startalbumindex : 0, this.data.startslideindex))
				throw new Error("Invalid configuration: startSlideIndex");
		}
		
		this.fireEvent("dataLoad");
	},
	
	onResize: function(sender, e)
	{
		/// <summary>Handles the event fired when the control is resized in embedded mode.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		SlideShow.Control.base.resize.call(this, this.host.content.actualWidth, this.host.content.actualHeight);
		//this.fireEvent("resize");
	},
	
	onFullScreenChange: function(sender, e)
	{
		/// <summary>Handles the event fired when the control changes between embedded and full-screen mode.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.onResize(this);
		this.fireEvent("fullScreenChange");
	}
});

//var initCounter = 0;
//var loadCounter = 0;

/*******************************************
 * class: SlideShow.PageContainer
 *******************************************/
SlideShow.PageContainer = function(control, parent, options)
{
	/// <summary>A resizable control that renders pages of controls.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="options">The options for the control.</param>
	
	var xaml =
		'<Canvas xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="PageContainer" Visibility="Collapsed">' +
		'	<Canvas.Resources>' +
		'		<Storyboard x:Name="storyboard">' +
		'			<DoubleAnimationUsingKeyFrames Storyboard.TargetName="currentPageTransform" Storyboard.TargetProperty="X"> ' +
		'				<SplineDoubleKeyFrame x:Name="currentPageSplineFrom" KeySpline="0,0 0,0" KeyTime="0:0:0" />' +
		'				<SplineDoubleKeyFrame x:Name="currentPageSplineTo" KeySpline="0,0 0,1" />' +
		'			</DoubleAnimationUsingKeyFrames> ' +
		'			<DoubleAnimationUsingKeyFrames Storyboard.TargetName="nextPageTransform" Storyboard.TargetProperty="X"> ' +
		'				<SplineDoubleKeyFrame x:Name="nextPageSplineFrom" KeySpline="0,0 0,0" KeyTime="0:0:0" />' +
		'				<SplineDoubleKeyFrame x:Name="nextPageSplineTo" KeySpline="0,0 0,1" />' +
		'			</DoubleAnimationUsingKeyFrames> ' +
		'		</Storyboard>' +
		'	</Canvas.Resources>' +
		'	<Canvas.Clip>' +
		'		<RectangleGeometry x:Name="centerClip" />' +
		'	</Canvas.Clip>' +
		'	<Canvas x:Name="currentPage">' +
		'		<Canvas.RenderTransform>' +
		'			<TranslateTransform x:Name="currentPageTransform" />' +
		'		</Canvas.RenderTransform>' +
		'	</Canvas>' +
		'	<Canvas x:Name="nextPage">' +
		'		<Canvas.RenderTransform>' +
		'			<TranslateTransform x:Name="nextPageTransform" />' +
		'		</Canvas.RenderTransform>' +
		'	</Canvas>' +
		'</Canvas>';
	
	SlideShow.PageContainer.base.constructor.call(this, control, parent, xaml);

	SlideShow.merge(this.options,
	{
		top: 0,
		left: 0,
		right: 0,
		bottom: 0,
		itemWidth: 220,
		itemHeight: 80,
		padding: 10,
		spacing: 10,
		animatePageChanges: true,
		animationDuration: 0.6
	});
	
	this.setOptions(options);
	
	this.columns = 0;
	this.rows = 0;
	this.itemCountPerPage = 0;	
	this.pageIndex = 0;
	this.pageCount = 0;
	this.currentPage = this.root.findName("currentPage");
	this.nextPage = this.root.findName("nextPage");
	this.centerClip = this.root.findName("centerClip");
	this.currentPageTransform = this.root.findName("currentPageTransform");
	this.nextPageTransform = this.root.findName("nextPageTransform");
	this.storyboard = this.root.findName("storyboard");
	this.currentPageSplineFrom = this.root.findName("currentPageSplineFrom");
	this.currentPageSplineTo = this.root.findName("currentPageSplineTo");
	this.nextPageSplineFrom = this.root.findName("nextPageSplineFrom");
	this.nextPageSplineTo = this.root.findName("nextPageSplineTo");
	
	this.storyboard.addEventListener("Completed", SlideShow.createDelegate(this, this.onStoryboardComplete));
};

SlideShow.extend(SlideShow.UserControl, SlideShow.PageContainer,
{
	render: function()
	{
		/// <summary>Renders the control using the current options.</summary>
		
		SlideShow.PageContainer.base.render.call(this);
		
		this.currentPage.visibility = "Visible";
		this.nextPage.visibility = "Collapsed";		
	},
	
	determineItemFit: function(itemSize, containerSize)
	{
		/// <summary>Determines how many items can fit within a container.</summary>
		/// <param name="itemSize">The item size.</param>
		/// <param name="containerSize">The container size.</param>
		/// <returns>The number of items.</returns>
		
		if (containerSize == 0 || itemSize == 0)
			return 0;
		
		var spacePerSpacer = this.options.spacing / containerSize;
		var spacePerItem = itemSize / containerSize;
		var totalSpacePerItem = spacePerSpacer + spacePerItem;
		var containerSpace = 1 - spacePerSpacer;
		return Math.floor(containerSpace / totalSpacePerItem);
	},
	
	determineCanvasPosition: function(itemSize, itemIndex)
	{
		/// <summary>Determines where to position an item.</summary>
		/// <param name="itemSize">The item size.</param>
		/// <param name="itemIndex">The item index.</param>
		/// <returns>The position.</returns>
		
		var spacingSize = this.options.spacing * itemIndex;
		var existingItemSize = itemIndex * itemSize;
		return existingItemSize + spacingSize;
	},
	
	initializePages: function()
	{
		/// <summary>Initializes rows, columns, and items on the pages.</summary>
		
		//++initCounter;
		//this.control.trace("initCounter: " + initCounter);
		
		var reload = false;
		var columns = this.determineItemFit(this.options.itemWidth, this.root.width);
		var rows = this.determineItemFit(this.options.itemHeight, this.root.height);
		
		if (this.columns != columns || this.rows != rows)
		{
			reload = true;
			this.columns = columns;
			this.rows = rows;
			this.itemCountPerPage = columns * rows;
		}
		
		var pageWidth = this.columns * this.options.itemWidth + (this.columns - 1) * this.options.spacing;
		var pageHeight = this.rows * this.options.itemHeight + (this.rows - 1) * this.options.spacing;
		var centeringWidth = Math.max(this.root.width / 2 - pageWidth / 2, this.options.padding);
		var centeringHeight = this.options.padding;
		
		this.currentPage.width = this.nextPage.width = pageWidth;
		this.currentPage.height = this.nextPage.height = pageHeight;
		this.currentPage["Canvas.Left"] = this.nextPage["Canvas.Left"] = centeringWidth;
		this.currentPage["Canvas.Top"] = this.nextPage["Canvas.Top"] = centeringHeight;
		this.centerClip.Rect = centeringWidth + "," + centeringHeight + "," + pageWidth + "," + pageHeight;
		
		return reload;
	},
	
	showPage: function(items, animationDirection)
	{
		/// <summary>Displays the specified page of items.</summary>
		/// <param name="items">The items to display.</param>
		/// <param name="animationDirection">The animation direction (i.e. "Next" or "Previous").</param>
		
		var currentDuration = 0;
		var nextDuration = 0;
		var currentToValue = 0;
		var nextToValue = 0;
	
		if (animationDirection && this.options.animatePageChanges)
		{
			if (animationDirection == "Next")
			{
				currentDuration = (this.currentPageTransform.x + this.root.width) / this.root.width * this.options.animationDuration;
				nextDuration = (this.nextPageTransform.x + this.root.width) / this.root.width * this.options.animationDuration;
				currentToValue = -(this.currentPage.width + this.options.spacing);
				nextToValue = -(this.nextPage.width + this.options.spacing);
				this.nextPage["Canvas.Left"] = (this.currentPage["Canvas.Left"] + this.nextPage.width + this.options.spacing);
			}
			else
			{
				currentDuration = (this.root.width - this.currentPageTransform.x) / this.root.width * this.options.animationDuration;
				nextDuration = (this.root.width - this.nextPageTransform.x) / this.root.width * this.options.animationDuration;
				currentToValue = this.currentPage.width + this.options.spacing;
				nextToValue = this.nextPage.width + this.options.spacing;
				this.nextPage["Canvas.Left"] = (this.currentPage["Canvas.Left"] - this.nextPage.width - this.options.spacing);
			}
		}
		
		this.addItemsToContainer(this.nextPage, items);
		this.nextPage.visibility = "Visible";
		
		this.currentPageSplineFrom.value = this.currentPageTransform.x;
		this.currentPageSplineTo.value = currentToValue;
		this.currentPageSplineTo.keyTime = "0:0:" + currentDuration.toFixed(8);
		
		this.nextPageSplineFrom.value = this.nextPageTransform.x;
		this.nextPageSplineTo.value = nextToValue;
		this.nextPageSplineTo.keyTime = "0:0:" + nextDuration.toFixed(8);
		
		this.storyboard.begin();
	},
	
	addItemsToContainer: function(page, items)
	{
		/// <summary>Adds an array of items to a page.</summary>
		/// <param name="page">The page.</param>
		/// <param name="items">The items to add.</param>
		
		page.children.clear();
		
		for (var i = 0, j = 0; j < this.rows; j++)
		{
			for (var k = 0; k < this.columns; k++, i++)
			{
				if (items.length > i)
				{
					var item = items[i];
					page.children.add(item.root);
					
					item.setOptions(
					{
						width: this.options.itemWidth,
						height: this.options.itemHeight,
						top: this.determineCanvasPosition(this.options.itemHeight, j),
						left: this.determineCanvasPosition(this.options.itemWidth, k)
					});
										
					item.render();
				}
			}
		}	
	},
	
	loadPageByOffset: function(offset)
	{
		/// <summary>Displays a new page based on an offset from the current page index.</summary>
		/// <param name="offset">An offset from the current page index.</param>
		
		//++loadCounter;
		//this.control.trace("loadCounter: " + loadCounter + ", initCounter: " + initCounter);
		
		var itemCount = this.parent.getItemCount();
		
		if (itemCount > 0 && this.itemCountPerPage > 0)
			this.pageCount = Math.ceil(itemCount / this.itemCountPerPage);
		else
			this.pageCount = 0;
		
		var pageIndex = this.pageIndex + offset;
		
		if (pageIndex < 0)
			pageIndex = 0;
		
		if (pageIndex < this.pageCount)
		{
			this.pageIndex = pageIndex;
			var page = this.parent.getItems(this.pageIndex * this.itemCountPerPage, this.itemCountPerPage);
			var direction = (offset > 0) ? "Next" : (offset < 0) ? "Previous" : null;
			this.showPage(page, direction);
		}
		
		this.fireEvent("pageLoad");
	},
	
	refresh: function()
	{
		if (this.initializePages())
			this.loadPageByOffset(0);
	},
	
	onStoryboardComplete: function(sender, e)
	{
		/// <summary>Swaps the page instance references and resets the animation storyboard for the next page change.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		var nextPage = this.nextPage;
		var currentPage = this.currentPage;
		
		nextPage.visibility = "Visible";
		currentPage.visibility = "Collapsed";
		currentPage.children.clear(); 
		
		this.currentPage = this.nextPage;
		this.nextPage = currentPage;
		this.storyboard.stop();
		
		this.initializePages();
	},
	
	onSizeChanged: function()
	{
		/// <summary>Handles the event fired when the control is resized.</summary>
		
		SlideShow.PageContainer.base.onSizeChanged.call(this);
		
		this.pageIndex = 0;
		
		if (this.parent.root.visibility != "Collapsed")
		{
			window.clearTimeout(this.refreshTimerId);
			this.refreshTimerId = window.setTimeout(SlideShow.createDelegate(this, this.refresh), 10);
		}
	}
});

/*******************************************
 * class: SlideShow.SlideNavigation
 *******************************************/
SlideShow.SlideNavigation = function(control, parent, xaml)
{
	/// <summary>Provides a base class for navigation controls that interact with a SlideViewer instance.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="xaml">The XAML for the control.</param>
	
	SlideShow.SlideNavigation.base.constructor.call(this, control, parent, xaml);
	
	SlideShow.merge(this.options,
	{
		enableNextSlide: true,
		enablePreviousSlide: true,
		enableTransitionOnNext: true,
		enableTransitionOnPrevious: false
	});
	
	this.control.addEventListener("modulesLoad", SlideShow.createDelegate(this, this.onControlModulesLoad));
};

SlideShow.extend(SlideShow.UserControl, SlideShow.SlideNavigation,
{
	slideExistsByOffset: function(offset)
	{
		/// <summary>Determines if a slide exists based on the specified offset.</summary>
		/// <param name="offset">An integer value representing an offset from the current slide index.</param>
		/// <returns>True if a slide exists based on the offset.</param>
	
		return (this.options.loopAlbum || this.control.isSlideIndexValid(this.slideViewer.currentAlbumIndex, this.slideViewer.currentSlideIndex + offset));
	},
	
	showPreviousSlide: function()
	{
		/// <summary>Shows the previous slide in the slideshow.</summary>
		
		var offset = -1;
		
		if (this.slideViewer.currentSlideIndex != this.slideViewer.getDataIndexByOffset(offset))
		{
			if (this.slideExistsByOffset(offset))
			{
				if (this.slideViewer.currentTransition && this.slideViewer.currentTransition.state == "Started")
					this.slideViewer.fromImage.setSource(this.slideViewer.toImage.image.source);
				
				this.slideViewer.loadImageByOffset(offset, this.options.enableTransitionOnPrevious);
			}
		}
	},
	
	showNextSlide: function()
	{
		/// <summary>Shows the next slide in the slideshow.</summary>
		
		var offset = 1;
		
		if (this.slideViewer.currentSlideIndex != this.slideViewer.getDataIndexByOffset(offset))
		{
			if (this.slideExistsByOffset(offset))
			{
				if (this.slideViewer.currentTransition && this.slideViewer.currentTransition.state == "Started")
					this.slideViewer.fromImage.setSource(this.slideViewer.toImage.image.source);

				this.slideViewer.loadImageByOffset(offset, this.options.enableTransitionOnNext);
			}
		}
	},
	
	onControlModulesLoad: function(sender, e)
	{
		/// <summary>Completes initialization after all modules have loaded.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.slideViewer = this.control.modules["SlideViewer"];
		
		if (!this.slideViewer)
			throw new Error("Expected module missing: SlideViewer");
	}
});

/*******************************************
 * class: SlideShow.PageNavigation
 *******************************************/
SlideShow.PageNavigation = function(control, parent, xaml)
{
	/// <summary>Provides a base class for page container navigation.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="xaml">The XAML for the control.</param>
	
	SlideShow.PageNavigation.base.constructor.call(this, control, parent, xaml);
};

SlideShow.extend(SlideShow.SlideNavigation, SlideShow.PageNavigation,
{
	showPreviousPage: function()
	{
		/// <summary>Shows the previous page in the page container.</summary>
		
		this.parent.pageContainer.loadPageByOffset(-1);
	},
	
	showNextPage: function()
	{
		/// <summary>Shows the next page in the page container.</summary>
		
		this.parent.pageContainer.loadPageByOffset(1);
	}
});﻿// Slide.Show, version 1.0
// Copyright © Vertigo Software, Inc.
// This source is subject to the Microsoft Public License (Ms-PL).
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/publiclicense.mspx.
// All other rights reserved.

/// <reference path="SlideShow.js" />

/*******************************************
 * class: SlideShow.AlbumViewer
 *******************************************/
SlideShow.AlbumViewer = function(control, parent, options)
{
	/// <summary>Displays pages of albums to choose from.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="options">The options for the control.</param>
	
	var xaml = '<Canvas xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="AlbumViewer" Visibility="Collapsed" />';
	
	SlideShow.AlbumViewer.base.constructor.call(this, control, parent, xaml);
	
	SlideShow.merge(this.options,
	{
		top: 0,
		left: 0,
		bottom: 0,
		right: 0,
		background: "Black",
		visibility: "Collapsed",
		transitionSlideOnAlbumChange: true,
		pageContainer: {},
		albumNavigation: {},
		albumButton: {}
	});
	
	this.setOptions(options);
	
	this.pageContainer = new SlideShow.PageContainer(control, this, this.options.pageContainer);
	this.albumNavigation = new SlideShow.AlbumNavigation(control, this, this.options.albumNavigation);
	this.pageContainer.options.bottom = this.albumNavigation.options.height;
	
	this.control.addEventListener("modulesLoad", SlideShow.createDelegate(this, this.onModulesLoad));
};

SlideShow.extend(SlideShow.UserControl, SlideShow.AlbumViewer,
{	
	getItems: function(fromIndex, count)
	{
		/// <summary>Retrieves a set of items from the album collection.</summary>
		/// <param name="fromIndex">The index from which to retrieve items from the album collection.</param>
		/// <param name="count">The number of items to retrieve.</param>
		/// <returns>An array of AlbumButton items.</returns>
		
		var items = [];
		
		for (var i = fromIndex, j = fromIndex + count; i < j; i++)
		{
			var album = this.control.data.album[i];
			
			if (album)
			{
				var item = new SlideShow.AlbumButton(this.control, null, album, this.options.albumButton);
				item.addEventListener("click", SlideShow.createDelegate(this, this.onAlbumClick));
				items.push(item);
			}
		}
		
		return items;
	},
	
	getItemCount: function()
	{
		/// <summary>Retrieves the number of items in the album collection.</summary>
		
		return this.control.isAlbumIndexValid(0) ? this.control.data.album.length : 0;
	},
	
	onModulesLoad: function(sender, e)
	{
		/// <summary>Completes initialization after all modules have loaded.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.slideViewer = this.control.modules["SlideViewer"];
		
		if (!this.slideViewer)
			throw new Error("Expected module missing: SlideViewer");
	},

	onAlbumClick: function(sender, e)
	{
		/// <summary>Bubbles up an event to indicate that an album has been selected.</summary>
		/// <param name="sender">The AlbumButton instance that was clicked.</param>
		/// <param name="e">The album information associated with the selected instance.</param>
	
		for (var i = 0, j = this.control.data.album.length; i < j; i++)
		{
			if (this.control.data.album[i] == e && (this.slideViewer.currentAlbumIndex != i || this.slideViewer.currentSlideIndex != 0))
			{
				this.slideViewer.currentAlbumIndex = i;
				this.slideViewer.currentSlideIndex = 0;
				this.slideViewer.loadImageByOffset(0, this.options.transitionSlideOnAlbumChange);
			}
		}
		
		this.fireEvent("albumClick", e);
	}
});

/*******************************************
 * class: SlideShow.AlbumNavigation
 *******************************************/
SlideShow.AlbumNavigation = function(control, parent, options)
{
	/// <summary>Provides page navigation for an AlbumViewer.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="options">The options for the control.</param>
	
	var xaml = '<Canvas xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="AlbumNavigation" Visibility="Collapsed"><TextBlock x:Name="StatusTextBlock" /></Canvas>';
	
	SlideShow.AlbumNavigation.base.constructor.call(this, control, parent, xaml);
	
	SlideShow.merge(this.options,
	{
		left: "50%",
		bottom: 0,
		width: 140,
		height: 30,
		foreground: "#777",
		fontFamily: "Portable User Interface",
		fontSize: 12,
		fontStretch: "Normal",
		fontStyle: "Normal",
		fontWeight: "Normal",
		previousButton:
		{
			width: 24,
			height: 30,
			radius: 0,
			strokeThickness: 0,
			backgroundColor1: "Transparent",
			backgroundColor2: "Transparent",
			pathData: "M0.049999999,0.81200001 C0.049999999,0.39115903 0.39115902,0.049999999 0.81199999,0.049999999 L2.711,0.049999999 C3.1318409,0.049999999 3.473,0.39115903 3.473,0.81200001 L3.473,9.1880002 C3.473,9.6088412 3.1318409,9.9500002 2.711,9.9500002 L0.81199999,9.9500002 C0.39115902,9.9500002 0.049999999,9.6088412 0.049999999,9.1880002 z M-3.3603748,0.016 L-9.344,4.9998124 -3.3599998,9.9840002 Z",
			pathWidth: 10,
			pathHeight: 8,
			pathFill: "#777",
			pathFillDisabled: "#333"
		},
		nextButton:
		{
			right: 0,
			width: 24,
			height: 30,
			radius: 0,
			strokeThickness: 0,
			backgroundColor1: "Transparent",
			backgroundColor2: "Transparent",
			pathData: "M0.049999999,0.81200001 C0.049999999,0.39115903 0.39115902,0.049999999 0.81199999,0.049999999 L2.711,0.049999999 C3.1318409,0.049999999 3.473,0.39115903 3.473,0.81200001 L3.473,9.1880002 C3.473,9.6088412 3.1318409,9.9500002 2.711,9.9500002 L0.81199999,9.9500002 C0.39115902,9.9500002 0.049999999,9.6088412 0.049999999,9.1880002 z M6.9063742,0.016 L12.875,4.9998124 6.8910001,9.9840002 Z",
			pathWidth: 10,
			pathHeight: 8,
			pathFill: "#777",
			pathFillDisabled: "#333"
		}
	});
	
	this.setOptions(options);
	
	this.previousButton = new SlideShow.PathButton(control, this, this.options.previousButton);
	this.nextButton = new SlideShow.PathButton(control, this, this.options.nextButton);
	this.statusTextBlock = this.root.findName("StatusTextBlock");
	
	this.previousButton.addEventListener("click", SlideShow.createDelegate(this, this.onPreviousClick));
	this.nextButton.addEventListener("click", SlideShow.createDelegate(this, this.onNextClick));
	parent.pageContainer.addEventListener("pageLoad", SlideShow.createDelegate(this, this.onPageLoad));	
};

SlideShow.extend(SlideShow.PageNavigation, SlideShow.AlbumNavigation,
{	
	render: function()
	{
		/// <summary>Renders the control using the current options.</summary>
		
		SlideShow.AlbumNavigation.base.render.call(this);
		
		this.statusTextBlock.width = this.options.width - this.previousButton.options.width - this.nextButton.options.width;
		this.statusTextBlock.height = this.options.height;
		this.statusTextBlock.foreground = this.options.foreground;
		this.statusTextBlock.fontFamily = this.options.fontFamily;
		this.statusTextBlock.fontSize = this.options.fontSize;
		this.statusTextBlock.fontStretch = this.options.fontStretch;
		this.statusTextBlock.fontStyle = this.options.fontStyle;
		this.statusTextBlock.fontWeight = this.options.fontWeight;
	},
	
	setStatus: function(text)
	{
		/// <summary>Sets the status text.</summary>
		/// <param name="text">The new text.</param>
		
		SlideShow.addTextToBlock(this.statusTextBlock, text);
		this.statusTextBlock["Canvas.Top"] = this.root.height / 2 - this.statusTextBlock.actualHeight / 2;
		this.statusTextBlock["Canvas.Left"] = this.root.width / 2 - this.statusTextBlock.actualWidth / 2;
	},
	
	onPreviousClick: function(sender, e) 
	{
		/// <summary>Handles the event fired when the previous button is clicked.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.showPreviousPage();
	},
	
	onNextClick: function(sender, e) 
	{
		/// <summary>Handles the event fired when the next button is clicked.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.showNextPage();
	},
	
	onPageLoad: function(sender, e) 
	{
		/// <summary>Handles the event fired when a page is loaded in the page container.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		var pageIndex = this.parent.pageContainer.pageIndex;
		var pageCount = this.parent.pageContainer.pageCount;
		
		this.setStatus(SlideShow.formatString("Page {0} of {1}", pageIndex + 1, pageCount));
		
		if (pageIndex > 0)
			this.previousButton.enable();
		else
			this.previousButton.disable();
		
		if (pageIndex < pageCount - 1)
			this.nextButton.enable();
		else
			this.nextButton.disable();
		
		this.root.visibility = (pageCount > 0) ? "Visible" : "Collapsed";			
	}
});﻿// Slide.Show, version 1.0
// Copyright © Vertigo Software, Inc.
// This source is subject to the Microsoft Public License (Ms-PL).
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/publiclicense.mspx.
// All other rights reserved.

/// <reference path="SlideShow.js" />

/*******************************************
 * class: SlideShow.Button
 *******************************************/
SlideShow.Button = function(control, parent, xaml)
{
	/// <summary>Provides a base class for button controls.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="xaml">The XAML for the button.</param>
	
	SlideShow.Button.base.constructor.call(this, control, parent, xaml);
	
	SlideShow.merge(this.options,
	{
		width: 22,
		height: 20,
		cursor: "Hand"
	});
	
	this.state = "Default";
	
	this.root.addEventListener("MouseEnter", SlideShow.createDelegate(this, this.onMouseEnter));
	this.root.addEventListener("MouseLeave", SlideShow.createDelegate(this, this.onMouseLeave));
	this.root.addEventListener("MouseLeftButtonDown", SlideShow.createDelegate(this, this.onMouseDown));
	this.root.addEventListener("MouseLeftButtonUp", SlideShow.createDelegate(this, this.onMouseUp));	
};

SlideShow.extend(SlideShow.UserControl, SlideShow.Button,
{
	render: function()
	{
		/// <summary>Renders the button using the current options.</summary>
		
		SlideShow.Button.base.render.call(this);
		
		this.root.cursor = this.options.cursor;
	},
	
	setState: function(state)
	{
		/// <summary>Sets the state of the button.</summary>
		/// <param name="state">The new state.</param>
		
		this.state = state;
		
		switch (state)
		{
			case "Disabled":
				this.root.cursor = "Default";
				break;	
			
			default:	
				this.root.cursor = this.options.cursor;
				break;
		}
	},
	
	enable: function()
	{
		/// <summary>Enables the button.</summary>
		
		if (this.state == "Disabled")
			this.setState("Default");
	},
	
	disable: function()
	{
		/// <summary>Disables the button.</summary>
		
		if (this.state != "Disabled")
			this.setState("Disabled");
	},
	
	onMouseEnter: function(sender, e)
	{
		/// <summary>Handles the event fired when the mouse enters the button.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		if (this.state != "Disabled")
			this.setState((this.state.indexOf("Active") > -1) ? "ActiveHover" : "Hover");
	},
	
	onMouseLeave: function(sender, e)
	{
		/// <summary>Handles the event fired when the mouse leaves the button.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		if (this.state != "Disabled")
			this.setState((this.state.indexOf("Active") > -1) ? "ActiveUnhover" : "Unhover");
	},
	
	onMouseDown: function(sender, e)
	{
		/// <summary>Handles the event fired when the mouse button is pressed.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		if (this.state != "Disabled")
		{
			this.setState((this.state.indexOf("Hover") > -1) ? "ActiveHover" : "Default");
			sender.captureMouse();
		}
	},
	
	onMouseUp: function(sender, e)
	{
		/// <summary>Handles the event fired when the mouse button is released.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		if (this.state != "Disabled")
		{
			var isClick = (this.state == "ActiveHover");
			this.setState((this.state.indexOf("Hover") > -1) ? "InactiveHover" : "Default");
			sender.releaseMouseCapture();
			
			if (isClick)
				this.onClick(e);
		}
	},
	
	onClick: function(e)
	{
		/// <summary>Handles the event fired when the button is clicked.</summary>
		/// <param name="e">The event arguments.</param>
		
		if (this.state != "Disabled")
			this.fireEvent("click", e);
	}
});

/*******************************************
 * class: SlideShow.PathButton
 *******************************************/
SlideShow.PathButton = function(control, parent, options)
{
	/// <summary>A path button control.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="options">The options for the button.</param>
	
	var xaml =
		'<Canvas xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="PathButton" Visibility="Collapsed">' +
		'	<Canvas.Resources>' +
		'		<Storyboard x:Name="HoverStoryboard">' +
		'			<ColorAnimationUsingKeyFrames Storyboard.TargetName="Path" Storyboard.TargetProperty="(Fill).(Color)">' +
		'				<LinearColorKeyFrame x:Name="HoverKeyFrame" />' +	
		'			</ColorAnimationUsingKeyFrames>' +
		'		</Storyboard>' +
		'	</Canvas.Resources>' +
		'	<Canvas.Clip>' +
		'		<RectangleGeometry x:Name="Clip" />' +
		'	</Canvas.Clip>' +
		'	<Rectangle x:Name="Background">' +
		'		<Rectangle.Fill>' +
		'			<LinearGradientBrush StartPoint="0.477254,1.16548" EndPoint="0.477254,0.0426189">' +
		'				<LinearGradientBrush.GradientStops>' +
		'					<GradientStop x:Name="BackgroundColor1" Offset="0.232877" />' +
		'					<GradientStop x:Name="BackgroundColor2" Offset="0.987288" />' +
		'				</LinearGradientBrush.GradientStops>' +
		'			</LinearGradientBrush>' +
		'		</Rectangle.Fill>' +
		'	</Rectangle>' +
		'	<Path x:Name="Path" />' +
		'</Canvas>';
	
	SlideShow.PathButton.base.constructor.call(this, control, parent, xaml);
	
	SlideShow.merge(this.options,
	{
		radius: 2,
		stroke: "#7F808BBC",
		strokeThickness: 1,
		backgroundColor1: "#273B5B",
		backgroundColor2: "#6A75A2",
		pathData: null,
		pathWidth: 10,
		pathHeight: 10,
		pathStretch: "Uniform",
		pathFill: "#BDC3DF",
		pathFillHover: "White",
		pathFillDisabled: "#6A75A2",
		hoverAnimationDuration: 0.2
	});
	
	this.setOptions(options);
	
	this.hoverStoryboard = this.root.findName("HoverStoryboard");
	this.hoverKeyFrame = this.root.findName("HoverKeyFrame");
	this.clip = this.root.findName("Clip");
	this.background = this.root.findName("Background");
	this.backgroundColor1 = this.root.findName("BackgroundColor1");
	this.backgroundColor2 = this.root.findName("BackgroundColor2");
	this.path = this.root.findName("Path");	
};

SlideShow.extend(SlideShow.Button, SlideShow.PathButton,
{
	render: function()
	{
		/// <summary>Renders the button using the current options.</summary>
		
		SlideShow.PathButton.base.render.call(this);
		
		// hover
		this.hoverKeyFrame.keyTime = SlideShow.formatString("0:0:{0}", this.options.hoverAnimationDuration);
		
		// clip
		this.clip.rect = SlideShow.formatString("0,0,{0},{1}", this.options.width, this.options.height);
		this.clip.radiusX = this.options.radius;
		this.clip.radiusY = this.options.radius;
		
		// background
		this.background.width = this.options.width;
		this.background.height = this.options.height;
		this.background.radiusX = this.options.radius;
		this.background.radiusY = this.options.radius;
		this.background.stroke = this.options.stroke;
		this.background.strokeThickness = this.options.strokeThickness;
		this.backgroundColor1.color = this.options.backgroundColor1;
		this.backgroundColor2.color = this.options.backgroundColor2;
		
		// path
		this.path.data = this.options.pathData;
		this.path.width = this.options.pathWidth;
		this.path.height = this.options.pathHeight;
		this.path.stretch = this.options.pathStretch;
		this.path.fill = this.options.pathFillDisabled;
		this.path["Canvas.Top"] = this.options.height / 2 - this.options.pathHeight / 2;
		this.path["Canvas.Left"] = this.options.width / 2 - this.options.pathWidth / 2;
		
		// disable by default
		this.disable();
	},
	
	setState: function(state)
	{
		/// <summary>Sets the button state.</summary>
		/// <param name="state">The new state.</param>
		
		SlideShow.PathButton.base.setState.call(this, state);
		
		switch (state)
		{
			case "Hover":
			case "ActiveHover":
			case "InactiveHover":
				this.hoverKeyFrame.value = this.options.pathFillHover;
				this.hoverStoryboard.begin();
				break;
				
			case "Disabled":
				this.hoverKeyFrame.value = this.options.pathFillDisabled;
				this.hoverStoryboard.begin();
				break;
			
			default:
				this.hoverKeyFrame.value = this.options.pathFill;
				this.hoverStoryboard.begin();
				break;
		}
	},
	
	setPath: function(data, width, height)
	{
		/// <summary>Sets the button path dynamically.</summary>
		/// <param name="data">The path data.</param>
		/// <param name="width">The width of the path.</param>
		/// <param name="height">The height of the path.</param>
		
		this.path.data = data;
		this.path.width = width || this.options.pathWidth;
		this.path.height = height || this.options.pathHeight;
		this.path["Canvas.Top"] = this.root.height / 2 - this.path.height / 2;
		this.path["Canvas.Left"] = this.root.width / 2 - this.path.width / 2;
	}
});

/*******************************************
 * class: SlideShow.AlbumButton
 *******************************************/
SlideShow.AlbumButton = function(control, parent, album, options)
{
	/// <summary>An album button control.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="options">The options for the button.</param>
	/// <param name="album">The target album.</param>
	
	var xaml =
		'<Canvas xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="AlbumButton" Visibility="Collapsed">' +
		'	<Canvas.Resources>' +
		'		<Storyboard x:Name="HoverStoryboard">' +
		'			<ColorAnimationUsingKeyFrames Storyboard.TargetName="Background" Storyboard.TargetProperty="(Fill).(Color)">' +
		'				<LinearColorKeyFrame x:Name="HoverKeyFrame" />' +	
		'			</ColorAnimationUsingKeyFrames>' +
		'		</Storyboard>' +
		'		<Storyboard x:Name="LoadStoryboard" Storyboard.TargetName="Image" Storyboard.TargetProperty="Opacity">' +
		'			<DoubleAnimation x:Name="LoadAnimation" To="1" />' +
		'		</Storyboard>' +		
		'	</Canvas.Resources>' +
		'	<Canvas.Clip>' +
		'		<RectangleGeometry x:Name="Clip" />' +
		'	</Canvas.Clip>' +
		'	<Rectangle x:Name="Background" />' +
		'	<Rectangle x:Name="ImageBackground" />' +
		'	<Rectangle x:Name="Image" Opacity="0">' +
		'		<Rectangle.Fill>' +
		'			<ImageBrush x:Name="ImageBrush" />' +
		'		</Rectangle.Fill>' +
		'	</Rectangle>' +
		'	<TextBlock x:Name="TitleTextBlock" TextWrapping="Wrap" />' +
		'	<TextBlock x:Name="DescriptionTextBlock" TextWrapping="Wrap" />' +
		'</Canvas>';
	
	SlideShow.AlbumButton.base.constructor.call(this, control, parent, xaml);
	
	SlideShow.merge(this.options,
	{
		background: "#222",
		backgroundHover: "#333",
		hoverAnimationDuration: 0.2,
		loadAnimationDuration: 0.5,
		radius: 4,
		stroke: "#333",
		strokeThickness: 1,
		imageWidth: 75,
		imageHeight: 60,
		imagePaddingTop: 6,
		imagePaddingLeft: 6,
		imageBackground: "#777",
		imageSource: null,
		imageStretch: "UniformToFill",
		imageRadius: 0,
		imageStroke: "#333",
		imageStrokeThickness: 1,
		titleWidth: 126,
		titleHeight: 20,
		titlePaddingTop: 4,
		titlePaddingLeft: 6,
		titleFontFamily: "Portable User Interface",
		titleFontSize: 12,
		titleFontStretch: "Normal",
		titleFontStyle: "Normal",
		titleFontWeight: "Bold",
		titleForeground: "White",
		descriptionWidth: 126,
		descriptionHeight: 52,
		descriptionPaddingTop: 0,
		descriptionPaddingLeft: 6,
		descriptionFontFamily: "Portable User Interface",
		descriptionFontSize: 10,
		descriptionFontStretch: "Normal",
		descriptionFontStyle: "Normal",
		descriptionFontWeight: "Normal",
		descriptionForeground: "#777"
	});
	
	this.setOptions(options);
	
	this.album = album;
	this.hoverStoryboard = this.root.findName("HoverStoryboard");
	this.hoverKeyFrame = this.root.findName("HoverKeyFrame");
	this.loadStoryboard = this.root.findName("LoadStoryboard");
	this.loadAnimation = this.root.findName("LoadAnimation");		
	this.clip = this.root.findName("Clip");
	this.background = this.root.findName("Background");
	this.imageBackground = this.root.findName("ImageBackground");
	this.image = this.root.findName("Image");
	this.imageBrush = this.root.findName("ImageBrush");
	this.titleTextBlock = this.root.findName("TitleTextBlock");
	this.descriptionTextBlock = this.root.findName("DescriptionTextBlock");
};

SlideShow.extend(SlideShow.Button, SlideShow.AlbumButton,
{
	render: function()
	{
		/// <summary>Renders the button using the current options.</summary>
				
		SlideShow.AlbumButton.base.render.call(this);
		
		// image source
		if (!this.album)
			throw new Error("Album missing");
		else if (this.album.image)
			this.options.imageSource = this.album.image;
		else if (this.album.slide && this.album.slide.length)
			this.options.imageSource = this.album.slide[0].image;
		
		// clip
		this.clip.rect = SlideShow.formatString("0,0,{0},{1}", this.options.width, this.options.height);
		this.clip.radiusX = this.options.radius;
		this.clip.radiusY = this.options.radius;
		
		// background
		this.background.width = this.options.width;
		this.background.height = this.options.height;
		this.background.radiusX = this.options.radius;
		this.background.radiusY = this.options.radius;
		this.background.fill = this.options.background;
		this.background.stroke = this.options.stroke;
		this.background.strokeThickness = this.options.strokeThickness;
		
		// image background
		this.imageBackground.width = this.options.imageWidth;
		this.imageBackground.height = this.options.imageHeight;
		this.imageBackground["Canvas.Top"] = this.options.imagePaddingTop;
		this.imageBackground["Canvas.Left"] = this.options.imagePaddingLeft;
		this.imageBackground.radiusX = this.options.imageRadius;
		this.imageBackground.radiusY = this.options.imageRadius;
		this.imageBackground.fill = this.options.imageBackground;
		this.imageBackground.stroke = this.options.imageStroke;
		this.imageBackground.strokeThickness = this.options.imageStrokeThickness;
		
		// image
		this.image.width = this.options.imageWidth;
		this.image.height = this.options.imageHeight;
		this.image["Canvas.Top"] = this.options.imagePaddingTop;
		this.image["Canvas.Left"] = this.options.imagePaddingLeft;
		this.image.radiusX = this.options.imageRadius;
		this.image.radiusY = this.options.imageRadius;
		this.image.stroke = this.options.imageStroke;
		this.image.strokeThickness = this.options.imageStrokeThickness;
		this.imageBrush.stretch = this.options.imageStretch;
		this.imageBrush.imageSource = this.options.imageSource;
		
		// load animation
		if (this.imageBrush.downloadProgress == 1)
		{
			this.image.opacity = 1;
		}
		else
		{
			this.loadAnimation.duration = "0:0:" + this.options.loadAnimationDuration;
			this.imageBrush.addEventListener("DownloadProgressChanged", SlideShow.createDelegate(this, this.onImageDownloadProgressChanged));
			this.imageBrush.addEventListener("ImageFailed", SlideShow.createDelegate(this, this.onImageDownloadFailed));
		}
		
		// title
		this.titleTextBlock.width = this.options.titleWidth;
		this.titleTextBlock.height = this.options.titleHeight;
		this.titleTextBlock["Canvas.Top"] = this.options.titlePaddingTop;
		this.titleTextBlock["Canvas.Left"] = (this.options.imageWidth > 0) ? this.options.imagePaddingLeft + this.options.imageWidth + this.options.titlePaddingLeft : this.options.titlePaddingLeft;
		this.titleTextBlock.fontFamily = this.options.titleFontFamily;
		this.titleTextBlock.fontSize = this.options.titleFontSize;
		this.titleTextBlock.fontStretch = this.options.titleFontStretch;
		this.titleTextBlock.fontStyle = this.options.titleFontStyle;
		this.titleTextBlock.fontWeight = this.options.titleFontWeight;
		this.titleTextBlock.foreground = this.options.titleForeground;
		SlideShow.addTextToBlock(this.titleTextBlock, this.album.title);
		
		// description
		this.descriptionTextBlock.width = this.options.descriptionWidth;
		this.descriptionTextBlock.height = this.options.descriptionHeight;
		this.descriptionTextBlock["Canvas.Top"] = (this.options.titleHeight > 0) ? this.options.titlePaddingTop + this.options.titleHeight + this.options.descriptionPaddingTop : this.options.descriptionPaddingTop;
		this.descriptionTextBlock["Canvas.Left"] = (this.options.imageWidth > 0) ? this.options.imagePaddingLeft + this.options.imageWidth + this.options.descriptionPaddingLeft : this.options.descriptionPaddingLeft;
		this.descriptionTextBlock.fontFamily = this.options.descriptionFontFamily;
		this.descriptionTextBlock.fontSize = this.options.descriptionFontSize;
		this.descriptionTextBlock.fontStretch = this.options.descriptionFontStretch;
		this.descriptionTextBlock.fontStyle = this.options.descriptionFontStyle;
		this.descriptionTextBlock.fontWeight = this.options.descriptionFontWeight;
		this.descriptionTextBlock.foreground = this.options.descriptionForeground;
		SlideShow.addTextToBlock(this.descriptionTextBlock, this.album.description);
		
		// hover animation
		this.hoverKeyFrame.keyTime = SlideShow.formatString("0:0:{0}", this.options.hoverAnimationDuration);
	},
	
	setState: function(state)
	{
		/// <summary>Sets the button state.</summary>
		/// <param name="state">The new state.</param>
		
		SlideShow.AlbumButton.base.setState.call(this, state);
		
		switch (state)
		{
			case "Hover":
			case "ActiveHover":
			case "InactiveHover":
				this.hoverKeyFrame.value = this.options.backgroundHover;
				this.hoverStoryboard.begin();
				break;
			
			default:
				this.hoverKeyFrame.value = this.options.background;
				this.hoverStoryboard.begin();
				break;
		}
	},
	
	onImageDownloadProgressChanged: function(sender, e)
	{
		/// <summary>Handles the event fired while the image is downloading.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		if (sender.downloadProgress == 1)
			this.loadStoryboard.begin();
	},
	
	onImageDownloadFailed: function(sender, e)
	{
		/// <summary>Handles the event fired when the image failed to download.</summary>
		/// <param name="sender">The event soure.</param>
		/// <param name="e">The event arguments.</param>
		
		throw new Error("Image download failed: " + this.imageBrush.imageSource);
	},
	
	onClick: function(e)
	{
		/// <summary>Handles the event fired when the button is clicked.</summary>
		/// <param name="e">The event arguments.</param>
		
		SlideShow.AlbumButton.base.onClick.call(this, this.album);
	}
});

/*******************************************
 * class: SlideShow.ThumbnailButton
 *******************************************/
SlideShow.ThumbnailButton = function(control, parent, slide, options)
{
	/// <summary>A thumbnail button control.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="slide">The target slide.</param>
	/// <param name="options">The options for the button.</param>
	
	var xaml =
		'<Canvas xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="ThumbnailButton" Visibility="Collapsed">' +
		'	<Canvas.Resources>' +
		'		<Storyboard x:Name="LoadStoryboard" Storyboard.TargetName="Image" Storyboard.TargetProperty="Opacity">' +
		'			<DoubleAnimation x:Name="LoadAnimation" To="1" />' +
		'		</Storyboard>' +
		'	</Canvas.Resources>' +
		'	<Canvas.Clip>' +
		'		<RectangleGeometry x:Name="Clip" />' +
		'	</Canvas.Clip>' +
		'	<Rectangle x:Name="Background" />' +
		'	<Rectangle x:Name="Image" Opacity="0">' +
		'		<Rectangle.Fill>' +
		'			<ImageBrush x:Name="ImageBrush" />' +
		'		</Rectangle.Fill>' +
		'	</Rectangle>' +
		'</Canvas>';
	
	SlideShow.ThumbnailButton.base.constructor.call(this, control, parent, xaml);
	
	SlideShow.merge(this.options,
	{
		radius: 0,
		stroke: "Black",
		selectedStroke: "White",
		strokeThickness: 1,
		imageStretch: "UniformToFill",
		imageBackground: "#333",
		loadAnimationDuration: 0.5
	});
	
	this.setOptions(options);
	
	this.slide = slide;
	this.isSelected = false;
	this.loadStoryboard = this.root.findName("LoadStoryboard");
	this.loadAnimation = this.root.findName("LoadAnimation");
	this.clip = this.root.findName("Clip");
	this.background = this.root.findName("Background");
	this.image = this.root.findName("Image");
	this.imageBrush = this.root.findName("ImageBrush");
};

SlideShow.extend(SlideShow.Button, SlideShow.ThumbnailButton,
{
	render: function()
	{
		/// <summary>Renders the button using the current options.</summary>
		
		SlideShow.ThumbnailButton.base.render.call(this);
		
		// clip
		this.clip.rect = SlideShow.formatString("0,0,{0},{1}", this.options.width, this.options.height);
		this.clip.radiusX = this.options.radius;
		this.clip.radiusY = this.options.radius;
		
		// background
		this.background.width = this.options.width;
		this.background.height = this.options.height;
		this.background.radiusX = this.options.radius;
		this.background.radiusY = this.options.radius;
		this.background.fill = this.options.imageBackground;
		this.background.stroke = this.isSelected ? this.options.selectedStroke : this.options.stroke;
		this.background.strokeThickness = this.options.strokeThickness;
		
		// image
		this.image.width = this.options.width;
		this.image.height = this.options.height;
		this.image.radiusX = this.options.radius;
		this.image.radiusY = this.options.radius;
		this.image.stroke = this.isSelected ? this.options.selectedStroke : this.options.stroke;
		this.image.strokeThickness = this.options.strokeThickness;
		this.imageBrush.stretch = this.options.imageStretch;
		this.imageBrush.imageSource = this.getThumbnailImageSource();
		
		// load animation
		if (this.imageBrush.downloadProgress == 1)
		{
			this.image.opacity = 1;
		}
		else
		{
			this.loadAnimation.duration = "0:0:" + this.options.loadAnimationDuration;
			this.imageBrush.addEventListener("DownloadProgressChanged", SlideShow.createDelegate(this, this.onImageDownloadProgressChanged));
			this.imageBrush.addEventListener("ImageFailed", SlideShow.createDelegate(this, this.onImageDownloadFailed));
		}
	},
	
	getThumbnailImageSource: function()
	{
		/// <summary>Retrieves the appropriate thumbnail image for the button.</summary>
		/// <returns>The thumbnail image source.</returns>
		
		return this.slide.thumbnail || this.slide.image;
	},

	onSlideLoading: function(sender, e)
	{
		/// <summary>Handles the event fired when a new slide begins downloading.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.isSelected = e == this.slide;
		this.background.stroke = this.isSelected ? this.options.selectedStroke : this.options.stroke;
		this.image.stroke = this.isSelected ? this.options.selectedStroke : this.options.stroke;
	},
	
	onMouseEnter: function(sender, e)
	{
		/// <summary>Handles the event fired when the mouse enters the button.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		SlideShow.ThumbnailButton.base.onMouseEnter.call(this, sender, e);
		this.fireEvent("mouseEnter", this.isSelected);
	},
	
	onMouseLeave: function(sender, e)
	{
		/// <summary>Handles the event fired when the mouse leaves the button.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		SlideShow.ThumbnailButton.base.onMouseLeave.call(this, sender, e);
		this.fireEvent("mouseLeave", this.isSelected);
	},
	
	onImageDownloadProgressChanged: function(sender, e)
	{
		/// <summary>Handles the event fired while the image is downloading.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		if (sender.downloadProgress == 1)
			this.loadStoryboard.begin();
	},
	
	onImageDownloadFailed: function(sender, e)
	{
		/// <summary>Handles the event fired when the image failed to download.</summary>
		/// <param name="sender">The event soure.</param>
		/// <param name="e">The event arguments.</param>
		
		throw new Error("Image download failed: " + this.imageBrush.imageSource);
	},
	
	onClick: function(e)
	{
		/// <summary>Handles the event fired when the button is clicked.</summary>
		/// <param name="e">The event arguments.</param>
		
		SlideShow.ThumbnailButton.base.onClick.call(this, this.slide);
	}
});﻿// Slide.Show, version 1.0
// Copyright © Vertigo Software, Inc.
// This source is subject to the Microsoft Public License (Ms-PL).
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/publiclicense.mspx.
// All other rights reserved.

/// <reference path="SlideShow.js" />

/*******************************************
 * class: SlideShow.DataProvider
 *******************************************/
SlideShow.DataProvider = function(control)
{
	/// <summary>Provides a base class for data providers.</summary>
	/// <param name="control">The Slide.Show control.</param>
	
	SlideShow.DataProvider.base.constructor.call(this);
	
	this.control = control;
};

SlideShow.extend(SlideShow.Object, SlideShow.DataProvider,
{
});

/*******************************************
 * class: SlideShow.XmlDataProvider
 *******************************************/
SlideShow.XmlDataProvider = function(control, options)
{
	/// <summary>Provides album/slide data from an XML file.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="options">The options for the provider.</param>
	
	SlideShow.XmlDataProvider.base.constructor.call(this, control);
	
	SlideShow.merge(this.options,
	{
		url: "Data.xml"
	});
	
	this.setOptions(options);
};

SlideShow.extend(SlideShow.DataProvider, SlideShow.XmlDataProvider,
{
	getData: function(dataHandler)
	{
		/// <summary>Retrieves the data asynchronously and calls the specified event handler (with the data).</summary>
		/// <param name="dataHandler">The event handler to be called after the data is retrieved.</param>
		
		var parser = new SlideShow.JsonParser({ arrays: "album,slide" });
		parser.addEventListener("parseComplete", dataHandler);
		parser.fromXml(this.options.url, true);
	}
});

/*******************************************
 * class: SlideShow.FlickrDataProvider
 *******************************************/
SlideShow.FlickrDataProvider = function(control, options)
{
	/// <summary>Provides album/slide data from Flickr.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="options">The options for the provider.</param>
	
	SlideShow.FlickrDataProvider.base.constructor.call(this, control);
	
	SlideShow.merge(this.options,
	{
		apiKey: "b45329018eafdf2a4f89ebf6fb2bf47a",
		userName: null,
		transition: "CrossFadeTransition"
	});
	
	this.setOptions(options);
	
	this.albums = {};
	this.data = { transition: this.options.transition, album: [] };
};

SlideShow.extend(SlideShow.DataProvider, SlideShow.FlickrDataProvider,
{
	getData: function(dataHandler)
	{
		/// <summary>Retrieves the data asynchronously and calls the specified event handler (with the data).</summary>
		/// <param name="dataHandler">The event handler to be called after the data is retrieved.</param>
		
		this.dataHandler = dataHandler;
		this.getUserId(this.options.userName);
	},
	
	getUserId: function(userName)
	{
		/// <summary>Gets the user ID corresponding to the specified Flickr username.</summary>
		/// <param name="userName">The Flickr username.</param>
		
		this.callFlickr("flickr.people.findByUsername&username=" + userName, SlideShow.createDelegate(this, this.onUserIdCallback));
	},
	
	getPhotosets: function(userId)
	{
		/// <summary>Gets all photosets for the specified Flickr user.</summary>
		/// <param name="userId">The Flickr user ID.</param>
		
		this.callFlickr("flickr.photosets.getList&user_id=" + userId, SlideShow.createDelegate(this, this.onPhotosetsCallback));
	},
	
	getPhotos: function(photosetId)
	{
		/// <summary>Gets all photos for the specified Flickr photoset.</summary>
		/// <param name="photosetId">The Flickr photoset ID.</param>
		
		this.callFlickr("flickr.photosets.getPhotos&photoset_id=" + photosetId, SlideShow.createDelegate(this, this.onPhotosCallback));
	},
	
	callFlickr: function(query, callbackHandler)
	{
		/// <summary>Calls a method from the Flickr API.</summary>
		/// <param name="query">The method and parameters to append to the REST endpoint URL.</param>
		/// <param name="callbackHandler">The event handler to be called when the callback event is fired.</param>
		
		var callback = SlideShow.getUniqueId("SlideShow_Callback_");
		var parser = new SlideShow.JsonParser();
		parser.addEventListener("callback", callbackHandler);
		parser.fromFeed("http://api.flickr.com/services/rest/?api_key=" + this.options.apiKey + "&format=json&jsoncallback=" + callback + "&method=" + query, callback);
	},
	
	buildAlbum: function(photoset)
	{
		/// <summary>Builds an album object from Flickr data.</summary>
		/// <param name="photoset">The photoset data.</param>
		/// <returns>The album object.</returns>
			
		var album = {};
		album.title = photoset.title._content;
		album.description = photoset.description._content;
		album.image = this.buildImageUrl(photoset.farm, photoset.server, photoset.primary, photoset.secret, "s");
		album.slide = [];
		return album;
	},
	
	buildSlide: function(photo)
	{
		/// <summary>Builds a slide object from Flickr data.</summary>
		/// <param name="photo">The photo data.</param>
		/// <returns>The slide object.</returns>
			
		var slide = {};
		slide.title = photo.title;
		slide.description = photo.description || "";
		slide.image = this.buildImageUrl(photo.farm, photo.server, photo.id, photo.secret);
		return slide;
	},
	
	buildImageUrl: function(farm, server, photoId, secret, option)
	{
		/// <summary>Builds an image URL from Flickr data -- see http://www.flickr.com/services/api/misc.urls.html for additional information.</summary>
		/// <param name="farm">The farm.</param>
		/// <param name="server">The server.</param>
		/// <param name="photoId">The photo ID.</param>
		/// <param name="secret">The secret.</param>
		/// <param name="option">The image option.</param>
		/// <returns>The image URL.</returns>
		
		return SlideShow.formatString("http://farm{0}.static.flickr.com/{1}/{2}_{3}{4}.jpg", farm, server, photoId, secret, (option) ? "_" + option : "");
	},
	
	onUserIdCallback: function(sender, e)
	{
		/// <summary>Handles the event fired when the callback function is called for the "getUserId" method.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		if (e.stat == "ok")
			this.getPhotosets(e.user.id);
		else
			throw new Error("Feed failed: " + e.message);
	},
		
	onPhotosetsCallback: function(sender, e)
	{
		/// <summary>Handles the event fired when the callback function is called for the "getPhotosets" method.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		if (e.stat == "ok")
		{
			this.albumCount = e.photosets.photoset.length;
			
			for (var i = 0, j = this.albumCount; i < j; i++)
			{
				var photoset = e.photosets.photoset[i];
				var album = this.buildAlbum(photoset);
				this.data.album.push(album);
				this.albums[photoset.id] = album;
				this.getPhotos(photoset.id);
			}
		}
		else
		{
			throw new Error("Feed failed: " + e.message);
		}
	},
	
	onPhotosCallback: function(sender, e)
	{
		/// <summary>Handles the event fired when the callback function is called for the "getPhotos" method.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		if (e.stat == "ok")
		{
			var album = this.albums[e.photoset.id];
			
			for (var i = 0, j = e.photoset.photo.length; i < j; i++)
			{
				var slide = this.buildSlide(e.photoset.photo[i]);
				album.slide.push(slide);
			}
			
			if (--this.albumCount == 0)
				this.dataHandler(this, { data: this.data });
		}
		else
		{
			throw new Error("Feed failed: " + e.message);
		}
	}
});﻿// Slide.Show, version 1.0
// Copyright © Vertigo Software, Inc.
// This source is subject to the Microsoft Public License (Ms-PL).
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/publiclicense.mspx.
// All other rights reserved.

/// <reference path="SlideShow.js" />

/*******************************************
 * class: SlideShow.NavigationTray
 *******************************************/
SlideShow.NavigationTray = function(control, parent, options)
{
	/// <summary>Controls Slide.Show navigation (including albums, slides, and fullscreen mode).</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="options">The options for the module.</param>
	
	var xaml =
		'<Canvas xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="NavigationTray" Visibility="Collapsed">' +
		'	<Canvas.RenderTransform>' +
		'		<TranslateTransform x:Name="SlideTransform" />' +
		'	</Canvas.RenderTransform>' +
		'	<Canvas.Resources>' +
		'		<Storyboard x:Name="FadeStoryboard" Storyboard.TargetName="NavigationTray" Storyboard.TargetProperty="Opacity">' +
		'			<DoubleAnimation x:Name="FadeAnimation" />' +
		'		</Storyboard>' +
		'		<Storyboard x:Name="SlideStoryboard" Storyboard.TargetName="SlideTransform" Storyboard.TargetProperty="Y">' +
		'			<DoubleAnimationUsingKeyFrames> ' +
		'				<SplineDoubleKeyFrame x:Name="SlideKeyFrame1" KeySpline="0,0 0,0" KeyTime="0:0:0" />' +
		'				<SplineDoubleKeyFrame x:Name="SlideKeyFrame2" KeySpline="0,0 0,1" />' +
		'			</DoubleAnimationUsingKeyFrames> ' +
		'		</Storyboard>' +	
		'	</Canvas.Resources>' +
		'</Canvas>';
	
	SlideShow.NavigationTray.base.constructor.call(this, control, parent, xaml);
	
	SlideShow.merge(this.options,
	{
		top: 0,
		bottom: 0,
		left: 0,
		right: 0,
		background: "#3C3C3C",
		alwaysVisibleHeight: 42,
		enableFadeAnimation: true,
		enableInitialFade: true,
		initialFadeTimout: 1000,
		initialAlbumView: false,
		fadeOpacity: 0.4,
		fadeInAnimationDuration: 0.5,
		fadeOutAnimationDuration: 0.5,
		slideAnimationDuration: 0.6,
		container:
		{
			top: 0,
			left: "50%",
			width: 500,
			height: 42
		},
		slideShowNavigation:
		{
			top: 11,
			left: 5
		},
		thumbnailViewer:
		{
			top: 3,
			left: 76,
			width: 350,
			height: 36
		},
		toggleAlbumViewButton:
		{
			top: 11,
			right: 32,
			pathData: "M0.3068684,6.6970766E-23 L4.8691305,6.6970766E-23 C5.0380702,3.6356173E-08 5.1759998,0.13792887 5.1759998,0.30690225 L5.1759998,2.6368366 C5.1759998,2.8058099 5.0380702,2.9437565 4.8691305,2.9437565 L0.3068684,2.9437565 C0.13680684,2.9437565 1.2251062E-13,2.8058099 -3.3276147E-30,2.6368366 L-3.3276147E-30,0.30690225 C1.2251062E-13,0.13792887 0.13680684,3.6356173E-08 0.3068684,6.6970766E-23 z M6.5989051,3.4025645E-07 L11.162244,3.4025645E-07 C11.331214,2.5640611E-07 11.468,0.13793494 11.468,0.30690496 L11.468,2.6368515 C11.468,2.8058217 11.331214,2.9437562 11.162244,2.9437562 L6.5989051,2.9437562 C6.4299352,2.9437562 6.2920002,2.8058217 6.2920002,2.6368515 L6.2920002,0.30690496 C6.2920002,0.13793494 6.4299352,2.5640611E-07 6.5989051,3.4025645E-07 z M0.30690471,4.0560001 L4.8690952,4.0560001 C5.0380656,4.0560001 5.1760002,4.1927856 5.1760002,4.3617556 L5.1760002,6.6928522 C5.1760002,6.861822 5.0380656,6.9997566 4.8690952,6.9997566 L0.30690471,6.9997566 C0.1367853,6.9997566 4.0662732E-08,6.861822 -3.3276153E-30,6.6928522 L-3.3276153E-30,4.3617556 C4.0662732E-08,4.1927856 0.1367853,4.0560001 0.30690471,4.0560001 z M6.5989051,4.0560005 L11.162244,4.0560005 C11.331214,4.0560005 11.468,4.192786 11.468,4.3617559 L11.468,6.6928519 C11.468,6.8618216 11.331214,6.9997562 11.162244,6.9997562 L6.5989051,6.9997562 C6.4299352,6.9997562 6.2920002,6.8618216 6.2920002,6.6928519 L6.2920002,4.3617559 C6.2920002,4.192786 6.4299352,4.0560005 6.5989051,4.0560005 z M0.30690471,8.1120001 L4.8690952,8.1120001 C5.0380656,8.1120001 5.1760002,8.2487856 5.1760002,8.4177556 L5.1760002,10.748852 C5.1760002,10.917822 5.0380656,11.055757 4.8690952,11.055757 L0.30690471,11.055757 C0.1367853,11.055757 4.0662732E-08,10.917822 -3.3276153E-30,10.748852 L-3.3276153E-30,8.4177556 C4.0662732E-08,8.2487856 0.1367853,8.1120001 0.30690471,8.1120001 z M6.5989051,8.1120005 L11.162244,8.1120005 C11.331214,8.1120005 11.468,8.248786 11.468,8.4177559 L11.468,10.748852 C11.468,10.917822 11.331214,11.055756 11.162244,11.055756 L6.5989051,11.055756 C6.4299352,11.055756 6.2920002,10.917822 6.2920002,10.748852 L6.2920002,8.4177559 C6.2920002,8.248786 6.4299352,8.1120005 6.5989051,8.1120005 Z",
			pathWidth: 11,
			pathHeight: 11
		},
		albumViewer:
		{
			top: 42
		},
		toggleFullScreenModeButton:
		{
			top: 11,
			right: 5,
			pathData: "M 7.90548,1.32341L 14.6458,1.32341L 14.6458,8.02661L 12.3811,5.78146L 8.43281,9.72004L 6.21009,7.42345L 10.1668,3.57935L 7.90548,1.32341 Z M -1.60064e-007,1.35265L 6.66667,1.35265L 6.66667,2.68583L 1.32284,2.68583L 1.32284,13.3317L 13.323,13.3317L 13.323,9.33174L 14.6562,9.33174L 14.6562,13.6585L 14.6458,13.6585L 14.6458,14.6963L -8.30604e-007,14.6963L -2.59563e-008,14.6651L -8.30604e-007,13.3317L -1.60064e-007,2.68583L -2.59563e-008,2.32351L -1.60064e-007,1.35265 Z",
			pathWidth: 12,
			pathHeight: 10
		}
	});
	
	this.setOptions(options);
	
	this.container = new SlideShow.UserControl(control, this, '<Canvas xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="NavigationContainer" Visibility="Collapsed" />', this.options.container);
	this.slideShowNavigation = new SlideShow.SlideShowNavigation(control, this.container, this.options.slideShowNavigation);
	this.thumbnailViewer = new SlideShow.ThumbnailViewer(this.control, this.container, this.options.thumbnailViewer);
	this.toggleAlbumViewButton = new SlideShow.PathButton(control, this.container, this.options.toggleAlbumViewButton);
	this.toggleFullScreenModeButton = new SlideShow.PathButton(control, this.container, this.options.toggleFullScreenModeButton);
	this.albumViewer = new SlideShow.AlbumViewer(control, this, this.options.albumViewer);
	
	this.fadeStoryboard = this.root.findName("FadeStoryboard");
	this.fadeAnimation = this.root.findName("FadeAnimation");
	this.slideTransform = this.root.findName("SlideTransform");
	this.slideStoryboard = this.root.findName("SlideStoryboard");
	this.slideKeyFrame1 = this.root.findName("SlideKeyFrame1");
	this.slideKeyFrame2 = this.root.findName("SlideKeyFrame2");
	
	// slide out events
	this.slideShowNavigation.addEventListener("previousClick", SlideShow.createDelegate(this, this.slideOut));
	this.slideShowNavigation.addEventListener("playClick", SlideShow.createDelegate(this, this.slideOut));
	this.slideShowNavigation.addEventListener("nextClick", SlideShow.createDelegate(this, this.slideOut));
	this.thumbnailViewer.addEventListener("thumbnailClick", SlideShow.createDelegate(this, this.slideOut));
	this.thumbnailViewer.thumbnailNavigation.addEventListener("previousClick", SlideShow.createDelegate(this, this.slideOut));
	this.thumbnailViewer.thumbnailNavigation.addEventListener("nextClick", SlideShow.createDelegate(this, this.slideOut));
	
	// local events
	this.toggleAlbumViewButton.addEventListener("click", SlideShow.createDelegate(this, this.onToggleAlbumViewClick));
	this.toggleFullScreenModeButton.addEventListener("click", SlideShow.createDelegate(this, this.onToggleFullScreenModeClick));
	this.albumViewer.addEventListener("albumClick", SlideShow.createDelegate(this, this.onAlbumClick));
	this.slideStoryboard.addEventListener("Completed", SlideShow.createDelegate(this, this.onSlideStoryboardCompleted));
	
	// control events
	this.control.addEventListener("modulesLoad", SlideShow.createDelegate(this, this.onControlModulesLoad));
	this.control.addEventListener("dataLoad", SlideShow.createDelegate(this, this.onControlDataLoad));
	this.control.addEventListener("fullScreenChange", SlideShow.createDelegate(this, this.onControlFullScreenChange));
	
	// fade events
	if (this.options.enableFadeAnimation)
	{
		this.root.addEventListener("MouseMove", SlideShow.createDelegate(this, this.onMouseMove));
		this.root.addEventListener("MouseEnter", SlideShow.createDelegate(this, this.onMouseEnter));
		this.root.addEventListener("MouseLeave", SlideShow.createDelegate(this, this.onMouseLeave));
	}
};

SlideShow.extend(SlideShow.UserControl, SlideShow.NavigationTray,
{
	render: function()
	{
		/// <summary>Renders the module using the current options.</summary>
		
		SlideShow.NavigationTray.base.render.call(this);
		
		this.root["Canvas.Top"] = this.root.height - this.options.alwaysVisibleHeight;
		this.toggleFullScreenModeButton.enable();
		
		if (this.options.enableFadeAnimation && this.options.enableInitialFade)
		{
			this.enableFade = true;
			window.setTimeout(SlideShow.createDelegate(this, this.fadeOut), this.options.initialFadeTimout);
		}
	},

	fadeIn: function()
	{
		/// <summary>Fades the control into view.</summary>
		
		if (this.enableFade)
		{
			var start = this.root.opacity;
			var finish = 1;
			var distance = 1 - this.options.fadeOpacity;
			var duration = (finish - start) / distance * this.options.fadeInAnimationDuration;			
			
			if (duration > 0)
			{
				this.fadeAnimation.to = finish;
				this.fadeAnimation.duration = "0:0:" + duration.toFixed(8);
				this.fadeStoryboard.begin();
			}
		}
	},
	
	fadeOut: function()
	{
		/// <summary>Fades the control out of view.</summary>
		
		if (this.enableFade && !this.isAlbumView && !this.isSlidingIn && !this.isSlidingOut)
		{
			var start = this.root.opacity;
			var finish = this.options.fadeOpacity;
			var distance = 1 - this.options.fadeOpacity;
			var duration = (start - finish) / distance * this.options.fadeOutAnimationDuration;		
			
			if (duration > 0)
			{
				this.fadeAnimation.to = finish;
				this.fadeAnimation.duration = "0:0:" + duration.toFixed(8);
				this.fadeStoryboard.begin();
			}
		}
	},	
	
	slideIn: function()
	{
		/// <summary>Slides the entire control into view.</summary>
		
		this.isAlbumView = false;
		this.isSlidingIn = true;
		this.isSlidingOut = false;
		
		this.fadeStoryboard.stop();
		this.albumViewer.pageContainer.refresh();
		this.albumViewer.root.visibility = "Visible";
		
		var start = this.slideTransform.y;
		var finish = -(this.root.height - this.options.alwaysVisibleHeight);
		var distance = this.root.height - this.options.alwaysVisibleHeight;
		var duration = (start - finish) / distance * this.options.slideAnimationDuration;
		
		if (duration > 0)
		{
			this.slideKeyFrame1.value = start;
			this.slideKeyFrame2.value = finish;
			this.slideKeyFrame2.keyTime = "0:0:" + duration.toFixed(8);
			this.slideStoryboard.begin();
		}
		
		this.thumbnailViewer.disablePreview();
	},	
	
	slideOut: function()
	{
		/// <summary>Slides the entire control out of view.</summary>
		
		if (this.isAlbumView || this.isSlidingIn)
		{
			this.isAlbumView = false;
			this.isSlidingIn = false;
			this.isSlidingOut = true;
			
			var start = this.slideTransform.y;
			var finish = 0;
			var distance = this.root.height - this.options.alwaysVisibleHeight;
			var duration = (finish - start) / distance * this.options.slideAnimationDuration;
			
			if (duration > 0)
			{
				this.slideKeyFrame1.value = start;
				this.slideKeyFrame2.value = finish;
				this.slideKeyFrame2.keyTime = "0:0:" + duration.toFixed(8);
				this.slideStoryboard.begin();
			}
			
			this.thumbnailViewer.resetPreview();
		}
	},
	
	onControlModulesLoad: function(sender, e)
	{
		/// <summary>Completes initialization after all modules have loaded.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.slideViewer = this.control.modules["SlideViewer"];
		
		if (!this.slideViewer)
			throw new Error("Expected module missing: SlideViewer");
	},
	
	onControlDataLoad: function(sender, e)
	{
		/// <summary>Handles the event fired when the configured data is loaded.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>

		if (this.control.isAlbumIndexValid(0))
		{
			this.toggleAlbumViewButton.enable();
			
			if (this.options.initialAlbumView)
				this.slideIn();
		}
	},
	
	onControlFullScreenChange: function(sender, e)
	{
		/// <summary>Handles the event fired when the control changes between embedded and full-screen mode.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		if (this.control.isFullScreenMode())
			this.toggleFullScreenModeButton.setPath("M 12.9504,9.72004L 6.21009,9.72004L 6.21009,3.01684L 8.47481,5.26199L 12.4231,1.32341L 14.6458,3.62L 10.6891,7.4641L 12.9504,9.72004 Z M 3.51898e-007,1.35265L 6.66667,1.35265L 6.66667,2.68583L 1.32284,2.68583L 1.32284,13.3317L 13.323,13.3317L 13.323,9.33174L 14.6562,9.33174L 14.6562,13.6585L 14.6458,13.6585L 14.6458,14.6963L -1.79383e-006,14.6963L 3.51898e-007,14.6651L -1.79383e-006,13.3317L 3.51898e-007,2.68583L 3.51898e-007,2.32351L 3.51898e-007,1.35265 Z");
		else
			this.toggleFullScreenModeButton.setPath("M 7.90548,1.32341L 14.6458,1.32341L 14.6458,8.02661L 12.3811,5.78146L 8.43281,9.72004L 6.21009,7.42345L 10.1668,3.57935L 7.90548,1.32341 Z M -1.60064e-007,1.35265L 6.66667,1.35265L 6.66667,2.68583L 1.32284,2.68583L 1.32284,13.3317L 13.323,13.3317L 13.323,9.33174L 14.6562,9.33174L 14.6562,13.6585L 14.6458,13.6585L 14.6458,14.6963L -8.30604e-007,14.6963L -2.59563e-008,14.6651L -8.30604e-007,13.3317L -1.60064e-007,2.68583L -2.59563e-008,2.32351L -1.60064e-007,1.35265 Z");
	},
	
	onMouseMove: function(sender, e)
	{
		/// <summary>Handles the event fired when the mouse moves within the tray.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.fadeIn();
		this.enableFade = false;
	},
	
	onMouseEnter: function(sender, e)
	{
		/// <summary>Handles the event fired when the mouse enters the tray.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.fadeIn();
		this.enableFade = false;
	},
	
	onMouseLeave: function(sender, e)
	{
		/// <summary>Handles the event fired when the mouse leaves the tray.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.enableFade = true;
		this.fadeOut();
	},
	
	onToggleAlbumViewClick: function(sender, e)
	{
		/// <summary>Handles the event fired when the albums button is clicked.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		if (this.isAlbumView || this.isSlidingIn)
			this.slideOut();
		else
			this.slideIn();
	},
	
	onToggleFullScreenModeClick: function(sender, e)
	{
		/// <summary>Handles the event fired when the full-screen button is clicked.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.control.toggleFullScreenMode();
	},
	
	onAlbumClick: function(sender, e)
	{
		/// <summary>Handles the event fired when an album button is clicked.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.thumbnailViewer.pageContainer.pageIndex = 0;
		this.thumbnailViewer.pageContainer.initializePages();
		this.thumbnailViewer.pageContainer.loadPageByOffset(0);
		this.slideOut();
	},

	onSlideStoryboardCompleted: function(sender, e)
	{
		/// <summary>Handles the event fired when an album button is clicked.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.isAlbumView = this.slideTransform.y != 0;
		this.isSlidingIn = false;
		this.isSlidingOut = false;
		this.onAlbumViewChanged();
	},
	
	onAlbumViewChanged: function()
	{
		this.albumViewer.root.visibility = this.isAlbumView ? "Visible" : "Collapsed";
		this.enableFade = !this.isAlbumView;
		
		if (this.enableFade && this.options.enableFadeAnimation)
			window.setTimeout(SlideShow.createDelegate(this, this.fadeOut), this.options.initialFadeTimout);
	},
	
	onSizeChanged: function()
	{
		/// <summary>Handles the event fired when the control is resized.</summary>
		
		SlideShow.NavigationTray.base.onSizeChanged.call(this);
		
		this.root["Canvas.Top"] = this.root.height - this.options.alwaysVisibleHeight;
		
		if (this.isAlbumView || this.isSlidingIn)
		{
			this.isAlbumView = true;
			this.isSlidingIn = false;
			this.isSlidingOut = false;
			this.slideStoryboard.stop();
			this.slideTransform.y = -this.root["Canvas.Top"];
			this.onAlbumViewChanged();
		}
		else if (this.isSlidingOut)
		{
			this.isAlbumView = false;
			this.isSlidingIn = false;
			this.isSlidingOut = false;
			this.slideStoryboard.stop();
			this.slideTransform.y = 0;
			this.onAlbumViewChanged();
		}
	}
});﻿// Slide.Show, version 1.0
// Copyright © Vertigo Software, Inc.
// This source is subject to the Microsoft Public License (Ms-PL).
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/publiclicense.mspx.
// All other rights reserved.

/// <reference path="SlideShow.js" />

/*******************************************
 * class: SlideShow.ProgressIndicator
 *******************************************/
SlideShow.ProgressIndicator = function(control, parent, xaml)
{
	/// <summary>Provides a base class for progress indicators.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="xaml">The XAML for the control.</param>
	
	SlideShow.ProgressIndicator.base.constructor.call(this, control, parent, xaml);
	
	SlideShow.merge(this.options,
	{
		top: "50%",
		left: "50%",
		width: 40,
		height: 40,
		opacity: 0,
		progressBackground: "#99000000",
		progressForeground: "#99FFFFFF",
		progressBorderColor: "White",
		progressBorderThickness: 2,
		fadeAnimationDuration: 0.3
	});
	
	this.fadeStoryboard = this.root.findName("FadeStoryboard");
	this.fadeAnimation = this.root.findName("FadeAnimation");
	
	this.control.addEventListener("modulesLoad", SlideShow.createDelegate(this, this.onControlModulesLoad));
};

SlideShow.extend(SlideShow.UserControl, SlideShow.ProgressIndicator,
{
	updateProgress: function(progress)
	{
		/// <summary>Updates the progress displayed by the control.</summary>
		/// <param name="progress">A value between 0 and 1 that specifies the current progress.</param>
		
		if (progress == 1)
			this.fadeOut();
		else if (this.root.opacity == 0)
			this.fadeIn();
	},
	
	fadeIn: function()
	{
		/// <summary>Fades the control into view.</summary>
		
		var duration = (1 - this.root.opacity) * this.options.fadeAnimationDuration;
		
		if (duration > 0)
		{
			this.fadeAnimation.to = 1;
			this.fadeAnimation.duration = "0:0:" + duration.toFixed(8);
			this.fadeStoryboard.begin();
		}
	},
	
	fadeOut: function()
	{
		/// <summary>Fades the control out of view.</summary>
		
		var duration = this.root.opacity * this.options.fadeAnimationDuration;
		
		if (duration > 0)
		{
			this.fadeAnimation.to = 0;
			this.fadeAnimation.duration = "0:0:" + duration.toFixed(8);
			this.fadeStoryboard.begin();
		}
	},
	
	onControlModulesLoad: function()
	{
		/// <summary>Handles the event fired when all modules configured for the Slide.Show control are loaded.</summary>
		
		this.slideViewer = this.control.modules["SlideViewer"];
		
		if (!this.slideViewer)
			throw new Error("Expected module missing: SlideViewer");
		
		this.slideViewer.addEventListener("slideLoading", SlideShow.createDelegate(this, this.onSlideLoading));	
		this.slideViewer.addEventListener("downloadProgressChanged", SlideShow.createDelegate(this, this.onSlideDownloadProgressChanged));
	},
	
	onSlideLoading: function(sender, e)
	{
		/// <summary>Resets the progress when the SlideViewer module fires the "slideLoading" event.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		if (e)
			this.updateProgress(0);
	},
	
	onSlideDownloadProgressChanged: function(sender, e)
	{
		/// <summary>Updates the progress when the SlideViewer module fires the "imageDownloadProgressChanged" event.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.updateProgress(e);
	}
});

/*******************************************
 * class: SlideShow.ProgressBar
 *******************************************/
SlideShow.ProgressBar = function(control, parent, options)
{
	/// <summary>Displays a progress bar while images are downloading.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="options">The options for the control.</param>

	var xaml =
		'<Canvas xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="ProgressBar" Visibility="Collapsed">' +
		'	<Canvas.Resources>' +
		'		<Storyboard x:Name="FadeStoryboard" Storyboard.TargetName="ProgressBar" Storyboard.TargetProperty="Opacity">' +
		'			<DoubleAnimation x:Name="FadeAnimation" />' +
		'		</Storyboard>' +
		'	</Canvas.Resources>' +
		'	<Rectangle x:Name="ProgressBackground" />' +
		'	<Rectangle x:Name="ProgressForeground" />' +
		'</Canvas>';
	
	SlideShow.ProgressBar.base.constructor.call(this, control, parent, xaml);
	
	SlideShow.merge(this.options,
	{
		width: 180,
		height: 4,
		progressForeground: "#CCFFFFFF",
		progressBorderThickness: 0	
	});
	
	this.setOptions(options);
	
	this.maxProgressForegroundWidth = 0;
	this.progressBackground = this.root.findName("ProgressBackground");
	this.progressForeground = this.root.findName("ProgressForeground");
};

SlideShow.extend(SlideShow.ProgressIndicator, SlideShow.ProgressBar,
{
	render: function()
	{
		/// <summary>Renders the control using the current options.</summary>
		
		SlideShow.ProgressBar.base.render.call(this);
		
		// background
		this.progressBackground.fill = this.options.progressBackground;
		this.progressBackground.stroke = this.options.progressBorderColor;
		this.progressBackground.strokeThickness = this.options.progressBorderThickness;
		
		// foreground
		this.progressForeground.fill = this.options.progressForeground;
		this.progressForeground["Canvas.Top"] = this.options.progressBorderThickness;
		this.progressForeground["Canvas.Left"] = this.options.progressBorderThickness;
	},
	
	updateProgress: function(progress)
	{
		/// <summary>Updates the progress displayed by the control.</summary>
		/// <param name="progress">A value between 0 and 1 that specifies the current progress.</param>
		
		SlideShow.ProgressBar.base.updateProgress.call(this, progress);
		this.progressForeground.width = progress * this.maxProgressForegroundWidth;
	},
	
	onSizeChanged: function()
	{
		/// <summary>Handles the event fired when the control is resized.</summary>
		
		SlideShow.ProgressBar.base.onSizeChanged.call(this);
		
		// background
		this.progressBackground.width = this.root.width;
		this.progressBackground.height = this.root.height;
		
		// foreground
		this.progressForeground.width = 0;
		this.progressForeground.height = this.root.height - this.options.progressBorderThickness * 2;
		this.maxProgressForegroundWidth = this.root.width - this.options.progressBorderThickness * 2;
	}	
});

/*******************************************
 * class: SlideShow.ProgressPie
 *******************************************/
SlideShow.ProgressPie = function(control, parent, options)
{
	/// <summary>Displays a progress pie while images are downloading.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="options">The options for the control.</param>
	
	var xaml =
		'<Canvas xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="ProgressPie" Visibility="Collapsed">' +
		'	<Canvas.Resources>' +
		'		<Storyboard x:Name="FadeStoryboard" Storyboard.TargetName="ProgressPie" Storyboard.TargetProperty="Opacity">' +
		'			<DoubleAnimation x:Name="FadeAnimation" />' +
		'		</Storyboard>' +
		'	</Canvas.Resources>' +
		'	<Ellipse x:Name="ProgressBackground" />' +
		'	<Path x:Name="ProgressForeground" />' +
		'	<Ellipse x:Name="ProgressBorder" />' +
		'</Canvas>';
	
	SlideShow.ProgressPie.base.constructor.call(this, control, parent, xaml);
	
	this.setOptions(options);
	
	this.progressBackground = this.root.findName("ProgressBackground");
	this.progressForeground = this.root.findName("ProgressForeground");
	this.progressBorder = this.root.findName("ProgressBorder");
};

SlideShow.extend(SlideShow.ProgressIndicator, SlideShow.ProgressPie,
{
	render: function()
	{
		/// <summary>Renders the control using the current options.</summary>
		
		SlideShow.ProgressPie.base.render.call(this);
		
		this.progressBackground.fill = this.options.progressBackground;
		this.progressForeground.fill = this.options.progressForeground;
		this.progressBorder.stroke = this.options.progressBorderColor;
		this.progressBorder.strokeThickness = this.options.progressBorderThickness;		
	},
	
	updateProgress: function(progress)
	{
		/// <summary>Updates the progress displayed by the control.</summary>
		/// <param name="progress">A value between 0 and 1 that specifies the current progress.</param>
		
		SlideShow.ProgressPie.base.updateProgress.call(this, progress);

		var radius = this.progressBackground.width / 2;
		var size = SlideShow.formatString("{0},{1}", radius, radius);
		var angle = 2 * Math.PI * progress;
		var direction = (progress > 0.5) ? 1 : 0;
		var startPoint = SlideShow.formatString("0,{0}", -radius);
		var endPoint = (progress < 1) ? SlideShow.formatString("{0},{1}", Math.sin(angle) * radius, Math.cos(angle) * -radius) : SlideShow.formatString("-0.05,{0}", -radius);
		
		this.progressForeground.data = SlideShow.formatString("M {0} A {1} {2} {3} 1 {4} L 0,0", startPoint, size, angle, direction, endPoint);
	},
	
	onSizeChanged: function()
	{
		/// <summary>Handles the event fired when the control is resized.</summary>
		
		SlideShow.ProgressPie.base.onSizeChanged.call(this);
		
		var diameter = Math.min(this.root.width, this.root.height);
		var radius = diameter / 2;
		var top = this.root.height / 2 - radius;
		var left = this.root.width / 2 - radius;
		
		// background
		this.progressBackground.width = diameter;
		this.progressBackground.height = diameter;
		this.progressBackground["Canvas.Top"] = top;
		this.progressBackground["Canvas.Left"] = left;
		
		// foreground
		this.progressForeground["Canvas.Top"] = top + radius;
		this.progressForeground["Canvas.Left"] = left + radius;
		
		// border
		this.progressBorder.width = diameter;
		this.progressBorder.height = diameter;
		this.progressBorder["Canvas.Top"] = top;
		this.progressBorder["Canvas.Left"] = left;
	}	
});﻿// Slide.Show, version 1.0
// Copyright © Vertigo Software, Inc.
// This source is subject to the Microsoft Public License (Ms-PL).
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/publiclicense.mspx.
// All other rights reserved.

/// <reference path="SlideShow.js" />

/*******************************************
 * class: SlideShow.SlideDescription
 *******************************************/
SlideShow.SlideDescription = function(control, parent, options)
{
	/// <summary>Displays the title and description for a slide.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="options">The options for the control.</param>
	
	var xaml =
		'<Canvas xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="SlideDescription" Visibility="Collapsed">' +
		'	<Canvas x:Name="VisibleCanvas">' +
		'		<Canvas.Resources>' +
		'			<Storyboard x:Name="SlideStoryboard" Storyboard.TargetName="SlideTransform" Storyboard.TargetProperty="Y">' +
		'				<DoubleAnimationUsingKeyFrames> ' +
		'					<SplineDoubleKeyFrame x:Name="SlideKeyFrame1" KeySpline="0,0 0,0" KeyTime="0:0:0" />' +
		'					<SplineDoubleKeyFrame x:Name="SlideKeyFrame2" KeySpline="0,0 0,1" />' +
		'				</DoubleAnimationUsingKeyFrames> ' +
		'			</Storyboard>' +
		'		</Canvas.Resources>' +
		'		<Canvas.RenderTransform>' +
		'			<TranslateTransform x:Name="SlideTransform" />' +
		'		</Canvas.RenderTransform>' +
		'		<Rectangle x:Name="Background" />' +
		'		<Rectangle x:Name="TitleRectangle" />' +
		'		<Rectangle x:Name="DescriptionRectangle" />' +
		'		<TextBlock x:Name="TitleTextBlock" TextWrapping="Wrap" />' +
		'		<TextBlock x:Name="DescriptionTextBlock" TextWrapping="Wrap" />' +
		'	</Canvas>' +
		'</Canvas>';
	
	SlideShow.SlideDescription.base.constructor.call(this, control, parent, xaml);
	
	SlideShow.merge(this.options,
	{
		top: 0,
		left: 0,
		right: 0,
		bottom: 0,
		backgroundFill: "Black",
		backgroundOpacity: 0,
		backgroundRadius: 0,		
		titleHeight: 30,
		titlePaddingTop: 5,
		titlePaddingLeft: 8,		
		titleFontFamily: "Portable User Interface",
		titleFontSize: 13,
		titleFontStretch: "Normal",
		titleFontStyle: "Normal",
		titleFontWeight: "Bold",
		titleForeground: "White",
		titleBackground: "Black",
		titleOpacity: 0.5,
		descriptionHeight: 30,
		descriptionPaddingTop: 6.5,
		descriptionPaddingLeft: 8,
		descriptionFontFamily: "Portable User Interface",
		descriptionFontSize: 11,
		descriptionFontStretch: "Normal",
		descriptionFontStyle: "Normal",
		descriptionFontWeight: "Normal",
		descriptionForeground: "White",
		descriptionBackground: "Black",
		descriptionOpacity: 0.2,
		slideAnimationDuration: 0.6,
		hideIfEmpty: false
	});
	
	this.setOptions(options);
	
	this.visibleCanvas = this.root.findName("VisibleCanvas");
	this.slideTransform = this.root.findName("SlideTransform");
	this.slideStoryboard = this.root.findName("SlideStoryboard");
	this.slideKeyFrame1 = this.root.findName("SlideKeyFrame1");
	this.slideKeyFrame2 = this.root.findName("SlideKeyFrame2");
	this.background = this.root.findName("Background");
	this.titleRectangle = this.root.findName("TitleRectangle");
	this.titleTextBlock = this.root.findName("TitleTextBlock");
	this.descriptionRectangle = this.root.findName("DescriptionRectangle");		
	this.descriptionTextBlock = this.root.findName("DescriptionTextBlock");
	
	this.root.addEventListener("MouseEnter", SlideShow.createDelegate(this, this.onMouseEnter));
	this.root.addEventListener("MouseLeave", SlideShow.createDelegate(this, this.onMouseLeave));
	this.control.addEventListener("modulesLoad", SlideShow.createDelegate(this, this.onControlModulesLoad));
};

SlideShow.extend(SlideShow.UserControl, SlideShow.SlideDescription,
{	
	render: function()
	{
		/// <summary>Renders the control using the current options.</summary>
		
		SlideShow.SlideDescription.base.render.call(this);
		
		var height = this.options.titleHeight + this.options.descriptionHeight;
		this.visibleCanvas["Canvas.Top"] = -height - this.options.top;
		this.visibleCanvas.height = height;
		
		this.background.height = this.options.titleHeight + this.options.descriptionHeight;
		this.background.fill = this.options.backgroundFill;
		this.background.opacity = this.options.backgroundOpacity;
		this.background.radiusX = this.options.backgroundRadius;
		this.background.radiusY = this.options.backgroundRadius;
		
		this.titleRectangle.height = this.options.titleHeight;
		this.titleRectangle.fill = this.options.titleBackground;
		this.titleRectangle.opacity = this.options.titleOpacity;
		
		this.titleTextBlock["Canvas.Top"] = this.options.titlePaddingTop;
		this.titleTextBlock["Canvas.Left"] = this.options.titlePaddingLeft;
		this.titleTextBlock.height = this.options.titleHeight - this.options.titlePaddingTop * 2;
		this.titleTextBlock.fontFamily = this.options.titleFontFamily;
		this.titleTextBlock.fontSize = this.options.titleFontSize;
		this.titleTextBlock.fontStretch = this.options.titleFontStretch;
		this.titleTextBlock.fontStyle = this.options.titleFontStyle;
		this.titleTextBlock.fontWeight = this.options.titleFontWeight;
		this.titleTextBlock.foreground = this.options.titleForeground;
		
		this.descriptionRectangle["Canvas.Top"] = this.options.titleHeight;
		this.descriptionRectangle.height = this.options.descriptionHeight;
		this.descriptionRectangle.fill = this.options.descriptionBackground;
		this.descriptionRectangle.opacity = this.options.descriptionOpacity;
		
		this.descriptionTextBlock["Canvas.Top"] = this.options.descriptionPaddingTop + this.options.titleHeight;
		this.descriptionTextBlock["Canvas.Left"] = this.options.descriptionPaddingLeft;
		this.descriptionTextBlock.height = this.options.descriptionHeight - this.options.descriptionPaddingTop * 2;
		this.descriptionTextBlock.fontFamily = this.options.descriptionFontFamily;
		this.descriptionTextBlock.fontSize = this.options.descriptionFontSize;
		this.descriptionTextBlock.fontStretch = this.options.descriptionFontStretch;
		this.descriptionTextBlock.fontStyle = this.options.descriptionFontStyle;
		this.descriptionTextBlock.fontWeight = this.options.descriptionFontWeight;
		this.descriptionTextBlock.foreground = this.options.descriptionForeground;
	},
	
	setTitle: function(text)
	{
		/// <summary>Sets the title.</summary>
		/// <param name="text">The title text.</param>
		
		SlideShow.addTextToBlock(this.titleTextBlock, text);
	},
	
	setDescription: function(text)
	{
		/// <summary>Sets the description.</summary>
		/// <param name="text">The description text.</param>
		
		SlideShow.addTextToBlock(this.descriptionTextBlock, text);
	},
	
	slideIn: function()
	{
		/// <summary>Slides the control into view.</summary>
		
		var start = this.slideTransform.y;
		var finish = this.visibleCanvas.height + this.options.top;
		var distance = this.visibleCanvas.height;
		var duration = (finish - start) / distance * this.options.slideAnimationDuration;
		
		if (duration > 0)
		{
			this.slideKeyFrame1.value = start;
			this.slideKeyFrame2.value = finish;
			this.slideKeyFrame2.keyTime = "0:0:" + duration.toFixed(8);
			this.slideStoryboard.begin();
		}
	},
	
	slideOut: function()
	{
		/// <summary>Slides the control out of view.</summary>
		
		var start = this.slideTransform.y;
		var finish = 0;
		var distance = this.visibleCanvas.height;
		var duration = (start - finish) / distance * this.options.slideAnimationDuration;
		
		if (duration > 0)
		{
			this.slideKeyFrame1.value = start;
			this.slideKeyFrame2.value = finish;
			this.slideKeyFrame2.keyTime = "0:0:" + duration.toFixed(8);
			this.slideStoryboard.begin();
		}
	},
	
	onControlModulesLoad: function(sender, e)
	{
		/// <summary>Wires up the necessary event handlers to the SlideViewer module.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		var slideViewer = this.control.modules["SlideViewer"];
		
		if (slideViewer)
			slideViewer.addEventListener("slideLoading", SlideShow.createDelegate(this, this.onSlideLoading));
	},
	
	onSlideLoading: function(sender, e)
	{
		/// <summary>Updates the title and description when a new slide is loaded.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.titleText = e ? e.title : "";
		this.descriptionText = e ? e.description : "";
		
		this.setTitle(this.titleText);
		this.setDescription(this.descriptionText);
		
		if (this.options.hideIfEmpty && !this.titleText && !this.descriptionText)
			this.slideOut();
	},
	
	onMouseEnter: function(sender, e)
	{
		/// <summary>Triggers the slide animation.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		if (!this.options.hideIfEmpty || this.titleText || this.descriptionText)
			this.slideIn();
	},
	
	onMouseLeave: function(sender, e)
	{
		/// <summary>Reverses the slide animation.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.slideOut();
	},
	
	onSizeChanged: function()
	{
		/// <summary>Handles the event fired when the control is resized.</summary>
		
		SlideShow.SlideDescription.base.onSizeChanged.call(this);
		
		var width = this.root.width;
		this.visibleCanvas.width = width;
		this.background.width = width;
		this.titleRectangle.width = width;
		this.descriptionRectangle.width = width;
		this.titleTextBlock.width = width - this.options.titlePaddingLeft * 2;
		this.descriptionTextBlock.width = width - this.options.descriptionPaddingLeft * 2;
		
		SlideShow.addTextToBlock(this.titleTextBlock, this.titleText);
		SlideShow.addTextToBlock(this.descriptionTextBlock, this.descriptionText);
	}
});﻿// Slide.Show, version 1.0
// Copyright © Vertigo Software, Inc.
// This source is subject to the Microsoft Public License (Ms-PL).
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/publiclicense.mspx.
// All other rights reserved.

/// <reference path="SlideShow.js" />

/*******************************************
 * class: SlideShow.SlideViewer
 *******************************************/
SlideShow.SlideViewer = function(control, parent, options)
{
	/// <summary>Displays slide images.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="options">The options for the control.</param>
	
	if (options.cacheWindowSize < 3)
		throw new Error("Invalid option: cacheWindowSize (must be at least 3)");
	
	var xaml = '<Canvas xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="SlideViewer" Visibility="Collapsed"><Canvas.Clip><RectangleGeometry x:Name="TransitionClip" /></Canvas.Clip><Image x:Name="BufferImage" Visibility="Collapsed" /></Canvas>';
	
	SlideShow.SlideViewer.base.constructor.call(this, control, parent, xaml);
	
	SlideShow.merge(this.options,
	{
		top: 0,
		left: 0,
		right: 0,
		bottom: 42,
		cacheWindowSize: 3,
		slideImage: {}
	});
	
	this.setOptions(options);
	
	this.cache = [];
	this.allowSlideChange = false;
	this.transitionNextImage = true;
	this.transitionClip = this.root.findName("TransitionClip");
	this.bufferImage = this.root.findName("BufferImage");
	
	this.control.addEventListener("dataLoad", SlideShow.createDelegate(this, this.onControlDataLoad));
	this.bufferImage.addEventListener("DownloadProgressChanged", SlideShow.createDelegate(this, this.onBufferImageDownloadProgressChanged));
	this.bufferImage.addEventListener("ImageFailed", SlideShow.createDelegate(this, this.onBufferImageDownloadFailed));
};

SlideShow.extend(SlideShow.UserControl, SlideShow.SlideViewer,
{	
	loadCache: function()
	{
		/// <summary>Clears the image cache and loads it with new images.</summary>
		
		for (var i = this.cache.length - 1; i >= 0; i--)
			this.removeCacheImage(i);
		
		this.cache = [];
		this.cacheSize = Math.floor(this.options.cacheWindowSize / 2);
	
		for (var offset = -this.cacheSize; offset < this.cacheSize; offset++)
		{
			var dataIndex = this.getDataIndexByOffset(offset);
			
			if (dataIndex != null)
				this.cache.push(this.addCacheImage(dataIndex));
		}
	},
	
	addCacheImage: function(index)
	{
		/// <summary>Adds an image to the cache for the specified index in the current album.</summary>
		/// <param name="index">The index of the image to add.</param>
		/// <returns>The added image.</returns>
		
		var image;
		
		if (this.control.isSlideIndexValid(this.currentAlbumIndex, index))
		{
			image = this.control.host.content.createFromXaml('<Image Visibility="Collapsed" />');
			image.source = this.control.data.album[this.currentAlbumIndex].slide[index].image;
			this.root.children.add(image);
		}
		
		return image;
	},
	
	removeCacheImage: function(index)
	{
		/// <summary>Removes the image at the specified index from the cache.</summary>
		/// <param name="index">The index of the image to remove.</param>
		
		this.root.children.remove(this.cache[index]);
		this.cache[index] = null;
		this.cache.splice(index, 1);
	},

	getDataIndexByOffset: function(offset)
	{
		/// <summary>Gets the slide index using an offset from the current slide index.</summary>
		/// <param name="offset">A positive or negative offset from the current slide index.</param>
		/// <returns>The slide index.</returns>
		
		var count = 0;
		
		if (this.control.isSlideIndexValid(this.currentAlbumIndex, 0))
			count = this.control.data.album[this.currentAlbumIndex].slide.length;
		
		if (count > 0)
		{
			var index = this.currentSlideIndex + offset;
			var mod = index % count;
			return (mod < 0) ? count + mod : mod;
		}
		
		return null;
	},
	
	loadImageByOffset: function(offset, useTransition)
	{
		/// <summary>Loads an image using an offset from the current slide index.</summary>
		/// <param name="offset">A positive or negative offset from the current slide index.</param>
		/// <param name="useTransition">A value indicating whether or not a transition should be used.</param>
		
		this.currentSlideIndex = this.getDataIndexByOffset(offset);
		this.loadImage(useTransition);
	},
	
	loadImage: function(transitionNextImage)
	{
		/// <summary>Loads the image at the current slide index and fires the "slideLoading" event.</summary>
		/// <param name="transitionNextImage">Specifies whether or not to use a transition for the slide.</param>
		
		this.allowSlideChange = true;
		this.transitionNextImage = transitionNextImage;
		
		var bufferSource = this.getCurrentImageSource();
		
		if (bufferSource)
			this.bufferImage.source = bufferSource;
		else
			this.changeImage("");
		
		this.fireEvent("slideLoading", this.getCurrentSlideData());
	},
	
	getCurrentImageSource: function()
	{
		/// <summary>Gets the source for the current image.</summary>
		/// <returns>The current image source.</returns>
		
		this.adjustCacheWindow();
		
		if (this.cache.length > 0)
			return this.cache[this.cacheSize].source;
		
		return "";
	},
	
	getCurrentSlideData: function()
	{
		/// <summary>Gets the data for the current slide.</summary>
		/// <returns>The current slide data.</returns>
		
		if (this.control.isSlideIndexValid(this.currentAlbumIndex, this.currentSlideIndex))
			return this.control.data.album[this.currentAlbumIndex].slide[this.currentSlideIndex];
		
		return null;
	},
	
	adjustCacheWindow: function()
	{
		/// <summary>Adjusts the cache window to the new slide index.</summary>
		
		var currentSlide = this.getCurrentSlideData();
		
		if (currentSlide)
		{
			var cacheIndex;
			
			for (var i = 0, j = this.cache.length; i < j; i++)
			{
				if (this.cache[i].source == currentSlide.image)
				{
					cacheIndex = i;
					break;
				}
			}

			if (cacheIndex == null)
			{
				this.loadCache();
			}
			else
			{
				var dataIndex, image;
				var offset = cacheIndex - this.cacheSize;
				var absOffset = Math.abs(offset);
				
				if (offset < 0)
				{
					for (var k = 1; k <= absOffset; k++)
					{
						dataIndex = this.getDataIndexByOffset(0 - k);
						image = this.addCacheImage(dataIndex);
						this.cache.unshift(image);
						this.removeCacheImage(this.cache.length - 1);
					}
				}
				else if (offset > 0)
				{
					for (var l = 1; l <= absOffset; l++)
					{
						dataIndex = this.getDataIndexByOffset(l);
						image = this.addCacheImage(dataIndex);
						this.cache.push(image);
						this.removeCacheImage(0);
					}
				}
			}
		}
		else
		{
			this.loadCache();
		}
	},
	
	changeImage: function(toImageSource)
	{
		/// <summary>Changes the image to the new slide.</summary>
		/// <param name="toImageSource">Specifies the source for the next slide image.</param>
		
		this.initializeSlideImages();
		
		if (this.slideImage1.root.visibility == "Visible")
		{
			this.fromImage = this.slideImage1;
			this.toImage = this.slideImage2;
		}
		else
		{
			this.fromImage = this.slideImage2;
			this.toImage = this.slideImage1;
		}
		
		this.toImage.setSource(toImageSource);
		
		this.currentTransition = null;
		
		if (this.transitionNextImage)
		{
			var transitionData = this.control.getSlideTransitionData(this.currentAlbumIndex, this.currentSlideIndex);
			this.currentTransition = this.control.createObjectInstanceFromConfig(transitionData);
		}
		else
		{
			this.currentTransition = this.control.createObjectInstanceFromConfig({ type: "NoTransition" });
			this.transitionNextImage = true;
		}
		
		this.currentTransition.addEventListener("complete", SlideShow.createDelegate(this, this.onSlideChange));
		this.currentTransition.begin(this.fromImage, this.toImage);
	},
	
	initializeSlideImages: function()
	{
		/// <summary>Initializes slide images for use in the next transition.</summary>
	
		if (this.slideImage1 == null || this.slideImage2 == null)
		{
			this.slideImage1 = new SlideShow.SlideImage(this.control, this, this.options.slideImage);
			this.slideImage2 = new SlideShow.SlideImage(this.control, this, this.options.slideImage);
			this.slideImage1.options.visibility = "Collapsed";
		}
		else
		{
			var slideImage1 = new SlideShow.SlideImage(this.control, this, this.options.slideImage);
			slideImage1.options.visibility = this.slideImage1.root.visibility;
			slideImage1.setSource(this.slideImage1.image.source);
			
			var slideImage2 = new SlideShow.SlideImage(this.control, this, this.options.slideImage);
			slideImage2.options.visibility = this.slideImage2.root.visibility;
			slideImage2.setSource(this.slideImage2.image.source);
			
			this.slideImage1.dispose();
			this.slideImage2.dispose();
			
			this.slideImage1 = slideImage1;
			this.slideImage2 = slideImage2;
		}
		
		this.slideImage1.render();
		this.slideImage2.render();
	},
	
	onControlDataLoad: function(sender, e)
	{
		/// <summary>Initializes the current album and slide indexes and loads the first available slide.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		if (this.control.isAlbumIndexValid(0))
		{
			if (this.control.data.startalbumindex || this.control.data.startslideindex)
			{
				this.currentAlbumIndex = this.control.data.startalbumindex || 0;
				this.currentSlideIndex = this.control.data.startslideindex || 0;
				this.loadImage(true);
			}
			else
			{
				for (var i = 0, j = this.control.data.album.length; i < j; i++)
				{
					var slides = this.control.data.album[i].slide;
					
					if (slides && slides.length > 0)
					{
						this.currentAlbumIndex = i;
						this.currentSlideIndex = 0;
						this.loadImage(true);
						break;
					}
				}
			}
		}
	},
	
	onBufferImageDownloadProgressChanged: function(sender, e)
	{
		/// <summary>Handles the event fired while a slide image is downloading.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		if (sender.downloadProgress == 1 && this.allowSlideChange)
		{
			this.allowSlideChange = false;
			this.changeImage(sender.source);
		}
		
		this.fireEvent("downloadProgressChanged", sender.downloadProgress);
	},
	
	onBufferImageDownloadFailed: function(sender, e)
	{
		/// <summary>Handles the event fired when a slide image failed to download.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		throw new Error("Image download failed: " + sender.source);
	},
	
	onSlideChange: function(sender, e)
	{
		/// <summary>Fires the slideChange event when a transition between slides is complete.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.fireEvent("slideChange", this.getCurrentSlideData());
	},
	
	onSizeChanged: function()
	{
		/// <summary>Handles the event fired when the control is resized.</summary>
		
		SlideShow.SlideViewer.base.onSizeChanged.call(this);
		this.transitionClip.rect = this.root["Canvas.Left"] + "," + this.root["Canvas.Top"] + "," + this.root.width + "," + this.root.height;
	}
});

/*******************************************
 * class: SlideShow.SlideImage
 *******************************************/
SlideShow.SlideImage = function(control, parent, options)
{
	/// <summary>Displays slide images within the SlideViewer module.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="options">The options for the control.</param>
	
	var xaml = '<Canvas xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="SlideImage" Visibility="Collapsed"><Image x:Name="Image" /></Canvas>';
	
	SlideShow.SlideImage.base.constructor.call(this, control, parent, xaml);
	
	SlideShow.merge(this.options,
	{
		top: 0,
		left: 0,
		right: 0,
		bottom: 0,
		stretch: "Uniform"
	});
	
	this.setOptions(options);
	
	this.image = this.root.findName("Image");
};

SlideShow.extend(SlideShow.UserControl, SlideShow.SlideImage,
{
	render: function()
	{
		/// <summary>Renders the image using the current options.</summary>
		
		SlideShow.SlideImage.base.render.call(this);
		
		this.image.stretch = this.options.stretch;
	},
	
	setSource: function(source)
	{
		/// <summary>Sets the image source.</summary>
		/// <param name="source">The image source.</param>
		
		this.image.source = source;
	},
	
	onSizeChanged: function()
	{
		/// <summary>Handles the event fired when the control is resized.</summary>
		
		SlideShow.SlideImage.base.onSizeChanged.call(this);
		
		this.image.width = this.root.width;
		this.image.height = this.root.height;
		
		this.fireEvent("sizeChange");
	}
});

/*******************************************
 * class: SlideShow.SlideShowNavigation
 *******************************************/
SlideShow.SlideShowNavigation = function(control, parent, options)
{
	/// <summary>Provides basic slideshow navigation.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="options">The options for this control.</param>
	
	var xaml =
		'<Canvas xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="SlideShowNavigation" Visibility="Collapsed">' +
		'	<Rectangle x:Name="Background">' +
		'		<Rectangle.Fill>' +
		'			<LinearGradientBrush StartPoint="0.477254,1.16548" EndPoint="0.477254,0.0426189">' +
		'				<LinearGradientBrush.GradientStops>' +
		'					<GradientStop x:Name="BackgroundColor1" Offset="0.232877" />' +
		'					<GradientStop x:Name="BackgroundColor2" Offset="0.987288" />' +
		'				</LinearGradientBrush.GradientStops>' +
		'			</LinearGradientBrush>' +
		'		</Rectangle.Fill>' +
		'	</Rectangle>' +
		'</Canvas>';
	
	SlideShow.SlideShowNavigation.base.constructor.call(this, control, parent, xaml);
	
	SlideShow.merge(this.options,
	{
		width: 66,
		height: 20,
		radius: 2,
		stroke: "#7FF19B77",
		strokeThickness: 1,
		backgroundColor1: "#7F2D12",
		backgroundColor2: "#E9A16B",
		playTimerInterval: 3000,
		enablePlayOnLoad: true,
		loopAlbum: false,
		playButton:
		{
			left: "50%",
			radius: 0,
			strokeThickness: 0,
			backgroundColor1: "Transparent",
			backgroundColor2: "Transparent",
			pathData: "M 100.048,736.889L 98.5482,736.889C 97.9959,736.889 97.5482,736.441 97.5482,735.889L 97.5482,727.889C 97.5482,727.337 97.9959,726.889 98.5482,726.889L 100.048,726.889C 100.6,726.889 101.048,727.337 101.048,727.889L 101.048,735.889C 101.048,736.441 100.6,736.889 100.048,736.889 Z M 106.922,736.889L 105.422,736.889C 104.87,736.889 104.422,736.441 104.422,735.889L 104.422,727.889C 104.422,727.337 104.87,726.889 105.422,726.889L 106.922,726.889C 107.475,726.889 107.922,727.337 107.922,727.889L 107.922,735.889C 107.922,736.441 107.475,736.889 106.922,736.889 Z",
			pathWidth: 10,
			pathHeight: 12,
			pathStretch: "Fill",
			pathFill: "#F2C29F",
			pathFillDisabled: "#D3895A"
		},
		previousButton:
		{
			radius: 0,
			strokeThickness: 0,
			backgroundColor1: "Transparent",
			backgroundColor2: "Transparent",
			pathData: "F1 M6.0000005,1.473075E-06 L6.0000005,10.000002 5.9604696E-07,5.0000014 6.0000005,1.473075E-06 z M-1.3709068E-06,4.0019114E-07 L-1.3709068E-06,10.000006 -6.0000029,5.0000029 -1.3709068E-06,4.0019114E-07 Z",
			pathWidth: 10,
			pathHeight: 8,
			pathFill: "#F2C29F",
			pathFillDisabled: "#D3895A"
		},
		nextButton:
		{
			right: 0,
			radius: 0,
			strokeThickness: 0,
			backgroundColor1: "Transparent",
			backgroundColor2: "Transparent",
			pathData: "F1 M-5.9999976,1.0552602E-05 L-1.9073478E-06,4.9999938 -1.9073478E-06,1.8062785E-05 5.9999981,5.0000033 -1.9073478E-06,10.000018 -1.9073478E-06,5.0000024 -5.9999976,9.9999857 -5.9999976,1.0552602E-05 Z",
			pathWidth: 10,
			pathHeight: 8,
			pathFill: "#F2C29F",
			pathFillDisabled: "#D3895A"
		}
	});
	
	this.setOptions(options);
	
	if (!this.options.enablePlayOnLoad)
		this.options.playButton.pathData = "F1 M 101.447,284.834L 101.447,274.714L 106.906,279.774L 101.447,284.834 Z";
	
	this.playButton = new SlideShow.PathButton(this.control, this, this.options.playButton);
	
	if (this.options.enablePreviousSlide)
	{
		this.previousButton = new SlideShow.PathButton(this.control, this, this.options.previousButton);
		this.previousButton.addEventListener("click", SlideShow.createDelegate(this, this.onPreviousClick));
	}
	
	if (this.options.enableNextSlide)
	{
		this.nextButton = new SlideShow.PathButton(this.control, this, this.options.nextButton);
		this.nextButton.addEventListener("click", SlideShow.createDelegate(this, this.onNextClick));
	}
	
	this.mode = "Pause";
	this.background = this.root.findName("Background");
	this.backgroundColor1 = this.root.findName("BackgroundColor1");
	this.backgroundColor2 = this.root.findName("BackgroundColor2");
	
	this.playButton.addEventListener("click", SlideShow.createDelegate(this, this.onPlayClick));
	this.control.root.addEventListener("KeyUp", SlideShow.createDelegate(this, this.onControlKeyPressed));
	this.control.addEventListener("dataLoad", SlideShow.createDelegate(this, this.onControlDataLoad));	
};

SlideShow.extend(SlideShow.SlideNavigation, SlideShow.SlideShowNavigation,
{
	render: function()
	{
		/// <summary>Renders the control using the current options.</summary>

		SlideShow.SlideShowNavigation.base.render.call(this);
		
		this.background.width = this.options.width;
		this.background.height = this.options.height;
		this.background.radiusX = this.options.radius;
		this.background.radiusY = this.options.radius;
		this.background.stroke = this.options.stroke;
		this.background.strokeThickness = this.options.strokeThickness;
		this.backgroundColor1.color = this.options.backgroundColor1;
		this.backgroundColor2.color = this.options.backgroundColor2;
	},
	
	play: function()
	{
		/// <summary>Plays the slideshow.</summary>
		
		if (this.mode != "Play")
		{
			this.mode = "Play";
			this.startPlayTimer();
			this.playButton.setPath("M 100.048,736.889L 98.5482,736.889C 97.9959,736.889 97.5482,736.441 97.5482,735.889L 97.5482,727.889C 97.5482,727.337 97.9959,726.889 98.5482,726.889L 100.048,726.889C 100.6,726.889 101.048,727.337 101.048,727.889L 101.048,735.889C 101.048,736.441 100.6,736.889 100.048,736.889 Z M 106.922,736.889L 105.422,736.889C 104.87,736.889 104.422,736.441 104.422,735.889L 104.422,727.889C 104.422,727.337 104.87,726.889 105.422,726.889L 106.922,726.889C 107.475,726.889 107.922,727.337 107.922,727.889L 107.922,735.889C 107.922,736.441 107.475,736.889 106.922,736.889 Z", 12); // FIXME: 12?
		}
	},
	
	pause: function()
	{
		/// <summary>Pauses the slideshow.</summary>
		
		if (this.mode != "Pause")
		{
			this.mode = "Pause";
			this.stopPlayTimer();
			this.playButton.setPath("F1 M 101.447,284.834L 101.447,274.714L 106.906,279.774L 101.447,284.834 Z");
		}
	},
	
	togglePlayMode: function()
	{
		/// <summary>Toggles the slideshow between play and pause mode.</summary>
	
		if (this.mode == "Pause")
			this.play();
		else
			this.pause();
	},
	
	startPlayTimer: function()
	{
		/// <summary>Starts the play timer.</summary>
		
		if (!this.playTimerId)
			this.playTimerId = window.setTimeout(SlideShow.createDelegate(this, this.showNextSlide), this.options.playTimerInterval);
	},
	
	stopPlayTimer: function()
	{
		/// <summary>Stops the play timer.</summary>
		
		if (this.playTimerId)
		{
			window.clearTimeout(this.playTimerId);
			this.playTimerId = null;
		}
	},
	
	onPreviousClick: function(sender, e) 
	{
		/// <summary>Handles the event fired when the previous button is clicked.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.showPreviousSlide();
		this.fireEvent("previousClick");
	},
	
	onPlayClick: function(sender, e)
	{
		/// <summary>Handles the event fired when the play button is clicked.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.togglePlayMode();
		this.fireEvent("playClick");
	},
	
	onNextClick: function(sender, e) 
	{
		/// <summary>Handles the event fired when the next button is clicked.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.showNextSlide();
		this.fireEvent("nextClick");
	},
	
	onControlModulesLoad: function(sender, e)
	{
		/// <summary>Completes initialization after all modules have loaded.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		SlideShow.SlideShowNavigation.base.onControlModulesLoad.call(this, sender, e);
		this.slideViewer.addEventListener("slideLoading", SlideShow.createDelegate(this, this.onSlideLoading));
		this.slideViewer.addEventListener("slideChange", SlideShow.createDelegate(this, this.onSlideChange));
	},
	
	onControlDataLoad: function(sender, e)
	{
		/// <summary>Completes initialization after all data has loaded.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>

		if (this.options.enablePlayOnLoad)
			this.play();

		this.slideViewer.loadImageByOffset(0, this.options.enableTransitionOnNext);
	},
	
	onSlideLoading: function(sender, e)
	{
		/// <summary>Handles the event fired when the next slide is loading. Resets the play timeout while the next slide downloads.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		if (this.mode == "Play")
			this.stopPlayTimer();
		
		if (this.options.enablePreviousSlide)
		{
			if (this.slideExistsByOffset(-1))
				this.previousButton.enable();
			else
				this.previousButton.disable();
		}

		if (this.options.enableNextSlide)
		{
			if (this.slideExistsByOffset(1))
				this.nextButton.enable();
			else
				this.nextButton.disable();
		}
		
		if (this.slideExistsByOffset(1))
		{
			this.playButton.enable();
		}
		else
		{
			this.pause();
			this.playButton.disable();
		}
	},
	
	onSlideChange: function(sender, e) 
	{
		/// <summary>Handles the event fired when the current slide changes.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		if (this.mode == "Play")
			this.startPlayTimer();
	},
	
	onControlKeyPressed: function(sender, e)	
	{
		/// <summary>Handles the event fired when a key is pressed.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		switch (e.key)
		{
			case 9:
				this.togglePlayMode();
				break;
				
			case 14:
				this.showPreviousSlide();
				break;
				
			case 16:
				this.showNextSlide();
				break;
		}
	}
});﻿// Slide.Show, version 1.1
// Copyright © Vertigo Software, Inc.
// This source is subject to the Microsoft Public License (Ms-PL).
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/publiclicense.mspx.
// All other rights reserved.

/// <reference path="SlideShow.js" />

/*******************************************
 * class: SlideShow.ThumbnailViewer
 *******************************************/
SlideShow.ThumbnailViewer = function(control, parent, options)
{
	/// <summary>Displays pages of thumbnails to choose from.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="options">The options for the control.</param>
	
	var xaml =
		'<Canvas xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="ThumbnailViewer" Visibility="Collapsed">' +
		'	<Rectangle x:Name="viewerBackground" />' +
		'</Canvas>';
	
	SlideShow.ThumbnailViewer.base.constructor.call(this, control, parent, xaml);
	
	// FIXME: incorrect calculations require strange values for top, left, etc. in page container below
	SlideShow.merge(this.options,
	{
		top: 0,
		left: 0,
		bottom: 0,
		right: 0,
		enablePreview: true,
		viewerBackground: {},
		pageContainer:
		{
			top: 3,
			left: 19,
			right: 19,
			bottom: -5,
			padding: 0,
			spacing: 3,
			itemWidth: 30,
			itemHeight: 30,
			zIndex: 1,
			cursor: "Hand"
		},
		thumbnailNavigation:
		{
			height: 30
		},
		thumbnailPreview: {},
		thumbnailButton: {}			
	});
	
	this.setOptions(options);
	
	this.previewEnabled = this.options.enablePreview;
	this.viewerBackground = new SlideShow.ViewerBackground(this.control, this, this.options.viewerBackground);
	this.pageContainer = new SlideShow.PageContainer(this.control, this, this.options.pageContainer);
	this.thumbnailNavigation = new SlideShow.ThumbnailNavigation(this.control, this, this.options.thumbnailNavigation);
	this.thumbnailPreview = new SlideShow.ThumbnailPreview(this.control, this, this.options.thumbnailPreview);
	
	this.control.addEventListener("modulesLoad", SlideShow.createDelegate(this, this.onControlModulesLoad));
	this.control.addEventListener("dataLoad", SlideShow.createDelegate(this, this.onControlDataLoad));
};

SlideShow.extend(SlideShow.UserControl, SlideShow.ThumbnailViewer,
{
	getItems: function(fromIndex, count)
	{
		/// <summary>Retrieves a page of items from the overall collection of slides for the current album.</summary>
		/// <param name="fromIndex">The first index to retrieve from the current album's slides.</param>
		/// <param name="count">The number of items to retrieve overall.</param>
		
		var currentPageItems = [];
		this.currentItemListenerTokens = [];
		
		for (k = 0, l = this.currentItemListenerTokens.length; k < l; k++)
			this.slideViewer.removeEventListener(this.currentItemListenerTokens[k]);
		
		for (var i = fromIndex, j = fromIndex + count; i < j; i++)
		{
			if (this.control.data.album[this.slideViewer.currentAlbumIndex].slide[i])
			{
				var item = new SlideShow.ThumbnailButton(this.control, null, this.control.data.album[this.slideViewer.currentAlbumIndex].slide[i], this.options.thumbnailButton);
				
				item.addEventListener("click", SlideShow.createDelegate(this, this.onThumbnailClick));
				this.currentItemListenerTokens.push(this.slideViewer.addEventListener("slideLoading", SlideShow.createDelegate(item, item.onSlideLoading)));
				item.onSlideLoading(null, this.control.data.album[this.slideViewer.currentAlbumIndex].slide[this.slideViewer.currentSlideIndex]);
				
				if (this.options.enablePreview)
				{
					item.addEventListener("mouseEnter", SlideShow.createDelegate(this, this.onThumbnailEnter));
					item.addEventListener("mouseLeave", SlideShow.createDelegate(this, this.onThumbnailLeave));
				}
				currentPageItems.push(item);
			}
		}
		
		return currentPageItems;
	},
	
	getItemCount: function()
	{
		/// <summary>Retrieves the total number of items in the current album's slide collection.</summary>
	
		if (this.control.data && this.control.data.album && this.control.data.album[this.slideViewer.currentAlbumIndex].slide)
			return this.control.data.album[this.slideViewer.currentAlbumIndex].slide.length;
		
		return 0;
	},
	
	disablePreview: function()
	{
		/// <summary>Disables the thumbnail preview feature.</summary>
		
		this.previewEnabled = false;
	},
	
	resetPreview: function()
	{
		/// <summary>Resets the thumbnail preview feature according to originally specified options.</summary>
	
		this.previewEnabled = this.options.enablePreview;
	},
	
	onControlDataLoad: function(sender, e)
	{
		/// <summary>Handles the event fired when the configured data is loaded.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
	
		if (this.control.data)
			this.pageContainer.loadPageByOffset(0);
	},
	
	onControlModulesLoad: function(sender, e)
	{
		/// <summary>Completes initialization after all modules have loaded.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.slideViewer = this.control.modules["SlideViewer"];
		
		if (!this.slideViewer)
			throw new Error("Expected module missing: SlideViewer");
	},
	
	onThumbnailEnter: function(sender, isSelected)
	{
		/// <summary>Handles the event fired when the mouse enters a thumbnail.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="isSelected">Event arguments that indicate if the thumbnail corresponds to the current image.</param>
		
		if (!isSelected && this.previewEnabled)
		{
			var top = sender.root["Canvas.Top"] - this.thumbnailPreview.root.height - this.thumbnailPreview.arrow.height + 3;
			var left = this.pageContainer.root["Canvas.Left"] + this.pageContainer.currentPage["Canvas.Left"] + sender.root["Canvas.Left"] + sender.root.width / 2 - this.thumbnailPreview.root.width / 2;
			
			this.thumbnailPreview.setOptions({ top: top, left: left });
			this.thumbnailPreview.reposition();			
			this.thumbnailPreview.show(sender.getThumbnailImageSource());
		}
	},
	
	onThumbnailLeave: function(sender, isSelected)
	{
		/// <summary>Handles the event fired when the mouse leaves a thumbnail.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="isSelected">Event arguments that indicate if the thumbnail corresponds to the current image.</param>
		
		this.thumbnailPreview.hide();
	},
	
	onThumbnailClick: function(sender, e)
	{
		/// <summary>Changes the currently displayed slide to the selected image and bubbles up an event to indicate that a thumbnail has been selected.</summary>
		/// <param name="sender">The ThumbnailButton instance that was clicked.</param>
		/// <param name="e">The slide data associated with the selected instance.</param>
		
		for (var i = 0, j = this.control.data.album[this.slideViewer.currentAlbumIndex].slide.length; i < j; i++)
		{
			if (this.control.data.album[this.slideViewer.currentAlbumIndex].slide[i] == e && this.slideViewer.currentSlideIndex != i)
			{
				this.slideViewer.currentSlideIndex = i;
				this.slideViewer.loadImageByOffset(0, true);
			}
		}
		
		if (this.previewEnabled)
			this.thumbnailPreview.hide();
		
		this.fireEvent("thumbnailClick", e);
	}
});

/*******************************************
 * class: SlideShow.ThumbnailNavigation
 *******************************************/
SlideShow.ThumbnailNavigation = function(control, parent, options)
{
	/// <summary>Provides navigation for ThumbnailViewer's page container.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="pageContainer">The page container.</param>
	/// <param name="options">The options for the control.</param>
	
	var xaml = '<Canvas xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="ThumbnailNavigation" Visibility="Collapsed" />';
	
	SlideShow.ThumbnailNavigation.base.constructor.call(this, control, parent, xaml);
	
	SlideShow.merge(this.options,
	{
		left: 0,
		right: 0,
		height: 20,
		foreground: "#777",
		enablePreviousSlide: false,
		enableNextSlide: false,
		previousPageButton: // FIXME: naming not consistent with AlbumNavigation
		{
			left: 0,
			height: 36,
			radius: 0,
			strokeThickness: 0,
			backgroundColor1: "Transparent",
			backgroundColor2: "Transparent",
			pathData: "M0.049999999,0.81200001 C0.049999999,0.39115903 0.39115902,0.049999999 0.81199999,0.049999999 L2.711,0.049999999 C3.1318409,0.049999999 3.473,0.39115903 3.473,0.81200001 L3.473,9.1880002 C3.473,9.6088412 3.1318409,9.9500002 2.711,9.9500002 L0.81199999,9.9500002 C0.39115902,9.9500002 0.049999999,9.6088412 0.049999999,9.1880002 z M-3.3603748,0.016 L-9.344,4.9998124 -3.3599998,9.9840002 Z",
			pathWidth: 10,
			pathHeight: 8,
			pathFill: "#777",
			pathFillDisabled: "#333"
		},
		nextPageButton:
		{
			right: 0,
			height: 36,
			radius: 0,
			strokeThickness: 0,
			backgroundColor1: "Transparent",
			backgroundColor2: "Transparent", 
			pathData: "M0.049999999,0.81200001 C0.049999999,0.39115903 0.39115902,0.049999999 0.81199999,0.049999999 L2.711,0.049999999 C3.1318409,0.049999999 3.473,0.39115903 3.473,0.81200001 L3.473,9.1880002 C3.473,9.6088412 3.1318409,9.9500002 2.711,9.9500002 L0.81199999,9.9500002 C0.39115902,9.9500002 0.049999999,9.6088412 0.049999999,9.1880002 z M6.9063742,0.016 L12.875,4.9998124 6.8910001,9.9840002 Z",
			pathWidth: 10,
			pathHeight: 8,
			pathFill: "#777",
			pathFillDisabled: "#333"
		},
		previousButton:
		{
			top: "50%",
			left: 22,
			radius: 0,
			strokeThickness: 0,
			backgroundColor1: "Transparent",
			backgroundColor2: "Transparent",
			pathData: "M256.9375,633.46875L256.9375,636.43725 267.2495,636.43725 267.2495,633.437255242651 Z",
			pathWidth: 10,
			pathHeight: 8,
			pathFill: "#777"
		},
		nextButton:
		{
			top: "50%",
			right: 22,
			radius: 0,
			strokeThickness: 0,
			backgroundColor1: "Transparent",
			backgroundColor2: "Transparent",
			pathData: "M612.21875,632.78125L612.21875,636.37525 608.56225,636.37525 608.56225,639.281489402504 612.21849996621,639.281489402504 612.21849996621,642.938131479117 615.093888547801,642.938131479117 615.093888547801,639.344555954334 618.8127229171,639.344555954334 618.8127229171,636.375554460764 615.093316965987,636.375554460764 615.12456845465,632.781500021178 Z",
			pathWidth: 10,
			pathHeight: 8,
			pathFill: "#777"
		}
	});
	
	this.setOptions(options);
	
	this.previousPageButton = new SlideShow.PathButton(this.control, this, this.options.previousPageButton);
	this.nextPageButton = new SlideShow.PathButton(this.control, this, this.options.nextPageButton);
	
	if (this.options.enablePreviousSlide)
	{
		this.previousButton = new SlideShow.PathButton(this.control, this, this.options.previousButton);
		this.previousButton.addEventListener("click", SlideShow.createDelegate(this, this.onPreviousClick));
	}
	
	if (this.options.enableNextSlide)
	{
		this.nextButton = new SlideShow.PathButton(this.control, this, this.options.nextButton);
		this.nextButton.addEventListener("click", SlideShow.createDelegate(this, this.onNextClick));
	}
	
	this.previousPageButton.addEventListener("click", SlideShow.createDelegate(this, this.onPreviousPageClick));
	this.nextPageButton.addEventListener("click", SlideShow.createDelegate(this, this.onNextPageClick));
	parent.pageContainer.addEventListener("pageLoad", SlideShow.createDelegate(this, this.onPageLoad));	
};

SlideShow.extend(SlideShow.PageNavigation, SlideShow.ThumbnailNavigation,
{
	onControlModulesLoad: function(sender, e)
	{
		/// <summary>Completes initialization after all modules have loaded.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		SlideShow.ThumbnailNavigation.base.onControlModulesLoad.call(this, sender, e);
		
		if (this.options.enablePreviousSlide || this.options.enableNextSlide)
			this.slideViewer.addEventListener("slideLoading", SlideShow.createDelegate(this, this.onSlideLoading));
	},
	
	onPreviousPageClick: function(sender, e) 
	{
		/// <summary>Handles the event fired when the previous page button is clicked.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.showPreviousPage();
	},
	
	onNextPageClick: function(sender, e) 
	{
		/// <summary>Handles the event fired when the next page button is clicked.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.showNextPage();
	},
	
	onPreviousClick: function(sender, e) 
	{
		/// <summary>Handles the event fired when the previous button is clicked.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.showPreviousSlide();
		this.fireEvent("previousClick");
	},
	
	onNextClick: function(sender, e) 
	{
		/// <summary>Handles the event fired when the next button is clicked.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.showNextSlide();
		this.fireEvent("nextClick");
	},
	
	onPageLoad: function(sender, e) 
	{
		/// <summary>Handles the event fired when a page is loaded in the page container.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		var pageIndex = this.parent.pageContainer.pageIndex;
		var pageCount = this.parent.pageContainer.pageCount;
		
		if (pageIndex > 0)
			this.previousPageButton.enable();
		else
			this.previousPageButton.disable();
		
		if (pageIndex < pageCount - 1)
			this.nextPageButton.enable();
		else
			this.nextPageButton.disable();
	},
	
	onSlideLoading: function(sender, e)
	{
		/// <summary>Handles the event fired when the next slide is loading.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>

		if (this.options.enablePreviousSlide)
		{
			if (this.slideExistsByOffset(-1))
				this.previousButton.enable();
			else
				this.previousButton.disable();
		}
		
		if (this.options.enableNextSlide)
		{
			if (this.slideExistsByOffset(1))
				this.nextButton.enable();
			else
				this.nextButton.disable();
		}
	}
});

/*******************************************
 * class: SlideShow.ThumbnailPreview
 *******************************************/
SlideShow.ThumbnailPreview = function(control, parent, options)
{
	/// <summary>Provides a preview image for thumbnails.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="options">The options for the control.</param>
	
	var xaml =
		'<Canvas xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="ThumbnailPreview" Visibility="Collapsed">' +
		'	<Canvas.Resources>' +
		'		<Storyboard x:Name="HoverStoryboard">' +
		'			<DoubleAnimation Storyboard.TargetName="ScaleTransform" Storyboard.TargetProperty="ScaleX" To="1" />' +
		'			<DoubleAnimation Storyboard.TargetName="ScaleTransform" Storyboard.TargetProperty="ScaleY" To="1" />' +
		'			<DoubleAnimation Storyboard.TargetName="TranslateTransform" Storyboard.TargetProperty="X" To="0" />' +
		'			<DoubleAnimation Storyboard.TargetName="TranslateTransform" Storyboard.TargetProperty="Y" To="0" />' +
		'		</Storyboard>' +
		'		<Storyboard x:Name="LoadStoryboard" Storyboard.TargetName="Image" Storyboard.TargetProperty="Opacity">' +
		'			<DoubleAnimation x:Name="LoadAnimation" To="1" />' +
		'		</Storyboard>' +
		'	</Canvas.Resources>' +
		'	<Canvas.RenderTransform>' +
		'		<TransformGroup>' +
		'			<ScaleTransform x:Name="ScaleTransform" />' +
		'			<TranslateTransform x:Name="TranslateTransform" />' +
		'		</TransformGroup>' +
		'	</Canvas.RenderTransform>' +	
		'	<Rectangle x:Name="Background" />' +
		'	<Rectangle x:Name="Image" Opacity="0">' +
		'		<Rectangle.Fill>' +
		'			<ImageBrush x:Name="ImageBrush" />' +
		'		</Rectangle.Fill>' +
		'	</Rectangle>' +
		'	<Path x:Name="Arrow" Stretch="Fill" Data="M257.90625,640.65625 L264.31275,640.65625 261.10987,643.87525Z" />' +
		'</Canvas>';
	
	SlideShow.ThumbnailPreview.base.constructor.call(this, control, parent, xaml);
	
	SlideShow.merge(this.options,
	{
		width: 150,
		height: 100,
		radius: 4,
		stroke: "White",
		strokeThickness: 2,
		arrowWidth: 10,
		arrowHeight: 6,
		arrowFill: "White",
		imageStretch: "UniformToFill",
		imageBackground: "#333",
		hoverAnimationDuration: 0.1,
		loadAnimationDuration: 0.5,
		visibility: "Collapsed"
	});
	
	this.setOptions(options);
	
	this.hoverStoryboard = this.root.findName("HoverStoryboard");
	this.loadStoryboard = this.root.findName("LoadStoryboard");
	this.loadAnimation = this.root.findName("LoadAnimation");
	this.scaleTransform = this.root.findName("ScaleTransform");
	this.translateTransform = this.root.findName("TranslateTransform");
	this.background = this.root.findName("Background");
	this.image = this.root.findName("Image");
	this.imageBrush = this.root.findName("ImageBrush");
	this.arrow = this.root.findName("Arrow");
};

SlideShow.extend(SlideShow.UserControl, SlideShow.ThumbnailPreview,
{	 
	render: function()
	{
		/// <summary>Renders the preview using the current options.</summary>

		SlideShow.ThumbnailPreview.base.render.call(this);
		
		// arrow
		this.arrow.width = this.options.arrowWidth;
		this.arrow.height = this.options.arrowHeight;
		this.arrow.fill = this.options.arrowFill;
		this.arrow["Canvas.Top"] = this.root.height;
		this.arrow["Canvas.Left"] = this.root.width / 2 - this.arrow.width / 2;	
		
		// background
		this.background.width = this.options.width;
		this.background.height = this.options.height;
		this.background.radiusX = this.options.radius;
		this.background.radiusY = this.options.radius;
		this.background.fill = this.options.imageBackground;
		this.background.stroke = this.options.stroke;
		this.background.strokeThickness = this.options.strokeThickness;
		
		// image
		this.image.width = this.options.width;
		this.image.height = this.options.height;
		this.image.radiusX = this.options.radius;
		this.image.radiusY = this.options.radius;
		this.image.stroke = this.options.stroke;
		this.image.strokeThickness = this.options.strokeThickness;
		this.imageBrush.stretch = this.options.imageStretch;	
		
		// load animation
		if (this.imageBrush.downloadProgress == 1)
		{
			this.image.opacity = 1;
		}
		else
		{
			this.loadAnimation.duration = "0:0:" + this.options.loadAnimationDuration;
			this.imageBrush.addEventListener("DownloadProgressChanged", SlideShow.createDelegate(this, this.onImageDownloadProgressChanged));
			this.imageBrush.addEventListener("ImageFailed", SlideShow.createDelegate(this, this.onImageDownloadFailed));
		}
		
		// hover animation
		for (var i = 0, j = this.hoverStoryboard.children.count; i < j; i++)
			this.hoverStoryboard.children.getItem(i).duration = "0:0:" + this.options.hoverAnimationDuration;
	},
	
	show: function(image)
	{
		/// <summary>Displays the control with the specified image.</summary>
		/// <param name="image">The image to preview.</param>
		
		this.scaleTransform.scaleX = 0;
		this.scaleTransform.scaleY = 0;
		this.translateTransform.x = this.root.width / 2;
		this.translateTransform.y = this.root.height;
		this.root.visibility = "Visible";
		this.imageBrush.imageSource = image;
		this.hoverStoryboard.begin();
	},
	
	hide: function()
	{
		/// <summary>Hides the control.</summary>
	
		this.root.visibility = "Collapsed";
		this.imageBrush.imageSource = "";
	},
	
	onImageDownloadProgressChanged: function(sender, e)
	{
		/// <summary>Handles the event fired while the image is downloading.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		if (sender.downloadProgress == 1)
			this.loadStoryboard.begin();
	},
	
	onImageDownloadFailed: function(sender, e)
	{
		/// <summary>Handles the event fired when the image failed to download.</summary>
		/// <param name="sender">The event soure.</param>
		/// <param name="e">The event arguments.</param>
		
		throw new Error("Image download failed: " + this.imageBrush.imageSource);
	}
});

/*******************************************
 * class: SlideShow.ViewerBackground
 *******************************************/
SlideShow.ViewerBackground = function(control, parent, options)
{
	/// <summary>Displays a background for the thumbnail viewer control.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="parent">The parent control.</param>
	/// <param name="options">The options for the control.</param>
	
	var xaml =
		'<Canvas xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="ViewerBackground" Visibility="Collapsed">' +
		'	<Rectangle x:Name="background" />' +
		'</Canvas>';
	
	SlideShow.ViewerBackground.base.constructor.call(this, control, parent, xaml);

	SlideShow.merge(this.options,
	{
		top: 0,
		bottom: 0,
		left: 0,
		right: 0,
		radius: 4,
		fill: "Black",
		stroke: "Black",
		strokeThickness: 0	
	});
	
	this.setOptions(options);
	
	this.background = this.root.findName("background");
};

SlideShow.extend(SlideShow.UserControl, SlideShow.ViewerBackground,
{
	render: function()
	{
		/// <summary>Renders the viewer background using the current options.</summary>
		
		SlideShow.ViewerBackground.base.render.call(this);
		
		this.background.width = this.root.width;
		this.background.height = this.root.height;
		this.background.radiusX = this.options.radius;
		this.background.radiusY = this.options.radius;
		this.background.fill = this.options.fill;
		this.background.stroke = this.options.stroke;
		this.background.strokeThickness = this.options.strokeThickness;
	},
	
	onSizeChanged: function()
	{
		/// <summary>Handles the event fired when the control is resized.</summary>
		
		SlideShow.ViewerBackground.base.onSizeChanged.call(this);
		
		this.background.width = this.root.width;
		this.background.height = this.root.height;
	}
});﻿// Slide.Show, version 1.0
// Copyright © Vertigo Software, Inc.
// This source is subject to the Microsoft Public License (Ms-PL).
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/publiclicense.mspx.
// All other rights reserved.

/// <reference path="SlideShow.js" />

/*******************************************
 * class: SlideShow.Transition
 *******************************************/
SlideShow.Transition = function(control)
{
	/// <summary>Provides a base class for transitions.</summary>
	/// <param name="control">The Slide.Show control.</param>
	
	SlideShow.Transition.base.constructor.call(this);
	
	this.control = control;
	this.state = "Stopped";
};

SlideShow.extend(SlideShow.Object, SlideShow.Transition,
{
	begin: function(fromImage, toImage)
	{
		/// <summary>Begins the transition between the specified images.</summary>
		/// <param name="fromImage">The initial image.</param>
		/// <param name="toImage">The final image.</param>
		
		this.state = "Started";
		this.fromImage = fromImage;
		this.toImage = toImage;
		this.outStoryboardComplete = false;
		this.inStoryboardComplete = false;
	},
	
	complete: function()
	{
		/// <summary>Cleans up after the transition and fires the "complete" event.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.fromImage.root.visibility = "Collapsed";
		this.toImage.root.visibility = "Visible";
		
		this.state = "Stopped";
		this.fireEvent("complete");
	},
	
	addStoryboard: function(slideImage, targetName, targetProperty, animationXaml)
	{
		/// <summary>Adds a storyboard to the specified SlideImage control.</summary>
		/// <param name="slideImage">The SlideImage control.</param>
		/// <param name="targetName">The TargetName value for the added storyboard.</param>
		/// <param name="targetProperty">The TargetProperty value for the added storyboard.</param>
		/// <param name="animationXaml">The animation XAML for the added storyboard.</param>
		/// <returns>The added storyboard.</returns>
		
		var storyboardXaml = '<Storyboard xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="TransitionStoryboard" Storyboard.TargetName="' + targetName + '" Storyboard.TargetProperty="' + targetProperty + '">' + animationXaml + '</Storyboard>';
		var storyboard = this.control.host.content.createFromXaml(storyboardXaml);
		slideImage.root.resources.add(storyboard);
		return storyboard;
	},
	
	createClippingPath: function(geometryXaml)
	{
		/// <summary>Creates a clipping path containing a geometry group with the specified XAML.</summary>
		/// <param name="geometryXaml">The geometry XAML representing the clipping path for the canvas.</param>
		/// <returns>The clipping path.</returns>
		
		var clipXaml = '<Canvas.Clip xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"><GeometryGroup>' + geometryXaml + '</GeometryGroup></Canvas.Clip>';
		return this.control.host.content.createFromXaml(clipXaml);
	},
	
	onOutStoryboardComplete: function(sender, e)
	{
		/// <summary>Cleans up the completed storyboards and fires the "complete" event.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.outStoryboardComplete = true;
		
		if (this.inStoryboardComplete)
			this.complete();
	},
	
	onInStoryboardComplete: function(sender, e)
	{
		/// <summary>Cleans up the completed storyboards and fires the "complete" event.</summary>
		/// <param name="sender">The event source.</param>
		/// <param name="e">The event arguments.</param>
		
		this.inStoryboardComplete = true;
		
		if (this.outStoryboardComplete)
			this.complete();
	}	
});

/*******************************************
 * class: SlideShow.NoTransition
 *******************************************/
SlideShow.NoTransition = function(control)
{
	/// <summary>A transition which simply switches the current image with a new one.</summary>
	/// <param name="control">The Slide.Show control.</param>
	
	SlideShow.NoTransition.base.constructor.call(this, control);
};

SlideShow.extend(SlideShow.Transition, SlideShow.NoTransition,
{
	begin: function(fromImage, toImage)
	{
		/// <summary>Begins the transition between the specified images.</summary>
		/// <param name="fromImage">The initial image.</param>
		/// <param name="toImage">The final image.</param>
		
		SlideShow.NoTransition.base.begin.call(this, fromImage, toImage);
		
		fromImage.root.visibility = "Collapsed";
		toImage.root.visibility = "Visible";	
		this.complete();
	}
});

/*******************************************
 * class: SlideShow.FadeTransition
 *******************************************/
SlideShow.FadeTransition = function(control, options)
{
	/// <summary>A transition which fades in a new image and fades out the current one.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="options">The options for the transition.</param>
	
	SlideShow.FadeTransition.base.constructor.call(this, control);
	
	SlideShow.merge(this.options,
	{
		direction: "InOut",
		duration: 0.8
	});
	
	this.setOptions(options);
};

SlideShow.extend(SlideShow.Transition, SlideShow.FadeTransition,
{
	begin: function(fromImage, toImage)
	{
		/// <summary>Begins the transition between the specified images.</summary>
		/// <param name="fromImage">The initial image.</param>
		/// <param name="toImage">The final image.</param>
		
		SlideShow.FadeTransition.base.begin.call(this, fromImage, toImage);
		
		switch (this.options.direction.toLowerCase())
		{
			case "in":
				fromImage.root.visibility = "Collapsed";
				toImage.root.visibility = "Visible";
				break;
			
			case "inout":
				fromImage.root.visibility = "Visible";
				toImage.root.visibility = "Visible";
				break;
			
			default:
				throw new Error("Invalid direction: " + this.options.direction);
		}
		
		// out storyboard
		var outAnimationXaml = '<DoubleAnimation Duration="0:0:' + this.options.duration + '" From="1" To="0" />';
		var outStoryboard = this.addStoryboard(fromImage, fromImage.image.name, "Opacity", outAnimationXaml);
		outStoryboard.addEventListener("Completed", SlideShow.createDelegate(this, this.onOutStoryboardComplete));
		outStoryboard.begin();
		
		// in storyboard
		var inAnimationXaml = '<DoubleAnimation Duration="0:0:' + this.options.duration + '" From="0" To="1" />';
		var inStoryboard = this.addStoryboard(toImage, toImage.image.name, "Opacity", inAnimationXaml);
		inStoryboard.addEventListener("Completed", SlideShow.createDelegate(this, this.onInStoryboardComplete));
		inStoryboard.begin();
	}
});

/*******************************************
 * class: SlideShow.SlideTransition
 *******************************************/
SlideShow.SlideTransition = function(control, options)
{
	/// <summary>A transition which slides in a new image and slides out the current one.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="options">The options for the transition.</param>
	
	SlideShow.SlideTransition.base.constructor.call(this, control);
	
	SlideShow.merge(this.options,
	{
		direction: "Left",
		duration: 0.8
	});
	
	this.setOptions(options);
};

SlideShow.extend(SlideShow.Transition, SlideShow.SlideTransition,
{
	begin: function(fromImage, toImage)
	{
		/// <summary>Begins the transition between the specified images.</summary>
		/// <param name="fromImage">The initial image.</param>
		/// <param name="toImage">The final image.</param>
		
		SlideShow.SlideTransition.base.begin.call(this, fromImage, toImage);
		
		var fromImageToValue, toImageFromValue, targetProperty;
		
		switch (this.options.direction.toLowerCase())
		{
			case "left":
				fromImageToValue = -this.control.root.width;
				toImageFromValue = this.control.root.width;
				targetProperty = "(Canvas.Left)";
				break;
			
			case "right":
				fromImageToValue = this.control.root.width;
				toImageFromValue = -this.control.root.width;
				targetProperty = "(Canvas.Left)";
				break;
			
			case "up":
				fromImageToValue = -this.control.root.height;
				toImageFromValue = this.control.root.height;
				targetProperty = "(Canvas.Top)";
				break;
			
			case "down":
				fromImageToValue = this.control.root.height;
				toImageFromValue = -this.control.root.height;
				targetProperty = "(Canvas.Top)";
				break;
			
			default:
				throw new Error("Invalid direction: " + this.options.direction);
		}
		
		fromImage.root.visibility = "Visible";
		toImage.root.visibility = "Visible";
		
		// out storyboard
		var outAnimationXaml =
			'<DoubleAnimationUsingKeyFrames>' +
			'	<SplineDoubleKeyFrame KeySpline="0,0 0,1" KeyTime="0:0:0" Value="0" />' +
			'	<SplineDoubleKeyFrame KeySpline="0,0 0,1" KeyTime="0:0:' + this.options.duration + '" Value="' + fromImageToValue + '" />' +
			'</DoubleAnimationUsingKeyFrames>';
		
		var outStoryboard = this.addStoryboard(fromImage, fromImage.image.name, targetProperty, outAnimationXaml);
		outStoryboard.addEventListener("Completed", SlideShow.createDelegate(this, this.onOutStoryboardComplete));
		outStoryboard.begin();	
		
		// in storyboard
		var inAnimationXaml =
			'<DoubleAnimationUsingKeyFrames>' +
			'	<SplineDoubleKeyFrame KeySpline="0,0 0,1" KeyTime="0:0:0" Value="' + toImageFromValue + '" />' +
			'	<SplineDoubleKeyFrame KeySpline="0,0 0,1" KeyTime="0:0:' + this.options.duration + '" Value="0" />' +
			'</DoubleAnimationUsingKeyFrames>';
		
		var inStoryboard = this.addStoryboard(toImage, toImage.image.name, targetProperty, inAnimationXaml);
		inStoryboard.addEventListener("Completed", SlideShow.createDelegate(this, this.onInStoryboardComplete));
		inStoryboard.begin();	
	}
});

/*******************************************
 * class: SlideShow.WipeTransition
 *******************************************/
SlideShow.WipeTransition = function(control, options)
{
	/// <summary>A transition which wipes a new image over the current one.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="options">The options for the transition.</param>
	
	SlideShow.WipeTransition.base.constructor.call(this, control);
	
	SlideShow.merge(this.options,
	{
		direction: "Left",
		duration: 0.8
	});
	
	this.setOptions(options);
};

SlideShow.extend(SlideShow.Transition, SlideShow.WipeTransition,
{
	begin: function(fromImage, toImage)
	{
		/// <summary>Begins the transition between the specified images.</summary>
		/// <param name="fromImage">The initial image.</param>
		/// <param name="toImage">The final image.</param>
		
		SlideShow.WipeTransition.base.begin.call(this, fromImage, toImage);
		
		var fromClippingPathTo, toClippingPathTop, toClippingPathLeft, toClippingPathFrom, targetProperty;
		
		switch (this.options.direction.toLowerCase())
		{
			case "left":
				fromClippingPathTo = -fromImage.parent.root.width;
				toClippingPathTop = 0;
				toClippingPathLeft = toImage.parent.root.width;
				toClippingPathFrom = toImage.parent.root.width;
				targetProperty = "X";
				break;
			
			case "right":
				fromClippingPathTo = fromImage.parent.root.width;
				toClippingPathTop = 0;
				toClippingPathLeft = -toImage.parent.root.width;
				toClippingPathFrom = -toImage.parent.root.width;
				targetProperty = "X";
				break;
			
			case "up":
				fromClippingPathTo = -fromImage.parent.root.height;
				toClippingPathTop = toImage.parent.root.height;
				toClippingPathLeft = 0;
				toClippingPathFrom = toImage.parent.root.height;
				targetProperty = "Y";
				break;
			
			case "down":
				fromClippingPathTo = fromImage.parent.root.height;
				toClippingPathTop = -toImage.parent.root.height;
				toClippingPathLeft = 0;
				toClippingPathFrom = -toImage.parent.root.height;
				targetProperty = "Y";
				break;
			
			default:
				throw new Error("Invalid direction: " + this.options.direction);
		}
		
		// from image
		var fromGeometryXaml =
			'<RectangleGeometry xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" Rect="0,0,' + fromImage.parent.root.width + "," + fromImage.parent.root.height + '">' +
			'	<RectangleGeometry.Transform>' +
			'		<TranslateTransform x:Name="VisibleTransform" X="0" Y="0" />' +
			'	</RectangleGeometry.Transform>' +
			'</RectangleGeometry>';
		
		fromImage.root.clip = this.createClippingPath(fromGeometryXaml);
		fromImage.root.visibility = "Visible";
				
		// to image
		var toGeometryXaml =
			'<RectangleGeometry xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" Rect="0,0,' + toImage.parent.root.width + "," + toImage.parent.root.height + '">' +
			'	<RectangleGeometry.Transform>' +
			'		<TranslateTransform x:Name="VisibleTransform" X="' + toClippingPathLeft + '" Y="' + toClippingPathTop + '" />' +
			'	</RectangleGeometry.Transform>' +
			'</RectangleGeometry>';
		
		toImage.root.clip = this.createClippingPath(toGeometryXaml);
		toImage.root.visibility = "Visible";
		
		fromImage.addEventListener("sizeChange", SlideShow.createDelegate(this, this.onSlideImageSizeChanged));
		toImage.addEventListener("sizeChange", SlideShow.createDelegate(this, this.onSlideImageSizeChanged));
		
		// out storyboard
		var outAnimationXaml =
			'<DoubleAnimationUsingKeyFrames>' +
			'	<SplineDoubleKeyFrame KeySpline="0,0 0,0" KeyTime="0:0:0" Value="0" />' +
			'	<SplineDoubleKeyFrame KeySpline="0,0 0,1" KeyTime="0:0:' + this.options.duration + '" Value="' + fromClippingPathTo + '" />' +
			'</DoubleAnimationUsingKeyFrames> ';
		
		this.outStoryboard = this.addStoryboard(fromImage, "VisibleTransform", targetProperty, outAnimationXaml);
		this.outStoryboard.addEventListener("Completed", SlideShow.createDelegate(this, this.onOutStoryboardComplete));
		this.outStoryboard.begin();
		
		// in storyboard
		var inAnimationXaml =
			'<DoubleAnimationUsingKeyFrames>' +
			'	<SplineDoubleKeyFrame KeySpline="0,0 0,0" KeyTime="0:0:0" Value="' + toClippingPathFrom + '" />' +
			'	<SplineDoubleKeyFrame KeySpline="0,0 0,1" KeyTime="0:0:' + this.options.duration + '" Value="0" />' +
			'</DoubleAnimationUsingKeyFrames> ';
		
		this.inStoryboard = this.addStoryboard(toImage, "VisibleTransform", targetProperty, inAnimationXaml);
		this.inStoryboard.addEventListener("Completed", SlideShow.createDelegate(this, this.onInStoryboardComplete));
		this.inStoryboard.begin();
	},
	
	onSlideImageSizeChanged: function(sender)
	{
		/// <summary>Handles the event fired when the slide image control is resized.</summary>
		/// <summary>Handles the event fired when the control is resized.</summary>

		if (this.state = "Started")
		{
			this.outStoryboard.seek('0:0:' + this.options.duration);
			this.inStoryboard.seek('0:0:' + this.options.duration);
			SlideShow.WipeTransition.base.complete.call(this);
		}

		var clipGeometry = sender.root.clip.children.getItem(0);
		clipGeometry.rect = '0,0,' + sender.parent.root.width + ',' + sender.parent.root.height;
	}
});

/*******************************************
 * class: SlideShow.ShapeTransition
 *******************************************/
SlideShow.ShapeTransition = function(control, options)
{
	/// <summary>A transition which cuts to a new image from the current one using a shape.</summary>
	/// <param name="control">The Slide.Show control.</param>
	/// <param name="options">The options for the transition.</param>
	
	SlideShow.ShapeTransition.base.constructor.call(this, control);
	
	SlideShow.merge(this.options,
	{
		shape: "Circle",
		direction: "Out",
		duration: 0.8
	});
	
	this.setOptions(options);
};

SlideShow.extend(SlideShow.Transition, SlideShow.ShapeTransition,
{
	begin: function(fromImage, toImage)
	{
		/// <summary>Begins the transition between the specified images.</summary>
		/// <param name="fromImage">The initial image.</param>
		/// <param name="toImage">The final image.</param>
		
		SlideShow.ShapeTransition.base.begin.call(this, fromImage, toImage);
		
		switch (this.options.shape.toLowerCase())
		{
			case "circle":
				var direction = this.options.direction.toLowerCase();
				
				if (direction != "out" && direction != "in")
					throw new Error("Invalid direction: " + direction);
				
				var maxSize = Math.max(this.control.root.width, this.control.root.height);
				var clipFrom = (direction == "out") ? 0 : maxSize;
				var clipTo = (direction == "out") ? maxSize : 0;
				
				var simpleXaml = '<EllipseGeometry xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml" x:Name="CircleBehaviorPath" Center="' + (this.control.root.width / 2) + ',' + (this.control.root.height / 2) + '" RadiusX="' + clipFrom + '" RadiusY="' + clipFrom + '" />';
				var complexXaml = simpleXaml + '<RectangleGeometry Rect="0,0,' + this.control.root.width + ',' + this.control.root.height + '" />';
				
				if (direction == "out")
				{
					fromImage.root.clip = this.createClippingPath(complexXaml);
					toImage.root.clip = this.createClippingPath(simpleXaml);
				}
				else
				{
					fromImage.root.clip = this.createClippingPath(simpleXaml);
					toImage.root.clip = this.createClippingPath(complexXaml);
				}
				
				fromImage.root.visibility = "Visible";
				toImage.root.visibility = "Visible";
				
				fromImage.addEventListener("sizeChange", SlideShow.createDelegate(this, this.onSlideImageSizeChanged));
				toImage.addEventListener("sizeChange", SlideShow.createDelegate(this, this.onSlideImageSizeChanged));
				
				// out storyboard
				var animationXaml =
					'<DoubleAnimation Storyboard.TargetProperty="RadiusX" Duration="0:0:' + this.options.duration + '" To="' + clipTo + '" />' +
					'<DoubleAnimation Storyboard.TargetProperty="RadiusY" Duration="0:0:' + this.options.duration + '" To="' + clipTo + '" />';
				
				this.outStoryboard = this.addStoryboard(fromImage, "CircleBehaviorPath", "RadiusX", animationXaml);
				this.outStoryboard.addEventListener("Completed", SlideShow.createDelegate(this, this.onOutStoryboardComplete));
				this.outStoryboard.begin();
				
				// in storyboard
				this.inStoryboard = this.addStoryboard(toImage, "CircleBehaviorPath", "RadiusX", animationXaml);
				this.inStoryboard.addEventListener("Completed", SlideShow.createDelegate(this, this.onInStoryboardComplete));
				this.inStoryboard.begin();
				
				break;

			default:
				throw new Error("Invalid shape: " + this.options.shape.toLowerCase());
		}
	},

	onSlideImageSizeChanged: function(sender)
	{
		/// <summary>Handles the event fired when the slide image control is resized.</summary>

		if (this.state = "Started")
		{
			this.outStoryboard.stop();
			this.inStoryboard.stop();
			SlideShow.ShapeTransition.base.complete.call(this);
		}

		var clipGeometry = sender.root.clip.children.getItem(0);
		var maxSize = Math.max(this.control.root.width, this.control.root.height);
		var center = (sender.parent.root.width / 2) + ',' + (sender.parent.root.height / 2);
		clipGeometry.center = center;
		clipGeometry.radiusX = maxSize;
		clipGeometry.radiusY = maxSize;
	}
});