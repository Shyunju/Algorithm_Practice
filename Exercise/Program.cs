using System;
using System.Collections.Generic;

namespace Exercise
{
    class Graph
    {
        int[,] adj = new int[6, 6]
        {
            {0, 15, 0, 35, 0, 0 },
            {15, 0, 5, 10, 0, 0 },
            {0, 5, 0, 0, 0, 0 },
            {35, 10, 0, 0, 5, 0 },
            {0, 0, 0, 5, 0, 5 },
            {0, 0, 0, 0, 5, 0 }
        };

        public void Dijikstra(int start)
        {
            bool[] visited = new bool[6];
            int[] distance = new int[6];
            int[] parent = new int[6];

            Array.Fill(distance, Int32.MaxValue);

            distance[start] = 0;
            parent[start] = start;
            while(true)
            {
                //가장 적합한 후보의 거리와 번호를 저장
                int closest = Int32.MaxValue;
                int now = -1;
                for(int i = 0; i < 6; i++)
                {
                    //이미 방문한 정점 스킵
                    if (visited[i])
                        continue;

                    //아직 예약되지 않았거나, 기존 후보보다 멀리 있으면 스킵
                    if (distance[i] == Int32.MaxValue || distance[i] >= closest)
                        continue;

                    //가장 적합한 후보 정보 갱신
                    closest = distance[i];
                    now = i;
                }
                //다음 후보가 없음 == 종료
                if (now == -1)
                    break;
                
                //제일 좋은 후보 방문
                visited[now] = true;

                //방문한 정점과 잊ㄴ접한 정점들을 조사해서
                //상황에 따라 발견한 최단거리를 갱신
                for(int next = 0; next < 6; next++)
                {
                    //연결되지 않은 정점 스킵
                    if (adj[now, next] == 0)
                        continue;

                    //이미 방문한 정점 스킵
                    if (visited[next])
                        continue;

                    //새로 조사된 정점의 최단거리
                    int nextDist = distance[now] + adj[now, next];

                    //기존 최단거리가 새로운 거리보다 크면 정보를 갱신
                    if (distance[next] > nextDist)
                    {
                        distance[next] = nextDist;
                        parent[next] = now;
                    }
                }
            }
        }
        public void BFS(int start)
        {
            bool[] found = new bool[6];

            Queue<int> q = new Queue<int>();
            q.Enqueue(start);
            found[start] = true;

            while(q.Count> 0)
            {
                int now = q.Dequeue();
                Console.WriteLine(now);

                for(int next = 0; next < 6; next++)
                {
                    if (adj[now, next] == 0)
                        continue;
                    if (found[next])
                        continue;
                    q.Enqueue(next);
                    found[next] = true;
                }
            }
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>();

            Queue<int> queue = new Queue<int>();

            Graph graph = new Graph();
            graph.Dijikstra(0);
        }
    }
}
