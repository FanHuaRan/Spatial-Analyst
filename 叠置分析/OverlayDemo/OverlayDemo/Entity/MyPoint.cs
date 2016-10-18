using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayDemo.Entity
{
    class MyPoint
    {
        public int X { get; set; }

        public int Y { get; set; }
        //点标志 0或者不赋值为普通点
        //1为待判断出入的交点 2为入点 3为出点
        public int PointFlag { get; set; }
        public MyPoint(int x,int y)
        {
            this.X = x;
            this.Y = y;
            this.PointFlag = 0;
        }
        public MyPoint(int x,int y,int flag)
        {
            this.X = x;
            this.Y = y;
            this.PointFlag = flag;
        }
    }
}
