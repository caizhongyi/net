// JavaScript Document
var Emot_PageSize=8;
Forum_Emot=Forum_Emot.split("<><><>");
function ShowForum_Emot(thepage)
{

	var Emot_PageCount;
	var Emot_Count=Forum_Emot.length-2;
	if(Emot_Count%Emot_PageSize==0)
	{
		Emot_PageCount=(Emot_Count)/Emot_PageSize
	}else{
		Emot_PageCount=Math.floor((Emot_Count)/Emot_PageSize)+1
	}
	thepage=parseInt(thepage);
	if (thepage<=Emot_PageCount){
	var istr
	var EmotStr='&nbsp;';
	var EmotPath=Forum_Emot[0];
	if (thepage!=1 && Emot_PageCount>1)
	{EmotStr+='<img style="cursor: pointer;" onClick="ShowForum_Emot('+(thepage-1)+');" src="/Images/Previous.gif"  title="上一页">&nbsp;';}
	for(i=(thepage-1)*Emot_PageSize;i<(thepage-1)*Emot_PageSize+Emot_PageSize;i++)
	{
		if (i==Emot_Count){break}
		if (i<8)
			{istr='em0'+(i+1)}
			else
			{istr='em'+(i+1)}
		EmotStr+='<img title="'+istr+'" style="cursor: pointer;" onClick=DoNetBbsPutEmot(); src="'+EmotPath+Forum_Emot[i+1]+'">&nbsp;';
	}
	if (thepage!=Emot_PageCount)
	{EmotStr+='<img style="cursor: pointer;" onClick="ShowForum_Emot('+(thepage+1)+');" src="/Images/Next.gif" title="下一页">&nbsp;';}
	EmotStr+='分页：<b>'+thepage+'</b>/<b>'+Emot_PageCount+'</b>，共<b>'+(Emot_Count)+'</b>个';
	EmotStr+="<select id=emotpage onchange=\"ShowForum_Emot(this.value);\">";
	for (i=1; i<=Emot_PageCount;i++ )
	{
		EmotStr+="<option value=\""+i+"\">"+i;
	}
	EmotStr+="<\/select>";
	var Forum_EmotObj=document.getElementById("emot")
	Forum_EmotObj.innerHTML=EmotStr;
	document.getElementById('emotpage').options[thepage-1].selected=true;
	}
}


//DoNetBbs_InitDocument('Body','gb2312');
ShowForum_Emot(1);


function DoNetBbsPutEmot()
{
if (!FalseDoNetBbsPostBodyEdityType())
{
return;
}
var urls = document.location.protocol + "//" + document.location.host;

	var url=event.srcElement.getAttribute("src", 0).substr(0).replace(urls,"");
editor_insertHTML('PostBodyHtml',"<IMG SRC="+ url + ">",null,null);
return;
  
}