//Javascript Document For funswitcher by trance

$(document).ready(function(){
//�õ�Ƭ
var j=1;
var MyTime=false;
var fot=200;//��ǰͼƬ��ʧʱ��
var fin=300;//��ͼƬ����ʱ��
var amt=300;//���Ǳ�־����ʱ��
var speed=3000;//�Զ����ż��
var maxpic=4;//�л�ͼƬ����
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
//��ǵ�ǰ	
function current(ele,tag){
	$(ele).addClass("cur").siblings(tag).removeClass("cur");
	}	
//�Զ����� ����Ҫ�Ļ�����ɾ����һ��	
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
	 //�Զ���ʼ 
	 var MyTime = setInterval(function(){
		$("#ppt").find("li").eq(j).mouseover();
		j++;
		if(j==maxpic){j=0;}
	 } , speed);

})