using System;

namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(10, 16);
            board.PrintBoard();
        }
    }
}
