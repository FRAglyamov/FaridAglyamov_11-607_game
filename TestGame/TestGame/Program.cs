using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestGameTry
{
    class Program
    {
        static Random random = new Random();
        static ConsoleKeyInfo key_info = new ConsoleKeyInfo();
        //static object Locker = new object();
        static bool game_over = false;
        static int height = 20;
        static int width = 16;
        static int framerate = 1000;
        static int FrogX = width / 2, FrogY = height - 1;
        static char frog = '@';
        static char car = 'O';
        static char ground = ' ';
        static int car_location;
        static char[,] game_ground = new char[height, width];
        static void Frame()
        {
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(width, i);
                Console.WriteLine('|');
            }
            Console.SetCursorPosition(0, height);
            for(int j = 0; j < width; j++)
            {
                Console.Write('-');
            }
        }
        static void Field()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    game_ground[i, j] = ground;
                }
            }
        }

        static void car_location_spawn()
        {
            car_location = random.Next(0, (height - 1));
            game_ground[car_location, (width - 1)] = car;
        }

        static void printmap()
        {
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(0, i);
                for (int j = 0; j < width; j++)
                {
                    Console.Write(game_ground[i, j]);
                }
            }
            Frame();
        }

        static void LeftArrowEvent()
        {
            if (FrogX != 0)
            {
                game_ground[FrogY, FrogX] = ground;
                FrogX--;
                game_ground[FrogY, FrogX] = frog;
            }
        }
        static void RightArrowEvent()
        {
            if (FrogX != (width - 1)) 
            {
                game_ground[FrogY, FrogX] = ground;
                FrogX++;
                game_ground[FrogY, FrogX] = frog;
            }
        }
        static void UpArrowEvent()
        {
            if (FrogY != 0)
            {
                game_ground[FrogY, FrogX] = ground;
                FrogY--;
                game_ground[FrogY, FrogX] = frog;
            }
        }
        static void DownArrowEvent()
        {
            if (FrogY != (height - 1)) 
            {
                game_ground[FrogY, FrogX] = ground;
                FrogY++;
                game_ground[FrogY, FrogX] = frog;
            }
        }

        static void carsearch()
        {
            while (!game_over)
            {
                for (int i = 0; i < height - 1; i++)
                {
                    for (int j = 0; j < width - 1; j++)
                    {
                        if (game_ground[i, j] == car) // searches for a car
                        {
                            game_ground[i, j] = ground;
                            if (game_ground[i, j - 1] == frog)
                            {
                               // game_over = true;
                            }
                            if (j != 0)
                                game_ground[i, j - 1] = car; //moves car left
                        }
                        Console.Clear();
                        printmap();
                        Thread.Sleep(framerate);
                    }
                }
            }
        }
        static void FrogMoveEvent()
        {
            while(true)
            {
                if(Console.KeyAvailable == true)
                {
                    key_info = Console.ReadKey(true);
                    switch (key_info.Key)
                    {
                        case ConsoleKey.RightArrow:
                            RightArrowEvent();
                            break;
                        case ConsoleKey.LeftArrow:
                            LeftArrowEvent();
                            break;
                        case ConsoleKey.UpArrow:
                            UpArrowEvent();
                            break;
                        case ConsoleKey.DownArrow:
                            DownArrowEvent();
                            break;
                    }
                    Console.Clear();
                    printmap();
                }
            }
        }

        static void IfFrogMoveEvent()
        {
            key_info = Console.ReadKey(true);
            if (key_info.Key == ConsoleKey.RightArrow)
                RightArrowEvent();
            else if (key_info.Key == ConsoleKey.LeftArrow)
                LeftArrowEvent();
            else if (key_info.Key == ConsoleKey.UpArrow)
                UpArrowEvent();
            else if (key_info.Key == ConsoleKey.DownArrow)
                DownArrowEvent();
            Console.Clear();
            printmap();
        }

        static void background()
        {
            while (true)
            {
                Console.SetCursorPosition(10, 10);
                Console.WriteLine("thread!");
                Thread.Sleep(500);
                car_location_spawn();
                carsearch();
                //Console.Clear();
                //printmap();
                Thread.Sleep(framerate);
            }
        }


        static void Main()
        {
            Console.CursorVisible = false;
            Field();
            //Task backgroundGame2 = new Task(background);
            Thread backgroundGame = new Thread(background);
            backgroundGame.Start();
            //backgroundGame.IsBackground = true;
            //Task FrogMove2 = new Task(FrogMoveEvent);
            Thread FrogMove = new Thread(FrogMoveEvent);
            FrogMove.Start();
            //Task.Run(new Action(background));

            //while (!game_over)
            //{
                //FrogMoveEvent();
            //}
        }
    }
}
