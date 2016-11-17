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
//画异或线函数
void CWeilerAthertonDemoView::DrawXorLine(CPoint p0, CPoint p1, CPoint oldP)
{
  	CClientDC dc(this);
	dc.SelectObject(myPen);
	dc.SetROP2(R2_XORPEN);//设置异或绘图模式
	//擦除原直线
	dc.MoveTo(p0);
	dc.LineTo(oldP);
	//绘制新直线
	dc.MoveTo(p0);
	dc.LineTo(p1);
}
//画线
void CWeilerAthertonDemoView::DrawLine(CPoint p0, CPoint p1)
{
 	CClientDC dc(this);
	dc.SelectObject(myPen);
	//设置异或绘图模式 颜色会有差异
	dc.SetROP2(R2_XORPEN);
	dc.MoveTo(p0);
	dc.LineTo(p1);
}

//画裁剪结果
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
/**********************************算法核心***************************************************/
//比较三点远近
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
//寻找两条线段的交点
int CWeilerAthertonDemoView::GetIntersectionPoint(WeilerPoint pointx1,WeilerPoint pointy1,WeilerPoint pointx2,WeilerPoint pointy2,WeilerPoint &point)
{
  /** 1 解线性方程组, 求线段交点. **/  
  // 如果分母为0 则平行或共线, 不相交  
    float denominator = (pointy1.y - pointx1.y)*(pointy2.x - pointx2.x) - (pointx1.x - pointy1.x)*(pointx2.y - pointy2.y);  
    if (denominator==0) {  
        return 0;  
    }  
   
  // 线段所在直线的交点坐标 (x , y)      
    float x = ( (pointy1.x - pointx1.x) * (pointy2.x - pointx2.x) * (pointx2.y - pointx1.y)   
                + (pointy1.y - pointx1.y) * (pointy2.x - pointx2.x) * pointx1.x   
                - (pointy2.y - pointx2.y) * (pointy1.x - pointx1.x) * pointx2.x ) / denominator ;  
    float y = -( (pointy1.y - pointx1.y) * (pointy2.y - pointx2.y) * (pointx2.x - pointx1.x)   
                + (pointy1.x - pointx1.x) * (pointy2.y - pointx2.y) * pointx1.y   
                - (pointy2.x - pointx2.x) * (pointy1.y - pointx1.y) * pointx2.y ) / denominator;  
  
  /** 2 判断交点是否在两条线段上 **/  
    if (  
        // 交点在线段1上  
        (x - pointx1.x) * (x - pointy1.x) <= 0 && (y - pointx1.y) * (y - pointy1.y) <= 0  
        // 且交点也在线段2上  
         && (x - pointx2.x) * (x - pointy2.x) <= 0 && (y - pointx2.y) * (y - pointy2.y) <= 0  
        )
	{  
		point.x=x;
		point.y=y;
		return 1;
    }  
    //否则不相交  
    return 0;  
}
//求初步的交点集
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

