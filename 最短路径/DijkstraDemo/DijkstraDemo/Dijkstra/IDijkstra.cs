using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraDemo.Dijkstra
{
    /// <summary>
    /// 迪杰斯特拉算法接口
    /// </summary>
   public interface IDijkstra
    {
       /// <summary>
        /// 指定起点 求到所有点的最短路径
       /// </summary>
       /// <param name="cost"></param>
       /// <param name="startPoint"></param>
       /// <param name="dist"></param>
       /// <returns></returns>
       List<List<int>> ALL_SHORTEST_PATH(List<List<float>> cost, int startPoint, out List<float> dist);

      /// <summary>
       /// 指定起点和终点，求最短路径
      /// </summary>
      /// <param name="cost"></param>
      /// <param name="startPoint"></param>
      /// <param name="endPoint"></param>
      /// <param name="dist"></param>
      /// <returns></returns>
       List<int> SHORTEST_PATH(List<List<float>> cost, int startPoint,int endPoint, out float dist);
    }
}
