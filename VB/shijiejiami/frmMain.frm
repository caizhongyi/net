VERSION 5.00
Begin VB.Form frmMain 
   Caption         =   "Encryption/Decryption Example"
   ClientHeight    =   5265
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   4455
   LinkTopic       =   "Form1"
   ScaleHeight     =   5265
   ScaleWidth      =   4455
   StartUpPosition =   1  'CenterOwner
   Begin VB.ComboBox Combo1 
      Height          =   315
      ItemData        =   "frmMain.frx":0000
      Left            =   120
      List            =   "frmMain.frx":0002
      Sorted          =   -1  'True
      Style           =   2  'Dropdown List
      TabIndex        =   19
      Top             =   340
      Width           =   4215
   End
   Begin VB.TextBox Text1 
      Height          =   320
      Index           =   3
      Left            =   120
      TabIndex        =   15
      Text            =   "This is a test key"
      Top             =   2880
      Width           =   4215
   End
   Begin VB.TextBox Text1 
      Height          =   320
      Index           =   0
      Left            =   120
      TabIndex        =   10
      Text            =   "C:\Saol.txt"
      Top             =   990
      Width           =   4215
   End
   Begin VB.TextBox Text1 
      Height          =   320
      Index           =   1
      Left            =   120
      TabIndex        =   9
      Text            =   "C:\Saol.enc"
      Top             =   1620
      Width           =   4215
   End
   Begin VB.Frame Frame1 
      Caption         =   "Information"
      Height          =   1335
      Left            =   120
      TabIndex        =   1
      Top             =   3360
      Width           =   4215
      Begin VB.Label Label2 
         AutoSize        =   -1  'True
         BackStyle       =   0  'Transparent
         Caption         =   "<unknown>"
         Height          =   195
         Index           =   2
         Left            =   1800
         TabIndex        =   7
         Top             =   870
         Width           =   840
      End
      Begin VB.Label Label2 
         AutoSize        =   -1  'True
         BackStyle       =   0  'Transparent
         Caption         =   "<unknown>"
         Height          =   195
         Index           =   1
         Left            =   1800
         TabIndex        =   6
         Top             =   585
         Width           =   840
      End
      Begin VB.Label Label2 
         AutoSize        =   -1  'True
         BackStyle       =   0  'Transparent
         Caption         =   "<unknown>"
         Height          =   195
         Index           =   0
         Left            =   1800
         TabIndex        =   5
         Top             =   285
         Width           =   840
      End
      Begin VB.Label Label1 
         AutoSize        =   -1  'True
         BackStyle       =   0  'Transparent
         Caption         =   "Progress:"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   195
         Index           =   4
         Left            =   240
         TabIndex        =   4
         Top             =   870
         Width           =   810
      End
      Begin VB.Label Label1 
         AutoSize        =   -1  'True
         BackStyle       =   0  'Transparent
         Caption         =   "Time spent:"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   195
         Index           =   3
         Left            =   240
         TabIndex        =   3
         Top             =   585
         Width           =   1005
      End
      Begin VB.Label Label1 
         AutoSize        =   -1  'True
         BackStyle       =   0  'Transparent
         Caption         =   "Size:"
         BeginProperty Font 
            Name            =   "MS Sans Serif"
            Size            =   8.25
            Charset         =   0
            Weight          =   700
            Underline       =   0   'False
            Italic          =   0   'False
            Strikethrough   =   0   'False
         EndProperty
         Height          =   195
         Index           =   2
         Left            =   240
         TabIndex        =   2
         Top             =   285
         Width           =   435
      End
   End
   Begin VB.TextBox Text1 
      Height          =   320
      Index           =   2
      Left            =   120
      TabIndex        =   0
      Text            =   "C:\Saol.dec"
      Top             =   2250
      Width           =   4215
   End
   Begin VB.CommandButton Command4 
      Caption         =   "Benchmark"
      Height          =   375
      Left            =   2880
      TabIndex        =   17
      Top             =   4800
      Width           =   1335
   End
   Begin VB.CommandButton Command2 
      Caption         =   "Decrypt"
      Height          =   375
      Left            =   1560
      TabIndex        =   8
      Top             =   4800
      Width           =   1335
   End
   Begin VB.CommandButton Command1 
      Caption         =   "Encrypt"
      Height          =   375
      Left            =   240
      TabIndex        =   11
      Top             =   4800
      Width           =   1335
   End
   Begin VB.Label lblHomepage 
      Caption         =   "Read about the encryption algorithm"
      BeginProperty Font 
         Name            =   "MS Sans Serif"
         Size            =   8.25
         Charset         =   0
         Weight          =   400
         Underline       =   -1  'True
         Italic          =   0   'False
         Strikethrough   =   0   'False
      EndProperty
      ForeColor       =   &H00FF0000&
      Height          =   255
      Left            =   1560
      MousePointer    =   14  'Arrow and Question
      TabIndex        =   20
      Top             =   120
      Width           =   2775
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "Encryption Method:"
      Height          =   195
      Index           =   7
      Left            =   120
      TabIndex        =   18
      Top             =   120
      Width           =   1380
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "Key:"
      Height          =   195
      Index           =   5
      Left            =   135
      TabIndex        =   16
      Top             =   2655
      Width           =   315
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "Original File/Text:"
      Height          =   195
      Index           =   0
      Left            =   135
      TabIndex        =   14
      Top             =   765
      Width           =   1245
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "Encrypted File/Text:"
      Height          =   195
      Index           =   1
      Left            =   135
      TabIndex        =   13
      Top             =   1395
      Width           =   1440
   End
   Begin VB.Label Label1 
      AutoSize        =   -1  'True
      BackStyle       =   0  'Transparent
      Caption         =   "Decrypt to File/Decrypted Text:"
      Height          =   195
      Index           =   6
      Left            =   135
      TabIndex        =   12
      Top             =   2025
      Width           =   2235
   End
