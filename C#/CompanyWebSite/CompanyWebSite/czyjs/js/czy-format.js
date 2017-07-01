if(typeof(czyjs.Format!="undefined"))
{
	czyjs.Format={};
}

//去左空格;
czyjs.Format.LTrim=function(s){
return s.replace( /^[" "|"　"]*/, "");
}
//去右空格;
czyjs.Format.RTrim=function (s){
return s.replace( /[" "|"　"]*$/, "");
}
//左右空格;
czyjs.Format.Trim=function (s){
return rtrim(ltrim(s));
}

//截字符窜
czyjs.Format.SubString=function (str,index,lenght)
{
	return str.substring(index,lenght);
}

//小写转为大写
czyjs.Format.UpperCaseStr=function (str)
{ 
  return str.toUpperCase();
}
//大写转为小写
czyjs.Format.LowerCaseStr=function (str)
{ 
   return str.toLowerCase();
}