// WeilerAthertonDemoView.cpp : implementation of the CWeilerAthertonDemoView class
//

#include "stdafx.h"
#include "WeilerAthertonDemo.h"

#include "WeilerAthertonDemoDoc.h"
#include "WeilerAthertonDemoView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CWeilerAthertonDemoView

IMPLEMENT_DYNCREATE(CWeilerAthertonDemoView, CView)

BEGIN_MESSAGE_MAP(CWeilerAthertonDemoView, CView)
	//{{AFX_MSG_MAP(CWeilerAthertonDemoView)
	ON_COMMAND(ID_Clipp, OnClipp)
	ON_COMMAND(ID_PolygonClipped, OnPolygonClipped)
	ON_COMMAND(ID_PolygonClipping, OnPolygonClipping)
	ON_COMMAND(ID_Remove, OnRemove)
	ON_WM_MOUSEMOVE()
	ON_WM_LBUTTONDOWN()
	ON_WM_LBUTTONUP()
	ON_WM_RBUTTONDOWN()
	//}}AFX_MSG_MAP
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, CView::OnFilePrintPreview)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CWeilerAthertonDemoView construction/destruction

CWeilerAthertonDemoView::CWeilerAthertonDemoView()
{
	// TODO: add construction code here
	myPen.CreatePen(PS_SOLID,1,RGB(255,255,0));
	m_MouseIsDown=false;
	flag=0;
}

CWeilerAthertonDemoView::~CWeilerAthertonDemoView()
{
}

BOOL CWeilerAthertonDemoView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}

/////////////////////////////////////////////////////////////////////////////
// CWeilerAthertonDemoView drawing

void CWeilerAthertonDemoView::OnDraw(CDC* pDC)
{
	CWeilerAthertonDemoDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	// TODO: add draw code for native data here
}

/////////////////////////////////////////////////////////////////////////////
// CWeilerAthertonDemoView printing

BOOL CWeilerAthertonDemoView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void CWeilerAthertonDemoView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void CWeilerAthertonDemoView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}

/////////////////////////////////////////////////////////////////////////////
// CWeilerAthertonDemoView diagnostics

#ifdef _DEBUG
void CWeilerAthertonDemoView::AssertValid() const
{
	CView::AssertValid();
}

void CWeilerAthertonDemoView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CWeilerAthertonDemoDoc* CWeilerAthertonDemoView::GetDocument() // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CWeilerAthertonDemoDoc)));
	return (CWeilerAthertonDemoDoc*)m_pDocument;
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CWeilerAthertonDemoView message handlers

void CWeilerAthertonDemoView::OnClipp() 
{
	// TODO: Add your command handler code here
	WeilerGenPolygon polygons=CWeilerAthertonDemoView::WeilerAtherton(CWeilerAthertonDemoView::clippedPolygon,CWeilerAthertonDemoView::clippingPolygon);
	CleanWindow();
	CWeilerAthertonDemoView::DrawResult(polygons);
}

void CWeilerAthertonDemoView::OnPolygonClipped() 
{
	// TODO: Add your command handler code here
	flag=1;
}

void CWeilerAthertonDemoView::OnPolygonClipping() 
{
	// TODO: Add your command handler code here
	flag=2;
}

void CWeilerAthertonDemoView::OnRemove() 
{
	// TODO: Add your command handler code here
	CWeilerAthertonDemoView::clippedPolygon.points.clear();
	CWeilerAthertonDemoView::clippingPolygon.points.clear();
	flag=0;
	CleanWindow();
}

void CWeilerAthertonDemoView::OnMouseMove(UINT nFlags, CPoint point) 
{
	// TODO: Add your message handler code here and/or call default
	if(m_MouseIsDown)
	{
		DrawXorLine(m_StartPoint,m_EndPoint,point);
		m_EndPoint=point;
	}
	CView::OnMouseMove(nFlags, point);
}

void CWeilerAthertonDemoView::OnLButtonDown(UINT nFlags, CPoint point) 
{
	// TODO: Add your message handler code here and/or call default
	if(!flag)
	{
		return;
	}
	m_MouseIsDown=true;
	m_StartPoint=point;
	m_EndPoint=m_StartPoint;
	WeilerPoint myPoint(point.x,point.y);
	if(flag==1)
	{
	
		CWeilerAthertonDemoView::clippedPolygon.points.push_back(myPoint);
	}
	else if(flag==2)
	{
		CWeilerAthertonDemoView::clippingPolygon.points.push_back(myPoint);
	}
	CView::OnLButtonDown(nFlags, point);
}

