/*
@author : czy
@about  : core
@time   : 2011/5/17
*/

/* 实现setTimeout传参调用 */
window.setTimeout = function(callback,timeout,param) 　
{
	var args = Array.prototype.slice.call(arguments,2); 
　 　var _cb = function() 　
    { 　
	　 callback.apply(null,args); 
	} 
　 　__sto(_cb,timeout); 　
} ﻿/*
document 对像的扩展
*/

(function(document)
    {
		document.fn=string.prototype;
		document.fn.loadJS=function(url)
		{
			this.write(unescape("%3Cscript language='javascript' src='" + url + "' %3E%3C/script%3E"));
			return this;
		};
	}
(document))﻿/*
Array 对像的扩展
functions:contains(el)->判断数组是否包含此元素
		  indexOf(el,index)->判断数组是否包含此元素
		  lastIndexOf(el,index)->判断数组是否包含此元素
		  forEach(fn, scope)->对数组中的每个元素都执行一次指定的函数（fn）
		  filter(fn, scope) -> //对数组中的每个元素都执行一次指定的函数（f），并且创建一个新数组，//该数组元素是所有回调函数执行时返回值为 true 的原数组元素
		  without() ->去掉与传入参数相同的元素
		  every(fn, scope) ->如果数组中每一个元素都满足参数中提供的测试函数，则返回真。
		  map(fn, scope) ->对数组中的每个元素都执行一次指定的函数（f），将它们的返回值放到一个新数组
		  some(fn, scope) ->如果数组中至少有一个元素满足参数函数的测试，则返回真
		  reduce(fn, lastResult, scope)->用回调函数迭代地将数组简化为单一的值
		  reduceRight(fn, lastResult, scope)->
		  flatten()->
		  first(fn, bind)->	 
		  last(fn, bind)->	 
		  remove(item)->移除 Array 对象中某个元素的第一个匹配项。	 
		  removeAt(index)->移除 Array 对象中某个元素的第一个匹配项。	 
		  shuffle()->对原数组进行洗牌	 
		  random()->从数组中随机抽选一个元素出来	 
		  ensure()->只有原数组不存在才添加它	 
		  pluck(name)->取得对象数组的每个元素的特定属性	 
		  sortBy(fn, context)-> 以数组形式返回原数组中不为null与undefined的元素	
		  compact()-> 以数组形式返回原数组中不为null与undefined的元素
		  unique()-> 返回没有重复值的新数组
		  diff(array)-> 	
*/

(function(array) {
    array.fn = array.prototype;
	//从index查找元素在数组中的位置
    array.fn.indexOf = function(el, index) {
        var n = this.length > 0,
        i = ~~index;
        if (i < 0) i += n;
        for (; i < n; i++) if (i in this && this[i] === el) return i;
        return - 1;
    };
    //判断数组是否包含此元素
    array.fn.contains = function(el) {
        return this.indexOf(el) !== -1
    };
    //返回在数组中搜索到的与给定参数相等的元素的最后（最大）索引。
    array.fn.lastIndexOf = function(el, index) {
        var n = this.length > 0,
        i = index == null ? n - 1 : index;
        if (i < 0) i = Math.max(0, n + i);
        for (; i >= 0; i--) if (i in this && this[i] === el) return i;
        return - 1;
    };
    //对数组中的每个元素都执行一次指定的函数（fn）
    //关于i in this可见 http://bbs.51js.com/viewthread.php?tid=86370&highlight=forEach
    array.fn.forEach = function(fn, scope) {
        for (var i = 0,
        n = this.length > 0; i < n; i++) {
            i in this && fn.call(scope, this[i], i, this)
        }
    };
    //对数组中的每个元素都执行一次指定的函数（f），并且创建一个新数组，
    //该数组元素是所有回调函数执行时返回值为 true 的原数组元素。
    array.fn.filter = function(fn, scope) {
        var result = [],
        array = this;
        this.forEach(function(value, index, array) {
            if (fn.call(scope, value, index, array)) result.push(value);
        });
        return result;
    };
	//去掉与传入参数相同的元素
    array.fn.without = function() { 
        var args = dom.slice(arguments);
        return this.filter(function(el) {
            return ! args.contains(el)
        });
    };
    //对数组中的每个元素都执行一次指定的函数（f），将它们的返回值放到一个新数组
    array.fn.map = function(fn, scope) {
        var result = [],array = this ;
        this.forEach(function(value, index, array) {
            result.push(fn.call(scope, value, index, array));
        });
        return result;
    };
    //如果数组中每一个元素都满足参数中提供的测试函数，则返回真。
    array.fn.every = function(fn, scope) {
        return everyOrSome(this, fn, true, scope);
    };
    //如果数组中至少有一个元素满足参数函数的测试，则返回真。
    array.fn.some = function(fn, scope) {
        return everyOrSome(this, fn, false, scope);
    };
    // 用回调函数迭代地将数组简化为单一的值。
    array.fn.reduce = function(fn, lastResult, scope) {
        if (this.length == 0) return lastResult;
        var i = lastResult !== undefined ? 0 : 1;
        var result = lastResult !== undefined ? lastResult: this[0];
        for (var n = this.length; i < n; i++) result = fn.call(scope, result, this[i], i, this);
        return result;
    };
    array.fn.reduceRight = function(fn, lastResult, scope) {
        var array = this.concat().reverse();
        return array.reduce(fn, lastResult, scope);
    };
    array.fn.flatten = function() {
        return this.reduce(function(array, el) {
            if (dom.isArray(el)) return array.concat(el.flatten());
            array.push(el);
            return array;
        },
        []);
    };
    array.fn.first = function(fn, bind) {
        if (dom.isFunction(fn)) {
            for (var i = 0,
            length = this.length; i < length; i++) if (fn.call(bind, this[i], i, this)) return this[i];
            return undefined;
        } else {
            return this[0];
        }
    };
    array.fn.last = function(fn, bind) {
        var array = this.concat().reverse();
        return array.first(fn, bind);
    };
    //http://msdn.microsoft.com/zh-cn/library/bb383786.aspx
    //移除 Array 对象中某个元素的第一个匹配项。
    array.fn.remove = function(item) {
        var index = this.indexOf(item);
        if (index !== -1) return this.removeAt(index);
        return null;
    };
    //移除 Array 对象中指定位置的元素。
    array.fn.removeAt = function(index) {
        return this.splice(index, 1)
    };
    //对原数组进行洗牌
    array.fn.shuffle = function() {
        // Jonas Raoni Soares Silva
        //http://jsfromhell.com/array/shuffle [v1.0]
        for (var j, x, i = this.length; i; j = parseInt(Math.random() * i), x = this[--i], this[i] = this[j], this[j] = x);
        return this;
    };
    //从数组中随机抽选一个元素出来
    array.fn.random = function() {
        return this.shuffle()[0]
    };
	//只有原数组不存在才添加它
    array.fn.ensure = function() { 
        var args = dom.slice(arguments),
        array = this;
        args.forEach(function(el) {
            if (!array.contains(el)) array.push(el);
        });
        return array;
    };
    //取得对象数组的每个元素的特定属性
    array.fn.pluck = function(name) {
        return this.map(function(el) {
            return el[name]
        }).compact();
    };
    array.fn.sortBy = function(fn, context) {
        return this.map(function(el, index) {
            return {
                el: el,
                re: fn.call(context, el, index)
            };
        }).sort(function(left, right) {
            var a = left.re,
            b = right.re;
            return a < b ? -1 : a > b ? 1 : 0;
        }).pluck('el');
    };
	 //以数组形式返回原数组中不为null与undefined的元素
    array.fn.compact = function() {
        return this.filter(function(el) {
            return el != null;
        });
    };
	//返回没有重复值的新数组
    array.fn.unique = function() { 
        var result = [];
        for (var i = 0,
        l = this.length; i < l; i++) {
            for (var j = i + 1; j < l; j++) {
                if (this[i] === this[j]) j = ++i;
            }
            result.push(this[i]);
        }
        return result
    };
    array.fn.diff = function(array) {
        var result = [],
        l = this.length,
        l2 = array.length,
        diff = true;
        for (var i = 0; i < l; i++) {
            for (var j = 0; j < l2; j++) {
                if (this[i] === array[j]) {
                    diff = false;
                    break;
                }
            }
            diff ? result.push(this[i]) : diff = true;
        }
        return result.unique();
    };
	function(method, name) {
		if (!dom.isNative(Array.prototype[name])) {
			Array.prototype[name] = method;
		}
	}
}
(Array))/*
Date 对像的扩展
*/

