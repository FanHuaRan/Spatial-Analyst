// WeilerAthertonDemoDoc.cpp : implementation of the CWeilerAthertonDemoDoc class
//

#include "stdafx.h"
#include "WeilerAthertonDemo.h"

#include "WeilerAthertonDemoDoc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CWeilerAthertonDemoDoc

IMPLEMENT_DYNCREATE(CWeilerAthertonDemoDoc, CDocument)

BEGIN_MESSAGE_MAP(CWeilerAthertonDemoDoc, CDocument)
	//{{AFX_MSG_MAP(CWeilerAthertonDemoDoc)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CWeilerAthertonDemoDoc construction/destruction

CWeilerAthertonDemoDoc::CWeilerAthertonDemoDoc()
{
	// TODO: add one-time construction code here

}

CWeilerAthertonDemoDoc::~CWeilerAthertonDemoDoc()
{
}

BOOL CWeilerAthertonDemoDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: add reinitialization code here
	// (SDI documents will reuse this document)

	return TRUE;
}



/////////////////////////////////////////////////////////////////////////////
// CWeilerAthertonDemoDoc serialization

void CWeilerAthertonDemoDoc::Serialize(CArchive& ar)
{
	if (ar.IsStoring())
	{
		// TODO: add storing code here
	}
	else
	{
		// TODO: add loading code here
	}
}

/////////////////////////////////////////////////////////////////////////////
// CWeilerAthertonDemoDoc diagnostics

#ifdef _DEBUG
void CWeilerAthertonDemoDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CWeilerAthertonDemoDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CWeilerAthertonDemoDoc commands
