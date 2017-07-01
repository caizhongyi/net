object Form1: TForm1
  Left = 190
  Top = 113
  Width = 689
  Height = 527
  BorderIcons = [biSystemMenu, biMinimize, biMaximize, biHelp]
  Caption = #25511#20214#20351#29992#31034#20363#65288'DELPHI'#65289
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Label12: TLabel
    Left = 16
    Top = 147
    Width = 60
    Height = 13
    Caption = #25163#26426#21495#30721#65306
  end
  object Smsgate1: TSmsgate
    Left = 8
    Top = 445
    Width = 663
    Height = 20
    TabOrder = 0
    OnStatusChange = Smsgate1StatusChange
    OnRecvMsg = Smsgate1RecvMsg
    OnRevReport = Smsgate1RevReport
    OnCall = Smsgate1Call
    ControlData = {
      93B2000048000000030008000BF25747200000005F0065007800740065006E00
      7400780086440000030008000AF25747E0FFFFFF5F0065007800740065006E00
      7400790011020000}
  end
  object GroupBox1: TGroupBox
    Left = 8
    Top = 0
    Width = 513
    Height = 73
    Caption = #36830#25509#25163#26426
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = #23435#20307
    Font.Style = []
    ParentFont = False
    TabOrder = 1
    object Label1: TLabel
      Left = 8
      Top = 19
      Width = 67
      Height = 13
      Caption = #31471#21475'COM'#65306' '
      Font.Charset = ANSI_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = #23435#20307
      Font.Style = []
      ParentFont = False
    end
    object Label2: TLabel
      Left = 97
      Top = 19
      Width = 65
      Height = 13
      Caption = #30701#20449#20013#24515#65306
      Font.Charset = ANSI_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = #23435#20307
      Font.Style = []
      ParentFont = False
    end
    object Label3: TLabel
      Left = 263
      Top = 20
      Width = 39
      Height = 13
      Caption = #36895#29575#65306
      Font.Charset = ANSI_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = #23435#20307
      Font.Style = []
      ParentFont = False
    end
    object Label4: TLabel
      Left = 355
      Top = 20
      Width = 65
      Height = 13
      Caption = #25163#26426#20018#21495#65306
      Font.Charset = ANSI_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = #23435#20307
      Font.Style = []
      ParentFont = False
    end
    object ComboBox1: TComboBox
      Left = 61
      Top = 16
      Width = 33
      Height = 21
      Ctl3D = False
      ItemHeight = 13
      ParentCtl3D = False
      TabOrder = 0
      Text = '1'
      Items.Strings = (
        '1'
        '2'
        '3'
        '4'
        '5'
        '6'
        '7')
    end
    object Edit1: TEdit
      Left = 155
      Top = 16
      Width = 105
      Height = 21
      TabOrder = 1
      Text = '+8613800200500'
    end
    object ComboBox2: TComboBox
      Left = 295
      Top = 16
      Width = 57
      Height = 21
      ItemHeight = 13
      TabOrder = 2
      Text = '19200'
      Items.Strings = (
        '1200'
        '4800'
        '9600'
        '19200'
        '38400'
        '57600'
        '115200'
        '230400')
    end
    object Button1: TButton
      Left = 8
      Top = 40
      Width = 97
      Height = 25
      Caption = #36830#25509
      Font.Charset = ANSI_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = #23435#20307
      Font.Style = []
      ParentFont = False
      TabOrder = 3
      OnClick = Button1Click
    end
    object edit2: TEdit
      Left = 414
      Top = 16
      Width = 79
      Height = 21
      TabOrder = 4
    end
    object CheckBox2: TCheckBox
      Left = 112
      Top = 44
      Width = 97
      Height = 17
      Caption = #33258#21160#25509#25910#30701#20449
      TabOrder = 5
      OnClick = CheckBox2Click
    end
    object Button8: TButton
      Left = 317
      Top = 40
      Width = 80
      Height = 25
      Caption = #36830#25509#29366#24577
      TabOrder = 6
      OnClick = Button8Click
    end
    object Button9: TButton
      Left = 413
      Top = 40
      Width = 81
      Height = 25
      Caption = #20851#38381#31471#21475
      TabOrder = 7
      OnClick = Button9Click
    end
    object CheckBox1: TCheckBox
      Left = 214
      Top = 44
      Width = 97
      Height = 17
      Caption = #30701#20449#21457#36865#25253#21578
      TabOrder = 8
    end
  end
  object GroupBox2: TGroupBox
    Left = 8
    Top = 80
    Width = 513
    Height = 145
    Caption = #21457#36865#20449#24687
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = #23435#20307
    Font.Style = []
    ParentFont = False
    TabOrder = 2
    object Label6: TLabel
      Left = 16
      Top = 16
      Width = 59
      Height = 13
      Caption = #25163#26426#21495#30721':'
    end
    object Edit3: TEdit
      Left = 80
      Top = 11
      Width = 137
      Height = 21
      TabOrder = 0
      Text = '916091'
    end
    object Button2: TButton
      Left = 225
      Top = 9
      Width = 96
      Height = 25
      Caption = #21457#36865#26222#36890#20449#24687
      TabOrder = 1
      OnClick = Button2Click
    end
    object Button3: TButton
      Left = 343
      Top = 9
      Width = 105
      Height = 25
      Caption = #21457#36865#20108#36827#21046#25968#25454
      TabOrder = 2
      OnClick = Button3Click
    end
    object Memo1: TMemo
      Left = 16
      Top = 48
      Width = 481
      Height = 89
      Lines.Strings = (
        #21457#36865#20869#23481)
      TabOrder = 3
    end
  end
  object GroupBox3: TGroupBox
    Left = 8
    Top = 227
    Width = 513
    Height = 161
    Caption = #35835#21462#20449#24687
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = #23435#20307
    Font.Style = []
    ParentFont = False
    TabOrder = 3
    object Label5: TLabel
      Left = 8
      Top = 20
      Width = 39
      Height = 13
      Caption = #21442#25968#65306
    end
    object Label7: TLabel
      Left = 162
      Top = 22
      Width = 341
      Height = 13
      Caption = #65288'0-'#20165#25910#21462#26410#35835#20449#24687#65307'1-'#25910#21462#24050#35835#20449#24687#65307'4-'#25910#21462#25152#26377#20449#24687#65289
    end
    object ComboBox3: TComboBox
      Left = 45
      Top = 18
      Width = 33
      Height = 21
      ItemHeight = 13
      TabOrder = 0
      Text = '4'
      Items.Strings = (
        '1'
        '4'
        '0')
    end
    object Button4: TButton
      Left = 82
      Top = 16
      Width = 75
      Height = 25
      Caption = #25910#20449#24687
      TabOrder = 1
      OnClick = Button4Click
    end
    object Memo2: TMemo
      Left = 8
      Top = 48
      Width = 497
      Height = 105
      ScrollBars = ssVertical
      TabOrder = 2
    end
  end
  object GroupBox4: TGroupBox
    Left = 8
    Top = 392
    Width = 513
    Height = 45
    Caption = #25163#24037#21024#38500#20449#24687
    Font.Charset = ANSI_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = #23435#20307
    Font.Style = []
    ParentFont = False
    TabOrder = 4
    object Label8: TLabel
      Left = 166
      Top = 20
      Width = 275
      Height = 13
      Caption = #65288#21442#25968#20540#65306'1-'#21024#38500#24050#35835#20449#24687#65307'2-'#21024#38500#25152#26377#20449#24687#65289
    end
    object Label10: TLabel
      Left = 8
      Top = 20
      Width = 39
      Height = 13
      Caption = #21442#25968#65306
    end
    object ComboBox4: TComboBox
      Left = 46
      Top = 16
      Width = 33
      Height = 21
      ItemHeight = 13
      TabOrder = 0
      Text = '1'
      Items.Strings = (
        '1'
        '2')
    end
    object Button5: TButton
      Left = 86
      Top = 14
      Width = 75
      Height = 25
      Caption = #20876#38500#20449#24687
      TabOrder = 1
      OnClick = Button5Click
    end
  end
  object GroupBox6: TGroupBox
    Left = 528
    Top = 0
    Width = 145
    Height = 137
    Caption = #35821#38899#21151#33021
    TabOrder = 5
    object Label9: TLabel
      Left = 7
      Top = 20
      Width = 36
      Height = 13
      Caption = #21495#30721#65306
    end
    object Edit4: TEdit
      Left = 40
      Top = 16
      Width = 97
      Height = 21
      TabOrder = 0
    end
    object Button10: TButton
      Left = 8
      Top = 40
      Width = 129
      Height = 25
      Caption = #25171#30005#35805
      TabOrder = 1
      OnClick = Button10Click
    end
    object Button11: TButton
      Left = 8
      Top = 72
      Width = 129
      Height = 25
      Caption = #25346#30005#35805
      TabOrder = 2
      OnClick = Button11Click
    end
    object Button12: TButton
      Left = 8
      Top = 104
      Width = 129
      Height = 25
      Caption = #25509#30005#35805
      TabOrder = 3
      OnClick = Button12Click
    end
  end
  object GroupBox7: TGroupBox
    Left = 528
    Top = 144
    Width = 145
    Height = 257
    Caption = #20854#23427#65288#37096#20998#25163#26426#19981#25903#25345#65289
    TabOrder = 6
    object Button13: TButton
      Left = 8
      Top = 24
      Width = 129
      Height = 25
      Caption = #35835#25163#26426#30005#35805#28325
      TabOrder = 0
      OnClick = Button13Click
    end
    object Button14: TButton
      Left = 8
      Top = 56
      Width = 129
      Height = 25
      Caption = #35835'SIM'#21345#30005#35805#28325
      TabOrder = 1
      OnClick = Button14Click
    end
    object Button15: TButton
      Left = 8
      Top = 88
      Width = 129
      Height = 25
      Caption = #24050#25509#30005#35805#35760#24405
      TabOrder = 2
      OnClick = Button15Click
    end
    object Button16: TButton
      Left = 8
      Top = 120
      Width = 129
      Height = 25
      Caption = #26410#25509#30005#35805#35760#24405
      TabOrder = 3
      OnClick = Button16Click
    end
    object Button17: TButton
      Left = 8
      Top = 152
      Width = 129
      Height = 25
      Caption = #26381#21153#20013#24515#21495#30721
      TabOrder = 4
      OnClick = Button17Click
    end
    object Button18: TButton
      Left = 8
      Top = 184
      Width = 129
      Height = 25
      Caption = #25163#26426#21378#23478#21517#31216
      TabOrder = 5
      OnClick = Button18Click
    end
    object Button19: TButton
      Left = 8
      Top = 216
      Width = 129
      Height = 25
      Caption = #25163#26426#22411#21495
      TabOrder = 6
      OnClick = Button19Click
    end
  end
  object StatusBar1: TStatusBar
    Left = 0
    Top = 481
    Width = 681
    Height = 19
    DragKind = dkDock
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -11
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    Panels = <
      item
        Text = '    '#24403#21069#31471#21475#29366#24577#65306#31354#38386
        Width = 160
      end
      item
        Text = '    '#26242#26080#28040#24687
        Width = 50
      end>
    ParentShowHint = False
    ShowHint = False
    UseSystemFont = False
  end
end
