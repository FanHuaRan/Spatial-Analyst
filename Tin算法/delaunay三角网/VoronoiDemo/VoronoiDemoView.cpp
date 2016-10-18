// VoronoiDemoView.cpp : implementation of the CVoronoiDemoView class
//

#include "stdafx.h"
#include "VoronoiDemo.h"

#include "VoronoiDemoDoc.h"
#include "VoronoiDemoView.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CVoronoiDemoView

IMPLEMENT_DYNCREATE(CVoronoiDemoView, CView)

BEGIN_MESSAGE_MAP(CVoronoiDemoView, CView)
	//{{AFX_MSG_MAP(CVoronoiDemoView)
	ON_COMMAND(ID_Area, OnArea)
	ON_COMMAND(ID_Build, OnBuild)
	ON_COMMAND(ID_Clear, OnClear)
	ON_COMMAND(ID_DrawPoint, OnDrawPoint)
	ON_WM_LBUTTONDOWN()
	ON_WM_LBUTTONUP()
	ON_WM_MOUSEMOVE()
	//}}AFX_MSG_MAP
	// Standard printing commands
	ON_COMMAND(ID_FILE_PRINT, CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_DIRECT, CView::OnFilePrint)
	ON_COMMAND(ID_FILE_PRINT_PREVIEW, CView::OnFilePrintPreview)
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CVoronoiDemoView construction/destruction

CVoronoiDemoView::CVoronoiDemoView()
{
	// TODO: add construction code here
	triPen.CreatePen(PS_DASH,1,RGB(0,0,0));
	IS_Drawing=false;
}

CVoronoiDemoView::~CVoronoiDemoView()
{
}

BOOL CVoronoiDemoView::PreCreateWindow(CREATESTRUCT& cs)
{
	// TODO: Modify the Window class or styles here by modifying
	//  the CREATESTRUCT cs

	return CView::PreCreateWindow(cs);
}

/////////////////////////////////////////////////////////////////////////////
// CVoronoiDemoView drawing

void CVoronoiDemoView::OnDraw(CDC* pDC)
{
	CVoronoiDemoDoc* pDoc = GetDocument();
	ASSERT_VALID(pDoc);
	/*
	CPoint p0(220,55);
	CPoint p1(100,150);
	CPoint p2(150,300);
	CPoint p3(160,170);
	points.push_back(p0);
	points.push_back(p1);
	points.push_back(p2);
	points.push_back(p3);
	BuildDelaunry();
	BuildTai();
	*/
	// TODO: add draw code for native data here
}

/////////////////////////////////////////////////////////////////////////////
// CVoronoiDemoView printing

BOOL CVoronoiDemoView::OnPreparePrinting(CPrintInfo* pInfo)
{
	// default preparation
	return DoPreparePrinting(pInfo);
}

void CVoronoiDemoView::OnBeginPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add extra initialization before printing
}

void CVoronoiDemoView::OnEndPrinting(CDC* /*pDC*/, CPrintInfo* /*pInfo*/)
{
	// TODO: add cleanup after printing
}

/////////////////////////////////////////////////////////////////////////////
// CVoronoiDemoView diagnostics

#ifdef _DEBUG
void CVoronoiDemoView::AssertValid() const
{
	CView::AssertValid();
}

void CVoronoiDemoView::Dump(CDumpContext& dc) const
{
	CView::Dump(dc);
}

CVoronoiDemoDoc* CVoronoiDemoView::GetDocument() // non-debug version is inline
{
	ASSERT(m_pDocument->IsKindOf(RUNTIME_CLASS(CVoronoiDemoDoc)));
	return (CVoronoiDemoDoc*)m_pDocument;
}
#endif //_DEBUG

/////////////////////////////////////////////////////////////////////////////
// CVoronoiDemoView message handlers

void CVoronoiDemoView::OnArea() 
{
	// TODO: Add your command handler code here
	shapeFlag=1;
}

void CVoronoiDemoView::OnBuild() 
{
	// TODO: Add your command handler code here
	//首先构建三角网
	BuildDelaunry();
//	BuildTai();
}

void CVoronoiDemoView::OnClear() 
{
	// TODO: Add your command handler code here
	ClearWindow();
	points.clear();
	lines.clear();
	triangles.clear();
	area.lines=NULL;
}

