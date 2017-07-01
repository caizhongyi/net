//Javascript Document For funswitcher by trance

$(document).ready(function(){
//幻灯片
var j=1;
var MyTime=false;
var fot=200;//当前图片消失时间
var fin=300;//新图片呈现时间
var amt=300;//三角标志滑动时间
var speed=3000;//自动播放间隔
var maxpic=4;//切换图片个数
	$("#ppt").find("li").each(function(i){
		$("#ppt").find("li").eq(i).mouseover(function(){											  
			var cur=$("#mpc").find("div:visible").prevAll().length;
			if(i!==cur){
				j=i;					
				$("#mpc").find("div:visible").fadeOut(fot,function(){
				$("#mpc").find("div").eq(i).fadeIn(fin);});
				$("#tri").animate({"top":i*87+"px"},amt,current(this,"li"));
				$("#ppt").find("dl:visible").slideUp(fot,function(){
				$("#ppt").find("dl").eq(i).slideDown(fin);});				
			}			
		})	
	})
//标记当前	
function current(ele,tag){
	$(ele).addClass("cur").siblings(tag).removeClass("cur");
	}	
//自动播放 不需要的话可以删除这一段	
$('#imgnav').hover(function(){
			  if(MyTime){
				 clearInterval(MyTime);
			  }
	 },function(){
			  MyTime = setInterval(function(){
			    $("#ppt").find("li").eq(j).mouseover();
				j++;
				if(j==maxpic){j=0;}
			  } , speed);
	 });
	 //自动开始 
	 var MyTime = setInterval(function(){
		$("#ppt").find("li").eq(j).mouseover();
		j++;
		if(j==maxpic){j=0;}
	 } , speed);

})