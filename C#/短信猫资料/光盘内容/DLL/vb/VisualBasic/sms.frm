VERSION 5.00
Begin VB.Form mainsms 
   BorderStyle     =   1  'Fixed Single
   Caption         =   "�����շ����ο����ӿ�����Դ��(VB��)"
   ClientHeight    =   6630
   ClientLeft      =   45
   ClientTop       =   330
   ClientWidth     =   9225
   LinkTopic       =   "Form1"
   MaxButton       =   0   'False
   MinButton       =   0   'False
   ScaleHeight     =   6630
   ScaleWidth      =   9225
   StartUpPosition =   3  '����ȱʡ
   Begin VB.TextBox MobPort 
      Height          =   330
      Left            =   1050
      TabIndex        =   24
      Text            =   "0"
      Top             =   150
      Width           =   555
   End
   Begin VB.Frame Frame5 
      Caption         =   "�Զ����ն���"
      Height          =   1455
      Left            =   4200
      TabIndex        =   18
      Top             =   120
      Width           =   4845
      Begin VB.CommandButton Sms_Start_Button 
         Caption         =   "��  ��"
         Height          =   465
         Left            =   780
         TabIndex        =   22
         Top             =   810
         Width           =   1365
      End
      Begin VB.Timer NewSms_Timer 
         Enabled         =   0   'False
         Interval        =   1000
         Left            =   3990
         Top             =   0
      End
      Begin VB.CommandButton Sms_Close_Button 
         Caption         =   "��  ��"
         Enabled         =   0   'False
         Height          =   465
         Left            =   2640
         TabIndex        =   20
         Top             =   810
         Width           =   1365
      End
      Begin VB.Label NewSms_Show 
         Alignment       =   2  'Center
         BackStyle       =   0  'Transparent
         BorderStyle     =   1  'Fixed Single
         Caption         =   "�Զ����ն��Ź��ܴ��ڹر�״̬"
         Height          =   435
         Left            =   180
         TabIndex        =   19
         Top             =   240
         Width           =   4485
      End
   End
   Begin VB.CommandButton Sms_Exit_Button 
      Caption         =   "��  ��"
      Height          =   465
      Left            =   7740
      TabIndex        =   16
      Top             =   5610
      Width           =   1365
   End
   Begin VB.Frame Frame4 
      Caption         =   "ɾ������"
      Height          =   855
      Left            =   4200
      TabIndex        =   11
      Top             =   5370
      Width           =   3405
      Begin VB.CommandButton Sms_Delete_Button 
         Caption         =   "ɾ  ��"
         Height          =   465
         Left            =   1890
         TabIndex        =   14
         Top             =   240
         Width           =   1365
      End
      Begin VB.TextBox DeleteSms_Index 
         Height          =   465
         Left            =   1260
         TabIndex        =   13
         Top             =   240
         Width           =   495
      End
      Begin VB.Label Label4 
         Caption         =   "���������ţ�"
         Height          =   195
         Left            =   150
         TabIndex        =   12
         Top             =   390
         Width           =   1125
      End
   End
   Begin VB.Frame Frame3 
      Caption         =   "���ն���"
      Height          =   3225
      Left            =   4200
      TabIndex        =   9
      Top             =   1860
      Width           =   4845
      Begin VB.CommandButton Sms_Receive_Button 
         Caption         =   "��  ��"
         Height          =   465
         Left            =   1620
         TabIndex        =   15
         Top             =   2520
         Width           =   1365
      End
      Begin VB.TextBox ReceiveSms_Text 
         Height          =   2055
         Left            =   180
         MultiLine       =   -1  'True
         ScrollBars      =   2  'Vertical
         TabIndex        =   10
         Top             =   300
         Width           =   4455
      End
   End
   Begin VB.Frame Frame2 
      Caption         =   "���Ͷ���"
      Height          =   3735
      Left            =   180
      TabIndex        =   2
      Top             =   2490
      Width           =   3795
      Begin VB.CommandButton Sms_Send_Button 
         Caption         =   "��  ��"
         Height          =   465
         Left            =   1260
         TabIndex        =   8
         Top             =   3150
         Width           =   1365
      End
      Begin VB.TextBox TelNum_Text 
         Height          =   345
         Left            =   150
         TabIndex        =   6
         Top             =   2280
         Width           =   3525
      End
      Begin VB.TextBox SendSms_Text 
         Height          =   1425
         Left            =   120
         MultiLine       =   -1  'True
         TabIndex        =   3
         Top             =   510
         Width           =   3555
      End
      Begin VB.Label Label3 
         Caption         =   "ע�������������70�����ֻ�180��Ӣ����ĸ,     ����ʱ�Զ��ֶη��͡�"
         Height          =   435
         Left            =   120
         TabIndex        =   7
         Top             =   2700
         Width           =   3615
      End
      Begin VB.Label Label2 
         AutoSize        =   -1  'True
         Caption         =   "�ֻ����룺"
         Height          =   180
         Left            =   150
         TabIndex        =   5
         Top             =   2040
         Width           =   825
      End
      Begin VB.Label Label1 
         Caption         =   "�������ݣ�"
         Height          =   225
         Left            =   120
         TabIndex        =   4
         Top             =   270
         Width           =   1185
      End
   End
   Begin VB.Frame Frame1 
      Caption         =   "����GSM MODEM"
      Height          =   1455
      Left            =   180
      TabIndex        =   0
      Top             =   870
      Width           =   3795
      Begin VB.CommandButton Sms_Connection_Button 
         Caption         =   "��  ��"
         Height          =   465
         Left            =   360
         TabIndex        =   21
         Top             =   810
         Width           =   1365
      End
      Begin VB.CommandButton Sms_Disconnection_Button 
         Caption         =   "��  ��"
         Height          =   465
         Left            =   2010
         TabIndex        =   17
         Top             =   810
         Width           =   1365
      End
      Begin VB.Label State_Show 
         Alignment       =   2  'Center
         BackStyle       =   0  'Transparent
         BorderStyle     =   1  'Fixed Single
         Height          =   435
         Left            =   150
         TabIndex        =   1
         Top             =   240
         Width           =   3495
      End
   End
   Begin VB.Label Label7 
      Caption         =   "ע��0Ϊ����ӿڣ�1,2,3,...Ϊ����"
      Height          =   225
      Left            =   180
      TabIndex        =   25
      Top             =   570
      Width           =   3795
   End
   Begin VB.Label Label6 
      AutoSize        =   -1  'True
      Caption         =   "�˿ںţ�"
      Height          =   180
      Left            =   180
      TabIndex        =   23
      Top             =   240
      Width           =   720
   End
