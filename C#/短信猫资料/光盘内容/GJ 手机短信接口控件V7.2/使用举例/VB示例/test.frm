VERSION 5.00
Object = "{6CAD7D08-B0A4-4196-A557-021DEC68E9B9}#1.0#0"; "SMSOCX308.ocx"
Begin VB.Form Form1 
   Caption         =   "�ֻ��ؼ�Ӧ��ʾ��V72"
   ClientHeight    =   8910
   ClientLeft      =   60
   ClientTop       =   345
   ClientWidth     =   9675
   LinkTopic       =   "Form1"
   ScaleHeight     =   8910
   ScaleWidth      =   9675
   StartUpPosition =   2  '��Ļ����
   Begin ShouYan_SmsGate7.Smsgate Smsgate1 
      Height          =   135
      Left            =   240
      TabIndex        =   45
      Top             =   7440
      Width           =   6255
      _ExtentX        =   11033
      _ExtentY        =   238
   End
   Begin VB.CommandButton Command7 
      Caption         =   "Command7"
      Height          =   255
      Left            =   6720
      TabIndex        =   44
      Top             =   4800
      Width           =   255
   End
   Begin VB.Frame Frame7 
      Caption         =   "��������"
      Height          =   2175
      Left            =   7080
      TabIndex        =   40
      Top             =   240
      Width           =   2295
      Begin VB.CommandButton Command15 
         Caption         =   "�ӵ绰"
         Height          =   495
         Left            =   480
         TabIndex        =   43
         Top             =   1440
         Width           =   1335
      End
      Begin VB.CommandButton Command14 
         Caption         =   "�ҵ绰"
         Height          =   495
         Left            =   480
         TabIndex        =   42
         Top             =   840
         Width           =   1335
      End
      Begin VB.CommandButton Command13 
         Caption         =   "��绰"
         Height          =   420
         Left            =   480
         TabIndex        =   41
         Top             =   240
         Width           =   1335
      End
   End
   Begin VB.Frame Frame3 
      Caption         =   "�ֹ�ɾ��Ϣ"
      Height          =   975
      Left            =   240
      TabIndex        =   12
      Top             =   5280
      Width           =   6255
      Begin VB.ComboBox Combo_del 
         Height          =   300
         ItemData        =   "test.frx":0000
         Left            =   360
         List            =   "test.frx":000A
         TabIndex        =   36
         Text            =   "1"
         Top             =   360
         Width           =   615
      End
      Begin VB.CommandButton Command3 
         Caption         =   "ɾ����Ϣ"
         Height          =   375
         Left            =   1080
         TabIndex        =   13
         Top             =   360
         Width           =   1095
      End
      Begin VB.Label Label6 
         AutoSize        =   -1  'True
         Caption         =   "������ֵ��1-ɾ���Ѷ���Ϣ��2-ɾ��������Ϣ��"
         Height          =   180
         Left            =   2280
         TabIndex        =   35
         Top             =   480
         Width           =   3780
      End
   End
   Begin VB.CommandButton Command10 
      Caption         =   "����״̬"
      Height          =   375
      Left            =   1920
      TabIndex        =   31
      Top             =   7800
      Width           =   1095
   End
   Begin VB.CommandButton Command8 
      Caption         =   "�رն˿�"
      Height          =   375
      Left            =   4080
      TabIndex        =   29
      Top             =   7800
      Width           =   1215
   End
   Begin VB.Frame Frame6 
      Caption         =   "��������"
      Height          =   6015
      Left            =   7080
      TabIndex        =   25
      Top             =   2640
      Width           =   2295
      Begin VB.CommandButton Command11 
         Caption         =   "�������ĺ���"
         Height          =   375
         Left            =   240
         TabIndex        =   37
         Top             =   480
         Width           =   1815
      End
      Begin VB.CommandButton Command9 
         Caption         =   "�ֻ��ͺ�"
         Height          =   375
         Left            =   240
         TabIndex        =   27
         Top             =   1680
         Width           =   1815
      End
      Begin VB.CommandButton Command3232ff 
         Caption         =   "�ֻ���������"
         Height          =   375
         Index           =   1
         Left            =   240
         TabIndex        =   26
         Top             =   1080
         Width           =   1815
      End
      Begin VB.Label Label14 
         AutoSize        =   -1  'True
         Caption         =   "�˿�״̬��δ����"
         Height          =   180
         Left            =   480
         TabIndex        =   30
         Top             =   7080
         Width           =   1440
      End
      Begin VB.Label Label12 
         Caption         =   "ʹ�ù��������κ��κ���Ҫ����������"
         Height          =   540
         Left            =   120
         TabIndex        =   28
         Top             =   3360
         Width           =   2100
      End
   End
   Begin VB.Frame Frame5 
      Caption         =   "�����Զ���������Ϣ"
      Height          =   735
      Left            =   240
      TabIndex        =   16
      Top             =   6480
      Width           =   6255
      Begin VB.CommandButton Command6 
         Caption         =   "�Զ����չ�"
         Height          =   375
         Left            =   4200
         TabIndex        =   18
         Top             =   240
         Width           =   1455
      End
      Begin VB.CommandButton Command5 
         Caption         =   "�Զ����տ�"
         Height          =   375
         Left            =   1080
         TabIndex        =   17
         Top             =   240
         Width           =   1335
      End
   End
   Begin VB.Frame Frame4 
      Caption         =   "�����ֻ�"
      Height          =   1215
      Left            =   240
      TabIndex        =   14
      Top             =   240
      Width           =   6255
      Begin VB.ComboBox Combo_rate 
         Height          =   300
         ItemData        =   "test.frx":0014
         Left            =   4800
         List            =   "test.frx":0030
         TabIndex        =   33
         Text            =   "9600"
         Top             =   240
         Width           =   1095
      End
      Begin VB.ComboBox Combo_com 
         Height          =   300
         ItemData        =   "test.frx":006B
         Left            =   1200
         List            =   "test.frx":0087
         TabIndex        =   22
         Text            =   "1"
         Top             =   240
         Width           =   495
      End
      Begin VB.TextBox Text_serno 
         Height          =   270
         Left            =   2640
         MaxLength       =   14
         TabIndex        =   19
         Text            =   "+8613800200500"
         Top             =   240
         Width           =   1335
      End
      Begin VB.CommandButton Command4 
         Caption         =   "���ӣ���ʼ���ֻ���"
         Height          =   375
         Left            =   600
         TabIndex        =   15
         Top             =   720
         Width           =   5295
      End
      Begin VB.Label Label15 
         AutoSize        =   -1  'True
         Caption         =   "���ʣ�"
         Height          =   180
         Left            =   4200
         TabIndex        =   32
         Top             =   360
         Width           =   540
      End
      Begin VB.Label Label8 
         Caption         =   "�������ģ�"
         Height          =   255
         Left            =   1800
         TabIndex        =   21
         Top             =   360
         Width           =   1455
      End
      Begin VB.Label Label7 
         AutoSize        =   -1  'True
         Caption         =   "�˿�(COM)��"
         Height          =   180
         Left            =   240
         TabIndex        =   20
         Top             =   360
         Width           =   990
      End
   End
   Begin VB.Frame Frame2 
      Caption         =   "����Ϣ"
      Height          =   975
      Left            =   240
      TabIndex        =   7
      Top             =   4080
      Width           =   6255
      Begin VB.CommandButton Command2 
         Caption         =   "����Ϣ"
         Height          =   375
         Left            =   1800
         TabIndex        =   10
         Top             =   360
         Width           =   975
      End
      Begin VB.ComboBox Combo_rewhy 
         Height          =   300
         ItemData        =   "test.frx":00A3
         Left            =   1080
         List            =   "test.frx":00B0
         TabIndex        =   8
         Text            =   "4"
         Top             =   360
         Width           =   615
      End
      Begin VB.Label Label4 
         Caption         =   "������ֵ��0-����ȡδ����Ϣ��1-��ȡ�Ѷ���Ϣ��4-��ȡ������Ϣ��"
         Height          =   495
         Left            =   2880
         TabIndex        =   11
         Top             =   360
         Width           =   3255
      End
      Begin VB.Label Label3 
         Caption         =   "����ֵ��"
         Height          =   375
         Left            =   240
         TabIndex        =   9
         Top             =   480
         Width           =   855
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "����Ϣ"
      Height          =   2175
      Left            =   240
      TabIndex        =   0
      Top             =   1680
      Width           =   6255
      Begin VB.CommandButton Command12 
         Caption         =   "������������"
         Height          =   375
         Left            =   4440
         TabIndex        =   39
         Top             =   240
         Width           =   1335
      End
      Begin VB.CheckBox Check_report 
         Caption         =   "���ͱ���"
         Height          =   255
         Left            =   120
         TabIndex        =   38
         Top             =   1800
         Width           =   1335
      End
      Begin VB.CommandButton Command1 
         Caption         =   "����ͨ��Ϣ"
         Height          =   375
         Left            =   2760
         TabIndex        =   5
         Top             =   240
         Width           =   1335
      End
      Begin VB.TextBox Text_nz 
         Height          =   855
         Left            =   1080
         MultiLine       =   -1  'True
         ScrollBars      =   2  'Vertical
         TabIndex        =   4
         Text            =   "test.frx":00BD
         Top             =   840
         Width           =   4815
      End
      Begin VB.TextBox Text_tomb 
         Height          =   270
         Left            =   1080
         TabIndex        =   1
         Text            =   "13437833281"
         Top             =   240
         Width           =   1455
      End
      Begin VB.Label Label16 
         AutoSize        =   -1  'True
         ForeColor       =   &H00C00000&
         Height          =   180
         Left            =   1440
         TabIndex        =   34
         Top             =   1800
         Width           =   90
      End
      Begin VB.Label Label10 
         Height          =   375
         Left            =   360
         TabIndex        =   23
         Top             =   1560
         Width           =   375
      End
      Begin VB.Label Label2 
         Height          =   375
         Left            =   2880
         TabIndex        =   6
         Top             =   360
         Width           =   615
      End
      Begin VB.Label Label1 
         AutoSize        =   -1  'True
         Caption         =   "�������ݣ�"
         Height          =   180
         Index           =   1
         Left            =   120
         TabIndex        =   3
         Top             =   960
         Width           =   900
      End
      Begin VB.Label Label1 
         AutoSize        =   -1  'True
         Caption         =   "�ֻ����룺"
         Height          =   180
         Index           =   0
         Left            =   120
         TabIndex        =   2
         Top             =   360
         Width           =   900
      End
   End
   Begin VB.Label Label5 
      AutoSize        =   -1  'True
      Caption         =   "˵�������ڲ���ǰ���ֹ���պͱ����ֻ�SIM���еġ��յ���Ϣ��"
      Height          =   180
      Left            =   960
      TabIndex        =   24
      Top             =   8400
      Width           =   5130
   End
