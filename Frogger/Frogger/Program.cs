using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Frogger
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


        static void Frame() // Прорисовка рамки
        {
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(width, i);
                Console.WriteLine('|');
            }
            Console.SetCursorPosition(0, height);
            for (int j = 0; j < width; j++)
            {
                Console.Write('-');
            }
        }
        static void Field() // Начальное(пустое) поле
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    game_ground[i, j] = ground;
                }
            }
        }

        static void CarSpawn() // Рандомный спавн машин (не нужен)
        {
            car_location = random.Next(0, (height - 1));
            game_ground[car_location, (width - 1)] = car;
        }

        static void PrintMap() // Прорисовка поля игры
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

        static void ShowMenu() // Запуск меню
        {
            Console.Clear();
            Console.Write("\n\n");
            Console.Write(" Frogger Game \n\n\n");
            Console.Write(" Press 'S' for start game!\n");
            Console.Write(" Press 'H' for showing rule!\n");
            Console.Write(" Press 'Esc' for exit!\n");
            key_info = Console.ReadKey(true);
            switch (key_info.Key)
            {
                case ConsoleKey.S:
                    StartGame();
                    break;
                case ConsoleKey.H:
                    HowToPlay();
                    break;
                case ConsoleKey.Escape:
                    Environment.Exit(0);
                    break;
                default:
                    ShowMenu();
                    break;
            }
        }

        static void HowToPlay() // Меню с управлением и целью игры
        {
            Console.Clear();
            Console.Write("\n\n\n\n");
            Console.Write(" Key Settings: \n");
            Console.Write(" Use Arrows for moving \n\n");
            Console.Write(" Mission: \n");
            Console.Write(" You must cross the road without falling under car\n\n\n\n");
            Console.Write(" Press 'B' for returning back\n");
            key_info = Console.ReadKey(true);
            switch (key_info.Key)
            {
                case ConsoleKey.B:
                    ShowMenu();
                    break;
                default:
                    HowToPlay();
                    break;
            }
        }


        static void LeftArrowEvent() // Движение лягушки влево
        {
            if (FrogX != 0)
            {
                game_ground[FrogY, FrogX] = ground;
                FrogX--;
                game_ground[FrogY, FrogX] = frog;
            }
        }
        static void RightArrowEvent() // Движение лягушки вправо
        {
            if (FrogX != (width - 1))
            {
                game_ground[FrogY, FrogX] = ground;
                FrogX++;
                game_ground[FrogY, FrogX] = frog;
            }
        }
        static void UpArrowEvent() // Движение лягушки вверх
        {
            if (FrogY != 0)
            {
                game_ground[FrogY, FrogX] = ground;
                FrogY--;
                game_ground[FrogY, FrogX] = frog;
            }
        }
        static void DownArrowEvent() // Движение лягушки вниз
        {
            if (FrogY != (height - 1))
            {
                game_ground[FrogY, FrogX] = ground;
                FrogY++;
                game_ground[FrogY, FrogX] = frog;
            }
        }

        static void CarSearch() // Поиск машины в массиве и её передвижение
        {
            while (!game_over)
            {
                for (int i = 0; i < height - 1; i++)
                {
                    for (int j = 0; j < width - 1; j++)
                    {
                        if (game_ground[i, j] == car)
                        {
                            game_ground[i, j] = ground;
                            if (game_ground[i, j - 1] == frog)
                            {
                                game_over = true;
                            }
                            if (j != 0)
                                game_ground[i, j - 1] = car;
                        }
                        Console.Clear();
                        PrintMap();
                        Thread.Sleep(framerate);
                    }
                }
            }
        }
        static void FrogMoveEvent() // Общий метод движения лягушки
        {
            while (!game_over)
            {
                if (Console.KeyAvailable == true)
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
                        case ConsoleKey.Escape:
                            Environment.Exit(0);
                            break;
                    }
                    Console.Clear();
                    PrintMap();
                }
            }
        }

        static void EnvironmentEvent()
        {
            while (!game_over)
            {
                CarSpawn();
                CarSearch();
                Thread.Sleep(framerate);
            }
        }

        static void StartGame() // Запуск игры
        {
            Field();
            Thread backgroundGame = new Thread(EnvironmentEvent);
            backgroundGame.Start();
            Thread FrogMove = new Thread(FrogMoveEvent);
            FrogMove.Start();
        }




        static void Main()
        {
            Console.Title = "Frogger Game";
            Console.CursorVisible = false;
            ShowMenu();
        }
    }
}