End
Attribute VB_Name = "mainsms"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = False
Option Explicit

Private Sub Form_Load()
Sms_Send_Button.Enabled = False
Sms_Receive_Button.Enabled = False
Sms_Delete_Button.Enabled = False
SendSms_Text.Enabled = False
TelNum_Text.Enabled = False
ReceiveSms_Text.Enabled = False
DeleteSms_Index.Enabled = False
Sms_Disconnection_Button.Enabled = False
Sms_Start_Button.Enabled = False
NewSms_Show.Caption = ""
NewSms_Show.Enabled = False
End Sub

Private Sub Sms_Delete_Button_Click()
Screen.MousePointer = vbHourglass
Sms_Delete_Button.Enabled = False
Sms_Delete (Trim(DeleteSms_Index.Text))
Sms_Delete_Button.Enabled = True
Screen.MousePointer = vbDefault
End Sub

Private Sub Sms_Exit_Button_Click()
  Unload Me
End Sub

Private Sub Sms_Receive_Button_Click()
Screen.MousePointer = vbHourglass
Sms_Receive_Button.Enabled = False
Dim ReceiveSmsStr As String
If Sms_Receive("4", ReceiveSmsStr) Then
   ReceiveSms_Text.Text = ReceiveSmsStr
End If
Sms_Receive_Button.Enabled = True

