using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class GameManager
    {
        public static List<Field> Fields { get; private set; }

        public static void SetFields(List<Field> input)
        {
            Fields = input;
        }
    }

    
}
