<!--
    // ◎◎◎◎◎图片路径。◎◎◎◎◎
    var ImgLoading="Images/Loading.gif"；
document.write('<div id="Load" class="load"><img alt="Loading..." src="'+ImgLoading+'" /><br /><span style="font-family: Comic Sans MS; font-size: 20px; color: #333333;">Please Wait. Loading...</span></div>');   
window.onload = function()
{
//    document.getElementById("DivContainer").style.display="block";          
    document.getElementById("Load").parentNode.removeChild(document.getElementById("Load"));
}
-->