End
Attribute VB_Name = "Form1"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False

Private Sub Command1_Click()
   
   Label16 = "���ڷ�����Ϣ"
   
  aaa = Me.Smsgate1.Sendsms(Me.Text_nz, Me.Text_tomb, Me.Check_report, False)
  If aaa = "y" Then Me.Label16 = "��Ϣ�ѷ��ͣ�" Else Me.Label16 = aaa
  

End Sub



Private Sub Command10_Click()
  MsgBox CStr(Me.Smsgate1.Link)
End Sub




Private Sub Command11_Click()
  MsgBox Me.Smsgate1.M_ServiceNo
End Sub



Private Sub Command13_Click()

aa = CStr(InputBox("������绰���룺"))
If aa = "" Then Exit Sub

STT = Timer

ATTT = Timer - STT

MsgBox Me.Smsgate1.CallPhone(CStr(aa), 100) & "-" & CStr(Timer - STT)


End Sub

Private Sub Command12_Click()
 MsgBox Me.Smsgate1.SendAsc2(Me.Text_nz, Me.Text_tomb, Me.Check_report.Value)
End Sub

Private Sub Command14_Click()
 MsgBox Me.Smsgate1.HangUpCall
End Sub

Private Sub Command15_Click()
 MsgBox Me.Smsgate1.AnswerCall
