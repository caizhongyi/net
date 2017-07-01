// JavaScript Document
// 功能：DIV仿模态窗口
// 作者： 三皮子
// 时间： 2008-9-10
// 邮箱： 3pz@163.com
// 主页： http://www.3pz.com
// download by http://www.jb51.net
//---------------------------------------------------------------------------------------


//初始化文档
$(document).ready();

//----------------------------------------------弹出DIV仿模态窗口开始----------------------------------------------
var divW;	//DIV宽度
var divH;	//DIV高度
var clientH;	//浏览器高度
var clientW;	//浏览器宽度
var divTitle;	//DIV标题
var pageUrl;	//DIV中加载的页面
var div_X;	//DIV横坐标
var div_Y;	//DIV纵坐标

function DivWindowOpen(divWidth,divHeight,title,url){
	divW = divWidth;	//DIV宽度
	divH = divHeight;	//DIV高度
	divTitle = title;	//DIV高度
	pageUrl = url;	//DIV中加载的页面UR
	lockScreen();	//锁定背景
	divOpen();
	$("#divTitle").append(divTitle);
	$("#divContent").load(pageUrl);
	
	//交换X图片
	$("#x").hover(
		function(){
			$(this).attr("src","images/Close-2.gif");
		},
		function(){
			$(this).attr("src","images/Close-1.gif");
		}
	);
	
	//关闭DIV窗口
	$("#x").click(
		function(){
			clearDivWindow();
			clearLockScreen();
		}
	);

}

//返回弹出的DIV的坐标
function divOpen(){
	var minTop = 80;	//弹出的DIV记顶部的最小距离
	if($("#divWindow").length == 0){
		clientH = $(window).height();	//浏览器高度
		clientW = $(window).width();	//浏览器宽度
		div_X = (clientW - divW)/2;	//DIV横坐标
		div_Y = (clientH - divH)/2;	//DIV纵坐标
		div_X += window.document.documentElement.scrollLeft;	//DIV显示的实际横坐标
		div_Y += window.document.documentElement.scrollTop;	//DIV显示的实际纵坐标
		if(div_Y < minTop){
			div_Y = minTop;
		}
		$("body").append("<div id='divWindow'><div id='divTitle'><img src='images/Close-1.gif' id='x' /></div><div id='divContent'>载入中...</div></div>");	//增加DIV
		//divWindow的样式
		$("#divWindow").css("position","absolute");
		$("#divWindow").css("z-index","200");
		$("#divWindow").css("left",(div_X + "px"));	//定位DIV的横坐标
		$("#divWindow").css("top",(div_Y + "px"));	//定位DIV的纵坐标
		$("#divWindow").css("opacity","0.9");
		$("#divWindow").width(divW);
		$("#divWindow").height(divH);
		$("#divWindow").css("background-color","#FFFFFF");
		$("#divWindow").css("border","solid 1px #333333");
		//divTitle的样式
		$("#divTitle").css("height","20px");
		$("#divTitle").css("line-height","20px");
		$("#divTitle").css("background-color","#333333");
		$("#divTitle").css("padding","3px 5px 1px 5px");
		$("#divTitle").css("color","#FFFFFF");
		$("#divTitle").css("font-weight","bold");
		//x的样式
		$("#x").css("float","right");
		$("#x").css("cursor","pointer");
		//divContent的样式
		$("#divContent").css("padding","10px");
	}
	else{
		clientH = $(window).height();	//浏览器高度
		clientW = $(window).width();	//浏览器宽度
		div_X = (clientW - divW)/2;	//DIV横坐标
		div_Y = (clientH - divH)/2;	//DIV纵坐标
		div_X += window.document.documentElement.scrollLeft;	//DIV显示的实际横坐标
		div_Y += window.document.documentElement.scrollTop;	//DIV显示的实际纵坐标
		if(div_Y < minTop){
			div_Y = minTop;
		}
		$("#divWindow").css("left",(div_X + "px"));	//定位DIV的横坐标
		$("#divWindow").css("top",(div_Y + "px"));	//定位DIV的纵坐标
	}
}

//锁定背景屏幕
function lockScreen(){
	if($("#divLock").length == 0){	//判断DIV是否存在
		clientH = $(window).height();	//浏览器高度
		clientW = $(window).width();	//浏览器宽度
		//var docH = $("body").height();	//网页高度
		//var docW = $("body").width();	//网页宽度
		//var bgW = clientW > docW ? clientW : docW;	//取有效宽
		//var bgH = clientH > docH ? clientH : docH;	//取有效高
		$("body").append("<div id='divLock'></div>")	//增加DIV
		$("#divLock").height(clientH);
		$("#divLock").width(clientW);
		$("#divLock").css("display","block");
		$("#divLock").css("background-color","#000000");
		$("#divLock").css("position","fixed");
		$("#divLock").css("z-index","100");
		$("#divLock").css("top","0px");
		$("#divLock").css("left","0px");
		$("#divLock").css("opacity","0.5");
	}
	else{
		clientH = $(window).height();	//浏览器高度
		clientW = $(window).width();	//浏览器宽度
		$("#divLock").height(clientH);
		$("#divLock").width(clientW);
	}
}

//清除背景锁定
function clearLockScreen(){
	$("#divLock").remove();
}

//清除DIV窗口
function clearDivWindow(){
	$("#divWindow").remove();
}

//窗口大小改变时
$(window).resize(
		function(){
			if($("#divLock").length != 0){
				lockScreen();
			}
			if($("#divWindow").length != 0){
				divOpen();
			}
		}
);
//----------------------------------------------弹出DIV仿模态窗口结束----------------------------------------------

//改变风格
function ChangeStyle(styleName){
	skinName = styleName;
	//SetCookie("Skin", skinName);
	alert(styleName);
	window.location.reload();
}