//寻找交点插入被裁剪多边形顶点序列的索引
int CWeilerAthertonDemoView::GetInsertClippedIndex(vector<WeilerPoint> points,vector<WeilerPoint> insertPoints,int index)
{
 //初始偏移量
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
 //所求起点的位置
 int getPointIndex=firstIndex+index-initialCount;
 //临时起点
 WeilerPoint startPoint=points[getPointIndex];
 //所求点
 WeilerPoint getPoint=insertPoints[index];
 //往后偏移量
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
//寻找交点插入裁剪窗口顶点序列的索引
int CWeilerAthertonDemoView::GetInsertClippingIndex(vector<WeilerPoint> points,vector<WeilerPoint> insertPoints,int index)
{
 //初始偏移量
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
 //所求起点的位置
 int getPointIndex=firstIndex+index-initialCount;
 //临时起点
 WeilerPoint startPoint=points[getPointIndex];
 //所求点
 WeilerPoint getPoint=insertPoints[index];
 //往后偏移量
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

//交点插入被裁剪多边形的顶点序列 并对交点序列进行重新排序和入出点标志的更新
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
  //计数器
  int count=0;
  //更新交点序列
  for(int k=0;k<resultPoints.size();k++)
  {
	  //偶数计数代表入点
	  //计数计数代表出点
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
//交点插入裁剪窗口的顶点序列
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
//寻找点集序列中所出现的入点 返回索引 注意入点是不可能位于第一个元素的 所以返回0代表没有找到
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
//根据已知坐标寻找出点 返回索引 注意出点是不可能位于第一个元素的 所以返回0代表没有找到
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
//末尾插入顶点
void CWeilerAthertonDemoView::InsertTail(vector<WeilerPoint> &points,WeilerPoint point)
{
	WeilerPoint tempPoint=point.CopyPoint();
	points.push_back(tempPoint);
}
//删除入点的标志
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
//核心算法
WeilerGenPolygon CWeilerAthertonDemoView::WeilerAtherton(WeilerPolygon polygonCliped,WeilerPolygon polygonCliping)
{
	//输出结果变量
	WeilerGenPolygon resultPolygons;
	//求得交点集
	vector<WeilerPoint> interSectionPoints=CWeilerAthertonDemoView::GetIntersectionPoint(polygonCliped,polygonCliping);
	//将交点插入被裁剪多边形顶点序列中
	vector<WeilerPoint> newClippedPoints=CWeilerAthertonDemoView::GetNewListPointClipped(polygonCliped,interSectionPoints);
	//将交点插入裁剪多边形顶点序列中
	vector<WeilerPoint> newClippingPoints=CWeilerAthertonDemoView::GetNewListPointClipping(polygonCliping,interSectionPoints);
	//搜寻点
	WeilerPoint point;
	//暂存点
	WeilerPoint storagePoint;
	//被裁减多边形搜寻索引
	int clippedSearchIndex=0;
	//裁剪窗口搜寻索引
	int clippingSearchIndex=0;
	//当在newClippedPoints寻找到入点
	while(CWeilerAthertonDemoView::GetIntoPoint(newClippedPoints,point))
	{
		//暂存入点
		 storagePoint=point.CopyPoint();
		//定义当前查找的输出结果
		WeilerPolygon polygon;
		do
		{
		//获取入点所在索引
		clippedSearchIndex=CWeilerAthertonDemoView::GetIntoPoint(newClippedPoints,point);
		//将入点存入输出结果
		CWeilerAthertonDemoView::InsertTail(polygon.points,point);
		//删除newClippedPoints中该点的入点标记
		CWeilerAthertonDemoView::DeletePointIntoFlag(newClippedPoints,point);
		//在newClippedPoints中取得入点后面顺序取顶点 碰上末尾则返回第一个元素
        for(int i=0;i<newClippedPoints.size();i++)
		{
			int index=clippedSearchIndex+i+1;
			if(index>newClippedPoints.size()-1)
			{
				index=index-newClippedPoints.size();
			}
		    point=newClippedPoints[index];
			//如果是出点则跳出
			if(point.pointFlag==2)
			{
				break;
			}
			//如果不是出点则存入输出结果
			CWeilerAthertonDemoView::InsertTail(polygon.points,point);
		}
		//获取出点所在索引
		clippingSearchIndex=CWeilerAthertonDemoView::GetOutPoint(newClippingPoints,point);
		//沿newClippingPoints顺序取顶点
		for(int j=0;j<newClippingPoints.size();j++)
			{
				int index=clippingSearchIndex+j;
				if(index>newClippingPoints.size()-1)
				{
					index=index-newClippingPoints.size();
				}
				point=newClippingPoints[index];
				//如果是入点则跳出
				if(point.pointFlag==1)
				{
					break;
				}
				//如果不是入点则存入输出结果
				CWeilerAthertonDemoView::InsertTail(polygon.points,point);
			}
		
		}
		//等于暂存点则退出
		while(point.x!=storagePoint.x||point.y!=storagePoint.y);

		//保存当前寻找到的多边形
		resultPolygons.polygons.push_back(polygon);
	}
	return resultPolygons;
}

/**********************************算法核心结束***************************************************/

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

