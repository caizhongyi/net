// SMSDlg.h : header file
//
//{{AFX_INCLUDES()
#include "_smsgate.h"
//}}AFX_INCLUDES

#if !defined(AFX_SMSDLG_H__262CBBCF_8912_4347_9DB9_3FAD69C8914F__INCLUDED_)
#define AFX_SMSDLG_H__262CBBCF_8912_4347_9DB9_3FAD69C8914F__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

/////////////////////////////////////////////////////////////////////////////
// CSMSDlg dialog

class CSMSDlg : public CDialog
{
// Construction
public:
	CSMSDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	//{{AFX_DATA(CSMSDlg)
	enum { IDD = IDD_SMS_DIALOG };
	CComboBox	m_ctlComboBoxRecv;
	C_Smsgate	m_ctrlSmsSend;
	int		m_nPort;
	CString	m_csSmsServer;
	UINT	m_nSpeed;
	CString	m_csMobileNumber;
	CString	m_csSmsBody;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSMSDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CSMSDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	virtual void OnOK();
	afx_msg void OnButtonInit();
	afx_msg void OnButtonSendNormal();
	afx_msg void OnButtonRecv();
	afx_msg void OnButtonDelSms();
	afx_msg void OnRadioDelRead();
	afx_msg void OnRadioDelAll();
	afx_msg void OnRadioAutoRecvOn();
	afx_msg void OnRadioAutoRecvOff();
	afx_msg void OnOnRecvMsgSmsgate1();
	afx_msg void OnOnRevReportSmsgate1();
	DECLARE_EVENTSINK_MAP()
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
private:
	bool m_bDelAllFlag,m_bAutoRecvFlag;
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SMSDLG_H__262CBBCF_8912_4347_9DB9_3FAD69C8914F__INCLUDED_)
