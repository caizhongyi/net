if(typeof(czyjs.Element!="undefined"))
{
	czyjs.Element={};
}

//获取iframe对像
czyjs.Element.getiframe=function(ifre){
       
       var doc;
        if (document.all){//IE
                doc = document.frames[ifre].document;
        }else{//Firefox    
                doc = document.getElementById(ifre).contentDocument;
        }
        return doc;

      
    }

//创建无素对像
czyjs.Element.CreateElement=function(jsonParam)
{
	 var param={
	 	id:jsonParam.id, //元素ID
		name:jsonParam.name, //无素名称
		className:jsonParam.className, //样式选择器名称
		contenter:document.getElementById(jsonParam.contenterId), // 为body则为body对像,为
		elementType:jsonParam.elementType  //无素类型
	 }
	 
     if(document.getElementById(param.id)==null)
     {	 
	     var obj=document.createElement(param.elementType);//
	     obj.id=param.id;
		 obj.name=param.name;
		 obj.className=param.className;
	 	 if(param.contenter !=null && param.contenter !='' )
		 {
		 	param.contenter.appendChild(obj);
		 }
	     else
		 {    
		 	 document.body.firstChild.appendChild (obj);//在body内添加该div对象
		 }
	    
     }
	
}
//删除对像
czyjs.Element.DeleteElement=function(id)
{ 
  var obj=document.getElmenetById(id);
  if(obj)
  {
    return   obj.parentNode.removeChild(obj);
  }
  else 
  {
  	return null;
  }
}

//获取父级元素
czyjs.Element.GetParentElement=function (obj)
{
      return obj.parentElement.parentElement;
}
//获取子级元素
czyjs.Element.GetChildrenNodes=function (obj){
      return obj.childNodes;
}
//获取父级元素
czyjs.Element.GetParentNode = function(obj){
	return obj.parentNode;
}
/*
 * 获取CSS文档属性值
 */
czyjs.Element.GetCssStyle=function (obj, prop) {      
     if (obj.currentStyle) {         
         return obj.currentStyle[prop];      
     }       
     else if (window.getComputedStyle) {         
        propprop = prop.replace (/([A-Z])/g, "-$1");            
        propprop = prop.toLowerCase ();         
         return document.defaultView.getComputedStyle (obj,null)[prop];      
     }       
     return null;    
}  

