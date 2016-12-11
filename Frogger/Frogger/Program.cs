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
        static bool game_over = false;
        static int height = 18;
        static int width = 30;
        static int framerate = 700;
        static int sum = 0;
        static int life = 5;
        static int score = 0;
        static int z = 666;
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

        static void CarSpawn() // Рандомный спавн машин
        {
            car_location = random.Next(1, (height - 10));
            game_ground[car_location, (width - 2)] = car;
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
            Console.Write("Lives: " + life + "  Score: " + score);
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

        static void ReturnFrogToStartPosition()
        {
            game_ground[FrogY, FrogX] = ground;
            FrogX = width / 2;
            FrogY = height - 1;
            game_ground[FrogY, FrogX] = frog;
        }


        static void LeftArrowEvent() // Движение лягушки влево
        {
            if (FrogX != 0)
            {
                if (game_ground[FrogY, FrogX - 1] == car)
                {
                    ReturnFrogToStartPosition();
                    life -= 1; // -жизнь
                }
                else
                {
                    game_ground[FrogY, FrogX] = ground;
                    FrogX--;
                    game_ground[FrogY, FrogX] = frog;
                }
            }
        }
        static void RightArrowEvent() // Движение лягушки вправо
        {
            if (FrogX != (width - 1))
            {
                if (game_ground[FrogY, FrogX + 1] == car)
                {
                    ReturnFrogToStartPosition();
                    life -= 1; // -жизнь
                }
                else
                {
                    game_ground[FrogY, FrogX] = ground;
                    FrogX++;
                    game_ground[FrogY, FrogX] = frog;
                }
            }
        }
        static void UpArrowEvent() // Движение лягушки вверх
        {
            if (FrogY != 0)
            {
                if (game_ground[FrogY - 1, FrogX] == car)
                {
                    ReturnFrogToStartPosition();
                    life -= 1; // -жизнь
                }
                else
                {
                    game_ground[FrogY, FrogX] = ground;
                    FrogY--;
                    game_ground[FrogY, FrogX] = frog;
                }

            }
        }
        static void DownArrowEvent() // Движение лягушки вниз
        {
            if (FrogY != (height - 1))
            {
                if (game_ground[FrogY + 1, FrogX] == car)
                {
                    ReturnFrogToStartPosition();
                    life -= 1; // -жизнь
                }
                else
                {
                    game_ground[FrogY, FrogX] = ground;
                    FrogY++;
                    game_ground[FrogY, FrogX] = frog;
                }
            }
        }

        static void MoveCar() // Ставит автомобили, которые двигаются влево через определенные промежутки времени
        {
            Thread.Sleep(framerate);
            CarSpawn();
            if (sum == 2)
            {
                game_ground[13, 29] = car;
                game_ground[13, 28] = car;
                game_ground[13, 27] = car;
                game_ground[10, 29] = car;
                game_ground[10, 28] = car;
                game_ground[10, 29] = car;
            }
            else if (sum == 10)
            {
                game_ground[9, 24] = car;
                game_ground[9, 29] = car;
                game_ground[9, 28] = car;
                game_ground[9, 27] = car;
                game_ground[9, 26] = car;
                game_ground[9, 25] = car;
                game_ground[15, 29] = car;
                game_ground[15, 28] = car;
                game_ground[15, 29] = car;
                game_ground[15, 28] = car;
                game_ground[10, 29] = car;
                game_ground[10, 28] = car;
                game_ground[10, 29] = car;
                game_ground[10, 28] = car;
            }
            else if (sum == 5)
            {
                game_ground[12, 29] = car;
                game_ground[12, 28] = car;
                game_ground[12, 27] = car;
                game_ground[12, 26] = car;
                game_ground[12, 25] = car;
                game_ground[9, 29] = car;
                game_ground[9, 28] = car;
            }
            else if (sum == 15)
            {
                game_ground[14, 29] = car;
                game_ground[14, 28] = car;
                game_ground[14, 27] = car;
                game_ground[14, 26] = car;
                game_ground[11, 29] = car;
                game_ground[11, 28] = car;

            }
            else if (sum == 20)
            {
                game_ground[15, 29] = car;
                game_ground[15, 28] = car;
                game_ground[15, 27] = car;
                game_ground[15, 26] = car;
                sum = 0; // обнуление счетчика
            }
            carsearch();
            sum++;
        }

        static void carsearch() // Поиск машины в массиве и её передвижение
        {

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (game_ground[i,j] == car) // поиск машины
                    {
                        game_ground[i,j] = ground;
                        if (j != 0) // если столбец не равен 0 - двигать машину
                        {

                            // столкновение с машиной
                            if (game_ground[i,j - 1] == frog)
                            {
                                ReturnFrogToStartPosition();
                                life -= 1; // -жизнь
                            }

                            game_ground[i,j - 1] = car; // движение машины влево
                        }
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
                if (FrogY == 0)
                {
                    ReturnFrogToStartPosition();
                    score += 50;
                    life += 1;
                    framerate -= 25;
                }

            }
        }

        static void EnvironmentEvent() 
        {
            while (!game_over)
            {
                MoveCar();
                Console.Clear();
                PrintMap();
                if (life <= 0)
                    game_over = true;
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
