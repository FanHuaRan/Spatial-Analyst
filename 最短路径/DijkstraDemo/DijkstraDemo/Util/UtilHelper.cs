using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraDemo.Util
{
    /// <summary>
    ///辅助帮助类
    /// </summary>
    class UtilHelper
    {
        //构造邻接矩阵
        public static List<List<float>> GetDistances(List<MyPoint> points,List<MyPolyline> polylines)
        {
             int n = points.Count;
            List<List<float>> distances = new List<List<float>>(n);
            for(int i=0;i<n;i++)
            {
                distances.Add(new List<float>(n));
            }
            for(int i=0;i<n;i++)
            {
                for(int j=0;j<n;j++)
                {
                     distances[i].Add(100000000);
                }
            }
            for (int i = 0; i < polylines.Count; i++)
            {
                MyPolyline polyline=polylines[i];
                MyPoint point1 = polyline.Point1;
                MyPoint point2 = polyline.Point2;
                int index1 = queryIndex(points, point1);
                int index2 = queryIndex(points, point2);
                distances[index1][index2] = polyline.Distance;
                distances[index2][index1] = polyline.Distance;
            }
          return distances;
        }
        //查找索引
        public static int queryIndex(List<MyPoint> points,MyPoint targetPoint)
        {
            int index = 0;
            for(int i=0;i<points.Count;i++)
            {
                MyPoint point=points[i];
                if(point==targetPoint)
                {
                    index=i;
                }
            }
            return index;
        }

    }
}
