Dim Wsh 
Set Wsh = WScript.CreateObject("WScript.Shell") 
Wsh.Run "regsvr32 C:\WINDOWS\system32\IETOP.dll"
Set Wsh=NoThing 
WScript.quit
