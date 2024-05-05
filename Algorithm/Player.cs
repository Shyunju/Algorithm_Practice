using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    class Pos
    {
        public Pos(int y, int x) { Y = y; X = x; }
        public int Y;
        public int X;

    }
    class Player
    {
        public int PosY { get; private set; }
        public int PosX { get; private set; }

        Random _random = new Random();
        Board _board;

        enum Dir
        {
            Up,
            Left,
            Down,
            Right
        }

        int _dir = (int)Dir.Up;
        List<Pos> _points = new List<Pos>();

        public void Initialize(int posY, int posX, Board board)
        {
            PosX = posX;
            PosY = posY;

            _board = board;

            BFS();
        }
        void BFS()
        {
            int[] deltaY = new int[] {-1, 0, 1, 0 };
            int[] deltaX = new int[] {0, -1, 0, 1 };
            bool[,] found = new bool[_board.Size, _board.Size];
            Pos[,] parent = new Pos[_board.Size, _board.Size];

            Queue<Pos> q = new Queue<Pos>();
            q.Enqueue(new Pos(PosY, PosX));
            found[PosY, PosX] = true;
            parent[PosY, PosX] = new Pos(PosY, PosX);

            while(q.Count > 0)
            {
                Pos pos = q.Dequeue();
                int nowY = pos.Y;
                int nowX = pos.X;

                for(int i = 0; i < 4; i++)
                {
                    int nextY = nowY + deltaY[i];
                    int nextX = nowX + deltaX[i];

                    if (nextX < 0 || nextX >= _board.Size || nextY < 0 || nextY >= _board.Size)
                        continue;
                    if (_board.Tile[nextY, nextX] == Board.TileType.Wall)
                        continue;
                    if(found[nextY, nextX])
                        continue;

                    q.Enqueue(new Pos(nextY, nextX));
                    found[nextY, nextX] = true;
                    parent[nextY, nextX] = new Pos(nowY, nowX);
                }
            }
            int y = _board.DestY;
            int x = _board.DestX;  //목표 좌표

            while(parent[y,x].Y != y || parent[y,x].X != x)   //나의 좌표와 부모의 좌표가 같다 == 출발지
            {
                _points.Add(new Pos(y, x));
                Pos pos = parent[y, x];
                y = pos.Y;
                x = pos.X;   // 부모좌표 역추적
            }
            _points.Add(new Pos(y, x)); //출발지
            //_points.Reverse();  //길을 찾은 순서 정방향
        }

        void RightHand()
        {
            int[] frontY = new int[] { -1, 0, 1, 0 };
            int[] frontX = new int[] { 0, -1, 0, 1 };

            int[] rightY = new int[] { 0, -1, 0, 1 };
            int[] rightX = new int[] { 1, 0, -1, 0 };

            _points.Add(new Pos(PosY, PosX));

            //목적지에 도착할때까지
            while (PosY != _board.DestY || PosX != _board.DestX)
            {
                //1.현재바라보는 방향 기준 오른쪽으로 갈수있는가
                if (_board.Tile[PosY + rightY[_dir], PosX + rightX[_dir]] == Board.TileType.Empty)
                {
                    //오른쪽으로 90도 회전 후 전전
                    _dir = (_dir - 1 + 4) % 4;
                    PosX = PosX + frontX[_dir];
                    PosY = PosY + frontY[_dir];
                    _points.Add(new Pos(PosY, PosX));
                }
                //2.현재 바라보는 방향 기준 전진할 수 있는가
                else if (_board.Tile[PosY + frontY[_dir], PosX + frontX[_dir]] == Board.TileType.Empty)
                {
                    //전진
                    PosY = PosY + frontY[_dir];
                    PosX = PosX + frontX[_dir];
                    _points.Add(new Pos(PosY, PosX));
                }
                else
                {
                    //왼쪽으로 90도 회전
                    _dir = (_dir + 1 + 4) % 4;
                }
            }
        }

        const int MOVE_TICK = 10;
        int _sumTick = 0;
        int _lastIndex = 0;
        public void Update(int deltaTick)
        {
            if (_lastIndex >= _points.Count)
                return;
            _sumTick += deltaTick;
            if(_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                PosY = _points[_lastIndex].Y;
                PosX = _points[_lastIndex].X;

                _lastIndex++;
            }
        }
    }
}
