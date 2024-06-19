using System;
using System.Collections.Generic;

namespace Exercise
{
    class PriorityQueue
    {
        List<int> heap = new List<int>();
        public void Push(int data)
        {
            //힙의 맨끝에 새로운 데이터 삽입
            heap.Add(data);
            int now = heap.Count - 1;

            //도장깨기
            while(now > 0)
            {
                //부모 노드가 나보다 크면 끝
                int next = (now - 1) / 2;
                if (heap[now] < heap[next])
                    break;

                //부모노드가 나보다 작다면 위치 교체
                int temp = heap[now];
                heap[now] = heap[next];
                heap[next] = temp;

                now = next;
            }
        }

        public int Pop()
        {
            //반환할 데이터 저장
            int ret = heap[0];

            //마지막 데이터를 루트로 이동
            int lastIndex = heap.Count - 1;
            heap[0] = heap[lastIndex];
            heap.RemoveAt(lastIndex);
            lastIndex--;

            int now = 0;
            //역 도장깨기
            while (true)
            {
                int left = 2 * now + 1; 
                int right = 2 * now + 2;
                int next = now;

                //왼쪽 자식이 존재하고 나보다 크다면 비교대상 교체
                if (left <= lastIndex && heap[next] < heap[left])
                    next = left;

                //(왼쪽자식과 비교중) 오른쪽 자식이 존재하고 나보다 크다면 교체
                if (right <= lastIndex && heap[next] < heap[right])
                    next = right;

                //현재 값이 가장 크면 현재 위치를 유지하며 종료
                if (next == now)
                    break;

                //교체 되었다면 실제 노드값 교체
                int temp = heap[now];
                heap[now] = heap[next];
                heap[next] = temp;

                now = next;

            }
            return ret;
        }
        public int Count()
        {
            return heap.Count;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            PriorityQueue q = new PriorityQueue();
            q.Push(20);
            q.Push(10);
            q.Push(30);
            q.Push(90);
            q.Push(40);

            while(q.Count() > 0)
            {
                Console.WriteLine(q.Pop());
            }
        }
    }
}
