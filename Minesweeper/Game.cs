using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Minesweeper
{
    public class Game
    {
        int boardLength = 8;
        List<Field> fieldList;
        Field topLeft;
        private bool gameOver = false;
        int bombCount;
        DateTime startTime;
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
            var x = Enumerable.Range(0, boardLength * boardLength).Select(_ => false)
                .ToArray();
            bombCount = (int)Math.Round(x.Length * 0.16m, 0);
            for (int i = 0; i <= bombCount; i++)
            {
                x[rand.Next(x.Length)] = true;
            }
            fieldList = x.Select(x => new Field(x)).ToList();
            topLeft = fieldList.First();
            for (int i = 0; i < boardLength; i++)
            {
                for (int j = 0; j < boardLength; j++)
                {
                    var field = fieldList[i * boardLength + j];
                    Field top = null;
                    Field bottom = null;
                    Field right = null;
                    Field left = null;

                    if (i != 0)
                    {
                        top = fieldList[i * boardLength + j - boardLength];
                    }

                    if (i != boardLength - 1)
                    {
                        bottom = fieldList[i * boardLength + j + boardLength];
                    }

                    if (j != boardLength - 1)
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
            SetRandomMines();

            Console.WriteLine("Welcome to Minesweeper.");
            Console.WriteLine($"There are {boardLength * boardLength} fields from which are {(int)bombCount} mines.");
            Console.WriteLine($"Avoid those to win.");


            DrawBoard();

            Console.ReadLine();

            startTime = DateTime.UtcNow;
            do
            {
                UserXYInput();

            } while (!gameOver);
        }
        private void AddFields(int amount)
        {
            List<Field> fields = new List<Field>();
            for (int i = 0; i < amount * amount; i++)
            {
                Field field = new Field(false);
                fields.Add(field);
            }


        }
        private void DrawBoard()
        {
            StringBuilder builder = new StringBuilder("     ");
            for (int a = 0; a < boardLength; a++)
            {
                builder.AppendFormat("  {0,2}. ", a + 1);
            }
            builder.AppendLine();
            var row = topLeft;
            int count = 1;
            while (row != null)
            {
                var currentField = row;
                builder.AppendLine(string.Join("", Enumerable.Repeat(0, 5 + boardLength * 6).Select(_ => '-')));
                builder.AppendFormat("{0,2}.  ", count++);
                while (currentField != null)
                {

                    builder.Append($"|  {currentField.GetRepresentation()}  ");

                    currentField = currentField.Right;
                }

                row = row.Bottom;
                builder.AppendLine("|");
                
            }

            Console.WriteLine(builder);
        }
        private void UserXYInput()
        {
            Console.Clear();
            DrawBoard();

            int x = 0;
            int y = 0;

            do
            {
                string xInput = ConsoleHelper.ReadLine("Which cordinate on the x axis do you choose? (m to enter mark mode) ");
                if(xInput == "m")
                {
                    MarkField();
                    DrawBoard();
                    return;
                }
                string yInput = ConsoleHelper.ReadLine("Which cordinate on the y axis do you choose? (m to enter mark mode) ");
                if(yInput == "m")
                {
                    MarkField();
                    DrawBoard();
                    return;
                }

                if (int.TryParse(xInput, out var xNum) && int.TryParse(yInput, out var yNum))
                {
                    if (xNum > 0 && xNum <= boardLength && yNum > 0 && yNum <= boardLength)
                    {
                        x = xNum;
                        y = yNum;
                        break;
                    }
                } 

            } while (true);

            Field field = fieldList[(y - 1) * boardLength + (x-1)];

            field.SetVisited();

            if(DidPlayerWin())
            {
                Console.Clear();
                DrawBoard();
                var elapsed = DateTime.UtcNow - startTime;
                gameOver = true;
                Console.WriteLine("You've uncovered every safe field. You win!");
                Console.WriteLine($"Your time: {elapsed.ToString(@"mm\:ss\.fff")} seconds");
                Console.WriteLine("\nPress any key to exit");
                Console.ReadKey();
            }

            if(field.isBomb)
            {
                Console.Clear();
                fieldList.ForEach(x => x.SetVisited());
                DrawBoard();
                var elapsed = DateTime.Now - startTime;

                gameOver = true;
                Console.WriteLine("You've stepped into a mine. It blew up.");
                Console.WriteLine($"Your time: {elapsed.ToString(@"mm\:ss\.fff")} seconds");
                Console.WriteLine("\nPress any key to exit");
                Console.ReadKey();
            }



        }
        private void MarkField()
        {
            Console.Clear();
            DrawBoard();
            int x = 0;
            int y = 0;
            do
            {
                string xInput = ConsoleHelper.ReadLine("Which cordinate on the x axis do you choose to mark? ");
                string yInput = ConsoleHelper.ReadLine("Which cordinate on the y axis do you choose to mark? ");

                if (int.TryParse(xInput, out var xNum) && int.TryParse(yInput, out var yNum))
                {
                    if (xNum > 0 && xNum <= boardLength && yNum > 0 && yNum <= boardLength)
                    {
                        x = xNum;
                        y = yNum;
                        break;
                    }
                }

            } while (true);

            Field field = fieldList[(y - 1) * boardLength + (x - 1)];

            field.SetMarked();
        }
        private bool DidPlayerWin()
        {
            var notVisited = fieldList.Where(f => !f.visited).Count();
            return notVisited == bombCount;
        }
    }
}
