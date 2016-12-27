using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace Frogger
{
    class Game
    {
        Random random = new Random();
        ConsoleKeyInfo key_info = new ConsoleKeyInfo();
        bool game_over = false;
        static int height = 18;
        static int width = 30;
        int framerate = 700;
        int sum = 0;
        int life = 3;
        int score = 0;
        int FrogX = 30 / 2, FrogY = 18 - 1;
        char frog = '@';
        char carR = '←';
        char carL = '→';
        char ground = ' ';
        int car_location;
        char[,] game_ground = new char[18, 30];

        //Класс записи рекорда
        public class MyRecord
        {
            public string Name { get; set; }
            public int Record { get; set; }
        }
        List<MyRecord> allRecords = new List<MyRecord>();


        /// <summary>
        /// Прорисовка рамки
        /// </summary>
        void Frame()
        {
            //Console.ForegroundColor = ConsoleColor.DarkGray;
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
        /// <summary>
        /// Начальное(пустое) поле
        /// </summary>
        void Field()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    game_ground[i, j] = ground;
                }
            }
        }

        /// <summary>
        /// Прорисовка поля игры
        /// </summary>
        void PrintMap()
        {
            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(0, i);
                for (int j = 0; j < width; j++)
                {
                    //if (game_ground[i, j] == carR)
                    //    Console.ForegroundColor = ConsoleColor.Red;
                    //else if (game_ground[i, j] == carL)
                    //    Console.ForegroundColor = ConsoleColor.Blue;
                    //else if(game_ground[i, j] == frog)
                    //    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(game_ground[i, j]);
                    //Console.ResetColor();
                }
            }

            Frame();
            Console.WriteLine();
            Console.Write("Lives: " + life + "  Score: " + score);
        }

        /// <summary>
        /// Запуск меню
        /// </summary>
        public void ShowMenu()
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

        /// <summary>
        /// Меню с управлением и целью игры
        /// </summary>
        void HowToPlay()
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

        void ReturnFrogToStartPosition()
        {
            game_ground[FrogY, FrogX] = ground;
            FrogX = width / 2;
            FrogY = height - 1;
            game_ground[FrogY, FrogX] = frog;
        }

        /// <summary>
        /// Движение лягушки влево
        /// </summary>
        void LeftArrowEvent()
        {
            if (FrogX != 0)
            {
                if ((game_ground[FrogY, FrogX - 1] == carR) || (game_ground[FrogY, FrogX - 1] == carL))
                {
                    ReturnFrogToStartPosition();
                    life -= 1; // -1 жизнь
                }
                else
                {
                    game_ground[FrogY, FrogX] = ground;
                    FrogX--;
                    game_ground[FrogY, FrogX] = frog;
                }
            }
        }
        /// <summary>
        /// Движение лягушки вправо
        /// </summary>
        void RightArrowEvent()
        {
            if (FrogX != (width - 1))
            {
                if ((game_ground[FrogY, FrogX + 1] == carR) || (game_ground[FrogY, FrogX + 1] == carL))
                {
                    ReturnFrogToStartPosition();
                    life -= 1; // -1 жизнь
                }
                else
                {
                    game_ground[FrogY, FrogX] = ground;
                    FrogX++;
                    game_ground[FrogY, FrogX] = frog;
                }
            }
        }
        /// <summary>
        /// Движение лягушки вверх
        /// </summary>
        void UpArrowEvent()
        {
            if (FrogY != 0)
            {
                if ((game_ground[FrogY - 1, FrogX] == carR) || (game_ground[FrogY - 1, FrogX] == carL))
                {
                    ReturnFrogToStartPosition();
                    life -= 1; // -1 жизнь
                }
                else
                {
                    game_ground[FrogY, FrogX] = ground;
                    FrogY--;
                    game_ground[FrogY, FrogX] = frog;
                }

            }
        }
        /// <summary>
        /// Движение лягушки вниз
        /// </summary>
        void DownArrowEvent()
        {
            if (FrogY != (height - 1))
            {
                if ((game_ground[FrogY + 1, FrogX] == carR) || (game_ground[FrogY + 1, FrogX] == carL))
                {
                    ReturnFrogToStartPosition();
                    life -= 1; // -1 жизнь
                }
                else
                {
                    game_ground[FrogY, FrogX] = ground;
                    FrogY++;
                    game_ground[FrogY, FrogX] = frog;
                }
            }
        }

        /// <summary>
        /// Рандомный спавн машин
        /// </summary>
        void CarSpawn()
        {
            car_location = random.Next(1, height / 2);
            game_ground[car_location, width - 1] = carR;
            //car_location = random.Next(height / 2, height - 1);
            //game_ground[car_location, width - 1] = carR;
            car_location = random.Next(height / 2 + 1, height - 1);
            game_ground[car_location, 0] = carL;
        }

        /// <summary>
        /// Ставит автомобили, которые двигаются через определенные промежутки времени
        /// </summary>
        void CarEvent()
        {
            Thread.Sleep(framerate);
            CarSpawn();
            /*if (sum == 2)
            {
                game_ground[13, 29] = carR;
                game_ground[13, 28] = carR;
                game_ground[13, 27] = carR;
                game_ground[10, 29] = carR;
                game_ground[10, 28] = carR;
                game_ground[10, 29] = carR;
            }
            else if (sum == 10)
            {
                game_ground[9, 24] = carR;
                game_ground[9, 29] = carR;
                game_ground[9, 28] = carR;
                game_ground[9, 27] = carR;
                game_ground[9, 26] = carR;
                game_ground[9, 25] = carR;
                game_ground[15, 29] = carR;
                game_ground[15, 28] = carR;
                game_ground[15, 29] = carR;
                game_ground[15, 28] = carR;
                game_ground[10, 29] = carR;
                game_ground[10, 28] = carR;
                game_ground[10, 29] = carR;
                game_ground[10, 28] = carR;
            }
            else if (sum == 5)
            {
                game_ground[12, 29] = carR;
                game_ground[12, 28] = carR;
                game_ground[12, 27] = carR;
                game_ground[12, 26] = carR;
                game_ground[12, 25] = carR;
                game_ground[9, 29] = carR;
                game_ground[9, 28] = carR;
            }
            else if (sum == 15)
            {
                game_ground[14, 29] = carR;
                game_ground[14, 28] = carR;
                game_ground[14, 27] = carR;
                game_ground[14, 26] = carR;
                game_ground[11, 29] = carR;
                game_ground[11, 28] = carR;

            }
            else if (sum == 20)
            {
                game_ground[15, 29] = carR;
                game_ground[15, 28] = carR;
                game_ground[15, 27] = carR;
                game_ground[15, 26] = carR;
                sum = 0; // обнуление счетчика
            }
            sum++;*/
            CarMove();
        }

        /// <summary>
        /// Поиск машины в массиве и её передвижение
        /// </summary>
        void CarMove()
        {

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {

                    if (game_ground[i, j] == carR) // поиск машины, которая едет справа
                    {
                        game_ground[i, j] = ground;
                        if (j != 0) // если столбец не равен 0 - двигать машину
                        {
                            // столкновение с машиной
                            if (game_ground[i, j - 1] == frog)
                            {
                                ReturnFrogToStartPosition();
                                life -= 1; // -1 жизнь
                            }
                            game_ground[i, j - 1] = carR; // движение машины влево
                        }
                    }

                    if (game_ground[i, j] == carL) // поиск машины, которая едет слева
                    {
                        game_ground[i, j] = ground;
                        if (j != width - 1)
                        {
                            // столкновение с машиной
                            if (game_ground[i, j + 1] == frog)
                            {
                                ReturnFrogToStartPosition();
                                life -= 1; // -1 жизнь
                            }
                            game_ground[i, j + 1] = carL; // движение машины вправо
                            j++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Общий метод движения лягушки
        /// </summary>
        void FrogEvent()
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
                    framerate -= 50;
                }
            }
        }

        void EnvironmentEvent()
        {
            while (!game_over)
            {
                CarEvent();
                Console.Clear();
                PrintMap();
                if (life <= 0)
                    game_over = true;
            }
        }

        /// <summary>
        /// Запуск игры
        /// </summary>
        void StartGame()
        {
            Field();
            Thread Car = new Thread(EnvironmentEvent);
            Car.Start();
            Thread Frog = new Thread(FrogEvent);
            Frog.Start();
        }
    }
}
