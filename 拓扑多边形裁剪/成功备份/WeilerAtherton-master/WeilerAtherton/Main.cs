using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeilerAtherton
{
    /* TEST CASES
        0,0/100,100/200,100/200,200/300,300/100,200//150,50/300,100/175,250/150,250
        0,0/100,100/200,100/200,200/300,300/100,200//0,0/100,100/200,100/200,200/300,300/100,200
      
        100,300/400,300/400,600//300,100/600,300/400,600  (1)
        100,300/400,300/400,600//300,100/400,300/400,600  (2)
        100,300/400,300/400,600//100,300/400,300/400,600  (3)
        100,100/400,100/400,400//100,300/400,100/200,400  (4)
        100,200/400,200/400,500//300,300/400,200/400,500  (5)
        100,200/400,200/400,500//100,200/500,100/500,800  (6)
        100,200/400,200/400,400//100,200/200,400/100,400  (7)
        100,200/300,200/200,400//300,200/400,400/200,400  (8)
      
        0,0/100,0/50,100//0,100/50,0/100,100
     */

    public partial class Main : Form
    {
        System.Drawing.Graphics g;
        Pen pen;
        public Main()
        {
            InitializeComponent();
            g = this.CreateGraphics();
            pen = new Pen(Color.Red, 2);
            txtInput.Text = "100,100/200,100/200,200/100,200//150,50/175,50/175,250/150,250";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            string[] lists = txtInput.Text.Split(new string[] { "//" }, StringSplitOptions.None);

            string[] first = lists[0].Split('/');
            string[] second = lists[1].Split('/');

            PointF[] clip = convert(first);
            PointF[] shape = convert(second);

            if(clip == null || shape == null) return;

            DrawLines(clip, Color.Red);
            DrawLines(shape, Color.Blue);

            doClip(clip, shape);
        }

        private void DrawLines(PointF[] points, Color color)
        {
            pen.Color = color;
            for (int i = 0; i < points.Length; i++)
            {
                PointF p1 = points[i];
                PointF p2 = points[(i + 1) % points.Length];
                g.DrawLine(pen, p1, p2);
            }
        }

        private PointF[] convert(string[] text)
        {
            PointF[] points = new PointF[text.Length];

            for(int i = 0; i < text.Length; i++)
            {
                string[] split = text[i].Split(',');
                float x;
                float y;
                if(!float.TryParse(split[0], out x)) return null;
                if (!float.TryParse(split[1], out y)) return null;

                points[i] = new PointF(x, y);
            }

            return points;
        }

        private void doClip(PointF[] clip, PointF[] shape)
        {
            List<DeepPoint> deepShape = new List<DeepPoint>(Array.ConvertAll(shape, p => new DeepPoint(p, DeepPoint.PointType.Normal, p.InOrOut(clip))));
            List<DeepPoint> deepClip = new List<DeepPoint>(Array.ConvertAll(clip, p => new DeepPoint(p, DeepPoint.PointType.Normal, p.InOrOut(shape))));

            for (int i = 0; i < deepShape.Count; i++)
            {
                DeepPoint p1 = deepShape[i];
                DeepPoint p2 = deepShape.NextAfter(i);

                //check for intersections
                for (int j = 0; j < deepClip.Count; j++)
                {
                    DeepPoint c1 = deepClip[j];
                    DeepPoint c2 = deepClip.NextAfter(j);

                    List<PointF> interOutput;
                    if (Line.Intersection(p1, p2, c1, c2, out interOutput))
                    {
                        foreach (PointF inter in interOutput)
                        {
                            //This ensures that we have the same intersection added to both (avoid precision errors)
                            DeepPoint intersection = new DeepPoint(inter, DeepPoint.PointType.Intersection, DeepPoint.PointStatus.Undetermined);

                            if (inter == p1.p || inter == p2.p || inter == c1.p || inter == c2.p)
                            {
                                if (inter == p1.p && inter == c1.p)
                                {
                                    p1.overlap = true;
                                    c1.overlap = true;
                                    p1.type = DeepPoint.PointType.Intersection;
                                    c1.type = DeepPoint.PointType.Intersection;
                                    continue;
                                }
                                else if (inter != p2.p && inter != c1.p && inter != c2.p)
                                {
                                    if (inter == p1.p)
                                    {
                                        p1.overlap = true;
                                        p1.type = DeepPoint.PointType.Intersection;
                                    }
                                    else p1.intersections.Add(intersection);

                                    c1.intersections.Add(intersection);
                                }
                            }
                            else
                            {
                                //TODO: if intersection is same as p1,p2,c1,c2 -> make a note of it?
                                p1.intersections.Add(intersection);
                                c1.intersections.Add(intersection);
                            }
                        }
                    }
                }

                //sort intersections by distance to p1
                p1.SortIntersections();

                
                //loop through intersections between p1 and p2
                for(int j = 0; j < p1.intersections.Count; j++)
                {
                    DeepPoint intersection = p1.intersections[j];

                    //if there's a previous intersection
                    if (j > 0)
                    {
                        DeepPoint prev = p1.intersections[j - 1];
                        if (prev.status == DeepPoint.PointStatus.In) intersection.status = DeepPoint.PointStatus.Out; //set inter pStatus to Out
                        else intersection.status = DeepPoint.PointStatus.In; //set inter as In
                    }
                    else if (p1.status == DeepPoint.PointStatus.In) intersection.status = DeepPoint.PointStatus.Out; //set inter as Out
                    else intersection.status = DeepPoint.PointStatus.In; //set inter as In
                }
            }

            //sort all intersections in clip
            for(int i = 0; i < deepClip.Count; i++)
            {
                deepClip[i].SortIntersections();
            }

            IntegrateIntersections(ref deepShape);
            IntegrateIntersections(ref deepClip);

            //Use these to jump from list to list
            Dictionary<PointF, int> shapeIntersectionToClipIndex = new Dictionary<PointF, int>();
            Dictionary<PointF, int> clipIntersectionToShapeIndex = new Dictionary<PointF, int>();
            BuildIntersectionMap(ref shapeIntersectionToClipIndex, deepShape, ref clipIntersectionToShapeIndex, deepClip);

            //start from entering points
            List<int> iEntering = new List<int>();

            //Get entering intersections
            for (int i = 0; i < deepShape.Count; i++)
            {
                DeepPoint point = deepShape[i];
                if(point.overlap || (point.type == DeepPoint.PointType.Intersection && point.status == DeepPoint.PointStatus.In))
                    iEntering.Add(i);
            }

            List<List<DeepPoint>> output = new List<List<DeepPoint>>();
            List<DeepPoint> currentShape = new List<DeepPoint>();

            bool allEnteringAreOverlap = true;
            foreach (int i in iEntering)
            {
                if(!deepShape[i].overlap)
                {
                    allEnteringAreOverlap = false;
                    break;
                }
            }

            bool hasNonOverlapIntersections = false;

            foreach (DeepPoint p in deepShape)
            {
                if (!p.overlap && p.type == DeepPoint.PointType.Intersection)
                {
                    hasNonOverlapIntersections = true;
                    break;
                }
            }

            //handle special cases
            if ((iEntering.Count == 0 || allEnteringAreOverlap) && !hasNonOverlapIntersections)
            {
                bool allInside = true;
                foreach(DeepPoint p in deepShape)
                {
                    if (p.status != DeepPoint.PointStatus.In && !p.overlap)
                    {
                        allInside = false;
                        break;
                    }
                }

                if(allInside)
                {
                    foreach (DeepPoint p in deepShape)
                    {
                        currentShape.Add(p);
                    }
                }
                else
                {
                    //check that deepClip are all inside
                    allInside = true;
                    foreach (DeepPoint p in deepClip)
                    {
                        if (p.status != DeepPoint.PointStatus.In && !p.overlap)
                        {
                            allInside = false;
                            break;
                        }
                    }

                    if (allInside)
                    {
                        foreach (DeepPoint p in deepClip)
                        {
                            currentShape.Add(p);
                        }
                    }
                    else return;
                }

                output.Add(currentShape);
                pen.Width = 5;
                DrawLines(Array.ConvertAll(output[0].ToArray(), p => p.p), Color.Green);
                pen.Width = 2;
                return;
            }

            //TODO: add method to ignore entering points that were included in an output shape already

            //go through all of our entering points
            for (int mainCount = 0; mainCount < iEntering.Count; mainCount++)
            {
                int goToIndex = iEntering[mainCount];

                bool complete = false;

                while (!complete)
                {
                    //loop through all shape points starting at goToIndex
                    for (int iCount = goToIndex; iCount < deepShape.Count + goToIndex; iCount++)
                    {
                        int i = iCount % deepShape.Count;
                        DeepPoint p1 = deepShape[i];
                        DeepPoint p2 = deepShape.NextAfter(i);

                        if (p1.overlap)
                        {
                            DeepPoint prev = deepShape.PrevBefore(i);
                            if (prev.overlap || prev.status == DeepPoint.PointStatus.Out)
                                p1.tempStatus = DeepPoint.PointStatus.In;
                            else p1.tempStatus = DeepPoint.PointStatus.Out;
                        }
                        else p1.tempStatus = p1.status;

                        if (p2.overlap)
                        {
                            if (p1.overlap || p1.status == DeepPoint.PointStatus.Out)
                                p2.tempStatus = DeepPoint.PointStatus.In;
                            else p2.tempStatus = DeepPoint.PointStatus.Out;
                        }
                        else p2.tempStatus = p2.status;

                        if (p1.type == DeepPoint.PointType.Normal)
                        {
                            if (p1.tempStatus == DeepPoint.PointStatus.In)
                            {
                                //break when we get back to start
                                if (currentShape.Count > 0 && currentShape[0].p == p1.p)
                                {
                                    complete = true;
                                    break;
                                }

                                currentShape.Add(p1);

                                //point2 must be heading outwards
                                if (p2.type == DeepPoint.PointType.Intersection)
                                {
                                    //go to clipPoints loop and start from intersection
                                    //goToIndex = shapeIntersectionToClipIndex[p2.p] + 1;
                                    //break;
                                }
                            }
                            //we don't care about point2 here
                            //if point1 is an outside normal point,
                            //	then point2 must either be an outside normal point OR an intersection going inwards.
                            //		The former doesn't not need to be handled, the latter will be handled upon looping

                        }
                        else //p1 is an intersection
                        {
                            //break when we get back to start
                            if (currentShape.Count > 0 && currentShape[0].p == p1.p)
                            {
                                complete = true;
                                break;
                            }

                            //we must add point 1 since it's on the border
                            currentShape.Add(p1);

                            //exiting
                            if (p1.tempStatus == DeepPoint.PointStatus.Out)
                            {
                                //go to clipPoints loop and start from after intersection;
                                goToIndex = (shapeIntersectionToClipIndex[p1.p] + 1) % deepClip.Count;
                                break;
                            }
                        }
                    } //end deepShape for

                    //break while loop if complete
                    if (complete) break;

                    //loop through all clip points starting at goToIndex
                    //we should only get here from a go to from shapePoints
                    for (int iCount = goToIndex; iCount < deepClip.Count + goToIndex; iCount++)
                    {
                        int i = iCount % deepClip.Count;
                        DeepPoint p1 = deepClip[i];
                        DeepPoint p2 = deepClip.NextAfter(i);

                        if (p1.overlap)
                        {
                            DeepPoint prev = deepClip.PrevBefore(i);
                            if (prev.overlap || prev.status == DeepPoint.PointStatus.Out)
                                p1.tempStatus = DeepPoint.PointStatus.In;
                            else p1.tempStatus = DeepPoint.PointStatus.Out;
                        }
                        else p1.tempStatus = p1.status;

                        if (p2.overlap)
                        {
                            if (p1.overlap || p1.status == DeepPoint.PointStatus.Out)
                                p2.tempStatus = DeepPoint.PointStatus.In;
                            else p2.tempStatus = DeepPoint.PointStatus.Out;
                        }
                        else p2.tempStatus = p2.status;

                        if (p1.type == DeepPoint.PointType.Intersection)
                        {
                            //break when we get back to start
                            if (currentShape.Count > 0 && currentShape[0].p == p1.p)
                            {
                                complete = true;
                                break;
                            }

                            //we must add point 1 since it's on the border
                            currentShape.Add(p1);

                            //if it was going inwards
                            if (p1.tempStatus == DeepPoint.PointStatus.In)
                            {
                                //go to shapePoints loop and start from after point1
                                goToIndex = (clipIntersectionToShapeIndex[p1.p] + 1) % deepShape.Count;
                                break;
                            }
                        }
                        else //p1 is normal
                        {
                            if (p1.tempStatus == DeepPoint.PointStatus.In)
                            {
                                //break when we get back to start
                                if (currentShape.Count > 0 && currentShape[0].p == p1.p)
                                {
                                    complete = true;
                                    break;
                                }

                                //we must add point 1 since it's on the border
                                currentShape.Add(p1);
                            }
                        }
                    } //end deepClip for
                }//end while loop

                output.Add(currentShape);
                currentShape = new List<DeepPoint>();
            }//end main for loop
            
            //remove duplicate points
            for(int iOutput = 0; iOutput < output.Count; iOutput++)
            {
                List<DeepPoint> points = output[iOutput];
                for (int i = 0; i < points.Count; i++)
                {
                    //remove duplicates
                    if (points[i].p == points.NextAfter(i).p)
                    {
                        //remove current
                        points.RemoveAt(i);
                        i--;
                    }
                }

                if (points.Count < 3)
                {
                    output.Remove(points);
                    iOutput--;
                }
            }

            pen.Width = 5;
            DrawLines(Array.ConvertAll(output[0].ToArray(), p => p.p), Color.Green);
            pen.Width = 2;
        } //end doClip

        //TODO: test this
        private void IntegrateIntersections(ref List<DeepPoint> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                DeepPoint normal = list[i];
                for (int j = 0; j < normal.intersections.Count; j++)
                {
                    DeepPoint intersection = normal.intersections[j];

                    list.Insert(i+1, intersection);
                    i++;
                }
            }
        }

        //TODO: test
        private void BuildIntersectionMap(ref Dictionary<PointF, int> fromMap, List<DeepPoint> from, ref Dictionary<PointF, int> toMap, List<DeepPoint> to)
        {
            for (int i = 0; i < from.Count; i++)
            {
                DeepPoint point = from[i];

                if (point.type == DeepPoint.PointType.Intersection)
                {
                    //we can do both at once since we should have the same number of intersections

                    for (int j = 0; j < to.Count; j++)
                    {
                        if(to[j].p == point.p) fromMap.Add(point.p, j);
                    }
                    toMap.Add(point.p, i);
                }
            }
        }
    }
}
