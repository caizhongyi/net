
var key='';
var str=new Array();

function createAjaxObj(){
	var httprequest=false
	if (window.XMLHttpRequest){ // if Mozilla, Safari etc
		httprequest=new XMLHttpRequest()
		if (httprequest.overrideMimeType)
			httprequest.overrideMimeType('text/xml');
	}
	else if (window.ActiveXObject){ // if IE
		try 
		{
			httprequest=new ActiveXObject("Msxml2.XMLHTTP");
		} 
		catch (e)
		{
			try
			{
				httprequest=new ActiveXObject("Microsoft.XMLHTTP");
			}
			catch (e){}
		}
	}
	return httprequest;
}

function load_feedback(arcID,pg){

/*
	var url = "http://127.0.0.1/plus/feedback_ajax.php?action=show&arcID="+arcID+"&pg="+pg;
    var ajax = new oAjax();
    // 是否显示错误
    //ajax.error = true;
    // 请求的页面的编码为'gb2312'或空
    //ajax.encode = 'gb2312';
    // 回调函数
    ajax.callback = function (content) {
        // 处理返回内容
		eval('var obj = ' + content);
		document.getElementById('fedbk').innerHTML=obj.a;
		document.getElementById('fedcount').innerHTML=obj.b;
    };
    ajax.send(url);
*/

	var xmlhttp = createAjaxObj();
	try
	{

		var params="action=show&arcID="+arcID+"&pg="+pg;
		xmlhttp.abort();	
		
		xmlhttp.open("get","http://127.0.0.1/plus/feedback_ajax.php?"+params,true);
	
		xmlhttp.setRequestHeader("Content-type", "text/html;charset=gb2312");	
		
		xmlhttp.setRequestHeader("If-Modified-Since","0");	
	
		xmlhttp.setRequestHeader("Content-length", params.length);
		
		xmlhttp.setRequestHeader("Connection", "close");
	
		xmlhttp.onreadystatechange=f

		xmlhttp.send(null);	
	
	}catch(ex){}
	function f()
	{
		
			if(xmlhttp.readyState!= 4 || xmlhttp.status!=200 )
				return ;
			var b= xmlhttp.responseText;
			var obj = eval("("+b+")");          
			document.getElementById('fedbk').innerHTML=obj.a;
			document.getElementById('fedcount').innerHTML=obj.b;
			document.getElementById('feedcounttop').innerHTML=obj.b;
	}
}


function load_allfeedback(arcID,pg){

	var xmlhttp = createAjaxObj();
	try
	{

		var params="action=showall&arcID="+arcID+"&pg="+pg;
		xmlhttp.abort();	
		
		xmlhttp.open("get","http://127.0.0.1/plus/feedback_ajax.php?"+params,true);
	
		xmlhttp.setRequestHeader("Content-type", "text/html;charset=gb2312");	
		
		xmlhttp.setRequestHeader("If-Modified-Since","0");	
	
		xmlhttp.setRequestHeader("Content-length", params.length);
		
		xmlhttp.setRequestHeader("Connection", "close");
	
		xmlhttp.onreadystatechange=f

		xmlhttp.send(null);	
	
	}catch(ex){}
	function f()
	{
		
			if(xmlhttp.readyState!= 4 || xmlhttp.status!=200 )
				return ;
			var content= xmlhttp.responseText;
			var obj = eval("("+content+")");      
           // eval('var obj = ' + content);    
			document.getElementById('fedbk').innerHTML=obj.a;
			//document.getElementById('fedcount').innerHTML=obj.b;
	}
}

