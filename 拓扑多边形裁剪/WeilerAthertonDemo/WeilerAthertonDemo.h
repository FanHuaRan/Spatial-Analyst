// WeilerAthertonDemo.h : main header file for the WEILERATHERTONDEMO application
//

#if !defined(AFX_WEILERATHERTONDEMO_H__76200B46_4D35_492C_991D_6C2FC5861C35__INCLUDED_)
#define AFX_WEILERATHERTONDEMO_H__76200B46_4D35_492C_991D_6C2FC5861C35__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"       // main symbols

/////////////////////////////////////////////////////////////////////////////
// CWeilerAthertonDemoApp:
// See WeilerAthertonDemo.cpp for the implementation of this class
//

class CWeilerAthertonDemoApp : public CWinApp
{
public:
	CWeilerAthertonDemoApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CWeilerAthertonDemoApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation
	//{{AFX_MSG(CWeilerAthertonDemoApp)
	afx_msg void OnAppAbout();
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_WEILERATHERTONDEMO_H__76200B46_4D35_492C_991D_6C2FC5861C35__INCLUDED_)
