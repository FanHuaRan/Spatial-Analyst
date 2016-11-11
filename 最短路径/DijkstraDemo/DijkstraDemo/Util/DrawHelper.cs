using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraDemo.Util
{
    /// <summary>
    /// 绘图帮助类
    /// </summary>
    class DrawHelper
    {
        private static Pen myPen = new Pen(Color.Red,1);
        private static Pen lastPen = new Pen(Color.Cyan,2);
        public static void DrawPoint(Graphics grahic, int x, int y, int num)
        {
            grahic.DrawEllipse(myPen, x, y, 5, 5);
            grahic.DrawString(num.ToString(), new Font(FontFamily.GenericSerif, 8), Brushes.Red, x+7, y+7);
        }
        public static void DrawLine(Graphics grahic,int x1,int y1,int x2,int y2)
        {
            grahic.DrawLine(myPen, x1,y1,x2,y2);
        }
        public static void DrawPath(Graphics grahic, List<Point> points)
        {
            for(int i=0;i<points.Count-1;i++)
            {
                  grahic.DrawLine(lastPen,points[i],points[i+1]);
            }
        }
    }
}
