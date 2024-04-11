using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    class MyList<T>
    {
        const int DEFAULT_SIZE = 1;
        T[] _data = new T[DEFAULT_SIZE];
        public int Count = 0; //실제 사용중인 데이터
        public int Capacity { get { return _data.Length; } }  //예약된 데이터 

        public void Add(T item)  // 아이템 추가?
        {
            //1.공간충분 확인
            if(Count >= Capacity)
            {
                //공간 다시 확보
                T[] newArray = new T[Count * 2];
                for(int i = 0; i < Count; i++)
                {
                    newArray[i] = _data[i];
                }

                _data = newArray;
            }
            //2. 공간에 데이터를 삽입
            _data[Count] = item;
            Count++;
        }

        public T this[int index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }

        public void RemoveAt(int index)
        {
            for(int i = index; i < Count -1; i++)
            {
                _data[i] = _data[i + 1];
            }
            _data[Count - 1] = default(T);
            Count--;
        }
    }
    class Board
    {
        public int[] _data = new int[25];
        public MyList<int> _data2 = new MyList<int>();
        public LinkedList<int> _data3 = new LinkedList<int>();
        public void Initialize()
        {
            _data2.Add(1);
            _data2.Add(2);
            _data2.Add(3);
            _data2.Add(4);
            _data2.Add(5);
        }
    }
}
