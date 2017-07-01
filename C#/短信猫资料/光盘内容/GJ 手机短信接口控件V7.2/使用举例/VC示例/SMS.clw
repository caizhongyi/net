; CLW file contains information for the MFC ClassWizard

[General Info]
Version=1
LastClass=CSMSDlg
LastTemplate=CDialog
NewFileInclude1=#include "stdafx.h"
NewFileInclude2=#include "SMS.h"

ClassCount=3
Class1=CSMSApp
Class2=CSMSDlg
Class3=CAboutDlg

ResourceCount=3
Resource1=IDD_ABOUTBOX
Resource2=IDR_MAINFRAME
Resource3=IDD_SMS_DIALOG

[CLS:CSMSApp]
Type=0
HeaderFile=SMS.h
ImplementationFile=SMS.cpp
Filter=N

[CLS:CSMSDlg]
Type=0
HeaderFile=SMSDlg.h
ImplementationFile=SMSDlg.cpp
Filter=D
BaseClass=CDialog
VirtualFilter=dWC
LastObject=IDC_SMSGATE1

[CLS:CAboutDlg]
Type=0
HeaderFile=SMSDlg.h
ImplementationFile=SMSDlg.cpp
Filter=D

[DLG:IDD_ABOUTBOX]
Type=1
Class=CAboutDlg
ControlCount=4
Control1=IDC_STATIC,static,1342177283
Control2=IDC_STATIC,static,1342308480
Control3=IDC_STATIC,static,1342308352
Control4=IDOK,button,1342373889

[DLG:IDD_SMS_DIALOG]
Type=1
Class=CSMSDlg
ControlCount=30
Control1=IDOK,button,1342242817
Control2=IDCANCEL,button,1073807360
Control3=IDC_SMSGATE1,{7DADDF3A-0EEC-434E-B2FB-9C355E179D3A},1342242816
Control4=IDC_STATIC,button,1342177287
Control5=IDC_STATIC,static,1342308352
Control6=IDC_COMBO_PORT,combobox,1344339971
Control7=IDC_STATIC,static,1342308352
Control8=IDC_EDIT_SMS_SERVER,edit,1350631552
Control9=IDC_STATIC,static,1342308352
Control10=IDC_EDIT_SPEED,edit,1350631552
Control11=IDC_BUTTON_INIT,button,1342242816
Control12=IDC_STATIC,button,1342177287
Control13=IDC_STATIC,static,1342308352
Control14=IDC_EDIT_MOBILE_NUMBER,edit,1350631552
Control15=IDC_BUTTON_SEND_NORMAL,button,1342242816
Control16=IDC_BUTTON_SEND_BINARY,button,1342242816
Control17=IDC_STATIC,static,1342308352
Control18=IDC_EDIT_SMS_BODY,edit,1352728580
Control19=IDC_CHECK_REPORT,button,1342242819
Control20=IDC_STATIC,button,1342177287
Control21=IDC_STATIC,static,1342308352
Control22=IDC_COMBO_RECV_INDEX,combobox,1344339971
Control23=IDC_BUTTON_RECV,button,1342242816
Control24=IDC_STATIC,button,1342177287
Control25=IDC_RADIO_DEL_READ,button,1342177284
Control26=IDC_RADIO_DEL_ALL,button,1342177284
Control27=IDC_BUTTON_DEL_SMS,button,1342242816
Control28=IDC_STATIC,button,1342177287
Control29=IDC_RADIO_AUTO_RECV_ON,button,1342177284
Control30=IDC_RADIO_AUTO_RECV_OFF,button,1342177284