void CVoronoiDemoView::OnDrawPoint() 
{
	// TODO: Add your command handler code here
	shapeFlag=2;
}

void CVoronoiDemoView::ClearWindow()
{
	CRect rect;
	GetClientRect(&rect);
	int width=rect.Width();
	int height=rect.Height();
	CDC *pDC=GetDC();
	CPen pen(PS_SOLID,1,RGB(255,255,255));
	CBrush brush(RGB(255,255,255));
	pDC->SelectObject(pen);
	pDC->FillRect(CRect(0,0,width,height),&brush);
}

void CVoronoiDemoView::DrawTriangle(CPoint p0, CPoint p1, CPoint p2)
{
  CClientDC dc(this);
  dc.SelectObject(triPen);
  dc.MoveTo(p0);
  dc.LineTo(p1);
  dc.LineTo(p2);
  dc.LineTo(p0);
  Triangle tri;
  tri.points[0]=p0;
  tri.points[1]=p1;
  tri.points[2]=p2;
  for(int i=0;i<triangles.size();i++)
  {
	  if(IsEqual(tri,triangles[i]))
		  return;
  }
  tri.heart.x=(p0.x+p1.x+p2.x)/3;
  tri.heart.y=(p0.y+p1.y+p2.y)/3;
  triangles.push_back(tri);
}

CPoint CVoronoiDemoView::GetThridPoint(CPoint p1, CPoint p2)
{
   //如果已经使用则直接返回
   bool flag=IsUseLine(p1,p2);
   if(flag)
	   return CPoint(0,0);

	double temp=1;
	double choose=0;
	//求点 该点离该边最近则余弦最小
    for(int i=0;i<points.size();i++)
	{
		double direction=((p1.x-points[i].x)*(p2.y-points[i].y)-(p1.y-points[i].y)*(p2.x-points[i].x));
		//如果direction小于0则该点在左侧 继续循环
		if(direction<=0) continue;
		double a=sqrt(pow(p1.x-points[i].x,2)+pow(p1.y-points[i].y,2));
		double b=sqrt(pow(p2.x-points[i].x,2)+pow(p2.y-points[i].y,2));
		double c=sqrt(pow(p1.x-p2.x,2)+pow(p1.y-p2.y,2));
		double d=(a*a+b*b-c*c)/(2*a*b);
		if(d<temp)
		{
			temp=d;
			choose=i;
		}
	}
	if(choose!=0)
		return points[choose];
	else 
		return CPoint(0,0);
}

bool CVoronoiDemoView::IsUseLine(CPoint p0, CPoint p1)
{
	bool flag=false;
	for(int i=0;i<lines.size();i++)
	{
		if(p0==lines[i].StartPoint&&p1==lines[i].EndPoint)
			flag=true;
	}
	return flag;
}

void CVoronoiDemoView::Delaunay(CPoint p1, CPoint p2)
{
	CPoint p3,thirdPoint;
	thirdPoint=GetThridPoint(p1,p2);
	//如果等于圆点则返回
	if(thirdPoint.x==0&&thirdPoint.y==0)
		return;
	else 
	{
		p3=thirdPoint;
		this->DrawTriangle(p1,p2,p3);
		Line line;
		line.StartPoint=p1;
		line.EndPoint=p2;
		lines.push_back(line); //储存已知直线
	}
	//注意方向 一直往直线右边寻找
	Delaunay(p1,p3);
	Delaunay(p3,p2);//对尚未构三角形的边用算法进行递归调用
}

void CVoronoiDemoView::OnLButtonDown(UINT nFlags, CPoint point) 
{
	// TODO: Add your message handler code here and/or call default

	switch(shapeFlag)
	{
	case 1:
		startPoint=point;
		oldPoint=startPoint;
		IS_Drawing=true;
		break;
	case 2:
		DrawPoint(point);
		points.push_back(point);
		break;
	default:
		break;
	}
	CView::OnLButtonDown(nFlags, point);
}

void CVoronoiDemoView::OnLButtonUp(UINT nFlags, CPoint point) 
{
	// TODO: Add your message handler code here and/or call default
	if(IS_Drawing==false)
		return;
	switch(shapeFlag)
	{
	case 1:
		DrawXorRect(startPoint,point,oldPoint);
		oldPoint=point;
		break;
	case 2:
		break;
	default:
		break;
	}
	IS_Drawing=false;
	CView::OnLButtonUp(nFlags, point);
}

