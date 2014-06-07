//Based on JustCars and JustSnake!

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Text;

class FallingRocks
{
    struct Obj
    {
        public int X, Y;
        public char C;
        public ConsoleColor Color;
        public Obj(int x, int y, char c, ConsoleColor color)
        {
            this.X = x;
            this.Y = y;
            this.C = c;
            this.Color = color;
        }
    }
    static void PrintOnPosition(int x, int y, char c, ConsoleColor color)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(c);
    }
    static void PrintStringOnPosition(int x, int y, string str, ConsoleColor color)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = color;
        Console.Write(str);
    }

    static void Main()
    {
        Console.Title = "Falling Rocks";
        Console.BufferHeight = Console.WindowHeight = 30;
        Console.BufferWidth = Console.WindowWidth = 60;
        int playfield = Console.WindowWidth - 10;
        int lives = 4;
        double speed = 0;
        int result = 0;
        
        //Welcome TEXT:
        PrintStringOnPosition(5,9, "You are the dwarf (O) at the bottom of the screen.", ConsoleColor.Green);
        PrintStringOnPosition(3, 12, "Use the arrow keys to stay away from the Falling Rocks.", ConsoleColor.Green);
        PrintStringOnPosition(20, 15, "Press ENTER to START!", ConsoleColor.Green);
        Console.ReadLine();
        
        //dwarf
        Obj dwarfLH = new Obj();
        dwarfLH.X = playfield / 2 - 1;
        dwarfLH.Y = Console.WindowHeight - 1;
        dwarfLH.C = '(';
        dwarfLH.Color = ConsoleColor.Green;
        Obj dwarfHead = new Obj();
        dwarfHead.X = dwarfLH.X + 1;
        dwarfHead.Y = dwarfLH.Y;
        dwarfHead.C = 'O';
        dwarfHead.Color = dwarfLH.Color;
        Obj dwarfRH = new Obj();
        dwarfRH.X = dwarfLH.X + 2;
        dwarfRH.Y = dwarfLH.Y;
        dwarfRH.C = ')';
        dwarfRH.Color = dwarfLH.Color;

        Random rnd = new Random();

        //Rocks
        List<Obj> rocks = new List<Obj>();
        
        while (true)
        {
            bool collision = false;
            if (speed < 150)
            {
                speed += 0.5;
            }

            //create rock start
            int rockEntry = rnd.Next(1,3);
            for (int i = 0; i <= rockEntry; i++)
            {
                Obj newRock = new Obj();
                newRock.X = rnd.Next(0, playfield);
                newRock.Y = 0;
                int generateC = rnd.Next(0, 10);
                switch (generateC)
                {
                    case 0: newRock.Color = ConsoleColor.Cyan;
                        newRock.C = '^'; break;
                    case 1: newRock.Color = ConsoleColor.Blue;
                        newRock.C = '@'; break;
                    case 2: newRock.Color = ConsoleColor.Gray;
                        newRock.C = '*'; break;
                    case 3: newRock.Color = ConsoleColor.Magenta;
                        newRock.C = '&'; break;
                    case 4: newRock.Color = ConsoleColor.Red;
                        newRock.C = '+'; break;
                    case 5: newRock.Color = ConsoleColor.White;
                        newRock.C = '%'; break;
                    case 6: newRock.Color = ConsoleColor.Yellow;
                        newRock.C = '$'; break;
                    case 7: newRock.Color = ConsoleColor.Blue;
                        newRock.C = '#'; break;
                    case 8: newRock.Color = ConsoleColor.Cyan;
                        newRock.C = '!'; break;
                    case 9: newRock.Color = ConsoleColor.Yellow;
                        newRock.C = '-'; break;
                    default: ; break;
                }
                rocks.Add(newRock);
            }
            //create rock end

            //Move dwarf start
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                //while (Console.KeyAvailable) Console.ReadKey(true);
                if (pressedKey.Key == ConsoleKey.LeftArrow)
                {
                    if (dwarfLH.X - 1 >= 0)
                    {
                        dwarfLH.X--;
                    }
                }
                else if (pressedKey.Key == ConsoleKey.RightArrow)
                {
                    if (dwarfLH.X + 2 + 1 <= playfield)
                    {
                        dwarfLH.X++;
                    }
                }
            }
            dwarfHead.X = dwarfLH.X + 1;
            dwarfRH.X = dwarfLH.X + 2;
            //Move dwarf end

            //Move rocks start
            List<Obj> movedRocks = new List<Obj>();
            for (int i = 0; i < rocks.Count; i++)
            {
                Obj oldRock = rocks[i];
                Obj movedRock = new Obj();
                movedRock.X = oldRock.X;
                movedRock.Y = oldRock.Y + 1;
                movedRock.C = oldRock.C;
                movedRock.Color = oldRock.Color;
                if (movedRock.Y == dwarfHead.Y && (movedRock.X == dwarfHead.X || movedRock.X == dwarfLH.X || movedRock.X == dwarfRH.X))
                {
                    lives--;
                    PrintOnPosition(movedRock.X, movedRock.Y, 'x', ConsoleColor.DarkRed);
                    collision = true;
                    if (lives < 0)
                    {
                        PrintStringOnPosition(25, 14, "GAME OVER!", ConsoleColor.Red);
                        PrintStringOnPosition(20, 16, "Press ENTER to exit!", ConsoleColor.Red);
                        Console.WriteLine();
                        Environment.Exit(0);
                    }
                }
                if (movedRock.Y <= Console.WindowHeight - 1)
                {
                    movedRocks.Add(movedRock);
                }
            }
            rocks = movedRocks;

            //Move rocks end
            Console.Clear();


            //draw rocks start
            foreach (var rock in rocks)
            {
                PrintOnPosition(rock.X, rock.Y, rock.C, rock.Color);
            }
            //draw rocks end

            //draw the dwarf
            if (collision)
            {
                rocks.Clear();
                speed = 0;
                PrintOnPosition(dwarfLH.X, dwarfLH.Y, '_', ConsoleColor.Red);
                PrintOnPosition(dwarfHead.X, dwarfHead.Y, 'x', ConsoleColor.Red);
                PrintOnPosition(dwarfRH.X, dwarfRH.Y, '_', ConsoleColor.Red);
            }
            else
            {
                PrintOnPosition(dwarfLH.X, dwarfLH.Y, dwarfLH.C, dwarfLH.Color);
                PrintOnPosition(dwarfHead.X, dwarfHead.Y, dwarfHead.C, dwarfHead.Color);
                PrintOnPosition(dwarfRH.X, dwarfRH.Y, dwarfRH.C, dwarfRH.Color);
            }
            //draw rocks end

            PrintStringOnPosition(playfield + 3, Console.WindowHeight / 2 - 5, "Lives: ", ConsoleColor.Green);
            PrintStringOnPosition(playfield + 3, Console.WindowHeight / 2 - 4, "" + lives, ConsoleColor.Green);
            PrintStringOnPosition(playfield + 3, Console.WindowHeight / 2 + 4, "Result:", ConsoleColor.DarkGreen);
            PrintStringOnPosition(playfield + 3, Console.WindowHeight / 2 + 5, "" + result, ConsoleColor.DarkGreen);

            //draw a wall for the result/lives section:
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                PrintOnPosition(playfield + 1, i, 'H', ConsoleColor.DarkGray);
            }

            Thread.Sleep((int)(200 - speed));
            result++;
        }
    }
}
