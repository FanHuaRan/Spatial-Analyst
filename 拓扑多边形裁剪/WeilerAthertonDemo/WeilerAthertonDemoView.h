// WeilerAthertonDemoView.h : interface of the CWeilerAthertonDemoView class
//
/////////////////////////////////////////////////////////////////////////////
#include "stdafx.h"

#if !defined(AFX_WEILERATHERTONDEMOVIEW_H__13DF7247_B96D_4D90_BE42_C3FED92EFA17__INCLUDED_)
#define AFX_WEILERATHERTONDEMOVIEW_H__13DF7247_B96D_4D90_BE42_C3FED92EFA17__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

//点结构体设计
struct WeilerPoint
{
	float x;
	float y;
	//0为普通顶点 1为入点 2为出点 3为交点待确定出入点标志
    int pointFlag;
	//当为交点时所在原裁剪多边形的点索引
	int clippedIndex;
	//当为交点时所在原裁剪窗口的点索引
	int clippingIndex;
	WeilerPoint(float x1,float y1)
	{
		x=x1;
		y=y1;
		pointFlag=0;
	}
	WeilerPoint(float x1,float y1,int _flag)
	{
		x=x1;
		y=y1;
		pointFlag=_flag;
	}
	WeilerPoint CopyPoint()
	{
		WeilerPoint point(x,y,pointFlag);
		return point;
	}
	WeilerPoint()
	{
	}
};
//多边形结构体设计
struct WeilerPolygon
{
	vector<WeilerPoint> points;
//	WeilerPolygon()
};
//裁剪结果多边形结构体设计
struct WeilerGenPolygon
{
   vector<WeilerPolygon> polygons;
};
class CWeilerAthertonDemoView : public CView
{
protected: // create from serialization only
	CWeilerAthertonDemoView();
	DECLARE_DYNCREATE(CWeilerAthertonDemoView)

// Attributes
public:
	CWeilerAthertonDemoDoc* GetDocument();
	//1是被裁减多边形
	//2是窗口

	 int flag;
	 bool m_MouseIsDown;
	 CPen myPen;
	 CPoint m_EndPoint;
	 CPoint m_StartPoint;
	 
	//被裁剪多边形
 	 WeilerPolygon clippedPolygon;
	//窗口
	 WeilerPolygon clippingPolygon;
// Operations
public:
   

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CWeilerAthertonDemoView)
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
	void CleanWindow();
	void DrawLine(CPoint p0, CPoint p1);
	virtual ~CWeilerAthertonDemoView();
#ifdef _DEBUG
	virtual void AssertValid() const;
	virtual void Dump(CDumpContext& dc) const;
#endif

protected:

// Generated message map functions
protected:
	void DrawXorLine(CPoint p0, CPoint p1, CPoint oldP);
	void DrawResult(WeilerGenPolygon polygons);
	void showVecter(vector<WeilerPoint> points);
	void showIndex(vector<int> indexs);
	//比较远近
	int CompareDistance(WeilerPoint startPoint,WeilerPoint endPoint1,WeilerPoint endPoint2);
	//寻找裁剪多边形的插入索引
	int CWeilerAthertonDemoView::GetInsertClippedIndex(vector<WeilerPoint> points,vector<WeilerPoint> insertPoints,int index);
	//寻找裁剪窗口的插入索引
    int CWeilerAthertonDemoView::GetInsertClippingIndex(vector<WeilerPoint> points,vector<WeilerPoint> insertPoints,int index);
	//核心算法
    WeilerGenPolygon WeilerAtherton(WeilerPolygon polygonCliped,WeilerPolygon polygonCliping);
	//求线段交点
	int GetIntersectionPoint(WeilerPoint pointx1,WeilerPoint pointy1,WeilerPoint pointx2,WeilerPoint pointy2,WeilerPoint &point);
	//求交点集
	vector<WeilerPoint> GetIntersectionPoint(WeilerPolygon polygonCliped,WeilerPolygon polygonCliping);
	//交点插入多边形顶点
	vector<WeilerPoint> GetNewListPointClipped(WeilerPolygon polygon,vector<WeilerPoint> &points);
	//交点插入裁剪窗口
	vector<WeilerPoint> GetNewListPointClipping(WeilerPolygon polygon,vector<WeilerPoint> points);
	//寻找入点 返回入点所在的索引
	int GetIntoPoint(vector<WeilerPoint> &points,WeilerPoint &point);
	//寻找出点 返回出点所在的索引
	int GetOutPoint(vector<WeilerPoint> &points,WeilerPoint &point);
	//删除入点标记
	void DeletePointIntoFlag(vector<WeilerPoint> &points,WeilerPoint point);
	//末尾插入顶点
	void InsertTail(vector<WeilerPoint> &points,WeilerPoint point);
	//{{AFX_MSG(CWeilerAthertonDemoView)
	afx_msg void OnClipp();
	afx_msg void OnPolygonClipped();
	afx_msg void OnPolygonClipping();
	afx_msg void OnRemove();
	afx_msg void OnMouseMove(UINT nFlags, CPoint point);
	afx_msg void OnLButtonDown(UINT nFlags, CPoint point);
	afx_msg void OnLButtonUp(UINT nFlags, CPoint point);
	afx_msg void OnRButtonDown(UINT nFlags, CPoint point);
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

#ifndef _DEBUG  // debug version in WeilerAthertonDemoView.cpp
inline CWeilerAthertonDemoDoc* CWeilerAthertonDemoView::GetDocument()
   { return (CWeilerAthertonDemoDoc*)m_pDocument; }
#endif

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_WEILERATHERTONDEMOVIEW_H__13DF7247_B96D_4D90_BE42_C3FED92EFA17__INCLUDED_)