void CVoronoiDemoView::OnMouseMove(UINT nFlags, CPoint point) 
{
	// TODO: Add your message handler code here and/or call default
	if(IS_Drawing==false)
		return;
	switch(shapeFlag)
	{
	case 1:
		DrawXorRect(startPoint,point,oldPoint);
		oldPoint=point;
		break;
	case 2:
		break;
	default:
		break;
	}
	CView::OnMouseMove(nFlags, point);
}

void CVoronoiDemoView::DrawXorRect(CPoint p0, CPoint p1, CPoint oldP)
{
	CClientDC dc(this);
	CPen areaPen(PS_SOLID,1,RGB(255,255,255));
	dc.SelectObject(areaPen);
	dc.SelectStockObject(NULL_BRUSH);
	dc.SetROP2(R2_XORPEN);//设置异或绘图模式
	//擦除原矩形
	dc.Rectangle(p0.x,p0.y,oldP.x,oldP.y);
	//绘制新矩形
	dc.Rectangle(p0.x,p0.y,p1.x,p1.y);
	Area temp;
	temp.lines=new Line[4];
	CPoint p3(p0.x,p1.y);
	CPoint p4(p1.x,p0.y);
	temp.lines[0].StartPoint=p0;
	temp.lines[0].EndPoint=p3;
	temp.lines[1].StartPoint=p3;
	temp.lines[1].EndPoint=p1;
	temp.lines[2].StartPoint=p1;
	temp.lines[2].EndPoint=p4;
	temp.lines[3].StartPoint=p4;
	temp.lines[3].EndPoint=p0;
}

void CVoronoiDemoView::DrawPoint(CPoint point)
{
	CClientDC dc(this);
	CBrush myBrush;
	myBrush.CreateSolidBrush(RGB(0,0,0));
	dc.SelectObject(myBrush);
	dc.Ellipse(point.x-2,point.y-2,point.x+2,point.y+2);
}

void CVoronoiDemoView::BuildDelaunry()
{
	int n = points.size();
	//选择排序首先将点按照x坐标从小到大排序
	for (int i=0; i<n-1; i++)
	{
		for (int j=i+1; j<n; j++)
		{
			if (points[i].x > points[j].x)
			{
				CPoint tp1;
				tp1 = points[i];
				points[i] = points[j];
				points[j] = tp1;		
			}
		}
	}
	//确定基线
	if (n>=3)
	{
		if (points[0].y > points[1].y)
			this->Delaunay(points[0], points[1]);
		else
			this->Delaunay(points[1], points[0]);
	}
	else
		MessageBox("请输入至少三个以上的点");
}



CPoint CVoronoiDemoView::GetPoint(Triangle Tri)
{
	CPoint p1=Tri.points[0];
	CPoint p2=Tri.points[1];
 	CPoint p3=Tri.points[2];
	Tri.heart.x=(p1.x+p2.x+p3.x)/3;
	Tri.heart.y=(p1.y+p2.y+p3.y)/3;
	return Tri.heart;
}

void CVoronoiDemoView::BuildTai()
{
	
	CClientDC dc(this);
	CPen areaPen(PS_SOLID,1,RGB(255,0,0));
	dc.SelectObject(areaPen);

	//对每一个点的三角形绕逆时针排序连线
	for(int i=0;i<points.size();i++)
	{
	  //首先清除外心点
	  HeartPoints.clear();
	  //寻找当前顶点所在的第一个三角形
	  int locationFlag=0;
	  Triangle tri=LookForFirstTriangle(points[i],&locationFlag);
	  HeartPoints.push_back(tri.heart);
      currentTriangle=tri;
	  vector<int> temp;
	  //求另外两点中y坐标更大的点
	  for(int j=0;j<3;j++)
	  {
		  if(j!=locationFlag)
          {
			temp.push_back(j);
		  }
	  }
	  CPoint p1=tri.points[temp[0]].y>=tri.points[temp[1]].y?tri.points[temp[0]]:tri.points[temp[1]];
	  LookForNextTriHeart(points[i],p1,p1,true);
	   //连接内心
	  dc.MoveTo(HeartPoints[0]);
	  for(int m=1;m<HeartPoints.size();m++)
	  {
		dc.LineTo(HeartPoints[m]);
	  }

	  HeartPoints.clear();
	  points.push_back(tri.heart);
	  CPoint p2=tri.points[temp[0]].y<=tri.points[temp[1]].y?tri.points[temp[0]]:tri.points[temp[1]];
	 // LookForNextTriHeart(points[i],p2,p2,true);
	  //  LookForNextTriHeart(points[i],tri.points[temp[0]],tri.points[temp[0]],true);
	 //  LookForNextTriHeart(points[i],tri.points[temp[1]],tri.points[temp[1]],true);
	 //连接内心
	  dc.MoveTo(HeartPoints[0]);
	  for( m=1;m<HeartPoints.size();m++)
	  {
		dc.LineTo(HeartPoints[m]);
	  }
     	//dc.LineTo(HeartPoints[0]);
	}
}

