using System;

namespace ConsoleGame
{
    public class Dot
    {
        public char Symbol { get; set; }

        public Dot()
        {
            Symbol = ' ';
        }

        public override String ToString()
        {
            return Symbol.ToString();
        }
    }
}