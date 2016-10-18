// WeilerAthertonDemoDoc.h : interface of the CWeilerAthertonDemoDoc class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_WEILERATHERTONDEMODOC_H__4405FE49_D9DC_4692_B730_2A683346FC74__INCLUDED_)
#define AFX_WEILERATHERTONDEMODOC_H__4405FE49_D9DC_4692_B730_2A683346FC74__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


class CWeilerAthertonDemoDoc : public CDocument
{
protected: // create from serialization only
	CWeilerAthertonDemoDoc();
	DECLARE_DYNCREATE(CWeilerAthertonDemoDoc)

// Attributes
public:

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CWeilerAthertonDemoDoc)
	public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);
	//}}AFX_VIRTUAL

// Implementation
public:
	virtual ~CWeilerAthertonDemoDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	//{{AFX_MSG(CWeilerAthertonDemoDoc)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_WEILERATHERTONDEMODOC_H__4405FE49_D9DC_4692_B730_2A683346FC74__INCLUDED_)
