Attribute VB_Name = "modcommon"
Option Explicit
Public played As Boolean
Public Declare Function GetWindowsDirectory Lib "Kernel32" Alias "GetWindowsDirectoryA" (ByVal lpBuffer As String, ByVal nSize As Long) As Long
Declare Function GetSystemDirectory Lib "Kernel32" Alias "GetSystemDirectoryA" (ByVal lpBuffer As String, ByVal nSize As Long) As Long
Public Declare Function sndPlaySound Lib "winmm.dll" Alias "sndPlaySoundA" (ByVal lpszSoundName As String, ByVal uFlags As Long) As Long
Public Declare Function CopyFile Lib "Kernel32" Alias "CopyFileA" (ByVal lpExistingFileName As String, ByVal lpNewFileName As String, ByVal bFailIfExists As Long) As Long
Public Declare Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hwnd As Long, ByVal wMsg As Long, ByVal wParam As Long, lParam As Any) As Long
Public Declare Function ReleaseCapture Lib "user32" () As Long
'download by http://www.codefans.net
Public Function timeout(ByVal sec%, reset As Boolean) As Boolean
Static r_time As String
If reset = True Then
    r_time = Timer
End If
If (Timer - r_time) > sec Then
    timeout = True
End If
End Function



Public Function centerform(ByVal frm As Form)
 frm.Top = ((Screen.Height * 0.85) \ 2 - frm.Height \ 2) + 500
frm.Left = Val(Screen.Width \ 2 - frm.Width \ 2)
End Function

Public Function addstrap(ByVal path1 As String, ByVal path2 As String) As String
If Right$(path1, 1) = "\" Then
     addstrap = path1 & path2
Else
         addstrap = path1 & "\" & path2
End If
End Function
Public Function getwinsysdir() As String
Dim a$
Dim ret%
a$ = String$(256, Chr(0))
ret = GetSystemDirectory(a, 256)
getwinsysdir = Left$(a, ret) & "\"
End Function
Public Function extractstring(ByVal str$, ByVal cmp$, ByVal no%) As String
Dim arr() As String
arr = Split(str, cmp)
If no <= UBound(arr) Then
    extractstring = arr(no)
Else
    extractstring = ""
End If

End Function
'Public Sub highlightbutton(ByVal Button As CommandButton)
'Button.BackColor = &HC0C000
'If Not prevbutton Is Nothing Then
'If prevbutton.Name <> Button.Name Then
''If prevbutton.Parent.Name = Button.Parent.Name Then
'    prevbutton.BackColor = &H8000000F
''End If
'End If
'End If
'Set prevbutton = Button
'End Sub
Public Sub selecttextbox(textbox As Object)
On Error Resume Next
textbox.SetFocus
textbox.SelStart = 0
textbox.SelLength = Len(textbox.Text)
End Sub
Public Sub navigatetextbox(Form As Form, textbox As textbox, ByVal KeyAscii%, Optional tabindex% = -1)
'------------------------------------------------------------------------------------------------
    'Textbox navigation by priyan
    'Visit me at www.priyan.tk or www.priyan.us.tt
'------------------------------------------------------------------------------------------------
Dim inc As Boolean
Dim tindex%
tindex = textbox.tabindex
If KeyAscii = 13 Then
    tindex = tindex + 1
    inc = True
ElseIf KeyAscii = vbKeyBack Then
    If textbox.Text = "" Then
        tindex = tindex - 1
    Else: Exit Sub
    End If
Else
    Exit Sub
End If
If tabindex <> -1 Then tindex = tabindex
On Error Resume Next
Dim Control
For Each Control In Form.Controls
    If Control.tabindex = tindex Then
      If TypeName(Control) = "TextBox" Then
            If Control.TabStop = False Then
                If inc = True Then
                    navigatetextbox Form, textbox, KeyAscii, tindex + 1: Exit Sub
                Else
                    navigatetextbox Form, textbox, KeyAscii, tindex - 1: Exit Sub
                End If
            End If
            Control.SetFocus
       Else
            Exit Sub
       End If
    End If
Next

End Sub

Public Function converttocode(ByVal n&) As String 'Converts the number to a code according to the code setted in frmsetcode
Dim code$, ret$, i%
Dim ch$
Dim num$
num = CStr(n)
code = GetSetting("priyan", App.Title, "code", "0123456789")
For i = 1 To Len(num)
    ch = Mid(num, i, 1)
    ret = ret & Mid(code, Val(ch) + 1, 1)
Next
    converttocode = ret
