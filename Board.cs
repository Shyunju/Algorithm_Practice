﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    
    class Board
    {
        const char CIRCLE = '\u25cf';
        public TileType[,] TIle { get; private set; }
        public int Size { get; private set; }

        Player _player;

        public enum TileType
        {
            Empty,
            Wall,
        }
        public void Initialize(int size, Player player)
        {
            if (size % 2 == 0)
                return;

            _player = player;

            TIle = new TileType[size, size];
            Size = size;

            //GenerateByBinaryTree();
            GenerateBySideWinder();
        }
        void GenerateByBinaryTree()
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                    {
                        TIle[y, x] = TileType.Wall;
                    }
                    else
                        TIle[y, x] = TileType.Empty;
                }
            }

            //랜덤으로 우 혹은 하로 길을 뚫는 작업
            Random rand = new Random();
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;
                    if (y == Size - 2 && x == Size - 2)
                        continue;

                    if (y == Size - 2)
                    {
                        TIle[y, x + 1] = TileType.Empty;
                        continue;
                    }
                    if (x == Size - 2)
                    {
                        TIle[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    if (rand.Next(0, 2) == 0)
                    {

                        TIle[y, x + 1] = TileType.Empty;  //우
                    }
                    else
                    {
                        TIle[y + 1, x] = TileType.Empty;   //하
                    }
                }
            }
        }
        void GenerateBySideWinder()
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {

                    if (x % 2 == 0 || y % 2 == 0)
                    {
                        TIle[y, x] = TileType.Wall;
                    }
                    else
                        TIle[y, x] = TileType.Empty;
                }
            }

            //랜덤으로 우 혹은 하로 길을 뚫는 작업
            Random rand = new Random();
            for (int y = 0; y < Size; y++)
            {

                int count = 1;
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue;
                    if (y == Size - 2 && x == Size - 2)
                        continue;

                    if (y == Size - 2)
                    {
                        TIle[y, x + 1] = TileType.Empty;
                        continue;
                    }
                    if (x == Size - 2)
                    {
                        TIle[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    if (rand.Next(0, 2) == 0)
                    {

                        TIle[y, x + 1] = TileType.Empty;  //우
                        count++;
                    }
                    else
                    {
                        int randomIndex = rand.Next(0, count);
                        TIle[y + 1, x - randomIndex * 2] = TileType.Empty;   //하
                        count = 1;
                    }
                }
            }
        }
        public void Render()
        {
            ConsoleColor prevColor = Console.ForegroundColor;
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    //플레이어 좌표와 현재 좌표가 일치하면 색상 변경
                    if (y == _player.PosY && x == _player.PosX)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else
                        Console.ForegroundColor = GetTileColor(TIle[y, x]);

                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = prevColor;
        }

        ConsoleColor GetTileColor(TileType type)
        {
            switch(type)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;

                case TileType.Wall:
                    return ConsoleColor.Red;

                default:
                    return ConsoleColor.Green;

            }
        }
    }
}