//寻找离散点所在的第一个三角形 和该点在离散点当中的位置
Triangle CVoronoiDemoView::LookForFirstTriangle(CPoint point,int *location)
{
  for(int i=0;i<triangles.size();i++)
  {
	  for(int j=0;j<3;j++)
	  {
		  if(triangles[i].points[j]==point)
		  {
			  *location=j;
			  return triangles[i];
		  }
	  }
  }
}
//寻找下一个环绕三角形外心
void CVoronoiDemoView::LookForNextTriHeart(CPoint LineStartPoint, CPoint LineEndPoint,CPoint p0,BOOL IsFirst)
{ 
	//找回了起点且不是第一次则返回
   if(LineEndPoint==p0&&!IsFirst)
   {
	   return;
   }
   Triangle temp;
   BOOL flag=false;//是否找到
   for(int i=0;i<triangles.size();i++)
   {
	  temp=triangles[i];
	  int flagCount=0;
	  for(int j=0;j<3;j++)
	  {
         if(temp.points[j]==LineStartPoint||temp.points[j]==p0)
			 flagCount++;
	  }
	  if(flagCount==2&&!(IsEqual(temp,currentTriangle)))
	  {
		  flag=true;
		  break;
	  }
  }
  if(flag)
  {
	  HeartPoints.push_back(temp.heart);
	  currentTriangle=temp;
	  CPoint thirdPoint=GetTriangleThirdPoint(currentTriangle,LineStartPoint,p0);
	  LookForNextTriHeart(LineStartPoint,LineEndPoint,thirdPoint,false);
  }
  else
  {
	CClientDC dc(this);
	CPen areaPen(PS_SOLID,1,RGB(255,0,0));
	CPoint midPoint;
	if(IsFirst)
	{
		midPoint.x=(LineStartPoint.x+p0.x)/2;
		midPoint.y=(LineStartPoint.y+p0.y)/2;
	}
	else 
	{
		CPoint thirdPoint2=GetTriangleThirdPoint(currentTriangle,LineStartPoint,p0);
		midPoint.x=(LineStartPoint.x+thirdPoint2.x)/2;
		midPoint.y=(LineStartPoint.y+thirdPoint2.y)/2;
	}
		dc.SelectObject(areaPen);
		dc.MoveTo(currentTriangle.heart);
		dc.LineTo(midPoint);
	 //做外围区
  }
}

BOOL CVoronoiDemoView::IsEqual(Triangle Tri1, Triangle Tri2)
{
  int flag=0;
  for(int i=0;i<3;i++)
  {
	  for(int j=0;j<3;j++)
	  {
		  if(Tri1.points[i].x==Tri2.points[j].x&&Tri1.points[i].y==Tri2.points[j].y)
			  flag++;
	  }
  }
  if(flag>2) return true;
  else return false;
}
//找寻三角形的第三点
CPoint CVoronoiDemoView::GetTriangleThirdPoint(Triangle Tri, CPoint p0, CPoint p1)
{
  vector<int> indexs;
   for(int j=0;j<3;j++)
	  {
         if(Tri.points[j]==p0||Tri.points[j]==p1)
			 indexs.push_back(j);
	  }
   for(int i=0;i<3;i++)
   {
	   if(i!=indexs[0]&&i!=indexs[1])
		   return Tri.points[i];
   }
}
