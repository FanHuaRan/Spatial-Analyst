using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstraDemo.Dijkstra
{
    public class SimpleDijkstra:IDijkstra
    {

        public List<List<int>> ALL_SHORTEST_PATH(List<List<float>> cost, int startPoint, out List<float> dist)
        {
            if(cost==null||startPoint<-1||startPoint>cost.Count)
            {
                dist = null;
                return null;
            }
            int n = cost.Count;
            List<List<int>> path = new List<List<int>>();
            for(int i=0;i<n;i++)
            {
                path.Add(new List<int>());
            }
            dist = new List<float>(n);
            //是否找到最短路径的标志数组
            List<int> arrayFlag = new List<int>(n);
            for (int i = 0; i < n; i++)//初始化
            {
                arrayFlag.Add(0);
                dist.Add(cost[startPoint][i]);
                path[i].Add(startPoint);
            }
            arrayFlag[startPoint] = 1;
            int count = 1;
            int u = startPoint;
            //控制循环n-1次
            while (count < n)
            {
                float temp = 1000000000;
                for (int i = 0; i < n; i++)//寻找没找到最短路径的当前路径权值最小的顶点
                {
                    if (arrayFlag[i] == 0 && dist[i] < temp)
                    {
                        u = i;
                        temp = dist[i];
                    }
                }
                arrayFlag[u] = 1;
                path[u].Add(u);
                for (int i = 0; i < n; i++)//寻找与u直接联通但没有确定最短路径的顶点,修改权值和路径
                {
                    if (arrayFlag[i] == 0 && dist[u] + cost[u][i] < dist[i])
                    {
                        dist[i] = dist[u] + cost[u][i];
                        path[i].Clear();
                        for (int k = 0; k < path[u].Count; k++)
                        {
                            path[i].Add(path[u][k]);
                        }
                       // arrayFlag[i] = arrayFlag[u];
                    }
                }
                count++;
            }
            return path;
        }

        public List<int> SHORTEST_PATH(List<List<float>> cost, int startPoint, int endPoint, out float singledist)
        {
            if (cost == null || startPoint < 0 || startPoint > cost.Count-1||endPoint<0||endPoint>cost.Count-1)
            {
                singledist = 0;
                return null;
            }
            List<int> singlePath = new List<int>();
            if(startPoint==endPoint)
            {
                singledist = 0;
                singlePath.Add(startPoint);
                return singlePath;
            }
            int n = cost.Count;
            List<List<int>> path = new List<List<int>>();
            for (int i = 0; i < n; i++)
            {
                path.Add(new List<int>());
            }
            List<float> dist = new List<float>(n);
            //是否找到最短路径的标志数组
            List<int> arrayFlag = new List<int>(n);
            for (int i = 0; i < n; i++)//初始化
            {
                arrayFlag.Add(0);
                dist.Add(cost[startPoint][i]);
                path[i].Add(startPoint);
            }
            arrayFlag[startPoint] = 1;
            int count = 1;
            int u = startPoint;
            //控制循环n-1次
            while (count < n)
            {
                float temp = 1000000000;
                for (int i = 0; i < n; i++)//寻找没找到最短路径的当前路径权值最小的顶点
                {
                    if (arrayFlag[i] == 0 && dist[i] < temp)
                    {
                        u = i;
                        temp = dist[i];
                    }
                }
                arrayFlag[u] = 1;
                path[u].Add(u);
                if (u == endPoint)
                {
                    break;
                }
                for (int i = 0; i < n; i++)//寻找与u直接联通但没有确定最短路径的顶点,修改权值和路径
                {
                    if (arrayFlag[i] == 0 && dist[u] + cost[u][i] < dist[i])
                    {
                        dist[i] = dist[u] + cost[u][i];
                        path[i].Clear();
                        for (int k = 0; k < path[u].Count; k++)
                        {
                            path[i].Add(path[u][k]);
                        }
                        // arrayFlag[i] = arrayFlag[u];
                    }
                }
                count++;
            }
            singledist = dist[endPoint];
            return singlePath = path[endPoint];
        }
    }
}
