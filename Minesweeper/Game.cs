using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Minesweeper
{
    public class Game
    {
        int boardLength = 8;
        public Field GenerateGrid(int height, int width)
        {
            return new Field(true);
        }
        public void StartGame()
        {
            WriteTitleScreen();

            Console.WriteLine("\n\nPress any key to start.");
            Console.ReadKey();
            Console.WriteLine();

            GameSetup();
        }

        private void SetRandomMines()
        {
            Random rand = new Random();
            int count = 0;

            var fieldList = Enumerable.Range(0, boardLength * boardLength).Select(_ => new Field(false)).ToList();
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    var field = fieldList[i * 8 + j];
                    Field top = null;
                    Field bottom = null;
                    Field right = null;
                    Field left = null;

                    if(i != 0)
                    {
                        top = fieldList[i * boardLength + j - boardLength];
                    }

                    if(i != boardLength -1)
                    {
                        bottom = fieldList[i * boardLength + j + boardLength];
                    }

                    if (j != boardLength -1)
                    {
                        right = fieldList[i * boardLength + j + 1];

                    }
                    if (j != 0)
                    {
                        left = fieldList[i * boardLength + j - 1];
                    }
                    field.SetTBLR(top, bottom, left, right);
                }
            }

            int mineCount = 0;
            do
            {
                int index = rand.Next(0, fieldList.Count);

                if(!fieldList[index].isBomb && !fieldList[index].visited)
                {
                    fieldList[index].SetupBomb();
                    mineCount++;
                }
                double percentage = fieldList.Count * 0.16d;
                if (mineCount == Math.Round(percentage))
                {
                    break;
                }
            } while (true);

            Console.WriteLine("\n\nMinecount: " + mineCount);
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
            do
            {
                Console.Clear();
                WriteTitleScreen();
                string input = ConsoleHelper.ReadLine("What size do you want your board to be? (between 8x8 and 26x26)");

                if (int.TryParse(input, out var num))
                {
                    boardLength = num;
                }
            } while (boardLength < 8 || boardLength > 26);

            AddFields(boardLength);

            DrawBoard();

            SetRandomMines();
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


            GameManager.SetFields(fields);
        }

        private void DrawBoard()
        {
            for(int i = 0; i < boardLength; i++)
            {
                for(int j = 0; j < boardLength; j++)
                {
                    Console.Write("_ ");
                }
                Console.WriteLine();
            }
        }

        
    }
}
