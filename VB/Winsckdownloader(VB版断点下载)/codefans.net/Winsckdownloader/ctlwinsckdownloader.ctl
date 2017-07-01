VERSION 5.00
Object = "{248DD890-BB45-11CF-9ABC-0080C7E7B78D}#1.0#0"; "Mswinsck.ocx"
Begin VB.UserControl ctlwinsckdownloader 
   ClientHeight    =   885
   ClientLeft      =   0
   ClientTop       =   0
   ClientWidth     =   2445
   InvisibleAtRuntime=   -1  'True
   ScaleHeight     =   885
   ScaleWidth      =   2445
   Begin MSWinsockLib.Winsock Winsock1 
      Left            =   1800
      Top             =   360
      _ExtentX        =   741
      _ExtentY        =   741
      _Version        =   393216
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      Caption         =   "Unni Downloader"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   12
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   300
      Left            =   0
      TabIndex        =   0
      Top             =   0
      Width           =   2070
   End
End
Attribute VB_Name = "ctlwinsckdownloader"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = True
Attribute VB_PredeclaredId = False
'download by http://www.codefans.net
Attribute VB_Exposed = False
Dim headers As New Collection
Dim header$
Dim fno%
Enum creq_typet
    Get_header
    download
End Enum
Dim request_type As creq_typet
Dim server$
Dim port$
Dim url_ As String
Dim filename_ As String
Dim request_name$
Dim file_length As Double
Dim resp_code As String
'=============================
Dim Still_Executing As Boolean
Dim Server_ok_ As Boolean
Dim canceled As Boolean
Dim downloaded As Boolean

Dim resume_supported_ As Boolean
Dim Resuming_ As Boolean
Dim SERVER_ERROR$
'=====================
'Programmed By Priyan R
'admin@priyan.tk
'http://priyan.tk
'=====================
Public Event Progress(ByVal downloaded As Double, ByVal Total As Double)
Public Event downloaded(ByVal dfile$)
Public Event DownloadError(ByVal description$, ByVal SERVER_ERROR As Boolean)
Public Event headerread()
Public Event Disconnected()
Public Event canceled()
Option Explicit

Public Sub Readheaders()
downloaded = False
canceled = False
Still_Executing = True
request_type = Get_header
Winsock1.Close
Winsock1.Connect server, port
End Sub
Public Sub downloadfile()
canceled = False
downloaded = False
Still_Executing = True
request_type = download
Winsock1.Close
Winsock1.Connect server, port
fno = FreeFile
Open filename_ For Binary As #fno
End Sub
'===========================================
Public Property Get URL() As String
URL = url_
End Property

Public Property Let URL(ByVal vNewValue As String)
url_ = vNewValue
server = url_
port = "80"
Dim pos%, temp$
server = Trim(Replace(url_, "http://", ""))
pos = InStr(server, "/")
If pos Then
    request_name = Mid(server, pos)
    server = Left(server, pos - 1)
Else
    request_name = "/"
End If
pos = InStr(server, ":")
If pos Then
    temp = server
    server = Left(server, pos - 1)
    If Len(temp) > pos Then
        port = Val(Mid(temp, pos + 1))
    End If
End If
End Property
Public Property Get Filename() As String
Filename = filename_
End Property

Public Property Let Filename(ByVal vNewValue As String)
filename_ = vNewValue
End Property

Private Sub UserControl_Resize()
On Error Resume Next
UserControl.Width = Label1.Width
UserControl.Height = Label1.Height
End Sub

Private Sub Winsock1_Close()
Close #fno
Winsock1.Close
Still_Executing = False
If file_length = -1 And request_type = download Then
    RaiseEvent downloaded(filename_)
Else
   If downloaded = False And canceled = False And request_type = download Then RaiseEvent Disconnected
End If
End Sub

Private Sub Winsock1_Connect()
Dim str$
Resuming_ = False
str = "GET " & request_name & " HTTP/1.1" & vbCrLf & _
"Host: " & server & vbCrLf & _
"Referrer: " & Winsock1.LocalHostName & vbCrLf & _
"Accept: */*" & vbCrLf & _
"Connection: Close" & vbCrLf & _
"User-Agent: Priyan Downloader" & vbCrLf
If request_type = download Then
    If LOF(fno) <> 0 Then
        Resuming_ = True
        Seek #fno, LOF(fno)
        str = str & "Range: bytes=" & Loc(fno) & "-" & vbCrLf
    End If
