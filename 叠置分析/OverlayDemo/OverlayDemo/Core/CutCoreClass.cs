using OverlayDemo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayDemo.Core
{
    class CutCoreClass:ICutCore
    {
        private IGraphCore graphCore;
        public CutCoreClass(IGraphCore graphCore)
        {
            this.graphCore = graphCore;
        }
        public List<List<MyPoint>> CutAlgorithm(Entity.MyPolygon polygon, List<MyPoint> points)
        {
            List<List<MyPoint>> resultPointArray = new List<List<MyPoint>>();
            //求所有交点
            List<MyPoint> interPoints = GetAllIntersePoint(polygon, points);
            //考虑没有交点的情况 全在多边形内或者全在多边形外
            if(interPoints.Count==0)
            {
                //判断一个点是否在多边形内 则返回全部线点
                if(graphCore.JudgePointWithPolygon(points[0],polygon)==2)
                {
                    resultPointArray.Add(points);
                }
                return resultPointArray;
            }
            //将交点有序插入序列中得到新序列
            List<MyPoint> newPoints = InsertPoint(points, interPoints);
            //判断出入点
          //  DefineOutIn(polygon, newPoints);
            List<MyPoint> tempPoints = new List<MyPoint>();
            bool isLianxu = false;
            //得出保留点
            for (int i = 0; i < newPoints.Count;i++ )
            {
                MyPoint point = newPoints[i];
                if (graphCore.JudgePointWithPolygon(point, polygon) == 0)
                {
                    if(isLianxu)
                    {
                        resultPointArray.Add(tempPoints);
                    }
                    isLianxu = false;
                    continue;
                }
                if (isLianxu)
                {
                    tempPoints.Add(point);
                }
                else
                {
                    tempPoints = new List<MyPoint>();
                    tempPoints.Add(point);
                }
                isLianxu = true;
            }
            return resultPointArray;
        }
        private List<MyPoint> GetAllIntersePoint(MyPolygon polygon,List<MyPoint>points)
        {
            List<MyPoint> interPoints = new List<MyPoint>();
            for(int i=0;i<polygon.Polylines.Count;i++)
            {
                for(int j=0;j<points.Count-1;j++)
                {
                    MyPoint point = graphCore.GetIntersectionPoint(polygon.Polylines[i], new MyPolyline(points[j], points[j + 1]));
                    if(point!=null)
                    {
                        interPoints.Add(point);
                    }
                }
            }
            foreach(var point in interPoints)
            {
                point.PointFlag = 1;
            }
            return interPoints;
        }
        private List<MyPoint> InsertPoint(List<MyPoint> points,List<MyPoint> interPoints)
        {
            List<MyPoint> newPoints = points.ToList<MyPoint>();
            for(int i=0;i<interPoints.Count;i++)
            {
                MyPoint point = interPoints[i];
                for (int j = 0; j < newPoints.Count - 1; j++)
                {
                    int value=graphCore.JudgePointWithLine(point, new MyPolyline(newPoints[j], newPoints[j + 1])) ;
                    if (value == 1)
                   {
                       newPoints.Insert(j+1, point);
                       break;
                   }
                }
            }
            return newPoints;
        }
        //判断出入点
        private void DefineOutIn(MyPolygon polygon,List<MyPoint> points)
        {
            for(int i=0;i<points.Count;i++)
            {
                MyPoint point = points[i];
                if(point.PointFlag==1)
                {
                    //上一个点在多边形外 即为入点 否则出点
                    if(graphCore.JudgePointWithPolygon(points[i-1],polygon)==0)
                    {
                        point.PointFlag = 2;
                    }
                    else
                    {
                        point.PointFlag = 3;
                    }
                }
            }
        }
    }
}
