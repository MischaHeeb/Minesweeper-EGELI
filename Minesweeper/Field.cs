
namespace Minesweeper
{
    public class Field
    {
        public Field Top { get; private set; }
        public Field Left { get; private set; }
        public Field Right { get; private set; }
        public Field Bottom { get; private set; }

        public bool isBomb { get; private set; }
        public bool visited { get; private set; } = false;
        public bool marked { get; private set; } = false;
        private int BombsCount { get => isBomb ? 1 : 0; }

        public Field(bool isBomb)
        {
            this.isBomb = isBomb;
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

        public void SetMarked()
        {
            if (marked)
            {
                marked = false;
            } else
            {
                marked = true;
            }
            
        }

        public int GetBombsAroundMe()
        {
            int counter = 0;
            if(Top != null)
            {
                counter += Top.BombsCount;

                if(Top.Left != null)
                {
                    counter += Top.Left.BombsCount;
                }
                if(Top.Right != null)
                {
                    counter += Top.Right.BombsCount;
                }
            }

            if (Bottom != null)
            {
                counter += Bottom.BombsCount;

                if (Bottom.Left != null)
                {
                    counter += Bottom.Left.BombsCount;
                }
                if (Bottom.Right != null)
                {
                    counter += Bottom.Right.BombsCount;
                }
            }

            if(Right != null)
            {
                counter += Right.BombsCount;
            }

            if(Left != null)
            {
                counter += Left.BombsCount;

            }

            return counter;
        }

        public string GetRepresentation()
        {
            if (!visited)
            {
                if(marked)
                {
                    return "!";
                }
                return "_";

            }
            if (isBomb)
            {
                return "x";
            }
            var bombs = GetBombsAroundMe();
            return bombs.ToString();
        }
    }
}