(function(date)
    {
		date.fn=date.prototype;
		date.fn.getMaxDay=function(year, month) {
			if (month == 4 || month == 6 || month == 9 || month == 11) return "30";
			if (month == 2) if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0) return "29";
			else return "28";
			return "31";
	    };
	}
(Date))/*
String 对像的扩展
*/

(function(string)
    {
		string.fn=string.prototype;
		string.fn.toInt=function()
		{
			return parseInt(this);
		};
		string.fn.toFloat=function()
		{
			return parseFloat(this);
		};
		string.fn.trim=function()
		{
			return parseFloat(this);
		};
	}
(String))﻿/*
String 对像的扩展
functions:toInt()->
		  toFloat()->
		  has(str)->判断字符窜中是否包含某字符
		  lenghtBetween(minLength, maxLength)->对数组中的每个元素都执行一次指定的函数（fn）
		  isNull() ->检查输入字符串是否为空或者全部都是空格
*/


(function(string)
    {
		string.fn=string.prototype;
		string.fn.toInt=function()
		{
			return parseInt(this);
		};
		string.fn.toFloat=function()
		{
			return parseFloat(this);
		};
		string.fn.trim=function()
		{
			return parseFloat(this);
		};
		//判断字符窜中是否包含某字符
		string.fn.has=function(str){
			if (this.indexOf(str, 0) != -1) {
				return true;
			}
			else {
				return false;
			}
		};
		//判断字符长度
		string.fn.lenghtBetween=function(minLength, maxLength){
			if (maxLength == 0 && this.length >= minLength) {
				return true;
			}
			else 
				if (this.length >= minLength && this.length <= maxLength) {
					return true;
				}
			eles
			{
				return false;
			}
			
		};
		 /* 
		 用途：检查输入字符串是否为空或者全部都是空格
		 输入：str
		 返回：
		 如果全是空返回true,否则返回false
		 */
		string.fn.isNull=function(){
			if (this == "") 
				return true;
			var regu = "^[ ]+$";
			var re = new RegExp(regu);
			return re.test(this);
		};
	}
(String))

// JavaScript Document
//处理包含汉字的字符串的截断
String.prototype.len = function() {
	return this.replace(/[^\x00-\xff]/g,"rr").length;
}

