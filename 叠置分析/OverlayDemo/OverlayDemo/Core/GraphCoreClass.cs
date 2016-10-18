using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlayDemo.Entity;
namespace OverlayDemo.Core
{
    class GraphCoreClass : IGraphCore
    {
        private int MINVALUE = -1;
        //误差值
        private int wucha = 200;
        /// <summary>
        /// 利用右手法则判断关系 返回1代表在线上 2代表在线段延长线上 3在线段右方 4在线段左方
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polyLine"></param>
        /// <returns></returns>
        public int JudgePointWithLine(MyPoint point, MyPolyline polyLine)
        {
            IMatirx matrix = new MatrixClass();
            List<List<float>> arrys = new List<List<float>>();
            List<float> firstRow = new List<float>() { point.X, point.Y, 1 };
            List<float> twiceRow = new List<float>() { polyLine.Point1.X, polyLine.Point1.Y, 1 };
            List<float> thirdRow = new List<float>() { polyLine.Point2.X, polyLine.Point2.Y, 1 };
            arrys.Add(firstRow);
            arrys.Add(twiceRow);
            arrys.Add(thirdRow);
            float value = matrix.GetRowColumnValue(arrys);
            //为了抵消误差 不用0
            if (Math.Abs(value)<=wucha)
            {
                if (IsInLineCore(point, polyLine))
                {
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
            else if (value >wucha)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }
        //是否在线段中央
        private bool IsInLineCore(MyPoint point, MyPolyline polyLine)
        {
            int x1 = polyLine.Point1.X;
            int x2 = polyLine.Point2.X;
            if (x1 > x2)
            {
                int temp = x2;
                x2 = x1;
                x1 = temp;
            }
            if (point.X >= x1 && point.X <= x2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        // 计算叉乘 |P0P1| × |P0P2| 
        private float Multiply(MyPoint p1, MyPoint p2, MyPoint p0)
        {

            return ((p1.X - p0.X) * (p2.Y - p0.Y) - (p2.X - p0.X) * (p1.Y - p0.Y));

        }
        //判断线与线是否相交 1为相交 0为不相交
        //应用了矢量外积算法
        public int JudgeLineWithLine(MyPolyline linea, MyPolyline lineB)
        {
            
            if ((Math.Max(linea.Point1.X, linea.Point2.X) >= Math.Min(lineB.Point1.X, lineB.Point2.X)) &&

                (Math.Max(lineB.Point1.X, lineB.Point2.X) >= Math.Min(linea.Point1.X, linea.Point2.X)) &&

                ((Math.Max(linea.Point1.Y, linea.Point2.Y) >= Math.Min(lineB.Point1.Y, lineB.Point2.Y)) &&

                (Math.Max(lineB.Point1.Y, lineB.Point2.Y) >= Math.Min(linea.Point1.Y, linea.Point2.Y)) &&

                (Multiply(lineB.Point1, linea.Point2, linea.Point1) * Multiply(linea.Point2, lineB.Point2, linea.Point1) >= 0) &&

                (Multiply(linea.Point1, lineB.Point2, lineB.Point1) * Multiply(lineB.Point2, linea.Point2, lineB.Point1) >= 0)))
            {
                return 1;
            }
            else
            {
                return 0;
            }
             
  
        }
        //使用射线算法判断点与多边形关系 奇内外偶
        //1在线上 2在多边形内 0在多边形外
        public int JudgePointWithPolygon(MyPoint point, MyPolygon polygon)
        {
            int n = polygon.Polylines.Count;
            //奇偶点计数器
            int count = 0;
            //构造射线
            MyPolyline line = new MyPolyline();
            line.Point1 = point;
            line.Point2 = new MyPoint(this.MINVALUE, point.Y);

            for (int i = 0; i < n; i++)
            {
                // 得到多边形的一条边
                MyPolyline side = polygon.Polylines[i];
                //如果在线上 则直接返回1
                if (JudgePointWithLine(point, side) == 1)
                {
                    return 1;
                }
                // 如果side平行x轴则不作考虑
                if (side.Point1.Y == side.Point2.Y)
                {
                    continue;
                }
                if (JudgePointWithLine(side.Point1, line) == 1)
                {
                    if (side.Point1.Y > side.Point2.Y) count++;
                }
                else if (JudgePointWithLine(side.Point2, line) == 1)
                {
                    if (side.Point2.Y > side.Point1.Y) count++;
                }
                else if (JudgeLineWithLine(line, side) == 1)
                {
                    count++;
                }
            }
            //奇数个数则在多边形内
            if (count % 2 == 1)
            {
                return 1;
            }
             //偶数个数在多边形外
            else
            {
                return 0;
            }
        }

        public MyPoint GetIntersectionPoint(MyPolyline linea, MyPolyline lineb)
        {
            MyPoint pointx1 = linea.Point1;
            MyPoint pointy1 = linea.Point2;
            MyPoint pointx2 = lineb.Point1;
            MyPoint pointy2 = lineb.Point2;
            /** 1 解线性方程组, 求线段交点. **/
            // 如果分母为0 则平行或共线, 不相交  
            float denominator = (pointy1.Y - pointx1.Y) * (pointy2.X - pointx2.X) - (pointx1.X - pointy1.X) * (pointx2.Y - pointy2.Y);
            if (denominator == 0)
            {
                return null;
            }
            // 线段所在直线的交点坐标 (x , y)      
            float x = ((pointy1.X - pointx1.X) * (pointy2.X - pointx2.X) * (pointx2.Y - pointx1.Y)
                        + (pointy1.Y - pointx1.Y) * (pointy2.X - pointx2.X) * pointx1.X
                        - (pointy2.Y - pointx2.Y) * (pointy1.X - pointx1.X) * pointx2.X) / denominator;
            float y = -((pointy1.Y - pointx1.Y) * (pointy2.Y - pointx2.Y) * (pointx2.X - pointx1.X)
                        + (pointy1.X - pointx1.X) * (pointy2.Y - pointx2.Y) * pointx1.Y
                        - (pointy2.X - pointx2.X) * (pointy1.Y - pointx1.Y) * pointx2.Y) / denominator;
            /** 2 判断交点是否在两条线段上 **/
            if (
                // 交点在线段1上  
                (x - pointx1.X) * (x - pointy1.X) <= 0 && (y - pointx1.Y) * (y - pointy1.Y) <= 0
                // 且交点也在线段2上  
                 && (x - pointx2.X) * (x - pointy2.X) <= 0 && (y - pointx2.Y) * (y - pointy2.Y) <= 0
            )
            {
                return new MyPoint((int)x, (int)y);
            }
            //否则不相交  
            return null;
        }
    }
}
