using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper
{
    public class Game
    {

        public void StartGame()
        {
            WriteTitleScreen();

            Console.WriteLine("\n\nPress any key to start.");
            Console.ReadKey();
            Console.WriteLine();

            GameSetup();
        }

        private void WriteTitleScreen()
        {
            String line;
            int lineLength = 0;
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\files\title.txt";
                //Pass the file path and file name to the StreamReader constructor
                using (StreamReader sr = new StreamReader(path))
                {
                    //Read the first line of text
                    line = sr.ReadLine();
                    //Continue to read until you reach end of file
                    while (line != null)
                    {
                        lineLength = line.Length;
                        //write the line to console window
                        Console.WriteLine(line);
                        //Read the next line
                        line = sr.ReadLine();

                    }

                    for (int i = 0; i < lineLength + 5; i++)
                    {
                        Console.Write("-");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Programmed by Mischa Heeb");
                    Console.WriteLine("Original game idea by Curt Johnson");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
        }

        private void GameSetup()
        {
            int userNum = 0;
            do
            {
                Console.Clear();
                WriteTitleScreen();
                string input = ConsoleHelper.ReadLine("What size do you want your board to be? (between 8x8 and 26x26)");

                if (int.TryParse(input, out var num))
                {
                    userNum = num;
                }
            } while (userNum < 8 || userNum > 26);

            AddFields(userNum);


        }

        private void AddFields(int amount)
        {
            List<Field> fields = new List<Field>();
            LinkedList<Field> test = new LinkedList<Field>();
            for (int i = 0; i < amount * amount; i++)
            {
                Field field = new Field(null, null, null, null, false);
                fields.Add(field);
            }

            foreach (Field field in fields)
            {
                int i = fields.IndexOf(field);
                field.SetTBLR(CheckOutOfBounds(fields, i - amount), CheckOutOfBounds(fields, i + amount), CheckOutOfBounds(fields, i - 1), CheckOutOfBounds(fields, i + 1));
            }

            GameManager.SetFields(fields);
        }

        private Field CheckOutOfBounds(List<Field> fields, int index)
        {
            Field field;
            if(index < 0 || index >= fields.Count)
            {
                return null;
            }

            try
            {
                field = fields[index];
                return field;
            } catch(IndexOutOfRangeException e)
            {
                return null;
            }
        }
    }
}
