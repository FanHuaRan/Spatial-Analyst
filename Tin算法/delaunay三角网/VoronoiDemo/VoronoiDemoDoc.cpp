// VoronoiDemoDoc.cpp : implementation of the CVoronoiDemoDoc class
//

#include "stdafx.h"
#include "VoronoiDemo.h"

#include "VoronoiDemoDoc.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CVoronoiDemoDoc

IMPLEMENT_DYNCREATE(CVoronoiDemoDoc, CDocument)

BEGIN_MESSAGE_MAP(CVoronoiDemoDoc, CDocument)
	//{{AFX_MSG_MAP(CVoronoiDemoDoc)
		// NOTE - the ClassWizard will add and remove mapping macros here.
		//    DO NOT EDIT what you see in these blocks of generated code!
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CVoronoiDemoDoc construction/destruction

CVoronoiDemoDoc::CVoronoiDemoDoc()
{
	// TODO: add one-time construction code here

}

CVoronoiDemoDoc::~CVoronoiDemoDoc()
{
}

BOOL CVoronoiDemoDoc::OnNewDocument()
{
	if (!CDocument::OnNewDocument())
		return FALSE;

	// TODO: add reinitialization code here
	// (SDI documents will reuse this document)

	return TRUE;
}



/////////////////////////////////////////////////////////////////////////////
// CVoronoiDemoDoc serialization

void CVoronoiDemoDoc::Serialize(CArchive& ar)
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
// CVoronoiDemoDoc diagnostics

#ifdef _DEBUG
void CVoronoiDemoDoc::AssertValid() const
{
	CDocument::AssertValid();
}

void CVoronoiDemoDoc::Dump(CDumpContext& dc) const
{
	CDocument::Dump(dc);
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CVoronoiDemoDoc commands
