// JavaScript Document
//网页特效|网页特效代码(JsHtml.cn)---网页对话框(无模式窗口)
//<body onLoad="showModelessDialog();">

//showModelessDialog('http://www.jshtml.cn','example05','dialogWidth:400px;dialogHeight:300px;dialogLeft:200px;dialogTop:150px;center:yes//; 
//help:yes;resizable:yes;status:yes') 

//<b>www.jshtml.cn</b> 
//<body> 
//</html>

//点击弹出窗口层，并且背景变暗渐变
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
//背景
  var bgObj=document.getElementById("bgDiv");
  bgObj.style.width = document.body.offsetWidth + "px";
  bgObj.style.height = screen.height + "px";
  
//定义窗口
 //var msgObj=document.getElementById("msgDiv");
// msgObj.style.marginTop = -75 +  document.documentElement.scrollTop + "px";
//关闭
  //document.getElementById("msgShut").onclick = function(){
 // bgObj.style.display = msgObj.style.display = "none";
 // }
 //msgObj.style.display = 
 bgObj.style.display ="block" ;
 alert('aa');
 bgObj.style.display ="none" ;
 // msgDetail.innerHTML="<p align=center>小窗口里的内容</p><p align=center><A href=http://www.jshtml.cn><FONT color=#0000ff>网页特效代码</FONT></A></p>"
}

/*
</head>
<body>
<!--<div id="msgDiv">
  <div id="msgShut">
  关闭</div>
 <div id="msgDetail">
 </div>
</div>-->
<div id="bgDiv">
</div>
<p><a href="#" onClick="showDetail()">点击我试试看</a></p>
</body>
*/
