using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrFan.Tool.Delaunry
{
    public interface ITinVoronoi
    {
        //数据访问接口
        DataStruct DS { get; }
        //创建包围壳
        void CreateConvex();
        //创建泰森多边形
        void CreateVoronoi(Graphics g);
        //创建Delaunay三角网
        void CreateTIN();
        //计算外接圆圆心
        void CalculateBC();
    }
}
