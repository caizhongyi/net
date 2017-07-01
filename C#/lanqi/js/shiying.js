window.onload = widthtest;
window.onresize = widthtest;
function widthtest() {
	var obj = document.getElementById('width');
	obj.style.width = '100%';
	if (parseInt(obj.offsetWidth) < 950)
		obj.style.width = '950px';
}