void CWeilerAthertonDemoView::OnLButtonUp(UINT nFlags, CPoint point) 
{
	// TODO: Add your message handler code here and/or call default
	if(!flag)
	{
		return;
	}
	if(m_MouseIsDown)
	{
		DrawXorLine(m_StartPoint,m_EndPoint,point);
	//	m_MouseIsDown=false;
		m_EndPoint=point;
		/*
		WeilerPoint myPoint(point.x,point.y);
		
		if(flag==1)
		{
		
			CWeilerAthertonDemoView::clippedPolygon.points.push_back(myPoint);
		}
		else if(flag==2)
		{
			CWeilerAthertonDemoView::clippingPolygon.points.push_back(myPoint);
		}
		*/
	}
	CView::OnLButtonUp(nFlags, point);
}

void CWeilerAthertonDemoView::OnRButtonDown(UINT nFlags, CPoint point) 
{
	// TODO: Add your message handler code here and/or call default
	if(!flag)
	{
		return;
	}
	m_MouseIsDown=false;
	CPoint point1;
	CPoint point2;
	if(flag==1)
	{
	 int size=CWeilerAthertonDemoView::clippedPolygon.points.size();
	 WeilerPoint myPoint1=CWeilerAthertonDemoView::clippedPolygon.points[size-1];
	 WeilerPoint myPoint2=CWeilerAthertonDemoView::clippedPolygon.points[0];
	 point1.x=myPoint1.x;
	 point1.y=myPoint1.y;
	 point2.x=myPoint2.x;
	 point2.y=myPoint2.y;
	}
	else if(flag==2)
	{
	 int size=CWeilerAthertonDemoView::clippingPolygon.points.size();
	 WeilerPoint myPoint1=CWeilerAthertonDemoView::clippingPolygon.points[size-1];
	 WeilerPoint myPoint2=CWeilerAthertonDemoView::clippingPolygon.points[0];
	 point1.x=myPoint1.x;
	 point1.y=myPoint1.y;
	 point2.x=myPoint2.x;
	 point2.y=myPoint2.y;
	}
	DrawLine(point1,point2);
}
//������ߺ���
void CWeilerAthertonDemoView::DrawXorLine(CPoint p0, CPoint p1, CPoint oldP)
{
  	CClientDC dc(this);
	dc.SelectObject(myPen);
	dc.SetROP2(R2_XORPEN);//��������ͼģʽ
	//����ԭֱ��
	dc.MoveTo(p0);
	dc.LineTo(oldP);
	//������ֱ��
	dc.MoveTo(p0);
	dc.LineTo(p1);
}
//����
void CWeilerAthertonDemoView::DrawLine(CPoint p0, CPoint p1)
{
 	CClientDC dc(this);
	dc.SelectObject(myPen);
	//��������ͼģʽ ��ɫ���в���
	dc.SetROP2(R2_XORPEN);
	dc.MoveTo(p0);
	dc.LineTo(p1);
}

//���ü����
void CWeilerAthertonDemoView::DrawResult(WeilerGenPolygon polygons)
{
	CClientDC dc(this);
	CPen resultPen;
	resultPen.CreatePen(PS_SOLID,5,RGB(255,0,0));
	dc.SelectObject(resultPen);
	CPoint p0;
	CPoint p1;
	CPoint testPoint(0,0);
	for(int i=0;i<polygons.polygons.size();i++)
	{
		vector<WeilerPoint> points=polygons.polygons[i].points;
		for(int j=0;j<points.size();j++)
		{
			p0.x=points[j].x;
			p0.y=points[j].y;
			if(j!=points.size()-1)
			{
				p1.x=points[j+1].x;
				p1.y=points[j+1].y;
			}
			else
			{
				p1.x=points[0].x;
				p1.y=points[0].y;
			}
			dc.MoveTo(p0);
			dc.LineTo(p1);
		}
	}
}
/**********************************�㷨����***************************************************/
//�Ƚ�����Զ��
 int CWeilerAthertonDemoView::CompareDistance(WeilerPoint startPoint,WeilerPoint endPoint1,WeilerPoint endPoint2)
 {
   float xLen=endPoint1.x-startPoint.x;
   float yLen=endPoint1.y-startPoint.y;
   float lineDistance=xLen*xLen+yLen*yLen;
   float xLen1=endPoint2.x-startPoint.x;
   float yLen1=endPoint2.y-startPoint.y;
   float lineDistance1=xLen1*xLen1+yLen1*yLen1;
   return lineDistance>lineDistance1?1:0;
 }