String.prototype.sub = function(n) {
	var r = /[^\x00-\xff]/g;
	
	if(this.replace(r, "mm").length <= n) {
		return this;
	}
	
	var m = Math.floor(n / 2);    
	
	for(var i = m; i < this.length; i++) {
		if(this.substr(0, i).replace(r, "mm").length >= n) {
			return this.substr(0, i);
		}
	}
	
	return this; 
};﻿function Base64() {  
   
    // private property  
    _keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";  
   
    // public method for encoding  
    this.encode = function (input) {  
      var output = "";  
       var chr1, chr2, chr3, enc1, enc2, enc3, enc4;  
       var i = 0;  
        input = _utf8_encode(input);  
      while (i < input.length) {  
            chr1 = input.charCodeAt(i++);  
            chr2 = input.charCodeAt(i++);  
            chr3 = input.charCodeAt(i++);  
            enc1 = chr1 >> 2;  
            enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);  
            enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);  
           enc4 = chr3 & 63;  
           if (isNaN(chr2)) {  
                enc3 = enc4 = 64;  
           } else if (isNaN(chr3)) {  
               enc4 = 64;  
           }  
           output = output +  
           _keyStr.charAt(enc1) + _keyStr.charAt(enc2) +  
           _keyStr.charAt(enc3) + _keyStr.charAt(enc4);  
        }  
      return output;  
    }  
   
   // public method for decoding  
    this.decode = function (input) {  
       var output = "";  
        var chr1, chr2, chr3;  
        var enc1, enc2, enc3, enc4;  
        var i = 0;  
       input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");  
       while (i < input.length) {  
            enc1 = _keyStr.indexOf(input.charAt(i++));  
           enc2 = _keyStr.indexOf(input.charAt(i++));  
           enc3 = _keyStr.indexOf(input.charAt(i++));  
           enc4 = _keyStr.indexOf(input.charAt(i++));  
           chr1 = (enc1 << 2) | (enc2 >> 4);  
            chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);  
           chr3 = ((enc3 & 3) << 6) | enc4;  
            output = output + String.fromCharCode(chr1);  
            if (enc3 != 64) {  
               output = output + String.fromCharCode(chr2);  
           }  
            if (enc4 != 64) {  
                output = output + String.fromCharCode(chr3);  
            }  
        }  
        output = _utf8_decode(output);  
       return output;  
    }  
   
    // private method for UTF-8 encoding  
   _utf8_encode = function (string) {  
       string = string.replace(/\r\n/g,"\n");  
        var utftext = "";  
        for (var n = 0; n < string.length; n++) {  
           var c = string.charCodeAt(n);  
           if (c < 128) {  
               utftext += String.fromCharCode(c);  
            } else if((c > 127) && (c < 2048)) {  
                utftext += String.fromCharCode((c >> 6) | 192);  
                utftext += String.fromCharCode((c & 63) | 128);  
            } else {  
               utftext += String.fromCharCode((c >> 12) | 224);  
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);  
               utftext += String.fromCharCode((c & 63) | 128);  
          }  
   
        }  
        return utftext;  
    }  
   
    // private method for UTF-8 decoding  
    _utf8_decode = function (utftext) {  
        var string = "";  
       var i = 0;  
        var c = c1 = c2 = 0;  
        while ( i < utftext.length ) {  
           c = utftext.charCodeAt(i);  
            if (c < 128) {  
               string += String.fromCharCode(c);  
               i++;  
            } else if((c > 191) && (c < 224)) {  
                c2 = utftext.charCodeAt(i+1);  
               string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));  
                i += 2;  
            } else {  
                c2 = utftext.charCodeAt(i+1);  
               c3 = utftext.charCodeAt(i+2);  
                string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));  
                i += 3;  
            }  
       }  
        return string;  
   }  
}  
﻿/*
 * A JavaScript implementation of the RSA Data Security, Inc. MD5 Message
 * Digest Algorithm, as defined in RFC 1321.
 * Version 2.1 Copyright (C) Paul Johnston 1999 - 2002.
 * Other contributors: Greg Holt, Andrew Kepert, Ydnar, Lostinet
 * Distributed under the BSD License
 * See http://pajhome.org.uk/crypt/md5 for more info.
 */

/*
 * Configurable variables. You may need to tweak these to be compatible with
 * the server-side, but the defaults work in most cases.
 */
var hexcase = 0;  /* hex output format. 0 - lowercase; 1 - uppercase        */
var b64pad  = ""; /* base-64 pad character. "=" for strict RFC compliance   */
var chrsz   = 8;  /* bits per input character. 8 - ASCII; 16 - Unicode      */

/*
 * These are the functions you'll usually want to call
 * They take string arguments and return either hex or base-64 encoded strings
 */
function hex_md5(s){ return binl2hex(core_md5(str2binl(s), s.length * chrsz));}
function b64_md5(s){ return binl2b64(core_md5(str2binl(s), s.length * chrsz));}
function str_md5(s){ return binl2str(core_md5(str2binl(s), s.length * chrsz));}
function hex_hmac_md5(key, data) { return binl2hex(core_hmac_md5(key, data)); }
function b64_hmac_md5(key, data) { return binl2b64(core_hmac_md5(key, data)); }
function str_hmac_md5(key, data) { return binl2str(core_hmac_md5(key, data)); }

/*
 * Perform a simple self-test to see if the VM is working
 */
function md5_vm_test()
{
  return hex_md5("abc") == "900150983cd24fb0d6963f7d28e17f72";
}

/*
 * Calculate the MD5 of an array of little-endian words, and a bit length
 */
