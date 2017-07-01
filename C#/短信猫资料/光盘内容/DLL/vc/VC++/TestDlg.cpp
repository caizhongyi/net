// TestDlg.cpp : implementation file
//

#include "stdafx.h"
#include "Test.h"
#include "TestDlg.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;

#endif

/////////////////////////////////////////////////////////////////////////////
// CTestDlg dialog

CTestDlg::CTestDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CTestDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CTestDlg)
	m_intPort = 0;
	m_strSMSCon = _T("");
	m_strSMSTel = _T("");
	m_strIndex = _T("");
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);

}

void CTestDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CTestDlg)
	DDX_Text(pDX, IDC_Port, m_intPort);
	DDX_Text(pDX, IDC_SMSCon, m_strSMSCon);
	DDX_Text(pDX, IDC_SMSTel, m_strSMSTel);
	DDX_Text(pDX, IDC_Index, m_strIndex);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CTestDlg, CDialog)
	//{{AFX_MSG_MAP(CTestDlg)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_Connection_Button, OnConnectionButton)
	ON_BN_CLICKED(IDC_Disconnection_Button, OnDisconnectionButton)
	ON_BN_CLICKED(IDC_Send_Button, OnSendButton)
	ON_BN_CLICKED(IDC_Receive_Button, OnReceiveButton)
	ON_BN_CLICKED(IDC_Delete_Button, OnDeleteButton)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CTestDlg message handlers

BOOL CTestDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	GetDlgItem(IDC_Port)->SetWindowText("1");
	
	hinstDLL=NULL; 
	hinstDLL=LoadLibrary("sms.dll");
	if (hinstDLL==NULL)
	{
		AfxMessageBox("没找到sms.dll文件!");
	}
	// TODO: Add extra initialization here
	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CTestDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CTestDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}



void CTestDlg::OnConnectionButton() 
{
    UpdateData();
	CString strText;
	CString strtmp;
	CString tmp[256];
	strtmp="//深圳市国爵电子有限公司,网址www.gprscat.com //";
	typedef int(_stdcall *Sms_Connection)(CString CopyRight,int Com_Port,int Com_BaudRate,CString *Mobile_Type);
	Sms_Connection Proc;
	Proc = (Sms_Connection)GetProcAddress(hinstDLL,"Sms_Connection");
	int iValue = Proc(strtmp,m_intPort,19200,tmp);
    if (iValue == 1)
	{
	    strText.Format("短信猫连接成功!(短信猫型号为:%s)",*tmp);
        AfxMessageBox(strText);
	} else if (iValue == 0) {
        AfxMessageBox("短信猫连接失败!(请重新连接短信猫)");
	}
}

void CTestDlg::OnDisconnectionButton() 
{
	UpdateData();
	typedef int(_stdcall *Sms_Disconnection)();
    Sms_Disconnection Proc;
	Proc = (Sms_Disconnection)GetProcAddress(hinstDLL,"Sms_Disconnection");
	int iValue = Proc();
}

void CTestDlg::OnSendButton() 
{
    UpdateData();
	CString strText;
	typedef int(_stdcall *Sms_Send)(CString Sms_TelNum,CString Sms_Text);
	Sms_Send Proc;
	Proc = (Sms_Send)GetProcAddress(hinstDLL,"Sms_Send");
	int iValue = Proc(m_strSMSTel,m_strSMSCon);
    if (iValue == 1)
	{
        AfxMessageBox("发送成功!");
	} else{
        AfxMessageBox("发送失败!");
	}
}

void CTestDlg::OnReceiveButton() 
{
    UpdateData();
	CString strText;
	CString tmp[1024];
	typedef int(_stdcall *Sms_Receive)(CString Sms_Type,CString *Sms_Text);
	Sms_Receive Proc;
	Proc = (Sms_Receive)GetProcAddress(hinstDLL,"Sms_Receive");
	int iValue = Proc("4",tmp);	
	strText.Format("%s",*tmp);
    AfxMessageBox(strText);
}

void CTestDlg::OnDeleteButton() 
{
    UpdateData();
	CString strText;
	CString tmp[256];
	typedef int(_stdcall *Sms_Delete)(CString Sms_Index);
	Sms_Delete Proc;
	Proc = (Sms_Delete)GetProcAddress(hinstDLL,"Sms_Delete");
	int iValue = Proc(m_strIndex);	
	if (iValue == 1)
	{
        AfxMessageBox("删除成功!");
	} else{
        AfxMessageBox("删除失败!");
	}
	
}

void CTestDlg::OnOK() 
{
	// TODO: Add extra validation here
	FreeLibrary(hinstDLL);
	CDialog::OnOK();
}