//Ѱ�������߶εĽ���
int CWeilerAthertonDemoView::GetIntersectionPoint(WeilerPoint pointx1,WeilerPoint pointy1,WeilerPoint pointx2,WeilerPoint pointy2,WeilerPoint &point)
{
  /** 1 �����Է�����, ���߶ν���. **/  
  // �����ĸΪ0 ��ƽ�л���, ���ཻ  
    float denominator = (pointy1.y - pointx1.y)*(pointy2.x - pointx2.x) - (pointx1.x - pointy1.x)*(pointx2.y - pointy2.y);  
    if (denominator==0) {  
        return 0;  
    }  
   
  // �߶�����ֱ�ߵĽ������� (x , y)      
    float x = ( (pointy1.x - pointx1.x) * (pointy2.x - pointx2.x) * (pointx2.y - pointx1.y)   
                + (pointy1.y - pointx1.y) * (pointy2.x - pointx2.x) * pointx1.x   
                - (pointy2.y - pointx2.y) * (pointy1.x - pointx1.x) * pointx2.x ) / denominator ;  
    float y = -( (pointy1.y - pointx1.y) * (pointy2.y - pointx2.y) * (pointx2.x - pointx1.x)   
                + (pointy1.x - pointx1.x) * (pointy2.y - pointx2.y) * pointx1.y   
                - (pointy2.x - pointx2.x) * (pointy1.y - pointx1.y) * pointx2.y ) / denominator;  
  
  /** 2 �жϽ����Ƿ��������߶��� **/  
    if (  
        // �������߶�1��  
        (x - pointx1.x) * (x - pointy1.x) <= 0 && (y - pointx1.y) * (y - pointy1.y) <= 0  
        // �ҽ���Ҳ���߶�2��  
         && (x - pointx2.x) * (x - pointy2.x) <= 0 && (y - pointx2.y) * (y - pointy2.y) <= 0  
        )
	{  
		point.x=x;
		point.y=y;
		return 1;
    }  
    //�����ཻ  
    return 0;  
}
//������Ľ��㼯
vector<WeilerPoint> CWeilerAthertonDemoView::GetIntersectionPoint(WeilerPolygon polygonCliped,WeilerPolygon polygonCliping)
{
	vector<WeilerPoint> points;
	int polygonClipedLenth=polygonCliped.points.size();
	int polygonClipingLenth=polygonCliping.points.size();
    for(int i=0;i<polygonClipedLenth;i++)
    {
		for(int j=0;j<polygonClipingLenth;j++)
		{
			WeilerPoint point;
			WeilerPoint pointx1=polygonCliped.points[i];
			WeilerPoint pointy1;
			if(i==polygonClipedLenth-1)
			{ 
				pointy1=polygonCliped.points[0];
			}
			else
			{
				pointy1=polygonCliped.points[i+1];
			}

			WeilerPoint pointx2=polygonCliping.points[j];
			WeilerPoint pointy2;
			if(j==polygonClipedLenth-1)
			{ 
				pointy2=polygonCliping.points[0];
			}
			else
			{
				pointy2=polygonCliping.points[j+1];
			}

			if(CWeilerAthertonDemoView::GetIntersectionPoint(pointx1,pointy1,pointx2,pointy2,point))
			{
				point.pointFlag=3;
				point.clippedIndex=i;
				point.clippingIndex=j;
				points.push_back(point);
			}
		}

   }
	return points;
}

