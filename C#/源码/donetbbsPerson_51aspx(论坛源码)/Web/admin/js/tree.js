function JsBoardDblClick(n1,n2)
{
    if (n1==0)
    {
    JsOpenModalDialog('Board/BigBoard.aspx?boardid='+n2+'','454','396',0)
    }
    else
    {
    JsOpenModalDialog('Board/TreeBoard.aspx?boardid='+n2+'','690','572',0);
    }
}
function JsBoardTree(n1,n2)
{
    if (event.button==2)
	{
	   JsTreeMenuDisplay(true);
	   JsShowMenu(n1,n2);
	}
}
function JsShowMenu(n1,n2)
{
    var obj=document.getElementById("TreeMenudivID");
    var menu;
    var h=80;
    var w=60;
    menu="<div style=\"border:#00CCFF solid 1px;width:60px;padding:3px;background-image:url(images/left_bg.gif)\" >";
    
    menu+="<div style=\"height:20px;width:100%;padding-top:4px;padding-left:10px;\" onMouseOver=\"this.style.backgroundImage='url(images/admin_bg_1.gif)'\" onMouseOut=\"this.style.backgroundImage=''\"> <a href=\"javascript:JsOpenModalDialog('Board/TreeBoard.aspx?parentBoardid="+n2+"','690','572',0);\">增加</a></div>";
    if (n1==0)
    {
        h=60
        menu+="<div style=\"height:20px;width:100%;padding-top:4px;padding-left:10px;\" onMouseOver=\"this.style.backgroundImage='url(images/admin_bg_1.gif)'\" onMouseOut=\"this.style.backgroundImage=''\"> <a href=\"javascript:JsOpenModalDialog('Board/BigBoard.aspx?boardid="+n2+"','454','396',0);\">修改</a></div>";
    }
    else
    {
        menu+="<div style=\"height:20px;width:100%;padding-top:4px;padding-left:10px;\" onMouseOver=\"this.style.backgroundImage='url(images/admin_bg_1.gif)'\" onMouseOut=\"this.style.backgroundImage=''\"> <a href=\"javascript:JsOpenModalDialog('Board/TreeBoard.aspx?boardid="+n2+"','690','572',0);\">修改</a></div>";
        menu+="<div style=\"height:20px;width:100%;padding-top:4px;padding-left:10px;\" onMouseOver=\"this.style.backgroundImage='url(images/admin_bg_1.gif)'\" onMouseOut=\"this.style.backgroundImage=''\"> <a href=\"javascript:JsOpenModalDialog('Board/MoveTo.aspx?boardid="+n2+"','314','186',0);\">转移</a></div>";
    }
    menu+="<div style=\"height:20px;width:100%;padding-top:4px;padding-left:10px;\" onMouseOver=\"this.style.backgroundImage='url(images/admin_bg_1.gif)'\" onMouseOut=\"this.style.backgroundImage=''\"> <a href=\"javascript:JsDeleteInfo('Board/DeleteBoard.aspx?boardid="+n2+"');\">删除</a></div>";
        

    menu +="</div>";    
    obj.innerHTML=menu;
    obj.style.position = "absolute";
    obj.style.left=event.x;
    obj.style.top=event.y+6;
    obj.style.width=w;
    obj.style.height=h;
}
function JsShowMenuOnMouse()
{
var obj=document.getElementById("TreeMenudivID");
obj.style.display='';
}
function JsTreeMenuDisplay(n1)
{
TreeMenu="TreeMenudivID";
    if (!document.getElementById(TreeMenu))
		{
			var newNode = document.createElement("<div id="+TreeMenu+" onmouseout=JsTreeMenuDisplay(false) style=\"z-index:2222;\"></div>");
			//newNode.setAttribute("id", TreeMenu);
			//newNode.setAttribute("onmouseout", "alert()");
			document.body.appendChild(newNode);
		}
		var obj=document.getElementById(TreeMenu);
		//
	if (n1)
	{
		document.oncontextmenu = nocontextmenu;
		obj.style.display='';
	}
	else
	{
		var l=obj.style.left;
		var t=obj.style.top;
		var w=obj.style.width;
		var h=obj.style.height;
		var x=event.x;
		var y=event.y;
		l=l.replace("px","");
		t=t.replace("px","");
		w=w.replace("px","");
		h=h.replace("px","");
		w=eval(l)+eval(w);
		h=eval(t)+eval(h);
		t=eval(t);
		l=eval(l);
		//alert(x+"t"+w)
		if ((x<l)||(x>w)||(y<t)||(y>h))
		{
		    obj.innerHTML="";
		    document.oncontextmenu=nocontextmenuhidden;
		    obj.style.display='none';
		}
	}
}
function nocontextmenu() 
{
event.cancelBubble = true
event.returnValue = false;
}
function nocontextmenuhidden() 
{
event.cancelBubble = false
event.returnValue = true;
}

