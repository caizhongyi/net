// SMSDlg.cpp : implementation file
//

#include "stdafx.h"
#include "SMS.h"
#include "SMSDlg.h"
#include   <stdlib.h>
#include <atlconv.h> 
#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif
//#include <comutil.h>
//#pragma comment(lib, <comsuppw.lib>)

/////////////////////////////////////////////////////////////////////////////
// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAboutDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	//{{AFX_MSG(CAboutDlg)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	//}}AFX_DATA_INIT
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
		// No message handlers
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CSMSDlg dialog

CSMSDlg::CSMSDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CSMSDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CSMSDlg)
	m_nPort = 0;
	m_csSmsServer = _T("+8613010314500");
//	m_csSmsServer = _T("+8613800210500");
	m_nSpeed = 9600;
	m_csMobileNumber = _T("");
	m_csSmsBody = _T("");
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_bDelAllFlag=false;
	m_bAutoRecvFlag=true;
}

void CSMSDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CSMSDlg)
	DDX_Control(pDX, IDC_COMBO_RECV_INDEX, m_ctlComboBoxRecv);
	DDX_Control(pDX, IDC_SMSGATE1, m_ctrlSmsSend);
	DDX_CBIndex(pDX, IDC_COMBO_PORT, m_nPort);
	DDX_Text(pDX, IDC_EDIT_SMS_SERVER, m_csSmsServer);
	DDX_Text(pDX, IDC_EDIT_SPEED, m_nSpeed);
	DDX_Text(pDX, IDC_EDIT_MOBILE_NUMBER, m_csMobileNumber);
	DDX_Text(pDX, IDC_EDIT_SMS_BODY, m_csSmsBody);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CSMSDlg, CDialog)
	//{{AFX_MSG_MAP(CSMSDlg)
	ON_WM_SYSCOMMAND()
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDC_BUTTON_INIT, OnButtonInit)
	ON_BN_CLICKED(IDC_BUTTON_SEND_NORMAL, OnButtonSendNormal)
	ON_BN_CLICKED(IDC_BUTTON_RECV, OnButtonRecv)
	ON_BN_CLICKED(IDC_BUTTON_DEL_SMS, OnButtonDelSms)
	ON_BN_CLICKED(IDC_RADIO_DEL_READ, OnRadioDelRead)
	ON_BN_CLICKED(IDC_RADIO_DEL_ALL, OnRadioDelAll)
	ON_BN_CLICKED(IDC_RADIO_AUTO_RECV_ON, OnRadioAutoRecvOn)
	ON_BN_CLICKED(IDC_RADIO_AUTO_RECV_OFF, OnRadioAutoRecvOff)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CSMSDlg message handlers

BOOL CSMSDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Add "About..." menu item to system menu.

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);

	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL)
	{
		CString strAboutMenu;
		strAboutMenu.LoadString(IDS_ABOUTBOX);
		if (!strAboutMenu.IsEmpty())
		{
			pSysMenu->AppendMenu(MF_SEPARATOR);
			pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
		}
	}

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// TODO: Add extra initialization here
	CheckDlgButton  (IDC_RADIO_DEL_READ,!m_bDelAllFlag);
	CheckDlgButton  (IDC_RADIO_DEL_ALL,m_bDelAllFlag);
	CheckDlgButton  (IDC_RADIO_AUTO_RECV_ON,m_bAutoRecvFlag);
	CheckDlgButton  (IDC_RADIO_AUTO_RECV_OFF,!m_bAutoRecvFlag);

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CSMSDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
	if ((nID & 0xFFF0) == IDM_ABOUTBOX)
	{
		CAboutDlg dlgAbout;
		dlgAbout.DoModal();
	}
	else
	{
		CDialog::OnSysCommand(nID, lParam);
	}
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CSMSDlg::OnPaint() 
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
HCURSOR CSMSDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

void CSMSDlg::OnOK() 
{
	// TODO: Add extra validation here
//	m_ctrlSmsSend.CommPort=1;
//	m_ctrlSmsSend.GetSmsService();
	m_ctrlSmsSend.GetRevInterval();
	m_ctrlSmsSend.GetPhoneNo();
//	short pTime[MAX_PATH],pReport[MAX_PATH],nTime=2;
//	VARIANT csResult=m_ctrlSmsSend.Connect(&nTime);
//	m_ctrlSmsSend.Sendsms((BSTR* )("测试内容"), (BSTR*)("13632263389"), pReport);
//	CDialog::OnOK();
}

void CSMSDlg::OnButtonInit() 
{
	// TODO: Add your control notification handler code here
	UpdateData();
	char *pSetting=new char[MAX_PATH];
	sprintf(pSetting,"%d,n,8,1",m_nSpeed);
	m_ctrlSmsSend.SetCommPort(m_nPort+1);
	m_ctrlSmsSend.SetSmsService(m_csSmsServer);
//	m_ctrlSmsSend.GetSmsService();
	m_ctrlSmsSend.SetSettings(pSetting);

	short nTime=2;
	VARIANT varResult=m_ctrlSmsSend.Connect(&nTime);
//	varResult=m_ctrlSmsSend.GetPhoneNo();
//	CString csResult;
	CString csResult =(BSTR)varResult.pbstrVal;
//	char *pTemp=_com_util::ConvertBSTRToString(NULL);
//	csResult.Format ("%S",(varResult.pbstrVal ));
	if(csResult=="y")
		csResult="初始化正确！";
	MessageBox(csResult);
	if(m_bAutoRecvFlag)
		varResult=m_ctrlSmsSend.RevAuto();
	else
		varResult=m_ctrlSmsSend.RevAutoClose();
	csResult =(BSTR)varResult.pbstrVal;
	if(csResult=="y" && m_bAutoRecvFlag)
		csResult="自动接收 打开！";
	else
		csResult="自动接收 关闭！";
	MessageBox(csResult);
	
}

