// TestDlg.h : header file
//

#if !defined(AFX_TESTDLG_H__376CE67F_7482_425B_8FFF_2AAE334C2098__INCLUDED_)
#define AFX_TESTDLG_H__376CE67F_7482_425B_8FFF_2AAE334C2098__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CTestDlg dialog

class CTestDlg : public CDialog
{
// Construction
public:
	CTestDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CTestDlg)
	enum { IDD = IDD_TEST_DIALOG };
	int		m_intPort;
	CString	m_strSMSCon;
	CString	m_strSMSTel;
	CString	m_strIndex;
    HINSTANCE hinstDLL; 
	
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CTestDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CTestDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnBtest();
	afx_msg void OnButton1();
	afx_msg void OnButton2();
	afx_msg void OnConnectionButton();
	afx_msg void OnDisconnectionButton();
	afx_msg void OnSendButton();
	afx_msg void OnReceiveButton();
	afx_msg void OnDeleteButton();
	virtual void OnOK();
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_TESTDLG_H__376CE67F_7482_425B_8FFF_2AAE334C2098__INCLUDED_)