Else
    str = str & "Range: bytes=0-" & vbCrLf
End If
Winsock1.SendData str & vbCrLf
End Sub

Private Sub Winsock1_DataArrival(ByVal bytesTotal As Long)
On Error GoTo ext:
Dim data$, pos%
Static download_started As Boolean
Winsock1.GetData data, vbString, bytesTotal
If request_type = Get_header Then
    download_started = False
    fillheaders data
    Still_Executing = False
    Winsock1.Close
    Close #fno
    If Server_ok_ = False Then
        RaiseEvent DownloadError(SERVER_ERROR, True)
        Winsock1.Close
    End If
    If Trim(getheader("content-length")) = "" Then
        file_length = -1
    Else
        file_length = Val(getheader("content-length"))
    End If
    RaiseEvent headerread
    Exit Sub
End If
    If download_started = False Then
        fillheaders data
        If Server_ok_ = False Then
            Close #fno
            Winsock1.Close
            Still_Executing = False
            RaiseEvent DownloadError(SERVER_ERROR, True)
            Exit Sub
        End If
        pos = InStr(data, vbCrLf & vbCrLf)
        data = Mid(data, pos + 4)
        If resume_supported_ = False And Resuming = True Then
            'sevrer does not support resuming
            Seek #fno, 0
        End If
        download_started = True
    End If
If Len(data) > 0 Then Put #fno, , data
On Error Resume Next
Debug.Print Loc(fno) / file_length * 100
RaiseEvent Progress(Loc(fno), file_length)
If file_length <> -1 Then
    If Loc(fno) >= file_length Then
        Still_Executing = False
        Close #fno
        Winsock1.Close
        downloaded = True
        RaiseEvent downloaded(filename_)
    End If
End If
Exit Sub
ext:
Winsock1.Close
Close #fno
If canceled = False Then
    RaiseEvent DownloadError(Err.description, False)
End If
End Sub

Private Sub Winsock1_Error(ByVal Number As Integer, description As String, ByVal Scode As Long, ByVal Source As String, ByVal HelpFile As String, ByVal HelpContext As Long, CancelDisplay As Boolean)
Winsock1.Close
Close #fno
Still_Executing = False
RaiseEvent DownloadError(description, False)
End Sub
Public Function fillheaders(ByVal data$)
On Error Resume Next
Dim str() As String, obj, temp, pos%
Set headers = New Collection
resp_code = ""
Server_ok_ = False
resume_supported_ = False
If InStr(data, vbCrLf & vbCrLf) Then
    header = Left(data, InStr(data, vbCrLf & vbCrLf) - 1)
    str = Split(header, vbCrLf)
    For Each obj In str
        pos = InStr(obj, ":")
        If pos Then
            headers.Add Trim(Mid(obj, pos + 1)), Trim(Left(obj, pos - 1))
        End If
    Next
    resp_code = Mid(header, 10, 3)
    Select Case resp_code
        Case "200"
            Server_ok_ = True
        'Case "302"
            'Server_ok_ = False
        Case "206"
            'supports resuming
            resume_supported_ = True
            Server_ok_ = True
        Case "204"
            SERVER_ERROR = "无法下载"
        Case "401"
            SERVER_ERROR = "验证失败"
        Case "404"
            SERVER_ERROR = "文件未找到"
        Case Else
            
            SERVER_ERROR = extractstring(header, vbCrLf, 0)
    End Select
End If
End Function

Public Function getheader(ByVal h_name) As String
On Error Resume Next
getheader = headers(h_name)
End Function

Public Property Get IsExecuting() As Boolean
IsExecuting = Still_Executing
End Property
Public Property Get IsServerOk() As Boolean
IsServerOk = Server_ok_
End Property
Public Property Get Resumesupported() As Boolean
Resumesupported = resume_supported_
End Property
Public Property Get Resuming() As Boolean
Resuming = Resuming_
End Property
Public Property Get Responsecode() As String
Responsecode = resp_code
End Property
Public Function Cancel_Download()
canceled = True
Winsock1.Close
Close #fno
RaiseEvent canceled
End Function
'==============================================
'General functions
Private Function extractstring(ByVal str$, ByVal cmp$, ByVal no%) As String
Dim arr() As String
arr = Split(str, cmp)
If no <= UBound(arr) Then
    extractstring = arr(no)
Else
    extractstring = ""
End If

End Function

Public Sub About()
Attribute About.VB_UserMemId = -552
End Sub