End Sub







Private Sub Command16_Click()
 Me.Text_nz = "������è�Ĳ�Ʒ���ǣ�" + Me.Smsgate1.Get_ID3
End Sub









Private Sub Command2_Click()
     aa = Me.Smsgate1.ReadMsg(Me.Combo_rewhy)
     MsgBox aa
     Me.Text_nz = aa
     
     MsgBox "���ϵ�Ϊ���ν��յ����ж������ݣ�����ֽ��ÿ���������绰���롢�����Լ��յ�ʱ�䣩��������ϸ��ʹ�÷����ɲο���Դ����"
     
     messageall = Split(aa, Chr(1))
            aak = 0
            For Each onem In messageall
                MsgBox onem
            Next
            
      '=======================��ϸ�ķֽ��յ���Ϣ���������ݿ��Դ�� �������� in
      
            '      aa = Me.Smsgate1.ReadMsg(1)     '           �ѱ����յ�������(�Ѷ���Ϣ)�������aa
            '      messageall = Split(aa, Chr(1))              '��aa������chr(1)���зָ��ÿ��
            '            aak = 0
            '          For Each onem In messageall              'ȡ��ÿ����Ϣ����onem�������绰�����ݣ�ʱ�䣩
            '              rs.addnew                            '�����ݿ�������һ�¼�¼
            '              kkk = 0                              '
            '              meVV = Split(onem, Chr(2))           '��ÿ������������chr(2)���зֽ�Ϊÿ����ϸֵ������Ϊ���绰���롱�������ݡ�����ʱ�䡱
            '              For Each icc In meVV                 '�ֱ�ȡ���绰�����ݣ�ʱ��
            '                kkk = kkk + 1
            '                If kkk = 1 Then rs("moble") = icc
            '                If kkk = 2 Then rs("msg") = icc
            '                If kkk = 3 Then rs("times") = icc
            '              Next
            '              rs.Update                            '�����¼
            '          Next
       '=======================��ϸ�ķֽ��յ���Ϣ��Դ��  end
            
