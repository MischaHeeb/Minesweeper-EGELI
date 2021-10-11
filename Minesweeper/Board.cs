using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Board
    {
        private sbyte[,] GameBoard;
        private char[,] BoardChars;
        private bool[,] visited;
        private sbyte size;
        public float MineSpawnChance { get; private set; }
        public sbyte BoardSize
        {
            get
            {
                return size;
            }
            set
            {
                if (value > 8 && value < 26)
                //if (value == 16)
                {
                    size = value;
                }
                else size = 26;
            }
        }
        public Board(sbyte sizeInput, float mineChance)
        {
            BoardSize = sizeInput;
            MineSpawnChance = mineChance;
            GameBoard = new sbyte[sizeInput, sizeInput];
            BoardChars = new char[sizeInput, sizeInput];
            visited = new bool[sizeInput, sizeInput];

            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    GameBoard[i, j] = 0;
                    BoardChars[i, j] = '_';
                    visited[i, j] = false;
                }
            }
        }

        public void PrintBoard()
        {
            
        }
    }
}
