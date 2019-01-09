using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGame
{
    static class CardPrinter
    {
        public static void PrintCards(params PlayingCard[] cards)
        {

            int leftMargin = Console.WindowWidth / 2 - (cards.Length * PlayingCard.PlayingCardTemplate[0].Length) / 2;
            int topMargin = Console.WindowHeight / 2 - PlayingCard.PlayingCardTemplate.Length / 2 - 3;

            int x = leftMargin;

            foreach (var card in cards)
            {
                Console.CursorTop = topMargin;
                foreach (var row in card.FaceDown ? PlayingCard.FaceDownCardTemplate : PlayingCard.PlayingCardTemplate)
                {
                    Console.CursorLeft = x;
                    Console.WriteLine(string.Format(row, card.SuitSymbol, card.RankSymbol));
                }
                x += PlayingCard.PlayingCardTemplate[0].Length + 1;

            }
            Console.WriteLine();
        }
    }
}
