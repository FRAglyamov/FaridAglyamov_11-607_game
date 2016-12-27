using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Frogger;

namespace Frogger
{
    class Program
    {
        static void Main()
        {
            Console.Title = "Frogger Game";
            Console.CursorVisible = false;
            Game game = new Game();
            game.ShowMenu();
        }
    }
}
