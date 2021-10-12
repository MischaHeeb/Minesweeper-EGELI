using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class ConsoleHelper
    {
        public static string ReadLine(string txt)
        {
            string input = string.Empty;
            do
            {
                Console.WriteLine(txt);
                input = Console.ReadLine();
            } while (input == "");
            return input;
        }
    }
}