void CSMSDlg::OnButtonSendNormal() 
{
	// TODO: Add your control notification handler code here
	UpdateData();
	short nReport=1;
//	GetButtonCheck();
	USES_CONVERSION;
//	BSTR *pBstr;
//	BSTR *ppMobile=new BSTR,pMobile,pMobile1;
	BSTR *ppMobile,pMobile,*ppBody,pBody;
//	pMobile=(A2W(m_csMobileNumber));
	pMobile=m_csMobileNumber.AllocSysString ();
	ppMobile=& pMobile;
	pBody=m_csSmsBody.AllocSysString ();
	ppBody=& pBody;
//	pMobile=(BSTR )m_csMobileNumber.GetBuffer (m_csMobileNumber.GetLength ());
//	CComBSTR bstr("");
//	CString csResult=m_ctrlSmsSend.Sendsms((A2W(m_csMobileNumber)), A2W(m_csMobileNumber), &nReport);
	CString csResult=m_ctrlSmsSend.Sendsms(ppBody, ppMobile, &nReport);
	SysFreeString(pMobile);
	SysFreeString(pBody);
//	CString csResult=m_ctrlSmsSend.Sendsms(&bstr, &bstr, &nReport);
	
}

void CSMSDlg::OnButtonRecv() 
{
	// TODO: Add your control notification handler code here
	short nIndex=m_ctlComboBoxRecv.GetCurSel();
	VARIANT varResult=m_ctrlSmsSend.ReadMsg(&nIndex);
	CString csResult =(BSTR)varResult.pbstrVal;
	if(csResult.IsEmpty ())
		MessageBox("无信息！");
	else
		MessageBox(csResult);

}

void CSMSDlg::OnButtonDelSms() 
{
	// TODO: Add your control notification handler code here
	short nDelFormat=m_bDelAllFlag+1;
	VARIANT varResult=m_ctrlSmsSend.DelSms(&nDelFormat);
	CString csResult =(BSTR)varResult.pbstrVal;
	if(csResult=="y")
		MessageBox("删除成功");
	else
		MessageBox("删除失败");
	
}

void CSMSDlg::OnRadioDelRead() 
{
	// TODO: Add your control notification handler code here
	m_bDelAllFlag=false;
	CheckDlgButton  (IDC_RADIO_DEL_READ,!m_bDelAllFlag);
	CheckDlgButton  (IDC_RADIO_DEL_ALL,m_bDelAllFlag);
}

void CSMSDlg::OnRadioDelAll() 
{
	// TODO: Add your control notification handler code here
	m_bDelAllFlag=true;
	CheckDlgButton  (IDC_RADIO_DEL_READ,!m_bDelAllFlag);
	CheckDlgButton  (IDC_RADIO_DEL_ALL,m_bDelAllFlag);
	
}

void CSMSDlg::OnRadioAutoRecvOn() 
{
	// TODO: Add your control notification handler code here
	m_bAutoRecvFlag=true;
	m_ctrlSmsSend.RevAuto();
	CheckDlgButton  (IDC_RADIO_AUTO_RECV_ON,m_bAutoRecvFlag);
	CheckDlgButton  (IDC_RADIO_AUTO_RECV_OFF,!m_bAutoRecvFlag);
	
}

void CSMSDlg::OnRadioAutoRecvOff() 
{
	// TODO: Add your control notification handler code here
	m_bAutoRecvFlag=false;
	m_ctrlSmsSend.RevAutoClose();
	CheckDlgButton  (IDC_RADIO_AUTO_RECV_ON,m_bAutoRecvFlag);
	CheckDlgButton  (IDC_RADIO_AUTO_RECV_OFF,!m_bAutoRecvFlag);
	
}

BEGIN_EVENTSINK_MAP(CSMSDlg, CDialog)
    //{{AFX_EVENTSINK_MAP(CSMSDlg)
	ON_EVENT(CSMSDlg, IDC_SMSGATE1, 2 /* OnRecvMsg */, OnOnRecvMsgSmsgate1, VTS_NONE)
	ON_EVENT(CSMSDlg, IDC_SMSGATE1, 3 /* OnRevReport */, OnOnRevReportSmsgate1, VTS_NONE)
	//}}AFX_EVENTSINK_MAP
END_EVENTSINK_MAP()

void CSMSDlg::OnOnRecvMsgSmsgate1() 
{
	// TODO: Add your control notification handler code here
	VARIANT varResult=m_ctrlSmsSend.NewMsg();
	CString csResult =(BSTR)varResult.pbstrVal;
	MessageBox(csResult);
	
}

void CSMSDlg::OnOnRevReportSmsgate1() 
{
	// TODO: Add your control notification handler code here
	VARIANT varResult=m_ctrlSmsSend.NewReport();
	CString csResult =(BSTR)varResult.pbstrVal;
	MessageBox(csResult);
	
}
