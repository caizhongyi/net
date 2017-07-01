function getHttpRequest() {
	var xmlreq = false;
	if (window.XMLHttpRequest) {
		xmlreq = new XMLHttpRequest();
	} else if (window.ActiveXObject) {
		try {
		  xmlreq = new ActiveXObject("Msxml2.XMLHTTP");
		} catch (e1) {
		  try {
		    xmlreq = new ActiveXObject("Microsoft.XMLHTTP");
		  } catch (e2) {
		     xmlreq = false;
		  }
		}
	}
	return xmlreq;
}
function showCity(id,uri){
	if('0'!=id){
		var req = getHttpRequest();
		if(req ){
			req.onreadystatechange = getCitys(req);
			var url = uri+'?id='+id
			req.open("POST",url,true );
				req.send(null);
		}
	}
}
function getCitys(req){
	return function(){
		if (req.readyState == 4) {
			if (req.status == 200) {
				var xmlDoc=req.responseXML.getElementsByTagName("city");
	            var select_root=document.getElementById('city');
	            select_root.options.length=0;
	          //  var opt1 = new Option('Ñ¡Ôñ³ÇÊÐ','');
	            //document.myform.city.options.add(opt1);
	            //select_root.add(opt1);
	            for(var i=0;i< xmlDoc.length;i++)
	            {
	                var xText=xmlDoc[i].childNodes[0].firstChild.nodeValue;
	                var xValue=xmlDoc[i].childNodes[1].firstChild.nodeValue;
	                var option=new Option(xText,xValue);
	                try{
	                	document.myform.city.options.add(option);
	                    //select_root.add(option);
	                }catch(e){
	                }
	            }
			}
		}
	}
}