End
Attribute VB_Name = "frmMain"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private EncryptCryptAPI As clsCryptAPI
Private WithEvents EncryptTEA As clsTEA
Attribute EncryptTEA.VB_VarHelpID = -1
Private WithEvents EncryptGost As clsGost
Attribute EncryptGost.VB_VarHelpID = -1
Private WithEvents EncryptSkipJack As clsSkipjack
Attribute EncryptSkipJack.VB_VarHelpID = -1
Private WithEvents EncryptTwofish As clsTwofish
Attribute EncryptTwofish.VB_VarHelpID = -1
Private WithEvents EncryptBlowfish As clsBlowfish
Attribute EncryptBlowfish.VB_VarHelpID = -1
Private WithEvents EncryptXOR As clsSimpleXOR
Attribute EncryptXOR.VB_VarHelpID = -1
Private WithEvents EncryptRC4 As clsRC4
Attribute EncryptRC4.VB_VarHelpID = -1
Private WithEvents EncryptDES As clsDES
Attribute EncryptDES.VB_VarHelpID = -1

Private EncryptObject As Object

Private Declare Function ShellExecute Lib "shell32.dll" Alias "ShellExecuteA" (ByVal hwnd As Long, ByVal lpOperation As String, ByVal lpFile As String, ByVal lpParameters As String, ByVal lpDirectory As String, ByVal nShowCmd As Long) As Long
Private Declare Sub CopyMem Lib "kernel32" Alias "RtlMoveMemory" (Destination As Any, Source As Any, ByVal Length As Long)

Private Sub AddEncryption(Object As Object, Name As String, Optional Homepage As String)

  'Add encryption to internal array
  ReDim Preserve EncryptObjects(EncryptObjectsCount)
  With EncryptObjects(EncryptObjectsCount)
    Set .Object = Object
    .Name = Name
    .Homepage = Homepage
  End With
  EncryptObjectsCount = EncryptObjectsCount + 1
  
  'Add encryption to combobox
  Call Combo1.AddItem(Name)
  Combo1.ItemData(Combo1.NewIndex) = (EncryptObjectsCount - 1)
  
