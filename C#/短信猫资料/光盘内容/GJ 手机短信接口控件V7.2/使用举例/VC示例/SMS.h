// SMS.h : main header file for the SMS application
//

#if !defined(AFX_SMS_H__F2A3C50F_3DA0_4A1E_A1E8_CEC945C42135__INCLUDED_)
#define AFX_SMS_H__F2A3C50F_3DA0_4A1E_A1E8_CEC945C42135__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"		// main symbols

/////////////////////////////////////////////////////////////////////////////
// CSMSApp:
// See SMS.cpp for the implementation of this class
//

class CSMSApp : public CWinApp
{
public:
	CSMSApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSMSApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation

	//{{AFX_MSG(CSMSApp)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SMS_H__F2A3C50F_3DA0_4A1E_A1E8_CEC945C42135__INCLUDED_)
