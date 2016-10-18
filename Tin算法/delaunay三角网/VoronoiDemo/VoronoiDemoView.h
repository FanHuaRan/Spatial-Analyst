// VoronoiDemoView.h : interface of the CVoronoiDemoView class
//
/////////////////////////////////////////////////////////////////////////////
#include "stdafx.h"
#if !defined(AFX_VORONOIDEMOVIEW_H__B0FC1FEA_2F5F_4D3C_939D_23FD84B92E89__INCLUDED_)
#define AFX_VORONOIDEMOVIEW_H__B0FC1FEA_2F5F_4D3C_939D_23FD84B92E89__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
struct Line
{
	CPoint StartPoint;
	CPoint EndPoint;
};
struct Triangle
{
	CPoint points[3];
	CPoint heart;

};
struct Area
{
	Line *lines;
};
class CVoronoiDemoView : public CView
{
protected: // create from serialization only
	CVoronoiDemoView();
	DECLARE_DYNCREATE(CVoronoiDemoView)

// Attributes
public:
	CVoronoiDemoDoc* GetDocument();
	CPen triPen;
	CPen pen;
	//1为画区域，2为画离散点
	int shapeFlag;
	CPoint oldPoint;
	CPoint startPoint;
	//边界
	Area area;
	vector<CPoint> HeartPoints;
	//当前寻找到的三角形
	Triangle currentTriangle;
	//线集合
	vector<Line> lines;
	//点集合
	vector<CPoint> points;
	//三角形集合
	vector<Triangle> triangles;
	BOOL IS_Drawing;
// Operations
public:
    
// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CVoronoiDemoView)
	public:
	virtual void OnDraw(CDC* pDC);  // overridden to draw this view
	virtual BOOL PreCreateWindow(CREATESTRUCT& cs);
	protected:
	virtual BOOL OnPreparePrinting(CPrintInfo* pInfo);
	virtual void OnBeginPrinting(CDC* pDC, CPrintInfo* pInfo);
	virtual void OnEndPrinting(CDC* pDC, CPrintInfo* pInfo);
	//}}AFX_VIRTUAL

// Implementation
public:
	CPoint GetTriangleThirdPoint(Triangle Tri, CPoint p0, CPoint p1);
	BOOL IsEqual(Triangle Tri1,Triangle Tri2);
	void LookForNextTriHeart(CPoint LineStartPoint,CPoint LineEndPoint,CPoint p0,int flag);
	Triangle CVoronoiDemoView::LookForFirstTriangle(CPoint point,int *location);
	void BuildTai(void);
	CPoint GetPoint(Triangle Tri);
	void BuildDelaunry(void);
	void DrawPoint(CPoint p0);
	void DrawXorRect(CPoint p0,CPoint p1,CPoint p2);
	void Delaunay(CPoint p1,CPoint p2);
	bool IsUseLine(CPoint p0,CPoint p1);
	CPoint GetThridPoint(CPoint p1,CPoint p2);
	void DrawTriangle(CPoint p0,CPoint p1,CPoint p2);
	void ClearWindow(void);
	virtual ~CVoronoiDemoView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	//{{AFX_MSG(CVoronoiDemoView)
	afx_msg void OnArea();
	afx_msg void OnBuild();
	afx_msg void OnClear();
	afx_msg void OnDrawPoint();
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);
	afx_msg void OnMouseMove(UINT nFlags, CPoint point);
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

#ifndef _DEBUG  // debug version in VoronoiDemoView.cpp
inline CVoronoiDemoDoc* CVoronoiDemoView::GetDocument()
   { return (CVoronoiDemoDoc*)m_pDocument; }
#endif

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_VORONOIDEMOVIEW_H__B0FC1FEA_2F5F_4D3C_939D_23FD84B92E89__INCLUDED_)
