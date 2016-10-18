// WeilerAthertonDemoView.h : interface of the CWeilerAthertonDemoView class
//
/////////////////////////////////////////////////////////////////////////////
#include "stdafx.h"

#if !defined(AFX_WEILERATHERTONDEMOVIEW_H__13DF7247_B96D_4D90_BE42_C3FED92EFA17__INCLUDED_)
#define AFX_WEILERATHERTONDEMOVIEW_H__13DF7247_B96D_4D90_BE42_C3FED92EFA17__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

//��ṹ�����
struct WeilerPoint
{
	float x;
	float y;
	//0Ϊ��ͨ���� 1Ϊ��� 2Ϊ���� 3Ϊ�����ȷ��������־
    int pointFlag;
	//��Ϊ����ʱ����ԭ�ü�����εĵ�����
	int clippedIndex;
	//��Ϊ����ʱ����ԭ�ü����ڵĵ�����
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
//����νṹ�����
struct WeilerPolygon
{
	vector<WeilerPoint> points;
//	WeilerPolygon()
};
//�ü��������νṹ�����
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
	//1�Ǳ��ü������
	//2�Ǵ���

	 int flag;
	 bool m_MouseIsDown;
	 CPen myPen;
	 CPoint m_EndPoint;
	 CPoint m_StartPoint;
	 
	//���ü������
 	 WeilerPolygon clippedPolygon;
	//����
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
	//�Ƚ�Զ��
	int CompareDistance(WeilerPoint startPoint,WeilerPoint endPoint1,WeilerPoint endPoint2);
	//Ѱ�Ҳü�����εĲ�������
	int CWeilerAthertonDemoView::GetInsertClippedIndex(vector<WeilerPoint> points,vector<WeilerPoint> insertPoints,int index);
	//Ѱ�Ҳü����ڵĲ�������
    int CWeilerAthertonDemoView::GetInsertClippingIndex(vector<WeilerPoint> points,vector<WeilerPoint> insertPoints,int index);
	//�����㷨
    WeilerGenPolygon WeilerAtherton(WeilerPolygon polygonCliped,WeilerPolygon polygonCliping);
	//���߶ν���
	int GetIntersectionPoint(WeilerPoint pointx1,WeilerPoint pointy1,WeilerPoint pointx2,WeilerPoint pointy2,WeilerPoint &point);
	//�󽻵㼯
	vector<WeilerPoint> GetIntersectionPoint(WeilerPolygon polygonCliped,WeilerPolygon polygonCliping);
	//����������ζ���
	vector<WeilerPoint> GetNewListPointClipped(WeilerPolygon polygon,vector<WeilerPoint> &points);
	//�������ü�����
	vector<WeilerPoint> GetNewListPointClipping(WeilerPolygon polygon,vector<WeilerPoint> points);
	//Ѱ����� ����������ڵ�����
	int GetIntoPoint(vector<WeilerPoint> &points,WeilerPoint &point);
	//Ѱ�ҳ��� ���س������ڵ�����
	int GetOutPoint(vector<WeilerPoint> &points,WeilerPoint &point);
	//ɾ�������
	void DeletePointIntoFlag(vector<WeilerPoint> &points,WeilerPoint point);
	//ĩβ���붥��
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