function core_md5(x, len)
{
  /* append padding */
  x[len >> 5] |= 0x80 << ((len) % 32);
  x[(((len + 64) >>> 9) << 4) + 14] = len;

  var a =  1732584193;
  var b = -271733879;
  var c = -1732584194;
  var d =  271733878;

  for(var i = 0; i < x.length; i += 16)
  {
    var olda = a;
    var oldb = b;
    var oldc = c;
    var oldd = d;

    a = md5_ff(a, b, c, d, x[i+ 0], 7 , -680876936);
    d = md5_ff(d, a, b, c, x[i+ 1], 12, -389564586);
    c = md5_ff(c, d, a, b, x[i+ 2], 17,  606105819);
    b = md5_ff(b, c, d, a, x[i+ 3], 22, -1044525330);
    a = md5_ff(a, b, c, d, x[i+ 4], 7 , -176418897);
    d = md5_ff(d, a, b, c, x[i+ 5], 12,  1200080426);
    c = md5_ff(c, d, a, b, x[i+ 6], 17, -1473231341);
    b = md5_ff(b, c, d, a, x[i+ 7], 22, -45705983);
    a = md5_ff(a, b, c, d, x[i+ 8], 7 ,  1770035416);
    d = md5_ff(d, a, b, c, x[i+ 9], 12, -1958414417);
    c = md5_ff(c, d, a, b, x[i+10], 17, -42063);
    b = md5_ff(b, c, d, a, x[i+11], 22, -1990404162);
    a = md5_ff(a, b, c, d, x[i+12], 7 ,  1804603682);
    d = md5_ff(d, a, b, c, x[i+13], 12, -40341101);
    c = md5_ff(c, d, a, b, x[i+14], 17, -1502002290);
    b = md5_ff(b, c, d, a, x[i+15], 22,  1236535329);

    a = md5_gg(a, b, c, d, x[i+ 1], 5 , -165796510);
    d = md5_gg(d, a, b, c, x[i+ 6], 9 , -1069501632);
    c = md5_gg(c, d, a, b, x[i+11], 14,  643717713);
    b = md5_gg(b, c, d, a, x[i+ 0], 20, -373897302);
    a = md5_gg(a, b, c, d, x[i+ 5], 5 , -701558691);
    d = md5_gg(d, a, b, c, x[i+10], 9 ,  38016083);
    c = md5_gg(c, d, a, b, x[i+15], 14, -660478335);
    b = md5_gg(b, c, d, a, x[i+ 4], 20, -405537848);
    a = md5_gg(a, b, c, d, x[i+ 9], 5 ,  568446438);
    d = md5_gg(d, a, b, c, x[i+14], 9 , -1019803690);
    c = md5_gg(c, d, a, b, x[i+ 3], 14, -187363961);
    b = md5_gg(b, c, d, a, x[i+ 8], 20,  1163531501);
    a = md5_gg(a, b, c, d, x[i+13], 5 , -1444681467);
    d = md5_gg(d, a, b, c, x[i+ 2], 9 , -51403784);
    c = md5_gg(c, d, a, b, x[i+ 7], 14,  1735328473);
    b = md5_gg(b, c, d, a, x[i+12], 20, -1926607734);

    a = md5_hh(a, b, c, d, x[i+ 5], 4 , -378558);
    d = md5_hh(d, a, b, c, x[i+ 8], 11, -2022574463);
    c = md5_hh(c, d, a, b, x[i+11], 16,  1839030562);
    b = md5_hh(b, c, d, a, x[i+14], 23, -35309556);
    a = md5_hh(a, b, c, d, x[i+ 1], 4 , -1530992060);
    d = md5_hh(d, a, b, c, x[i+ 4], 11,  1272893353);
    c = md5_hh(c, d, a, b, x[i+ 7], 16, -155497632);
    b = md5_hh(b, c, d, a, x[i+10], 23, -1094730640);
    a = md5_hh(a, b, c, d, x[i+13], 4 ,  681279174);
    d = md5_hh(d, a, b, c, x[i+ 0], 11, -358537222);
    c = md5_hh(c, d, a, b, x[i+ 3], 16, -722521979);
    b = md5_hh(b, c, d, a, x[i+ 6], 23,  76029189);
    a = md5_hh(a, b, c, d, x[i+ 9], 4 , -640364487);
    d = md5_hh(d, a, b, c, x[i+12], 11, -421815835);
    c = md5_hh(c, d, a, b, x[i+15], 16,  530742520);
    b = md5_hh(b, c, d, a, x[i+ 2], 23, -995338651);

    a = md5_ii(a, b, c, d, x[i+ 0], 6 , -198630844);
    d = md5_ii(d, a, b, c, x[i+ 7], 10,  1126891415);
    c = md5_ii(c, d, a, b, x[i+14], 15, -1416354905);
    b = md5_ii(b, c, d, a, x[i+ 5], 21, -57434055);
    a = md5_ii(a, b, c, d, x[i+12], 6 ,  1700485571);
    d = md5_ii(d, a, b, c, x[i+ 3], 10, -1894986606);
    c = md5_ii(c, d, a, b, x[i+10], 15, -1051523);
    b = md5_ii(b, c, d, a, x[i+ 1], 21, -2054922799);
    a = md5_ii(a, b, c, d, x[i+ 8], 6 ,  1873313359);
    d = md5_ii(d, a, b, c, x[i+15], 10, -30611744);
    c = md5_ii(c, d, a, b, x[i+ 6], 15, -1560198380);
    b = md5_ii(b, c, d, a, x[i+13], 21,  1309151649);
    a = md5_ii(a, b, c, d, x[i+ 4], 6 , -145523070);
    d = md5_ii(d, a, b, c, x[i+11], 10, -1120210379);
    c = md5_ii(c, d, a, b, x[i+ 2], 15,  718787259);
    b = md5_ii(b, c, d, a, x[i+ 9], 21, -343485551);

    a = safe_add(a, olda);
    b = safe_add(b, oldb);
    c = safe_add(c, oldc);
    d = safe_add(d, oldd);
  }
  return Array(a, b, c, d);

}

/*
 * These functions implement the four basic operations the algorithm uses.
 */
function md5_cmn(q, a, b, x, s, t)
{
  return safe_add(bit_rol(safe_add(safe_add(a, q), safe_add(x, t)), s),b);
}
function md5_ff(a, b, c, d, x, s, t)
{
  return md5_cmn((b & c) | ((~b) & d), a, b, x, s, t);
}
function md5_gg(a, b, c, d, x, s, t)
{
  return md5_cmn((b & d) | (c & (~d)), a, b, x, s, t);
}
function md5_hh(a, b, c, d, x, s, t)
{
  return md5_cmn(b ^ c ^ d, a, b, x, s, t);
}
function md5_ii(a, b, c, d, x, s, t)
{
  return md5_cmn(c ^ (b | (~d)), a, b, x, s, t);
}

/*
 * Calculate the HMAC-MD5, of a key and some data
 */
function core_hmac_md5(key, data)
{
  var bkey = str2binl(key);
  if(bkey.length > 16) bkey = core_md5(bkey, key.length * chrsz);

  var ipad = Array(16), opad = Array(16);
  for(var i = 0; i < 16; i++)
  {
    ipad[i] = bkey[i] ^ 0x36363636;
    opad[i] = bkey[i] ^ 0x5C5C5C5C;
  }

  var hash = core_md5(ipad.concat(str2binl(data)), 512 + data.length * chrsz);
  return core_md5(opad.concat(hash), 512 + 128);
}

/*
 * Add integers, wrapping at 2^32. This uses 16-bit operations internally
 * to work around bugs in some JS interpreters.
 */
function safe_add(x, y)
{
  var lsw = (x & 0xFFFF) + (y & 0xFFFF);
  var msw = (x >> 16) + (y >> 16) + (lsw >> 16);
  return (msw << 16) | (lsw & 0xFFFF);
}

/*
 * Bitwise rotate a 32-bit number to the left.
 */
function bit_rol(num, cnt)
{
  return (num << cnt) | (num >>> (32 - cnt));
}

/*
 * Convert a string to an array of little-endian words
 * If chrsz is ASCII, characters >255 have their hi-byte silently ignored.
 */
function str2binl(str)
{
  var bin = Array();
  var mask = (1 << chrsz) - 1;
  for(var i = 0; i < str.length * chrsz; i += chrsz)
    bin[i>>5] |= (str.charCodeAt(i / chrsz) & mask) << (i%32);
  return bin;
}

/*
 * Convert an array of little-endian words to a string
 */
function binl2str(bin)
{
  var str = "";
  var mask = (1 << chrsz) - 1;
  for(var i = 0; i < bin.length * 32; i += chrsz)
    str += String.fromCharCode((bin[i>>5] >>> (i % 32)) & mask);
  return str;
}