End Sub
Private Function CmpFile(File1 As String, File2 As String)

  Dim a As Long
  Dim S1 As String
  Dim S2 As String
  
  Open File1 For Binary As #1
  S1 = Space$(LOF(1))
  Get #1, , S1
  Close #1
  
  Open File2 For Binary As #2
  S2 = Space$(LOF(2))
  Get #2, , S2
  Close #2
  
  CmpFile = (S1 = S2)
  
End Function

Private Sub Combo1_Click()

  With EncryptObjects(Combo1.ItemData(Combo1.ListIndex))
    Set EncryptObject = .Object
    lblHomepage.Enabled = (Len(.Homepage) > 0)
  End With
  
End Sub
Private Sub Command1_Click()

  Dim OldTimer As Single
  
  On Error GoTo ErrorHandler
  
  'Reset the labels
  Label2(0).Caption = "<unknown>"
  Label2(1).Caption = "<unknown>"
  Label2(2).Caption = "<unknown>"
  
  'If the text fields contain filenames we
  'want to encrypt the file given
  If (Mid$(Text1(0).Text, 2, 2) = ":\") Then
    If (Mid$(Text1(1).Text, 2, 2) = ":\") Then
      Label2(0).Caption = FileLen(Text1(0).Text) & " bytes"
      OldTimer = Timer
      Call EncryptObject.EncryptFile(Text1(0).Text, Text1(1).Text, Text1(3).Text)
      Label2(1).Caption = Timer - OldTimer
      Call MsgBox("File Encryption successful.")
      Exit Sub
    End If
  End If

  'Encrypt the content of the first textbox and
  'store it in the Tag property for future use
  '(putting it into the Text property directly
  'will let VB reformat it)
  OldTimer = Timer
  Text1(1).Tag = EncryptObject.EncryptString(Text1(0).Text, Text1(3).Text)
  Text1(1).Text = Text1(1).Tag
  Label2(1).Caption = Timer - OldTimer
  Exit Sub
  
Finished:
  Call MsgBox("Encryption/Decryption successful.", vbExclamation)
  Exit Sub
  
ErrorHandler:
  Call MsgBox("Hrmm.. something went terribly wrong." & vbCrLf & vbCrLf & Err.Description, vbExclamation)

End Sub
Private Sub Command2_Click()

  Dim OldTimer As Single

  On Error GoTo ErrorHandler
  
  'Reset the labels
  Label2(0).Caption = "<unknown>"
  Label2(1).Caption = "<unknown>"
  Label2(2).Caption = "<unknown>"
  
  'If the text fields contain filenames we
  'want to encrypt the file given
  If (Mid$(Text1(0).Text, 2, 2) = ":\") Then
    If (Mid$(Text1(1).Text, 2, 2) = ":\") Then
      Label2(0).Caption = FileLen(Text1(1).Text) & " bytes"
      OldTimer = Timer
      Call EncryptObject.DecryptFile(Text1(1).Text, Text1(2).Text, Text1(3).Text)
      Label2(1).Caption = Timer - OldTimer
      Call MsgBox("File Decryption successful.")
      Exit Sub
    End If
  End If

  'Decrypt the content of the second textbox
  'making sure to use the value from the Tag
  'property instead of the Text property
  Text1(2).Text = EncryptObject.DecryptString(Text1(1).Tag, Text1(3).Text)
    
  Exit Sub
  
ErrorHandler:
  Call MsgBox("Hrmm.. something went terribly wrong." & vbCrLf & vbCrLf & Err.Description, vbExclamation)

End Sub

Private Sub Command4_Click()

  On Error Resume Next
  
  Label2(0).Caption = BENCHMARKSIZE & " bytes"
  Label2(1).Caption = "<unknown>"
  Label2(2).Caption = "<unknown>"
  
  Call frmBenchmark.Show(vbModal, Me)
  
End Sub

Private Sub EncryptBlowfish_Progress(Percent As Long)

  'Update the progress label
  Label2(2).Caption = Percent & "%"
  DoEvents

End Sub

Private Sub EncryptDES_Progress(Percent As Long)

  'Update the progress label
  Label2(2).Caption = Percent & "%"
  DoEvents

End Sub


Private Sub EncryptGost_Progress(Percent As Long)
  
  'Update the progress label
  Label2(2).Caption = Percent & "%"
  DoEvents

End Sub

Private Sub EncryptRC4_Progress(Percent As Long)

  'Update the progress label
  Label2(2).Caption = Percent & "%"
  DoEvents

End Sub


Private Sub EncryptSkipJack_Progress(Percent As Long)

  'Update the progress label
  Label2(2).Caption = Percent & "%"
  DoEvents

End Sub


Private Sub EncryptTEA_Progress(Percent As Long)

  'Update the progress label
  Label2(2).Caption = Percent & "%"
  DoEvents

End Sub


Private Sub EncryptTwofish_Progress(Percent As Long)

  'Update the progress label
  Label2(2).Caption = Percent & "%"
  DoEvents

End Sub


Private Sub EncryptXOR_Progress(Percent As Long)

  'Update the progress label
  Label2(2).Caption = Percent & "%"
  DoEvents

End Sub


Private Sub Form_Load()

  'Create instances of encryption classes
  Set EncryptSkipJack = New clsSkipjack
  Set EncryptBlowfish = New clsBlowfish
  Set EncryptCryptAPI = New clsCryptAPI
  Set EncryptTwofish = New clsTwofish
  Set EncryptXOR = New clsSimpleXOR
  Set EncryptGost = New clsGost
  Set EncryptTEA = New clsTEA
  Set EncryptRC4 = New clsRC4
  Set EncryptDES = New clsDES
  
  'Add all encryption classes to an
  'internal array for easier access
  Call AddEncryption(EncryptBlowfish, "Blowfish", "http://www.counterpane.com/blowfish.html")
  Call AddEncryption(EncryptCryptAPI, "CryptAPI")
  Call AddEncryption(EncryptDES, "DES (Data Encryption Standard)", "http://csrc.nist.gov/fips/fips46-3.pdf")
  Call AddEncryption(EncryptGost, "Gost", "http://www.jetico.sci.fi/index.htm#/gost.htm")
  Call AddEncryption(EncryptXOR, "Simple XOR", "http://tuath.pair.com/docs/xorencrypt.html")
  Call AddEncryption(EncryptRC4, "RC4", "http://www.rsasecurity.com/rsalabs/faq/3-6-3.html")
  Call AddEncryption(EncryptSkipJack, "Skipjack", "http://csrc.nist.gov/encryption/skipjack-kea.htm")
  Call AddEncryption(EncryptTEA, "TEA, A Tiny Encryption Algorithm", "http://www.cl.cam.ac.uk/Research/Papers/djw-rmn/djw-rmn-tea.html")
  Call AddEncryption(EncryptTwofish, "Twofish", "http://www.counterpane.com/twofish.html")
  
  'Pre-select the first item in the list
  Combo1.ListIndex = 0

End Sub
Function Run(strFilePath As String, Optional strParms As String, Optional strDir As String) As String
       
  Const SW_SHOW = 5
  
  'Run the Program and Evaluate errors
  Select Case ShellExecute(0, "Open", strFilePath, strParms, strDir, SW_SHOW)
  Case 0
    Run = "Insufficent system memory or corrupt program file"
  Case 2
    Run = "File not found"
  Case 3
    Run = "Invalid path"
  Case 5
    Run = "Sharing or Protection Error"
  Case 6
    Run = "Seperate data segments are required for each task"
  Case 8
    Run = "Insufficient memory to run the program"
  Case 10
    Run = "Incorrect Windows version"
  Case 11
    Run = "Invalid program file"
  Case 12
    Run = "Program file requires a different operating system"
  Case 13
    Run = "Program requires MS-DOS 4.0"
  Case 14
    Run = "Unknown program file type"
  Case 15
    Run = "Windows program does not support protected memory mode"
  Case 16
    Run = "Invalid use of data segments when loading a second instance of a program"
  Case 19
    Run = "Attempt to run a compressed program file"
  Case 20
    Run = "Invalid dynamic link library"
  Case 21
    Run = "Program requires Windows 32-bit extensions"
  Case Else
    Run = ""
  End Select

End Function

Private Sub lblHomepage_Click()

  Call Run(EncryptObjects(Combo1.ItemData(Combo1.ListIndex)).Homepage)

End Sub


