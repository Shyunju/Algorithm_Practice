using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    class Player
    {
        public int PosY { get; private set; }
        public int PosX { get; private set; }

        Random _random = new Random();
        Board _board;

        public void Initialize(int posY, int posX, int destY, int destX, Board board)
        {
            PosX = posX;
            PosY = posY;

            _board = board;
        }

        const int MOVE_TICK = 100;
        int _sumTick = 0;
        public void Update(int deltaTick)
        {
            _sumTick += deltaTick;
            if(_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                //0.1초마다 실행될 로직
                int randVlaue = _random.Next(0, 5);
                switch (randVlaue)
                {
                    case 0:
                        if (PosY - 1 >= 0 && _board.TIle[PosY - 1, PosX] == Board.TileType.Empty)
                            PosY = PosY -1;
                        break;
                    case 1:
                        if (PosY + 1 < _board.Size && _board.TIle[PosY + 1, PosX] == Board.TileType.Empty)
                            PosY = PosY + 1;
                        break;
                    case 2:
                        if (PosX - 1 >= 0 && _board.TIle[PosY , PosX - 1] == Board.TileType.Empty)
                            PosX = PosX - 1;
                        break;
                    case 3:
                        if (PosX + 1 < _board.Size && _board.TIle[PosY, PosX + 1] == Board.TileType.Empty)
                            PosX = PosX + 1;
                        break;
                }
            }
        }
    }
}
