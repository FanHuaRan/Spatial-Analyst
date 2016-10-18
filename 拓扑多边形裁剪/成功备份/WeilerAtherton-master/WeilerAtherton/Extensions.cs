using System;
using System.Collections.Generic;
using System.Drawing;

namespace WeilerAtherton
{
    public static class Extensions
    {
        public static DeepPoint.PointStatus InOrOut(this PointF point, PointF[] shape)
        {
            //a point that we can guarantee is outside of our shape
            PointF outside = new PointF(float.MaxValue, float.MaxValue);

            //find the leftmost and topmost bounds
            for (int i = 0; i < shape.Length; i++)
            {
                PointF p = shape[i];

                if (p.X < outside.X) outside.X = p.X;
                if (p.Y < outside.Y) outside.Y = p.Y;
            }

            outside.X-=7;
            outside.Y-=13;

            HashSet<PointF> intersections = new HashSet<PointF>();

            int intersectionCount = 0;
            for (int i = 0; i < shape.Length; i++)
            {                
                PointF c1 = shape[i];
                PointF c2 = shape[(i + 1) % shape.Length];

                //if our point is the same as one of the shape points, we consider it inside the shape
                //if(point == c1 || point == c2) return DeepPoint.PointStatus.In;

                Console.WriteLine("{0} == {1}", DeepPoint.Distance(point, c1) + DeepPoint.Distance(point, c2), DeepPoint.Distance(c1, c2));
                if (DeepPoint.Distance(point, c1) + DeepPoint.Distance(point, c2) == DeepPoint.Distance(c1, c2)) return DeepPoint.PointStatus.In; //TODO: mark it as border

                
                float det;
                float A1, B1, C1;
                float A2, B2, C2;

                //https://www.topcoder.com/community/data-science/data-science-tutorials/geometry-concepts-line-intersection-and-its-applications/#line_line_intersection
                A1 = c2.Y - c1.Y;
                B1 = c1.X - c2.X;
                C1 = A1 * c1.X + B1 * c1.Y;

                A2 = outside.Y - point.Y;
                B2 = point.X - outside.X;
                C2 = A2 * point.X + B2 * point.Y;

                det = A1 * B2 - A2 * B1;
                if (det == 0)
                {
                    throw new Exception("PARALLEL");
                }
              
                PointF intersect = new PointF(B2 * C1 - B1 * C2, A1 * C2 - A2 * C1); //would normally be /det
                intersect.X /= det;
                intersect.Y /= det;

                //using multiplication instead since it's less likely to have precision errors
                float xMin = Math.Min(c1.X, c2.X);
                float xMax = Math.Max(c1.X, c2.X);

                float yMin = Math.Min(c1.Y, c2.Y);
                float yMax = Math.Max(c1.Y, c2.Y);

                float xMin2 = Math.Min(point.X, outside.X);
                float xMax2 = Math.Max(point.X, outside.X);

                float yMin2 = Math.Min(point.Y, outside.Y);
                float yMax2 = Math.Max(point.Y, outside.Y);

                if (xMin <= intersect.X && intersect.X <= xMax
                    && yMin <= intersect.Y && intersect.Y <= yMax
                    && xMin2 <= intersect.X && intersect.X <= xMax2
                    && yMin2 <= intersect.Y && intersect.Y <= yMax2
                    && !intersections.Contains(intersect))
                {
                    intersectionCount++;
                    intersections.Add(intersect);
                }
            }

            //covers 0 + evens
            if (intersectionCount % 2 == 0) return DeepPoint.PointStatus.Out;
            else return DeepPoint.PointStatus.In; //odds > 0
        }

        public static T NextAfter<T>(this List<T> list, int i)
        {
            return list[(i + 1) % list.Count];
        }

        public static int IndexAfter<T>(this List<T> list, int i)
        {
            return (i + 1) % list.Count;
        }

        public static T PrevBefore<T>(this List<T> list, int i)
        {
            return list[((i - 1) + list.Count) % list.Count];
        }

        public static int PrevIndex<T>(this List<T> list, int i)
        {
            return ((i - 1) + list.Count) % list.Count;
        }
    }
}
