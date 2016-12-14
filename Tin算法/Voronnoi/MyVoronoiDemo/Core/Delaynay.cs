using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MrFan.Tool.Delaunry
{
    /// <summary>
    /// 使用凸包内插算法实现
    /// </summary>
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
        //增量法生成Delaunay三角网
        public void CreateTIN()
        {
            //建立凸壳
            CreateConvex();
            //初始三角剖分
            HullTriangulation();
            //逐点插入修改Tin
            PlugInEveryVertex();
            //建立边的拓扑结构
            TopologizeEdge();
        }
        //建立逆时针方向的凸壳
        public void CreateConvex()
        {
            #region 初始化凸壳顶点链表 
            //初始化凸壳顶点链表 
            //若为空则实例化 否则就去除链表的顶点的凸壳标记 然后清除元素
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
            #endregion
            #region 分别计算x-y和x+y的最大、最小点、得出一个初步大型包围壳
            //x-y最大代表是最右上方的点
            //x-y最小代表是最左下方的点
            //x+y最大代表是最右下方的点
            //x+y最小代表是最左上方的点
            //刚好一个大型的包围壳
            PntV_ID MaxMinus, MinMinus, MaxAdd, MinAdd;
            //用顶点序列里面的第一个顶点初始化
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
            #region 按照左下-右下-右上-左上的逆时针顺序加入凸壳链表中
            //将4点加入链表
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
            //将凸壳点在顶点序列中标记，下一步插入则不再处理
            for (int i = 0; i < HullPoint.Count; i++)
            {
                DS.Vertex[HullPoint[i]].isHullEdge = -1;
            }

            #endregion
            #region 循环遍历，根据条件插入其他点
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
                        //根据插入点及其之后两个点三点关系判断
                        pnt1 = new PointF(Convert.ToSingle(DS.Vertex[HullPoint[(j + 3) % HullPoint.Count]].x),
                            Convert.ToSingle(DS.Vertex[HullPoint[(j + 3) % HullPoint.Count]].y));
                        isOnRight = VectorXMultiply(pnt3, pnt2, pnt1);
                        //删除内凹点p2(即v[j+2])
                        if (isOnRight > 0)   
                        {
                            //点在凸壳链表中的索引
                            int index = (j + 2) % HullPoint.Count;    
                            DS.Vertex[HullPoint[index]].isHullEdge = 0;
                            HullPoint.RemoveAt(index);
                        }
                        break;
                    }
                }
            }
            #endregion
            #region 对凸壳顶点进行标记
            for (int i = 0; i < HullPoint.Count; i++)
            {
                //将属于凸壳链表的顶点的凸壳标记设为1
                DS.Vertex[HullPoint[i]].isHullEdge = 1;
            }
            #endregion
        }
        //凸壳三角剖分 根据凸壳建立初步的三角网
        private void HullTriangulation()
        {
            //初始化三角形个数为0
            DS.TriangleNum = 0;
            //凸壳为点或线的情况
            //多点共边的情况
            //复制凸壳顶点
            List<long> points = new List<long>();
            for (int i = 0; i < HullPoint.Count; i++)
            {
                points.Add(HullPoint[i]);
            }
            //对所有凸壳顶点有序进行三角网的构建
            long id1, id2, id3;
            while (points.Count >= 3)
            {
                for (int i = 0; i < points.Count; i++)
                {
                    //将相邻三个顶点构建三角形 
                    id1 = points[i];
                    id2 = points[(i + 1) % points.Count];
                    id3 = points[(i + 2) % points.Count];
                    //三角形为干净的则加入网中
                    if (IsClean(id1, id2, id3))
                    {
                        DS.Triangle[DS.TriangleNum].V1Index = id1;
                        DS.Triangle[DS.TriangleNum].V2Index = id2;
                        DS.Triangle[DS.TriangleNum].V3Index = id3;
                        DS.TriangleNum++;
                        //标记顶点为已经处理过的凸壳点
                        DS.Vertex[id2].isHullEdge = 2; 
                        points.Remove(id2);
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 逐点加入修改TIN
        /// </summary>
        private void PlugInEveryVertex()
        {
            //非公共边缓冲区
            Edge[] EdgesBuf = new Edge[DataStruct.MaxTriangles];  
            bool IsInCircle;
            int i, j, k;
            int EdgeCount;
            //逐点加入
            for (i = 0; i < DS.VerticesNum; i++)    
            {
                //跳过凸壳顶点
                if (DS.Vertex[i].isHullEdge != 0)
                {
                    continue;
                }
                EdgeCount = 0;
                #region 插入和修正
              // 定位待插入点影响的所有三角形
             ///删除受影响的三角形、同时保存这些三角形之间的不重复边
             ///将这些边与该点组成新的三角形
             ///这些三角形后面也会被进行检查！！
               
                //定位待插入点影响的所有三角形
                for (j = 0; j < DS.TriangleNum; j++) 
                {
					//判断点是否在三角形的外接圆中
                    IsInCircle = InTriangleExtCircle(DS.Vertex[i].x, DS.Vertex[i].y, DS.Vertex[DS.Triangle[j].V1Index].x, DS.Vertex[DS.Triangle[j].V1Index].y,
                        DS.Vertex[DS.Triangle[j].V2Index].x, DS.Vertex[DS.Triangle[j].V2Index].y,
                        DS.Vertex[DS.Triangle[j].V3Index].x, DS.Vertex[DS.Triangle[j].V3Index].y);
                    //三角形J在影响范围内
                    if (IsInCircle)   
                    {
                        //先获取到三角形的三边
                        Edge[] eee ={new Edge(DS.Triangle[j].V1Index, DS.Triangle[j].V2Index),
                            new Edge(DS.Triangle[j].V2Index, DS.Triangle[j].V3Index),
                            new Edge(DS.Triangle[j].V3Index, DS.Triangle[j].V1Index)};  
                        #region 存储除公共边外的三角形边
                        bool IsNotComnEdge;
                        for (k = 0; k < 3; k++)
                        {
                            IsNotComnEdge = true;
                            for (int n = 0; n < EdgeCount; n++)
                            {
                                //删除已缓存的公共边
                                if (Edge.Compare(eee[k], EdgesBuf[n]))   
                                {
                                    IsNotComnEdge = false;
                                    EdgesBuf[n] = EdgesBuf[EdgeCount - 1];
                                    EdgeCount--;
                                    break;
                                }
                            }
                            //不是公共边则加入缓冲区中
                            if (IsNotComnEdge)
                            {
                                EdgesBuf[EdgeCount] = eee[k];  
                                EdgeCount++;
                            }
                        }
                        #endregion
                        //删除三角形J, 三角形数组整体前移
                        DS.Triangle[j].V1Index = DS.Triangle[DS.TriangleNum - 1].V1Index;
                        DS.Triangle[j].V2Index = DS.Triangle[DS.TriangleNum - 1].V2Index;
                        DS.Triangle[j].V3Index = DS.Triangle[DS.TriangleNum - 1].V3Index;
                        j--;
                        DS.TriangleNum--;
                    }
                }
                #endregion
                #region 将非公共边与顶点构成新三角形
                for (j = 0; j < EdgeCount; j++)
                {
                    DS.Triangle[DS.TriangleNum].V1Index = EdgesBuf[j].Vertex1ID;
                    DS.Triangle[DS.TriangleNum].V2Index = EdgesBuf[j].Vertex2ID;
                    DS.Triangle[DS.TriangleNum].V3Index = i;
                    DS.TriangleNum++;
                }
                #endregion
            }
        }
       
        ///<summary>
        ///建立三角形的三条边的拓扑关系
        ///实际上就是遍历每一个三角形的每条边 
        ///不重复的插入到DS的TinEdges中 
        ///并且保存边与点和三角形的对应关系
        /// </summary>
        private void TopologizeEdge()
        {
            //清除旧数据
            DS.TinEdgeNum = 0;
            DS.TinEdges = new Edge[DataStruct.MaxEdges];
            //3个顶点索引
            long[] Vindex = new long[3]; 
            //遍历每个三角形的三条边
            for (int i = 0; i < DS.TriangleNum; i++)
            {
                Vindex[0] = DS.Triangle[i].V1Index;
                Vindex[1] = DS.Triangle[i].V2Index;
                Vindex[2] = DS.Triangle[i].V3Index;
                //每条边
                for (int j = 0; j < 3; j++)
                {
                    Edge e = new Edge(Vindex[j], Vindex[(j + 1) % 3]);
                    //判断边在数组中是否已存在
                    int k;
                    for (k = 0; k < DS.TinEdgeNum; k++)
                    {
                        //此边已构造
                        if (Edge.Compare(e, DS.TinEdges[k]))
                        {
                            //存储第二个三角形
                            DS.TinEdges[k].AdjTriangle2ID = i;
                            //同时为两个三角形所有则一定不是凸壳边
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
        /// <summary>
        ///构建并显示Voronoi图、基础建立在Delaunry三角网之上、不断连接
        /// </summary>
        /// <param name="g"></param>
        public void CreateVoronoi(Graphics g)
        {
            //获取每条Tin边
            for (int i = 0; i < DS.TinEdgeNum; i++)
            {
                //三角形边为凸壳边
                if (!DS.TinEdges[i].NotHullEdge) 
                {
                    //单独画 因为有可能在边缘
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
        /// <summary>
        /// 计算所有三角形的外接圆圆心
        /// </summary>
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
        /// <summary>
        /// 求三角形的外接圆心
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="bcX"></param>
        /// <param name="bcY"></param>
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
        /// <summary>
        /// 判断三角形外接圆中不含其他凸壳顶点 空圆法则
        /// </summary>
        /// <param name="p1ID"></param>
        /// <param name="p2ID"></param>
        /// <param name="p3ID"></param>
        /// <returns></returns>
        private bool IsClean(long p1ID, long p2ID, long p3ID)
        {
            for (int i = 0; i < HullPoint.Count; i++)
            {
                //跳过已构网的凸壳点和三角形顶点
                if (DS.Vertex[HullPoint[i]].isHullEdge == 2 || HullPoint[i] == p1ID || HullPoint[i] == p2ID || HullPoint[i] == p3ID)
                    continue;

                //如果点i位于三角形外接圆内
                if (InTriangleExtCircle(DS.Vertex[HullPoint[i]].x, DS.Vertex[HullPoint[i]].y, DS.Vertex[p1ID].x,
                    DS.Vertex[p1ID].y, DS.Vertex[p2ID].x, DS.Vertex[p2ID].y, DS.Vertex[p3ID].x, DS.Vertex[p3ID].y))
                    return false;
            }

            return true;
        }
        /// <summary>
        /// 判断点是否在三角形的外接圆中
        /// 先求得该三角形外心和外接圆半径
        /// 再根据该点到外心的距离和半径的大小得出结论
        /// </summary>
        /// <param name="xp"></param>
        /// <param name="yp"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <returns></returns>
        private bool InTriangleExtCircle(double xp, double yp, double x1, double y1, double x2, double y2, double x3, double y3)
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
        /// <summary>
        /// 画凸壳边的泰森边
        /// </summary>
        /// <param name="i">TinEdge的ID号</param>
        /// <param name="g"></param>
        private void DrawHullVorEdge(int i, Graphics g)
        {
            //求出凸壳边的中点 
            //外心和中点的连线就是中垂线，求出中垂线与包围壳的交点
            //连接该三角形外心和交点
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

            //该凸壳边是三角形的钝角边则外接圆心在三角形外
            bool obtEdge = IsObtuseEdge(i);  

            #region 根据三角形圆心在凸壳内还是在外求VEdge
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
     
        /// <summary>
        /// 若为钝角边则返回true
        /// </summary>
        /// <param name="index">index为TinEdge的索引号</param>
        /// <returns></returns>
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
        /// <summary>
        /// 判读点与三角形的关系 点在三角形内则返回true
        /// </summary>
        /// <param name="PntIndex"></param>
        /// <param name="index"></param>
        /// <returns></returns>
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
       /// <summary>
        /// 运用右手法则判读点与线的关系
       /// </summary>
       /// <param name="BaryCnt"></param>
       /// <param name="pnt1"></param>
       /// <param name="pnt2"></param>
        /// <returns> 结果>0在右方、等于0在线段所在直线上，否则在左方</returns>
        private double VectorXMultiply(PointF BaryCnt, PointF pnt1, PointF pnt2)
        {
            PointF V1 = new PointF((pnt1.X - BaryCnt.X), (pnt1.Y - BaryCnt.Y));
            PointF V2 = new PointF((pnt2.X - BaryCnt.X), (pnt2.Y - BaryCnt.Y));
            return (V1.X * V2.Y - V2.X * V1.Y);
        }
        /// <summary>
        /// 判断点是否在包围壳中
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private bool PointInBox(PointF point)
        {
            return (point.X >= DS.BBOX.XLeft && point.X <= DS.BBOX.XRight &&
                    point.Y >= DS.BBOX.YTop && point.Y <= DS.BBOX.YBottom);
        }
    }
}


