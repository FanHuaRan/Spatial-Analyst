using OverlayDemo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayDemo.Core
{
    /// <summary>
    /// 裁剪实现类
    /// </summary>
    class CutCoreClass:ICutCore
    {
        //基本空间分析字段
        private IGraphCore graphCore;

        public CutCoreClass(IGraphCore graphCore)
        {
            this.graphCore = graphCore;
        }

        public List<List<MyPoint>> CutAlgorithm(Entity.MyPolygon polygon, List<MyPoint> points)
        {
            //结果线段集
            List<List<MyPoint>> resultPointArray = new List<List<MyPoint>>();
            //所有交点
            List<MyPoint> interPoints = GetAllIntersePoint(polygon, points);
            //考虑没有交点的情况 全在多边形内或者全在多边形外
            if (interPoints.Count == 0)
            {
                //判断一个点是否在多边形内 则返回全部线点
                if (graphCore.JudgePointWithPolygon(points[0], polygon) == 2)
                {
                    MyPoint[] pointArray = new MyPoint[points.Count];
                    points.CopyTo(pointArray);
                    resultPointArray.Add(pointArray.ToList<MyPoint>());
                }
                return resultPointArray;
            }
            //将交点有序插入序列中得到新序列
            List<MyPoint> newPoints = InsertPoint(points, interPoints);
            //求解保留点过程中是否连续的标志位
            bool contin = false;
            List<MyPoint> tempPoints = new List<MyPoint>();
            //得出保留点
            for (int i = 0; i < newPoints.Count; i++)
            {
                MyPoint point = newPoints[i];
                //该点在多边形之外
                if (graphCore.JudgePointWithPolygon(point, polygon) == 0)
                {
                    if (contin)
                    {
                        resultPointArray.Add(tempPoints);
                    }
                    contin = false;
                    continue;
                }
                //点在多边形内
                if (contin)
                {
                    tempPoints.Add(point);
                }
                else
                {
                    tempPoints = new List<MyPoint>();
                    tempPoints.Add(point);
                }
                contin = true;
            }
            return resultPointArray;
        }
        /// <summary>
        /// 获取线段与多边形的所有交点
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="points"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 利用射线算法有序插入交点
        /// </summary>
        /// <param name="points"></param>
        /// <param name="interPoints"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 出入点判断
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="points"></param>
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
