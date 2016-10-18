using OverlayDemo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayDemo.Core
{
    interface IGraphCore
    {
        //点与线的空间关系判断
        int JudgePointWithLine(MyPoint point, MyPolyline polyLine);
        //线与线的空间是否相交判断
        int JudgeLineWithLine(MyPolyline linea, MyPolyline lineB);
        //点与多边形关系判断
        int JudgePointWithPolygon(MyPoint point, MyPolygon polygon);
        //求交点
        MyPoint GetIntersectionPoint(MyPolyline linea, MyPolyline lineb);
    }
}
