if(typeof(czyjs.AjaxHelper)=="undefined")
{
    czyjs.AjaxHelper = {};
}
czyjs.AjaxHelper.MyAjax = Class.create();
czyjs.AjaxHelper.MyAjax.prototype = {
	initialize: function(url, param, fun,Match){
    if (Match.toLowerCase  == "post") {
		this.func = BindAsEventListener(this, function(){
			this.SendRequestQueryByPost(url, param, fun, true);
		});
	}
	else
	{
		this.func = BindAsEventListener(this, function(){
			this.SendRequestQuery(url, param, fun, true);
		});
	}
	this.func();
	},
	
	XHConn: function(){
		var xmlhttp;
		try {
			xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
		} 
		catch (e) {
			try {
				xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
			} 
			catch (e) {
				try {
					xmlhttp = new XMLHttpRequest();
				} 
				catch (e) {
					xmlhttp = false;
				}
			}
		}
		
		return xmlhttp;
	},

	//ajax示例获取XML示例(Get传参)
	//*********************************************************************
	//url页面路径
	//param参数，如果为空则无参数,格式为&param1=parm&param2=param
	//processResponse回调函数
	//Asy同异步,true为异步，false为同步
	//*********************************************************************
	SendRequestQuery: function(url, param, processResponse, Asy){
		var oXmlHttp = this.XHConn();
		var request_url = url + "?random =" + Math.random() * 10000 + param;
		 
		try {
			
			oXmlHttp.open("Get", request_url, Asy);
			
			//oXmlHttp.setRequestHeader('content-type', 'text/xml');
			//oXmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded;");
			oXmlHttp.send(null);
		
			oXmlHttp.onreadystatechange = function()
			{	 
				 if (oXmlHttp.readyState == 4 && oXmlHttp.status == 200) {
				 	
				     var data=oXmlHttp.responseText;
					
				     processResponse(data);
				 }
			}
		
		} 
		catch (e) {
			return true;
		}
	},
//ajax示例获取XML示例(Get传参)
	//*********************************************************************
	//url页面路径
	//param参数，如果为空则无参数,格式为&param1=parm&param2=param
	//processResponse回调函数
	//Asy同异步,true为异步，false为同步
	//*********************************************************************
	SendRequestQueryByPost: function(url, param, processResponse, Asy){
		var oXmlHttp = this.XHConn();
		var request_url = url + "?random =" + Math.random() * 10000  ;
		
		try {
		
			oXmlHttp.open("Post", request_url, Asy);
			
			//oXmlHttp.setRequestHeader('content-type', 'text/xml');
			//oXmlHttp.setRequestHeader("Content-Type","application/x-www-form-urlencoded;");
			oXmlHttp.send(param);
			
			oXmlHttp.onreadystatechange = function(){
				if (oXmlHttp.readyState == 4 && oXmlHttp.status == 200) {
				
					var data = oXmlHttp.responseText;
					
					processResponse(data);
				}
			}
			
		} 
		catch (e) {
			return true;
		}
	}
}
////////////////////////////////////////////////////////////////////////////////////////
var Table = Class.create();
Table.prototype ={
	 id:"",
	 name:"",
	 initialize: function()
	 {}
}
//ajax回调函数
//*********************************************************************
//url页面路径
//param参数，如果为空则无参数,格式为&param1=parm&param2=param
//processResponse回调函数
//Asy同异步,true为异步，false为同步
//*********************************************************************
function ProcessResponseXML ()
{  
	var oXmlHttp=XHC ;
	
    if (oXmlHttp.readyState == 4 && oXmlHttp.status == 200) {
            
              //获取列表
              var  resoultTxt = oXmlHttp.responseText;
              //var resoultXML = oXmlHttp.responseXml;
		
			  var contetName='TextContet';
			  var tableName='Table';
			  var pageTableName="PageTable";
			  var listObj = resoultXML.getElementsByTagName(tableName);
				
              var myRepObj= document.getElementById(contetName);
              //dt_totalPage=page[0].childNodes[0].text;
                

              var pageListObj = resoultXML.getElementsByTagName(pageTableName);
			  var table	=new Table();
				  for (var i = 0; i < listObj.length; i++) {
				    var NcNo;
				  	if (listObj[i].selectSingleNode("sm_id") != null) {
				  		Table.id = listObj[i].selectSingleNode("sm_id").text;

				  	}
					myRepObj.innerHTML+=Table.id;
				  }
              

      
               
               //html+="<div onclick='GetGoods(currentPage,type,orderType);'>"+name+"</div>";
               //}
               
           
           }
}
//回调
	function DataRequest(data){
	
		var json = data;
		var list = eval("(" + json + ")");
		for (var i = 0; i < list.Table.length; i++) {
		
			var btn1 = document.getElementById(list.Table[i].cz_name + "1");
			var btn2 = document.getElementById(list.Table[i].cz_name + "2");
			var btn3 = document.getElementById(list.Table[i].cz_name + "3");
			btn1.value = list.Table[i].cz_a;
			btn2.value = list.Table[i].cz_b;
			btn3.value = list.Table[i].cz_c;
		}
	}
	


///////////////////////////////////////////////////////////////////////////////////////////////////

// JavaScript Document
var xmlHttp;//============
function createXmlHttpRequest(url)
{
			
        try
        {
           xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
        }catch(e)
        {
                try
                {
                    xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
                }catch(E)
                {
                    xmlHttp = false;
                }
        }    
            
        if(!xmlHttp && typeof XMLHttpRequest != "undefined")
        {
                xmlHttp = new XMLHttpRequest();
				if(xmlHttp.overrideMimeType)
				{
					 xmlHttp.overrideMimeType('text/xml'); 
				}

				
        }
		
}
//调用连接
function connection(url,reqxml,fun)
{
        createXmlHttpRequest();/////////
    	//设置回调的方法
        xmlHttp.onreadystatechange = fun;
        xmlHttp.open("post", url, true);
	    xmlHttp.setRequestHeader('content-type', 'text/xml');
        xmlHttp.send(reqxml);
}
//调用连接
function connectionHtml(url,reqxml,fun)
{
        createXmlHttpRequest();/////////
    	//设置回调的方法
        xmlHttp.onreadystatechange = fun;
        xmlHttp.open("post", url, true);
	    xmlHttp.setRequestHeader('content-type', 'application/x-www-form-urlencoded');
        xmlHttp.send(reqxml);
}
//////////////////////////////////////////////////////////////////////////////////////////////////////////
