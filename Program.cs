using System;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            board.Initialize(25);


            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;

            while(true)
            {
                Console.SetCursorPosition(0, 0);
                board.Render();
            }
            
        }
    }
}
