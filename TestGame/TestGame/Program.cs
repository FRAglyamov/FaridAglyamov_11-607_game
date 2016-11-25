using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Xsl;
using ConsoleGame;

namespace TestGame
{
    class Program
    {
        static Random random = new Random();
        static ConsoleKeyInfo key_info = new ConsoleKeyInfo();
        static Thread backgroundGame = new Thread(background);
        static Thread MoveCar = new Thread(carsearch);
        static bool game_over = false;
        static int height = Console.WindowHeight - 5;
        static int width = Console.WindowWidth / 2;
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
            //Console.SetCursorPosition((width-1), car_location);
            //Console.Write(car);
            game_ground[car_location, (width - 1)] = car;
            printmap();
            //Thread.Sleep(framerate);
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
                                game_over = true;
                            }
                            if (j != 0)
                                game_ground[i, j - 1] = car; //moves car left
                        }
                        Thread.Sleep(framerate);
                    }
                }
            }
        }
        static void FrogMoveEvent()
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
        }

        static void background()
        {
            while (!game_over)
            {
                Console.Clear();
                Frame();
                car_location_spawn();
                //carsearch();
                //Console.SetCursorPosition(0, 0);
                //Console.SetCursorPosition(FrogY, FrogX);
                //Console.Write(frog);
                Thread.Sleep(framerate);
            }
        }


        static void Main(string[] args)
        {
            //Renderer renderer = new Renderer();
            //renderer.Draw();
            //renderer.Start();
            Console.CursorVisible = false;
            Field();
            Frame();

            //backgroundGame.Start();
            //backgroundGame.IsBackground = true;
            //car_location_spawn();
            MoveCar.Start();
            MoveCar.IsBackground = true;
            while (!game_over)
            {
                FrogMoveEvent();
                printmap();
                car_location_spawn();
                //carsearch();
            }
        }
    }
}
