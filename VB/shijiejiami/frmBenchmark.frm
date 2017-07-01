VERSION 5.00
Begin VB.Form frmBenchmark 
   BorderStyle     =   4  'Fixed ToolWindow
   Caption         =   "Benchmark Results"
   ClientHeight    =   1635
   ClientLeft      =   45
   ClientTop       =   285
   ClientWidth     =   4680
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   1635
   ScaleWidth      =   4680
   ShowInTaskbar   =   0   'False
   StartUpPosition =   3  'Windows Default
   Begin VB.Label lblDecrypt 
      BackStyle       =   0  'Transparent
      Height          =   195
      Index           =   0
      Left            =   3120
      TabIndex        =   6
      Top             =   960
      Width           =   1245
   End
   Begin VB.Label lblEncrypt 
      BackStyle       =   0  'Transparent
      Height          =   195
      Index           =   0
      Left            =   1560
      TabIndex        =   5
      Top             =   960
      Width           =   1245
   End
   Begin VB.Label Label3 
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "METHOD"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000003&
      Height          =   195
      Index           =   2
      Left            =   120
      TabIndex        =   4
      Top             =   600
      Width           =   810
   End
   Begin VB.Label Label3 
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "DECRYPTION"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000003&
      Height          =   195
      Index           =   1
      Left            =   3120
      TabIndex        =   3
      Top             =   600
      Width           =   1215
   End
   Begin VB.Label Label3 
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "ENCRYPTION"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000003&
      Height          =   195
      Index           =   0
      Left            =   1560
      TabIndex        =   2
      Top             =   600
      Width           =   1215
   End
   Begin VB.Line Line1 
      BorderColor     =   &H80000016&
      Index           =   1
      X1              =   120
      X2              =   4560
      Y1              =   495
      Y2              =   495
   End
   Begin VB.Line Line1 
      BorderColor     =   &H80000015&
      Index           =   0
      X1              =   120
      X2              =   4560
      Y1              =   480
      Y2              =   480
   End
   Begin VB.Label lblName 
      BackStyle       =   0  'Transparent
      Height          =   195
      Index           =   0
      Left            =   120
      TabIndex        =   1
      Top             =   960
      Width           =   1305
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "BENCHMARK RESULTS"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   700
         Underline       =   0   'False
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H80000003&
      Height          =   195
      Left            =   1200
      TabIndex        =   0
      Top             =   240
      Width           =   2085
   End
End
Attribute VB_Name = "frmBenchmark"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Form_Load()

  Dim a As Long
  Dim b As Long
  Dim MinHeight As Long
  Dim TimeEnc As Single
  Dim TimeDec As Single
  Dim OldTimer As Single
  Dim ByteArray() As Byte
  
  'Add the encryption names and if needed
  'resize the window to be able to show
  'all the encryption names
  For a = 0 To (EncryptObjectsCount - 1)
    If (a > 0) Then
      Load lblName(a)
      Load lblEncrypt(a)
      Load lblDecrypt(a)
    End If
    lblName(a).Top = lblName(0).Top + (lblName(0).Height * 1.2) * a
    lblEncrypt(a).Top = lblName(a).Top
    lblDecrypt(a).Top = lblName(a).Top
    lblName(a).Caption = EncryptObjects(a).Name
    lblName(a).Visible = True
    lblEncrypt(a).Visible = True
    lblDecrypt(a).Visible = True
  Next
  
  'Make sure the size of the form is okay, that
  'is all label controls are visible
  MinHeight = lblName(lblName.Count - 1).Top + lblName(lblName.Count - 1).Height + 720
  If (Me.Height < MinHeight) Then Me.Height = MinHeight
  
  'Show the window and give Windoze time
  'to draw the window
  Call Show
  DoEvents
  
  'Create a binary array of size BENCHMARKSIZE
  '(constant defined in the bas module)
  ReDim ByteArray(0 To BENCHMARKSIZE - 1)
  For a = 0 To (BENCHMARKSIZE - 1)
    ByteArray(a) = a Mod 255
  Next

  'Encrypt the byte array and then decrypt it
  'again to get the source byte array (do this
  'for every available encryption class)
  For a = LBound(EncryptObjects) To UBound(EncryptObjects)
    With EncryptObjects(a)
      ReDim Preserve ByteArray(0 To BENCHMARKSIZE - 1)
      
      'Set the key
      .Object.Key = frmMain.Text1(3).Text
        
      'Encrypt the byte array
      lblEncrypt(a).Caption = "<encrypting..>"
      lblEncrypt(a).Refresh
      OldTimer = Timer
      Call .Object.EncryptByte(ByteArray)
      TimeEnc = Timer - OldTimer
      If (TimeEnc = 0) Then TimeEnc = 0.00001
      lblEncrypt(a).Caption = (BENCHMARKSIZE / TimeEnc) \ 1000 & " kbyte/s"
      lblEncrypt(a).Refresh
      
      'Decrypt the byte array
      lblDecrypt(a).Caption = "<decrypting..>"
      lblDecrypt(a).Refresh
      OldTimer = Timer
      Call .Object.DecryptByte(ByteArray)
      TimeDec = Timer - OldTimer
      If (TimeDec = 0) Then TimeDec = 0.00001
      lblDecrypt(a).Caption = (BENCHMARKSIZE / TimeDec) \ 1000 & " kbyte/s"
      lblDecrypt(a).Refresh
      
      'Check to make sure the array is intact (this
      'is unneccessary but if someone is doubting
      'that the encryption/decryption routine is
      'working or not ;))
      For b = 0 To (BENCHMARKSIZE - 1)
        If (ByteArray(b) <> b Mod 255) Then
          Call Err.Raise(vbObjectError, , "Byte array mismatch")
        End If
      Next
    End With
  Next
  
  'All done here
  Exit Sub

ErrorHandler:
  Call MsgBox("Benchmark unsuccessful" & vbCrLf & vbCrLf & "Something went wrong during the encryption or decryption routine", vbExclamation)
  Unload Me
  
End Sub

