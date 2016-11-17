using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MrFan.Tool.Delaunry
{
    //离散点
    public struct Vertex
    {
        public long x;
        public long y;
        public long ID;
        public int isHullEdge; //凸壳顶点标记,系统初始化为0

        //相等则返回true
        public static bool Compare(Vertex a, Vertex b)
        {
            return a.x == b.x && a.y == b.y ;
        }
    }
    //Tin边 
    public struct Edge
    {
        public long Vertex1ID;   //点索引
        public long Vertex2ID;
        public Boolean NotHullEdge;  //非凸壳边
        public long AdjTriangle1ID;
        public long AdjacentT1V3;    //△1的第三顶点在顶点数组的索引
        public long AdjTriangle2ID;

        public Edge(long iV1, long iV2)
        {
            Vertex1ID = iV1;
            Vertex2ID = iV2;
            NotHullEdge = false;
            AdjTriangle1ID = 0;
            AdjTriangle2ID = 0;
            AdjacentT1V3 = 0;
        }

        //相等则返回true
        public static bool Compare(Edge a, Edge b)
        {
            return ((a.Vertex1ID== b.Vertex1ID) && (a.Vertex2ID==b.Vertex2ID)) ||
                ((a.Vertex1ID== b.Vertex2ID) && (a.Vertex2ID==b.Vertex1ID));
        }    
       
    }

    //三角形
    public struct Triangle
    {
        public long V1Index; //点在链表中的索引值
        public long V2Index;
        public long V3Index;
    }

    //外接圆心
    public struct Barycenter
    {
        public double X;
        public double Y;
    }

    //包围壳
    public struct BoundaryBox
    {
        public long XLeft;
        public long YTop;
        public long XRight;
        public long YBottom;
    }
    //总的数据结构
    public class DataStruct
    { 
        public static int MaxVertices=500;
        public static int MaxEdges=2000;
        public static int MaxTriangles = 1000;
        public Vertex[] Vertex=new Vertex[MaxVertices];
        public Triangle[] Triangle = new Triangle[MaxTriangles];
        public Barycenter[] Barycenters = new Barycenter[MaxTriangles]; 
        //不重复的Tin三角网边
        public Edge[] TinEdges = new Edge[MaxEdges];
        public BoundaryBox BBOX = new BoundaryBox();  
        //初始索引
        public int VerticesNum = 0;
        public int TinEdgeNum = 0;
        public int TriangleNum = 0;
    }

}
