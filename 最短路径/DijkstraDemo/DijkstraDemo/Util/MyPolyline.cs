﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraDemo.Util
{
    /// <summary>
    /// 线
    /// </summary>
    class MyPolyline
    {
        public MyPoint Point1{get;set;}
        public MyPoint Point2 { get; set; }
        public float Distance { get; set; }
        public MyPolyline(MyPoint point1,MyPoint point2)
        {
            this.Point1 = point1;
            this.Point2 = point2;
            int xLen = Math.Abs(point1.X - point2.X);
            int yLen = Math.Abs(point1.Y - point2.Y);
            this.Distance = (float)Math.Pow((double)(xLen * xLen + yLen * yLen),0.5);
        }
    }
}
