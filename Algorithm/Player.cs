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

            int[] frontY = new int[] { -1, 0, 1, 0 };
            int[] frontX = new int[] { 0, -1, 0, 1 };

            int[] rightY = new int[] { 0, -1, 0, 1 };
            int[] rightX = new int[] { 1, 0, -1, 0 };

            _points.Add(new Pos(PosY, PosX));

            //목적지에 도착할때까지
            while(PosY != board.DestY || PosX != board.DestX)
            {
                //1.현재바라보는 방향 기준 오른쪽으로 갈수있는가
                if(_board.TIle[PosY + rightY[_dir], PosX + rightX[_dir]] == Board.TileType.Empty)
                {
                    //오른쪽으로 90도 회전 후 전전
                    _dir = (_dir - 1 + 4) % 4;
                    PosX = PosX + frontX[_dir];
                    PosY = PosY+ frontY[_dir];
                    _points.Add(new Pos(PosY, PosX));
                }
                //2.현재 바라보는 방향 기준 전진할 수 있는가
                else if(_board.TIle[PosY + frontY[_dir],  PosX + frontX[_dir]] == Board.TileType.Empty)
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
