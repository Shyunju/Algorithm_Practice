using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Exercise
{
    class PriorityQueue<T> where T : IComparable<T>
    {
        List<T> heap = new List<T>();
        public void Push(T data)
        {
            //힙의 맨끝에 새로운 데이터 삽입
            heap.Add(data);
            int now = heap.Count - 1;

            //도장깨기
            while(now > 0)
            {
                //부모 노드가 나보다 크면 끝
                int next = (now - 1) / 2;
                if (heap[now].CompareTo(heap[next]) < 0)
                    break;

                //부모노드가 나보다 작다면 위치 교체
                T temp = heap[now];
                heap[now] = heap[next];
                heap[next] = temp;

                now = next;
            }
        }

        public T Pop()
        {
            //반환할 데이터 저장
            T ret = heap[0];

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
                if (left <= lastIndex && heap[next].CompareTo(heap[left]) < 0)
                    next = left;

                //(왼쪽자식과 비교중) 오른쪽 자식이 존재하고 나보다 크다면 교체
                if (right <= lastIndex && heap[next].CompareTo(heap[right]) < 0 )
                    next = right;

                //현재 값이 가장 크면 현재 위치를 유지하며 종료
                if (next == now)
                    break;

                //교체 되었다면 실제 노드값 교체
                T temp = heap[now];
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
    class Knight : IComparable<Knight>
    {
        public int Id { get; set; }

        public int CompareTo(Knight other)
        {
            if (Id == other.Id) return 0;
            return Id > other.Id ? 1 : -1;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            PriorityQueue<Knight> q = new PriorityQueue<Knight>();
            q.Push(new Knight() { Id = 20 });
            q.Push(new Knight() { Id = 30 });
            q.Push(new Knight() { Id = 90 });
            q.Push(new Knight() { Id = 10 });
            q.Push(new Knight() { Id = 40 });

            while (q.Count() > 0)
            {
                Console.WriteLine(q.Pop().Id);
            }

        }
        
    }
}
