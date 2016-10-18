// VoronoiDemo.h : main header file for the VORONOIDEMO application
//

#if !defined(AFX_VORONOIDEMO_H__39869889_1DA3_4C9B_956B_88A290A67337__INCLUDED_)
#define AFX_VORONOIDEMO_H__39869889_1DA3_4C9B_956B_88A290A67337__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#ifndef __AFXWIN_H__
	#error include 'stdafx.h' before including this file for PCH
#endif

#include "resource.h"       // main symbols

/////////////////////////////////////////////////////////////////////////////
// CVoronoiDemoApp:
// See VoronoiDemo.cpp for the implementation of this class
//

class CVoronoiDemoApp : public CWinApp
{
public:
	CVoronoiDemoApp();

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CVoronoiDemoApp)
	public:
	virtual BOOL InitInstance();
	//}}AFX_VIRTUAL

// Implementation
	//{{AFX_MSG(CVoronoiDemoApp)
	afx_msg void OnAppAbout();
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};


/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_VORONOIDEMO_H__39869889_1DA3_4C9B_956B_88A290A67337__INCLUDED_)
