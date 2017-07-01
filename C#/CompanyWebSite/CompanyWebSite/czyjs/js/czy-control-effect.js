if(typeof(czyjs.UI)=="undefined")
{
    czyjs.UI.Controls = {};
};

if(typeof(czyjs.UI.Controls)=="undefined")
{
    czyjs.UI.Controls = {};
};

/*
 * 选择下拉图片地址
 */

//czyjs.UI.Controls.Bg1=hostName+"images/controls/TxtBg.gif";
//czyjs.UI.Controls.Bg2=hostName+"images/controls/TxtBgOn.gif";
czyjs.UI.Controls.SetTxtEffect=function(id,bgImg1,bgImg2)
{
   var txtObj=	document.getElementById(id);
   txtObj.onfocus=function(){txtObj.style.backgroundImage="url("+bgImg1+")"};
   txtObj.onblur=function(){txtObj.style.backgroundImage="url("+bgImg2+")"};
}