function feed_back(){
	
	var msg   = trim(document.getElementById('msg').value);
	var arcID = document.getElementById('arcID').value;
	var username = trim(document.getElementById('username').value);
	var pwd   = trim(document.getElementById('pwd').value);
	var checkbox = document.getElementById('notuser');
	if(checkbox.checked)
		var notuser = 1;
	else
		var notuser = 0;	
		

	var xmlhttp = createAjaxObj();
	try
	{
		
		params="action=send&arcID="+arcID+"&msg="+msg+"&username="+username
				+"&pwd="+pwd+"&notuser="+notuser;
			
		xmlhttp.abort();	
		
		xmlhttp.open("get","http://127.0.0.1/plus/feedback_ajax.php?"+params,true);
	
		xmlhttp.setRequestHeader("Content-type", "text/html;charset=gb2312");	
		
		xmlhttp.setRequestHeader("If-Modified-Since","0");	
	
		xmlhttp.setRequestHeader("Content-length", params.length);
		
		xmlhttp.setRequestHeader("Connection", "close");
	
		xmlhttp.onreadystatechange=f

		xmlhttp.send(null);	
	
	}catch(ex){}
	function f()
	{	
			if(xmlhttp.readyState!= 4 || xmlhttp.status!=200 )
				return ;
				var arr = xmlhttp.responseText.split("@:");
				
				if(!arr[1]){
					alert(xmlhttp.responseText);
				}
				else{	
					if(arr[0] != 'true')
						alert(arr[0]);
					alert(arr[2]);
					ck_yzimg();	
					load_feedback(arr[1],1);
				}
	}
	
	
	
}

function goodbad(fid,arcid,actname){
	
	
	var xmlhttp = createAjaxObj();
	
	try
	{
		params = "action="+actname+"&fid="+fid+"&arcID="+arcid;
		
		xmlhttp.abort();	
		
		xmlhttp.open("get","http://127.0.0.1/plus/feedback_ajax.php?"+params,true);
	
		xmlhttp.setRequestHeader("Content-type", "text/html;charset=gb2312");	
		
		xmlhttp.setRequestHeader("If-Modified-Since","0");	
	
		xmlhttp.setRequestHeader("Content-length", params.length);
		
		xmlhttp.setRequestHeader("Connection", "close");
	
		xmlhttp.onreadystatechange=f

		xmlhttp.send(null);	
	
	}catch(ex){}
	function f()
	{	
			if(xmlhttp.readyState!= 4 || xmlhttp.status!=200 )
				return ;
				var arr = xmlhttp.responseText.split("@:");
				
				if(!arr[1])
					alert(xmlhttp.responseText);
				else{	
					alert(arr[0]);
					load_feedback(arr[1]);
				}
	}
	
}

function goodbad2(fid,arcid,actname){
	
	
	var xmlhttp = createAjaxObj();
	
	try
	{
		params = "action="+actname+"&fid="+fid+"&arcID="+arcid;
		
		xmlhttp.abort();	
		
		xmlhttp.open("get","http://127.0.0.1/plus/feedback_ajax.php?"+params,true);
	
		xmlhttp.setRequestHeader("Content-type", "text/html;charset=gb2312");	
		
		xmlhttp.setRequestHeader("If-Modified-Since","0");	
	
		xmlhttp.setRequestHeader("Content-length", params.length);
		
		xmlhttp.setRequestHeader("Connection", "close");
	
		xmlhttp.onreadystatechange=f

		xmlhttp.send(null);	
	
	}catch(ex){}
	function f()
	{	
			if(xmlhttp.readyState!= 4 || xmlhttp.status!=200 )
				return ;
				var arr = xmlhttp.responseText.split("@:");
				
				if(!arr[1])
					alert(xmlhttp.responseText);
				else{	
					alert(arr[0]);
					load_allfeedback(arr[1]);
				}
	}
	
}

function pg(vl){
	var arcID = document.getElementById('arcID').value;
	load_feedback(arcID,vl);
}

function ck_yzimg(){
	//document.all.cknum.value='';
	document.all.msg.value='';
	document.all.username.value='';
	document.all.pwd.value='';
	//document.all.img1.onclick();
}

function trim(s) {
 return s.replace( /^\s*/, "" ).replace( /\s*$/, "" );

}

