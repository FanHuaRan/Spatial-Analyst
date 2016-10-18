using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WeilerAtherton
{
    public class DeepPoint
    {
        public PointF p;
        public PointType type;
        public PointStatus status;
        public bool overlap = false;
        public PointStatus tempStatus;

        public List<DeepPoint> intersections;

        public enum PointType
        {
            Normal,
            Intersection
        }

        public enum PointStatus
        {
            In,
            Out,
            Undetermined
        }

        public DeepPoint() { }

        public DeepPoint(PointF point, PointType pType, PointStatus pStatus)
        {
            p = point;

            type = pType;
            status = pStatus;

            intersections = new List<DeepPoint>();
        }

        public void SortIntersections()
        {
            if (intersections.Count <= 1) return;

            //sort by closest to point = first
            intersections.Sort((p1, p2) =>
            {
                
                float d1 = DistanceSquared(p1, this);
                float d2 = DistanceSquared(p2, this);
                if (d1 < d2) return -1;
                else if (d1 > d2) return 1;
                else return 0;
            });
        }

        //Distance Squared
        public static float DistanceSquared(DeepPoint p1, DeepPoint p2)
        {
            return (p2.p.X - p1.p.X) * (p2.p.X - p1.p.X) + (p2.p.Y - p1.p.Y) * (p2.p.Y - p1.p.Y);
        }

        //Distance
        public static float Distance(PointF p1, PointF p2)
        {
            return (float)Math.Sqrt((p2.X - p1.X) * (p2.X - p1.X) + (p2.Y - p1.Y) * (p2.Y - p1.Y));
        }
    }
    
   public static class DeepPointExtensions
    {
        //extension for float
        public static int Sign(this float f)
        {
            if (f >= 0) return 1;
            else return -1;
        }
    }
}
