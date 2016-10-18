using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraDemo.Dijkstra
{
   public interface IDijkstra
    {
       //指定起点 求所有的最短路径
       List<List<int>> ALL_SHORTEST_PATH(List<List<float>> cost, int startPoint, out List<float> dist);

      //求某两个点之间的最短距离
       List<int> SHORTEST_PATH(List<List<float>> cost, int startPoint,int endPoint, out float dist);
    }
}