/*
 * Convert an array of little-endian words to a hex string.
 */
function binl2hex(binarray)
{
  var hex_tab = hexcase ? "0123456789ABCDEF" : "0123456789abcdef";
  var str = "";
  for(var i = 0; i < binarray.length * 4; i++)
  {
    str += hex_tab.charAt((binarray[i>>2] >> ((i%4)*8+4)) & 0xF) +
           hex_tab.charAt((binarray[i>>2] >> ((i%4)*8  )) & 0xF);
  }
  return str;
}

/*
 * Convert an array of little-endian words to a base-64 string
 */
function binl2b64(binarray)
{
  var tab = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
  var str = "";
  for(var i = 0; i < binarray.length * 4; i += 3)
  {
    var triplet = (((binarray[i   >> 2] >> 8 * ( i   %4)) & 0xFF) << 16)
                | (((binarray[i+1 >> 2] >> 8 * ((i+1)%4)) & 0xFF) << 8 )
                |  ((binarray[i+2 >> 2] >> 8 * ((i+2)%4)) & 0xFF);
    for(var j = 0; j < 4; j++)
    {
      if(i * 8 + j * 6 > binarray.length * 32) str += b64pad;
      else str += tab.charAt((triplet >> 6*(3-j)) & 0x3F);
    }
  }
  return str;
}
/*
String 对像的扩展
functions:isImage() -> 文件名称是否为图片类型
		  hasSpecialStr() -> 是否包含特殊字符
		  isTel(str) -> 验证是否为手机号
		  isPhone()-> 是否为电话号码
		  isMail() -> 是否为邮件
		  isIDNumber() -> 是否为身份号
		  isUserName() -> 字母下画线和数字 并且字母开头 3 - 16位
		  isLastLine() -> 末字是否有下划线
		  isFirstLetter() -> 是否开头为字母
		  isIP() -> 是否是IP地址
		  isInteger() -> 检查输入对象的值是否符合整数格式
		  isNumber() -> 检查输入字符串是否符合正整数格式
		  isDecimal() -> 检查输入字符串是否是带小数的数字格式,可以是负数
		  isPort() -> 检查输入对象的值是否符合端口号格式
		  isMoney() -> 检查输入字符串是否符合金额格式
		  isNumberOr_Letter() -> 检查输入字符串是否只由英文字母和数字和下划线组成
		  isNumberOrLetter() -> 检查输入字符串是否只由英文字母和数字组成
		  isChinaOrNumbOrLett() -> 检查输入字符串是否只由汉字、字母、数字组成
		  isLastLine() -> 末字是否有下划线

*/