End Function
Public Sub loadwindowpostions(ByVal frm As Form)
On Error Resume Next
frm.Width = GetSetting("priyan\" & App.Title, frm.Name, "width", frm.Width)
frm.Height = GetSetting("priyan\" & App.Title, frm.Name, "height", frm.Height)
frm.Left = GetSetting("priyan\" & App.Title, frm.Name, "left", frm.Left)
frm.Top = GetSetting("priyan\" & App.Title, frm.Name, "top", frm.Top)
If GetSetting("priyan\" & App.Title, frm.Name, "max", "0") = "1" Then
    frm.WindowState = vbMaximized
End If
End Sub
Public Sub savewindowpostions(ByVal frm As Form)
On Error Resume Next
If frm.WindowState = vbMinimized Then Exit Sub
If frm.WindowState = vbMaximized Then
    SaveSetting "priyan\" & App.Title, frm.Name, "max", "1"
    Exit Sub
End If
SaveSetting "priyan\" & App.Title, frm.Name, "max", "0"
If frm.BorderStyle = vbSizable Then
    SaveSetting "priyan\" & App.Title, frm.Name, "width", frm.Width
    SaveSetting "priyan\" & App.Title, frm.Name, "height", frm.Height
End If
SaveSetting "priyan\" & App.Title, frm.Name, "left", frm.Left
SaveSetting "priyan\" & App.Title, frm.Name, "top", frm.Top
End Sub

'Public Function backuptofloppy() As Boolean
'Dim srcfile$, srcfile1$, destpath$
'destpath = "a:\"
'srcfile = addstrap(App.Path, "data.mdb")
'srcfile1 = addstrap(App.Path, "rpttemp.mdb")
'If CopyFile2(srcfile, addstrap(destpath, "data.mdb")) = True Then
'    backuptofloppy = True
'End If
'CopyFile2 srcfile1, addstrap(destpath, "rpttemp.mdb")
'End Function

Public Function getwindowsdir() As String
Dim a$
Dim ret%
a$ = String$(256, Chr(0))
ret = GetWindowsDirectory(a, 256)
getwindowsdir = Left$(a, ret) & "\"

End Function

Public Function isalpha(ByVal str$) As Boolean
If Asc(str) >= 65 And Asc(str) <= 90 Then
    isalpha = True
ElseIf Asc(str) >= 97 And Asc(str) <= 122 Then
    isalpha = True
End If
End Function
Public Function getfilename(file$) As String
Dim pos%
getfilename = StrReverse(file)
pos = InStr(1, getfilename, "\")
If pos = 0 Then getfilename = file: Exit Function
getfilename = Left(getfilename, pos - 1)
getfilename = StrReverse(getfilename)
End Function
Public Function getdirname(file$) As String
Dim pos%
getdirname = StrReverse(file)
pos = InStr(1, getdirname, "\")
getdirname = Mid(getdirname, pos + 1, Len(file) - pos)
getdirname = StrReverse(getdirname)
End Function


Public Function removeextension(ByVal Filename$) As String
Dim pos%
Filename = StrReverse(Filename)
pos = InStr(1, Filename, ".")
removeextension = StrReverse(Mid(Filename, pos + 1, Len(Filename) - pos))
End Function
Public Function FormDrag(TheForm As Form)
    ReleaseCapture
    SendMessage TheForm.hwnd, &HA1, 2, 0&
End Function
'Public Sub loaddatagridsettings(ByVal lview As DataGrid)
'On Error Resume Next
'Dim i%
'Dim temp$
'For i = 0 To lview.Columns.Count - 1
'     temp = GetSetting("priyan", App.title & "\" & lview.Parent.Name & "\" & lview.Name, i, lview.Columns(i).Width)
'     lview.Columns(i).Width = Val(temp)
'Next
'End Sub
'Public Sub cleardatagridsettings(ByVal lview As DataGrid)
'On Error Resume Next
'modRegistry.deletekey HK_CURRENT_USER, "Software\VB and VBA Program Settings\priyan\" & App.title & "\" & lview.Parent.Name & "\" & lview.Name
'
'End Sub
'Public Sub savelistviewsettings(ByVal lview As ListView)
'Dim i%
'For i = 1 To lview.ColumnHeaders.Count
'    SaveSetting "priyan", App.Title & "\" & lview.Parent.Name & "\" & lview.Name, i, lview.ColumnHeaders(i).Width
'    'SaveSetting "priyan", App.Title & "\" & "a", i, lview.ColumnHeaders(i).Width
'Next
'End Sub
'Public Sub loadlistviewsettings(ByVal lview As ListView)
'Dim i%
'Dim temp$
'For i = 1 To lview.ColumnHeaders.Count
'     temp = GetSetting("priyan", App.Title & "\" & lview.Parent.Name & "\" & lview.Name, i, lview.ColumnHeaders(i).Width)
'     lview.ColumnHeaders(i).Width = Val(temp)
'Next
'End Sub
Public Function getextension(ByVal file$) As String
Dim pos&
file = StrReverse(file)
pos = InStr(1, file, ".")
If pos Then
    getextension = StrReverse(Left(file, pos - 1))
Else
    getextension = ""
End If
End Function