If Sms_AutoFlag() Then
    If Sms_Start_Button.Enabled = True Then
       NewSms_Show.Caption = "�Զ����ն��Ź��ܴ��ڹر�״̬"
    Else
       NewSms_Show.Caption = "δ�յ��¶���"
    End If
    
  Else
    NewSms_Show.Caption = "�ö���è��֧���Զ����ն��Ź���"
  End If
Screen.MousePointer = vbDefault
End Sub

Private Sub Sms_Disconnection_Button_Click()
Screen.MousePointer = vbHourglass
Sms_Disconnection_Button.Enabled = False
Sms_Disconnection
Sms_Connection_Button.Enabled = True
Sms_Send_Button.Enabled = False
Sms_Receive_Button.Enabled = False
Sms_Delete_Button.Enabled = False
SendSms_Text.Enabled = False
TelNum_Text.Enabled = False
ReceiveSms_Text.Enabled = False
DeleteSms_Index.Enabled = False
Sms_Start_Button.Enabled = False
Sms_Close_Button.Enabled = False
State_Show.Caption = ""
NewSms_Show.Caption = ""
NewSms_Show.Enabled = False
NewSms_Timer.Enabled = False
Screen.MousePointer = vbDefault
End Sub

Private Sub Sms_Connection_Button_Click()
Load Me
Screen.MousePointer = vbHourglass
Sms_Connection_Button.Enabled = False
Dim TypeStr As String
Dim CopyRightToCOMStr As String
Dim CopyRightStr As String
    CopyRightStr = "//�����й����������޹�˾,��ַwww.gprscat.com //"

If Sms_Connection(CopyRightStr, CInt(MobPort.Text), 9600, TypeStr, CopyRightToCOMStr) Then '��ʹ��ŵ�����ƶ��绰,��ʹ�������׼����⴮������
   State_Show.Caption = "���Ӷ���è�ɹ�" & Chr(10) & "(����è�ͺ�Ϊ��" & TypeStr & ")"
   Sms_Send_Button.Enabled = True
   Sms_Receive_Button.Enabled = True
   Sms_Delete_Button.Enabled = True
   SendSms_Text.Enabled = True
   TelNum_Text.Enabled = True
   ReceiveSms_Text.Enabled = True
   DeleteSms_Index.Enabled = True
   Sms_Disconnection_Button.Enabled = True
   Sms_Start_Button.Enabled = True
   NewSms_Show.Caption = "�Զ����ն��Ź��ܴ��ڹر�״̬"
   NewSms_Show.Enabled = True
   NewSms_Timer.Enabled = False
Else
   State_Show.Caption = "���Ӷ���èʧ��" & Chr(10) & "(���������Ӷ���è)"
   Sms_Connection_Button.Enabled = True
End If
Screen.MousePointer = vbDefault

End Sub

Private Sub Sms_Send_Button_Click()
Screen.MousePointer = vbHourglass
Sms_Send_Button.Enabled = False
If Len(Trim(TelNum_Text.Text)) >= 11 And Sms_Send(Trim(TelNum_Text.Text), Trim(SendSms_Text.Text)) Then
   MsgBox "���Ͷ��ųɹ���", vbInformation, "��ʾ"
Else
   MsgBox "���Ͷ���ʧ�ܣ�", vbCritical, "����"
End If
Sms_Send_Button.Enabled = True
Screen.MousePointer = vbDefault
End Sub

Private Sub Sms_Start_Button_Click()
  If Sms_AutoFlag() Then
    NewSms_Show.Caption = "δ�յ��¶���"
    Sms_Start_Button.Enabled = False
    Sms_Close_Button.Enabled = True
    NewSms_Timer.Enabled = True
  Else
    NewSms_Show.Caption = "�ö���è��֧���Զ����ն��Ź���"
  End If
End Sub

Private Sub Sms_Close_Button_Click()
    NewSms_Show.Caption = "�Զ����ն��Ź��ܴ��ڹر�״̬"
    Sms_Start_Button.Enabled = True
    NewSms_Timer.Enabled = False
    Sms_Close_Button.Enabled = False
End Sub

Private Sub NewSms_Timer_Timer()
  If Sms_NewFlag() Then
     NewSms_Show.Caption = "�յ��¶���,�����!"
  End If
End Sub


