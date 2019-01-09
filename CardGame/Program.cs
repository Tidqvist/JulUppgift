using System;
using System.Collections.Generic;
using System.Text;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var game = new HigherOrLower();
            game.Start();
        }
    }
}
