using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Field
    {
        public Field Top { get; private set; }
        public Field Left { get; private set; }
        public Field Right { get; private set; }
        public Field Bottom { get; private set; }

        public bool isBomb { get; }
        public bool visited { get; private set; } = false;
        private int BombsCount { get => visited ? 1 : 0; }

        public Field(Field top, Field left, Field right, Field bottom, bool isBomb)
        {
            this.isBomb = isBomb;
            Top = top;
            Bottom = bottom;
            Right = right;
            Left = left;
        }

        public void SetTBLR(Field top, Field bottom, Field left, Field right)
        {
            this.Top = top;
            this.Bottom = bottom;
            this.Left = left;
            this.Right = right;
        }

        public void SetVisited()
        {
            if(visited)
            {
                return;
            }

            visited = true;

            if(GetBombsAroundMe() == 0)
            {
                Right?.SetVisited();
                Left?.SetVisited();
                Top?.SetVisited();
                Bottom?.SetVisited();
                Bottom?.Left?.SetVisited();
                Bottom?.Right?.SetVisited();
                Top?.Left?.SetVisited();
                Top?.Right?.SetVisited();
            }
        }

        public int GetBombsAroundMe()
        {
            int? counter = 0;
            counter += Right?.BombsCount;
            counter += Left?.BombsCount;
            counter += Top?.BombsCount;
            counter += Bottom?.BombsCount;
            counter += Bottom?.Left?.BombsCount;
            counter += Bottom?.Right?.BombsCount;
            counter += Top?.Left?.BombsCount;
            counter += Top?.Right?.BombsCount;

            return (int)counter;
        }
    }
}
