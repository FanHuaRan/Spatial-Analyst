// VoronoiDemoDoc.h : interface of the CVoronoiDemoDoc class
//
/////////////////////////////////////////////////////////////////////////////

#if !defined(AFX_VORONOIDEMODOC_H__6FE33373_4793_446B_B90E_0F3FB718D536__INCLUDED_)
#define AFX_VORONOIDEMODOC_H__6FE33373_4793_446B_B90E_0F3FB718D536__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000


class CVoronoiDemoDoc : public CDocument
{
protected: // create from serialization only
	CVoronoiDemoDoc();
	DECLARE_DYNCREATE(CVoronoiDemoDoc)

// Attributes
public:

// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CVoronoiDemoDoc)
	public:
	virtual BOOL OnNewDocument();
	virtual void Serialize(CArchive& ar);
	//}}AFX_VIRTUAL

// Implementation
public:
	virtual ~CVoronoiDemoDoc();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	//{{AFX_MSG(CVoronoiDemoDoc)
		// NOTE - the ClassWizard will add and remove member functions here.
		//    DO NOT EDIT what you see in these blocks of generated code !
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_VORONOIDEMODOC_H__6FE33373_4793_446B_B90E_0F3FB718D536__INCLUDED_)
