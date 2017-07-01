Option Strict Off
Option Explicit On
Friend Class mainsms
	Inherits System.Windows.Forms.Form
#Region "Windows 窗体设计器生成的代码"
	Public Sub New()
		MyBase.New()
		If m_vb6FormDefInstance Is Nothing Then
			If m_InitializingDefInstance Then
				m_vb6FormDefInstance = Me
			Else
				Try 
					'对于启动窗体，所创建的第一个实例为默认实例。
					If System.Reflection.Assembly.GetExecutingAssembly.EntryPoint.DeclaringType Is Me.GetType Then
						m_vb6FormDefInstance = Me
					End If
				Catch
				End Try
			End If
		End If
		'此调用是 Windows 窗体设计器所必需的。
		InitializeComponent()
	End Sub
	'窗体重写处置，以清理组件列表。
	Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Windows 窗体设计器所必需的
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents MobPort As System.Windows.Forms.TextBox
	Public WithEvents Sms_Start_Button As System.Windows.Forms.Button
	Public WithEvents NewSms_Timer As System.Windows.Forms.Timer
	Public WithEvents Sms_Close_Button As System.Windows.Forms.Button
	Public WithEvents NewSms_Show As System.Windows.Forms.Label
	Public WithEvents Frame5 As System.Windows.Forms.GroupBox
	Public WithEvents Sms_Exit_Button As System.Windows.Forms.Button
	Public WithEvents Sms_Delete_Button As System.Windows.Forms.Button
	Public WithEvents DeleteSms_Index As System.Windows.Forms.TextBox
	Public WithEvents Label4 As System.Windows.Forms.Label
	Public WithEvents Frame4 As System.Windows.Forms.GroupBox
	Public WithEvents Sms_Receive_Button As System.Windows.Forms.Button
	Public WithEvents ReceiveSms_Text As System.Windows.Forms.TextBox
	Public WithEvents Frame3 As System.Windows.Forms.GroupBox
	Public WithEvents Sms_Send_Button As System.Windows.Forms.Button
	Public WithEvents TelNum_Text As System.Windows.Forms.TextBox
	Public WithEvents SendSms_Text As System.Windows.Forms.TextBox
	Public WithEvents Label3 As System.Windows.Forms.Label
	Public WithEvents Label2 As System.Windows.Forms.Label
	Public WithEvents Label1 As System.Windows.Forms.Label
	Public WithEvents Frame2 As System.Windows.Forms.GroupBox
	Public WithEvents Sms_Connection_Button As System.Windows.Forms.Button
	Public WithEvents Sms_Disconnection_Button As System.Windows.Forms.Button
	Public WithEvents State_Show As System.Windows.Forms.Label
	Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    '注意：以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器来修改它。
    '不要使用代码编辑器来修改它。
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.MobPort = New System.Windows.Forms.TextBox
        Me.Frame5 = New System.Windows.Forms.GroupBox
        Me.Sms_Start_Button = New System.Windows.Forms.Button
        Me.Sms_Close_Button = New System.Windows.Forms.Button
        Me.NewSms_Show = New System.Windows.Forms.Label
        Me.NewSms_Timer = New System.Windows.Forms.Timer(Me.components)
        Me.Sms_Exit_Button = New System.Windows.Forms.Button
        Me.Frame4 = New System.Windows.Forms.GroupBox
        Me.Sms_Delete_Button = New System.Windows.Forms.Button
        Me.DeleteSms_Index = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Frame3 = New System.Windows.Forms.GroupBox
        Me.Sms_Receive_Button = New System.Windows.Forms.Button
        Me.ReceiveSms_Text = New System.Windows.Forms.TextBox
        Me.Frame2 = New System.Windows.Forms.GroupBox
        Me.Sms_Send_Button = New System.Windows.Forms.Button
        Me.TelNum_Text = New System.Windows.Forms.TextBox
        Me.SendSms_Text = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.Sms_Connection_Button = New System.Windows.Forms.Button
        Me.Sms_Disconnection_Button = New System.Windows.Forms.Button
        Me.State_Show = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Frame5.SuspendLayout()
        Me.Frame4.SuspendLayout()
        Me.Frame3.SuspendLayout()
        Me.Frame2.SuspendLayout()
        Me.Frame1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MobPort
        '
        Me.MobPort.AcceptsReturn = True
        Me.MobPort.BackColor = System.Drawing.SystemColors.Window
        Me.MobPort.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.MobPort.ForeColor = System.Drawing.SystemColors.WindowText
        Me.MobPort.Location = New System.Drawing.Point(96, 8)
        Me.MobPort.MaxLength = 0
        Me.MobPort.Name = "MobPort"
        Me.MobPort.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.MobPort.Size = New System.Drawing.Size(37, 21)
        Me.MobPort.TabIndex = 25
        Me.MobPort.Text = "0"
        '
        'Frame5
        '
        Me.Frame5.BackColor = System.Drawing.SystemColors.Control
        Me.Frame5.Controls.Add(Me.Sms_Start_Button)
        Me.Frame5.Controls.Add(Me.Sms_Close_Button)
        Me.Frame5.Controls.Add(Me.NewSms_Show)
        Me.Frame5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame5.Location = New System.Drawing.Point(280, 8)
        Me.Frame5.Name = "Frame5"
        Me.Frame5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame5.Size = New System.Drawing.Size(320, 97)
        Me.Frame5.TabIndex = 19
        Me.Frame5.TabStop = False
        Me.Frame5.Text = "自动接收短信"
        '
        'Sms_Start_Button
        '
        Me.Sms_Start_Button.BackColor = System.Drawing.SystemColors.Control
        Me.Sms_Start_Button.Cursor = System.Windows.Forms.Cursors.Default
        Me.Sms_Start_Button.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Sms_Start_Button.Location = New System.Drawing.Point(56, 56)
        Me.Sms_Start_Button.Name = "Sms_Start_Button"
        Me.Sms_Start_Button.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Sms_Start_Button.Size = New System.Drawing.Size(91, 31)
        Me.Sms_Start_Button.TabIndex = 23
        Me.Sms_Start_Button.Text = "启  动"
        Me.Sms_Start_Button.UseVisualStyleBackColor = False
        '
        'Sms_Close_Button
        '
        Me.Sms_Close_Button.BackColor = System.Drawing.SystemColors.Control
        Me.Sms_Close_Button.Cursor = System.Windows.Forms.Cursors.Default
        Me.Sms_Close_Button.Enabled = False
        Me.Sms_Close_Button.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Sms_Close_Button.Location = New System.Drawing.Point(168, 56)
        Me.Sms_Close_Button.Name = "Sms_Close_Button"
        Me.Sms_Close_Button.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Sms_Close_Button.Size = New System.Drawing.Size(91, 31)
        Me.Sms_Close_Button.TabIndex = 21
        Me.Sms_Close_Button.Text = "关  闭"
        Me.Sms_Close_Button.UseVisualStyleBackColor = False
        '
        'NewSms_Show
        '
        Me.NewSms_Show.BackColor = System.Drawing.Color.Transparent
        Me.NewSms_Show.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.NewSms_Show.Cursor = System.Windows.Forms.Cursors.Default
        Me.NewSms_Show.ForeColor = System.Drawing.SystemColors.ControlText
        Me.NewSms_Show.Location = New System.Drawing.Point(8, 16)
        Me.NewSms_Show.Name = "NewSms_Show"
        Me.NewSms_Show.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.NewSms_Show.Size = New System.Drawing.Size(299, 29)
        Me.NewSms_Show.TabIndex = 20
        Me.NewSms_Show.Text = "自动接收短信功能处于关闭状态"
        Me.NewSms_Show.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'NewSms_Timer
        '
        Me.NewSms_Timer.Interval = 1000
        '
        'Sms_Exit_Button
        '
        Me.Sms_Exit_Button.BackColor = System.Drawing.SystemColors.Control
        Me.Sms_Exit_Button.Cursor = System.Windows.Forms.Cursors.Default
        Me.Sms_Exit_Button.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Sms_Exit_Button.Location = New System.Drawing.Point(512, 376)
        Me.Sms_Exit_Button.Name = "Sms_Exit_Button"
        Me.Sms_Exit_Button.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Sms_Exit_Button.Size = New System.Drawing.Size(91, 31)
        Me.Sms_Exit_Button.TabIndex = 17
        Me.Sms_Exit_Button.Text = "退  出"
        Me.Sms_Exit_Button.UseVisualStyleBackColor = False
        '
        'Frame4
        '
        Me.Frame4.BackColor = System.Drawing.SystemColors.Control
        Me.Frame4.Controls.Add(Me.Sms_Delete_Button)
        Me.Frame4.Controls.Add(Me.DeleteSms_Index)
        Me.Frame4.Controls.Add(Me.Label4)
        Me.Frame4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame4.Location = New System.Drawing.Point(280, 360)
        Me.Frame4.Name = "Frame4"
        Me.Frame4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame4.Size = New System.Drawing.Size(227, 57)
        Me.Frame4.TabIndex = 11
        Me.Frame4.TabStop = False
        Me.Frame4.Text = "删除短信"
        '
        'Sms_Delete_Button
        '
        Me.Sms_Delete_Button.BackColor = System.Drawing.SystemColors.Control
        Me.Sms_Delete_Button.Cursor = System.Windows.Forms.Cursors.Default
        Me.Sms_Delete_Button.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Sms_Delete_Button.Location = New System.Drawing.Point(128, 16)
        Me.Sms_Delete_Button.Name = "Sms_Delete_Button"
        Me.Sms_Delete_Button.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Sms_Delete_Button.Size = New System.Drawing.Size(91, 31)
        Me.Sms_Delete_Button.TabIndex = 14
        Me.Sms_Delete_Button.Text = "删  除"
        Me.Sms_Delete_Button.UseVisualStyleBackColor = False
        '
        'DeleteSms_Index
        '
        Me.DeleteSms_Index.AcceptsReturn = True
        Me.DeleteSms_Index.BackColor = System.Drawing.SystemColors.Window
        Me.DeleteSms_Index.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.DeleteSms_Index.ForeColor = System.Drawing.SystemColors.WindowText
        Me.DeleteSms_Index.Location = New System.Drawing.Point(88, 16)
        Me.DeleteSms_Index.MaxLength = 0
        Me.DeleteSms_Index.Name = "DeleteSms_Index"
        Me.DeleteSms_Index.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.DeleteSms_Index.Size = New System.Drawing.Size(33, 21)
        Me.DeleteSms_Index.TabIndex = 13
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.SystemColors.Control
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(16, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "短信索引号："
        '
        'Frame3
        '
        Me.Frame3.BackColor = System.Drawing.SystemColors.Control
        Me.Frame3.Controls.Add(Me.Sms_Receive_Button)
        Me.Frame3.Controls.Add(Me.ReceiveSms_Text)
        Me.Frame3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame3.Location = New System.Drawing.Point(280, 120)
        Me.Frame3.Name = "Frame3"
        Me.Frame3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame3.Size = New System.Drawing.Size(320, 208)
        Me.Frame3.TabIndex = 9
        Me.Frame3.TabStop = False
        Me.Frame3.Text = "接收短信"
        '
        'Sms_Receive_Button
        '
        Me.Sms_Receive_Button.BackColor = System.Drawing.SystemColors.Control
        Me.Sms_Receive_Button.Cursor = System.Windows.Forms.Cursors.Default
        Me.Sms_Receive_Button.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Sms_Receive_Button.Location = New System.Drawing.Point(104, 168)
        Me.Sms_Receive_Button.Name = "Sms_Receive_Button"
        Me.Sms_Receive_Button.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Sms_Receive_Button.Size = New System.Drawing.Size(91, 31)
        Me.Sms_Receive_Button.TabIndex = 15
        Me.Sms_Receive_Button.Text = "接  收"
        Me.Sms_Receive_Button.UseVisualStyleBackColor = False
        '
        'ReceiveSms_Text
        '
        Me.ReceiveSms_Text.AcceptsReturn = True
        Me.ReceiveSms_Text.BackColor = System.Drawing.SystemColors.Window
        Me.ReceiveSms_Text.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.ReceiveSms_Text.ForeColor = System.Drawing.SystemColors.WindowText
        Me.ReceiveSms_Text.Location = New System.Drawing.Point(8, 24)
        Me.ReceiveSms_Text.MaxLength = 0
        Me.ReceiveSms_Text.Multiline = True
        Me.ReceiveSms_Text.Name = "ReceiveSms_Text"
        Me.ReceiveSms_Text.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ReceiveSms_Text.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.ReceiveSms_Text.Size = New System.Drawing.Size(304, 137)
        Me.ReceiveSms_Text.TabIndex = 10
        '
        'Frame2
        '
        Me.Frame2.BackColor = System.Drawing.SystemColors.Control
        Me.Frame2.Controls.Add(Me.Sms_Send_Button)
        Me.Frame2.Controls.Add(Me.TelNum_Text)
        Me.Frame2.Controls.Add(Me.SendSms_Text)
        Me.Frame2.Controls.Add(Me.Label3)
        Me.Frame2.Controls.Add(Me.Label2)
        Me.Frame2.Controls.Add(Me.Label1)
        Me.Frame2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame2.Location = New System.Drawing.Point(16, 160)
        Me.Frame2.Name = "Frame2"
        Me.Frame2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame2.Size = New System.Drawing.Size(253, 256)
        Me.Frame2.TabIndex = 2
        Me.Frame2.TabStop = False
        Me.Frame2.Text = "发送短信"
        '
        'Sms_Send_Button
        '
        Me.Sms_Send_Button.BackColor = System.Drawing.SystemColors.Control
        Me.Sms_Send_Button.Cursor = System.Windows.Forms.Cursors.Default
        Me.Sms_Send_Button.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Sms_Send_Button.Location = New System.Drawing.Point(64, 216)
        Me.Sms_Send_Button.Name = "Sms_Send_Button"
        Me.Sms_Send_Button.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Sms_Send_Button.Size = New System.Drawing.Size(91, 31)
        Me.Sms_Send_Button.TabIndex = 8
        Me.Sms_Send_Button.Text = "发  送"
        Me.Sms_Send_Button.UseVisualStyleBackColor = False
        '
        'TelNum_Text
        '
        Me.TelNum_Text.AcceptsReturn = True
        Me.TelNum_Text.BackColor = System.Drawing.SystemColors.Window
        Me.TelNum_Text.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TelNum_Text.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TelNum_Text.Location = New System.Drawing.Point(8, 32)
        Me.TelNum_Text.MaxLength = 0
        Me.TelNum_Text.Name = "TelNum_Text"
        Me.TelNum_Text.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TelNum_Text.Size = New System.Drawing.Size(235, 21)
        Me.TelNum_Text.TabIndex = 6
        '
        'SendSms_Text
        '
        Me.SendSms_Text.AcceptsReturn = True
        Me.SendSms_Text.BackColor = System.Drawing.SystemColors.Window
        Me.SendSms_Text.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.SendSms_Text.ForeColor = System.Drawing.SystemColors.WindowText
        Me.SendSms_Text.Location = New System.Drawing.Point(8, 80)
        Me.SendSms_Text.MaxLength = 0
        Me.SendSms_Text.Multiline = True
        Me.SendSms_Text.Name = "SendSms_Text"
        Me.SendSms_Text.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SendSms_Text.Size = New System.Drawing.Size(237, 95)
        Me.SendSms_Text.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(8, 184)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(241, 29)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "注：发送内容最多70个汉字或180个英文字母, 超长时自动分段发送。"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(8, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(65, 12)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "短信内容："
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(8, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(79, 15)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "手机号码："
        '
        'Frame1
        '
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.Sms_Connection_Button)
        Me.Frame1.Controls.Add(Me.Sms_Disconnection_Button)
        Me.Frame1.Controls.Add(Me.State_Show)
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Location = New System.Drawing.Point(16, 56)
        Me.Frame1.Name = "Frame1"
        Me.Frame1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Frame1.Size = New System.Drawing.Size(248, 96)
        Me.Frame1.TabIndex = 0
        Me.Frame1.TabStop = False
        Me.Frame1.Text = "连接GSM MODEM"
        '
        'Sms_Connection_Button
        '
        Me.Sms_Connection_Button.BackColor = System.Drawing.SystemColors.Control
        Me.Sms_Connection_Button.Cursor = System.Windows.Forms.Cursors.Default
        Me.Sms_Connection_Button.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Sms_Connection_Button.Location = New System.Drawing.Point(24, 56)
        Me.Sms_Connection_Button.Name = "Sms_Connection_Button"
        Me.Sms_Connection_Button.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Sms_Connection_Button.Size = New System.Drawing.Size(91, 31)
        Me.Sms_Connection_Button.TabIndex = 22
        Me.Sms_Connection_Button.Text = "连  接"
        Me.Sms_Connection_Button.UseVisualStyleBackColor = False
        '
        'Sms_Disconnection_Button
        '
        Me.Sms_Disconnection_Button.BackColor = System.Drawing.SystemColors.Control
        Me.Sms_Disconnection_Button.Cursor = System.Windows.Forms.Cursors.Default
        Me.Sms_Disconnection_Button.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Sms_Disconnection_Button.Location = New System.Drawing.Point(136, 56)
        Me.Sms_Disconnection_Button.Name = "Sms_Disconnection_Button"
        Me.Sms_Disconnection_Button.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Sms_Disconnection_Button.Size = New System.Drawing.Size(91, 31)
        Me.Sms_Disconnection_Button.TabIndex = 18
        Me.Sms_Disconnection_Button.Text = "断  开"
        Me.Sms_Disconnection_Button.UseVisualStyleBackColor = False
        '
        'State_Show
        '
        Me.State_Show.BackColor = System.Drawing.Color.Transparent
        Me.State_Show.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.State_Show.Cursor = System.Windows.Forms.Cursors.Default
        Me.State_Show.ForeColor = System.Drawing.SystemColors.ControlText
        Me.State_Show.Location = New System.Drawing.Point(8, 16)
        Me.State_Show.Name = "State_Show"
        Me.State_Show.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.State_Show.Size = New System.Drawing.Size(233, 29)
        Me.State_Show.TabIndex = 1
        Me.State_Show.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(16, 32)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(253, 15)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "注：0为红外接口，1,2,3,...为串口"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.SystemColors.Control
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(16, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(53, 12)
        Me.Label6.TabIndex = 24
        Me.Label6.Text = "端口号："
        '
        'mainsms
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(6, 14)
        Me.ClientSize = New System.Drawing.Size(618, 433)
        Me.Controls.Add(Me.Frame5)
        Me.Controls.Add(Me.Sms_Exit_Button)
        Me.Controls.Add(Me.Frame4)
        Me.Controls.Add(Me.Frame3)
        Me.Controls.Add(Me.Frame2)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.MobPort)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "mainsms"
        Me.Text = "调用短信收发二次开发接口例程源码(VBNET版)"
        Me.Frame5.ResumeLayout(False)
        Me.Frame4.ResumeLayout(False)
        Me.Frame4.PerformLayout()
        Me.Frame3.ResumeLayout(False)
        Me.Frame3.PerformLayout()
        Me.Frame2.ResumeLayout(False)
        Me.Frame2.PerformLayout()
        Me.Frame1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
#End Region
#Region "升级支持"
    Private Shared m_vb6FormDefInstance As mainsms
    Private Shared m_InitializingDefInstance As Boolean
    Public Shared Property DefInstance() As mainsms
        Get
            If m_vb6FormDefInstance Is Nothing OrElse m_vb6FormDefInstance.IsDisposed Then
                m_InitializingDefInstance = True
                m_vb6FormDefInstance = New mainsms()
                m_InitializingDefInstance = False
            End If
            DefInstance = m_vb6FormDefInstance
        End Get
        Set(ByVal value As mainsms)
            m_vb6FormDefInstance = value
        End Set
    End Property
#End Region

    Private Sub mainsms_Load(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles MyBase.Load
        Sms_Send_Button.Enabled = False
        Sms_Receive_Button.Enabled = False
        Sms_Delete_Button.Enabled = False
        SendSms_Text.Enabled = False
        TelNum_Text.Enabled = False
        ReceiveSms_Text.Enabled = False
        DeleteSms_Index.Enabled = False
        Sms_Disconnection_Button.Enabled = False
        Sms_Start_Button.Enabled = False
        NewSms_Show.Text = ""
        NewSms_Show.Enabled = False
    End Sub

    Private Sub Sms_Delete_Button_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Sms_Delete_Button.Click
        'UPGRADE_WARNING: Screen 属性 Screen.MousePointer 具有新的行为。 单击以获得更多信息：'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup2065"'
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Sms_Delete_Button.Enabled = False
        Sms_Delete(Trim(DeleteSms_Index.Text))
        Sms_Delete_Button.Enabled = True
        'UPGRADE_WARNING: Screen 属性 Screen.MousePointer 具有新的行为。 单击以获得更多信息：'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup2065"'
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Sms_Exit_Button_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Sms_Exit_Button.Click
        Me.Close()
    End Sub

    Private Sub Sms_Receive_Button_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Sms_Receive_Button.Click
        'UPGRADE_WARNING: Screen 属性 Screen.MousePointer 具有新的行为。 单击以获得更多信息：'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup2065"'
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Sms_Receive_Button.Enabled = False
        Dim ReceiveSmsStr As String
        If Sms_Receive("4", ReceiveSmsStr) Then
            ReceiveSms_Text.Text = ReceiveSmsStr
        End If
        Sms_Receive_Button.Enabled = True

        If Sms_AutoFlag() Then
            If Sms_Start_Button.Enabled = True Then
                NewSms_Show.Text = "自动接收短信功能处于关闭状态"
            Else
                NewSms_Show.Text = "未收到新短信"
            End If

        Else
            NewSms_Show.Text = "该短信猫不支持自动接收短信功能"
        End If
        'UPGRADE_WARNING: Screen 属性 Screen.MousePointer 具有新的行为。 单击以获得更多信息：'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup2065"'
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Sms_Disconnection_Button_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Sms_Disconnection_Button.Click
        'UPGRADE_WARNING: Screen 属性 Screen.MousePointer 具有新的行为。 单击以获得更多信息：'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup2065"'
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Sms_Disconnection_Button.Enabled = False
        Sms_Disconnection()
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
        State_Show.Text = ""
        NewSms_Show.Text = ""
        NewSms_Show.Enabled = False
        NewSms_Timer.Enabled = False
        'UPGRADE_WARNING: Screen 属性 Screen.MousePointer 具有新的行为。 单击以获得更多信息：'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup2065"'
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Sms_Connection_Button_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Sms_Connection_Button.Click
        'UPGRADE_ISSUE: 不支持 Load 语句。 单击以获得更多信息：'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1039"'
        'Load(Me)
        'UPGRADE_WARNING: Screen 属性 Screen.MousePointer 具有新的行为。 单击以获得更多信息：'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup2065"'
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Sms_Connection_Button.Enabled = False
        Dim TypeStr As String
        Dim CopyRightToCOMStr As String
        Dim CopyRightStr As String
        CopyRightStr = "//深圳市国爵电子有限公司,网址www.gprscat.com //"

        If Sms_Connection(CopyRightStr, CShort(MobPort.Text), 9600, TypeStr, CopyRightToCOMStr) Then '若使用诺基亚移动电话,请使用数据套件虚拟串口连接
            State_Show.Text = "连接短信猫成功" & Chr(10) & "(短信猫型号为：" & TypeStr & ")"
            Sms_Send_Button.Enabled = True
            Sms_Receive_Button.Enabled = True
            Sms_Delete_Button.Enabled = True
            SendSms_Text.Enabled = True
            TelNum_Text.Enabled = True
            ReceiveSms_Text.Enabled = True
            DeleteSms_Index.Enabled = True
            Sms_Disconnection_Button.Enabled = True
            Sms_Start_Button.Enabled = True
            NewSms_Show.Text = "自动接收短信功能处于关闭状态"
            NewSms_Show.Enabled = True
            NewSms_Timer.Enabled = False
        Else
            State_Show.Text = "连接短信猫失败" & Chr(10) & "(请重新连接短信猫)"
            Sms_Connection_Button.Enabled = True
        End If
        'UPGRADE_WARNING: Screen 属性 Screen.MousePointer 具有新的行为。 单击以获得更多信息：'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup2065"'
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default

    End Sub

    Private Sub Sms_Send_Button_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Sms_Send_Button.Click
        'UPGRADE_WARNING: Screen 属性 Screen.MousePointer 具有新的行为。 单击以获得更多信息：'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup2065"'
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Sms_Send_Button.Enabled = False
        If Len(Trim(TelNum_Text.Text)) >= 11 And Sms_Send(Trim(TelNum_Text.Text), Trim(SendSms_Text.Text)) Then
            MsgBox("发送短信成功！", MsgBoxStyle.Information, "提示")
        Else
            MsgBox("发送短信失败！", MsgBoxStyle.Critical, "警告")
        End If
        Sms_Send_Button.Enabled = True
        'UPGRADE_WARNING: Screen 属性 Screen.MousePointer 具有新的行为。 单击以获得更多信息：'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup2065"'
        System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub

    Private Sub Sms_Start_Button_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Sms_Start_Button.Click
        If Sms_AutoFlag() Then
            NewSms_Show.Text = "未收到新短信"
            Sms_Start_Button.Enabled = False
            Sms_Close_Button.Enabled = True
            NewSms_Timer.Enabled = True
        Else
            NewSms_Show.Text = "该短信猫不支持自动接收短信功能"
        End If
    End Sub

    Private Sub Sms_Close_Button_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles Sms_Close_Button.Click
        NewSms_Show.Text = "自动接收短信功能处于关闭状态"
        Sms_Start_Button.Enabled = True
        NewSms_Timer.Enabled = False
        Sms_Close_Button.Enabled = False
    End Sub

    Private Sub NewSms_Timer_Tick(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles NewSms_Timer.Tick
        If Sms_NewFlag() Then
            NewSms_Show.Text = "收到新短信,请查收!"
        End If
    End Sub
End Class