End Sub

Private Sub Command22_Click(Index As Integer)
 MsgBox Me.Smsgate1.ReadNB("rc")
End Sub

Private Sub Command3_Click()
    aa = Me.Smsgate1.DelSms(Combo_del)
  MsgBox aa
If aa = "y" Then
 
If Combo_del = 1 Then MsgBox "�ɹ�ɾ���Ѷ���Ϣ"
If Combo_del = 2 Then MsgBox "�ɹ�ɾ��������Ϣ"

End If
End Sub

Private Sub Command3232_Click(Index As Integer)
  MsgBox Me.Smsgate1.ReadNB("mc")
End Sub

Private Sub Command3232ff_Click(Index As Integer)
   MsgBox Me.Smsgate1.M_ltd
   
End Sub

Private Sub Command4_Click()
  Me.Smsgate1.CommPort = Combo_com
  Me.Smsgate1.SmsService = Me.Text_serno
  Me.Smsgate1.Settings = CStr(Me.Combo_rate) & ",n,8,1"
  
  a = Me.Smsgate1.Connect(2)
   If a = "y" Then MsgBox "�ɹ����ӣ������շ������ˣ�"
  If a = "y" Then
   Command4.Caption = Command4.Caption + "-[�ɹ�]"
   Text1_usrno = Me.Smsgate1.M_imei
   

   
  Else
  
  MsgBox a
   
  End If
End Sub

Private Sub Command5_Click()
 MsgBox Me.Smsgate1.RevAuto
End Sub

Private Sub Command6_Click()
   MsgBox Me.Smsgate1.RevAutoClose
End Sub








Private Sub Command8_Click()
 MsgBox Me.Smsgate1.ClosePort
End Sub

Private Sub Command9_Click()
  MsgBox Me.Smsgate1.M_model
End Sub

Private Sub Commandsssd_Click(Index As Integer)
    MsgBox Me.Smsgate1.ReadNB("sm")
End Sub





Private Sub Form_Unload(Cancel As Integer)
 Me.Smsgate1.ClosePort
End Sub

Private Sub Smsgate1_OnCall(PhoneNo As Variant)
 MsgBox "�е绰������" + PhoneNo
End Sub

Private Sub Smsgate1_OnRecvMsg()
 MsgBox "������Ϣ����"

MsgBox Me.Smsgate1.NewMsg
 
End Sub

Private Sub Smsgate1_OnRevReport()

 MsgBox Me.Smsgate1.NewReport
End Sub

Private Sub Smsgate1_OnStatusChange()

 If Me.Smsgate1.Isbusy = False Then Me.Label14 = " �˿�״̬������" Else Me.Label14 = " �˿�״̬��������"
End Sub

Private Sub Text_nz_Change()
  Label10.Caption = Str(Len(Me.Text_nz))
End Sub

