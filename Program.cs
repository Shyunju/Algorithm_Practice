using System;

namespace Algorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
            Console.WriteLine("Hello World!");
            //adsfasdfasdf

            const char CIRCLE = '\u25cf';

            for(int i = 0; i < 25; i++)
            {
                for(int j = 0; j< 25; j++)
                {
                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }
        }
    }
}
