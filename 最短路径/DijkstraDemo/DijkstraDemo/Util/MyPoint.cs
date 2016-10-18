using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraDemo.Util
{
    class MyPoint
    {
        public int X { get; set; }

        public int Y { get; set; }

        public MyPoint(int x,int y)
        {
            this.X = x;
            this.Y = y;
        }
        //寻找点
        public static MyPoint LookForPoint(List<MyPoint> points, int x, int y, int rolation)
        {
            MyPoint point=null;
            if (points != null)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    MyPoint tempPoint = points[i];
                    int xLenth = Math.Abs(tempPoint.X - x);
                    int yLenth = Math.Abs(tempPoint.Y - y);
                    int distance = xLenth * xLenth + yLenth * yLenth;
                    if (rolation * rolation > distance)
                    {
                        point = tempPoint;
                        break;
                    }
                }
            }
           return point;
        }
    }
}