//Ѱ�ҽ�����뱻�ü�����ζ������е�����
int CWeilerAthertonDemoView::GetInsertClippedIndex(vector<WeilerPoint> points,vector<WeilerPoint> insertPoints,int index)
{
 //��ʼƫ����
 int initialCount=0;
 int firstIndex=insertPoints[index].clippedIndex;
 vector<int> storageSames;
 for(int i=0;i<index;i++)
 {
	 if(insertPoints[i].clippedIndex>firstIndex)
	 {
		initialCount++;
	 }
	 else if(insertPoints[i].clippedIndex==firstIndex)
	 {
		storageSames.push_back(i);
		initialCount++;
	 }
 }
 //��������λ��
 int getPointIndex=firstIndex+index-initialCount;
 //��ʱ���
 WeilerPoint startPoint=points[getPointIndex];
 //�����
 WeilerPoint getPoint=insertPoints[index];
 //����ƫ����
 int count=0;
 for(int j=0;j<storageSames.size();j++)
 {
   if(CompareDistance(startPoint,insertPoints[storageSames[j]],getPoint))
   {
	   break;
   }
   count++;
 }
 return getPointIndex+count;
}
//Ѱ�ҽ������ü����ڶ������е�����
int CWeilerAthertonDemoView::GetInsertClippingIndex(vector<WeilerPoint> points,vector<WeilerPoint> insertPoints,int index)
{
 //��ʼƫ����
 int initialCount=0;
 int firstIndex=insertPoints[index].clippingIndex;
 vector<int> storageSames;
 for(int i=0;i<index;i++)
 {
	 if(insertPoints[i].clippingIndex>firstIndex)
	 {
		initialCount++;			
	 }
	 else if(insertPoints[i].clippingIndex==firstIndex)
	 {
		storageSames.push_back(i);
		initialCount++;
	 }
 }
 //��������λ��
 int getPointIndex=firstIndex+index-initialCount;
 //��ʱ���
 WeilerPoint startPoint=points[getPointIndex];
 //�����
 WeilerPoint getPoint=insertPoints[index];
 //����ƫ����
 int count=0;
 for(int j=0;j<storageSames.size();j++)
 {
   if(CompareDistance(startPoint,insertPoints[storageSames[j]],getPoint))
   {
	   break;
   }
   count++;
 }
 return getPointIndex+count;
}

