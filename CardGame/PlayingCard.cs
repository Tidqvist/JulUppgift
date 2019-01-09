using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CardGame
{
    public class PlayingCard
    {
        public static List<string> RankSymbols = new List<string> { "E", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };
        public static List<string> SuitSymbols = new List<string> { "♣", "♦", "♥", "♠" };

        public static string[] FaceDownCardTemplate = new string[] { "┌───────────┐", "│░░░░░░░░░░░│", "│░░░░░░░░░░░│", "│░░░░░░░░░░░│", "│░░░░░░░░░░░│", "│░░░░░░░░░░░│", "│░░░░░░░░░░░│", "│░░░░░░░░░░░│", "└───────────┘" };
        public static string[] PlayingCardTemplate = new string[] { "┌───────────┐", "│{0} {1,-2}       │", "│           │", "│           │", "│     {0}     │", "│           │", "│           │", "│       {1,2} {0}│", "└───────────┘" };

        public Suits Suit { get; set; }
        public string SuitSymbol { get { return SuitSymbols[(int)Suit - 1]; } }
        public Ranks Rank { get; set; }
        public string RankSymbol { get { return RankSymbols[(int)Rank - 1]; } }

        public bool FaceDown { get; set; }

        public int Value
        {
            get
            {
                return (int)Rank * 4 + (int)Suit;
            }
        }

        public PlayingCard(Suits suit, Ranks rank, bool faceDown)
        {
            Suit = suit;
            Rank = rank;
            FaceDown = faceDown;
        }

        public void Flip()
        {
            FaceDown = !FaceDown;
        }

        public override string ToString()
        {
            return $"{SuitSymbol} {RankSymbol}";
        }
    }

    public enum Ranks
    {
        Ace = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }

    public enum Suits
    {
        Clubs = 1,
        Diamonds,
        Hearts,
        Spades
    }
}
