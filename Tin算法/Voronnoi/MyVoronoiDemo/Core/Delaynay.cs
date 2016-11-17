using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MrFan.Tool.Delaunry
{
    public class Delaynay : ITinVoronoi
    {
        public DataStruct DS
        {
            get { return this.m_DS; }
        }
        //总的数据结构成员
        private  DataStruct m_DS = new DataStruct();
        //凸壳顶点索引链表
        public List<long> HullPoint; 
        //用于计算凸壳的临时辅助结构
        private struct PntV_ID
        {
            
            public long Value;
            public long ID;
        }

        //建立凸壳
        public void CreateConvex()
        {
            //初始化凸壳顶点链表
            if (HullPoint == null)
                HullPoint = new List<long>();
            else
            {
                //去除顶点的凸壳标记
                for (int i = 0; i < HullPoint.Count; i++)
                {
                    DS.Vertex[HullPoint[i]].isHullEdge = 0; 
                }
                HullPoint.Clear();
            }

            #region 计算x-y和x+y的最大、最小点、得出一个初步大型包围壳
            PntV_ID MaxMinus, MinMinus, MaxAdd, MinAdd;
            //用第一点初始化
            MaxMinus.ID = MinMinus.ID = MaxAdd.ID = MinAdd.ID = DS.Vertex[0].ID;
            MaxMinus.Value = MinMinus.Value = DS.Vertex[0].x - DS.Vertex[0].y;
            MaxAdd.Value = MinAdd.Value = DS.Vertex[0].x + DS.Vertex[0].y;

            long temp;
            for (int i = 1; i < DS.VerticesNum; i++)
            {
                temp = DS.Vertex[i].x - DS.Vertex[i].y;
                if (temp > MaxMinus.Value)
                {
                    MaxMinus.Value = temp;
                    MaxMinus.ID = DS.Vertex[i].ID;
                }
                if (temp < MinMinus.Value)
                {
                    MinMinus.Value = temp;
                    MinMinus.ID = DS.Vertex[i].ID;
                }

                temp = DS.Vertex[i].x + DS.Vertex[i].y;
                if (temp > MaxAdd.Value)
                {
                    MaxAdd.Value = temp;
                    MaxAdd.ID = DS.Vertex[i].ID;
                }
                if (temp < MinAdd.Value)
                {
                    MinAdd.Value = temp;
                    MinAdd.ID = DS.Vertex[i].ID;
                }
            }
            #endregion

            //加入链表
            HullPoint.Add(MinMinus.ID);
            HullPoint.Add(MaxAdd.ID);
            HullPoint.Add(MaxMinus.ID);
            HullPoint.Add(MinAdd.ID);
            //去除重复点
            for (int i = 0; i < HullPoint.Count; i++)
            {
                if (HullPoint[i] == HullPoint[(i + 1) % HullPoint.Count])
                    HullPoint.RemoveAt(i);
            }

            //将凸壳点标记，下一步不再处理
            for (int i = 0; i < HullPoint.Count; i++)
            {
                DS.Vertex[HullPoint[i]].isHullEdge = -1;
            }
            #region 插入其他点
            for (int i = 0; i < DS.VerticesNum; i++)
            {
                //是凸壳点则直接跳过
                if (DS.Vertex[i].isHullEdge == -1)
                {
                    continue;
                }
                //判断该点与每条凸壳边的关系
                double isOnRight;
                for (int j = 0; j < HullPoint.Count; j++)
                {
                    PointF pnt1 = new PointF(Convert.ToSingle(DS.Vertex[HullPoint[j]].x),
                            Convert.ToSingle(DS.Vertex[HullPoint[j]].y));
                    PointF pnt2 = new PointF(Convert.ToSingle(DS.Vertex[HullPoint[(j + 1) % HullPoint.Count]].x),
                            Convert.ToSingle(DS.Vertex[HullPoint[(j + 1) % HullPoint.Count]].y));
                    PointF pnt3 = new PointF(Convert.ToSingle(DS.Vertex[i].x), Convert.ToSingle(DS.Vertex[i].y));
                    // >0则位于外侧(设备坐标系，逆时针)
                    isOnRight = VectorXMultiply(pnt1, pnt2, pnt3);

                    //如果点在边外侧则修改凸壳
                    if (isOnRight >= 0)
                    {
                        //点插入凸边链表
                        HullPoint.Insert((j + 1) % (HullPoint.Count + 1), DS.Vertex[i].ID);

                        //判断添加点后是否出现内凹
                        pnt1 = new PointF(Convert.ToSingle(DS.Vertex[HullPoint[(j + 3) % HullPoint.Count]].x),
                            Convert.ToSingle(DS.Vertex[HullPoint[(j + 3) % HullPoint.Count]].y));
                        isOnRight = VectorXMultiply(pnt3, pnt2, pnt1);
                        if (isOnRight > 0)    //删除内凹点p2(即v[j+2])
                        {
                            int index = (j + 2) % HullPoint.Count;    //点在凸壳链表中的索引
                            DS.Vertex[HullPoint[index]].isHullEdge = 0;
                            HullPoint.RemoveAt(index);
                        }

                        break;
                    }
                }
            }
            #endregion

            //将顶点的凸壳标记设为1
            for (int i = 0; i < HullPoint.Count; i++)
                DS.Vertex[HullPoint[i]].isHullEdge = 1;
        }

        //凸壳三角剖分
        private void HullTriangulation()
        {
            DS.TriangleNum = 0;

            //凸壳为点或线的情况
            //多点共边的情况

            //复制凸壳顶点
            List<long> points = new List<long>();
            for (int i = 0; i < HullPoint.Count; i++)
                points.Add(HullPoint[i]);

            //构网
            long id1, id2, id3;
            while (points.Count >= 3)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    //△为干净的则加入网中
                    id1 = points[i];
                    id2 = points[(i + 1) % points.Count];
                    id3 = points[(i + 2) % points.Count];
                    if (IsClean(id1, id2, id3))
                    {
                        DS.Triangle[DS.TriangleNum].V1Index = id1;
                        DS.Triangle[DS.TriangleNum].V2Index = id2;
                        DS.Triangle[DS.TriangleNum].V3Index = id3;
                        DS.TriangleNum++;
                        DS.Vertex[id2].isHullEdge = 2;  //标记已构网点
                        points.Remove(id2);

                        break;
                    }
                }
            }
        }

        //判断三角形外接圆中不含其他凸壳顶点
        private bool IsClean(long p1ID, long p2ID, long p3ID)
        {
            for (int i = 0; i < HullPoint.Count; i++)
            {
                //跳过已构网的点和△顶点
                if (DS.Vertex[HullPoint[i]].isHullEdge == 2 || HullPoint[i] == p1ID || HullPoint[i] == p2ID || HullPoint[i] == p3ID)
                    continue;

                //如果点i位于△外接圆内
                if (InTriangleExtCircle(DS.Vertex[HullPoint[i]].x, DS.Vertex[HullPoint[i]].y, DS.Vertex[p1ID].x,
                    DS.Vertex[p1ID].y, DS.Vertex[p2ID].x, DS.Vertex[p2ID].y, DS.Vertex[p3ID].x, DS.Vertex[p3ID].y))
                    return false;
            }

            return true;
        }
       
        //构建并显示Voronoi图
        //基础建立在Delaunry三角网之上
        //不断连接
        public void CreateVoronoi(Graphics g)
        {
            //获取每条
            for (int i = 0; i < DS.TinEdgeNum; i++)
            {
                if (!DS.TinEdges[i].NotHullEdge) //△边为凸壳边
                {
                    DrawHullVorEdge(i, g);
                    continue;
                }

                //连接左/右△的外接圆心
                //获取边的左右三角形
                //再进行连接
                long index1 = DS.TinEdges[i].AdjTriangle1ID;
                long index2 = DS.TinEdges[i].AdjTriangle2ID;
                PointF p1 = new PointF(Convert.ToSingle(DS.Barycenters[index1].X), Convert.ToSingle(DS.Barycenters[index1].Y));
                PointF p2 = new PointF(Convert.ToSingle(DS.Barycenters[index2].X), Convert.ToSingle(DS.Barycenters[index2].Y));
                //圆心在box外则直接跳过
                //if (PointInBox(p1) && PointInBox(p2))
                    g.DrawLine(new Pen(Color.Black, 1), p1, p2);
            }

        }

        //增量法生成Delaunay三角网
        public void CreateTIN()
        {
            //建立凸壳并三角剖分
            CreateConvex();
            HullTriangulation();

            //逐点插入
            PlugInEveryVertex();

            //建立边的拓扑结构
            TopologizeEdge();    
        }

        //逐点加入修改TIN
        private void PlugInEveryVertex()
        {
            Edge[] EdgesBuf = new Edge[DataStruct.MaxTriangles];  //△边缓冲区

            bool IsInCircle;
            int i, j, k;
            int EdgeCount;
            for (i = 0; i < DS.VerticesNum; i++)    //逐点加入
            {
                //跳过凸壳顶点
                if (DS.Vertex[i].isHullEdge != 0)
                    continue;

                EdgeCount = 0;            
                for (j = 0; j < DS.TriangleNum; j++) //定位待插入点影响的所有△
                {
                    IsInCircle = InTriangleExtCircle(DS.Vertex[i].x, DS.Vertex[i].y, DS.Vertex[DS.Triangle[j].V1Index].x, DS.Vertex[DS.Triangle[j].V1Index].y,
                        DS.Vertex[DS.Triangle[j].V2Index].x, DS.Vertex[DS.Triangle[j].V2Index].y,
                        DS.Vertex[DS.Triangle[j].V3Index].x, DS.Vertex[DS.Triangle[j].V3Index].y);
                    if (IsInCircle)    //△j在影响范围内
                    {
                        Edge[] eee ={new Edge(DS.Triangle[j].V1Index, DS.Triangle[j].V2Index),
                            new Edge(DS.Triangle[j].V2Index, DS.Triangle[j].V3Index),
                            new Edge(DS.Triangle[j].V3Index, DS.Triangle[j].V1Index)};  //△的三边

                        #region 存储除公共边外的△边
                        bool IsNotComnEdge;
                        for (k = 0; k < 3; k++)
                        {
                            IsNotComnEdge = true;
                            for(int n=0; n<EdgeCount; n++)
                            {
                                if (Edge.Compare(eee[k], EdgesBuf[n]))   //此边为公共边
                                {
                                    //删除已缓存的公共边
                                    IsNotComnEdge = false;
                                    EdgesBuf[n] = EdgesBuf[EdgeCount - 1];
                                    EdgeCount--;
                                    break;
                                }
                            }

                            if (IsNotComnEdge)
                            {
                                EdgesBuf[EdgeCount] = eee[k];    //边加入Buffer
                                EdgeCount++;
                            }
                        }
                        #endregion

                        //删除△j, 表尾△前移插入
                        DS.Triangle[j].V1Index = DS.Triangle[DS.TriangleNum - 1].V1Index;
                        DS.Triangle[j].V2Index = DS.Triangle[DS.TriangleNum - 1].V2Index;
                        DS.Triangle[j].V3Index = DS.Triangle[DS.TriangleNum - 1].V3Index;
                        j--;
                        DS.TriangleNum--;
                    }
                }//for 定位点

                #region 构建新△
                for (j = 0; j < EdgeCount; j++)
                {                    
                    DS.Triangle[DS.TriangleNum].V1Index = EdgesBuf[j].Vertex1ID;
                    DS.Triangle[DS.TriangleNum].V2Index = EdgesBuf[j].Vertex2ID;
                    DS.Triangle[DS.TriangleNum].V3Index = i;
                    DS.TriangleNum ++;
                }
                #endregion
            }//逐点加入for
        }

        //计算外接圆圆心
        public void CalculateBC()
        {
            double x1, y1, x2, y2, x3, y3;
            for (int i = 0; i < DS.TriangleNum; i++)
            {
                //计算三角形的外接圆心
                x1 = DS.Vertex[DS.Triangle[i].V1Index].x;
                y1 = DS.Vertex[DS.Triangle[i].V1Index].y;
                x2 = DS.Vertex[DS.Triangle[i].V2Index].x;
                y2 = DS.Vertex[DS.Triangle[i].V2Index].y;
                x3 = DS.Vertex[DS.Triangle[i].V3Index].x;
                y3 = DS.Vertex[DS.Triangle[i].V3Index].y;
                GetTriangleBarycnt(x1, y1, x2, y2, x3, y3, ref DS.Barycenters[i].X, ref DS.Barycenters[i].Y);
            }

        }

        //求三角形的外接圆心
        private void GetTriangleBarycnt(double x1, double y1, double x2, double y2, double x3, double y3, ref double bcX, ref double bcY)
        {
            double precision = 0.000001;
            double k1, k2;   //两条中垂线斜率

            //三点共线
            if (Math.Abs(y1 - y2) < precision && Math.Abs(y2 - y3) < precision)
            {
                MessageBox.Show("Three Points on one line!");
                Application.Exit();
            }

            //边的中点
            double MidX1 = (x1 + x2) / 2;
            double MidY1 = (y1 + y2) / 2;
            double MidX2 = (x3 + x2) / 2;
            double MidY2 = (y3 + y2) / 2;

            if (Math.Abs(y2 - y1) < precision)  //p1p2平行于X轴
            {
                k2 = -(x3 - x2) / (y3 - y2);
                bcX = MidX1;
                bcY = k2 * (bcX - MidX2) + MidY2;
            }
            else if (Math.Abs(y3 - y2) < precision)   //p2p3平行于X轴
            {
                k1 = -(x2 - x1) / (y2 - y1);
                bcX = MidX2;
                bcY = k1 * (bcX - MidX1) + MidY1;
            }
            else
            {
                k1 = -(x2 - x1) / (y2 - y1);
                k2 = -(x3 - x2) / (y3 - y2);
                bcX = (k1 * MidX1 - k2 * MidX2 + MidY2 - MidY1) / (k1 - k2);
                bcY = k1 * (bcX - MidX1) + MidY1;
            }
        }

        //判断点是否在△的外接圆中
        private Boolean InTriangleExtCircle(double xp, double yp, double x1, double y1, double x2, double y2, double x3, double y3)
        {
            double RadiusSquare;    //半径的平方
            double DisSquare;  //距离的平方
            double BaryCntX = 0, BaryCntY = 0;
            GetTriangleBarycnt(x1, y1, x2, y2, x3, y3, ref  BaryCntX, ref BaryCntY);

            RadiusSquare = (x1 - BaryCntX) * (x1 - BaryCntX) + (y1 - BaryCntY) * (y1 - BaryCntY);
            DisSquare = (xp - BaryCntX) * (xp - BaryCntX) + (yp - BaryCntY) * (yp - BaryCntY);

            if (DisSquare <= RadiusSquare)
                return true;
            else
                return false;
        }

        //建立三角形的三条边的拓扑关系
        //实际上就是遍历每一个三角形的每条边 
        //不重复的插入到DS的TinEdges中 并且保存边与点和三角形的对应关系
        private void TopologizeEdge()
        {
            DS.TinEdgeNum = 0;
            DS.TinEdges = new Edge[DataStruct.MaxEdges];   //清除旧数据
            long[] Vindex = new long[3]; //3个顶点索引

            //遍历每个三角形的三条边
            for (int i = 0; i < DS.TriangleNum; i++)
            {
                Vindex[0] = DS.Triangle[i].V1Index;
                Vindex[1] = DS.Triangle[i].V2Index;
                Vindex[2] = DS.Triangle[i].V3Index;
                //每条边
                for (int j = 0; j < 3; j++)   
                {
                    Edge e = new Edge(Vindex[j],Vindex[(j + 1) % 3]);

                    //判断边在数组中是否已存在
                    int k;
                    for (k = 0; k < DS.TinEdgeNum; k++)
                    {
                        if (Edge.Compare(e, DS.TinEdges[k]))   //此边已构造
                        {
                            DS.TinEdges[k].AdjTriangle2ID = i;
                            DS.TinEdges[k].NotHullEdge = true;
                            break;
                        }
                    }

                    if (k == DS.TinEdgeNum)   //此边为新边
                    {
                        //存储于点的关系
                        DS.TinEdges[DS.TinEdgeNum].Vertex1ID = e.Vertex1ID;
                        DS.TinEdges[DS.TinEdgeNum].Vertex2ID = e.Vertex2ID;
                        //存储与三角形的关系
                        DS.TinEdges[DS.TinEdgeNum].AdjTriangle1ID = i;
                        //与定点关系
                        DS.TinEdges[DS.TinEdgeNum].AdjacentT1V3 = Vindex[(j + 2) % 3];
                        DS.TinEdgeNum++;
                    }

                }
            }
        }

        //i为TinEdge的ID号
        private void DrawHullVorEdge(int i, Graphics g)
        {
            
            PointF pnt1 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex1ID].x),
                Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex1ID].y));
            PointF pnt2 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex2ID].x), 
                Convert.ToSingle(DS.Vertex[DS.TinEdges[i].Vertex2ID].y));
            PointF pnt3 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[i].AdjacentT1V3].x),
                Convert.ToSingle(DS.Vertex[DS.TinEdges[i].AdjacentT1V3].y));    //边对应的△顶点

            PointF MidPnt = new PointF((pnt1.X + pnt2.X) / 2, (pnt2.Y + pnt2.Y) / 2);  //TinEdge中点
            PointF BaryCnt = new PointF(Convert.ToSingle(DS.Barycenters[DS.TinEdges[i].AdjTriangle1ID].X),
                Convert.ToSingle(DS.Barycenters[DS.TinEdges[i].AdjTriangle1ID].Y));  //外接圆心
            PointF EndPnt = new PointF();   //圆心连接于此点构成VEdge

            //圆心在box外则直接跳过
            if (!(BaryCnt.X >= DS.BBOX.XLeft && BaryCnt.X <= DS.BBOX.XRight &&
                BaryCnt.Y >= DS.BBOX.YTop && BaryCnt.Y <= DS.BBOX.YBottom))    
                return;

            //求斜率
            float k = 0;  //斜率
            bool KExist = true;
            if (Math.Abs(pnt1.Y - pnt2.Y) < 0.000001)
                KExist = false;     //k不存在
            else
                k = (pnt1.X - pnt2.X) / (pnt2.Y - pnt1.Y);

            //该凸壳边是△的钝角边则外接圆心在△外
            bool obtEdge = IsObtuseEdge(i);  

            #region 根据△圆心在凸壳内还是在外求VEdge
            //圆心在边右则往左延伸，在左则往右

            if (!obtEdge)   //圆心在凸壳内(或边界上)/////////////////////////////////
            {
                if (!KExist)    //k不存在
                {
                    // MessageBox.Show("斜率不存在的△-"+DS.TinEdges[i].AdjTriangle1ID.ToString());
                    if (BaryCnt.Y > MidPnt.Y || BaryCnt.Y < pnt3.Y)// BaryCnt<y3 ->圆心与中点重合
                        EndPnt.Y = DS.BBOX.YTop;
                    else if (BaryCnt.Y < MidPnt.Y || BaryCnt.Y > pnt3.Y)
                        EndPnt.Y = DS.BBOX.YBottom;

                    EndPnt.X = BaryCnt.X;
                }
                else      //K存在
                {
                    if (BaryCnt.X > MidPnt.X || (BaryCnt.X == MidPnt.X && BaryCnt.X < pnt3.X))
                        EndPnt.X = DS.BBOX.XLeft;
                    else if (BaryCnt.X < MidPnt.X || (BaryCnt.X == MidPnt.X && BaryCnt.X > pnt3.X))
                        EndPnt.X = DS.BBOX.XRight;

                    EndPnt.Y = k * (EndPnt.X - BaryCnt.X) + BaryCnt.Y;
                }

            }
            else    //圆心在凸壳外/////////////////////////////////////////////
            {
                if (!KExist)    //k不存在
                {
                    if (BaryCnt.Y < MidPnt.Y || BaryCnt.Y < pnt3.Y)
                        EndPnt.Y = DS.BBOX.YTop;
                    else if (BaryCnt.Y > MidPnt.Y || BaryCnt.Y > pnt3.Y)
                        EndPnt.Y = DS.BBOX.YBottom;

                    EndPnt.X = BaryCnt.X;
                }
                else   //K存在
                {
                    if (BaryCnt.X < MidPnt.X)
                        EndPnt.X = DS.BBOX.XLeft;
                    else if (BaryCnt.X > MidPnt.X)
                        EndPnt.X = DS.BBOX.XRight;

                    EndPnt.Y = k * (EndPnt.X - BaryCnt.X) + BaryCnt.Y;
                }

            }//else 在△外

            //与外框交点在边界外的处理
            if (k != 0 && KExist)
            {
                if (EndPnt.Y < DS.BBOX.YTop)
                    EndPnt.Y = DS.BBOX.YTop;
                else if (EndPnt.Y > DS.BBOX.YBottom)
                    EndPnt.Y = DS.BBOX.YBottom;

                EndPnt.X = (EndPnt.Y - BaryCnt.Y) / k + BaryCnt.X;
            }

            #endregion

            g.DrawLine(new Pen(Color.Black, 1), BaryCnt, EndPnt);

        }

        //index为TinEdge的索引号
        //若为钝角边则返回true
        private bool IsObtuseEdge(int index)
        {
            PointF EdgePnt1 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[index].Vertex1ID].x),
                Convert.ToSingle(DS.Vertex[DS.TinEdges[index].Vertex1ID].y));
            PointF EdgePnt2 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[index].Vertex2ID].x),
                Convert.ToSingle(DS.Vertex[DS.TinEdges[index].Vertex2ID].y));
            PointF Pnt3 = new PointF(Convert.ToSingle(DS.Vertex[DS.TinEdges[index].AdjacentT1V3].x),
                 Convert.ToSingle(DS.Vertex[DS.TinEdges[index].AdjacentT1V3].y));

            PointF V1 = new PointF((EdgePnt1.X - Pnt3.X), (EdgePnt1.Y - Pnt3.Y));
            PointF V2 = new PointF((EdgePnt2.X - Pnt3.X), (EdgePnt2.Y - Pnt3.Y));
            return (V1.X * V2.X + V1.Y * V2.Y) < 0; //a·b的值<0则为钝角
        }

        //判读点与三角形的关系 点在△内则返回true
        private bool PointInTriganle(long PntIndex, long index)
        {
            PointF pnt1 = new PointF(Convert.ToSingle(DS.Vertex[DS.Triangle[index].V1Index].x),
                Convert.ToSingle(DS.Vertex[DS.Triangle[index].V1Index].y));
            PointF pnt2 = new PointF(Convert.ToSingle(DS.Vertex[DS.Triangle[index].V2Index].x),
                Convert.ToSingle(DS.Vertex[DS.Triangle[index].V2Index].y));
            PointF pnt3 = new PointF(Convert.ToSingle(DS.Vertex[DS.Triangle[index].V3Index].x),
                 Convert.ToSingle(DS.Vertex[DS.Triangle[index].V3Index].y));
            PointF JudgePoint = new PointF(Convert.ToSingle(DS.Barycenters[index].X), Convert.ToSingle(DS.Barycenters[index].Y));  //外接圆心

            int IsPositive;    //正则等于1，负则等于-1
            double result = VectorXMultiply(JudgePoint, pnt1, pnt2);
            if (result > 0)
                IsPositive = 1;
            else
                IsPositive = -1;

            result = VectorXMultiply(JudgePoint, pnt2, pnt3);
            if ((IsPositive == 1 && result < 0) || (IsPositive == -1 && result > 0))
                return false;

            result = VectorXMultiply(JudgePoint, pnt3, pnt1);
            if ((IsPositive == 1 && result > 0) || (IsPositive == -1 && result < 0))
                return true;
            else
                return false;
        }
       //判读点与线的关系 >0
        private double VectorXMultiply(PointF BaryCnt, PointF pnt1, PointF pnt2)
        {
            PointF V1 = new PointF((pnt1.X - BaryCnt.X), (pnt1.Y - BaryCnt.Y));
            PointF V2 = new PointF((pnt2.X - BaryCnt.X), (pnt2.Y - BaryCnt.Y));
            return (V1.X * V2.Y - V2.X * V1.Y);
        }
        //判断点是否在包围壳中
        private bool PointInBox(PointF point)
        {
            return (point.X >= DS.BBOX.XLeft && point.X <= DS.BBOX.XRight &&
                    point.Y >= DS.BBOX.YTop && point.Y <= DS.BBOX.YBottom);
        }
    }
}