//������뱻�ü�����εĶ������� ���Խ������н������������������־�ĸ���
vector<WeilerPoint> CWeilerAthertonDemoView::GetNewListPointClipped(WeilerPolygon polygon,vector<WeilerPoint> &points)
{
  vector<WeilerPoint> resultPoints;
  for(int i=0;i<polygon.points.size();i++)
  {
	  WeilerPoint point=polygon.points[i].CopyPoint();
	  resultPoints.push_back(point);
  }
  for(int j=0;j<points.size();j++)
  {
	  int index=GetInsertClippedIndex(resultPoints,points,j);
	  resultPoints.insert(resultPoints.begin()+index+1,points[j]);
  }
  //������
  int count=0;
  //���½�������
  for(int k=0;k<resultPoints.size();k++)
  {
	  //ż�������������
	  //���������������
	  if(resultPoints[k].pointFlag==3)
	  {
		  if(count%2==0)
		  {
			  resultPoints[k].pointFlag=1;
		  }
		  else
		  {
			  resultPoints[k].pointFlag=2;
		  }
		  points[count]=resultPoints[k];
		  count++;
	  }
  }
  return resultPoints;
}
//�������ü����ڵĶ�������
vector<WeilerPoint> CWeilerAthertonDemoView::GetNewListPointClipping(WeilerPolygon polygon,vector<WeilerPoint> points)
{
  vector<WeilerPoint> resultPoints;
  for(int i=0;i<polygon.points.size();i++)
  {
	  WeilerPoint point=polygon.points[i].CopyPoint();
	  resultPoints.push_back(point);
  }
  for(int j=0;j<points.size();j++)
  {
	  WeilerPoint point=points[j];
	  int index=GetInsertClippingIndex(resultPoints,points,j);
	  resultPoints.insert(resultPoints.begin()+index+1,points[j]);
  }
  return resultPoints;
}
//Ѱ�ҵ㼯�����������ֵ���� �������� ע������ǲ�����λ�ڵ�һ��Ԫ�ص� ���Է���0����û���ҵ�
int CWeilerAthertonDemoView::GetIntoPoint(vector<WeilerPoint> &points,WeilerPoint &point)
{
	for(int i=0;i<points.size();i++)
	{
		WeilerPoint tempPoint=points[i];
		if(tempPoint.pointFlag==1)
		{
			point.x=tempPoint.x;
			point.y=tempPoint.y;
		    point.pointFlag=tempPoint.pointFlag;
			return i;
		}
	}
	return 0;
}
//������֪����Ѱ�ҳ��� �������� ע������ǲ�����λ�ڵ�һ��Ԫ�ص� ���Է���0����û���ҵ�
int CWeilerAthertonDemoView::GetOutPoint(vector<WeilerPoint> &points,WeilerPoint &point)
{
	for(int i=0;i<points.size();i++)
	{
		WeilerPoint tempPoint=points[i];
		if(point.x==tempPoint.x&&point.y==tempPoint.y&&tempPoint.pointFlag==2)
		{
		    point.pointFlag=tempPoint.pointFlag;
			return i;
		}
		
	}
	return 0;
}
//ĩβ���붥��
void CWeilerAthertonDemoView::InsertTail(vector<WeilerPoint> &points,WeilerPoint point)
{
	WeilerPoint tempPoint=point.CopyPoint();
	points.push_back(tempPoint);
}
//ɾ�����ı�־
void CWeilerAthertonDemoView::DeletePointIntoFlag(vector<WeilerPoint> &points,WeilerPoint point)
{
 	
	for(int i=0;i<points.size();i++)
	{
		if(points[i].x==point.x&&points[i].y==point.y)
		{
			points[i].pointFlag=0;
			break;
		}
	}
}
//�����㷨
WeilerGenPolygon CWeilerAthertonDemoView::WeilerAtherton(WeilerPolygon polygonCliped,WeilerPolygon polygonCliping)
{
	//����������
	WeilerGenPolygon resultPolygons;
	//��ý��㼯
	vector<WeilerPoint> interSectionPoints=CWeilerAthertonDemoView::GetIntersectionPoint(polygonCliped,polygonCliping);
	//��������뱻�ü�����ζ���������
	vector<WeilerPoint> newClippedPoints=CWeilerAthertonDemoView::GetNewListPointClipped(polygonCliped,interSectionPoints);
	//���������ü�����ζ���������
	vector<WeilerPoint> newClippingPoints=CWeilerAthertonDemoView::GetNewListPointClipping(polygonCliping,interSectionPoints);
	//��Ѱ��
	WeilerPoint point;
	//�ݴ��
	WeilerPoint storagePoint;
	//���ü��������Ѱ����
	int clippedSearchIndex=0;
	//�ü�������Ѱ����
	int clippingSearchIndex=0;
	//����newClippedPointsѰ�ҵ����
	while(CWeilerAthertonDemoView::GetIntoPoint(newClippedPoints,point))
	{
		//�ݴ����
		 storagePoint=point.CopyPoint();
		//���嵱ǰ���ҵ�������
		WeilerPolygon polygon;
		do
		{
		//��ȡ�����������
		clippedSearchIndex=CWeilerAthertonDemoView::GetIntoPoint(newClippedPoints,point);
		//��������������
		CWeilerAthertonDemoView::InsertTail(polygon.points,point);
		//ɾ��newClippedPoints�иõ�������
		CWeilerAthertonDemoView::DeletePointIntoFlag(newClippedPoints,point);
		//��newClippedPoints��ȡ��������˳��ȡ���� ����ĩβ�򷵻ص�һ��Ԫ��
        for(int i=0;i<newClippedPoints.size();i++)
		{
			int index=clippedSearchIndex+i+1;
			if(index>newClippedPoints.size()-1)
			{
				index=index-newClippedPoints.size();
			}
		    point=newClippedPoints[index];
			//����ǳ���������
			if(point.pointFlag==2)
			{
				break;
			}
			//������ǳ��������������
			CWeilerAthertonDemoView::InsertTail(polygon.points,point);
		}
		//��ȡ������������
		clippingSearchIndex=CWeilerAthertonDemoView::GetOutPoint(newClippingPoints,point);
		//��newClippingPoints˳��ȡ����
		for(int j=0;j<newClippingPoints.size();j++)
			{
				int index=clippingSearchIndex+j;
				if(index>newClippingPoints.size()-1)
				{
					index=index-newClippingPoints.size();
				}
				point=newClippingPoints[index];
				//��������������
				if(point.pointFlag==1)
				{
					break;
				}
				//���������������������
				CWeilerAthertonDemoView::InsertTail(polygon.points,point);
			}
		
		}
		//�����ݴ�����˳�
		while(point.x!=storagePoint.x||point.y!=storagePoint.y);

		//���浱ǰѰ�ҵ��Ķ����
		resultPolygons.polygons.push_back(polygon);
	}
	return resultPolygons;
}

/**********************************�㷨���Ľ���***************************************************/

void CWeilerAthertonDemoView::showVecter(vector<WeilerPoint> points)
{
	int size=points.size();
	for(int i=0;i<size;i++)
	{
		WeilerPoint point=points[i];
	}
}
void CWeilerAthertonDemoView::showIndex(vector<int> indexs)
{
	int size=indexs.size();
	for(int i=0;i<size;i++)
	{
		int index=indexs[i];
	}
}
void CWeilerAthertonDemoView::CleanWindow()
{
	CRect rect;
	GetClientRect(&rect);
	int width=rect.Width();
	int height=rect.Height();
	CDC *pdc=GetDC();
	CPen cleanPen(PS_SOLID,1,RGB(255,255,255));
	CBrush brush(RGB(255,255,255));
	pdc->SelectObject(cleanPen);
	pdc->FillRect(CRect(0,0,width,height),&brush);
}