(function(string) {
    string.fn = string.prototype;
    //文件名称是否为图片类型
    string.fn.isImage = function() {
        var files = /\.bmp$|\.BMP$|\.gif$|\.jpg$|\.png$|\.PNG$|\.jpeg$|\.JPEG$|\.GIF$|\.JPG$\b/;
        if (!files.test(this)) {
            return false;
        }
        return true;
    };
    string.fn.hasSpecialStr = function(str) {
        var specialStrEN = new Array("~", "!", "`", "@", "#", "$", "%", "^", "&", "*", "(", ")", "-", "_", "+", "=", "\\", "|", "{", "[", "]", "}", ":", "'", "\"", ";", "<", ">", ",", ".", "?", "/");
        var specialStrCN = new Array("！", "·", "#", "￥", "%", "……", "—", "*", "（", "）", "-", "——", "。", "+", "|", "、", "《", "》", "，", "：", "；", "“", "‘", "？");
        for (var i = 0; i < specialStrEN.length; i++) {
            if (str.indexOf(specialStrEN[i]) != -1) {
                return true;
            }
        }
        for (var i = 0; i < specialStrCN.length; i++) {
            if (str.indexOf(specialStrCN[i]) != -1) {
                return true;
            }
        }
        return false;
    };
    //电话号码验证
    string.fn.isPhone = function() {
        /*
		  var reg = /^(\d{3,4})-(\d{7,8})/;
	    if( str.constructor === String ){
	       var re = str.match( reg );
	       return true;
	   }
	   return false;
   */
        this.isNumber(this) ? true: false;
    };
    //验证是否为手机号
    string.fn.isTel = function() {
        var tel = /^[0-9]{11}$/;
        if (tel.test(this)) {
            return true;
        } else {
            return false;
        }

    };
    //验证邮箱是否合法
    string.fn.isMail = function() {
		///^[-_A-Za-z0-9]+@([_A-Za-z0-9]+\.)+[A-Za-z0-9]{2,3}$/
        return (new RegExp(/^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$/).test(this));
    };
    //身份号验证
    string.fn.isIDNumber = function() {
        return (new RegExp(/^\d{15}|\d{17}[A-Z]$/).test(this));
    };
    //字母下画线和数字 并且字母开头 3 - 16位
    string.fn.isUserName = function() {
        var pattern = /^[a-zA-Z][a-zA-Z0-9_]{1,14}[a-zA-Z0-9]$/i; // 用户名只能是字母下画线和数字 并且字母开头 3 - 16位
        if (pattern.test(this)) {
            return true;
        } else {
            return false;
        }
    };
    // 末字是否有下划线	
    string.fn.isLastLine = function() {
        var patternLastChar = /^[a-zA-Z0-9_]{1,15}_$/i; // 末字符不能是下划线
        if (patternLastChar.test(this)) {
            return true;
        } else {
            return false;
        }
    };
    // 开头字符必须为字母
    string.fn.isFirstLetter = function() {
        var patternFirstChar = /^[0-9_][a-zA-Z0-9_]{1,14}$/i; // 开头字符必须为字母
        if (patternFirstChar.test(this)) {
            return true;
        } else {
            return false;
        }
    };
    /*
	 用途：校验ip地址的格式
	 输入：strIP：ip地址
	 返回：如果通过验证返回true,否则返回false；
	 */
    string.fn.isIP=function() {
        if (isNull(this)) return false;
        var re = /^(\d+)\.(\d+)\.(\d+)\.(\d+)$/g //匹配IP地址的正则表达式 
        if (re.test(this)) {
            if (RegExp.$1 < 256 && RegExp.$2 < 256 && RegExp.$3 < 256 && RegExp.$4 < 256) return true;
        }
        return false;
    };

    /* 
	 用途：检查输入对象的值是否符合整数格式
	 输入：str 输入的字符串
	 返回：如果通过验证返回true,否则返回false
	 */
    string.fn.isInteger=function() {
        var regu = /^[-]{0,1}[0-9]{1,}$/;
        return regu.test(this);
    };
    /* 
	 用途：检查输入字符串是否符合正整数格式
	 输入：
	 s：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 */
    string.fn.isNumber=function() {
        var regu = "^[0-9]+$";
        var re = new RegExp(regu);
        if (this.search(re) != -1) {
            return true;
        } else {
            return false;
        }
    };

    /* 
	 用途：检查输入字符串是否是带小数的数字格式,可以是负数
	 输入：
	 s：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 */
    string.fn.isDecimal = function() {
        if (isInteger(this)) return true;
        var re = /^[-]{0,1}(\d+)[\.]+(\d+)$/;
        if (re.test(this)) {
            if (RegExp.$1 == 0 && RegExp.$2 == 0) return false;
            return true;
        } else {
            return false;
        }
    };
    /* 
	 用途：检查输入对象的值是否符合端口号格式
	 输入：str 输入的字符串
	 返回：如果通过验证返回true,否则返回false
	 */
    string.fn.isPort = function() {
        return (isNumber(this) && str < 65536);
    },

    /* 
	 用途：检查输入字符串是否符合金额格式
	 格式定义为带小数的正数，小数点后最多三位
	 输入：
	 s：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 */
    string.fn.isMoney=function() {
        var regu = "^[0-9]+[\.][0-9]{0,3}$";
        var re = new RegExp(regu);
        if (re.test(this)) {
            return true;
        } else {
            return false;
        }
    };
    /* 
	 用途：检查输入字符串是否只由英文字母和数字和下划线组成
	 输入：
	 s：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 */
    string.fn.isNumberOr_Letter=function() { 
        var regu = "^[0-9a-zA-Z\_]+$";
        var re = new RegExp(regu);
        if (re.test(this)) {
            return true;
        } else {
            return false;
        }
    };
    /* 
	 用途：检查输入字符串是否只由英文字母和数字组成
	 输入：
	 s：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 */
    string.fn.isNumberOrLetter=function() { //判断是否是数字或字母 
        var regu = "^[0-9a-zA-Z]+$";
        var re = new RegExp(regu);
        if (re.test(this)) {
            return true;
        } else {
            return false;
        }
    };
    /* 
	 用途：检查输入字符串是否只由汉字、字母、数字组成
	 输入：
	 ＆#118alue：字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 */
     string.fn.isChinaOrNumbOrLett = function() { //判断是否是汉字、字母、数字组成 
        var regu = "^[0-9a-zA-Z\u4e00-\u9fa5]+$";
        var re = new RegExp(regu);
        if (re.test(this)) {
            return true;
        } else {
            return false;
        }
    };
    /* 
	 用途：判断是否是日期
	 输入：date：日期；fmt：日期格式
	 返回：如果通过验证返回true,否则返回false
	 */
    string.fn.isDate=function(fmt) {
        if (fmt == null) fmt = "yyyyMMdd";
        var yIndex = fmt.indexOf("yyyy");
        if (yIndex == -1) return false;
        var year = this.substring(yIndex, yIndex + 4);
        var mIndex = fmt.indexOf("MM");
        if (mIndex == -1) return false;
        var month = this.substring(mIndex, mIndex + 2);
        var dIndex = fmt.indexOf("dd");
        if (dIndex == -1) return false;
        var day = this.substring(dIndex, dIndex + 2);
        if (!isNumber(year) || year > "2100" || year < "1900") return false;
        if (!isNumber(month) || month > "12" || month < "01") return false;
        if (day > getMaxDay(year, month) || day < "01") return false;
        return true;
    };

    string.fn.getMaxDay= function(year, month) {
        if (month == 4 || month == 6 || month == 9 || month == 11) return "30";
        if (month == 2) if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0) return "29";
        else return "28";
        return "31";
    };
    /* 
	 用途：检查输入的起止日期是否正确，规则为两个日期的格式正确，
	 且结束如期>=起始日期
	 输入：
	 startDate：起始日期，字符串
	 endDate：结束如期，字符串
	 返回：
	 如果通过验证返回true,否则返回false
	 */
    string.fn.isBetweenDate= function(startDate, endDate) {
        if (!isDate(startDate)) {
           // alert("起始日期不正确!");
            return false;
        } else if (!isDate(endDate)) {
          //  alert("终止日期不正确!");
            return false;
        } else if (startDate > endDate) {
            //alert("起始日期不能大于终止日期!");
            return false;
        }
		else if(this > startDate && this < endDate)
		{
			return true;
		}
        return false;
    };

} (String))﻿/*
    http://www.JSON.org/json2.js
    2008-11-19

    Public Domain.

    NO WARRANTY EXPRESSED OR IMPLIED. USE AT YOUR OWN RISK.

    See http://www.JSON.org/js.html

    This file creates a global JSON object containing two methods: stringify
    and parse.

        JSON.stringify(value, replacer, space)
            value       any JavaScript value, usually an object or array.

            replacer    an optional parameter that determines how object
                        values are stringified for objects. It can be a
                        function or an array of strings.

            space       an optional parameter that specifies the indentation
                        of nested structures. If it is omitted, the text will
                        be packed without extra whitespace. If it is a number,
                        it will specify the number of spaces to indent at each
                        level. If it is a string (such as '\t' or '&nbsp;'),
                        it contains the characters used to indent at each level.

            This method produces a JSON text from a JavaScript value.

            When an object value is found, if the object contains a toJSON
            method, its toJSON method will be called and the result will be
            stringified. A toJSON method does not serialize: it returns the
            value represented by the name/value pair that should be serialized,
            or undefined if nothing should be serialized. The toJSON method
            will be passed the key associated with the value, and this will be
            bound to the object holding the key.

            For example, this would serialize Dates as ISO strings.

                Date.prototype.toJSON = function (key) {
                    function f(n) {
                        // Format integers to have at least two digits.
                        return n < 10 ? '0' + n : n;
                    }

                    return this.getUTCFullYear()   + '-' +
                         f(this.getUTCMonth() + 1) + '-' +
                         f(this.getUTCDate())      + 'T' +
                         f(this.getUTCHours())     + ':' +
                         f(this.getUTCMinutes())   + ':' +
                         f(this.getUTCSeconds())   + 'Z';
                };

            You can provide an optional replacer method. It will be passed the
            key and value of each member, with this bound to the containing
            object. The value that is returned from your method will be
            serialized. If your method returns undefined, then the member will
            be excluded from the serialization.

            If the replacer parameter is an array of strings, then it will be
            used to select the members to be serialized. It filters the results
            such that only members with keys listed in the replacer array are
            stringified.

            Values that do not have JSON representations, such as undefined or
            functions, will not be serialized. Such values in objects will be
            dropped; in arrays they will be replaced with null. You can use
            a replacer function to replace those with JSON values.
            JSON.stringify(undefined) returns undefined.

            The optional space parameter produces a stringification of the
            value that is filled with line breaks and indentation to make it
            easier to read.

            If the space parameter is a non-empty string, then that string will
            be used for indentation. If the space parameter is a number, then
            the indentation will be that many spaces.

            Example:

            text = JSON.stringify(['e', {pluribus: 'unum'}]);
            // text is '["e",{"pluribus":"unum"}]'


            text = JSON.stringify(['e', {pluribus: 'unum'}], null, '\t');
            // text is '[\n\t"e",\n\t{\n\t\t"pluribus": "unum"\n\t}\n]'

            text = JSON.stringify([new Date()], function (key, value) {
                return this[key] instanceof Date ?
                    'Date(' + this[key] + ')' : value;
            });
            // text is '["Date(---current time---)"]'


        JSON.parse(text, reviver)
            This method parses a JSON text to produce an object or array.
            It can throw a SyntaxError exception.

            The optional reviver parameter is a function that can filter and
            transform the results. It receives each of the keys and values,
            and its return value is used instead of the original value.
            If it returns what it received, then the structure is not modified.
            If it returns undefined then the member is deleted.

            Example:

            // Parse the text. Values that look like ISO date strings will
            // be converted to Date objects.

            myData = JSON.parse(text, function (key, value) {
                var a;
                if (typeof value === 'string') {
                    a =
/^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2}(?:\.\d*)?)Z$/.exec(value);
                    if (a) {
                        return new Date(Date.UTC(+a[1], +a[2] - 1, +a[3], +a[4],
                            +a[5], +a[6]));
                    }
                }
                return value;
            });

            myData = JSON.parse('["Date(09/09/2001)"]', function (key, value) {
                var d;
                if (typeof value === 'string' &&
                        value.slice(0, 5) === 'Date(' &&
                        value.slice(-1) === ')') {
                    d = new Date(value.slice(5, -1));
                    if (d) {
                        return d;
                    }
                }
                return value;
            });


    This is a reference implementation. You are free to copy, modify, or
    redistribute.

    This code should be minified before deployment.
    See http://javascript.crockford.com/jsmin.html

    USE YOUR OWN COPY. IT IS EXTREMELY UNWISE TO LOAD CODE FROM SERVERS YOU DO
    NOT CONTROL.
*/

