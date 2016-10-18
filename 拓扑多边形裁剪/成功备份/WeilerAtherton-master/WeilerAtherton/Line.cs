using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeilerAtherton
{
    static class Line
    {
        public static bool Intersection(DeepPoint start1, DeepPoint end1, DeepPoint start2, DeepPoint end2, out List<PointF> output)
        {
            output = new List<PointF>();
            float det;
            float A1, B1, C1;
            float A2, B2, C2;

            //https://www.topcoder.com/community/data-science/data-science-tutorials/geometry-concepts-line-intersection-and-its-applications/#line_line_intersection
            A1 = end1.p.Y - start1.p.Y;
            B1 = start1.p.X - end1.p.X;
            C1 = A1 * start1.p.X + B1 * start1.p.Y;

            A2 = end2.p.Y - start2.p.Y;
            B2 = start2.p.X - end2.p.X;
            C2 = A2 * start2.p.X + B2 * start2.p.Y;

            det = A1 * B2 - A2 * B1;

            if(det == 0)
            {
                float mTop = end1.p.Y - start1.p.Y;
                float mBot = end1.p.X - start1.p.X;

                float b1 = end1.p.Y - (mTop * end1.p.X) / mBot;
                float b2 = end2.p.Y - (mTop * end2.p.X) / mBot;

                if (b1 != b2) return false;

                bool overlap = false;

                //there will possibly be two points
                float cxMin = Math.Min(start2.p.X, end2.p.X);
                float cxMax = Math.Max(start2.p.X, end2.p.X);

                float cyMin = Math.Min(start2.p.Y, end2.p.Y);
                float cyMax = Math.Max(start2.p.Y, end2.p.Y);

                if (cxMin <= start1.p.X && start1.p.X <= cxMax
                && cyMin <= start1.p.Y && start1.p.Y <= cyMax)
                {
                    //use start1
                    output.Add(start1.p);
                    overlap = true;
                }
                else
                {
                    //use start2
                    output.Add(start2.p);
                }

                if (cxMin <= end1.p.X && end1.p.X <= cxMax
                    && cyMin <= end1.p.Y && end1.p.Y <= cyMax)
                {
                    //use end1
                    output.Add(end1.p);
                    overlap = true;
                }
                else
                {
                    //use end2
                    output.Add(end2.p);
                }

                //lines are parallel and possibly overlap
                Console.WriteLine("Parallel Lines");
                return overlap;
            }
              
            PointF intersect = new PointF(B2 * C1 - B1 * C2, A1 * C2 - A2 * C1); //would normally be /det

            intersect.X /= det;
            intersect.Y /= det;
            //using multiplication instead since it's less likely to have precision errors
            float xMin = Math.Min(start2.p.X, end2.p.X);
            float xMax = Math.Max(start2.p.X, end2.p.X);

            float yMin = Math.Min(start2.p.Y, end2.p.Y);
            float yMax = Math.Max(start2.p.Y, end2.p.Y);

            float xMin2 = Math.Min(start1.p.X, end1.p.X);
            float xMax2 = Math.Max(start1.p.X, end1.p.X);

            float yMin2 = Math.Min(start1.p.Y, end1.p.Y);
            float yMax2 = Math.Max(start1.p.Y, end1.p.Y);

            if (xMin <= intersect.X && intersect.X <= xMax
                && yMin <= intersect.Y && intersect.Y <= yMax
                && xMin2 <= intersect.X && intersect.X <= xMax2
                && yMin2 <= intersect.Y && intersect.Y <= yMax2)
            {
                output.Add(intersect);
                return true;
            }
            else return false;
        }
    }
}
