// JavaScript Document
//��ҳ��Ч|��ҳ��Ч����(JsHtml.cn)---��ҳ�Ի���(��ģʽ����)
//<body onLoad="showModelessDialog();">

//showModelessDialog('http://www.jshtml.cn','example05','dialogWidth:400px;dialogHeight:300px;dialogLeft:200px;dialogTop:150px;center:yes//; 
//help:yes;resizable:yes;status:yes') 

//<b>www.jshtml.cn</b> 
//<body> 
//</html>

//����������ڲ㣬���ұ����䰵����
/*
<style>
body {font-size:12px;background:#9EC7E7}
img {border:0px}
#msgDiv {
   z-index:10001;
    width:500px;
   height:400px;
    background:white;
    border:#336699 1px solid;
   position:absolute;
    left:50%;
    top:20%;
    font-size:12px;
   margin-left:-225px;
   display: none;
}
#bgDiv {
    display: none;
    position: absolute;
   top: 0px;
    left: 0px;
   right:0px;
    background-color: #777;
   filter:progid:DXImageTransform.Microsoft.Alpha(style=3,opacity=25,finishOpacity=75)
   opacity: 0.6;
}
</style>
*/

function showDetail() { //www.jshtml.cn
//����
  var bgObj=document.getElementById("bgDiv");
  bgObj.style.width = document.body.offsetWidth + "px";
  bgObj.style.height = screen.height + "px";
  
//���崰��
 //var msgObj=document.getElementById("msgDiv");
// msgObj.style.marginTop = -75 +  document.documentElement.scrollTop + "px";
//�ر�
  //document.getElementById("msgShut").onclick = function(){
 // bgObj.style.display = msgObj.style.display = "none";
 // }
 //msgObj.style.display = 
 bgObj.style.display ="block" ;
 alert('aa');
 bgObj.style.display ="none" ;
 // msgDetail.innerHTML="<p align=center>С�����������</p><p align=center><A href=http://www.jshtml.cn><FONT color=#0000ff>��ҳ��Ч����</FONT></A></p>"
}

/*
</head>
<body>
<!--<div id="msgDiv">
  <div id="msgShut">
  �ر�</div>
 <div id="msgDetail">
 </div>
</div>-->
<div id="bgDiv">
</div>
<p><a href="#" onClick="showDetail()">��������Կ�</a></p>
</body>
*/
