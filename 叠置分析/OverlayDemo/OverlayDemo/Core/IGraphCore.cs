using OverlayDemo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayDemo.Core
{
    /// <summary>
    /// 基本空间分析操作接口
    /// </summary>
    interface IGraphCore
    {
        /// <summary>
        /// 点与线的空间关系判断
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polyLine"></param>
        /// <returns>1代表在线上 2代表在线段延长线上 3在线段右方 4在线段左方</returns>
        int JudgePointWithLine(MyPoint point, MyPolyline polyLine);
        /// <summary>
        /// 线与线的空间是否相交判断
        /// </summary>
        /// <param name="linea"></param>
        /// <param name="lineB"></param>
        /// <returns></returns>
        int JudgeLineWithLine(MyPolyline linea, MyPolyline lineB);
        /// <summary>
        /// 点与多边形关系判断
        /// </summary>
        /// <param name="point"></param>
        /// <param name="polygon"></param>
        /// <returns>1在线上 2在多边形内 0在多边形外</returns>
        int JudgePointWithPolygon(MyPoint point, MyPolygon polygon);
        /// <summary>
        /// 求交点
        /// </summary>
        /// <param name="linea"></param>
        /// <param name="lineb"></param>
        /// <returns></returns>
        MyPoint GetIntersectionPoint(MyPolyline linea, MyPolyline lineb);
    }
}