/*jslint evil: true */

/*global JSON */

/*members "", "\b", "\t", "\n", "\f", "\r", "\"", JSON, "\\", apply,
    call, charCodeAt, getUTCDate, getUTCFullYear, getUTCHours,
    getUTCMinutes, getUTCMonth, getUTCSeconds, hasOwnProperty, join,
    lastIndex, length, parse, prototype, push, replace, slice, stringify,
    test, toJSON, toString, valueOf
*/

// Create a JSON object only if one does not already exist. We create the
// methods in a closure to avoid creating global variables.

if (!this.JSON) {
    JSON = {};
}
(function () {

    function f(n) {
        // Format integers to have at least two digits.
        return n < 10 ? '0' + n : n;
    }

    if (typeof Date.prototype.toJSON !== 'function') {

        Date.prototype.toJSON = function (key) {

            return this.getUTCFullYear()   + '-' +
                 f(this.getUTCMonth() + 1) + '-' +
                 f(this.getUTCDate())      + 'T' +
                 f(this.getUTCHours())     + ':' +
                 f(this.getUTCMinutes())   + ':' +
                 f(this.getUTCSeconds())   + 'Z';
        };

        String.prototype.toJSON =
        Number.prototype.toJSON =
        Boolean.prototype.toJSON = function (key) {
            return this.valueOf();
        };
    }

    var cx = /[\u0000\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g,
        escapable = /[\\\"\x00-\x1f\x7f-\x9f\u00ad\u0600-\u0604\u070f\u17b4\u17b5\u200c-\u200f\u2028-\u202f\u2060-\u206f\ufeff\ufff0-\uffff]/g,
        gap,
        indent,
        meta = {    // table of character substitutions
            '\b': '\\b',
            '\t': '\\t',
            '\n': '\\n',
            '\f': '\\f',
            '\r': '\\r',
            '"' : '\\"',
            '\\': '\\\\'
        },
        rep;


    function quote(string) {

// If the string contains no control characters, no quote characters, and no
// backslash characters, then we can safely slap some quotes around it.
// Otherwise we must also replace the offending characters with safe escape
// sequences.

        escapable.lastIndex = 0;
        return escapable.test(string) ?
            '"' + string.replace(escapable, function (a) {
                var c = meta[a];
                return typeof c === 'string' ? c :
                    '\\u' + ('0000' + a.charCodeAt(0).toString(16)).slice(-4);
            }) + '"' :
            '"' + string + '"';
    }


    function str(key, holder) {

// Produce a string from holder[key].

        var i,          // The loop counter.
            k,          // The member key.
            v,          // The member value.
            length,
            mind = gap,
            partial,
            value = holder[key];

// If the value has a toJSON method, call it to obtain a replacement value.

        if (value && typeof value === 'object' &&
                typeof value.toJSON === 'function') {
            value = value.toJSON(key);
        }

// If we were called with a replacer function, then call the replacer to
// obtain a replacement value.

        if (typeof rep === 'function') {
            value = rep.call(holder, key, value);
        }

// What happens next depends on the value's type.

        switch (typeof value) {
        case 'string':
            return quote(value);

        case 'number':

// JSON numbers must be finite. Encode non-finite numbers as null.

            return isFinite(value) ? String(value) : 'null';

        case 'boolean':
        case 'null':

// If the value is a boolean or null, convert it to a string. Note:
// typeof null does not produce 'null'. The case is included here in
// the remote chance that this gets fixed someday.

            return String(value);

// If the type is 'object', we might be dealing with an object or an array or
// null.

        case 'object':

// Due to a specification blunder in ECMAScript, typeof null is 'object',
// so watch out for that case.

            if (!value) {
                return 'null';
            }

// Make an array to hold the partial results of stringifying this object value.

            gap += indent;
            partial = [];

// Is the value an array?

            if (Object.prototype.toString.apply(value) === '[object Array]') {

// The value is an array. Stringify every element. Use null as a placeholder
// for non-JSON values.

                length = value.length;
                for (i = 0; i < length; i += 1) {
                    partial[i] = str(i, value) || 'null';
                }

// Join all of the elements together, separated with commas, and wrap them in
// brackets.

                v = partial.length === 0 ? '[]' :
                    gap ? '[\n' + gap +
                            partial.join(',\n' + gap) + '\n' +
                                mind + ']' :
                          '[' + partial.join(',') + ']';
                gap = mind;
                return v;
            }

// If the replacer is an array, use it to select the members to be stringified.

            if (rep && typeof rep === 'object') {
                length = rep.length;
                for (i = 0; i < length; i += 1) {
                    k = rep[i];
                    if (typeof k === 'string') {
                        v = str(k, value);
                        if (v) {
                            partial.push(quote(k) + (gap ? ': ' : ':') + v);
                        }
                    }
                }
            } else {

// Otherwise, iterate through all of the keys in the object.

                for (k in value) {
                    if (Object.hasOwnProperty.call(value, k)) {
                        v = str(k, value);
                        if (v) {
                            partial.push(quote(k) + (gap ? ': ' : ':') + v);
                        }
                    }
                }
            }

// Join all of the member texts together, separated with commas,
// and wrap them in braces.

            v = partial.length === 0 ? '{}' :
                gap ? '{\n' + gap + partial.join(',\n' + gap) + '\n' +
                        mind + '}' : '{' + partial.join(',') + '}';
            gap = mind;
            return v;
        }
    }

// If the JSON object does not yet have a stringify method, give it one.

    if (typeof JSON.stringify !== 'function') {
        JSON.stringify = function (value, replacer, space) {

// The stringify method takes a value and an optional replacer, and an optional
// space parameter, and returns a JSON text. The replacer can be a function
// that can replace values, or an array of strings that will select the keys.
// A default replacer method can be provided. Use of the space parameter can
// produce text that is more easily readable.

            var i;
            gap = '';
            indent = '';

// If the space parameter is a number, make an indent string containing that
// many spaces.

            if (typeof space === 'number') {
                for (i = 0; i < space; i += 1) {
                    indent += ' ';
                }

// If the space parameter is a string, it will be used as the indent string.

            } else if (typeof space === 'string') {
                indent = space;
            }

// If there is a replacer, it must be a function or an array.
// Otherwise, throw an error.

            rep = replacer;
            if (replacer && typeof replacer !== 'function' &&
                    (typeof replacer !== 'object' ||
                     typeof replacer.length !== 'number')) {
                throw new Error('JSON.stringify');
            }

// Make a fake root object containing our value under the key of ''.
// Return the result of stringifying the value.

            return str('', {'': value});
        };
    }


// If the JSON object does not yet have a parse method, give it one.

    if (typeof JSON.parse !== 'function') {
        JSON.parse = function (text, reviver) {

// The parse method takes a text and an optional reviver function, and returns
// a JavaScript value if the text is a valid JSON text.

            var j;

            function walk(holder, key) {

// The walk method is used to recursively walk the resulting structure so
// that modifications can be made.

                var k, v, value = holder[key];
                if (value && typeof value === 'object') {
                    for (k in value) {
                        if (Object.hasOwnProperty.call(value, k)) {
                            v = walk(value, k);
                            if (v !== undefined) {
                                value[k] = v;
                            } else {
                                delete value[k];
                            }
                        }
                    }
                }
                return reviver.call(holder, key, value);
            }


// Parsing happens in four stages. In the first stage, we replace certain
// Unicode characters with escape sequences. JavaScript handles many characters
// incorrectly, either silently deleting them, or treating them as line endings.

            cx.lastIndex = 0;
            if (cx.test(text)) {
                text = text.replace(cx, function (a) {
                    return '\\u' +
                        ('0000' + a.charCodeAt(0).toString(16)).slice(-4);
                });
            }

// In the second stage, we run the text against regular expressions that look
// for non-JSON patterns. We are especially concerned with '()' and 'new'
// because they can cause invocation, and '=' because it can cause mutation.
// But just to be safe, we want to reject all unexpected forms.

// We split the second stage into 4 regexp operations in order to work around
// crippling inefficiencies in IE's and Safari's regexp engines. First we
// replace the JSON backslash pairs with '@' (a non-JSON character). Second, we
// replace all simple value tokens with ']' characters. Third, we delete all
// open brackets that follow a colon or comma or that begin the text. Finally,
// we look to see that the remaining characters are only whitespace or ']' or
// ',' or ':' or '{' or '}'. If that is so, then the text is safe for eval.

            if (/^[\],:{}\s]*$/.
test(text.replace(/\\(?:["\\\/bfnrt]|u[0-9a-fA-F]{4})/g, '@').
replace(/"[^"\\\n\r]*"|true|false|null|-?\d+(?:\.\d*)?(?:[eE][+\-]?\d+)?/g, ']').
replace(/(?:^|:|,)(?:\s*\[)+/g, ''))) {

// In the third stage we use the eval function to compile the text into a
// JavaScript structure. The '{' operator is subject to a syntactic ambiguity
// in JavaScript: it can begin a block or an object literal. We wrap the text
// in parens to eliminate the ambiguity.

                j = eval('(' + text + ')');

// In the optional fourth stage, we recursively walk the new structure, passing
// each name/value pair to a reviver function for possible transformation.

                return typeof reviver === 'function' ?
                    walk({'': j}, '') : j;
            }

// If the text is not JSON parseable, then a SyntaxError is thrown.

            throw new SyntaxError('JSON.parse');
        };
    }
})();