//////
/*
String.prototype.Contains = function(str) {
    return (this.indexOf(str) > -1);
};
var Browser = {
    s : navigator.userAgent.toLowerCase()
};
(function (b) {
    b.IsIE     = b.s.Contains('msie');
    b.IsIE5    = b.s.Contains('msie 5');
    b.IsIE6    = b.s.Contains('msie 6');
    b.IsIE7    = b.s.Contains('msie 7');
    b.IsIE56   = !b.IsIE7 && (b.IsIE6 || b.IsIE5);
    b.IsGecko  = b.s.Contains('gecko');
    b.IsSafari = b.s.Contains('safari');
    b.IsOpera  = b.s.Contains('opera');
    b.IsMac    = b.s.Contains('macintosh');

    b.IsIELike = (b.IsIE || b.IsOpera);
	b.IsGeckoLike = (b.IsGecko || b.IsSafari);
}) (Browser);
function oAjax () {
    this.req = null;
    this.url = '';
    this.content = '';
    this.type = 'text';
    this.encode = '';
    this.asyn = true;
    this.action = 'get';
    this.error = false;
}
oAjax.prototype.init = function () {
    if (window.XMLHttpRequest) {
        this.req = new XMLHttpRequest();
    }
    else if (window.ActiveXObject) {
        // isIE = true;
        try {
            this.req = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e) {
            try {
                this.req = new ActiveXObject("Microsoft.XMLHTTP");
            }
            catch(e) {
                this.req = false;
            }
        }
    }
    var self = this;
    if (this.req) {
        this.req.onreadystatechange = function () {self.listener()};
    }
};

oAjax.prototype.listener = function () {
    if (this.req.readyState == 4) {
        if (this.req.status == 200) {
            // right
            try {
                this.callback(Browser.IsIE && this.encode == 'gb2312' ? oAjax.gb2utf8(this.req.responseBody) : (this.type == 'text' ? this.req.responseText : this.req.responseXML));
            }
            catch (e) {
                this.halt('[callback] ' + e.name + ':' + e.message);
            }
        }
        else {
            // error
            this.halt('[callback error] ' + this.req.status);
        }
    }
};

oAjax.prototype.send = function (url) {
    this.init();

    url = this.url = url || this.url || '';
    this.content = !!this.content ? this.content : '';
    this.encode = this.encode ? this.encode.toLowerCase() : '';
    this.asyn = this.asyn == undefined ? true : !!this.asyn;
    this.action = (this.action == undefined || this.action == 'get') ? 'Get' : 'Post';
    this.error = this.error == undefined ? false : !!this.error;

    if (! url && this.error) {
        alert('Ajax请求URL不能为空。');
        return;
    }
    try {
        this.req.open(this.action, url, this.asyn);
    }
    catch (e) {
        this.halt('[open] ' + e.name + ':' + e.message);
        return;
    }
    try {
        this.req.setRequestHeader('Connection', 'close');
        this.req.setRequestHeader('Accept-Encoding', 'gzip, deflate');
        this.req.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded' + (this.encode ? ';charset=' + this.encode : ''));
        if(this.req.overrideMimeType && this.encode) {
            this.req.overrideMimeType('text/xml' + (this.encode ? ';charset=' + this.encode : ''));
        }
        this.req.send(this.content);
    }
    catch (e) {
        this.halt('[open] ' + e.name + ':' + e.message + '\n** 检查是否为跨域访问。');
    }
};

oAjax.prototype.callback = function (content) {
    //alert(content);
};

    // abort
oAjax.prototype.abort = function () {
    this.req.abort();
};

oAjax.prototype.halt = function (description) {
    this.error && alert(description);
};

// gb2312 to utf8
oAjax.gb2utf8 = function (data) {
    var glbEncode = [];
    gb2utf8_data = data;
    execScript("gb2utf8_data = MidB(gb2utf8_data, 1)", "VBScript");
    var t = escape(gb2utf8_data).replace(/%u/g,"").replace(/(.{2})(.{2})/g,"%$2%$1").replace(/%([A-Z].)%(.{2})/g,"@$1$2");
    t = t.split("@");
    var i=0, j = t.length, k;
    while(++i < j) {
        k = t[i].substring(0,4);
        if(!glbEncode[k]) {
            gb2utf8_char = eval("0x" + k);
            execScript("gb2utf8_char = Chr(gb2utf8_char)", "VBScript");
            glbEncode[k] = escape(gb2utf8_char).substring(1, 6);
        }
        t[i] = glbEncode[k] + t[i].substring(4);
    }
    gb2utf8_data = gb2utf8_char = null;
    return unescape(t.join("%"));
}
*/