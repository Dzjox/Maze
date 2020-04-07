using System;

namespace MazeStructure
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
            Maze MyMaze = new Maze(21);
            MyMaze.ShowMaze();
            MyMaze.ShowGoodLookMaze();
            Console.ReadKey();
            } while (true);

        }
    }
}
