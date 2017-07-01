var cut_div;  //�ü�ͼƬ���div
var cut_img;  //�ü�ͼƬ
var imgdefw;  //ͼƬĬ�Ͽ��
var imgdefh;  //ͼƬĬ�ϸ߶�
var offsetx = 82; //ͼƬλ��λ��x
var offsety = -193; //ͼƬλ��λ��y
var divx = 284; //�����
var divy = 266; //���߶�
var cutx = 120;  //�ü����
var cuty = 120;  //�ü��߶�
var zoom = 1; //���ű���
var zmin = 0.1; //��С����
var zmax = 10; //������
var grip_pos = 5; //�϶���λ��0-���� 10 ����
var img_grip; //�϶���
var img_track; //�϶���
var grip_y; //�϶���yֵ
var grip_minx; //�϶���x��Сֵ
var grip_maxx; //�϶���x���ֵ
//ͼƬ��ʼ��
function imageinit(){
	cut_div = document.getElementById('cut_div');
	cut_img = document.getElementById('cut_img');
	imgdefw = cut_img.width;
	imgdefh = cut_img.height;
	if(imgdefw > divx){
		zoom = divx / imgdefw;
		cut_img.width = divx;
		cut_img.height = Math.round(imgdefh * zoom);
	}

	cut_img.style.left = Math.round((divx - cut_img.width) / 2);
	cut_img.style.top = Math.round((divy - cut_img.height) / 2) - divy;

	if(imgdefw > cutx){
		zmin = cutx / imgdefw;
	}else{
		zmin = 1;
	}
	zmax =  zmin > 0.25 ? 8.0: 4.0 / Math.sqrt(zmin);
	if(imgdefw > cutx){
		zmin = cutx / imgdefw;
		grip_pos = 5 * (Math.log(zoom * zmax) / Math.log(zmax));
	}else{
		zmin = 1;
		grip_pos = 5;
	}

	Drag.init(cut_div, cut_img);
	cut_img.onDrag = when_Drag;
	
	getcutpos();
}
//ͼƬ������
function imageresize(flag){
    if(flag){
		zoom = zoom * 1.5;
	}else{
		zoom = zoom / 1.5;
	}
	if(zoom < zmin) zoom = zmin;
	if(zoom > zmax) zoom = zmax;
	cut_img.width = Math.round(imgdefw * zoom);
	cut_img.height = Math.round(imgdefh * zoom);
	checkcutpos();
	grip_pos = 5 * (Math.log(zoom * zmax) / Math.log(zmax));
	img_grip.style.left = (grip_minx + (grip_pos / 10 * (grip_maxx - grip_minx))) + "px";
	
	getcutpos();
}
//���style���涨λ
function getStylepos(e){ return {x:parseInt(e.style.left), y:parseInt(e.style.top)}; }
//��þ��Զ�λ
function getPosition(e){  
	var t=e.offsetTop;  
	var l=e.offsetLeft;  
	while(e=e.offsetParent){  
		t+=e.offsetTop;  
		l+=e.offsetLeft;  
	}
	return {x:l, y:t}; 
}
//���ͼƬλ��
function checkcutpos(){
	var imgpos = getStylepos(cut_img);
	
	max_x = Math.max(offsetx, offsetx + cutx - cut_img.clientWidth);
	min_x = Math.min(offsetx + cutx - cut_img.clientWidth, offsetx);
	if(imgpos.x > max_x) cut_img.style.left = max_x + 'px';
	else if(imgpos.x < min_x) cut_img.style.left = min_x + 'px';

	max_y = Math.max(offsety, offsety + cuty - cut_img.clientHeight);
	min_y = Math.min(offsety + cuty - cut_img.clientHeight, offsety);

	if(imgpos.y > max_y) cut_img.style.top = max_y + 'px';
	else if(imgpos.y < min_y) cut_img.style.top = min_y + 'px';
	
	getcutpos();
}
//ͼƬ�϶�ʱ����
function when_Drag(clientX , clientY){checkcutpos();}
//���ͼƬ�ü�λ��
function getcutpos(){
	var imgpos = getStylepos(cut_img);
	var x = offsetx - imgpos.x;
	var y = offsety - imgpos.y;
	var cut_pos = document.getElementById('cut_pos');
	cut_pos.value = x + ',' + y + ',' + cut_img.width + ',' + cut_img.height;
	document.getElementById('img_pos').value = cut_img.width + ',' + cut_img.height;
	return true;
}
//��������ʼ��
function gripinit(){
	img_grip = document.getElementById('img_grip');
	img_track = document.getElementById('img_track');
	track_pos = getPosition(img_track);

	grip_y = track_pos.y;
	grip_minx = track_pos.x + 4;
	grip_maxx = track_pos.x + img_track.clientWidth - img_grip.clientWidth - 5;

	img_grip.style.left = (grip_minx + (grip_pos / 10 * (grip_maxx - grip_minx))) + "px";
	img_grip.style.top = grip_y + "px";

	Drag.init(img_grip, img_grip);
	img_grip.onDrag = grip_Drag;
	
	getcutpos();
}
//�������϶�ʱ����
function grip_Drag(clientX , clientY){
	var posx = clientX;
	img_grip.style.top = grip_y + "px";
	if(clientX < grip_minx){
		img_grip.style.left = grip_minx + "px";
		posx = grip_minx;
	}
	if(clientX > grip_maxx){
		img_grip.style.left = grip_maxx + "px";
		posx = grip_maxx;
	}

	grip_pos = (posx - grip_minx) * 10 / (grip_maxx - grip_minx);
	zoom = Math.pow(zmax, grip_pos / 5) / zmax;
	if(zoom < zmin) zoom = zmin;
	if(zoom > zmax) zoom = zmax;
	cut_img.width = Math.round(imgdefw * zoom);
	cut_img.height = Math.round(imgdefh * zoom);
	checkcutpos();
}
//ҳ�������ʼ��
function avatarinit(){imageinit();gripinit();}
if (document.all){window.attachEvent('onload',avatarinit);}else{window.addEventListener('load',avatarinit,false);}